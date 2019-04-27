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

            vehicles.All(vehicle => !vehicle.Value.GaijinId.IsNullOrWhiteSpaceFluently()).Should().BeTrue();
            vehicles.All(vehicle => !vehicle.Value.Nation.IsNullOrWhiteSpaceFluently()).Should().BeTrue();
            vehicles.All(vehicle => !vehicle.Value.MoveType.IsNullOrWhiteSpaceFluently()).Should().BeTrue();
            vehicles.All(vehicle => !vehicle.Value.Class.IsNullOrWhiteSpaceFluently()).Should().BeTrue();

            vehicles.Any(vehicle => vehicle.Value.PurchaseCostInSilver == 0).Should().BeTrue(); // non-purchasable
            vehicles.Any(vehicle => vehicle.Value.PurchaseCostInSilver > 0).Should().BeTrue();

            vehicles.Any(vehicle => vehicle.Value.NumberOfSpawnsInSimulation == null).Should().BeTrue(); // air and naval vehicles, plus some heavy tanks
            vehicles.Any(vehicle => vehicle.Value.NumberOfSpawnsInSimulation == 1).Should().BeTrue(); // attack helicopters and some tanks
            vehicles.Any(vehicle => vehicle.Value.NumberOfSpawnsInSimulation == 2).Should().BeTrue(); // SPAAGs and other ground vehicles

            vehicles.Any(vehicle => vehicle.Value.BaseCrewTrainCostInSilver == 0).Should().BeTrue(); // reserve vehicles
            vehicles.Any(vehicle => vehicle.Value.BaseCrewTrainCostInSilver > 0).Should().BeTrue();

            vehicles.All(vehicle => vehicle.Value.ExpertCrewTrainCostInSilver > 0).Should().BeTrue();
        }

        #endregion Methods: Deserialization
    }
}
