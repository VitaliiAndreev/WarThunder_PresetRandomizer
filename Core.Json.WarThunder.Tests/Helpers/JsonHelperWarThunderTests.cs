using Core.DataBase.WarThunder.Objects.Json;
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

namespace Core.Json.WarThunder.Tests.Helpers
{
    /// <summary> See <see cref="JsonHelperWarThunder"/></summary>
    [TestClass]
    public class JsonHelperWarThunderTests
    {
        #region Fake Classes

        private class TestClass_NoDuplicates
        {
            public int Property1 { get; set; }

            public int[] Array1 { get; set; }
        }

        private class TestClass_DuplicatesAggregated
        {
            public int[] Property1 { get; set; }
        }

        #endregion Fake Classes
        #region Fields

        private IFileManager _fileManager;
        private IFileReader _fileReader;
        private IUnpacker _unpacker;
        private IJsonHelperWarThunder _jsonHelper;
        private string _rootDirectory;
        private string _defaultTempDirectory;

        #endregion Fields
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
        #region Tests: DeserializeObject()

        [TestMethod]
        public void DeserializeObject_NoDuplicates()
        {
            // arrange
            var propertyName1 = "Property1";
            var propertyValue1 = 17;
            var arrayName1 = "Array1";
            var arrayValue1 = new int[] { 1, 2 };
            var jsonObjectText = "\r\n{\r\n\"" + propertyName1 + "\": " + propertyValue1 + ",\r\n\"" + arrayName1 + "\":\r\n[\r\n" + arrayValue1[0] + ",\r\n" + arrayValue1[1] + "\r\n]\r\n}";

            // act
            var testObject = _jsonHelper.DeserializeObject<TestClass_NoDuplicates>(jsonObjectText);

            // assert
            testObject.Property1.Should().Be(propertyValue1);
            testObject.Array1.Should().BeEquivalentTo(arrayValue1);
        }

        [TestMethod]
        public void DeserializeObject_DuplicatesAggregatedIntoArrays()
        {
            // arrange
            var propertyName1 = "Property1";
            var propertyValue1 = 17;
            var propertyValue1d = 42;
            var jsonObjectText = "[\r\n{\r\n\"" + propertyName1 + "\": " + propertyValue1 + "\r\n},\r\n{\r\n\"" + propertyName1 + "\": " + propertyValue1d + "\r\n}\r\n]";

            // act
            var testObject = _jsonHelper.DeserializeObject<TestClass_DuplicatesAggregated>(jsonObjectText);

            // assert
            testObject.Property1.Should().BeEquivalentTo(new int[] { propertyValue1, propertyValue1d });
        }

        #endregion Tests: DeserializeObject()
        #region Tests: DeserializeList()

        [TestMethod]
        public void DeserializeList_Vehicles()
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
            var vehicles = _jsonHelper.DeserializeList<VehicleDeserializedFromJson>(wpCostJson);

            // assert
            vehicles.Count().Should().BeGreaterThan(1300);
            
