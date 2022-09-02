using Core.Csv.WarThunder.Helpers.Interfaces;
using Core.DataBase.Enumerations;
using Core.DataBase.Helpers;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
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
using Core.Web.WarThunder.Helpers.Interfaces;
using Core.Web.WarThunder.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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
        /// <summary> Whether to extract game files. </summary>
        private readonly bool _readPreviouslyUnpackedJson;

        private readonly EStartup _startupConfiguration;

        /// <summary> Parts of Gaijin IDs of vehicles excluded from display. </summary>
        private readonly IEnumerable<string> _excludedGaijinIdParts;

        private bool _generateNewDatabase;
        private Version _gameClientVersion;
        private string _gameClientVersionString;
        private Version _latestAvailableDatabaseVersion;

        protected readonly IWarThunderFileManager _fileManager;
        protected readonly IWarThunderFileReader _fileReader;
        protected readonly IWarThunderSettingsManager _settingsManager;
        protected readonly IParser _parser;
        protected readonly IUnpacker _unpacker;
        protected readonly IConverter _converter;
        protected readonly IWarThunderJsonHelper _jsonHelper;
        protected readonly ICsvDeserializer _csvDeserializer;
        protected readonly IDataRepositoryFactory _dataRepositoryFactory;
        protected readonly IRandomiser _randomizer;
        protected readonly IVehicleSelector _vehicleSelector;
        protected readonly IPresetGenerator _presetGenerator;
        protected readonly IThunderSkillParser _thunderSkillParser;

        /// <summary> An instance of a data repository. </summary>
        protected IDataRepository _dataRepository;
        /// <summary> The cache of persistent objects. </summary>
        protected readonly List<IPersistentObject> _cache;
        /// <summary> Counts of usage of vehicles with given <see cref="IVehicle.EconomicRank"/>s on each game mode. </summary>

        #endregion Fields
        #region Properties

        public bool ShowThunderSkillData { get; private set; }

        /// <summary> Research trees. This collection needs to be filled up after caching vehicles up from the database by calling <see cref="CacheData"/>. </summary>
        public IDictionary<ENation, ResearchTree> ResearchTrees { get; }

        /// <summary> Playable vehicles loaded into memory. </summary>
        public IDictionary<string, IVehicle> PlayableVehicles { get; }

        public IDictionary<EGameMode, IDictionary<EBranch, IDictionary<int, int>>> EconomicRankUsage { get; }

        #endregion Properties
        #region Delegates

        protected Action<IVehicle> ProcessVehicleImages;

        #endregion Delegates
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
            IDataRepositoryFactory dataRepositoryFactory,
            IRandomiser randomizer,
            IVehicleSelector vehicleSelector,
            IPresetGenerator presetGenerator,
            IThunderSkillParser thunderSkillParser,
            bool generateDatabase,
            bool readOnlyJson,
            bool readPreviouslyUnpackedJson,
            params IConfiguredLogger[] loggers
        ) : base(EOrganizationLogCategory.Manager, loggers)
        {
            _generateDatabase = generateDatabase;
            _readOnlyJson = readOnlyJson;
            _readPreviouslyUnpackedJson = readPreviouslyUnpackedJson;
            _startupConfiguration = GetStartupConfiguration(_generateDatabase, _readOnlyJson, _readPreviouslyUnpackedJson);
            _excludedGaijinIdParts = new List<string>()
            {
                "_football",
                "germ_panzerkampflaufer_565_r",
                "germ_panzerkampflaufer_565_r_2",
                "_killstreak",
                "po-2_nw",
                "_race",
                "sdi_",
                "_space_suit",
                "toon_",
                "_tutorial",
                "ucav_assault",
                "ucav_scout",
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
            _dataRepositoryFactory = dataRepositoryFactory;
            _randomizer = randomizer;
            _vehicleSelector = vehicleSelector;
            _presetGenerator = presetGenerator;
            _thunderSkillParser = thunderSkillParser;

            _cache = new List<IPersistentObject>();

            PlayableVehicles = new Dictionary<string, IVehicle>();

            static IDictionary<EBranch, IDictionary<int, int>> createDictionary() =>
                typeof(EBranch)
                    .GetEnumerationItems<EBranch>(true)
                    .ToDictionary(branch => branch, branch => default(IDictionary<int, int>))
                ;

            EconomicRankUsage = new Dictionary<EGameMode, IDictionary<EBranch, IDictionary<int, int>>>
            {
                { EGameMode.Arcade, createDictionary()},
                { EGameMode.Realistic, createDictionary()},
                { EGameMode.Simulator, createDictionary()},
            };

            _settingsManager = settingsManager;
            _settingsManager.Initialise(_startupConfiguration.IsIn(new List<EStartup> { EStartup.ReadDatabase, EStartup.ReadUnpackedJson }));
            LoadSettings();

            ShowThunderSkillData = true;
            ResearchTrees = new Dictionary<ENation, ResearchTree>();

            LogDebug(CoreLogMessage.Created.Format(EOrganizationLogCategory.Manager));
        }

        #endregion Constructors
        #region Methods: Initialisation

        private EStartup GetStartupConfiguration(bool generateDatabase, bool readJson, bool readUnpackedJson)
        {
            if (readUnpackedJson)
            {
                return EStartup.ReadUnpackedJson;
            }
            else if (readJson)
            {
                if (generateDatabase)
                    return EStartup.CreateDatabaseReadJson;
                else
                    return EStartup.ReadJson;
            }
            else if (generateDatabase)
            {
                return EStartup.CreateDatabaseReadDatabase;
            }
            else
            {
                return EStartup.ReadDatabase;
            }
        }

        /// <summary> Reads and stores the version of the game client. </summary>
        public void InitialiseGameClientVersion()
        {
            _gameClientVersion = _parser.GetClientVersion(_fileReader.ReadInstallData(EClientVersion.Current));
            _gameClientVersionString = _gameClientVersion.ToString();
        }

        /// <summary> Initialises memebers of the <see cref="EReference"/> class. </summary>
        private void InitialiseReferences()
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

        private void InitialiseEconomicRankUsageDictionaries()
        {
            static IDictionary<int, int> createDictionary() =>
                Enumerable
                    .Range(Integer.Number.Zero, EReference.MaximumEconomicRank + Integer.Number.One)
                    .ToDictionary(number => number, number => Integer.Number.Zero);

            foreach (var branchDictionary in EconomicRankUsage.Values)
            {
                foreach (var branch in branchDictionary.Keys.ToList())
                    branchDictionary[branch] = createDictionary();
            }
        }

        /// <summary> Initialises research trees from cached vehicles. Obviously, should be called after <see cref="CacheData"/>. </summary>
        private void InitialiseResearchTrees()
        {
            LogInfo(EOrganizationLogMessage.InitialisingResearchTrees);

            var columnCount = default(int);

            foreach (var vehicle in PlayableVehicles.Values)
            {
                if (vehicle.ResearchTreeData is null)
                    continue;

                var nation = vehicle.Nation.AsEnumerationItem;
                var branch = vehicle.Branch;
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

        private void InitialiseEconomicRankUsageStatistics()
        {
            if (!_thunderSkillParser.IsLoaded)
                return;

            LogInfo(EOrganizationLogMessage.AggregatingVehicleUsageStatistics);

            var vehicleUsage = new Dictionary<EBranch, IDictionary<string, VehicleUsage>>
            {
                { EBranch.Army, _thunderSkillParser.GetVehicleUsage(EBranch.Army) },
                { EBranch.Helicopters, _thunderSkillParser.GetVehicleUsage(EBranch.Helicopters) },
                { EBranch.Aviation, _thunderSkillParser.GetVehicleUsage(EBranch.Aviation) },
                { EBranch.AllFleet, _thunderSkillParser.GetVehicleUsage(EBranch.AllFleet) },
            };

            if (vehicleUsage.All(branchStatistics => branchStatistics.Value.IsEmpty()))
            {
                ShowThunderSkillData = false;

                LogWarn(EOrganizationLogMessage.NoUsefulStatisticsHaveBeenFound);

                return;
            }

            foreach (var vehicle in PlayableVehicles.Values)
            {
                var branch = vehicle.Category.AsEnumerationItem == EVehicleCategory.Fleet ? EBranch.AllFleet : vehicle.Branch;

                var branchVehicleUsage = vehicleUsage[branch];

                if (branchVehicleUsage.TryGetValue(vehicle.GaijinId, out var usageCounts))
                {
                    void increment(EGameMode gameMode, int key, int value) => EconomicRankUsage[gameMode][vehicle.Branch].Increment(key, value);

                    if (vehicle.EconomicRank.Arcade.HasValue)
                        increment(EGameMode.Arcade, vehicle.EconomicRank.Arcade.Value, usageCounts.ArcadeCount);

                    if (vehicle.EconomicRank.Realistic.HasValue)
                        increment(EGameMode.Realistic, vehicle.EconomicRank.Realistic.Value, usageCounts.RealisticCount);

                    if (vehicle.EconomicRank.Simulator.HasValue)
                        increment(EGameMode.Simulator, vehicle.EconomicRank.Simulator.Value, usageCounts.SimulatorCount);
                }
            }

            LogInfo(EOrganizationLogMessage.FinishedAggregatingVehicleUsageStatistics);
        }

        /// <summary> Tries to unpack game files, convert them into JSON, deserialise it into objects, and persist them into the database. </summary>
        private void TryToUnpackDeserialiseAndPersist()
        {
            try
            {
                UnpackDeserialisePersist(_readPreviouslyUnpackedJson);
            }
            catch
            {
                if (_generateDatabase && _generateNewDatabase)
                {
                    var databaseFileName = $"{_gameClientVersionString}{Character.Period}{FileExtension.SqLite3}";
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
            if (string.IsNullOrWhiteSpace(_gameClientVersionString) && _latestAvailableDatabaseVersion is Version)
            {
                LogWarn(EOrganizationLogMessage.WarThunderVersionNotInitialised);
                _generateNewDatabase = false;
            }
            else if (_latestAvailableDatabaseVersion is null || !_gameClientVersionString.IsIn(_latestAvailableDatabaseVersion.ToString()))
            {
                LogInfo(EOrganizationLogMessage.NotFoundDatabaseFor.Format(_gameClientVersionString));
                _generateNewDatabase = true;
            }
            else
            {
                LogInfo(EOrganizationLogMessage.FoundDatabaseMatchesWithWarThunderVersion);
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
                var latestAvailableDatabaseVersion = _latestAvailableDatabaseVersion.ToString();

                _dataRepository = _dataRepositoryFactory.Create(EDataRepository.SqliteSingleSession, _generateDatabase ? (_gameClientVersionString ?? latestAvailableDatabaseVersion) : latestAvailableDatabaseVersion, false, Assembly.Load(EAssembly.DataBaseMapping));

                LogInfo(EOrganizationLogMessage.DatabaseConnectionEstablished);
            }
        }

        private void InitialiseLatestAvailableDatabaseVersion()
        {
            var databaseIsUsed = _startupConfiguration.IsIn(new List<EStartup> { EStartup.CreateDatabaseReadDatabase, EStartup.CreateDatabaseReadJson, EStartup.ReadDatabase });
            var availableDatabaseVersions = _fileManager.GetWarThunderDatabaseVersions();

            if (availableDatabaseVersions.Any())
            {
                _latestAvailableDatabaseVersion = availableDatabaseVersions.Max();

                if (databaseIsUsed)
                    LogInfo(EOrganizationLogMessage.FoundDatabaseFor.Format(_latestAvailableDatabaseVersion));

                if (!_generateDatabase)
                    _generateNewDatabase = false;
            }

            var builtDatabaseIsRequired = _startupConfiguration == EStartup.ReadDatabase;

            if (_latestAvailableDatabaseVersion is null)
            {
                if (builtDatabaseIsRequired)
                    throw new FileNotFoundException(EOrganizationLogMessage.DatabaseNotFound);
            }
        }

        /// <summary> Initialises the <see cref="_dataRepository"/>. </summary>
        private void InitialiseDataRepository()
        {
            InitialiseLatestAvailableDatabaseVersion();

            if (_generateDatabase)
                CheckForExistingDatabase();

            if (_startupConfiguration.IsIn(new List<EStartup> { EStartup.CreateDatabaseReadJson, EStartup.ReadJson }) || _startupConfiguration == EStartup.CreateDatabaseReadDatabase && _generateNewDatabase)
            {
                LogInfo(EOrganizationLogMessage.ClearingTempDirectory);
                {
                    _fileManager.CleanUpTempDirectory();
                }
                LogInfo(EOrganizationLogMessage.TempDirectoryCleared);
            }

            if (_readOnlyJson)
                InitialiseDatabaselessDataRepository();
            else
                InitialiseDatabaseBasedDataRepository();
        }

        private async Task LoadThunderSkillVehicleUsageStatistics()
        {
            try
            {
                LogInfo(EOrganizationLogMessage.ReadingVehicleUsageStatisticsFromThunderSkill);
                {
                    var task = Task.Factory.StartNew(() => _thunderSkillParser.Load());
                    var timeout = Integer.Time.MillisecondsInSecond * Integer.Number.Thirty;

                    if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
                    {
                        await task;

                        LogInfo(EOrganizationLogMessage.FinishedReadingVehicleUsageStatisticsFromThunderSkill);
                        return;
                    }
                    else
                    {
                        throw new TimeoutException();
                    }
                        
                }
            }
            catch (Exception exception)
            {
                LogWarn(EOrganizationLogMessage.FailedToReadVehicleUsageStatisticsFromThunderSkill.Format(exception));

                ShowThunderSkillData = false;
            }
        }

        /// <summary> Fills the <see cref="_cache"/> up. </summary>
        public async void CacheData()
        {
            var loadThunderSkillVehicleUsageStatisticsTask = LoadThunderSkillVehicleUsageStatistics();

            InitialiseDataRepository();

            LogInfo(EOrganizationLogMessage.CachingObjects);

            if (_cache.IsEmpty())
                _cache.AddRange(_dataRepository.Query<IVehicle>());

            PlayableVehicles.AddRange
            (
                _cache
                    .OfType<IVehicle>()
                    .Where(vehicle => !vehicle.GaijinId.ContainsAny(_excludedGaijinIdParts))
                    .AsParallel()
                    .ToDictionary(vehicle => vehicle.GaijinId)
            );
            _presetGenerator.SetPlayableVehicles(PlayableVehicles.Values);

            LogInfo(EOrganizationLogMessage.CachingComplete);

            await loadThunderSkillVehicleUsageStatisticsTask;

            InitialiseReferences();
            InitialiseResearchTrees();
            InitialiseEconomicRankUsageDictionaries();
            InitialiseEconomicRankUsageStatistics();
        }

        internal IEnumerable<FileInfo> GetFilesWithoutProcessing(string fileType, string sourceFileName, string processedSubdirectory = "")
        {
            var outputDirectory = new DirectoryInfo(Path.Combine(Settings.TempLocation, $"{sourceFileName}_u", processedSubdirectory));

            return outputDirectory.GetFiles($"{Character.Asterisk}{Character.Period}{fileType}", SearchOption.AllDirectories);
        }

        internal DirectoryInfo Unpack(string packedFileName, string warThunderSubdirectory = "", string outputSubdirectory = "")
        {
            var sourceFile = _fileManager.GetFileInfo(Path.Combine(Settings.WarThunderLocation, warThunderSubdirectory), packedFileName);

            return new DirectoryInfo(Path.Combine(_unpacker.Unpack(sourceFile), outputSubdirectory));
        }

        internal DirectoryInfo Unpack(FileInfo sourceFile, string outputSubdirectory = "")
        {
            return new DirectoryInfo(Path.Combine(_unpacker.Unpack(sourceFile), outputSubdirectory));
        }

        private DirectoryInfo GetCachesDirectory()
        {
            var cacheDirectory = new DirectoryInfo(Settings.CacheLocation);

            if (cacheDirectory.Exists)
            {
                if (_gameClientVersion is null)
                {
                    LogWarn(CoreLogMessage.MemberNotInitialisedProperly.Format(nameof(_gameClientVersion)));

                    return null;
                }

                var cacheDirectories = cacheDirectory
                    .GetDirectories($"binary.{_gameClientVersion.ToString(Integer.Number.Three)}*", SearchOption.TopDirectoryOnly)
                    .OrderByDescending(directory => directory.LastWriteTimeUtc);

                if (cacheDirectories.Any())
                {
                    if (cacheDirectories.HasSeveral())
                        throw new AmbiguousMatchException(EOrganizationLogMessage.SeveralCacheDirectoriesFound.Format(cacheDirectories.Select(directory => $"\"{directory.Name}\"").StringJoin(Separator.CommaAndSpace)));

                    return cacheDirectories.First();
                }
            }
            return null;
        }

        /// <summary> Unpacks a file with the <paramref name="packedFileName"/> and gets files of the <paramref name="fileType"/> from its contents, doing conversions if necessary. </summary>
        /// <param name="fileType"> The type of files to search for after unpacking. </param>
        /// <param name="packedFileName"> The name of the packed file. </param>
        /// <param name="warThunderSubdirectory"> War Thunder subdirectory where to look for the <paramref name="packedFileName"/>. </param>
        /// <param name="processedSubdirectory"> Subdirectory of the output directory that requires processing (if only a subset of data is required). </param>
        /// <returns></returns>
        internal IEnumerable<FileInfo> GetFiles(string fileType, string packedFileName, string warThunderSubdirectory = "", string processedSubdirectory = "")
        {
            var sourceFile = _fileManager.GetFileInfo(Path.Combine(Settings.WarThunderLocation, warThunderSubdirectory), packedFileName);
            var outputDirectory = default(DirectoryInfo);

            if (fileType == FileExtension.Blkx)
            {
                var cachesDirectory = GetCachesDirectory();
                
                if (cachesDirectory is DirectoryInfo && cachesDirectory.GetFiles(packedFileName, SearchOption.TopDirectoryOnly).FirstOrDefault() is FileInfo cachedFile && cachedFile.LastWriteTimeUtc > sourceFile.LastWriteTimeUtc)
                    outputDirectory = Unpack(cachedFile, processedSubdirectory);
            }

            if (outputDirectory is null)
                outputDirectory = Unpack(sourceFile, processedSubdirectory);

            return GetFiles(fileType, outputDirectory);
        }

        internal IEnumerable<FileInfo> GetFiles(string fileType, DirectoryInfo outputDirectory, string subdirectory = "")
        {
            var processedDirectory = new DirectoryInfo(Path.Combine(outputDirectory.FullName, subdirectory));

            switch (fileType)
            {
                case FileExtension.Blkx:
                {
                    _unpacker.Unpack(processedDirectory, ETool.BlkUnpacker);
                    break;
                }
                case FileExtension.Png:
                {
                    _unpacker.Unpack(processedDirectory, ETool.DdsxUnpacker);
                    _converter.ConvertDdsToPng(processedDirectory, SearchOption.AllDirectories);
                    break;
                }
            }

            return processedDirectory.GetFiles($"{Character.Asterisk}{Character.Period}{fileType}", SearchOption.AllDirectories);
        }

        /// <summary> Unpacks a file with the speficied name as a BIN file and returns BLK files it contains converted into BLKX files. </summary>
        /// <param name="sourceFileName"> The BIN file to unpack. </param>
        /// <param name="readAlreadyUnpackedFiles"> Whether to unpack game files. </param>
        /// <returns></returns>
        internal IEnumerable<FileInfo> GetBlkxFiles(string sourceFileName, bool readAlreadyUnpackedFiles) =>
            readAlreadyUnpackedFiles
                ? GetFilesWithoutProcessing(FileExtension.Blkx, sourceFileName)
                : GetFiles(FileExtension.Blkx, sourceFileName);

        /// <summary> Unpacks a file with the speficied name as a BIN file and returns CSV files it contains. </summary>
        /// <param name="sourceFileName"> The BIN file to unpack. </param>
        /// <param name="readAlreadyUnpackedFiles"> Whether to unpack game files. </param>
        /// <returns></returns>
        internal IEnumerable<FileInfo> GetCsvFiles(string sourceFileName, bool readAlreadyUnpackedFiles) =>
            readAlreadyUnpackedFiles
                ? GetFilesWithoutProcessing(FileExtension.Csv, sourceFileName)
                : GetFiles(FileExtension.Csv, sourceFileName);

        /// <summary> Gets vehicle icon PNG files contained in the <paramref name="sourceFileName"/>. </summary>
        /// <param name="sourceFileName"> The BIN file to unpack. </param>
        /// <param name="readAlreadyUnpackedFiles"> Whether to unpack game files. </param>
        /// <returns></returns>
        internal IEnumerable<FileInfo> GetVehicleIcons(string sourceFileName, bool readAlreadyUnpackedFiles)
        {
            var processedSubdirectory = EDirectory.WarThunder.Archive.AtlasesWromfsBin.UnitIcons;

            return readAlreadyUnpackedFiles
                ? GetFilesWithoutProcessing(FileExtension.Png, sourceFileName, processedSubdirectory)
                : GetFiles(FileExtension.Png, sourceFileName, EDirectory.WarThunder.Subdirectory.Ui, processedSubdirectory);
        }

        internal IEnumerable<FileInfo> GetVehiclePortraits(bool readAlreadyUnpackedFiles)
        {
            var sourceFileName = EFile.WarThunderUi.Textures;
            var outputDirectory = readAlreadyUnpackedFiles
                ? new DirectoryInfo(Path.Combine(Settings.TempLocation, $"{sourceFileName}_u"))
                : Unpack(sourceFileName, EDirectory.WarThunder.Subdirectory.Ui);

            var vehiclePortraitFiles = new List<FileInfo>();
            var processedSubdirectories = new List<string>
            {
                EDirectory.WarThunder.Archive.TexWromfsBin.Aircraft,
                EDirectory.WarThunder.Archive.TexWromfsBin.Ships,
                EDirectory.WarThunder.Archive.TexWromfsBin.Tanks,
            };

            IEnumerable<FileInfo> getPortraitFiles(string subdirectory) => readAlreadyUnpackedFiles
                ? new DirectoryInfo(Path.Combine(outputDirectory.FullName, subdirectory)).GetFiles($"{Character.Asterisk}{Character.Period}{FileExtension.Png}", SearchOption.AllDirectories)
                : GetFiles(FileExtension.Png, outputDirectory, subdirectory);

            foreach (var processedSubdirectory in processedSubdirectories)
                vehiclePortraitFiles.AddRange(getPortraitFiles(processedSubdirectory));

            return vehiclePortraitFiles;
        }

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
            _fileReader.ReadCsv(csvFiles.First(file => file.Name.Contains(unpackedFileName)), Character.Semicolon);

        /// <summary> Creates the database for the current War Thunder client version. It starts out blank and needs to be filled up. </summary>
        private void CreateBlankDataBase()
        {
            if (_generateNewDatabase)
                LogInfo(EOrganizationLogMessage.CreatingBlankDatabase);

            _dataRepository = _dataRepositoryFactory.Create(EDataRepository.SqliteSingleSession, _gameClientVersionString, _generateNewDatabase, Assembly.Load(EAssembly.DataBaseMapping));

            if (_generateNewDatabase)
                LogInfo(EOrganizationLogMessage.BlankDatabaseCreated);

            if (!_readOnlyJson)
                LogInfo(EOrganizationLogMessage.DatabaseConnectionEstablished);
        }

        /// <summary> Initialises the database with the <see cref="_dataRepository"/>. </summary>
        private void InitialiseDatabase()
        {
            LogInfo(EOrganizationLogMessage.InitialisingDatabase);

            _dataRepository.PersistNewObjects();

            LogInfo(EOrganizationLogMessage.DatabaseInitialized);
        }
        
        private bool AttachVehicleImage(IVehicle vehicle, IDictionary<string, FileInfo> vehicleIconFiles, Action<IVehicle, byte[]> setImage, EVehicleImage imageType)
        {
            if (vehicle.IsInternal)
                return false;

            LogTrace(EOrganizationLogMessage.AttachingImageToVehicle.Format(vehicle.GaijinId));

            bool tryToSetIcon(string gaijinId)
            {
                if (vehicleIconFiles.TryGetValue(gaijinId.ToLower(), out var iconFile))
                {
                    setImage(vehicle, _fileReader.ReadBytes(iconFile));
                    return true;
                }
                return false;
            }

            var inheritedGaijinId = vehicle.GraphicsData.GetInheritedGaijinId(imageType);
            var imageLocated = string.IsNullOrWhiteSpace(inheritedGaijinId)
                ? tryToSetIcon(vehicle.GaijinId)
                : tryToSetIcon(inheritedGaijinId);

            if (imageLocated)
                LogTrace(EOrganizationLogMessage.ImageLocatedAndAttachedToVehicle.Format(vehicle.GaijinId));
            else
                LogWarn(EOrganizationLogMessage.ImageNotFoundForVehicle.Format(vehicle.GaijinId));

            return imageLocated;
        }

        private Task InitialiseDataRepositoryCore()
        {
            if (_generateDatabase)
            {
                var task = new Task(CreateBlankDataBase);

                task.Start();

                return task;
            }
            else
            {
                _dataRepository = _dataRepositoryFactory.Create(EDataRepository.InMemory);

                return null;
            }
        }

        private void FillDataRepository()
        {
            if (_generateNewDatabase)
                InitialiseDatabase();

            else if (!_generateDatabase)
                _dataRepository.PersistNewObjects();
        }

        /// <summary> Unpacks game files, converts them into JSON, deserializes it into objects, and persists them into the database. </summary>
        /// <param name="readAlreadyUnpackedFiles"> Whether to unpack game files. </param>
        private void UnpackDeserialisePersist(bool readAlreadyUnpackedFiles = false)
        {
            var initialiseDataRepositoryTask = InitialiseDataRepositoryCore();

            if (!_readPreviouslyUnpackedJson)
                LogInfo(EOrganizationLogMessage.PreparingGameFiles);

            #region Start Unpacking Tasks

            IEnumerable<FileInfo> getBlkxFiles()
            {
                if (!_readPreviouslyUnpackedJson)
                    LogDebug(EOrganizationLogMessage.PreparingBlkxFiles);

                var files = GetBlkxFiles(EFile.WarThunder.StatAndBalanceParameters, readAlreadyUnpackedFiles);

                if (!_readPreviouslyUnpackedJson)
                    LogDebug(EOrganizationLogMessage.BlkxFilesPrepared);

                return files;
            }
            var getBlkxFilesTask = new Task<IEnumerable<FileInfo>>(getBlkxFiles);
            getBlkxFilesTask.Start();

            IEnumerable<FileInfo> getCsvFiles()
            {
                if (!_readPreviouslyUnpackedJson)
                    LogDebug(EOrganizationLogMessage.PreparingCsvFiles);

                var files = GetCsvFiles(EFile.WarThunder.LocalizationParameters, readAlreadyUnpackedFiles);

                if (!_readPreviouslyUnpackedJson)
                    LogDebug(EOrganizationLogMessage.CsvFilesPrepared);

                return files;
            }
            var getCsvFilesTask = new Task<IEnumerable<FileInfo>>(getCsvFiles);
            getCsvFilesTask.Start();

            IDictionary<string, FileInfo> getVehicleIcons()
            {
                if (!_readPreviouslyUnpackedJson)
                    LogDebug(EOrganizationLogMessage.PreparingVehicleIcons);

                var icons = GetVehicleIcons(EFile.WarThunderUi.Icons, readAlreadyUnpackedFiles).ToDictionary(file => file.GetNameWithoutExtension().ToLower());

                if (!_readPreviouslyUnpackedJson)
                    LogDebug(EOrganizationLogMessage.VehicleIconsPrepared);

                return icons;
            }
            var getVehicleIconsTask = new Task<IDictionary<string, FileInfo>>(getVehicleIcons);
            getVehicleIconsTask.Start();

            IDictionary<string, FileInfo> getVehiclePortraitFiles()
            {
                if (!_readPreviouslyUnpackedJson)
                    LogDebug(EOrganizationLogMessage.PreparingVehiclePortraits);

                var vehiclePortraits = GetVehiclePortraits(readAlreadyUnpackedFiles).ToDictionary(file => file.GetNameWithoutExtension().ToLower());

                if (!_readPreviouslyUnpackedJson)
                    LogDebug(EOrganizationLogMessage.VehiclePortraitsPrepared);

                return vehiclePortraits;
            }
            var getVehiclePortraitFilesTask = new Task<IDictionary<string, FileInfo>>(getVehiclePortraitFiles);
            getVehiclePortraitFilesTask.Start();

            #endregion Start Unpacking Tasks

            initialiseDataRepositoryTask?.Wait();
            getBlkxFilesTask.Wait();
            getCsvFilesTask.Wait();

            var blkxFiles = getBlkxFilesTask.Result;
            var csvFiles = getCsvFilesTask.Result;

            if (!_readPreviouslyUnpackedJson)
                LogInfo(EOrganizationLogMessage.GameDataFilesPrepared);

            LogInfo(EOrganizationLogMessage.DeserialisingGameFiles);

            #region Start Deserialisation Tasks

            IDictionary<string, IVehicle> getVehicles()
            {
                LogDebug(EOrganizationLogMessage.DeserialisingMainVehicleData);

                var wpCostJsonText = GetJsonText(blkxFiles, EFile.CharVromfs.GeneralVehicleData);
                var vehicles = _jsonHelper.DeserializeList<Vehicle>(_dataRepository, wpCostJsonText).ToDictionary(vehicle => vehicle.GaijinId, vehicle => vehicle as IVehicle);

                LogDebug(EOrganizationLogMessage.MainVehicleDataDeserialised);

                return vehicles;
            };
            var getVehiclesTask = new Task<IDictionary<string, IVehicle>>(getVehicles);
            getVehiclesTask.Start();

            IDictionary<string, VehicleDeserializedFromJsonUnitTags> getAdditionalVehicleData()
            {
                LogDebug(EOrganizationLogMessage.DeserialisingAdditionalVehicleData);

                var unitTagsJsonText = GetJsonText(blkxFiles, EFile.CharVromfs.AdditionalVehicleData);
                var unitTags = _jsonHelper.DeserializeList<VehicleDeserializedFromJsonUnitTags>(unitTagsJsonText).ToDictionary(vehicle => vehicle.GaijinId);

                LogDebug(EOrganizationLogMessage.AdditionalVehicleDataDeserialised);

                return unitTags;
            }
            var getAdditionalVehicleDataTask = new Task<IDictionary<string, VehicleDeserializedFromJsonUnitTags>>(getAdditionalVehicleData);
            getAdditionalVehicleDataTask.Start();

            IDictionary<string, ResearchTreeVehicleFromJson> getResearchTreeData()
            {
                LogDebug(EOrganizationLogMessage.DeserialisingResearchTrees);

                var researchTreeJsonText = GetJsonText(blkxFiles, EFile.CharVromfs.ResearchTreeData);
                var researchTrees = _jsonHelper.DeserializeResearchTrees(researchTreeJsonText).SelectMany(researchTree => researchTree.Vehicles).ToDictionary(vehicle => vehicle.GaijinId);

                LogDebug(EOrganizationLogMessage.ResearchTreesDeserialised);

                return researchTrees;
            }
            var getResearchTreeDataTask = new Task<IDictionary<string, ResearchTreeVehicleFromJson>>(getResearchTreeData);
            getResearchTreeDataTask.Start();

            #endregion Start Deserialisation Tasks

            getVehiclesTask.Wait();
            getAdditionalVehicleDataTask.Wait();
            getResearchTreeDataTask.Wait();

            var vehicles = getVehiclesTask.Result;
            var additionalVehicleData = getAdditionalVehicleDataTask.Result;
            var researchTreeData = getResearchTreeDataTask.Result;

            #region Start Deserialisation Tasks

            void deserializeVehicleLocalization()
            {
                LogDebug(EOrganizationLogMessage.DeserialisingVehicleLocalisation);

                var vehicleLocalizationRecords = GetCsvRecords(csvFiles, EFile.LangVromfs.Units);
                _csvDeserializer.DeserializeVehicleLocalization(vehicles, vehicleLocalizationRecords);

                LogDebug(EOrganizationLogMessage.VehicleLocalisationDeserialised);
            }
            var deserializeVehicleLocalizationTask = new Task(deserializeVehicleLocalization);
            deserializeVehicleLocalizationTask.Start();

            #endregion Start Deserialisation Tasks

            getVehicleIconsTask.Wait();
            getVehiclePortraitFilesTask.Wait();

            var vehicleIconFiles = getVehicleIconsTask.Result;
            var vehiclePortaitFiles = getVehiclePortraitFilesTask.Result;

            if (!_readPreviouslyUnpackedJson)
                LogInfo(EOrganizationLogMessage.GameImageFilesPrepared);

            LogDebug(EOrganizationLogMessage.InitialisingVehicles);
            LogDebug(EOrganizationLogMessage.ProcessingVehicleImages);

            var initialiseVehicleTasks = new List<Task>();
            var processVehicleImagesTasks = new List<Task>();

            foreach (var vehicle in vehicles.Values)
            {
                AttachVehicleImage(vehicle, vehicleIconFiles, (vehicle, bytes) => vehicle.SetIcon(bytes), EVehicleImage.Banner);
                AttachVehicleImage(vehicle, vehiclePortaitFiles, (vehicle, bytes) => vehicle.SetPortrait(bytes), EVehicleImage.Portrait);

                #region Start Initialisation Tasks

                void initialiseVehicle()
                {
                    if (additionalVehicleData.TryGetValue(vehicle.GaijinId, out var additionalDataEntry))
                        vehicle.InitializeWithDeserializedAdditionalVehicleDataJson(additionalDataEntry);

                    if (researchTreeData.TryGetValue(vehicle.GaijinId, out var researchTreeEntry))
                        vehicle.InitializeWithDeserializedResearchTreeJson(researchTreeEntry);
                }
                var initialiseVehicleTask = new Task(initialiseVehicle);
                initialiseVehicleTask.AddInto(initialiseVehicleTasks);
                initialiseVehicleTask.Start();

                void processVehicleImages() => ProcessVehicleImages?.Invoke(vehicle);
                var processVehicleImagesTask = new Task(processVehicleImages);
                processVehicleImagesTask.AddInto(processVehicleImagesTasks);
                processVehicleImagesTask.Start();

                #endregion Start Initialisation Tasks
            }

            initialiseVehicleTasks.ForEach(task => task.Wait());
            deserializeVehicleLocalizationTask.Wait();

            LogDebug(EOrganizationLogMessage.VehiclesInitialised);
            LogInfo(EOrganizationLogMessage.DeserialisationComplete);

            _cache.AddRange(_dataRepository.NewObjects);

            FillDataRepository();

            processVehicleImagesTasks.ForEach(task => task.Wait());

            LogDebug(EOrganizationLogMessage.VehicleImagesProcessed);
        }

        #endregion Methods: Initialisation
        #region Methods: Settings

        /// <summary> Loads settings from the <see cref="_settingsManager"/> into the given settings class. </summary>
        /// <param name="settingsClass"> The settings class. </param>
        protected void LoadSettings(Type settingsClass)
        {
            var settingProperties = settingsClass.GetProperties(BindingFlags.Public | BindingFlags.Static).Where(property => property.GetCustomAttribute<RequiredSettingAttribute>() is RequiredSettingAttribute);

            Parallel.ForEach
            (
                settingProperties,
                settingProperty => settingProperty.SetValue(null, _settingsManager.GetSetting(settingProperty.Name))
            );
        }

        /// <summary> Loads settings from the <see cref="_settingsManager"/>. </summary>
        protected virtual void LoadSettings() => LoadSettings(typeof(Settings));

        #endregion Methods: Settings

        /// <summary> Releases unmanaged resources. </summary>
        public void Dispose()
        {
            _dataRepository.Dispose();
        }

        /// <summary> Removes log files older than a week. </summary>
        public void RemoveOldLogFiles() =>
            _fileManager.DeleteOldFiles
            (
                Path.Combine(Directory.GetCurrentDirectory(), Subdirectory.Logs),
                DateTime.Now.AddDays(-Integer.Number.Seven)
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