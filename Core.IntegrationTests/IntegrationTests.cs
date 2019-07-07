using Core.DataBase.Tests.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Helpers;
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
            _unpacker = new Unpacker(_fileManager, Presets.Logger);
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
        #region Helper Methods

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

        #endregion Helper Methods
        [TestMethod]

        public void DeserializeAndPersistNationList()
        {
            // arrange
            var blkxFiles = GetBlkxFiles(EFile.RootFolder.StatAndBalanceParameters);
            var rankJsonText = GetJsonText(blkxFiles, EFile.CharVromfs.RankData);

            // act
            var databaseFileName = $"{ToString()}.{MethodBase.GetCurrentMethod().Name}()";

            using (var dataRepository = new DataRepositoryWarThunder(databaseFileName, true, Assembly.Load(EAssemblies.WarThunderMappingAssembly), Presets.Logger))
            {
                void assert(IEnumerable<INation> nationCollection)
                {
                    nationCollection.Count().Should().BeGreaterOrEqualTo(7);
                    nationCollection.Any(nation => nation.GaijinId.IsNullOrWhiteSpaceFluently()).Should().BeFalse();
                }

                var nationsBeforePersistence = _jsonHelper.DeserializeList<Nation>(dataRepository, rankJsonText);
                assert(nationsBeforePersistence);

                dataRepository.PersistNewObjects();
                var nationsAfterPersistence = dataRepository.Query<INation>();

                // assert
                assert(nationsAfterPersistence);
                nationsAfterPersistence.IsEquivalentTo(nationsBeforePersistence, 1).Should().BeTrue();
            }
        }

        [TestMethod]
        public void DeserializeAndPersistVehicleList()
        {
            // arrange
            var blkxFiles = GetBlkxFiles(EFile.RootFolder.StatAndBalanceParameters);
            var wpCostJsonText = GetJsonText(blkxFiles, EFile.CharVromfs.GeneralVehicleData);

            // act
            var databaseFileName = $"{ToString()}.{MethodBase.GetCurrentMethod().Name}()";

            using (var dataRepository = new DataRepositoryWarThunder(databaseFileName, true, Assembly.Load(EAssemblies.WarThunderMappingAssembly), Presets.Logger))
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
                    vehicleCollection.All(vehicle => !vehicle.Nation?.GaijinId.IsNullOrWhiteSpaceFluently() ?? false).Should().BeTrue();
                    vehicleCollection.Any(vehicle => vehicle.MoveType.IsNullOrWhiteSpaceFluently()).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.Class.IsNullOrWhiteSpaceFluently()).Should().BeFalse();
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
                    // repairs
                    vehicleCollection.All(vehicle => vehicle.RepairTimeWithCrew.AsDictionary().All(keyValuePair => keyValuePair.Value is null || keyValuePair.Value >= 0m)).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.RepairTimeWithoutCrew.AsDictionary().All(keyValuePair => keyValuePair.Value is null || keyValuePair.Value >= 0m)).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.RepairCost.AsDictionary().All(keyValuePair => keyValuePair.Value is null || keyValuePair.Value >= 0)).Should().BeTrue();
                    vehicleCollection.Any(vehicle => vehicle.FreeRepairs < 0).Should().BeFalse();
                    // rewards
                    vehicleCollection.All(vehicle => vehicle.BattleTimeAward.AsDictionary().All(keyValuePair => keyValuePair.Value is null || keyValuePair.Value > 0)).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.AverageAward.AsDictionary().All(keyValuePair => keyValuePair.Value is null || keyValuePair.Value > 0)).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.RewardMultiplier.AsDictionary().All(keyValuePair => keyValuePair.Value is null || keyValuePair.Value > 0m)).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.VisualRewardMultiplier.AsDictionary().All(keyValuePair => keyValuePair.Value is null || keyValuePair.Value > 0m)).Should().BeTrue();
                    vehicleCollection.Any(vehicle => vehicle.VisualPremiumRewardMultiplier.Arcade <= 0m || vehicle.VisualPremiumRewardMultiplier.Realistic <= 0m || vehicle.VisualPremiumRewardMultiplier.Simulator <= 0m).Should().BeFalse();
                    vehicleCollection.All(vehicle => vehicle.ResearchRewardMultiplier > 0m).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.GroundKillRewardMultiplier > 0m).Should().BeTrue();
                    vehicleCollection.All(vehicle => vehicle.BattleTime.AsDictionary().All(keyValuePair => keyValuePair.Value is null || keyValuePair.Value > 0m)).Should().BeTrue();
                    // weapons
                    vehicleCollection.Any(vehicle => vehicle.TurretTraverseSpeeds?.Any(value => value <= 0m) ?? false).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.MachineGunReloadTime <= 0m).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.CannonReloadTime?.All(value => value <= 0m) ?? false).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.GunnerReloadTime <= 0m).Should().BeFalse();
                    vehicleCollection.Any(vehicle => vehicle.MaximumAmmunition <= 0).Should().BeFalse();
                }

                var vehiclesBeforePersistence = _jsonHelper.DeserializeList<Vehicle>(dataRepository, wpCostJsonText);
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