namespace Core
{
    public static class RegularExpressionPattern
    {
        public const string AtLeastOneNumber = "[0-9]{1,}";

        public static readonly string VersionMajorMinor = $"{AtLeastOneNumber}.{AtLeastOneNumber}";
        public static readonly string VersionMajorMinorBuild = $"{VersionMajorMinor}.{AtLeastOneNumber}";
        public static readonly string VersionFull = $"{VersionMajorMinorBuild}.{AtLeastOneNumber}";
    }
}