using Core.Helpers;
using Core.Helpers.Logger.Interfaces;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.WarThunderExtractionToolsIntegration;
using System.Collections.Generic;

namespace Core.UnpackingToolsIntegration.Helpers
{
    public class WarThunderSettingsManager : SettingsManager, IWarThunderSettingsManager
    {
        #region Fields

        /// <summary> An instance of a file manager. </summary>
        protected readonly IWarThunderFileManager _fileManager;

        #endregion Fields
        #region Constructors

        /// <summary> Creates a new file manager. </summary>
        /// <param name="settingsFileName"> The name of the settings file to attach to this manager. </param>
        /// <param name="fileManager"> The name of the settings file to attach to this manager. </param>
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
        /// <param name="settingName">Node of XML to read</param>
        /// <param name="newValue">Value to write to that node</param>
        /// <returns></returns>
        public override void Save(string settingName, string newValue)
        {
            base.Save(settingName, newValue);

            switch (settingName)
            {
                case nameof(Settings.KlensysWarThunderToolsLocation):
                    Settings.KlensysWarThunderToolsLocation = newValue;
                    break;
                case nameof(Settings.WarThunderLocation):
                    Settings.WarThunderLocation = newValue;
                    break;
            }
        }

        #endregion Methods: Writing
    }
}