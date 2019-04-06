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

        #region Internal Methods

        [TestInitialize]
        public void CreateFileManager()
        {
            _fileManager = new FileManager(Presets.Logger);
        }

        #endregion Internal Methods
        #region DeleteFiles()

        [TestMethod]
        public void DeleteFiles_All_FolderDoesntExist_ShouldNotThrow()
        {
            // arrange
            var path = $"{Directory.GetCurrentDirectory()}\\TestFiles";

            _fileManager.DeleteDirectory(path);
            Directory.Exists(path).Should().BeFalse();

            // act
            Action deleteFiles = () => _fileManager.DeleteFiles(path);

            // assert
            deleteFiles.Should().NotThrow();
        }

        [TestMethod]
        public void DeleteFiles_All_FolderExists_FolderIsEmpty_ShouldReturn()
        {
            // arrange
            var path = $"{Directory.GetCurrentDirectory()}\\TestFiles";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            else
                _fileManager.EmptyDirectory(path);

            // act
            Directory.GetFiles(path).Should().BeEmpty();
            Action deleteFiles = () =>_fileManager.DeleteFiles(path);

            // assert
            deleteFiles.Should().NotThrow();

            // clean up
            Presets.Logger.LogInfo(ECoreLogCategory.UnitTests, ECoreLogMessage.CleanUpAfterUnitTestStartsHere);
            _fileManager.DeleteDirectory(path);
        }

        [TestMethod]
        public void DeleteFiles_Specific_FolderExists_FilesDontExist_ShouldReturn()
        {
            // arrange
            var path = $"{Directory.GetCurrentDirectory()}\\TestFiles";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            else
                _fileManager.EmptyDirectory(path);

            var fileNameCfg = $"{path}\\0.cfg";
            var fileNameLog = $"{path}\\1.log";

            File.Create(fileNameCfg).Close();
            File.Create(fileNameLog).Close();

            // act
            Directory.GetFiles(path).Count().Should().Be(2);
            _fileManager.DeleteFiles(path, "txt");

            // assert
            Directory.GetFiles(path).Count().Should().Be(2);

            // clean up
            Presets.Logger.LogInfo(ECoreLogCategory.UnitTests, ECoreLogMessage.CleanUpAfterUnitTestStartsHere);
            _fileManager.DeleteDirectory(path);
        }

        [TestMethod]
        public void DeleteFiles_Specific_FolderExists_FilesExist_ShouldRemove()
        {
            // arrange
            var path = $"{Directory.GetCurrentDirectory()}\\TestFiles";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            else
                _fileManager.EmptyDirectory(path);

            var fileNameCfg = $"{path}\\0.cfg";
            var fileNameLog = $"{path}\\1.log";
            var fileNameTxtA = $"{path}\\2.txt";
            var fileNameTxtB = $"{path}\\3.txt";

            File.Create(fileNameCfg).Close();
            File.Create(fileNameLog).Close();
            File.Create(fileNameTxtA).Close();
            File.Create(fileNameTxtB).Close();

            // act
            _fileManager.DeleteFiles(path, "txt");

            // assert
            var fileNamesLeft = Directory.GetFiles(path);
            fileNamesLeft.Count().Should().Be(2);
            fileNamesLeft.Should().Contain(fileName => fileName.Contains(".cfg"));
            fileNamesLeft.Should().Contain(fileName => fileName.Contains(".log"));

            // clean up
            Presets.Logger.LogInfo(ECoreLogCategory.UnitTests, ECoreLogMessage.CleanUpAfterUnitTestStartsHere);
            _fileManager.DeleteDirectory(path);
        }

        [TestMethod]
        public void DeleteFilesInSubfolders_Specific_FolderExists_FilesExist_ShouldRemove()
        {
            // arrange
            var path = $"{Directory.GetCurrentDirectory()}\\TestFiles";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            else
                _fileManager.EmptyDirectory(path);

            var subfolderPath = $"{path}\\subfolder";
            Directory.CreateDirectory(subfolderPath);

            var fileNameCfg = $"{path}\\0.cfg";
            var fileNameLog = $"{path}\\1.log";
            var fileNameTxtA = $"{path}\\2.txt";
            var fileNameTxtB = $"{path}\\3.txt";
            var fileNameTxtC = $"{subfolderPath}\\4.txt";
            var fileNameDocA = $"{subfolderPath}\\5.doc";

            File.Create(fileNameCfg).Close();
            File.Create(fileNameLog).Close();
            File.Create(fileNameTxtA).Close();
            File.Create(fileNameTxtB).Close();
            File.Create(fileNameTxtC).Close();
            File.Create(fileNameDocA).Close();

            // act
            _fileManager.DeleteFiles(path, new List<string> { "txt" }, true);

            // assert
            var fileNamesLeftInRootFolder = Directory.GetFiles(path);
            var fileNamesLeftInSubfolder = Directory.GetFiles(subfolderPath);

            fileNamesLeftInRootFolder.Count().Should().Be(2);
            fileNamesLeftInRootFolder.Should().Contain(fileName => fileName.Contains(".cfg"));
            fileNamesLeftInRootFolder.Should().Contain(fileName => fileName.Contains(".log"));

            fileNamesLeftInSubfolder.Count().Should().Be(1);
            fileNamesLeftInSubfolder.Should().Contain(fileName => fileName.Contains(".doc"));

            // clean up
            Presets.Logger.LogInfo(ECoreLogCategory.UnitTests, ECoreLogMessage.CleanUpAfterUnitTestStartsHere);
            _fileManager.DeleteDirectory(path);
        }

        #endregion DeleteFiles()
        #region EmptyDirectory()

        [TestMethod]
        public void EmptyDirectory_ShouldRemoveFilesAndFolders()
        {
            // arrange
            var path = $"{Directory.GetCurrentDirectory()}\\TestFiles";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            else
                _fileManager.EmptyDirectory(path);

            var subfolderPath = $"{path}\\subfolder";
            Directory.CreateDirectory(subfolderPath);

            var filePathA = $"{path}\\A.txt";
            var filePathB = $"{subfolderPath}\\B.txt";

            File.Create(filePathA).Close();
            File.Create(filePathB).Close();

            // act
            _fileManager.EmptyDirectory(path);

            // assert
            Directory.GetFiles(path).Should().BeEmpty();
            Directory.Exists(subfolderPath).Should().BeFalse();

            // clean up
            Presets.Logger.LogInfo(ECoreLogCategory.UnitTests, ECoreLogMessage.CleanUpAfterUnitTestStartsHere);
            _fileManager.DeleteDirectory(path);
        }

        #endregion EmptyDirectory()
        #region DeleteDirectory()

        [TestMethod]
        public void DeleteDirectory_ShouldRemoveFilesAndFolders()
        {
            // arrange
            var path = $"{Directory.GetCurrentDirectory()}\\TestFiles";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            else
                _fileManager.EmptyDirectory(path);

            var subfolderPath = $"{path}\\subfolder";
            Directory.CreateDirectory(subfolderPath);

            var filePathA = $"{path}\\A.txt";
            var filePathB = $"{subfolderPath}\\B.txt";

            File.Create(filePathA).Close();
            File.Create(filePathB).Close();

            // act
            _fileManager.DeleteDirectory(path);

            // assert
            Directory.Exists(path).Should().BeFalse();
            Directory.Exists(subfolderPath).Should().BeFalse();

            // clean up
            Presets.Logger.LogInfo(ECoreLogCategory.UnitTests, ECoreLogMessage.CleanUpAfterUnitTestStartsHere);
            _fileManager.DeleteDirectory(path);
        }

        #endregion DeleteDirectory()
    }
}