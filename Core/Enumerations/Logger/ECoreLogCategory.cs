namespace Core.Enumerations.Logger
{
    /// <summary> Categories of events provided to a logger. </summary>
    public class ECoreLogCategory
    {
        public static readonly string Empty = string.Empty;
        public static readonly string FileManager = $"{EWord.File} {EWord.Manager}";
        public static readonly string FileReader = $"{EWord.File} {EWord.Reader}";
        public static readonly string IntegrationTests = $"{EWord.Integration} {EWord.Tests}";
        public static readonly string Logger = EWord.Logger;
        public static readonly string SettingsManager = $"{EWord.Settings} {EWord.Manager}";
        public static readonly string UnitTests = $"{EWord.Unit} {EWord.Tests}";
        public static readonly string Unpacker = EWord.Unpacker;
    }
}