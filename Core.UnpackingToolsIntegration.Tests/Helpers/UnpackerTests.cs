using Core.Extensions;
using Core.Helpers;
using Core.Helpers.Interfaces;
using Core.Tests;
using Core.UnpackingToolsIntegration.Enumerations;
using Core.UnpackingToolsIntegration.Exceptions;
using Core.UnpackingToolsIntegration.Helpers;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.WarThunderExtractionToolsIntegration;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

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
        public void Initialise()
        {
            _fileManager = new FileManager(Presets.Logger);
            _unpacker = new Unpacker(_fileManager, Presets.Logger);
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
            Presets.CleanUp();
            _fileManager.DeleteDirectory(_rootDirectory);

            Settings.TempLocation = _defaultTempDirectory;
        }

        #endregion Internal Methods
        #region Tests: Unpack

        [TestMethod]
        public void Unpack_Bin_OutputFolderShouldContainFiles()
        {
            // arrange
            var sourceFile = _fileManager.GetFileInfo(Settings.WarThunderLocation, EFile.WarThunder.StatAndBalanceParameters);

            // act
            var outputDirectory = new DirectoryInfo(_unpacker.Unpack(sourceFile));

            // assert
            outputDirectory.GetFiles().Should().NotBeEmpty();
        }

        [TestMethod]
        public void Unpack_Blkx_OutputFolderShouldContainFiles()
        {
            // arrange
            var sourceFile = _fileManager.GetFileInfo(Settings.WarThunderLocation, EFile.WarThunder.WorldWarParameters);
            var binOutputDirectory = new DirectoryInfo(_unpacker.Unpack(sourceFile));
            var blkFile = binOutputDirectory.GetDirectories().First().GetDirectories().First().GetFiles(file => file.Extension.ToLower().Contains(FileExtension.Blk)).First();

            // act
            var blkxOutput = new FileInfo(_unpacker.Unpack(blkFile));

            // assert
            blkxOutput.Exists.Should().BeTrue();
            blkxOutput.Extension.ToLower().Should().Contain(FileExtension.Blkx);
        }

        [TestMethod]
        public void Unpack_Exe_ShouldThrow()
        {
            // arrange
            var sourceFile = _fileManager.GetFileInfo(Settings.WarThunderLocation, EFile.WarThunder.Launcher);

            // act
            Action unpack = () => new DirectoryInfo(_unpacker.Unpack(sourceFile));

            // assert
            unpack.Should().Throw<FileExtensionNotSupportedException>();
        }

        #endregion Tests: Unpack
    }
}