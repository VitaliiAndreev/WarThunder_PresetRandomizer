using Core.DataBase.Tests;
using Core.Enumerations;
using Core.Extensions;
using Core.Helpers;
using Core.Helpers.Interfaces;
using Core.Helpers.Logger.Enumerations;
using Core.Json.Helpers;
using Core.Json.WarThunder.Helpers.Interfaces;
using Core.UnpackingToolsIntegration.Enumerations;
using Core.UnpackingToolsIntegration.Helpers;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.WarThunderExtractionToolsIntegration;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core.Json.WarThunder.Tests.Helpers
{
    /// <summary> See <see cref="JsonHelperWarThunder"/></summary>
    [TestClass]
    public class JsonHelperWarThunderTests
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
            Presets.Logger.LogInfo(ECoreLogCategory.UnitTests, ECoreLogMessage.CleanUpAfterUnitTestStartsHere);
            Presets.CleanUp();
            _fileManager.DeleteDirectory(_rootDirectory);

            Settings.TempLocation = _defaultTempDirectory;
        }

        #endregion Internal Methods
        #region Methods: Deserialization

        [TestMethod]
        public void DeserializeVehicleList()
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
            {
                blkxFiles.AddRange(outputDirectory.GetFiles($"{ECharacter.Asterisk}{ECharacter.Period}{EFileExtension.Blkx}", SearchOption.AllDirectories));
            }

            var wpCostJson = _fileReader.Read(blkxFiles.First(file => file.Name.Contains(EFile.GeneralVehicleData)));

            // act
            var vehicles = _jsonHelper.DeserializeVehicleList(wpCostJson);

            // assert
            vehicles.Count().Should().BeGreaterThan(1300);
            
            /// crew
            vehicles.All(vehicle => vehicle.Value.BaseCrewTrainCostInSilver >= 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.ExpertCrewTrainCostInSilver > 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.AceCrewTrainCostInGold > 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.AceCrewTrainCostInResearch > 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.CrewCount > 0).Should().BeTrue();
            vehicles.Any(vehicle => vehicle.Value.MinumumCrewCountToOperate <= 0).Should().BeFalse();
            vehicles.All(vehicle => vehicle.Value.GunnersCount >= 0).Should().BeTrue();
            /// general
            vehicles.Any(vehicle => vehicle.Value.GaijinId.IsNullOrWhiteSpaceFluently()).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.Value.Nation.IsNullOrWhiteSpaceFluently()).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.Value.MoveType.IsNullOrWhiteSpaceFluently()).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.Value.Class.IsNullOrWhiteSpaceFluently()).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.Value.UnlockCostInResearch < 0).Should().BeFalse();
            vehicles.All(vehicle => vehicle.Value.PurchaseCostInSilver >= 0).Should().BeTrue();
            vehicles.Any(vehicle => vehicle.Value.PurchaseCostInGold < 0).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.Value.DiscountedPurchaseCostInGold <= 0).Should().BeFalse();
            vehicles.All(vehicle => vehicle.Value.NumberOfSpawnsInEvents is null || vehicle.Value.NumberOfSpawnsInEvents == 3).Should().BeTrue();
            vehicles.Any(vehicle => vehicle.Value.NumberOfSpawnsInArcade <= 0).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.Value.NumberOfSpawnsInRealistic <= 0).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.Value.NumberOfSpawnsInSimulation <= 0).Should().BeFalse();
            /// graphical
            vehicles.Any(vehicle => vehicle.Value.BulletsIconParam < 0).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.Value.WeaponMask < 0).Should().BeFalse();
            /// modifications
            vehicles.All(vehicle => vehicle.Value.AmountOfModificationsResearchedIn_Tier0_RequiredToUnlock_Tier1 == 1).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.AmountOfModificationsResearchedIn_Tier1_RequiredToUnlock_Tier2 >= 1).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.AmountOfModificationsResearchedIn_Tier2_RequiredToUnlock_Tier3 >= 1).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.AmountOfModificationsResearchedIn_Tier3_RequiredToUnlock_Tier4 >= 1).Should().BeTrue();
            /// performance
            vehicles.Any(vehicle => vehicle.Value.Speed <= 0m).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.Value.TurretTraverseSpeeds?.Any (value => value <= 0m) ?? false).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.Value.MachineGunReloadTime <= 0m).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.Value.CannonReloadTime <= 0m).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.Value.GunnerReloadTime <= 0m).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.Value.MaximumAmmunition <= 0).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.Value.MaximumFireExtinguishingTime <= 0).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.Value.HullBreachRepairSpeed <= 0).Should().BeFalse();
            /// rank
            vehicles.All(vehicle => vehicle.Value.Rank > 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.EconomicRankInArcade >= 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.EconomicRankInRealistic >= 0).Should().BeTrue();
            vehicles.Any(vehicle => vehicle.Value.EconomicRankInSimulation < 0).Should().BeFalse();
            /// repairs
            vehicles.All(vehicle => vehicle.Value.RepairTimeWithCrewInArcade >= 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.RepairTimeWithCrewInRealistic >= 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.RepairTimeWithCrewInSimulation >= 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.RepairTimeWithoutCrewInArcade >= 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.RepairTimeWithoutCrewInRealistic >= 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.RepairTimeWithoutCrewInSimulation >= 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.RepairCostInArcade >= 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.RepairCostInRealistic >= 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.RepairCostInSimulation >= 0).Should().BeTrue();
            vehicles.Any(vehicle => vehicle.Value.FreeRepairs < 0).Should().BeFalse();
            /// rewards
            vehicles.All(vehicle => vehicle.Value.BattleTimeAwardInArcade > 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.BattleTimeAwardInRealistic > 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.BattleTimeAwardInSimulation > 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.AverageAwardInArcade > 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.AverageAwardInRealistic > 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.AverageAwardInSimulation > 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.RewardMultiplierInArcade > 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.RewardMultiplierInRealistic > 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.RewardMultiplierInSimulation > 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.VisualRewardMultiplierInArcade > 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.VisualRewardMultiplierInRealistic > 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.VisualRewardMultiplierInSimulation > 0m).Should().BeTrue();
            vehicles.Any(vehicle => vehicle.Value.VisualPremiumRewardMultiplierInArcade <= 0m).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.Value.VisualPremiumRewardMultiplierInRealistic <= 0m).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.Value.VisualPremiumRewardMultiplierInSimulation <= 0m).Should().BeFalse();
            vehicles.All(vehicle => vehicle.Value.ResearchRewardMultiplier > 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.GroundKillRewardMultiplier > 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.BattleTimeArcade > 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.BattleTimeRealistic > 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.Value.BattleTimeSimulation > 0m).Should().BeTrue();
        }

        #endregion Methods: Deserialization
    }
}
