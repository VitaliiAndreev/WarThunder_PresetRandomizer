using Core.UnpackingToolsIntegration.Attributes;

namespace Core.WarThunderExtractionToolsIntegration
{
    /// <summary>
    /// Stores settings for the extraction tools (see https://github.com/klensy/wt-tools).
    /// If these settings are imported from XML files, XML node names must match property names here.
    /// </summary>
    public class Settings
    {
        [RequiredSetting]
        public static string WarThunderLocation { get; set; } = @"D:\Games\_Steam\steamapps\common\War Thunder";
        [RequiredSetting]
        public static string KlensysWarThunderToolsLocation { get; set; } = @"D:\Software\War Thunder Tools";
        public static string TempLocation { get; set; } = KlensysWarThunderToolsLocation + @"\_temp";
    }
}