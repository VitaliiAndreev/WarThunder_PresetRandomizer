using Core.Csv.WarThunder.Helpers;
using Core.Csv.WarThunder.Helpers.Interfaces;
using Core.DataBase.Extensions;
using Core.DataBase.Tests.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Helpers;
using Core.DataBase.WarThunder.Objects;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Json;
using Core.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Json.Helpers;
using Core.Json.WarThunder.Helpers.Interfaces;
using Core.Organization.Helpers;
using Core.Organization.Helpers.Interfaces;
using Core.Randomization.Helpers.Interfaces;
using Core.Tests;
using Core.UnpackingToolsIntegration.Enumerations;
using Core.UnpackingToolsIntegration.Helpers;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.WarThunderExtractionToolsIntegration;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Core.IntegrationTests
{
    [TestClass]
    public class IntegrationTests
    {
        #region Fields

        private readonly IEnumerable<string> _ignoredPropertyNames;

        private IWarThunderFileManager _fileManager;
        private IWarThunderFileReader _fileReader;
        private IUnpacker _unpacker;
        private IWarThunderJsonHelper _jsonHelper;
        private ICsvDeserializer _csvDeserializer;
        private Manager _manager;
        private string _rootDirectory;
        private string _defaultTempDirectory;

        #endregion Fields
        #region Internal Methods

        public IntegrationTests()
        {
            _ignoredPropertyNames = new List<string>
            {
                nameof(INation.AsEnumerationItem),
                nameof(IVehicle.RankAsEnumerationItem),
            };
        }

        [TestInitialize]
        public void Initialize()
        {
            _fileManager = new WarThunderFileManager(Presets.Logger);
            _fileReader = new WarThunderFileReader(Presets.Logger);
            _unpacker = new Unpacker(_fileManager, Presets.Logger);
            _jsonHelper = new WarThunderJsonHelper(Presets.Logger);
            _csvDeserializer = new CsvDeserializer(Presets.Logger);

            var settingsManager = new Mock<IWarThunderSettingsManager>();
            settingsManager
                .Setup(manager => manager.GetSetting(nameof(Settings.WarThunderLocation)))
                .Returns(Settings.WarThunderLocation);
            settingsManager
                .Setup(manager => manager.GetSetting(nameof(Settings.KlensysWarThunderToolsLocation)))
                .Returns(Settings.KlensysWarThunderToolsLocation);
            settingsManager
                .Setup(manager => manager.GetSetting(nameof(Settings.TempLocation)))
                .Returns(Settings.TempLocation);

            _manager = new Manager(_fileManager, _fileReader, settingsManager.Object, new Mock<IParser>().Object, _unpacker, _jsonHelper, _csvDeserializer, new Mock<IRandomizer>().Object, new Mock<IVehicleSelector>().Object, Presets.Logger);
            _rootDirectory = $"{Directory.GetCurrentDirectory()}\\TestFiles";
            _defaultTempDirectory = Settings.TempLocation;

            if (!Directory.Exists(_rootDirectory))
                Directory.CreateDirectory(_rootDirectory);
            else
                _fileManager.EmptyDirectory(_rootDirectory);

            Settings.TempLocation = _rootDirectory;
        }

        [TestCleanup]
        public void CleanUp()
        {
            Presets.Logger.LogInfo(ECoreLogCategory.IntegrationTests, ECoreLogMessage.CleanUpAfterIntegrationTestStartsHere);
            Presets.CleanUp();
            _fileManager.DeleteDirectory(_rootDirectory);

            Settings.TempLocation = _defaultTempDirectory;
        }

        public override string ToString() => nameof(IntegrationTests);

        #endregion Internal Methods

        [TestMethod]
        public void DeserializeAndPersistNationList()
        {
            // arrange
            var blkxFiles = _manager.GetBlkxFiles(EFile.WarThunder.StatAndBalanceParameters);
            var rankJsonText = _manager.GetJsonText(blkxFiles, EFile.CharVromfs.RankData);

            // act
            var databaseFileName = $"{ToString()}.{MethodBase.GetCurrentMethod().Name}()";

            using (var dataRepository = new DataRepositoryWarThunderWithoutSession(databaseFileName, true, Assembly.Load(EAssembly.WarThunderMappingAssembly), Presets.Logger))
            {
                static void assert(IEnumerable<INation> nationCollection)
                {
                    nationCollection.Count().Should().BeGreaterOrEqualTo(7);
                    nationCollection.Any(nation => string.IsNullOrWhiteSpace(nation.GaijinId)).Should().BeFalse();
                }

                var nationsBeforePersistence = _jsonHelper.DeserializeList<Nation>(dataRepository, rankJsonText);
                assert(nationsBeforePersistence);

                dataRepository.PersistNewObjects();
                var nationsAfterPersistence = dataRepository.Query<INation>();

                // assert
                assert(nationsAfterPersistence);
                nationsAfterPersistence.IsEquivalentTo(nationsBeforePersistence, 1, _ignoredPropertyNames).Should().BeTrue();
            }
        }

        [TestMethod]
        public void DeserializeAndPersistVehicleList()
        {
            // arrange
            var blkxFiles = _manager.GetBlkxFiles(EFile.WarThunder.StatAndBalanceParameters);
            var csvFiles = _manager.GetCsvFiles(EFile.WarThunder.LocalizationParameters);

            var wpCostJsonText = _manager.GetJsonText(blkxFiles, EFile.CharVromfs.GeneralVehicleData);
            var unitTagsJsonText = _manager.GetJsonText(blkxFiles, EFile.CharVromfs.AdditionalVehicleData);
            var researchTreeJsonText = _manager.GetJsonText(blkxFiles, EFile.CharVromfs.ResearchTreeData);
            var vehicleLocalizationRecords = _manager.GetCsvRecords(csvFiles, EFile.LangVromfs.Units);

            var additionalVehicleData = _jsonHelper.DeserializeList<VehicleDeserializedFromJsonUnitTags>(unitTagsJsonText).ToDictionary(vehicle => vehicle.GaijinId);
            var researchTreeData = _jsonHelper.DeserializeResearchTrees(researchTreeJsonText).SelectMany(researchTree => researchTree.Vehicles).ToDictionary(vehicle => vehicle.GaijinId);

            // act
            var databaseFileName = $"{ToString()}.{MethodBase.GetCurrentMethod().Name}()";

            using (var dataRepository = new DataRepositoryWarThunderWithoutSession(databaseFileName, true, Assembly.Load(EAssembly.WarThunderMappingAssembly), Presets.Logger))
            {
                static void assert(IEnumerable<IVehicle> vehicleCollection)
                {
                    vehicleCollection.Count().Should().BeGreaterThan(1300);

                    // association
                    vehicleCollection.All(vehicle => !string.IsNullOrWhiteSpace(vehicle.Nation?.GaijinId)).Should().BeTrue();
                    vehicleCollection.All(vehicle => !string.IsNullOrWhiteSpace(vehicle.Branch?.GaijinId)).Should().BeTrue();

                    // general
                    vehicleCollection.All(vehicle => vehicle.Id != 0).Should().BeTrue();
                    vehicleCollection.Any(vehicle => string.IsNullOrWhiteSpace(vehicle.GaijinId)).Should().BeFalse();
                    vehicleCollection.All(vehicle => vehicle.Country != ECountry.None).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.Class != EVehicleClass.None).Should().BeTrue();
                    vehicleCollection.Any(vehicle => vehicle.PurchaseCostInGold < 0).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.DiscountedPurchaseCostInGold <= 0).Should().BeFalse();

                    // localization
                    vehicleCollection.Count(vehicle => !string.IsNullOrWhiteSpace(vehicle.FullName?.English)).Should().BeGreaterThan(EInteger.Number.Thousand);
                    vehicleCollection.Count(vehicle => !string.IsNullOrWhiteSpace(vehicle.ResearchTreeName?.French)).Should().BeGreaterThan(EInteger.Number.Thousand);
                    vehicleCollection.Count(vehicle => !string.IsNullOrWhiteSpace(vehicle.ShortName?.Italian)).Should().BeGreaterThan(EInteger.Number.Thousand);
                    vehicleCollection.Count(vehicle => !string.IsNullOrWhiteSpace(vehicle.ClassName?.German)).Should().BeGreaterThan(EInteger.Number.Thousand);

                    // rank
                    vehicleCollection.All(vehicle => vehicle.Rank > 0).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.EconomicRank.Arcade >= 0 && vehicle.EconomicRank.Realistic >= 0).Should().BeTrue();
                    vehicleCollection.Any(vehicle => vehicle.EconomicRank.Simulator < 0).Should().BeFalse();
                    vehicleCollection.All(vehicle => vehicle.BattleRatingFormatted.Arcade.Matches(Vehicle.BattleRatingRegExPattern) && vehicle.BattleRatingFormatted.Realistic.Matches(Vehicle.BattleRatingRegExPattern)).Should().BeTrue();

                    // research tree data
                    vehicleCollection.Any(vehicle => vehicle.ResearchTreeData != null).Should().BeTrue();

                    var vehiclesWithResearchTreeData = vehicleCollection.Where(vehicle => vehicle.ResearchTreeData is VehicleResearchTreeData);
                    vehiclesWithResearchTreeData.All(vehicle => vehicle.ResearchTreeData.Vehicle == vehicle).Should().BeTrue();
                    vehiclesWithResearchTreeData.All(vehicle => vehicle.ResearchTreeData.Rank == vehicle.Rank).Should().BeTrue();
                    vehiclesWithResearchTreeData.Any(vehicle => vehicle.ResearchTreeData.PresetCellCoordinatesWithinRank is List<int> coordinates && coordinates.Count() == 2).Should().BeTrue();
                    vehiclesWithResearchTreeData.All(vehicle => vehicle.ResearchTreeData.CellCoordinatesWithinRank.Count() == 2).Should().BeTrue();
                    vehiclesWithResearchTreeData.Any(vehicle => !string.IsNullOrEmpty(vehicle.ResearchTreeData.RequiredVehicleGaijinId)).Should().BeTrue();
                    vehiclesWithResearchTreeData.Any(vehicle => vehicle.ResearchTreeData.FolderIndex.HasValue).Should().BeTrue();
                    vehiclesWithResearchTreeData.Any(vehicle => !string.IsNullOrEmpty(vehicle.ResearchTreeData.CategoryOfHiddenVehicles)).Should().BeTrue();
                    vehiclesWithResearchTreeData.Any(vehicle => vehicle.ResearchTreeData.ShowOnlyWhenBought).Should().BeTrue();
                    vehiclesWithResearchTreeData.Any(vehicle => !string.IsNullOrEmpty(vehicle.ResearchTreeData.PlatformGaijinIdVehicleIsAvailableOn)).Should().BeTrue();
                    vehiclesWithResearchTreeData.Any(vehicle => !string.IsNullOrEmpty(vehicle.ResearchTreeData.PlatformGaijinIdVehicleIsHiddenOn)).Should().BeTrue();
                    vehiclesWithResearchTreeData.Any(vehicle => !string.IsNullOrEmpty(vehicle.ResearchTreeData.HideCondition)).Should().BeTrue();
                    vehiclesWithResearchTreeData.Any(vehicle => vehicle.ResearchTreeData.MarketplaceId.HasValue).Should().BeTrue();
                }

                var vehiclesBeforePersistence = _jsonHelper.DeserializeList<Vehicle>(dataRepository, wpCostJsonText).ToDictionary(vehicle => vehicle.GaijinId, vehicle => vehicle as IVehicle);
                
                foreach (var vehicle in vehiclesBeforePersistence.Values)
                {
                    if (additionalVehicleData.TryGetValue(vehicle.GaijinId, out var additionalDataEntry))
                        vehicle.InitializeWithDeserializedAdditionalVehicleDataJson(additionalDataEntry);

                    if (researchTreeData.TryGetValue(vehicle.GaijinId, out var researchTreeEntry))
                        vehicle.InitializeWithDeserializedResearchTreeJson(researchTreeEntry);
                }
                _csvDeserializer.DeserializeVehicleLocalization(vehiclesBeforePersistence, vehicleLocalizationRecords);

                assert(vehiclesBeforePersistence.Values);

                dataRepository.PersistNewObjects();
                var vehiclesAfterPersistence = dataRepository.Query<IVehicle>();

                // assert
                assert(vehiclesAfterPersistence);
                vehiclesAfterPersistence.IsEquivalentTo(vehiclesBeforePersistence.Values, 1, _ignoredPropertyNames).Should().BeTrue();
            }
        }
    }
}