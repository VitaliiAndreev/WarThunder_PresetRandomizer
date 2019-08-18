using Core.Enumerations;

namespace Core.Helpers.Interfaces
{
    /// <summary> Handles work with settings files. </summary>
    public interface ISettingsManager
    {
        #region Properties

        /// <summary> The status of the settings file after initialization. </summary>
        ESettingsFileStatus SettingsFileStatus { get; }

        #endregion Properties
        #region Methods

        /// <summary> Returns the setting with the specified name. </summary>
        /// <param name="settingName"> The name of the setting to get. </param>
        /// <returns></returns>
        string GetSetting(string settingName);

        /// <summary> Saves the <paramref name="newValue"/> of the setting with the specified name. </summary>
        /// <param name="settingName">Node of XML to read</param>
        /// <param name="newValue">Value to write to that node</param>
        /// <returns></returns>
        void Save(string settingName, string newValue);

        #endregion Methods
    }
}