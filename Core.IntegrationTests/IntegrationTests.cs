﻿using Core.Csv.WarThunder.Helpers;
using Core.Csv.WarThunder.Helpers.Interfaces;
using Core.DataBase.Extensions;
using Core.DataBase.Tests.Enumerations;
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

            var wpCostJsonText = _manager.GetJsonText(blkxFiles, EFile.CharVromfs.GeneralVehicleData);
            var unitTagsJsonText = _manager.GetJsonText(blkxFiles, EFile.CharVromfs.AdditionalVehicleData);
            var researchTreeJsonText = _manager.GetJsonText(blkxFiles, EFile.CharVromfs.ResearchTreeData);

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

                    // crew
                    vehicleCollection.All(vehicle => vehicle.BaseCrewTrainCostInSilver >= 0).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.ExpertCrewTrainCostInSilver > 0).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.AceCrewTrainCostInGold > 0).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.AceCrewTrainCostInResearch > 0).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.CrewCount > 0).Should().BeTrue();
                    vehicleCollection.Any(vehicle => vehicle.MinumumCrewCountToOperate <= 0).Should().BeFalse();
                    vehicleCollection.All(vehicle => vehicle.GunnersCount >= 0).Should().BeTrue();

                    // general
                    vehicleCollection.All(vehicle => vehicle.Id != 0).Should().BeTrue();
                    vehicleCollection.Any(vehicle => string.IsNullOrWhiteSpace(vehicle.GaijinId)).Should().BeFalse();
                    vehicleCollection.Any(vehicle => string.IsNullOrWhiteSpace(vehicle.MoveType)).Should().BeFalse();
                    vehicleCollection.Any(vehicle => string.IsNullOrWhiteSpace(vehicle.Class)).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.UnlockCostInResearch < 0).Should().BeFalse();
                    vehicleCollection.All(vehicle => vehicle.PurchaseCostInSilver >= 0).Should().BeTrue();
                    vehicleCollection.Any(vehicle => vehicle.PurchaseCostInGold < 0).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.DiscountedPurchaseCostInGold <= 0).Should().BeFalse();
                    vehicleCollection.All(vehicle => vehicle.NumberOfSpawns.Event is null || vehicle.NumberOfSpawns.Event == 3).Should().BeTrue();
                    vehicleCollection.Any(vehicle => vehicle.NumberOfSpawns.Arcade <= 0 || vehicle.NumberOfSpawns.Realistic <= 0 || vehicle.NumberOfSpawns.Simulator <= 0).Should().BeFalse();

                    // graphical
                    vehicleCollection.Any(vehicle => vehicle.BulletsIconParam < 0).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.WeaponMask < 0).Should().BeFalse();

                    // modifications
                    vehicleCollection.All(vehicle => vehicle.AmountOfModificationsResearchedIn_Tier0_RequiredToUnlock_Tier1 == 1).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.AmountOfModificationsResearchedIn_Tier1_RequiredToUnlock_Tier2 >= 1).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.AmountOfModificationsResearchedIn_Tier2_RequiredToUnlock_Tier3 >= 1).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.AmountOfModificationsResearchedIn_Tier3_RequiredToUnlock_Tier4 >= 1).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.BackupSortieCostInGold > 0).Should().BeTrue();

                    // performance
                    vehicleCollection.Any(vehicle => vehicle.Speed <= 0m).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.MaximumFireExtinguishingTime <= 0).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.HullBreachRepairSpeed <= 0).Should().BeFalse();

                    // rank
                    vehicleCollection.All(vehicle => vehicle.Rank > 0).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.EconomicRank.Arcade >= 0 && vehicle.EconomicRank.Realistic >= 0).Should().BeTrue();
                    vehicleCollection.Any(vehicle => vehicle.EconomicRank.Simulator < 0).Should().BeFalse();
                    vehicleCollection.All(vehicle => vehicle.BattleRatingFormatted.Arcade.Matches(Vehicle.BattleRatingRegExPattern) && vehicle.BattleRatingFormatted.Realistic.Matches(Vehicle.BattleRatingRegExPattern)).Should().BeTrue();

                    // research tree data
                    vehicleCollection.Any(vehicle => vehicle.ResearchTreeData != null).Should().BeTrue();

                    var vehiclesWithResearchTreeData = vehicleCollection.Where(vehicle => vehicle.ResearchTreeData is VehicleResearchTreeData);
                    vehiclesWithResearchTreeData.All(vehicle => string.IsNullOrEmpty(vehicle.ResearchTreeData.GaijinId)).Should().BeFalse();
                    vehiclesWithResearchTreeData.All(vehicle => vehicle.ResearchTreeData.Vehicle == vehicle).Should().BeTrue();
                    vehiclesWithResearchTreeData.All(vehicle => vehicle.ResearchTreeData.Rank == vehicle.Rank).Should().BeTrue();
                    vehiclesWithResearchTreeData.Any(vehicle => vehicle.ResearchTreeData.PresetCellCoordinatesWithinRank is List<int> coordinates && coordinates.Count() == 2).Should().BeTrue();
                    vehiclesWithResearchTreeData.All(vehicle => vehicle.ResearchTreeData.CellCoordinatesWithinRank.Count() == 2).Should().BeTrue();
                    vehiclesWithResearchTreeData.Any(vehicle => !string.IsNullOrEmpty(vehicle.ResearchTreeData.RequiredVehicleGaijinId)).Should().BeTrue();
                    vehiclesWithResearchTreeData.Any(vehicle => vehicle.ResearchTreeData.FolderIndex.HasValue).Should().BeTrue();
                    vehiclesWithResearchTreeData.Any(vehicle => !string.IsNullOrEmpty(vehicle.ResearchTreeData.CategoryOfHiddenVehicles)).Should().BeTrue();
                    vehiclesWithResearchTreeData.Any(vehicle => vehicle.ResearchTreeData.ShowOnlyWhenBought.HasValue).Should().BeTrue();
                    vehiclesWithResearchTreeData.Any(vehicle => !string.IsNullOrEmpty(vehicle.ResearchTreeData.PlatformGaijinIdVehicleIsAvailableOn)).Should().BeTrue();
                    vehiclesWithResearchTreeData.Any(vehicle => !string.IsNullOrEmpty(vehicle.ResearchTreeData.PlatformGaijinIdVehicleIsHiddenOn)).Should().BeTrue();
                    vehiclesWithResearchTreeData.Any(vehicle => !string.IsNullOrEmpty(vehicle.ResearchTreeData.HideCondition)).Should().BeTrue();
                    vehiclesWithResearchTreeData.Any(vehicle => vehicle.ResearchTreeData.MarketplaceId.HasValue).Should().BeTrue();

                    // repairs
                    vehicleCollection.All(vehicle => vehicle.RepairTimeWithCrew.AsDictionary().All(keyValuePair => keyValuePair.Value is null || keyValuePair.Value >= 0m)).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.RepairTimeWithoutCrew.AsDictionary().All(keyValuePair => keyValuePair.Value is null || keyValuePair.Value >= 0m)).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.RepairCost.AsDictionary().All(keyValuePair => keyValuePair.Value is null || keyValuePair.Value >= 0)).Should().BeTrue();
                    vehicleCollection.Any(vehicle => vehicle.FreeRepairs < 0).Should().BeFalse();

                    // rewards
                    vehicleCollection.All(vehicle => vehicle.BattleTimeAward.Arcade > 0).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.BattleTimeAward.Realistic > 0).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.BattleTimeAward.Simulator >= 0).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.AverageAward.Arcade > 0).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.AverageAward.Realistic > 0).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.AverageAward.Simulator >= 0).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.RewardMultiplier.AsDictionary().All(keyValuePair => keyValuePair.Value is null || keyValuePair.Value > 0m)).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.VisualRewardMultiplier.AsDictionary().All(keyValuePair => keyValuePair.Value is null || keyValuePair.Value > 0m)).Should().BeTrue();
                    vehicleCollection.Any(vehicle => vehicle.VisualPremiumRewardMultiplier.Arcade <= 0m || vehicle.VisualPremiumRewardMultiplier.Realistic <= 0m || vehicle.VisualPremiumRewardMultiplier.Simulator <= 0m).Should().BeFalse();
                    vehicleCollection.All(vehicle => vehicle.ResearchRewardMultiplier > 0m).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.GroundKillRewardMultiplier > 0m).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.BattleTime.Arcade > 0).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.BattleTime.Realistic > 0).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.BattleTime.Simulator >= 0).Should().BeTrue();

                    // weapons
                    vehicleCollection.Any(vehicle => vehicle.TurretTraverseSpeeds?.Any(value => value < 0m) ?? false).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.MachineGunReloadTime <= 0m).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.CannonReloadTime <= 0m).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.GunnerReloadTime <= 0m).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.MaximumAmmunition <= 0).Should().BeFalse();
                }

                var vehiclesBeforePersistence = _jsonHelper.DeserializeList<Vehicle>(dataRepository, wpCostJsonText);
                
                foreach (var vehicle in vehiclesBeforePersistence)
                {
                    if (additionalVehicleData.TryGetValue(vehicle.GaijinId, out var additionalDataEntry))
                        vehicle.InitializeWithDeserializedAdditionalVehicleDataJson(additionalDataEntry);

                    if (researchTreeData.TryGetValue(vehicle.GaijinId, out var researchTreeEntry))
                        vehicle.InitializeWithDeserializedResearchTreeJson(researchTreeEntry);
                }

                assert(vehiclesBeforePersistence);

                dataRepository.PersistNewObjects();
                var vehiclesAfterPersistence = dataRepository.Query<IVehicle>();

                // assert
                assert(vehiclesAfterPersistence);
                vehiclesAfterPersistence.IsEquivalentTo(vehiclesBeforePersistence, 1, _ignoredPropertyNames).Should().BeTrue();
            }
        }
    }
}