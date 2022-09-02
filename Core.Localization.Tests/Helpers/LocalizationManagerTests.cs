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
        public void Initialise()
        {
            _fileManager = new FileManager(Presets.Logger);
            _fileReader = new FileReader(Presets.Logger);

            _localizationDirectory = $@"{Directory.GetCurrentDirectory()}\{Word.Localisation}";

            if (!Directory.Exists(_localizationDirectory))
                Directory.CreateDirectory(_localizationDirectory);
            else
                _fileManager.EmptyDirectory(_localizationDirectory);
        }

        [TestCleanup]
        public void CleanUp()
        {
            Presets.Logger.LogInfo(CoreLogCategory.UnitTests, CoreLogMessage.CleanUpAfterUnitTestStartsHere);
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
<{Word.Localisation}>
  <{Word.Line} {Word.Key.ToLower()}=""{key}"" {Word.Value.ToLower()}=""{value}""/>
</{Word.Localisation}>";

            var filePath = $@"{_localizationDirectory}\{fileName}{Character.Period}{FileExtension.Xml}";
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