            /// crew
            vehicles.All(vehicle => vehicle.BaseCrewTrainCostInSilver >= 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.ExpertCrewTrainCostInSilver > 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.AceCrewTrainCostInGold > 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.AceCrewTrainCostInResearch > 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.CrewCount > 0).Should().BeTrue();
            vehicles.Any(vehicle => vehicle.MinumumCrewCountToOperate <= 0).Should().BeFalse();
            vehicles.All(vehicle => vehicle.GunnersCount >= 0).Should().BeTrue();
            /// general
            vehicles.Any(vehicle => vehicle.GaijinId.IsNullOrWhiteSpaceFluently()).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.Nation.IsNullOrWhiteSpaceFluently()).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.MoveType.IsNullOrWhiteSpaceFluently()).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.Class.IsNullOrWhiteSpaceFluently()).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.UnlockCostInResearch < 0).Should().BeFalse();
            vehicles.All(vehicle => vehicle.PurchaseCostInSilver >= 0).Should().BeTrue();
            vehicles.Any(vehicle => vehicle.PurchaseCostInGold < 0).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.DiscountedPurchaseCostInGold <= 0).Should().BeFalse();
            vehicles.All(vehicle => vehicle.NumberOfSpawnsInEvents is null || vehicle.NumberOfSpawnsInEvents == 3).Should().BeTrue();
            vehicles.Any(vehicle => vehicle.NumberOfSpawnsInArcade <= 0).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.NumberOfSpawnsInRealistic <= 0).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.NumberOfSpawnsInSimulation <= 0).Should().BeFalse();
            /// graphical
            vehicles.Any(vehicle => vehicle.BulletsIconParam < 0).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.WeaponMask < 0).Should().BeFalse();
            /// modifications
            vehicles.All(vehicle => vehicle.AmountOfModificationsResearchedIn_Tier0_RequiredToUnlock_Tier1 == 1).Should().BeTrue();
            vehicles.All(vehicle => vehicle.AmountOfModificationsResearchedIn_Tier1_RequiredToUnlock_Tier2 >= 1).Should().BeTrue();
            vehicles.All(vehicle => vehicle.AmountOfModificationsResearchedIn_Tier2_RequiredToUnlock_Tier3 >= 1).Should().BeTrue();
            vehicles.All(vehicle => vehicle.AmountOfModificationsResearchedIn_Tier3_RequiredToUnlock_Tier4 >= 1).Should().BeTrue();
            vehicles.All(vehicle => vehicle.BackupSortie.PurchaseCostInSilver == 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.BackupSortie.PurchaseCostInGold > 0).Should().BeTrue();
            /// performance
            vehicles.Any(vehicle => vehicle.Speed <= 0m).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.TurretTraverseSpeeds?.Any (value => value <= 0m) ?? false).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.MachineGunReloadTime <= 0m).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.CannonReloadTime?.All(value => value <= 0m) ?? false).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.GunnerReloadTime <= 0m).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.MaximumAmmunition <= 0).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.MaximumFireExtinguishingTime <= 0).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.HullBreachRepairSpeed <= 0).Should().BeFalse();
            /// rank
            vehicles.All(vehicle => vehicle.Rank > 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.EconomicRankInArcade >= 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.EconomicRankInRealistic >= 0).Should().BeTrue();
            vehicles.Any(vehicle => vehicle.EconomicRankInSimulation < 0).Should().BeFalse();
            /// repairs
            vehicles.All(vehicle => vehicle.RepairTimeWithCrewInArcade >= 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.RepairTimeWithCrewInRealistic >= 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.RepairTimeWithCrewInSimulation >= 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.RepairTimeWithoutCrewInArcade >= 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.RepairTimeWithoutCrewInRealistic >= 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.RepairTimeWithoutCrewInSimulation >= 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.RepairCostInArcade >= 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.RepairCostInRealistic >= 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.RepairCostInSimulation >= 0).Should().BeTrue();
            vehicles.Any(vehicle => vehicle.FreeRepairs < 0).Should().BeFalse();
            /// rewards
            vehicles.All(vehicle => vehicle.BattleTimeAwardInArcade > 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.BattleTimeAwardInRealistic > 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.BattleTimeAwardInSimulation > 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.AverageAwardInArcade > 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.AverageAwardInRealistic > 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.AverageAwardInSimulation > 0).Should().BeTrue();
            vehicles.All(vehicle => vehicle.RewardMultiplierInArcade > 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.RewardMultiplierInRealistic > 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.RewardMultiplierInSimulation > 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.VisualRewardMultiplierInArcade > 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.VisualRewardMultiplierInRealistic > 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.VisualRewardMultiplierInSimulation > 0m).Should().BeTrue();
            vehicles.Any(vehicle => vehicle.VisualPremiumRewardMultiplierInArcade <= 0m).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.VisualPremiumRewardMultiplierInRealistic <= 0m).Should().BeFalse();
            vehicles.Any(vehicle => vehicle.VisualPremiumRewardMultiplierInSimulation <= 0m).Should().BeFalse();
            vehicles.All(vehicle => vehicle.ResearchRewardMultiplier > 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.GroundKillRewardMultiplier > 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.BattleTimeArcade > 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.BattleTimeRealistic > 0m).Should().BeTrue();
            vehicles.All(vehicle => vehicle.BattleTimeSimulation > 0m).Should().BeTrue();
        }

        #endregion Tests: DeserializeList()
    }
}