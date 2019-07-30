﻿using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects.Interfaces;
using Core.DataBase.WarThunder.Helpers;
using Core.DataBase.WarThunder.Objects;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Json;
using Core.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using Core.Json.WarThunder.Helpers.Interfaces;
using Core.Organization.Enumerations;
using Core.Organization.Enumerations.Logger;
using Core.Organization.Helpers.Interfaces;
using Core.Randomization.Helpers.Interfaces;
using Core.UnpackingToolsIntegration.Enumerations;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.WarThunderExtractionToolsIntegration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Core.Organization.Helpers
{
    /// <summary> Controls the flow of the application. </summary>
    public class Manager : LoggerFluency, IManager
    {
        #region Constants

        /// <summary>
        /// The maximum difference in battle rating from the battle rating selected by user.
        /// <para> Example: if the difference is 1 and the user chooses 5.7, vehicles of 4.7-5.7 are selected. </para>
        /// </summary>
        protected const decimal _maximumBattleRatingDifference = 2.0m;

        #endregion Constants
        #region Fields

        /// <summary> An instance of a file manager. </summary>
        protected readonly IWarThunderFileManager _fileManager;
        /// <summary> An instance of a file reader. </summary>
        protected readonly IWarThunderFileReader _fileReader;
        /// <summary> An instance of a parser. </summary>
        protected readonly IParser _parser;
        /// <summary> An instance of an unpacker. </summary>
        protected readonly IUnpacker _unpacker;
        /// <summary> An instance of a JSON helper. </summary>
        protected readonly IWarThunderJsonHelper _jsonHelper;
        /// <summary> An instance of a randomizer. </summary>
        protected readonly IRandomizer _randomizer;
        /// <summary> An instance of a vehicle selector. </summary>
        protected readonly IVehicleSelector _vehicleSelector;

        /// <summary> The string representation of the game client version. </summary>
        private readonly string _gameClientVersion;

        /// <summary> An instance of a data repository. </summary>
        protected IDataRepository _dataRepository;
        /// <summary> The cache of persistent objects. </summary>
        protected readonly List<IPersistentObject> _cache;

        #endregion Fields
        #region Properties

        /// <summary> An instance of a settings manager. </summary>
        public IWarThunderSettingsManager SettingsManager { get; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new manager. </summary>
        public Manager
        (
            IWarThunderFileManager fileManager,
            IWarThunderFileReader fileReader,
            IWarThunderSettingsManager settingsManager,
            IParser parser,
            IUnpacker unpacker,
            IWarThunderJsonHelper jsonHelper,
            IRandomizer randomizer,
            IVehicleSelector vehicleSelector,
            params IConfiguredLogger[] loggers
        ) : base(EOrganizationLogCategory.Manager, loggers)
        {
            _fileManager = fileManager;
            _fileReader = fileReader;
            _parser = parser;
            _unpacker = unpacker;
            _jsonHelper = jsonHelper;
            _randomizer = randomizer;
            _vehicleSelector = vehicleSelector;
            _cache = new List<IPersistentObject>();

            _gameClientVersion = _parser.GetClientVersion(_fileReader.ReadInstallData(EClientVersion.Current)).ToString();

            _fileManager.CleanUpTempDirectory();

            SettingsManager = settingsManager;
            LoadSettings();
        }

        #endregion Constructors
        #region Methods: Initialization

        /// <summary> Fills the <see cref="_cache"/> up. </summary>
        public void CacheVehicles()
        {
            var availableDatabaseVersions = _fileManager.GetWarThunderDatabaseVersions();

            if (availableDatabaseVersions.IsEmpty() || !_gameClientVersion.IsIn(availableDatabaseVersions.Max().ToString()))
            {
                LogInfo(EOrganizationLogMessage.NotFoundDatabaseFor.FormatFluently(_gameClientVersion));

                try
                {
                    UnpackDeserializePersist();
                }
                catch
                {
                    var databaseFile = $"{_gameClientVersion}{ECharacter.Period}{EFileExtension.SqLite3}";

                    LogInfo(ECoreLogMessage.Deleting.FormatFluently(databaseFile));

                    _fileManager.DeleteFileSafely(databaseFile);

                    LogInfo(ECoreLogMessage.Deleted.FormatFluently(databaseFile));

                    throw;
                }
            }
            else
            {
                LogInfo(EOrganizationLogMessage.FoundDatabaseFor.FormatFluently(_gameClientVersion));

                _dataRepository = new DataRepositoryWarThunderWithSession(_gameClientVersion, false, Assembly.Load(EAssembly.DataBaseMapping), _loggers);

                LogInfo(EOrganizationLogMessage.DataBaseConnectionEstablished);
            }

            LogInfo(EOrganizationLogMessage.CachingObjects);

            _cache.AddRange(_dataRepository.Query<IVehicle>());

            LogInfo(EOrganizationLogMessage.CachingComplete);
        }

        private IEnumerable<FileInfo> GetBlkxFiles(string sourceFileName)
        {
            var sourceFile = _fileManager.GetFileInfo(Settings.WarThunderLocation, sourceFileName);
            var outputDirectory = new DirectoryInfo(_unpacker.Unpack(sourceFile));

            _unpacker.Unpack(outputDirectory, ETool.BlkUnpacker);

            return outputDirectory.GetFiles($"{ECharacter.Asterisk}{ECharacter.Period}{EFileExtension.Blkx}", SearchOption.AllDirectories);
        }

        private string GetJsonText(IEnumerable<FileInfo> blkxFiles, string unpackedFileName)
        {
            return _fileReader.Read(blkxFiles.First(file => file.Name.Contains(unpackedFileName)));
        }

        /// <summary> Creates the database for the current War Thunder client version. </summary>
        private void CreateDataBase()
        {
            LogInfo(EOrganizationLogMessage.CreatingDatabase);

            _dataRepository = new DataRepositoryWarThunderWithSession(_gameClientVersion, true, Assembly.Load(EAssembly.DataBaseMapping), _loggers);

            LogInfo(EOrganizationLogMessage.DatabaseCreatedConnectionEstablished);
        }

        /// <summary> Unpacks game files, converts them into JSON, deserializes it into objects, and persists them into the database. </summary>
        private void UnpackDeserializePersist()
        {
            CreateDataBase();

            LogInfo(EOrganizationLogMessage.PreparingGameFiles);

            var blkxFiles = GetBlkxFiles(EFile.WarThunder.StatAndBalanceParameters);

            var wpCostJsonText = GetJsonText(blkxFiles, EFile.CharVromfs.GeneralVehicleData);
            var unitTagsJsonText = GetJsonText(blkxFiles, EFile.CharVromfs.AdditionalVehicleData);

            LogInfo(EOrganizationLogMessage.GameFilesPrepared);
            LogInfo(EOrganizationLogMessage.InitializingDatabase);

            var vehicles = _jsonHelper.DeserializeList<Vehicle>(_dataRepository, wpCostJsonText);
            var additionalVehicleData = _jsonHelper.DeserializeList<VehicleDeserializedFromJsonUnitTags>(unitTagsJsonText);

            foreach (var vehicle in vehicles)
                if (additionalVehicleData.FirstOrDefault(item => item.GaijinId == vehicle.GaijinId) is VehicleDeserializedFromJsonUnitTags matchedData)
                    vehicle.DoPostInitalization(matchedData);

            _dataRepository.PersistNewObjects();

            LogInfo(EOrganizationLogMessage.DatabaseInitialized);
        }

        #endregion Methods: Initialization
        #region Methods: Settings

        /// <summary> Loads settings from the file attached to <see cref="SettingsManager"/>. </summary>
        private void LoadSettings()
        {
            Settings.UnpackingToolsLocation = SettingsManager.Load(nameof(Settings.UnpackingToolsLocation));
            Settings.WarThunderLocation = SettingsManager.Load(nameof(Settings.WarThunderLocation));
        }

        #endregion Methods: Settings

        /// <summary> Releases unmanaged resources. </summary>
        public void Dispose() =>
            _dataRepository.Dispose();
    }
}