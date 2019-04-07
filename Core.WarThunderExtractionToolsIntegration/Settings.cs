namespace Core.WarThunderExtractionToolsIntegration
{
    /// <summary> Stores settings for the extraction tools (see https://github.com/klensy/wt-tools). </summary>
    public class Settings
    {
        public static string WarThunderLocation { get; set; } = @"D:\Games\_Steam\steamapps\common\War Thunder";
        public static string ToolsLocation { get; set; } = @"D:\Software\War Thunder Tools";
        public static string TempLocation { get; set; } = ToolsLocation + @"\temp";
    }
}
