using Core.Exceptions;
using Core.Helpers;
using Core.Helpers.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Core.Tests.Helpers
{
    /// <summary> See <see cref="SettingsManager"/>. </summary>
    [TestClass]
    public class SettingsManagerTests
    {
        #region Fields

        private IFileManager _fileManager;
        private FileInfo _settingsFile;

        #endregion Fields
        #region Internal Methods

        [TestInitialize]
        public void Initialise()
        {
            _fileManager = new FileManager(Presets.Logger);

            _settingsFile = new FileInfo("Settings.xml");
            _settingsFile.Create().Close();
        }

        [TestCleanup]
        public void CleanUp()
        {
            Presets.Logger.LogInfo(CoreLogCategory.UnitTests, CoreLogMessage.CleanUpAfterUnitTestStartsHere);
            Presets.CleanUp();

            _fileManager.DeleteFileSafely(_settingsFile);
        }

        #endregion Internal Methods

        #region Tests: SettingsManager()

        [TestMethod]
        public void SettingsFileNotFound_GeneratesFile()
        {
            // arrange
            var settingsFile = new FileInfo($"Carramba{Character.Period}{FileExtension.Xml}");

            if (settingsFile.Exists)
                settingsFile.Delete();

            var settingName = "Bananza";
            var settingsManager = new SettingsManager(_fileManager, settingsFile.Name, new List<string> { settingName }, Presets.Logger);
            Action getSetting = () => settingsManager.GetSetting(settingName);

            // act, assert
            getSetting.Should().NotThrow();

            // clean-up
            settingsFile.Delete();
        }

        [TestMethod]
        public void SettingsFileFound_RequiredSettingMissing_GeneratesFile()
        {
            // arrange
            using (var textWriter = new StreamWriter(_settingsFile.FullName))
            {
                var fileContents = $@"<?xml version=""1.0"" encoding=""utf-8"" ?>
  <Settings>
  </Settings>";
                textWriter.Write(fileContents);
            }

            var settingName = "Bananza";
            Action createSettingsManager = () => new SettingsManager(_fileManager, _settingsFile.Name, new List<string> { settingName }, Presets.Logger);

            // act, assert
            createSettingsManager.Should().Throw<SettingsFileRegeneratedException>();
        }

        #endregion Tests: SettingsManager()
        #region Tests: Load()

        [TestMethod]
        public void Load_NodeNotFound_Throws()
        {
            // arrange
            var setting = "Setting";
            var settingValue = string.Empty;

            using (var textWriter = new StreamWriter(_settingsFile.FullName))
            {
                var fileContents = $@"<?xml version=""1.0"" encoding=""utf-8"" ?>
  <Settings>
    <{setting}>{settingValue}</{setting}>
  </Settings>";
                textWriter.Write(fileContents);
            }

            var settingsManager = new SettingsManager(_fileManager, _settingsFile.Name, new List<string> { setting }, Presets.Logger);

            // act, assert
            Action loadSetting = () => settingsManager.GetSetting("WhatIsThisIDontEven");
            loadSetting.Should().Throw<KeyNotFoundException>();
        }

        [TestMethod]
        public void Load_SettingEmpty_ReturnsEmptyValue()
        {
            // arrange
            var setting = "Setting";
            var settingValue = string.Empty;

            using (var textWriter = new StreamWriter(_settingsFile.FullName))
            {
                var fileContents = $@"<?xml version=""1.0"" encoding=""utf-8"" ?>
  <Settings>
    <{setting}>{settingValue}</{setting}>
  </Settings>";
                textWriter.Write(fileContents);
            }

            var settingsManager = new SettingsManager(_fileManager, _settingsFile.Name, new List<string> { setting }, Presets.Logger);

            // act
            var settingValueFromSettings = settingsManager.GetSetting(setting);

            // assert
            settingValueFromSettings.Should().Be(settingValue);
        }

        [TestMethod]
        public void Load_ReturnsValue()
        {
            // arrange
            var setting1 = "Setting1";
            var setting2 = "Setting2";
            var settingValue1 = @"\\Carramba!\";
            var settingValue2 = @"\\Bananza!\";

            using (var textWriter = new StreamWriter(_settingsFile.FullName))
            {
                var fileContents = $@"<?xml version=""1.0"" encoding=""utf-8"" ?>
  <Settings>
    <{setting1}>{settingValue1}</{setting1}>
    <{setting2}>{settingValue2}</{setting2}>
  </Settings>";
                textWriter.Write(fileContents);
            }

            var settingsManager = new SettingsManager(_fileManager, _settingsFile.Name, new List<string> { setting1, setting2 }, Presets.Logger);

            // act
            var settingValueFromSettings1 = settingsManager.GetSetting(setting1);
            var settingValueFromSettings2 = settingsManager.GetSetting(setting2);

            // assert
            settingValueFromSettings1.Should().Be(settingValue1);
            settingValueFromSettings2.Should().Be(settingValue2);
        }

        #endregion Tests: Load()
        #region Tests: Save()

        [TestMethod]
        public void Save_NodeNotFound_Throws()
        {
            // arrange
            var setting = "Setting";
            var settingValue = string.Empty;

            using (var textWriter = new StreamWriter(_settingsFile.FullName))
            {
                var fileContents = $@"<?xml version=""1.0"" encoding=""utf-8"" ?>
  <Settings>
    <{setting}>{settingValue}</{setting}>
  </Settings>";
                textWriter.Write(fileContents);
            }

            var settingsManager = new SettingsManager(_fileManager, _settingsFile.Name, new List<string> { setting }, Presets.Logger);

            // act, assert
            Action saveSetting = () => settingsManager.Save("WhatIsThisIDontEven", "Whatever");
            saveSetting.Should().Throw<XmlException>();
        }

        [TestMethod]
        public void Save()
        {
            // arrange
            var setting1 = "Setting1";
            var setting2 = "Setting2";

            using (var textWriter = new StreamWriter(_settingsFile.FullName))
            {
                var fileContents = $@"<?xml version=""1.0"" encoding=""utf-8"" ?>
  <Settings>
    <{setting1}></{setting1}>
    <{setting2}></{setting2}>
  </Settings>";
                textWriter.Write(fileContents);
            }

            var settingsManager = new SettingsManager(_fileManager, _settingsFile.Name, new List<string> { setting1, setting2 }, Presets.Logger);

            settingsManager.GetSetting(setting1).Should().BeEmpty();
            settingsManager.GetSetting(setting2).Should().BeEmpty();

            var settingValue1 = @"\\Carramba!\";
            var settingValue2 = @"\\Bananza!\";

            // act
            settingsManager.Save(setting1, settingValue1);
            settingsManager.Save(setting2, settingValue2);

            // assert
            var settingValueFromSettings1 = settingsManager.GetSetting(setting1);
            var settingValueFromSettings2 = settingsManager.GetSetting(setting2);
            settingValueFromSettings1.Should().Be(settingValue1);
            settingValueFromSettings2.Should().Be(settingValue2);
        }

        #endregion Tests: Save()
    }
}