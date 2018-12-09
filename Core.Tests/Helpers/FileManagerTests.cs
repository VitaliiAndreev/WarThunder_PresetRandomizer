using Core.DataBase.Tests;
using Core.Helpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace Core.Tests.Helpers
{
    /// <summary> See <see cref="FileManager"/>. </summary>
    [TestClass]
    public class FileManagerTests
    {
        #region Internal Methods

        private void DeleteDirectory(string path)
        {
            EmptyDirectory(path);
            Directory.Delete(path);
        }

        private void EmptyDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                foreach (var fileName in Directory.GetFiles(path))
                    File.Delete(fileName);
            }
        }

        #endregion Internal Methods
        #region DeleteFiles()

        [TestMethod]
        public void DeleteFiles_FolderDoesntExist_ShouldReturn()
        {
            // arrange
            var fileManager = new FileManager(Presets.Logger);
            var path = $"{Directory.GetCurrentDirectory()}\\TestFiles";

            Directory.Exists(path).Should().BeFalse();

            // act
            fileManager.DeleteFiles(path, "txt");
        }

        [TestMethod]
        public void DeleteFiles_FolderExists_FilesDontExist_ShouldReturn()
        {
            // arrange
            var fileManager = new FileManager(Presets.Logger);
            var path = $"{Directory.GetCurrentDirectory()}\\TestFiles";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            else
                EmptyDirectory(path);

            var fileNameCfg = $"{path}\\0.cfg";
            var fileNameLog = $"{path}\\1.log";

            File.Create(fileNameCfg).Close();
            File.Create(fileNameLog).Close();

            // act
            Directory.GetFiles(path).Count().Should().Be(2);
            fileManager.DeleteFiles(path, "txt");

            // assert
            Directory.GetFiles(path).Count().Should().Be(2);
        }

        [TestMethod]
        public void DeleteFiles_FolderExists_FilesExist_ShouldRemove()
        {
            // arrange
            var fileManager = new FileManager(Presets.Logger);
            var path = $"{Directory.GetCurrentDirectory()}\\TestFiles";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            else
                EmptyDirectory(path);

            var fileNameCfg = $"{path}\\0.cfg";
            var fileNameLog = $"{path}\\1.log";
            var fileNameTxtA = $"{path}\\2.txt";
            var fileNameTxtB = $"{path}\\3.txt";

            File.Create(fileNameCfg).Close();
            File.Create(fileNameLog).Close();
            File.Create(fileNameTxtA).Close();
            File.Create(fileNameTxtB).Close();

            // act
            fileManager.DeleteFiles(path, "txt");

            // assert
            var fileNamesLeft = Directory.GetFiles(path);
            fileNamesLeft.Count().Should().Be(2);
            fileNamesLeft.Should().Contain(fileName => fileName.Contains(".cfg"));
            fileNamesLeft.Should().Contain(fileName => fileName.Contains(".log"));

            // clean up
            DeleteDirectory(path);
        }

        #endregion DeleteFiles()
    }
}
