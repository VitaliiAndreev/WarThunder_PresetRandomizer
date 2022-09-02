namespace Core
{
    public class RegularExpressionPattern
    {
        public const string AtLeastOneNumber = "[0-9]{1,}";

        public static string VersionMajorMinor = $"{AtLeastOneNumber}.{AtLeastOneNumber}";
        public static string VersionMajorMinorBuild = $"{VersionMajorMinor}.{AtLeastOneNumber}";
        public static string VersionFull = $"{VersionMajorMinorBuild}.{AtLeastOneNumber}";
    }
}