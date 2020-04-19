namespace Core.Enumerations
{
    /// <summary> Collection separators. </summary>
    public static class ESeparator
    {
        public static string CommaAndSpace = $"{ECharacter.Comma}{ECharacter.Space}";
        public static string VerticalBarAndSpace = $"{ECharacter.VerticalBar}{ECharacter.Space}";
        public static string Space = $"{ECharacter.Space}";
        public static string SpaceSlashSpace = $"{ECharacter.Space}{ECharacter.Slash}{ECharacter.Space}";
    }
}