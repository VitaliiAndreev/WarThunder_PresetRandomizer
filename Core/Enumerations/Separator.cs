namespace Core
{
    /// <summary> Collection separators. </summary>
    public static class Separator
    {
        public static string CommaAndSpace = $"{Character.Comma}{Character.Space}";
        public static string VerticalBarAndSpace = $"{Character.VerticalBar}{Character.Space}";
        public static string Space = $"{Character.Space}";
        public static string SpaceMinusSpace = $"{Character.Space}{Character.Minus}{Character.Space}";
        public static string SpaceSlashSpace = $"{Character.Space}{Character.Slash}{Character.Space}";
    }
}