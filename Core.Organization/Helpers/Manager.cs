using Core.Csv.WarThunder.Helpers.Interfaces;
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
        /// <summary> Playable vehicles loaded into memory. </summary>
        private readonly List<IVehicle> _playableVehicles;

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
            _playableVehicles = new List<IVehicle>();

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

        #region Methods: Helper Methods for GeneratePrimaryAndFallbackPresets()

        /// <summary> Filters <see cref="_playableVehicles"/> with <paramref name="enabledNationCountryPairs"/>. </summary>
        /// <param name="enabledNationCountryPairs"> Nation-country pairs enabled via GUI. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> SelectVehiclesWithCountryFilter(IEnumerable<NationCountryPair> enabledNationCountryPairs)
        {
            var filteredVehicles = _playableVehicles.Where(vehicle => new NationCountryPair(vehicle.Nation.AsEnumerationItem, vehicle.Country).IsIn(enabledNationCountryPairs));

            if (filteredVehicles.IsEmpty())
            {
                LogWarn(EOrganizationLogMessage.NoVehiclesAvailableFor.FormatFluently(enabledNationCountryPairs.StringJoin(EVocabulary.ListSeparator)));
                return null;
            }
            return filteredVehicles;
        }

        /// <summary> Filters <paramref name="vehiclesWithCountryFilter"/> with <paramref name="validVehicleClasses"/>. </summary>
        /// <param name="validVehicleClasses"> Vehicle classes available after filtering with nation-country pairs. </param>
        /// <param name="vehiclesWithCountryFilter"> Vehicles filtered by <see cref="SelectVehiclesWithCountryFilter"/>. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> SelectVehiclesWithClassFilter(IEnumerable<EVehicleClass> validVehicleClasses, IEnumerable<IVehicle> vehiclesWithCountryFilter)
        {
            var filteredVehicles = vehiclesWithCountryFilter.Where(vehicle => vehicle.Class.IsIn(validVehicleClasses));

            if (filteredVehicles.IsEmpty())
            {
                LogWarn(EOrganizationLogMessage.NoVehiclesAvailableFor.FormatFluently(validVehicleClasses.StringJoin(EVocabulary.ListSeparator)));
                return null;
            }
            return filteredVehicles;
        }

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
        /// <see cref="SelectVehiclesWithClassFilter(IEnumerable{EVehicleClass}, IEnumerable{IVehicle})"/>, and by the selected nation.
        /// </param>
        /// <returns></returns>
        private IEnumerable<EBranch> SelectValidBranches(IDictionary<EBranch, IEnumerable<IVehicle>> vehiclesByBranches)
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
        private IEnumerable<int> SelectValidEconomicRanks(IEnumerable<int> enabledEconomicRanks, IEnumerable<int> economicRanksWithVehicles, Func<int, string> getFormattedBattleRating, ENation nation, EBranch mainBranch)
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

        #endregion Methods: Helper Methods for GeneratePrimaryAndFallbackPresets()

        /// <summary> Generates two vehicle presets (primary and fallback) based on the given specification. </summary>
        /// <param name="specification"> The specification to base vehicle selection on. </param>
        /// <returns></returns>
        public IDictionary<EPreset, Preset> GeneratePrimaryAndFallbackPresets(Specification specification)
        {
            #region Initial extraction of parameters from specification.

            var emptyPreset = new Dictionary<EPreset, Preset>();
            var gameMode = specification.GameMode;
            var enabledVehicleClassesByBranch = specification.BranchSpecifications.ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value.VehicleClasses);
            var enabledNationCountryPairs = specification.NationSpecifications.Values.SelectMany(nationSpecification => nationSpecification.Countries.Select(country => new NationCountryPair(nationSpecification.Nation, country)));

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Filtering by countiries.

            var vehiclesWithCountryFilter = SelectVehiclesWithCountryFilter(enabledNationCountryPairs);

            if (vehiclesWithCountryFilter is null)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Filtering by classes.

            var availableVehicleClasses = vehiclesWithCountryFilter.Select(vehicle => vehicle.Class).Distinct().ToList();
            var validVehicleClasses = enabledVehicleClassesByBranch.Values.SelectMany(branchVehicleClasses => branchVehicleClasses.Where(vehicleClass => vehicleClass.IsIn(availableVehicleClasses)));
            var vehiclesWithClassFilter = SelectVehiclesWithClassFilter(validVehicleClasses, vehiclesWithCountryFilter);

            if (vehiclesWithClassFilter is null)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
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

            var vehiclesFromNation = vehiclesWithClassFilter.Where(vehicle => vehicle.Nation.AsEnumerationItem == nation);

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Branch selection.

            var vehiclesByBranches = vehiclesFromNation.GroupBy(vehicle => vehicle.Branch.AsEnumerationItem).ToDictionary(group => group.Key, group => group.AsEnumerable());
            var validBranches = SelectValidBranches(vehiclesByBranches);

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
            var validEconomicRanks = SelectValidEconomicRanks(enabledEconomicRanks, economicRanksWithVehicles, getFormattedBattleRating, nation, mainBranch);

            if (validEconomicRanks is null)
                return new Dictionary<EPreset, Preset> { { EPreset.Primary, new Preset(nation, mainBranch, string.Empty, new List<IVehicle>()) } };

            var economicRank = _randomizer.GetRandom(validEconomicRanks);
            var battleRating = GetBattleRating(economicRank, getFormattedBattleRating);
            var formattedBattleRating = getFormattedBattleRating(economicRank);
            var battleRatingBracket = new Interval<decimal>(true, battleRating - _maximumBattleRatingDifference, battleRating, true);

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------

            var vehiclesByBranchesAndBattleRatings = new VehiclesByBranchesAndBattleRating
            (
                presetComposition
                    .Keys
                    .Select(branch => new { Branch = branch, Vehicles = vehiclesForPreset.Where(vehicle => vehicle.Branch.AsEnumerationItem == branch).OrderByHighestBattleRating(_vehicleSelector, gameMode, battleRatingBracket) })
                    .ToDictionary(item => item.Branch, item => new VehiclesByBattleRating(item.Vehicles))
            );

            void addPreset(EPreset presetType) => presets.Add(presetType, new Preset(nation, mainBranch, formattedBattleRating, GetRandomVehiclesForPreset(vehiclesByBranchesAndBattleRatings, presetComposition, crewSlotAmount)));

            addPreset(EPreset.Primary);
            addPreset(EPreset.Fallback);

            return presets;
        }
    }
}