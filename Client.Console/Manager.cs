using Client.Console.Enumerations;
using Client.Console.Enumerations.Logger;
using Client.Console.Interfaces;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Helpers;
using Core.DataBase.WarThunder.Objects;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Json;
using Core.Enumerations;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Enumerations;
using Core.Json.Helpers;
using Core.Json.WarThunder.Helpers.Interfaces;
using Core.Objects;
using Core.Randomizer.Objects.SearchSpecifications;
using Core.UnpackingToolsIntegration.Enumerations;
using Core.UnpackingToolsIntegration.Helpers;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.WarThunderExtractionToolsIntegration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Client.Console
{
    /// <summary> Controls the flow of the application. </summary>
    public class Manager : LoggerFluency, IManager
    {
        /// <summary> An instance of a file manager. </summary>
        private readonly IWarThunderFileManager _fileManager;
        /// <summary> An instance of a file reader. </summary>
        private readonly IWarThunderFileReader _fileReader;
        /// <summary> An instance of a parser. </summary>
        private readonly IParser _parser;
        /// <summary> An instance of an unpacker. </summary>
        private readonly IUnpacker _unpacker;
        /// <summary> An instance of a JSON helper. </summary>
        private readonly IJsonHelperWarThunder _jsonHelper;

        /// <summary> The string representation of the game client version. </summary>
        private readonly string _gameClientVersion;
        /// <summary> The cache of persistent objects. </summary>
        private readonly List<IPersistentObject> _cache;
        /// <summary> The map of the nation enumeration onto corresponding database values. </summary>
        private readonly IDictionary<ENation, string> _nations = new Dictionary<ENation, string>
        {
            { ENation.None, "country_0" },
            { ENation.Usa, "country_usa" },
            { ENation.Germany, "country_germany" },
            { ENation.Ussr, "country_ussr" },
            { ENation.Commonwealth, "country_britain" },
            { ENation.Japan, "country_japan" },
            { ENation.Italy, "country_italy" },
            { ENation.France, "country_france" },
        };
        /// <summary> The map of the military branch enumeration onto corresponding database values. </summary>
        private readonly IDictionary<EBranch, string> _branches = new Dictionary<EBranch, string>
        {
            { EBranch.Army, "tank" },
            { EBranch.Aviation, "aircraft" },
            { EBranch.Fleet, "ship" },
            { EBranch.Helicopters, "helicopter" },
        };

        /// <summary> An instance of a data repository. </summary>
        private IDataRepository _dataRepository;

        /// <summary> Creates a new manager. </summary>
        public Manager()
            : base(EConsoleUiLogCategory.Manager, new ConfiguredNLogger(ELoggerName.FileLogger, new ExceptionFormatter()), new ConfiguredNLogger(ELoggerName.ConsoleLogger, new ExceptionFormatter()))
        {
            _fileManager = new WarThunderFileManager(_loggers);
            _fileReader = new WarThunderFileReader(_loggers);
            _parser = new Parser(_loggers);
            _unpacker = new Unpacker(_fileManager, _loggers);
            _jsonHelper = new JsonHelperWarThunder(_loggers);

            _gameClientVersion = _parser.GetClientVersion(_fileReader.ReadInstallData(EClientVersion.Current)).ToString();
            _cache = new List<IPersistentObject>();

            _fileManager.CleanUpTempDirectory();

            CacheVehicles();
        }

        /// <summary> Fills the <see cref="_cache"/> up. </summary>
        private void CacheVehicles()
        {
            var upToDateExxtractedDataBaseName = _fileManager.GetWarThunderDataBaseFileNames().Max();

            if (upToDateExxtractedDataBaseName is string && !_gameClientVersion.IsIn(upToDateExxtractedDataBaseName))
            {
                LogInfo(EConsoleUiLogMessage.NotFoundDatabaseFor.FormatFluently(_gameClientVersion));

                UnpackDeserializePersist();
            }
            else
            {
                LogInfo(EConsoleUiLogMessage.FoundDatabaseFor.FormatFluently(_gameClientVersion));

                _dataRepository = new DataRepositoryWarThunder(_gameClientVersion, false, Assembly.Load(EAssembly.DataBaseMapping), _loggers);

                LogInfo(EConsoleUiLogMessage.DataBaseConnectionEstablished);
            }

            LogInfo(EConsoleUiLogMessage.CachingObjects);

            _cache.AddRange(_dataRepository.Query<IVehicle>());

            LogInfo(EConsoleUiLogMessage.CachingComplete);
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

        /// <summary> Unpacks game files, converts them into JSON, deserializes it into objects, and persists them into the database. </summary>
        private void UnpackDeserializePersist()
        {
            LogInfo(EConsoleUiLogMessage.PreparingGameFiles);

            var blkxFiles = GetBlkxFiles(EFile.RootFolder.StatAndBalanceParameters);

            var wpCostJsonText = GetJsonText(blkxFiles, EFile.CharVromfs.GeneralVehicleData);
            var unitTagsJsonText = GetJsonText(blkxFiles, EFile.CharVromfs.AdditionalVehicleData);

            LogInfo(EConsoleUiLogMessage.GameFilesPrepared);
            LogInfo(EConsoleUiLogMessage.CreatingDatabase);

            _dataRepository = new DataRepositoryWarThunder(_gameClientVersion, true, Assembly.Load(EAssembly.DataBaseMapping), _loggers);

            LogInfo(EConsoleUiLogMessage.DatabaseCreatedConnectionEstablished);
            LogInfo(EConsoleUiLogMessage.InitializingDatabase);

            var vehicles = _jsonHelper.DeserializeList<Vehicle>(_dataRepository, wpCostJsonText);
            var additionalVehicleData = _jsonHelper.DeserializeList<VehicleDeserializedFromJsonUnitTags>(unitTagsJsonText);

            foreach (var vehicle in vehicles)
                if (additionalVehicleData.FirstOrDefault(item => item.GaijinId == vehicle.GaijinId) is VehicleDeserializedFromJsonUnitTags matchedData)
                    vehicle.DoPostInitalization(matchedData);

            _dataRepository.PersistNewObjects();

            LogInfo(EConsoleUiLogMessage.DatabaseInitialized);
        }

        /// <summary> Selects vehicles based on the given specification. </summary>
        /// <param name="specification"> The specification to base the selection on. </param>
        /// <returns></returns>
        public IEnumerable<IVehicle> GetVehicles(Specification specification)
        {
            var battleRatingBracket = new IntervalDecimal(true, specification.BattleRating - 2.0m, specification.BattleRating, true);

            bool battleRatingIsValid(IVehicle vehicle)
            {
                var battleRating = vehicle.BattleRating.AsDictionary()[specification.GameMode];

                if (battleRating.HasValue)
                    return battleRatingBracket.Contains(battleRating.Value);

                else
                    return false;
            }

            return _cache.OfType<IVehicle>()
                .Where(vehicle => vehicle.Nation.GaijinId == _nations[specification.Nation])
                .Where(vehicle => vehicle.Branch.GaijinId == _branches[specification.Branch])
                .Where(vehicle => battleRatingIsValid(vehicle))
                .OrderByDescending(vehicle => vehicle.BattleRating.AsDictionary()[specification.GameMode])
                .Take(10)
            ;
        }

        /// <summary> Releases unmanaged resources. </summary>
        public void Dispose() =>
            _dataRepository.Dispose();
    }
}