using Client.Wpf.Helpers.Interfaces;
using Core.Helpers.Logger.Interfaces;
using Core.UnpackingToolsIntegration.Helpers;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.WarThunderExtractionToolsIntegration;
using System.Collections.Generic;

namespace Client.Wpf.Helpers
{
    public class WpfClientSettingsManager : WarThunderSettingsManager, IWpfClientSettingsManager
    {
        #region Constructors

        /// <summary> Creates a new file manager. </summary>
        /// <param name="settingsFileName"> The name of the settings file to attach to this manager. </param>
        /// <param name="fileManager"> The name of the settings file to attach to this manager. </param>
        /// <param name="requiredSettingNames"> Names of required settings. </param>
        /// <param name="loggers"> Instances of loggers. </param>
        public WpfClientSettingsManager(IWarThunderFileManager fileManager, string settingsFileName, IEnumerable<string> requiredSettingNames, params IConfiguredLogger[] loggers)
            : base(fileManager, settingsFileName, requiredSettingNames, loggers)
        {
        }

        #endregion Constructors

        /// <summary> Saves the <paramref name="newValue"/> of the setting with the specified name. </summary>
        /// <param name="settingName">Node of XML to read</param>
        /// <param name="newValue">Value to write to that node</param>
        /// <returns></returns>
        public override void Save(string settingName, string newValue)
        {
            base.Save(settingName, newValue);

            switch (settingName)
            {
                case nameof(WpfSettings.Localization):
                    WpfSettings.Localization = newValue;
                    break;
            }
        }
    }
}