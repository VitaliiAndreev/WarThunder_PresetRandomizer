using Core.Enumerations.Logger;
using Core.Helpers;
using Core.Helpers.Interfaces;
using Core.Tests;
using Core.UnpackingToolsIntegration.Enumerations;
using Core.UnpackingToolsIntegration.Helpers;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.WarThunderExtractionToolsIntegration;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Core.UnpackingToolsIntegration.Tests.Helpers
{
    /// <summary>
    /// See <see cref="Parser"/>.
    /// These unit tests require a War Thunder client to be installed (see <see cref="Settings.WarThunderLocation"/>) and updated at least once.
    /// To skip these, comment the <see cref="TestClassAttribute"/> out.
    /// </summary>
    [TestClass]
    public class ParserTests
    {
        private IFileManager _fileManager;
        private IWarThunderFileReader _fileReader;
        private IParser _parser;
        private string _rootDirectory;
        private string _defaultWarThunderDirectory;

        #region Internal Methods

        [TestInitialize]
        public void Initialize()
        {
            _fileManager = new FileManager(Presets.Logger);
            _fileReader = new WarThunderFileReader(Presets.Logger);
            _parser = new Parser(Presets.Logger);
            _rootDirectory = $"{Directory.GetCurrentDirectory()}\\TestFiles";
            _defaultWarThunderDirectory = Settings.WarThunderLocation;

            if (!Directory.Exists(_rootDirectory))
                Directory.CreateDirectory(_rootDirectory);
            else
                _fileManager.EmptyDirectory(_rootDirectory);
        }

        [TestCleanup]
        public void CleanUp()
        {
            Presets.Logger.LogInfo(ECoreLogCategory.UnitTests, ECoreLogMessage.CleanUpAfterUnitTestStartsHere);
            Presets.CleanUp();
            _fileManager.DeleteDirectory(_rootDirectory);

            Settings.WarThunderLocation = _defaultWarThunderDirectory;
        }

        #endregion Internal Methods
        #region Tests: GetClientVersion()

        [TestMethod]
        public void ReadClientVersion_CurrentVersionShouldNotBePreviousVersion()
        {
            // arrange
            void copyFile(string relativeFilePath) => _fileManager.CopyFile($"{Settings.WarThunderLocation}\\{relativeFilePath}", _rootDirectory, true, true);
            copyFile(EFile.RootFolder.CurrentIntallData);
            copyFile(EFile.RootFolder.PreviousVersionInstallData);

            Settings.WarThunderLocation = _rootDirectory;

            var currentInstallData = _fileReader.ReadInstallData(EClientVersion.Current);
            var previousVersionInstallData = _fileReader.ReadInstallData(EClientVersion.Previous);

            // act
            var currentVersion = _parser.GetClientVersion(currentInstallData);
            var previousVersion = _parser.GetClientVersion(previousVersionInstallData);

            // assert
            currentVersion.Should().NotBe(previousVersion);
        }

        #endregion Tests: GetClientVersion()
    }
}