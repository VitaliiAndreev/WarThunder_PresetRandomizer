using Core.DataBase.Helpers;
using Core.DataBase.Tests.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using Core.Extensions;
using Core.Helpers;
using Core.Helpers.Interfaces;
using Core.Helpers.Logger.Enumerations;
using Core.Json.Helpers;
using Core.Json.WarThunder.Helpers.Interfaces;
using Core.Tests;
using Core.UnpackingToolsIntegration.Enumerations;
using Core.UnpackingToolsIntegration.Helpers;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.WarThunderExtractionToolsIntegration;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Core.IntegrationTests
{
    [TestClass]
    public class IntegrationTests
    {
        private IFileManager _fileManager;
        private IFileReader _fileReader;
        private IUnpacker _unpacker;
        private IJsonHelperWarThunder _jsonHelper;
        private string _rootDirectory;
        private string _defaultTempDirectory;

        #region Internal Methods

        [TestInitialize]
        public void Initialize()
        {
            _fileManager = new FileManager(Presets.Logger);
            _fileReader = new FileReader(Presets.Logger);
            _unpacker = new Unpacker(Presets.Logger, _fileManager);
            _jsonHelper = new JsonHelperWarThunder(Presets.Logger);
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
        public void DeserializeAndPersistVehicleList()
        {
            // arrange
            var sourceFiles = new List<FileInfo>
            {
                _fileManager.GetFileInfo(Settings.WarThunderLocation, EFile.StatAndBalanceParameters)
            };

            var outputDirectories = new List<DirectoryInfo>();

            foreach (var sourceFile in sourceFiles)
            {
                var outputDirectory = new DirectoryInfo(_unpacker.Unpack(sourceFile));
                outputDirectories.Add(outputDirectory);
                _unpacker.Unpack(outputDirectory, ETool.BlkUnpacker);
            }

            var blkxFiles = new List<FileInfo>();

            foreach (var outputDirectory in outputDirectories)
                blkxFiles.AddRange(outputDirectory.GetFiles($"{ECharacter.Asterisk}{ECharacter.Period}{EFileExtension.Blkx}", SearchOption.AllDirectories));

            // act
            var wpCostJson = _fileReader.Read(blkxFiles.First(file => file.Name.Contains(EFile.GeneralVehicleData)));
            var databaseFileName = $"{ToString()}.{MethodBase.GetCurrentMethod().Name}()";

            using (var dataRepository = new DataRepository(databaseFileName, true, Assembly.Load(EAssemblies.WarThunderMappingAssembly), Presets.Logger))
            {
                void assert(IEnumerable<IVehicle> vehicleCollection)
                {
                    vehicleCollection.Count().Should().BeGreaterThan(1300);

                    // crew
                    vehicleCollection.All(vehicle => vehicle.BaseCrewTrainCostInSilver >= 0).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.ExpertCrewTrainCostInSilver > 0).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.AceCrewTrainCostInGold > 0).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.AceCrewTrainCostInResearch > 0).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.CrewCount > 0).Should().BeTrue();
                    vehicleCollection.Any(vehicle => vehicle.MinumumCrewCountToOperate <= 0).Should().BeFalse();
                    vehicleCollection.All(vehicle => vehicle.GunnersCount >= 0).Should().BeTrue();
                    // general
                    vehicleCollection.Any(vehicle => vehicle.GaijinId.IsNullOrWhiteSpaceFluently()).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.Nation.IsNullOrWhiteSpaceFluently()).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.MoveType.IsNullOrWhiteSpaceFluently()).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.Class.IsNullOrWhiteSpaceFluently()).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.UnlockCostInResearch < 0).Should().BeFalse();
                    vehicleCollection.All(vehicle => vehicle.PurchaseCostInSilver >= 0).Should().BeTrue();
                    vehicleCollection.Any(vehicle => vehicle.PurchaseCostInGold < 0).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.DiscountedPurchaseCostInGold <= 0).Should().BeFalse();
                    vehicleCollection.All(vehicle => vehicle.NumberOfSpawns[EGameMode.Event] is null || vehicle.NumberOfSpawns[EGameMode.Event] == 3).Should().BeTrue();
                    vehicleCollection.Any(vehicle => vehicle.NumberOfSpawns[EGameMode.Arcade] <= 0 || vehicle.NumberOfSpawns[EGameMode.Realistic] <= 0 || vehicle.NumberOfSpawns[EGameMode.Simulator] <= 0).Should().BeFalse();
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
                    vehicleCollection.All(vehicle => vehicle.EconomicRanks[EGameMode.Arcade] >= 0 && vehicle.EconomicRanks[EGameMode.Realistic] >= 0).Should().BeTrue();
                    vehicleCollection.Any(vehicle => vehicle.EconomicRanks[EGameMode.Simulator] < 0).Should().BeFalse();
                    vehicleCollection.All(vehicle => vehicle.BattleRatings[EGameMode.Arcade].Matches(Vehicle.BattleRatingRegExPattern) && vehicle.BattleRatings[EGameMode.Realistic].Matches(Vehicle.BattleRatingRegExPattern)).Should().BeTrue();
                    // repairs
                    vehicleCollection.All(vehicle => vehicle.RepairTimesWithCrew.All(keyValuePair => keyValuePair.Value >= 0m)).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.RepairTimesWithoutCrew.All(keyValuePair => keyValuePair.Value >= 0m)).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.RepairCosts.All(keyValuePair => keyValuePair.Value >= 0)).Should().BeTrue();
                    vehicleCollection.Any(vehicle => vehicle.FreeRepairs < 0).Should().BeFalse();
                    // rewards
                    vehicleCollection.All(vehicle => vehicle.BattleTimeAwards.All(keyValuePair => keyValuePair.Value > 0)).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.AverageAwards.All(keyValuePair => keyValuePair.Value > 0)).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.RewardMultipliers.All(keyValuePair => keyValuePair.Value > 0m)).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.VisualRewardMultipliers.All(keyValuePair => keyValuePair.Value > 0m)).Should().BeTrue();
                    vehicleCollection.Any(vehicle => vehicle.VisualPremiumRewardMultipliers[EGameMode.Arcade] <= 0m || vehicle.VisualPremiumRewardMultipliers[EGameMode.Realistic] <= 0m || vehicle.VisualPremiumRewardMultipliers[EGameMode.Simulator] <= 0m).Should().BeFalse();
                    vehicleCollection.All(vehicle => vehicle.ResearchRewardMultiplier > 0m).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.GroundKillRewardMultiplier > 0m).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.BattleTimes.All(keyValuePair => keyValuePair.Value > 0m)).Should().BeTrue();
                    // weapons
                    vehicleCollection.Any(vehicle => vehicle.TurretTraverseSpeeds?.Any(value => value <= 0m) ?? false).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.MachineGunReloadTime <= 0m).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.CannonReloadTime?.All(value => value <= 0m) ?? false).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.GunnerReloadTime <= 0m).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.MaximumAmmunition <= 0).Should().BeFalse();
                }

                var vehiclesBeforePersistence = _jsonHelper.DeserializeList<Vehicle>(dataRepository, wpCostJson);
                assert(vehiclesBeforePersistence);

                dataRepository.PersistNewObjects();
                var vehiclesAfterPersistence = dataRepository.Query<IVehicle>();

                // assert
                assert(vehiclesAfterPersistence);
                vehiclesAfterPersistence.IsEquivalentTo(vehiclesBeforePersistence, 1).Should().BeTrue();
            }
        }
    }
}