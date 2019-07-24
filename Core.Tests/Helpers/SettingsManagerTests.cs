using Core.Enumerations.Logger;
using Core.Helpers;
using Core.Helpers.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
        public void Initialize()
        {
            _fileManager = new FileManager(Presets.Logger);
            _settingsFile = new FileInfo("Settings.xml");
        }

        [TestCleanup]
        public void CleanUp()
        {
            Presets.Logger.LogInfo(ECoreLogCategory.UnitTests, ECoreLogMessage.CleanUpAfterUnitTestStartsHere);
            Presets.CleanUp();

            _fileManager.DeleteFileSafely(_settingsFile);
        }

        #endregion Internal Methods

        #region Tests: SettingsManager()

        [TestMethod]
        public void SettingsFileNotFound_Throws()
        {
            // arrange
            Action createNewSettingsManager = () => new SettingsManager("Carramba!", Presets.Logger);

            // act, assert
            createNewSettingsManager.Should().Throw<FileNotFoundException>();
        }

        [TestMethod]
        public void SettingsFileFound()
        {
            // arrange
            _settingsFile.Create().Close();

            Action createNewSettingsManager = () => new SettingsManager(_settingsFile.Name, Presets.Logger);

            // act, assert
            createNewSettingsManager.Should().NotThrow();
        }

        #endregion Tests: SettingsManager()
        #region Tests: Load()

        [TestMethod]
        public void Load_NodeNotFound_Throws()
        {
            // arrange
            _settingsFile.Create().Close();

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

            var settingsManager = new SettingsManager(_settingsFile.Name, Presets.Logger);

            // act, assert
            Action loadSetting = () => settingsManager.Load("WhatIsThisIDontEven");
            loadSetting.Should().Throw<XmlException>();
        }

        [TestMethod]
        public void Load_SettingEmpty_ReturnsEmptyValue()
        {
            // arrange
            _settingsFile.Create().Close();

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

            var settingsManager = new SettingsManager(_settingsFile.Name, Presets.Logger);

            // act
            var settingValueFromSettings = settingsManager.Load(setting);

            // assert
            settingValueFromSettings.Should().Be(settingValue);
        }

        [TestMethod]
        public void Load_ReturnsValue()
        {
            // arrange
            _settingsFile.Create().Close();

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

            var settingsManager = new SettingsManager(_settingsFile.Name, Presets.Logger);

            // act
            var settingValueFromSettings1 = settingsManager.Load(setting1);
            var settingValueFromSettings2 = settingsManager.Load(setting2);

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
            _settingsFile.Create().Close();

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

            var settingsManager = new SettingsManager(_settingsFile.Name, Presets.Logger);

            // act, assert
            Action saveSetting = () => settingsManager.Save("WhatIsThisIDontEven", "Whatever");
            saveSetting.Should().Throw<XmlException>();
        }

        [TestMethod]
        public void Save()
        {
            // arrange
            _settingsFile.Create().Close();

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

            var settingsManager = new SettingsManager(_settingsFile.Name, Presets.Logger);

            settingsManager.Load(setting1).Should().BeEmpty();
            settingsManager.Load(setting2).Should().BeEmpty();

            var settingValue1 = @"\\Carramba!\";
            var settingValue2 = @"\\Bananza!\";

            // act
            settingsManager.Save(setting1, settingValue1);
            settingsManager.Save(setting2, settingValue2);

            // assert
            var settingValueFromSettings1 = settingsManager.Load(setting1);
            var settingValueFromSettings2 = settingsManager.Load(setting2);
            settingValueFromSettings1.Should().Be(settingValue1);
            settingValueFromSettings2.Should().Be(settingValue2);
        }

        #endregion Tests: Save()
    }
}