using Core.Localization.Enumerations;

namespace Core.WarThunderExtractionToolsIntegration
{
    /// <summary>
    /// Stores currently loaded settings for the application.
    /// If these settings are imported from XML files, XML node names must match property names here.
    /// </summary>
    public class WpfSettings : Settings
    {
        public static string Localization { get; set; } = ELanguage.EnglishUsa.ToString();
    }
}