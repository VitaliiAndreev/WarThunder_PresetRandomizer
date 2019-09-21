using Core.Helpers;
using Core.Helpers.Logger.Interfaces;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.WarThunderExtractionToolsIntegration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.UnpackingToolsIntegration.Helpers
{
    /// <summary> Handles work with settings files. </summary>
    public class WarThunderSettingsManager : SettingsManager, IWarThunderSettingsManager
    {
        #region Fields

        /// <summary> An instance of a file manager. </summary>
        new protected readonly IWarThunderFileManager _fileManager;

        #endregion Fields
        #region Constructors

        /// <summary> Creates a new settings manager. </summary>
        /// <param name="settingsFileName"> The name of the settings file to attach to this manager. </param>
        /// <param name="fileManager"> An instance of a file manager. </param>
        /// <param name="requiredSettingNames"> Names of required settings. </param>
        /// <param name="loggers"> Instances of loggers. </param>
        public WarThunderSettingsManager(IWarThunderFileManager fileManager, string settingsFileName, IEnumerable<string> requiredSettingNames, params IConfiguredLogger[] loggers)
            : base(fileManager, settingsFileName, requiredSettingNames, loggers)
        {
            _fileManager = fileManager;
        }

        #endregion Constructors
        #region Methods: Validation

        /// <summary> Checks whether the currently loaded location of Klensy's War Thunder Tools is valid. </summary>
        /// <returns></returns>
        public bool KlensysWarThunderToolLocationIsValid() => _fileManager.KlensysWarThunderToolLocationIsValid(Settings.KlensysWarThunderToolsLocation);

        /// <summary> Checks whether the currently loaded location of War Thunder is valid. </summary>
        /// <returns></returns>
        public bool WarThunderLocationIsValid() => _fileManager.WarThunderLocationIsValid(Settings.WarThunderLocation);

        #endregion Methods: Validation
        #region Methods: Writing

        /// <summary> Saves the <paramref name="newValue"/> of the setting with the specified name. </summary>
        /// <param name="settingsClass">Node of XML to read</param>
        /// <param name="settingName">Node of XML to read</param>
        /// <param name="newValue">Value to write to that node</param>
        /// <returns></returns>
        protected void Save(Type settingsClass, string settingName, string newValue)
        {
            var settingProperties = settingsClass.GetProperties(BindingFlags.Static | BindingFlags.Public);
            var settingProperty = settingProperties.FirstOrDefault(property => property.Name == settingName);

            if (settingProperty is PropertyInfo)
                settingProperty.SetValue(null, newValue);
        }

        /// <summary> Saves the <paramref name="newValue"/> of the setting with the specified name. </summary>
        /// <param name="settingName">Node of XML to read</param>
        /// <param name="newValue">Value to write to that node</param>
        /// <returns></returns>
        public override void Save(string settingName, string newValue)
        {
            base.Save(settingName, newValue);

            Save(typeof(Settings), settingName, newValue);
        }

        #endregion Methods: Writing
    }
}