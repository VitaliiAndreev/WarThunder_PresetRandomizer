using Core.Localization.Enumerations;
using Core.UnpackingToolsIntegration.Attributes;

namespace Core.WarThunderExtractionToolsIntegration
{
    /// <summary>
    /// Stores currently loaded settings for the application.
    /// If these settings are imported from XML files, XML node names must match property names here.
    /// </summary>
    public class WpfSettings : Settings
    {
        [RequiredSetting]
        public static string Localization { get; set; } = ELanguage.EnglishUsa.ToString();
    }
}