using Core.Enumerations;
using Core.Enumerations.Logger;
using Core.Helpers;
using Core.Helpers.Interfaces;
using Core.Localization.Enumerations;
using Core.Localization.Exceptions;
using Core.Localization.Helpers;
using Core.Tests;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Core.Localization.Tests.Helpers
{
    /// <summary> See <see cref="LocalizationManager"/>. </summary>
    [TestClass]
    public class LocalizationManagerTests
    {
        #region Fields

        private IFileManager _fileManager;
        private IFileReader _fileReader;

        private string _localizationDirectory;

        #endregion Fields
        #region Internal Methods

        [TestInitialize]
        public void Initialize()
        {
            _fileManager = new FileManager(Presets.Logger);
            _fileReader = new FileReader(Presets.Logger);

            _localizationDirectory = $@"{Directory.GetCurrentDirectory()}\{EWord.Localization}";

            if (!Directory.Exists(_localizationDirectory))
                Directory.CreateDirectory(_localizationDirectory);
            else
                _fileManager.EmptyDirectory(_localizationDirectory);
        }

        [TestCleanup]
        public void CleanUp()
        {
            Presets.Logger.LogInfo(ECoreLogCategory.UnitTests, ECoreLogMessage.CleanUpAfterUnitTestStartsHere);
            Presets.CleanUp();

            _fileManager.DeleteDirectory(_localizationDirectory);
        }

        #endregion Internal Methods
        #region Tests: Constructor()

        [TestMethod]
        public void Constructor_LanguageNotRecognized_ShouldThrow()
        {
            // arrange, act
            Action createLocalizationManager = () => new LocalizationManager(_fileReader, "", Presets.Logger);

            // assert
            createLocalizationManager.Should().Throw<LanguageNotRecognizedException>();
        }

        [TestMethod]
        public void Constructor_LanguageRecognized_ShouldLoad()
        {
            // arrange
            var key = "SomethingSomething";
            var value = "Ooh, it actually has something! :D";
            var expectedFileContents = $@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<{EWord.Localization}>
  <{EWord.Line} {EWord.Key_L}=""{key}"" {EWord.Value_L}=""{value}""/>
</{EWord.Localization}>";

            var filePath = $@"{_localizationDirectory}\English.xml";
            File.Create(filePath).Close();

            using (var streamWriter = new StreamWriter(filePath))
                streamWriter.Write(expectedFileContents);

            // act
            var localizationManager = new LocalizationManager(_fileReader, ELocalization.English.ToString(), Presets.Logger);

            // assert
            localizationManager.GetLocalizedString(key).Should().Be(value);
        }

        #endregion Tests: Constructor()
    }
}