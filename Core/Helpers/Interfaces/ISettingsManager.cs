namespace Core.Helpers.Interfaces
{
    public interface ISettingsManager
    {
        /// <summary> Loads the setting with the specified name. </summary>
        /// <param name="settingName"> The name of the setting to read. </param>
        /// <returns></returns>
        string Load(string settingName);

        /// <summary> Saves the <paramref name="newValue"/> of the setting with the specified name. </summary>
        /// <param name="settingName">Node of XML to read</param>
        /// <param name="newValue">Value to write to that node</param>
        /// <returns></returns>
        void Save(string settingName, string newValue);
    }
}