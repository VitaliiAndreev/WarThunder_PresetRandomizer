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
    /// <summary> See <see cref="LocalisationManager"/>. </summary>
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

            _localizationDirectory = $@"{Directory.GetCurrentDirectory()}\{EWord.Localisation}";

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
            Action createLocalizationManager = () => new LocalisationManager(_fileReader, "", Presets.Logger);

            // assert
            createLocalizationManager.Should().Throw<FileNotFoundException>();
        }

        [TestMethod]
        public void Constructor_LanguageRecognized_ShouldLoad()
        {
            // arrange
            var fileName = "Whatsitsface";
            var key = "SomethingSomething";
            var value = "Ooh, it actually has something! :D";
            var expectedFileContents = $@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<{EWord.Localisation}>
  <{EWord.Line} {EWord.Key.ToLower()}=""{key}"" {EWord.Value.ToLower()}=""{value}""/>
</{EWord.Localisation}>";

            var filePath = $@"{_localizationDirectory}\{fileName}{ECharacter.Period}{EFileExtension.Xml}";
            File.Create(filePath).Close();

            using (var streamWriter = new StreamWriter(filePath))
                streamWriter.Write(expectedFileContents);

            // act
            var localizationManager = new LocalisationManager(_fileReader, fileName, Presets.Logger);

            // assert
            localizationManager.GetLocalisedString(key).Should().Be(value);
        }

        #endregion Tests: Constructor()
    }
}