namespace Core.Enumerations
{
    /// <summary> Operating system processes. </summary>
    public class Process
    {
        private const string _c = @"C:\";
        private const string _windows = @"Windows\";
        private const string _system32 = @"system32\";

        public const string CommandShell = _c + _windows + _system32 + "cmd" + CharacterString.Period + FileExtension.Exe;
    }
}
