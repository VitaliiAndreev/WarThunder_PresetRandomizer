using Core.Csv.WarThunder.Helpers.Interfaces;
using Core.DataBase.Helpers;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
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
using Core.Organization.Collections;
using Core.Organization.Enumerations;
using Core.Organization.Enumerations.Logger;
using Core.Organization.Helpers.Interfaces;
using Core.Organization.Objects;
using Core.Organization.Objects.SearchSpecifications;
using Core.Randomization.Helpers.Interfaces;
using Core.UnpackingToolsIntegration.Attributes;
using Core.UnpackingToolsIntegration.Enumerations;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.WarThunderExtractionToolsIntegration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Core.Organization.Helpers
{
    /// <summary> Controls the flow of the application. </summary>
    public class Manager : LoggerFluency, IManager
    {
        #region Fields

        /// <summary> Whether to generate the database. </summary>
        private readonly bool _generateDatabase;
        /// <summary> Whether to read data from JSON instead of the database. </summary>
        private readonly bool _readOnlyJson;
        /// <summary> Parts of Gaijin IDs of vehicles excluded from display. </summary>
        private readonly IEnumerable<string> _excludedGaijinIdParts;
        /// <summary> Playable vehicles loaded into memory. </summary>
        private readonly List<IVehicle> _playableVehicles;

        /// <summary> Whether a new database should be generated. </summary>
        private bool _generateNewDatabase;
        /// <summary> The string representation of the game client version. </summary>
        private string _gameClientVersion;

        /// <summary> An instance of a file manager. </summary>
        protected readonly IWarThunderFileManager _fileManager;
        /// <summary> An instance of a file reader. </summary>
        protected readonly IWarThunderFileReader _fileReader;
        /// <summary> An instance of a settings manager. </summary>
        protected readonly IWarThunderSettingsManager _settingsManager;
        /// <summary> An instance of a parser. </summary>
        protected readonly IParser _parser;
        /// <summary> An instance of an unpacker. </summary>
        protected readonly IUnpacker _unpacker;
        /// <summary> An instance of a converter. </summary>
        protected readonly IConverter _converter;
        /// <summary> An instance of a JSON helper. </summary>
        protected readonly IWarThunderJsonHelper _jsonHelper;
        /// <summary> An instance of a CSV deserializer. </summary>
        protected readonly ICsvDeserializer _csvDeserializer;
        /// <summary> An instance of a randomizer. </summary>
        protected readonly IRandomiser _randomizer;
        /// <summary> An instance of a vehicle selector. </summary>
        protected readonly IVehicleSelector _vehicleSelector;
        /// <summary> An instance of a vehicle selector. </summary>
        protected readonly IPresetGenerator _presetGenerator;

        /// <summary> An instance of a data repository. </summary>
        protected IDataRepository _dataRepository;
        /// <summary> The cache of persistent objects. </summary>
        protected readonly List<IPersistentObject> _cache;

        #endregion Fields
        #region Properties

        /// <summary> Research trees. This collection needs to be filled up after caching vehicles up from the database by calling <see cref="CacheData"/>. </summary>
        public IDictionary<ENation, ResearchTree> ResearchTrees { get; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new manager and loads settings stored in the settings file. </summary>
        /// <param name="generateDatabase"> Whether to read data from JSON instead of the database. </param>
        /// <param name="readOnlyJson"> Whether to generate the database. </param>
        public Manager
        (
            IWarThunderFileManager fileManager,
            IWarThunderFileReader fileReader,
            IWarThunderSettingsManager settingsManager,
            IParser parser,
            IUnpacker unpacker,
            IConverter converter,
            IWarThunderJsonHelper jsonHelper,
            ICsvDeserializer csvDeserializer,
            IRandomiser randomizer,
            IVehicleSelector vehicleSelector,
            IPresetGenerator presetGenerator,
            bool generateDatabase,
            bool readOnlyJson,
            params IConfiguredLogger[] loggers
        ) : base(EOrganizationLogCategory.Manager, loggers)
        {
            _generateDatabase = generateDatabase;
            _readOnlyJson = readOnlyJson;
            _excludedGaijinIdParts = new List<string>()
            {
                "_football",
                "germ_panzerkampflaufer_565_r",
                "germ_panzerkampflaufer_565_r_2",
                "po-2_nw",
                "_race",
                "_space_suit",
                "_tutorial",
                "uk_centaur_aa_mk_",
                "us_m35",
                "us_m35_2",
                "ussr_sht_1",
                "ussr_sht_1_2",
            };
            _fileManager = fileManager;
            _fileReader = fileReader;
            _parser = parser;
            _unpacker = unpacker;
            _converter = converter;
            _jsonHelper = jsonHelper;
            _csvDeserializer = csvDeserializer;
            _randomizer = randomizer;
            _vehicleSelector = vehicleSelector;
            _presetGenerator = presetGenerator;

            _cache = new List<IPersistentObject>();
            _playableVehicles = new List<IVehicle>();

            _settingsManager = settingsManager;
            LoadSettings();

            _fileManager.CleanUpTempDirectory();

            ResearchTrees = new Dictionary<ENation, ResearchTree>();

            LogDebug(ECoreLogMessage.Created.FormatFluently(EOrganizationLogCategory.Manager));
        }

        #endregion Constructors
        #region Methods: Initialization

        /// <summary> Reads and stores the version of the game client. </summary>
        public void InitializeGameClientVersion() =>
            _gameClientVersion = _parser.GetClientVersion(_fileReader.ReadInstallData(EClientVersion.Current)).ToString();

        /// <summary> Initializes memebers of the <see cref="EReference"/> class. </summary>
        private void InitializeReferences()
        {
            var vehicles = _cache.OfType<IVehicle>();
            var nationCountryCombinations = vehicles.Select(vehicle => new { Nation = vehicle.Nation.AsEnumerationItem, vehicle.Country }).Distinct();
            var baseNationCountryCombinations = nationCountryCombinations.Where(combination => combination.Nation.GetBaseCountry() == combination.Country);

            EReference.MaximumEconomicRank = vehicles.Max
            (
                vehicle => vehicle
                    .EconomicRank
                    .AsDictionary()
                    .Values
                    .Where(nullableValue => nullableValue.HasValue)
                    .Max(nullableValue => nullableValue.Value)
            );

            foreach (var combination in baseNationCountryCombinations) // The base countries need to be first in each nation.
            {
                EReference.CountriesByNation.Append(combination.Nation, combination.Country);
            }
            foreach (var combination in nationCountryCombinations.Except(baseNationCountryCombinations))
            {
                EReference.CountriesByNation.Append(combination.Nation, combination.Country);
                EReference.NationsByCountry.Append(combination.Country, combination.Nation);
            }
        }

        /// <summary> Initializes research trees from cached vehicles. Obviously, should be called after <see cref="CacheData"/>. </summary>
        private void InitializeResearchTrees()
        {
            LogInfo(EOrganizationLogMessage.InitializingResearchTrees);

            var columnCount = default(int);

            foreach (var vehicle in _playableVehicles)
            {
                if (vehicle.ResearchTreeData is null)
                    continue;

                var nation = vehicle.Nation.AsEnumerationItem;
                var branch = vehicle.Branch.AsEnumerationItem;
                var rank = vehicle.Rank.CastTo<ERank>();
                var columnNumber = vehicle.ResearchTreeData.CellCoordinatesWithinRank.First();
                var rowNumber = vehicle.ResearchTreeData.CellCoordinatesWithinRank.Last();

                if (columnNumber > columnCount)
                    columnCount = columnNumber;

                ResearchTrees
                    .GetWithInstantiation(nation)
                    .GetWithInstantiation(branch)
                    .GetWithInstantiation(rank)
                    .Add(new ResearchTreeCoordinatesWithinRank(columnNumber, rowNumber, vehicle.ResearchTreeData.FolderIndex), vehicle)
                ;
            }

            foreach (var branch in ResearchTrees.Values.SelectMany(reseatchTree => reseatchTree.Values))
                branch.InitializeProperties(columnCount);

            LogInfo(EOrganizationLogMessage.ResearchTreesInitialized);
        }

        /// <summary> Tries to unpack game files, convert them into JSON, deserialise it into objects, and persist them into the database. </summary>
        private void TryToUnpackDeserialiseAndPersist()
        {
            try
            {
                UnpackDeserializePersist();
            }
            catch
            {
                if (_generateDatabase && _generateNewDatabase)
                {
                    var databaseFileName = $"{_gameClientVersion}{ECharacter.Period}{EFileExtension.SqLite3}";
                    var databaseJournalFileName = $"{databaseFileName}-journal";

                    _fileManager.DeleteFiles(new string[] { databaseFileName, databaseJournalFileName });
                }
                throw;
            }
        }

        /// <summary> Initialises the <see cref="_dataRepository"/> with a as a <see cref="DataRepositoryInMemory"/>. </summary>
        private void InitialiseDatabaselessDataRepository()
        {
            TryToUnpackDeserialiseAndPersist();
        }

        /// <summary> Checks for an existing database and initialises the <see cref="_generateNewDatabase"/> field appropriately. </summary>
        private void CheckForExistingDatabase()
        {
            var availableDatabaseVersions = _fileManager.GetWarThunderDatabaseVersions();

            if (availableDatabaseVersions.IsEmpty() || !_gameClientVersion.IsIn(availableDatabaseVersions.Max().ToString()))
            {
                LogInfo(EOrganizationLogMessage.NotFoundDatabaseFor.FormatFluently(_gameClientVersion));
                _generateNewDatabase = true;
            }
            else
            {
                LogInfo(EOrganizationLogMessage.FoundDatabaseFor.FormatFluently(_gameClientVersion));
                _generateNewDatabase = false;
            }
        }

        /// <summary> Initialises the <see cref="_dataRepository"/> with a as a <see cref="DataRepositoryWarThunderWithSession"/>. </summary>
        private void InitialiseDatabaseBasedDataRepository()
        {
            if (_generateNewDatabase)
            {
                TryToUnpackDeserialiseAndPersist();
            }
            else
            {
                _dataRepository = new DataRepositoryWarThunderWithSession(_gameClientVersion, false, Assembly.Load(EAssembly.DataBaseMapping), _loggers);

                LogInfo(EOrganizationLogMessage.DatabaseConnectionEstablished);
            }
        }

        /// <summary> Initialises the <see cref="_dataRepository"/>. </summary>
        private void InitialiseDataRepository()
        {
            if (_generateDatabase)
                CheckForExistingDatabase();

            if (_readOnlyJson)
                InitialiseDatabaselessDataRepository();
            else
                InitialiseDatabaseBasedDataRepository();
        }

        /// <summary> Fills the <see cref="_cache"/> up. </summary>
        public void CacheData()
        {
            InitialiseDataRepository();

            LogInfo(EOrganizationLogMessage.CachingObjects);

            if (_cache.IsEmpty())
                _cache.AddRange(_dataRepository.Query<IVehicle>());

            _playableVehicles.AddRange(_cache.OfType<IVehicle>().Where(vehicle => !vehicle.GaijinId.ContainsAny(_excludedGaijinIdParts)).ToList());
            _presetGenerator.SetPlayableVehicles(_playableVehicles);

            LogInfo(EOrganizationLogMessage.CachingComplete);

            InitializeReferences();
            InitializeResearchTrees();
        }

        /// <summary> Unpacks a file with the speficied name and gets files of the given type from its contents, doing conversions if necessary. </summary>
        /// <param name="fileType"> The type of files to search for after unpacking. </param>
        /// <param name="packedFileName"> The name of the packed file. </param>
        /// <param name="warThunderSubdirectory"> War Thunder subdirectory where to look for the <paramref name="packedFileName"/>. </param>
        /// <param name="processedSubdirectory"> Subdirectory of the output directory that requires processing (if only a subset of data is required). </param>
        /// <returns></returns>
        internal IEnumerable<FileInfo> GetFiles(string fileType, string packedFileName, string warThunderSubdirectory = "", string processedSubdirectory = "")
        {
            var sourceFile = _fileManager.GetFileInfo(Path.Combine(Settings.WarThunderLocation, warThunderSubdirectory), packedFileName);
            var outputDirectory = new DirectoryInfo(Path.Combine(_unpacker.Unpack(sourceFile), processedSubdirectory));

            switch (fileType)
            {
                case EFileExtension.Blkx:
                {
                    _unpacker.Unpack(outputDirectory, ETool.BlkUnpacker);
                    break;
                }
                case EFileExtension.Png:
                {
                    _unpacker.Unpack(outputDirectory, ETool.DdsxUnpacker);
                    _converter.ConvertDdsToPng(outputDirectory, SearchOption.AllDirectories);
                    break;
                }
            }

            return outputDirectory.GetFiles($"{ECharacter.Asterisk}{ECharacter.Period}{fileType}", SearchOption.AllDirectories);
        }

        /// <summary> Unpacks a file with the speficied name as a BIN file and returns BLK files it contains converted into BLKX files. </summary>
        /// <param name="sourceFileName"> The BIN file to unpack. </param>
        /// <returns></returns>
        internal IEnumerable<FileInfo> GetBlkxFiles(string sourceFileName) =>
            GetFiles(EFileExtension.Blkx, sourceFileName);

        /// <summary> Unpacks a file with the speficied name as a BIN file and returns CSV files it contains. </summary>
        /// <param name="sourceFileName"> The BIN file to unpack. </param>
        /// <returns></returns>
        internal IEnumerable<FileInfo> GetCsvFiles(string sourceFileName) =>
            GetFiles(EFileExtension.Csv, sourceFileName);

        /// <summary> Gets vehicle icon PNG files contained in the <paramref name="sourceFileName"/>. </summary>
        /// <param name="sourceFileName"> The BIN file to unpack. </param>
        /// <returns></returns>
        internal IEnumerable<FileInfo> GetVehicleIcons(string sourceFileName) =>
            GetFiles(EFileExtension.Png, sourceFileName, EDirectory.WarThunder.Subdirectory.Ui, EDirectory.WarThunder.Archive.AtlasesWromfsBin.UnitIcons);

        /// <summary> Reads a file with the specified name from the given collection of files. </summary>
        /// <param name="files"> The collection of files. </param>
        /// <param name="unpackedFileName"> The name of the file to read. </param>
        /// <returns></returns>
        internal string GetJsonText(IEnumerable<FileInfo> files, string unpackedFileName) =>
            _fileReader.Read(files.First(file => file.Name.Contains(unpackedFileName)));

        /// <summary> Reads a CSV file with the specified name from the given collection of CSV files. </summary>
        /// <param name="csvFiles"> The collection of CSV files. </param>
        /// <param name="unpackedFileName"> The name of the CSV file to read. </param>
        /// <returns></returns>
        internal IList<IList<string>> GetCsvRecords(IEnumerable<FileInfo> csvFiles, string unpackedFileName) =>
            _fileReader.ReadCsv(csvFiles.First(file => file.Name.Contains(unpackedFileName)), ECharacter.Semicolon);

        /// <summary> Creates the database for the current War Thunder client version. </summary>
        private void CreateDataBase()
        {
            if (_generateNewDatabase)
                LogInfo(EOrganizationLogMessage.CreatingDatabase);

            _dataRepository = new DataRepositoryWarThunderWithSession(_gameClientVersion, _generateNewDatabase, Assembly.Load(EAssembly.DataBaseMapping), _loggers);

            if (_generateNewDatabase)
                LogInfo(EOrganizationLogMessage.DatabaseCreated);

            if (!_readOnlyJson)
                LogInfo(EOrganizationLogMessage.DatabaseConnectionEstablished);
        }

        /// <summary> Initialises the database with the <see cref="_dataRepository"/>. </summary>
        private void InitialiseDatabase()
        {
            LogInfo(EOrganizationLogMessage.InitializingDatabase);

            _dataRepository.PersistNewObjects();

            LogInfo(EOrganizationLogMessage.DatabaseInitialized);
        }

        private bool AttachVehicleIcon(IVehicle vehicle, IDictionary<string, FileInfo> vehicleIconFiles)
        {
            var vehicleGaijinId = vehicle.GaijinId.ToLower();
            var footballGaijinIdPart = "_football";
            var forTutorialGaijinIdPart = "_for_tutorial";
            var raceGaijinIdPart = "_race";
            var tutorialGaijinIdPart = "_tutorial";
            var youTubeCupGaijinIdPart = "_yt_cup";
            var chinesePrefix = $"{EReference.NationPrefixes[ENation.China]}_";
            var chineseVehicleNumberSuffix = $"_no";
            var matchedFileName = new Dictionary<string, string>
            {
                { "cn_t_26_no531", "ussr_t_26_1940"},
                { "cn_type_86", "ussr_bmp_1"},
            };

            bool tryToSetIcon(string gaijinId)
            {
                if (vehicleIconFiles.TryGetValue(gaijinId, out var iconFile))
                {
                    vehicle.SetIcon(iconFile);
                    return true;
                }
                return false;
            }
            bool tryToSetReplacementIcon(string gaijinIdPart)
            {
                if (vehicleGaijinId.Contains(gaijinIdPart))
                {
                    vehicleGaijinId = vehicleGaijinId.TakeBefore(gaijinIdPart);
                    return tryToSetIcon(vehicleGaijinId);
                }
                return false;
            }
            bool tryToSetReplacementIconFromAnotherNation()
            {
                var gaijinIdParts = vehicleGaijinId.Split(ECharacter.Underscore);
                var originalPrefix = gaijinIdParts.FirstOrDefault();

                if (EReference.NationsFromPrefix.TryGetValue(originalPrefix, out var originalNation))
                {
                    var gaijinIdWithoutPrefix = gaijinIdParts.Skip(EInteger.Number.One).StringJoin(ECharacter.Underscore);

                    return tryToMatchAnotherPrefix(gaijinIdWithoutPrefix, originalNation);
                }
                else
                {
                    return tryToMatchAnotherPrefix(vehicleGaijinId);
                }
            }
            bool tryToSetReplacementIconForChineseCapturedVehicles()
            {
                if (vehicleGaijinId.Contains(chinesePrefix) && vehicleGaijinId.Contains(chineseVehicleNumberSuffix))
                {
                    vehicleGaijinId = vehicleGaijinId.TakeBefore(chineseVehicleNumberSuffix);
                    return tryToSetIcon(vehicleGaijinId);
                }
                return false;
            }
            bool tryToSetReplacementIconFromKnownMatches()
            {
                return matchedFileName.TryGetValue(vehicleGaijinId, out var replacementGaijinId) && tryToSetIcon(replacementGaijinId);
            }
            bool tryToMatchAnotherPrefix(string gaijinIdWithoutPrefix, ENation originalNation = ENation.None)
            {
                foreach (var nationPrefix in EReference.NationPrefixes)
                {
                    var nation = nationPrefix.Key;

                    if (!nation.IsValid() || nation == originalNation)
                        continue;

                    var prefix = nationPrefix.Value;
                    var gaijinIdWithAnotherPrefix = $"{prefix}{ECharacter.Underscore}{gaijinIdWithoutPrefix}";

                    if (tryToSetIcon(gaijinIdWithAnotherPrefix))
                        return true;
                }
                return false;
            }

            return tryToSetIcon(vehicleGaijinId)
                || tryToSetReplacementIcon(footballGaijinIdPart)
                || tryToSetReplacementIcon(forTutorialGaijinIdPart)
                || tryToSetReplacementIcon(raceGaijinIdPart)
                || tryToSetReplacementIcon(tutorialGaijinIdPart)
                || tryToSetReplacementIcon(youTubeCupGaijinIdPart)
                || tryToSetReplacementIconFromKnownMatches()
                || tryToSetReplacementIconForChineseCapturedVehicles()
                || tryToSetReplacementIconFromAnotherNation()
            ;
        }

        /// <summary> Unpacks game files, converts them into JSON, deserializes it into objects, and persists them into the database. </summary>
        private void UnpackDeserializePersist()
        {
            if (_generateDatabase)
                CreateDataBase();
            else
                _dataRepository = new DataRepositoryInMemory(_loggers);

            LogInfo(EOrganizationLogMessage.PreparingGameFiles);

            var blkxFiles = GetBlkxFiles(EFile.WarThunder.StatAndBalanceParameters);
            var csvFiles = GetCsvFiles(EFile.WarThunder.LocalizationParameters);
            var vehicleIconFiles = GetVehicleIcons(EFile.WarThunderUi.Icons).ToDictionary(file => file.GetNameWithoutExtension().ToLower());

            var wpCostJsonText = GetJsonText(blkxFiles, EFile.CharVromfs.GeneralVehicleData);
            var unitTagsJsonText = GetJsonText(blkxFiles, EFile.CharVromfs.AdditionalVehicleData);
            var researchTreeJsonText = GetJsonText(blkxFiles, EFile.CharVromfs.ResearchTreeData);
            var vehicleLocalizationRecords = GetCsvRecords(csvFiles, EFile.LangVromfs.Units);

            LogInfo(EOrganizationLogMessage.GameFilesPrepared);
            LogInfo(EOrganizationLogMessage.DeserialisingGameFiles);

            var vehicles = _jsonHelper.DeserializeList<Vehicle>(_dataRepository, wpCostJsonText).ToDictionary(vehicle => vehicle.GaijinId, vehicle => vehicle as IVehicle);
            var additionalVehicleData = _jsonHelper.DeserializeList<VehicleDeserializedFromJsonUnitTags>(unitTagsJsonText).ToDictionary(vehicle => vehicle.GaijinId);
            var researchTreeData = _jsonHelper.DeserializeResearchTrees(researchTreeJsonText).SelectMany(researchTree => researchTree.Vehicles).ToDictionary(vehicle => vehicle.GaijinId);

            foreach (var vehicle in vehicles.Values)
            {
                if (additionalVehicleData.TryGetValue(vehicle.GaijinId, out var additionalDataEntry))
                    vehicle.InitializeWithDeserializedAdditionalVehicleDataJson(additionalDataEntry);

                if (researchTreeData.TryGetValue(vehicle.GaijinId, out var researchTreeEntry))
                    vehicle.InitializeWithDeserializedResearchTreeJson(researchTreeEntry);

                AttachVehicleIcon(vehicle, vehicleIconFiles);
            }

            _csvDeserializer.DeserializeVehicleLocalization(vehicles, vehicleLocalizationRecords);
            _cache.AddRange(_dataRepository.NewObjects);

            LogInfo(EOrganizationLogMessage.DeserialisationComplete);

            if (_generateNewDatabase)
                InitialiseDatabase();
        }

        #endregion Methods: Initialization
        #region Methods: Settings

        /// <summary> Loads settings from the <see cref="_settingsManager"/> into the given settings class. </summary>
        /// <param name="settingsClass"> The settings class. </param>
        protected void LoadSettings(Type settingsClass)
        {
            var settingProperties = settingsClass.GetProperties(BindingFlags.Public | BindingFlags.Static).Where(property => property.GetCustomAttribute<RequiredSettingAttribute>() is RequiredSettingAttribute);

            foreach (var settingProperty in settingProperties)
                settingProperty.SetValue(null, _settingsManager.GetSetting(settingProperty.Name));
        }

        /// <summary> Loads settings from the <see cref="_settingsManager"/>. </summary>
        protected virtual void LoadSettings() => LoadSettings(typeof(Settings));

        #endregion Methods: Settings

        /// <summary> Releases unmanaged resources. </summary>
        public void Dispose() =>
            _dataRepository.Dispose();

        /// <summary> Removes log files older than a week. </summary>
        public void RemoveOldLogFiles() =>
            _fileManager.DeleteOldFiles
            (
                Path.Combine(Directory.GetCurrentDirectory(), ESubdirectory.Logs),
                DateTime.Now.AddDays(-EInteger.Number.Seven)
            );

        /// <summary> Generates two vehicle presets (primary and fallback) based on the given specification. </summary>
        /// <param name="specification"> The specification to base vehicle selection on. </param>
        /// <returns></returns>
        public IDictionary<EPreset, Preset> GeneratePrimaryAndFallbackPresets(Specification specification)
        {
            return _presetGenerator.GeneratePrimaryAndFallbackPresets(specification);
        }
    }
}