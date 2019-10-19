using Core.Csv.WarThunder.Helpers.Interfaces;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
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

        /// <summary> Parts of Gaijin IDs of vehicles excluded from display. </summary>
        private readonly IEnumerable<string> _excludedGaijinIdParts;

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
            params IConfiguredLogger[] loggers
        ) : base(EOrganizationLogCategory.Manager, loggers)
        {
            _excludedGaijinIdParts = new List<string>()
            {
                "_football",
                "germ_panzerkampflaufer_565_r",
                "germ_panzerkampflaufer_565_r_2",
                "po-2_nw",
                "_race",
                "_tutorial",
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

            _fileManager.CleanUpTempDirectory();

            _settingsManager = settingsManager;
            LoadSettings();

            ResearchTrees = new Dictionary<ENation, ResearchTree>();

            LogDebug(ECoreLogMessage.Created.FormatFluently(EOrganizationLogCategory.Manager));
        }

        #endregion Constructors
        #region Methods: Initialization

        /// <summary> Reads and stores the version of the game client. </summary>
        public void InitializeGameClientVersion() =>
            _gameClientVersion = _parser.GetClientVersion(_fileReader.ReadInstallData(EClientVersion.Current)).ToString();

        private void InitializeReferences()
        {
            var nationCountryCombinations = _cache.OfType<IVehicle>().Select(vehicle => new { Nation = vehicle.Nation.AsEnumerationItem, vehicle.Country }).Distinct();

            foreach(var combination in nationCountryCombinations)
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

            foreach (var vehicle in _cache.OfType<IVehicle>().Where(vehicle => !vehicle.GaijinId.ContainsAny(_excludedGaijinIdParts)))
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

        /// <summary> Fills the <see cref="_cache"/> up. </summary>
        public void CacheData()
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

            if (_cache.IsEmpty())
                _cache.AddRange(_dataRepository.Query<IVehicle>());

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
            var csvFiles = GetCsvFiles(EFile.WarThunder.LocalizationParameters);

            var wpCostJsonText = GetJsonText(blkxFiles, EFile.CharVromfs.GeneralVehicleData);
            var unitTagsJsonText = GetJsonText(blkxFiles, EFile.CharVromfs.AdditionalVehicleData);
            var researchTreeJsonText = GetJsonText(blkxFiles, EFile.CharVromfs.ResearchTreeData);
            var vehicleLocalizationRecords = GetCsvRecords(csvFiles, EFile.LangVromfs.Units);

            LogInfo(EOrganizationLogMessage.GameFilesPrepared);
            LogInfo(EOrganizationLogMessage.InitializingDatabase);

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
            _dataRepository.PersistNewObjects();

            LogInfo(EOrganizationLogMessage.DatabaseInitialized);
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

        /// <summary> Generate a vehicle type composition for a preset. </summary>
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

        /// <summary> Randomly selects vehicles from the provided collection based on given parameters. </summary>
        /// <param name="vehiclesByBranchesAndBattleRatings"> The vehicle collecion to select from, grouped by branches and battle ratings. </param>
        /// <param name="presetComposition"> The preset composition. </param>
        /// <param name="crewSlotAmount"> The amount of available crew slots. </param>
        /// <returns></returns>
        private IList<IVehicle> GetRandomVehiclesForPreset(VehiclesByBranchesAndBattleRating vehiclesByBranchesAndBattleRatings, IDictionary<EBranch, int> presetComposition, int crewSlotAmount)
        {
            var randomVehicles = new List<IVehicle>();

            while (randomVehicles.Count() < crewSlotAmount && vehiclesByBranchesAndBattleRatings.Any())
                foreach (var branch in vehiclesByBranchesAndBattleRatings.Keys.ToList())
                {
                    var vehiclesToTake = Math.Min(crewSlotAmount - randomVehicles.Count, presetComposition[branch]);
                    randomVehicles.AddRange(vehiclesByBranchesAndBattleRatings[branch].GetRandomVehicles(_vehicleSelector, vehiclesToTake));

                    if (randomVehicles.Count() == crewSlotAmount)
                        break;
                    if (vehiclesByBranchesAndBattleRatings[branch].IsEmpty())
                        vehiclesByBranchesAndBattleRatings.Remove(branch);
                }

            return presetComposition
                .Keys
                .SelectMany(branch => randomVehicles.Where(vehicle => vehicle.Branch.AsEnumerationItem == branch))
                .ToList()
            ;
        }

        /// <summary> Generates two vehicle presets (primary and fallback) based on the given specification. </summary>
        /// <param name="specification"> The specification to base vehicle selection on. </param>
        /// <returns></returns>
        public IDictionary<EPreset, Preset> GeneratePrimaryAndFallbackPresets(Specification specification)
        {
            var gameMode = specification.GameMode;
            var mainBranch = _randomizer.GetRandom(specification.BranchSpecifications.Keys);
            var vehicleClasses = specification.BranchSpecifications.ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value.VehicleClasses);

            bool hasVehicleClasses(EBranch branch) => vehicleClasses.TryGetValue(branch, out var branchVehicleClasses) && branchVehicleClasses.Any();

            LogDebug(ECoreLogMessage.Selected.FormatFluently(mainBranch));

            if (!hasVehicleClasses(mainBranch))
            {
                LogWarn(EOrganizationLogMessage.MainBranchHasNoVehicleClassesEnabled);
                return new Dictionary<EPreset, Preset>();
            }

            var nationSpecification = _randomizer.GetRandom(specification.NationSpecifications.Values.Where(nationSpecification => nationSpecification.Branches.Contains(mainBranch)));
            var nation = nationSpecification.Nation;

            LogDebug(ECoreLogMessage.Selected.FormatFluently(nation));

            if (nationSpecification is null)
            {
                LogWarn(EOrganizationLogMessage.NationsHaveNoBranch.FormatFluently(specification.NationSpecifications.Values.Select(nationSpecification => nationSpecification.Nation).StringJoin(Settings.Separator), mainBranch));
                return new Dictionary<EPreset, Preset>();
            }

            var crewSlotAmount = nationSpecification.CrewSlots;
            var vehiclesFromNation = _cache.OfType<IVehicle>().Where(vehicle => !vehicle.GaijinId.ContainsAny(_excludedGaijinIdParts) && vehicle.Nation.AsEnumerationItem == nation);

            if (vehiclesFromNation.IsEmpty())
            {
                LogWarn(EOrganizationLogMessage.DummyNationSelected);
                return new Dictionary<EPreset, Preset>();
            }

            var vehiclesByBranches = vehiclesFromNation.GroupBy(vehicle => vehicle.Branch.AsEnumerationItem).ToDictionary(group => group.Key, group => group.AsEnumerable());
            var availableBranches = nationSpecification.Branches.Where(branch => hasVehicleClasses(branch) && vehiclesByBranches.TryGetValue(branch, out var vehiclesInBranch) && vehiclesInBranch.Any());

            if (availableBranches.IsEmpty())
            {
                LogWarn(EOrganizationLogMessage.NoVehiclesAvailableForSelectedBranches);
                return new Dictionary<EPreset, Preset>();
            }

            var presetComposition = GetPresetComposition(gameMode, availableBranches, crewSlotAmount, mainBranch);
            var presets = new Dictionary<EPreset, Preset>();
            var vehiclesForPreset = vehiclesByBranches.Where(keyValuePair => keyValuePair.Key.IsIn(presetComposition.Keys)).SelectMany(keyValuePair => keyValuePair.Value);

            var vehiclesByClasses = vehiclesForPreset.GroupBy(vehicle => vehicle.Class).ToDictionary(group => group.Key, group => group.AsEnumerable());
            var availableVehicleClasses = vehicleClasses.Where(keyValuePair => keyValuePair.Key.IsIn(availableBranches)).SelectMany(keyValuePair => keyValuePair.Value);

            if (availableVehicleClasses.IsEmpty())
            {
                LogWarn(EOrganizationLogMessage.NoVehicleClassesAvailableForSelectedBranches);
                return new Dictionary<EPreset, Preset>();
            }

            var filteredVehicles = vehiclesByClasses.Where(keyValuePair => keyValuePair.Key.IsIn(availableVehicleClasses)).SelectMany(keyValuePair => keyValuePair.Value);

            if (filteredVehicles.IsEmpty())
            {
                LogWarn(EOrganizationLogMessage.NoVehiclesAvailableForPreset);
                return new Dictionary<EPreset, Preset>();
            }

            var economicRanksWithVehicles = filteredVehicles
                .Where(vehicle => vehicle.Branch.AsEnumerationItem == mainBranch)
                .Where(vehicle => vehicle.EconomicRank[gameMode].HasValue)
                .Select(vehicle => vehicle.EconomicRank[gameMode].Value)
                .Distinct()
            ;

            static string getFormattedBattleRating(int economicRank) => Calculator.GetBattleRating(economicRank).ToString(BattleRating.Format);

            var enabledEconomicRanks = specification.EconomicRankIntervals[nation].AsEnumerable();
            var availableEconomicRanks = enabledEconomicRanks.Intersect(economicRanksWithVehicles);

            if (availableEconomicRanks.IsEmpty())
            {
                var minimumBattleRating = getFormattedBattleRating(enabledEconomicRanks.Min());
                var maximumBattleRating = getFormattedBattleRating(enabledEconomicRanks.Max());

                LogWarn(EOrganizationLogMessage.NoVehiclesAvailableForSelectedBattleRatings.FormatFluently(minimumBattleRating, maximumBattleRating, mainBranch, nation));
                return new Dictionary<EPreset, Preset> { { EPreset.Primary, new Preset(nation, mainBranch, string.Empty, new List<IVehicle>()) } };
            }

            var economicRank = _randomizer.GetRandom(availableEconomicRanks);
            var battleRating = Calculator.GetBattleRating(economicRank);
            var formattedBattleRating = getFormattedBattleRating(economicRank);

            LogDebug(ECoreLogMessage.Selected.FormatFluently(formattedBattleRating));

            var battleRatingBracket = new Interval<decimal>(true, battleRating - _maximumBattleRatingDifference, battleRating, true);
            var vehiclesByBranchesAndBattleRatings = new VehiclesByBranchesAndBattleRating
            (
                presetComposition
                    .Keys
                    .Select(branch => new { Branch = branch, Vehicles = filteredVehicles.Where(vehicle => vehicle.Branch.AsEnumerationItem == branch).OrderByHighestBattleRating(_vehicleSelector, gameMode, battleRatingBracket) })
                    .ToDictionary(item => item.Branch, item => new VehiclesByBattleRating(item.Vehicles))
            );

            void addPreset(EPreset presetType) => presets.Add(presetType, new Preset(nation, mainBranch, formattedBattleRating, GetRandomVehiclesForPreset(vehiclesByBranchesAndBattleRatings, presetComposition, crewSlotAmount)));

            addPreset(EPreset.Primary);
            addPreset(EPreset.Fallback);

            return presets;
        }
    }
}