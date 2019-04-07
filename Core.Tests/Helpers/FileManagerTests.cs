using Core.DataBase.Tests;
using Core.Helpers;
using Core.Helpers.Interfaces;
using Core.Helpers.Logger.Enumerations;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core.Tests.Helpers
{
    /// <summary> See <see cref="FileManager"/>. </summary>
    [TestClass]
    public class FileManagerTests
    {
        private IFileManager _fileManager;
        private string _rootDirectory;

        #region Internal Methods

        [TestInitialize]
        public void CreateFileManager()
        {
            _fileManager = new FileManager(Presets.Logger);
            _rootDirectory = $"{Directory.GetCurrentDirectory()}\\TestFiles";
        }

        [TestCleanup]
        public void CleanUp()
        {
            Presets.Logger.LogInfo(ECoreLogCategory.UnitTests, ECoreLogMessage.CleanUpAfterUnitTestStartsHere);
            _fileManager.DeleteDirectory(_rootDirectory);
        }

        #endregion Internal Methods
        #region DeleteFiles()

        [TestMethod]
        public void DeleteFiles_All_FolderDoesntExist_ShouldNotThrow()
        {
            // arrange
            _fileManager.DeleteDirectory(_rootDirectory);
            Directory.Exists(_rootDirectory).Should().BeFalse();

            // act
            Action deleteFiles = () => _fileManager.DeleteFiles(_rootDirectory);

            // assert
            deleteFiles.Should().NotThrow();
        }

        [TestMethod]
        public void DeleteFiles_All_FolderExists_FolderIsEmpty_ShouldReturn()
        {
            // arrange
            if (!Directory.Exists(_rootDirectory))
                Directory.CreateDirectory(_rootDirectory);
            else
                _fileManager.EmptyDirectory(_rootDirectory);

            // act
            Directory.GetFiles(_rootDirectory).Should().BeEmpty();
            Action deleteFiles = () =>_fileManager.DeleteFiles(_rootDirectory);

            // assert
            deleteFiles.Should().NotThrow();
        }

        [TestMethod]
        public void DeleteFiles_Specific_FolderExists_FilesDontExist_ShouldReturn()
        {
            // arrange
            if (!Directory.Exists(_rootDirectory))
                Directory.CreateDirectory(_rootDirectory);
            else
                _fileManager.EmptyDirectory(_rootDirectory);

            var fileNameCfg = $"{_rootDirectory}\\0.cfg";
            var fileNameLog = $"{_rootDirectory}\\1.log";

            File.Create(fileNameCfg).Close();
            File.Create(fileNameLog).Close();

            // act
            Directory.GetFiles(_rootDirectory).Count().Should().Be(2);
            _fileManager.DeleteFiles(_rootDirectory, "txt");

            // assert
            Directory.GetFiles(_rootDirectory).Count().Should().Be(2);
        }

        [TestMethod]
        public void DeleteFiles_Specific_FolderExists_FilesExist_ShouldRemove()
        {
            // arrange
            if (!Directory.Exists(_rootDirectory))
                Directory.CreateDirectory(_rootDirectory);
            else
                _fileManager.EmptyDirectory(_rootDirectory);

            var fileNameCfg = $"{_rootDirectory}\\0.cfg";
            var fileNameLog = $"{_rootDirectory}\\1.log";
            var fileNameTxtA = $"{_rootDirectory}\\2.txt";
            var fileNameTxtB = $"{_rootDirectory}\\3.txt";

            File.Create(fileNameCfg).Close();
            File.Create(fileNameLog).Close();
            File.Create(fileNameTxtA).Close();
            File.Create(fileNameTxtB).Close();

            // act
            _fileManager.DeleteFiles(_rootDirectory, "txt");

            // assert
            var fileNamesLeft = Directory.GetFiles(_rootDirectory);
            fileNamesLeft.Count().Should().Be(2);
            fileNamesLeft.Should().Contain(fileName => fileName.Contains(".cfg"));
            fileNamesLeft.Should().Contain(fileName => fileName.Contains(".log"));
        }

        [TestMethod]
        public void DeleteFilesInSubfolders_Specific_FolderExists_FilesExist_ShouldRemove()
        {
            // arrange
            if (!Directory.Exists(_rootDirectory))
                Directory.CreateDirectory(_rootDirectory);
            else
                _fileManager.EmptyDirectory(_rootDirectory);

            var subfolderPath = $"{_rootDirectory}\\subfolder";
            Directory.CreateDirectory(subfolderPath);

            var fileNameCfg = $"{_rootDirectory}\\0.cfg";
            var fileNameLog = $"{_rootDirectory}\\1.log";
            var fileNameTxtA = $"{_rootDirectory}\\2.txt";
            var fileNameTxtB = $"{_rootDirectory}\\3.txt";
            var fileNameTxtC = $"{subfolderPath}\\4.txt";
            var fileNameDocA = $"{subfolderPath}\\5.doc";

            File.Create(fileNameCfg).Close();
            File.Create(fileNameLog).Close();
            File.Create(fileNameTxtA).Close();
            File.Create(fileNameTxtB).Close();
            File.Create(fileNameTxtC).Close();
            File.Create(fileNameDocA).Close();

            // act
            _fileManager.DeleteFiles(_rootDirectory, new List<string> { "txt" }, true);

            // assert
            var fileNamesLeftInRootFolder = Directory.GetFiles(_rootDirectory);
            var fileNamesLeftInSubfolder = Directory.GetFiles(subfolderPath);

            fileNamesLeftInRootFolder.Count().Should().Be(2);
            fileNamesLeftInRootFolder.Should().Contain(fileName => fileName.Contains(".cfg"));
            fileNamesLeftInRootFolder.Should().Contain(fileName => fileName.Contains(".log"));

            fileNamesLeftInSubfolder.Count().Should().Be(1);
            fileNamesLeftInSubfolder.Should().Contain(fileName => fileName.Contains(".doc"));
        }

        #endregion DeleteFiles()
        #region EmptyDirectory()

        [TestMethod]
        public void EmptyDirectory_ShouldRemoveFilesAndFolders()
        {
            // arrange
            if (!Directory.Exists(_rootDirectory))
                Directory.CreateDirectory(_rootDirectory);
            else
                _fileManager.EmptyDirectory(_rootDirectory);

            var subfolderPath = $"{_rootDirectory}\\subfolder";
            Directory.CreateDirectory(subfolderPath);

            var filePathA = $"{_rootDirectory}\\A.txt";
            var filePathB = $"{subfolderPath}\\B.txt";

            File.Create(filePathA).Close();
            File.Create(filePathB).Close();

            // act
            _fileManager.EmptyDirectory(_rootDirectory);

            // assert
            Directory.GetFiles(_rootDirectory).Should().BeEmpty();
            Directory.Exists(subfolderPath).Should().BeFalse();
        }

        #endregion EmptyDirectory()
        #region DeleteDirectory()

        [TestMethod]
        public void DeleteDirectory_ShouldRemoveFilesAndFolders()
        {
            // arrange
            if (!Directory.Exists(_rootDirectory))
                Directory.CreateDirectory(_rootDirectory);
            else
                _fileManager.EmptyDirectory(_rootDirectory);

            var subfolderPath = $"{_rootDirectory}\\subfolder";
            Directory.CreateDirectory(subfolderPath);

            var filePathA = $"{_rootDirectory}\\A.txt";
            var filePathB = $"{subfolderPath}\\B.txt";

            File.Create(filePathA).Close();
            File.Create(filePathB).Close();

            // act
            _fileManager.DeleteDirectory(_rootDirectory);

            // assert
            Directory.Exists(_rootDirectory).Should().BeFalse();
            Directory.Exists(subfolderPath).Should().BeFalse();
        }

        #endregion DeleteDirectory()
    }
}