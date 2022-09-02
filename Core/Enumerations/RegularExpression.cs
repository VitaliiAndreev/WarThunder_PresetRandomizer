namespace Core.Enumerations
{
    /// <summary> Regular expression strings. </summary>
    public class RegularExpression
    {
        public const string AtLeastOneNumber = "[0-9]{1,}";
        public const string VersionMajorMinor = AtLeastOneNumber + CharacterString.Period + AtLeastOneNumber;
        public const string VersionMajorMinorBuild = VersionMajorMinor + CharacterString.Period + AtLeastOneNumber;
        public const string VersionFull = VersionMajorMinorBuild + CharacterString.Period + AtLeastOneNumber;
    }
}