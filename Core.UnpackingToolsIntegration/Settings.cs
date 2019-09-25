using Core.UnpackingToolsIntegration.Attributes;

namespace Core.WarThunderExtractionToolsIntegration
{
    /// <summary>
    /// Stores settings for the extraction tools (see https://github.com/klensy/wt-tools).
    /// If these settings are imported from XML files, XML node names must match property names here.
    /// </summary>
    public class Settings
    {
        #region Constants

        /// <summary> A separator between items in a collection string. </summary>
        public const string Separator = ", ";

        #endregion Constants
        #region Properties

        [RequiredSetting]
        public static string WarThunderLocation { get; set; }
        [RequiredSetting]
        public static string KlensysWarThunderToolsLocation { get; set; }

        public static string TempLocation { get; set; } = KlensysWarThunderToolsLocation + @"\_temp";

        #endregion Properties
    }
}