using Core.Helpers;
using Core.Helpers.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Core.Tests.Helpers
{
    /// <summary> See <see cref="FileReader"/>. </summary>
    [TestClass]
    public class FileReaderTests
    {
        private IFileManager _fileManager;
        private IFileReader _fileReader;
        private string _rootDirectory;

        #region Internal Methods

        [TestInitialize]
        public void Initialise()
        {
            _fileManager = new FileManager(Presets.Logger);
            _fileReader = new FileReader(Presets.Logger);
            _rootDirectory = $"{Directory.GetCurrentDirectory()}\\TestFiles";

            if (!Directory.Exists(_rootDirectory))
                Directory.CreateDirectory(_rootDirectory);
            else
                _fileManager.EmptyDirectory(_rootDirectory);
        }

        [TestCleanup]
        public void CleanUp()
        {
            Presets.Logger.LogInfo(CoreLogCategory.UnitTests, CoreLogMessage.CleanUpAfterUnitTestStartsHere);
            Presets.CleanUp();

            _fileManager.DeleteDirectory(_rootDirectory);
        }

        #endregion Internal Methods
        #region Tests: Read()

        [TestMethod]
        public void Read_NoFile_ShouldThrowFileNotFoundException()
        {
            // arrange
            var filePath = $"{_rootDirectory}\\iDoNotExist.sad";
            File.Exists(filePath).Should().BeFalse();

            // act
            Action readFile = () => _fileReader.Read(filePath);

            // assert
            readFile.Should().Throw<FileNotFoundException>();
        }

        [TestMethod]
        public void Read_FileExists_ShouldRead()
        {
            // arrange
            var expectedFileContents = "What is this?\nI don't even--";
            var filePath = $"{_rootDirectory}\\iDoNotExist.sad";
            File.Create(filePath).Close();

            using (var streamWriter = new StreamWriter(filePath))
                streamWriter.Write(expectedFileContents);

            // act
            var actualFileContents = _fileReader.Read(filePath);

            // assert
            actualFileContents.Should().Be(expectedFileContents);
        }

        #endregion Tests: Read()
    }
}