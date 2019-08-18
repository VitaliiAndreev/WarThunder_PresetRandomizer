namespace Core.Enumerations.Logger
{
    /// <summary> Categories of events provided to a logger. </summary>
    public class ECoreLogCategory
    {
        public static string Empty = string.Empty;
        public static string FileManager = $"{EWord.File} {EWord.Manager}";
        public static string FileReader = $"{EWord.File} {EWord.Reader}";
        public static string IntegrationTests = $"{EWord.Integration} {EWord.Tests}";
        public static string Logger = EWord.Logger;
        public static string Parser = EWord.Parser;
        public static string SettingsManager = $"{EWord.Settings} {EWord.Manager}";
        public static string UnitTests = $"{EWord.Unit} {EWord.Tests}";
        public static string Unpacker = EWord.Unpacker;
    }
}