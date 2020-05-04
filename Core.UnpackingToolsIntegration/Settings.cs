using Core.Enumerations;
using Core.UnpackingToolsIntegration.Attributes;
using System.IO;

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
        public static string Separator = ESeparator.CommaAndSpace;

        #endregion Constants
        #region Fields

        private static string _warThunderLocation;

        private static string _klensysWarThunderToolsLocation;

        #endregion Fields
        #region Properties

        [RequiredSetting]
        public static string WarThunderLocation
        {
            get => _warThunderLocation;
            set
            {
                _warThunderLocation = value;
                CacheLocation = GetCacheLocation();
            }
        }
        [RequiredSetting]
        public static string KlensysWarThunderToolsLocation
        {
            get => _klensysWarThunderToolsLocation;
            set
            {
                _klensysWarThunderToolsLocation = value;
                TempLocation = GetTempLocation();
            }
        }

        public static string TempLocation { get; set; }

        public static string CacheLocation { get; set; }

        #endregion Properties

        private static string GetTempLocation() => _klensysWarThunderToolsLocation is null ? null : Path.Combine(_klensysWarThunderToolsLocation, "_temp");

        private static string GetCacheLocation() => _warThunderLocation is null ? null : Path.Combine(_warThunderLocation, "cache");
    }
}