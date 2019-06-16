namespace Core.Enumerations
{
    /// <summary> Regular expression strings. </summary>
    public class ERegularExpression
    {
        public const string AtLeastOneNumber = "[0-9]{1,}";
        public const string VersionMajorMinor = AtLeastOneNumber + ECharacterString.Period + AtLeastOneNumber;
        public const string VersionMajorMinorBuild = VersionMajorMinor + ECharacterString.Period + AtLeastOneNumber;
        public const string VersionFull = VersionMajorMinorBuild + ECharacterString.Period + AtLeastOneNumber;
    }
}