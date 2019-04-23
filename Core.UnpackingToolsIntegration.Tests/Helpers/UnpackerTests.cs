using System;
using System.IO;
using Core.DataBase.Tests;
using Core.Helpers;
using Core.Helpers.Interfaces;
using Core.Helpers.Logger.Enumerations;
using Core.UnpackingToolsIntegration.Enumerations;
using Core.UnpackingToolsIntegration.Helpers;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.WarThunderExtractionToolsIntegration;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.UnpackingToolsIntegration.Tests.Helpers
{
    /// <summary> See <see cref="Unpacker"/>. </summary>
    [TestClass]
    public class UnpackerTests
    {
        private IFileManager _fileManager;
        private IUnpacker _unpacker;
        private string _rootDirectory;
        private string _defaultTempDirectory;

        #region Internal Methods

        [TestInitialize]
        public void Initialize()
        {
            _fileManager = new FileManager(Presets.Logger);
            _unpacker = new Unpacker(Presets.Logger, _fileManager);
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
            _fileManager.DeleteDirectory(_rootDirectory);

            Settings.TempLocation = _defaultTempDirectory;
        }

        #endregion Internal Methods
        #region Tests: Unpack

        [TestMethod]
        public void Unpack_OutputFolderShouldContainFiles()
        {
            // arrange
            var sourceFile = _fileManager.GetFileInfo(Settings.WarThunderLocation, EFile.StatAndBalanceParameters);

            // act
            var outputDirectory = new DirectoryInfo(_unpacker.Unpack(sourceFile));

            // assert
            outputDirectory.GetFiles().Should().NotBeEmpty();
        }

        #endregion Tests: Unpack
    }
}
