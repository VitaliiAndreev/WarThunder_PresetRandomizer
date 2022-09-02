namespace Core.UnpackingToolsIntegration.Enumerations
{
    public static class EFile
    {
        private const string warthunder = "warthunder";
        internal const string Vromfs = "vromfs";

        private static readonly string vromfsBin = $".{Vromfs}.{FileExtension.Bin}";

        /// <summary> Files in the root directory of War Thunder. </summary>
        public static class WarThunder
        {
            public const string Launcher = "launcher.exe";

            private static readonly string yup = $".{FileExtension.Yup}";

            public static readonly string CoreParameters = $"aces{vromfsBin}";
            public static readonly string GuiParameters = $"gui{vromfsBin}";
            public static readonly string LocalizationParameters = $"lang{vromfsBin}";
            public static readonly string MissionParameters = $"mis{vromfsBin}";
            public static readonly string StatAndBalanceParameters = $"char{vromfsBin}";
            public static readonly string WebUiParameters = $"webUi{vromfsBin}";
            public static readonly string WorldWarParameters = $"wwdata{vromfsBin}";

            public static readonly string CurrentIntallData = $"{warthunder}{yup}";
            public static readonly string PreviousVersionInstallData = $"{warthunder}_old{yup}";
        }

        public static class WarThunderUi
        {
            public static readonly string Icons = $"atlases{vromfsBin}";
            public static readonly string Textures = $"tex{vromfsBin}";
        }

        public static class CharVromfs
        {
            private static readonly string blkx = $".{FileExtension.Blkx}";

            public static readonly string AdditionalVehicleData = $"unittags{blkx}";
            public static readonly string GeneralVehicleData = $"wpcost{blkx}";
            public static readonly string RankData = $"rank{blkx}";
            public static readonly string ResearchTreeData = $"shop{blkx}";
        }

        public static class LangVromfs
        {
            public static readonly string Units = $"units.{FileExtension.Csv}";
        }
    }
}