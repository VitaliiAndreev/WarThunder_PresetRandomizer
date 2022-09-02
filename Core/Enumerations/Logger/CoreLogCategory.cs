namespace Core
{
    /// <summary> Categories of events provided to a logger. </summary>
    public class CoreLogCategory
    {
        public static readonly string Empty = string.Empty;
        public static readonly string FileManager = $"{Word.File} {Word.Manager}";
        public static readonly string FileReader = $"{Word.File} {Word.Reader}";
        public static readonly string IntegrationTests = $"{Word.Integration} {Word.Tests}";
        public static readonly string Logger = Word.Logger;
        public static readonly string SettingsManager = $"{Word.Settings} {Word.Manager}";
        public static readonly string UnitTests = $"{Word.Unit} {Word.Tests}";
        public static readonly string Unpacker = Word.Unpacker;
    }
}