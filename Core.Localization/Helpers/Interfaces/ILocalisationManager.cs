namespace Core.Localization.Helpers.Interfaces
{
    /// <summary> Provide methods to work with localisation. </summary>
    public interface ILocalisationManager
    {
        /// <summary> Returns a localised string by its key. </summary>
        /// <param name="key"> The key of the localised string. </param>
        /// <param name="suppressWarnings"> Whether to suppress warnings if a key was not found. </param>
        /// <returns></returns>
        string GetLocalisedString(string key, bool suppressWarnings = false);

        string GetLocalisedString(object key, bool suppressWarnings = false);
    }
}