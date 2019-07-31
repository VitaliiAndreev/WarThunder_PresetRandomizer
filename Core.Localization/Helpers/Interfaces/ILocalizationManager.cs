namespace Core.Localization.Helpers.Interfaces
{
    /// <summary> Provide methods to work with localization. </summary>
    public interface ILocalizationManager
    {
        /// <summary> Returns a localized string by its key. </summary>
        /// <param name="key"> The key of the localized string. </param>
        /// <returns></returns>
        string GetLocalizedString(string key);
    }
}