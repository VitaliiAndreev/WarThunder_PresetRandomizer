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
using Core.DataBase.WarThunder.Objects.VehicleGameModeParameterSet.String;
using Core.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using Core.Json.WarThunder.Helpers.Interfaces;
using Core.Objects;
using Core.Organization.Collections;
using Core.Organization.Enumerations;
using Core.Organization.Enumerations.Logger;
using Core.Organization.Extensions;
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
        #region Constants

        /// <summary>
        /// The maximum difference in battle rating from the battle rating selected by user.
        /// <para> Example: if the difference is 1 and the user chooses 5.7, vehicles of 4.7-5.7 are selected. </para>
        /// </summary>
        private const decimal _maximumBattleRatingDifference = 2.0m;

        #endregion Constants
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
        /// <summary> An instance of a JSON helper. </summary>
        protected readonly IWarThunderJsonHelper _jsonHelper;
        /// <summary> An instance of a CSV deserializer. </summary>
        protected readonly ICsvDeserializer _csvDeserializer;
        /// <summary> An instance of a randomizer. </summary>
        protected readonly IRandomizer _randomizer;
        /// <summary> An instance of a vehicle selector. </summary>
        protected readonly IVehicleSelector _vehicleSelector;

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
            IWarThunderJsonHelper jsonHelper,
            ICsvDeserializer csvDeserializer,
            IRandomizer randomizer,
            IVehicleSelector vehicleSelector,
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
            _jsonHelper = jsonHelper;
            _csvDeserializer = csvDeserializer;
            _randomizer = randomizer;
            _vehicleSelector = vehicleSelector;

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

            LogInfo(EOrganizationLogMessage.CachingComplete);

            InitializeReferences();
            InitializeResearchTrees();
        }

        /// <summary> Unpacks a file with the speficied name and gets files of the given type from its contents, doing conversions if necessary. </summary>
        /// <param name="fileType"> The type of files to search for after unpacking. </param>
        /// <param name="packedFileName"> The name of the packed file. </param>
        /// <returns></returns>
        internal IEnumerable<FileInfo> GetFiles(string fileType, string packedFileName)
        {
            var sourceFile = _fileManager.GetFileInfo(Settings.WarThunderLocation, packedFileName);
            var outputDirectory = new DirectoryInfo(_unpacker.Unpack(sourceFile));

            switch (fileType)
            {
                case EFileExtension.Blkx:
                    _unpacker.Unpack(outputDirectory, ETool.BlkUnpacker);
                    break;
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

        #region Methods: Helper Methods for GeneratePrimaryAndFallbackPresets()

        /// <summary> Filters <see cref="_playableVehicles"/> with <paramref name="vehicleGaijinIds"/>. </summary>
        /// <param name="vehicleGaijinIds"> Gaijin IDs of vehicles enabled via GUI. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> FilterVehiclesByGaijinIds(IEnumerable<string> vehicleGaijinIds)
        {
            var filteredVehicles = _playableVehicles.Where(vehicle => vehicle.GaijinId.IsIn(vehicleGaijinIds));

            if (filteredVehicles.IsEmpty())
            {
                LogWarn(EOrganizationLogMessage.NoVehiclesSelected);
                return null;
            }
            return filteredVehicles;
        }

        /// <summary> Assesses <paramref name="filteredVehicles"/> does post-filtering operations. </summary>
        /// <param name="filteredVehicles"> Filtered vehicles to assess. </param>
        /// <param name="validItems"> Items accepted by the filter. </param>
        /// <param name="suppressLogging"> Whether to log empty results. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> AssessFilterResult<T>(IEnumerable<IVehicle> filteredVehicles, IEnumerable<T> validItems, bool suppressLogging)
        {
            if (filteredVehicles.IsEmpty())
            {
                if (!suppressLogging)
                    LogWarn(EOrganizationLogMessage.NoVehiclesAvailableFor.FormatFluently(validItems.StringJoin(EVocabulary.ListSeparator)));

                return null;
            }
            return filteredVehicles;
        }

        /// <summary>
        /// Filters given <paramref name="vehicles"/> based on the <paramref name="itemSelector"/> and <paramref name="validItems"/>.
        /// If emptiness of the resulting collection is logged elsewhere, it's possible to <paramref name="suppressLogging"/>.
        /// </summary>
        /// <typeparam name="T"> The type of filter criteria items. </typeparam>
        /// <param name="vehicles"> Vehicles to filter through. </param>
        /// <param name="validItems"> Items accepted by the filter. </param>
        /// <param name="itemSelector"> The selector function that extracts an evaluated value from given <paramref name="vehicles"/>. </param>
        /// <param name="suppressLogging"> Whether to log empty results. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> FilterVehicles<T>(IEnumerable<IVehicle> vehicles, IEnumerable<T> validItems, Func<IVehicle, T> itemSelector, bool suppressLogging = false)
        {
            var filteredVehicles = vehicles.Where(vehicle => itemSelector(vehicle).IsIn(validItems));

            return AssessFilterResult(filteredVehicles, validItems, suppressLogging);
        }

        /// <summary>
        /// Filters given <paramref name="vehicles"/> based on the <paramref name="itemSelector"/> and <paramref name="validItems"/>.
        /// If emptiness of the resulting collection is logged elsewhere, it's possible to <paramref name="suppressLogging"/>.
        /// </summary>
        /// <typeparam name="T"> The type of filter criteria items. </typeparam>
        /// <param name="vehicles"> Vehicles to filter through. </param>
        /// <param name="validItems"> Items accepted by the filter. </param>
        /// <param name="itemSelector"> The selector function that extracts evaluated values from given <paramref name="vehicles"/>. </param>
        /// <param name="suppressLogging"> Whether to log empty results. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> FilterVehicles<T>(IEnumerable<IVehicle> vehicles, IEnumerable<T> validItems, Func<IVehicle, IEnumerable<T>> itemSelector, bool suppressLogging = false)
        {
            var filteredVehicles = vehicles.Where(vehicle => itemSelector(vehicle).Intersect(validItems).Any());

            return AssessFilterResult(filteredVehicles, validItems, suppressLogging);
        }

        /// <summary> Filters <paramref name="vehicles"/> with <paramref name="enabledNationCountryPairs"/>. </summary>
        /// <param name="enabledNationCountryPairs"> Nation-country pairs enabled via GUI. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> FilterVehiclesByCountries(IEnumerable<NationCountryPair> enabledNationCountryPairs, IEnumerable<IVehicle> vehicles) =>
            FilterVehicles(vehicles, enabledNationCountryPairs, vehicle => new NationCountryPair(vehicle.Nation.AsEnumerationItem, vehicle.Country));

        /// <summary> Filters <paramref name="vehicles"/> with <paramref name="validVehicleClasses"/>. </summary>
        /// <param name="validVehicleClasses"> Vehicle classes enabled via GUI and actually available. </param>
        /// <param name="vehicles"> Vehicles to filter. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> FilterVehiclesByClassFilters(IEnumerable<EVehicleClass> validVehicleClasses, IEnumerable<IVehicle> vehicles) =>
            FilterVehicles(vehicles, validVehicleClasses, vehicle => vehicle.Class);

        /// <summary> Filters <paramref name="vehicles"/> with <paramref name="validVehicleSubclasses"/>. </summary>
        /// <param name="validVehicleSubclasses"> Vehicle subclasses enabled via GUI and actually available. </param>
        /// <param name="vehicles"> Vehicles to filter. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> FilterVehiclesBySubclassFilters(IEnumerable<EVehicleSubclass> validVehicleSubclasses, IEnumerable<IVehicle> vehicles) =>
            FilterVehicles(vehicles, validVehicleSubclasses, vehicle => vehicle.Subclasses.All);

        /// <summary> Filters <paramref name="vehicles"/> with <paramref name="validBranches"/>. </summary>
        /// <param name="validBranches"> Vehicle branches enabled via GUI and actually available. </param>
        /// <param name="vehicles"> Vehicles to filter. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> FilterVehiclesByBranches(IEnumerable<EBranch> validBranches, IEnumerable<IVehicle> vehicles) =>
            FilterVehicles(vehicles, validBranches, vehicle => vehicle.Branch.AsEnumerationItem);

        /// <summary> Filters <paramref name="vehicles"/> with <paramref name="validNations"/>. </summary>
        /// <param name="validNations"> Vehicle classes enabled via GUI and actually available. </param>
        /// <param name="vehicles"> Vehicles to filter. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> FilterVehiclesByNations(IEnumerable<ENation> validNations, IEnumerable<IVehicle> vehicles) =>
            FilterVehicles(vehicles, validNations, vehicle => vehicle.Nation.AsEnumerationItem);

        /// <summary> Filters <paramref name="vehicles"/> with <paramref name="validEconomicRanksNations"/>. </summary>
        /// <param name="validEconomicRanksNations"> Vehicle classes enabled via GUI and actually available. </param>
        /// <param name="vehicles"> Vehicles to filter. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> FilterVehiclesByEconomicRanks(EGameMode gameMode, IDictionary<ENation, IEnumerable<IVehicle>> vehiclesByNations, IDictionary<ENation, IEnumerable<int>> validEconomicRanks)
        {
            var vehiclesWithEconomocRankFilter = new List<IVehicle>();

            foreach (var nationKeyValuePair in validEconomicRanks)
            {
                var nation = nationKeyValuePair.Key;
                var economicRanks = nationKeyValuePair.Value;

                vehiclesWithEconomocRankFilter.AddRange(FilterVehiclesByEconomicRanks(gameMode, economicRanks, vehiclesByNations[nation]));
            }

            if (vehiclesWithEconomocRankFilter.IsEmpty())
            {
                var parameterString = validEconomicRanks
                    .Select(keyValuePair => $"{keyValuePair.Key}: {keyValuePair.Value.StringJoin(EVocabulary.ListSeparator)}")
                    .StringJoin(EVocabulary.ListGroupSeparator)
                ;

                LogWarn(EOrganizationLogMessage.NoVehiclesAvailableFor.FormatFluently(parameterString));
                return null;
            }

            return vehiclesWithEconomocRankFilter;
        }

        /// <summary> Filters <paramref name="vehicles"/> with <paramref name="validEconomicRanksNations"/>. </summary>
        /// <param name="validEconomicRanksNations"> Vehicle classes enabled via GUI and actually available. </param>
        /// <param name="vehicles"> Vehicles to filter. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> FilterVehiclesByEconomicRanks(EGameMode gameMode, IEnumerable<int> validEconomicRanksNations, IEnumerable<IVehicle> vehicles) =>
            FilterVehicles(vehicles, validEconomicRanksNations, vehicle => vehicle.EconomicRank.AsDictionary()[gameMode].Value, true);

        /// <summary> Selects the main branch from selected via GUI (passed with <paramref name="specification"/>) and <paramref name="availableBranches"/>. </summary>
        /// <param name="specification"> The search specification. </param>
        /// <param name="availableBranches"> Branches available after filtering with vehicle classes. </param>
        /// <returns></returns>
        private EBranch SelectMainBranch(Specification specification, IEnumerable<EBranch> availableBranches)
        {
            var mainBranch = _randomizer.GetRandom(specification.BranchSpecifications.Keys.Where(branch => branch.IsIn(availableBranches)));

            LogDebug(ECoreLogMessage.Selected.FormatFluently(mainBranch));

            if (mainBranch == EBranch.None)
            {
                LogWarn(EOrganizationLogMessage.NoVehiclesAvailableFor.FormatFluently(availableBranches.StringJoin(EVocabulary.ListSeparator)));
                return EBranch.None;
            }
            return mainBranch;
        }

        /// <summary> Selects the nation specification from <paramref name="specification"/> based on previously selected <paramref name="mainBranch"/> and <paramref name="availableNations"/>. </summary>
        /// <param name="specification"> The search specification. </param>
        /// <param name="mainBranch"> The main vehicle branch. </param>
        /// <param name="availableNations"> Nations available after filtering with vehicle classes. </param>
        /// <returns></returns>
        private NationSpecification SelectNationSpecification(Specification specification, EBranch mainBranch, IEnumerable<ENation> availableNations)
        {
            var nationSpecificationsWithValidBranches = specification.NationSpecifications.Values.Where(nationSpecification => nationSpecification.Branches.Contains(mainBranch));

            if (nationSpecificationsWithValidBranches.IsEmpty())
            {
                LogWarn(EOrganizationLogMessage.NationsHaveNoBranch.FormatFluently(specification.NationSpecifications.Values.Select(nationSpecification => nationSpecification.Nation).StringJoin(EVocabulary.ListSeparator), mainBranch));
                return null;
            }

            var nationSpecification = _randomizer.GetRandom(nationSpecificationsWithValidBranches.Where(nationSpecification => nationSpecification.Nation.IsIn(availableNations)));

            if (nationSpecification is null)
                LogWarn(EOrganizationLogMessage.NoVehiclesAvailableFor.FormatFluently(availableNations.StringJoin(EVocabulary.ListSeparator)));

            return nationSpecification;
        }

        /// <summary> Selects the nation from <paramref name="nationSpecification"/>. </summary>
        /// <param name="nationSpecification"> The previously selected nation specification. </param>
        /// <returns></returns>
        private ENation SelectNation(NationSpecification nationSpecification)
        {
            var nation = nationSpecification.Nation;

            LogDebug(ECoreLogMessage.Selected.FormatFluently(nation));

            return nation;
        }

        /// <summary> Selects valid branches from <paramref name="vehiclesByBranches"/>. </summary>
        /// <param name="vehiclesByBranches">
        /// Vehicles filtered by <see cref="SelectVehiclesWithCountryFilter(IEnumerable{NationCountryPair})"/>,
        /// <see cref="FilterVehiclesByClassFilters(IEnumerable{EVehicleClass}, IEnumerable{IVehicle})"/>, and by the selected nation.
        /// </param>
        /// <returns></returns>
        private IEnumerable<EBranch> GetValidBranches(IDictionary<EBranch, IEnumerable<IVehicle>> vehiclesByBranches)
        {
            var validBranches = vehiclesByBranches.Keys;

            if (validBranches.IsEmpty())
            {
                LogWarn(EOrganizationLogMessage.NoVehiclesAvailableFor.FormatFluently(validBranches.StringJoin(EVocabulary.ListSeparator)));
                return null;
            }
            return validBranches;
        }

        /// <summary> Selects valid economic ranks from <paramref name="enabledEconomicRanks"/> based on <paramref name="economicRanksWithVehicles"/></summary>
        /// <param name="enabledEconomicRanks"> Economic ranks enabled via GUI (as battle ratings). </param>
        /// <param name="economicRanksWithVehicles"> Economic ranks that have vehicles after application of previous filters. </param>
        /// <param name="getFormattedBattleRating"> A function to get a formatted battle rating from an economic rank. </param>
        /// <param name="nation"> The nation. </param>
        /// <param name="mainBranch"> The main branch. </param>
        /// <returns></returns>
        private IEnumerable<int> GetEconomicRanks(IEnumerable<int> enabledEconomicRanks, IEnumerable<int> economicRanksWithVehicles, Func<int, string> getFormattedBattleRating, ENation nation, EBranch mainBranch)
        {
            var validEconomicRanks = enabledEconomicRanks.Intersect(economicRanksWithVehicles);

            if (validEconomicRanks.IsEmpty())
            {
                var minimumBattleRating = getFormattedBattleRating(enabledEconomicRanks.Min());
                var maximumBattleRating = getFormattedBattleRating(enabledEconomicRanks.Max());

                LogWarn(EOrganizationLogMessage.NoVehiclesAvailableForSelectedBattleRatings.FormatFluently(minimumBattleRating, maximumBattleRating, mainBranch, nation));
                return null;
            }
            return validEconomicRanks;
        }

        /// <summary> Gets a battle rating from <paramref name="economicRank"/>. </summary>
        /// <param name="economicRank"> The economic rank to get a battle rating from. </param>
        /// <param name="getFormattedBattleRating"> A function to get a formatted battle rating from an economic rank. </param>
        /// <returns></returns>
        private decimal GetBattleRating(int economicRank, Func<int, string> getFormattedBattleRating)
        {
            var battleRating = Calculator.GetBattleRating(economicRank);

            LogDebug(ECoreLogMessage.Selected.FormatFluently(getFormattedBattleRating(economicRank)));

            return battleRating;
        }

        /// <summary> Generates a vehicle type composition for a preset. </summary>
        /// <param name="gameMode"> The game mode to generate preset composition for. </param>
        /// <param name="allowedBranches"> Branches allowed in the composition. </param>
        /// <param name="crewSlotAmount"> The amount of available crew slots. </param>
        /// <param name="mainBranch"> The branch whose vehicles serve as the core of a preset. </param>
        /// <returns></returns>
        private IDictionary<EBranch, int> GetPresetComposition(EGameMode gameMode, IEnumerable<EBranch> allowedBranches, int crewSlotAmount, EBranch mainBranch)
        {
            var presetComposition = new Dictionary<EBranch, int>();

            void setAll(EBranch branch) => presetComposition.Add(branch, crewSlotAmount);
            void setQuarter(EBranch branch) => presetComposition.Add(branch, Convert.ToInt32(Math.Ceiling(crewSlotAmount * 0.25)));
            void setTwoThirds(EBranch branch) => presetComposition.Add(branch, Convert.ToInt32(Math.Ceiling(crewSlotAmount * 2.0 / 3.0)));
            void setThreeQuarters(EBranch branch) => presetComposition.Add(branch, Convert.ToInt32(Math.Ceiling(crewSlotAmount * 0.75)));
            void setHalf(EBranch branch) => presetComposition.Add(branch, Convert.ToInt32(Math.Ceiling(crewSlotAmount * 0.5)));
            void setRemaining(EBranch branch, params EBranch[] otherBranches) => presetComposition.Add(branch, crewSlotAmount - otherBranches.Sum(branch => presetComposition[branch]));

            if (mainBranch == EBranch.Army)
            {
                if (gameMode == EGameMode.Arcade || !EBranch.Aviation.IsIn(allowedBranches) && !EBranch.Helicopters.IsIn(allowedBranches))
                {
                    setAll(mainBranch);
                }
                else if (!EBranch.Aviation.IsIn(allowedBranches))
                {
                    setThreeQuarters(EBranch.Army);
                    setRemaining(EBranch.Helicopters, EBranch.Army);
                }
                else if (!EBranch.Helicopters.IsIn(allowedBranches))
                {
                    setThreeQuarters(EBranch.Army);
                    setRemaining(EBranch.Aviation, EBranch.Army);
                }
                else
                {
                    setHalf(EBranch.Army);
                    setQuarter(EBranch.Aviation);
                    setRemaining(EBranch.Helicopters, EBranch.Army, EBranch.Aviation);
                }
            }
            else if (mainBranch == EBranch.Helicopters)
            {
                if (gameMode == EGameMode.Arcade)
                {
                    setAll(mainBranch);
                }
                else
                {
                    setTwoThirds(EBranch.Helicopters);
                    setRemaining(EBranch.Army, EBranch.Helicopters);
                }
            }
            else if (mainBranch == EBranch.Aviation)
            {
                setAll(mainBranch);
            }
            else if (mainBranch == EBranch.Fleet)
            {
                if (gameMode == EGameMode.Simulator)
                    return presetComposition;

                if (crewSlotAmount <= EInteger.Number.Three || !EBranch.Aviation.IsIn(allowedBranches))
                {
                    setAll(mainBranch);
                }
                else
                {
                    setTwoThirds(EBranch.Fleet);
                    setRemaining(EBranch.Aviation, EBranch.Fleet);
                }
            }
            return presetComposition;
        }

        /// <summary> Removes the <paramref name="mainVehicle"/> from <paramref name="randomVehicles"/>. </summary>
        /// <param name="vehiclesByBranchesAndBattleRatings"> The collection of vehicles grouped first by battle ratings and then by branches.</param>
        /// <param name="presetComposition"> The affected preset composition. </param>
        /// <param name="gameMode"> The selected game mode. </param>
        /// <param name="randomVehicles"> Random vehicles to remove <paramref name="mainVehicle"/> from. </param>
        /// <param name="mainVehicle"> The vehicle to remove from <paramref name="randomVehicles"/>. </param>
        private void RemoveMainVehicleBeforeSelectingRandomVehicles
        (
            VehiclesByBranchesAndBattleRating vehiclesByBranchesAndBattleRatings,
            IDictionary<EBranch, int> presetComposition,
            EGameMode gameMode,
            List<IVehicle> randomVehicles,
            IVehicle mainVehicle = null
        )
        {
            if
            (
                mainVehicle is IVehicle
                && presetComposition.ContainsKey(mainVehicle.Branch.AsEnumerationItem)
                && vehiclesByBranchesAndBattleRatings.TryGetValue(mainVehicle.Branch.AsEnumerationItem, out var vehiclesByBattleRating)
                && mainVehicle.BattleRating.AsDictionary()[gameMode] is decimal battleRating
                && vehiclesByBattleRating.ContainsKey(battleRating)
            )
            {
                randomVehicles.Add(mainVehicle);
                presetComposition[mainVehicle.Branch.AsEnumerationItem]--;
                vehiclesByBattleRating[battleRating].Remove(mainVehicle);
            }
        }

        /// <summary> Randomly selects vehicles from the provided collection based on given parameters. </summary>
        /// <param name="vehiclesByBranchesAndBattleRatings"> The vehicle collecion to select from, grouped by branches and battle ratings. </param>
        /// <param name="presetComposition"> The preset composition. </param>
        /// <param name="crewSlotAmount"> The amount of available crew slots. </param>
        /// <returns></returns>
        private IList<IVehicle> GetRandomVehiclesForPreset(VehiclesByBranchesAndBattleRating vehiclesByBranchesAndBattleRatings, IDictionary<EBranch, int> presetComposition, int crewSlotAmount, EGameMode gameMode, IVehicle mainVehicle = null)
        {
            var randomVehicles = new List<IVehicle>();

            RemoveMainVehicleBeforeSelectingRandomVehicles(vehiclesByBranchesAndBattleRatings, presetComposition, gameMode, randomVehicles, mainVehicle);

            while (randomVehicles.Count() < crewSlotAmount && vehiclesByBranchesAndBattleRatings.Any())
            {
                foreach (var branch in vehiclesByBranchesAndBattleRatings.Keys.ToList())
                {
                    var vehiclesToTake = Math.Min(crewSlotAmount - randomVehicles.Count, presetComposition[branch]);
                    randomVehicles.AddRange(vehiclesByBranchesAndBattleRatings[branch].GetRandomVehicles(_vehicleSelector, vehiclesToTake));

                    if (randomVehicles.Count() == crewSlotAmount)
                        break;
                    if (vehiclesByBranchesAndBattleRatings[branch].IsEmpty())
                        vehiclesByBranchesAndBattleRatings.Remove(branch);
                }
            }

            return presetComposition
                .Keys
                .SelectMany(branch => randomVehicles.Where(vehicle => vehicle.Branch.AsEnumerationItem == branch))
                .ToList()
            ;
        }

        #endregion Methods: Helper Methods for GeneratePrimaryAndFallbackPresets()

        /// <summary> Generates two vehicle presets (primary and fallback) based on the given specification. </summary>
        /// <param name="specification"> The specification to base vehicle selection on. </param>
        /// <returns></returns>
        public IDictionary<EPreset, Preset> GeneratePrimaryAndFallbackPresets(Specification specification)
        {
            #region Initial extraction of parameters from specification.

            var emptyPreset = new Dictionary<EPreset, Preset>();
            var enabledVehicleClassesByBranch = specification.BranchSpecifications.ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value.VehicleClasses);
            var enabledNationCountryPairs = specification.NationSpecifications.Values.SelectMany(nationSpecification => nationSpecification.Countries.Select(country => new NationCountryPair(nationSpecification.Nation, country)));

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Filtering by vehicle Gaijin IDs.

            var vehiclesWithGaijinIdFilter = FilterVehiclesByGaijinIds(specification.VehicleGaijinIds);

            if (vehiclesWithGaijinIdFilter is null)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Filtering by countiries.

            var vehiclesWithCountryFilter = FilterVehiclesByCountries(enabledNationCountryPairs, vehiclesWithGaijinIdFilter);

            if (vehiclesWithCountryFilter is null)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Filtering by classes.

            var availableVehicleClasses = vehiclesWithCountryFilter.Select(vehicle => vehicle.Class).Distinct().ToList();
            var validVehicleClasses = enabledVehicleClassesByBranch.Values.SelectMany(branchVehicleClasses => branchVehicleClasses.Where(vehicleClass => vehicleClass.IsIn(availableVehicleClasses)));
            var vehiclesWithClassFilter = FilterVehiclesByClassFilters(validVehicleClasses, vehiclesWithCountryFilter);

            if (vehiclesWithClassFilter is null)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Filtering by subclasses.

            var vehicleSubclassesFromValidClasses = validVehicleClasses.SelectMany(vehicleClass => vehicleClass.GetVehicleSubclasses());
            var validVehicleSubclasses = specification.VehicleSubclasses.Intersect(vehicleSubclassesFromValidClasses).Including(EVehicleSubclass.None);
            var vehiclesWithSubclassFilter = FilterVehiclesBySubclassFilters(validVehicleSubclasses, vehiclesWithClassFilter);

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------

            return specification.Randomisation switch
            {
                ERandomisation.CategoryBased => GeneratePrimaryAndFallbackPresetsByCategories(specification, vehiclesWithSubclassFilter),
                ERandomisation.VehicleBased => GeneratePrimaryAndFallbackPresetsByVehicles(specification, vehiclesWithSubclassFilter),
                _ => new Dictionary<EPreset, Preset>(),
            };
        }

        /// <summary> Finalises generation of presets by grouping vehicles by battle ratings and branches for orderly selection and doing selection proper. </summary>
        /// <param name="gameMode"> The selected game mode. </param>
        /// <param name="vehicles"> Vehicles to choose from. </param>
        /// <param name="nation"> The chosen nation. </param>
        /// <param name="mainBranch"> The chosen branch, main in the <paramref name="presetComposition"/>. </param>
        /// <param name="presetComposition"> The preset composition used for selection. </param>
        /// <param name="battleRatingBracket"> The battle rating bracket to pick from. </param>
        /// <param name="formattedBattleRating"> The chose battle rating, formatted for display. </param>
        /// <param name="crewSlotAmount"> The amount of crew slots. </param>
        /// <param name="mainVehicle"> The main vehicle in the primary preset. </param>
        /// <returns></returns>
        private IDictionary<EPreset, Preset> FinalisePresetGeneration
        (
            EGameMode gameMode,
            IEnumerable<IVehicle> vehicles,
            ENation nation,
            EBranch mainBranch,
            IDictionary<EBranch, int> presetComposition,
            Interval<decimal> battleRatingBracket,
            string formattedBattleRating,
            int crewSlotAmount,
            IVehicle mainVehicle = null)
        {
            var presets = new Dictionary<EPreset, Preset>();

            var vehiclesByBranchesAndBattleRatings = new VehiclesByBranchesAndBattleRating
            (
                presetComposition
                    .Keys
                    .Select(branch => new { Branch = branch, Vehicles = vehicles.Where(vehicle => vehicle.Branch.AsEnumerationItem == branch).OrderByHighestBattleRating(_vehicleSelector, gameMode, battleRatingBracket) })
                    .ToDictionary(item => item.Branch, item => new VehiclesByBattleRating(item.Vehicles))
            );

            void addPreset(EPreset presetType) =>
                presets.Add
                (
                    presetType,
                    new Preset(nation, mainBranch, formattedBattleRating, GetRandomVehiclesForPreset(vehiclesByBranchesAndBattleRatings, presetComposition, crewSlotAmount, gameMode, presetType == EPreset.Primary ? mainVehicle : null))
                );

            addPreset(EPreset.Primary);
            addPreset(EPreset.Fallback);

            return presets;
        }

        /// <summary> Generates two vehicle presets (primary and fallback) based on the given specification, following the <see cref="ERandomisation.CategoryBased"/> algorithm. </summary>
        /// <param name="specification"> The specification to base vehicle selection on. </param>
        /// <returns></returns>
        private IDictionary<EPreset, Preset> GeneratePrimaryAndFallbackPresetsByCategories(Specification specification, IEnumerable<IVehicle> vehiclesWithClassFilter)
        {
            var emptyPreset = new Dictionary<EPreset, Preset>();
            var gameMode = specification.GameMode;

            #region Main branch selection.

            var availableBranches = vehiclesWithClassFilter.Select(vehicle => vehicle.Branch.AsEnumerationItem).Distinct().ToList();
            var mainBranch = SelectMainBranch(specification, availableBranches);

            if (mainBranch == EBranch.None)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Nation selection.

            var availableNations = vehiclesWithClassFilter.Select(vehicle => vehicle.Nation.AsEnumerationItem).Distinct().ToList();
            var nationSpecification = SelectNationSpecification(specification, mainBranch, availableNations);

            if (nationSpecification is null)
                return emptyPreset;

            var nation = SelectNation(nationSpecification);

            if (nation == ENation.None)
                return emptyPreset;

            var crewSlotAmount = nationSpecification.CrewSlots;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Filtering by nation.

            var vehiclesWithNationFilter = FilterVehiclesByNations(new List<ENation> { nation }, vehiclesWithClassFilter);

            if (vehiclesWithNationFilter is null)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Branch selection.

            var vehiclesByBranches = vehiclesWithNationFilter.GroupBy(vehicle => vehicle.Branch.AsEnumerationItem).ToDictionary(group => group.Key, group => group.AsEnumerable());
            var validBranches = GetValidBranches(vehiclesByBranches);

            if (validBranches is null)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Filtering by preset composition.

            var presetComposition = GetPresetComposition(gameMode, validBranches, crewSlotAmount, mainBranch);
            var presets = new Dictionary<EPreset, Preset>();
            var vehiclesForPreset = vehiclesByBranches.Where(keyValuePair => keyValuePair.Key.IsIn(presetComposition.Keys)).SelectMany(keyValuePair => keyValuePair.Value);

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Battle rating selection.

            var economicRanksWithVehicles = vehiclesForPreset
                .Where(vehicle => vehicle.Branch.AsEnumerationItem == mainBranch)
                .Where(vehicle => vehicle.EconomicRank[gameMode].HasValue)
                .Select(vehicle => vehicle.EconomicRank[gameMode].Value)
                .Distinct()
            ;

            static string getFormattedBattleRating(int economicRank) => Calculator.GetBattleRating(economicRank).ToString(BattleRating.Format);

            var enabledEconomicRanks = specification.EconomicRankIntervals[nation].AsEnumerable();
            var validEconomicRanks = GetEconomicRanks(enabledEconomicRanks, economicRanksWithVehicles, getFormattedBattleRating, nation, mainBranch);

            if (validEconomicRanks is null)
                return new Dictionary<EPreset, Preset> { { EPreset.Primary, new Preset(nation, mainBranch, string.Empty, new List<IVehicle>()) } };

            var economicRank = _randomizer.GetRandom(validEconomicRanks);
            var battleRating = GetBattleRating(economicRank, getFormattedBattleRating);
            var formattedBattleRating = getFormattedBattleRating(economicRank);
            var battleRatingBracket = new Interval<decimal>(true, battleRating - _maximumBattleRatingDifference, battleRating, true);

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------

            return FinalisePresetGeneration(gameMode, vehiclesForPreset, nation, mainBranch, presetComposition, battleRatingBracket, formattedBattleRating, crewSlotAmount);
        }

        /// <summary> Generates two vehicle presets (primary and fallback) based on the given specification, following the <see cref="ERandomisation.VehicleBased"/> algorithm. </summary>
        /// <param name="specification"> The specification to base vehicle selection on. </param>
        /// <returns></returns>
        private IDictionary<EPreset, Preset> GeneratePrimaryAndFallbackPresetsByVehicles(Specification specification, IEnumerable<IVehicle> vehiclesWithClassFilter)
        {
            var emptyPreset = new Dictionary<EPreset, Preset>();
            var gameMode = specification.GameMode;

            #region Filtering by branches.

            var availableBranches = vehiclesWithClassFilter.Select(vehicle => vehicle.Branch.AsEnumerationItem).Distinct().ToList();
            var validBranches = availableBranches.Intersect(specification.BranchSpecifications.Keys).ToList();
            var vehiclesWithBranchFilter = FilterVehiclesByBranches(validBranches, vehiclesWithClassFilter);

            if (vehiclesWithBranchFilter is null)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Filtering by nations.

            var availableNations = vehiclesWithBranchFilter.Select(vehicle => vehicle.Nation.AsEnumerationItem).Distinct().ToList();
            var validNations = availableNations.Intersect(specification.NationSpecifications.Keys).ToList();
            var vehiclesWithNationFilter = FilterVehiclesByNations(validNations, vehiclesWithBranchFilter);

            if (vehiclesWithNationFilter is null)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Filtering by economic ranks.

            var vehiclesByNations = vehiclesWithNationFilter.GroupBy(item => item.Nation.AsEnumerationItem).ToDictionary(group => group.Key, group => group.AsEnumerable());
            var availableEconomicRanks = vehiclesByNations.ToDictionary
            (
                nationKeyValuePair => nationKeyValuePair.Key,
                nationKeyValuePair => nationKeyValuePair.Value.Select(vehicle =>
                {
                    if (vehicle.EconomicRank.AsDictionary()[gameMode] is int economicRank)
                        return new { Nation = nationKeyValuePair.Key, EconomicRank = economicRank };

                    return null;
                })
                .Distinct()
                .Where(item => !(item is null))
            );
            var validEconomicRanks = new Dictionary<ENation, IEnumerable<int>>();

            foreach (var nation in availableEconomicRanks.Keys)
            {
                var availableEconomicRanksForNation = availableEconomicRanks[nation].Select(item => item.EconomicRank);
                var validEconomicRanksForNation = availableEconomicRanksForNation.Where(economicRanks => specification.EconomicRankIntervals[nation].Contains(economicRanks));

                validEconomicRanks.Add(nation, validEconomicRanksForNation);
            }

            var vehiclesWithEconomocRankFilter = FilterVehiclesByEconomicRanks(gameMode, vehiclesByNations, validEconomicRanks);

            if (vehiclesWithEconomocRankFilter is null)
            {
                return emptyPreset;
            }

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Vehicle selection.

            var mainVehicle = _randomizer.GetRandom(vehiclesWithEconomocRankFilter);
            var selectedNation = mainVehicle.Nation.AsEnumerationItem;
            var branch = mainVehicle.Branch.AsEnumerationItem;
            var availableSupplementaryVehicles = vehiclesWithEconomocRankFilter.Where(vehicle => vehicle.Nation.AsEnumerationItem == selectedNation).Except(mainVehicle);
            var branchesAvailableForPresetComposition = availableSupplementaryVehicles.Select(vehicle => vehicle.Branch.AsEnumerationItem).Distinct().ToList();
            var crewSlotAmount = specification.NationSpecifications[selectedNation].CrewSlots;
            var presetComposition = GetPresetComposition(gameMode, branchesAvailableForPresetComposition, crewSlotAmount, branch);

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Filtering by nation.

            var vehiclesOfSelectedNation = FilterVehiclesByNations(new List<ENation> { selectedNation }, vehiclesWithEconomocRankFilter);

            if (vehiclesWithNationFilter is null)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Battle rating selection.

            static string getFormattedBattleRating(int economicRank) => Calculator.GetBattleRating(economicRank).ToString(BattleRating.Format);

            if (!(mainVehicle.EconomicRank.AsDictionary()[gameMode] is int economicRank))
            {
                LogWarn(EOrganizationLogMessage.NoEconomiRankSetForVehicleInGameMode.FormatFluently(mainVehicle.GaijinId));
                return null;
            }
            var battleRating = GetBattleRating(economicRank, getFormattedBattleRating);
            var formattedBattleRating = getFormattedBattleRating(economicRank);
            var battleRatingBracket = new Interval<decimal>(true, battleRating - _maximumBattleRatingDifference, battleRating, true);

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------

            return FinalisePresetGeneration(gameMode, vehiclesOfSelectedNation, selectedNation, branch, presetComposition, battleRatingBracket, formattedBattleRating, crewSlotAmount, mainVehicle);
        }
    }
}