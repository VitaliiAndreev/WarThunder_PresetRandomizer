﻿using Client.Wpf.Helpers.Interfaces;
using Core.Helpers.Logger.Interfaces;
using Core.UnpackingToolsIntegration.Helpers;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using System.Collections.Generic;

namespace Client.Wpf.Helpers
{
    /// <summary> Handles work with settings files. </summary>
    public class WpfClientSettingsManager : WarThunderSettingsManager, IWpfClientSettingsManager
    {
        #region Constructors

        /// <summary> Creates a new settings manager. </summary>
        /// <param name="settingsFileName"> The name of the settings file to attach to this manager. </param>
        /// <param name="fileManager"> An instance of a file manager. </param>
        /// <param name="requiredSettingNames"> Names of required settings. </param>
        /// <param name="loggers"> Instances of loggers. </param>
        public WpfClientSettingsManager(IWarThunderFileManager fileManager, string settingsFileName, IEnumerable<string> requiredSettingNames, params IConfiguredLogger[] loggers)
            : base(fileManager, settingsFileName, requiredSettingNames, loggers)
        {
        }

        #endregion Constructors

        /// <summary> Sets and saves the <paramref name="newValue"/> of the setting with the specified name. </summary>
        /// <param name="settingName"> The name of the setting. </param>
        /// <param name="newValue"> The new value to set. </param>
        /// <returns></returns>
        public override void Save(string settingName, string newValue)
        {
            base.Save(settingName, newValue);

            Save(typeof(WpfSettings), settingName, newValue);
        }
    }
}