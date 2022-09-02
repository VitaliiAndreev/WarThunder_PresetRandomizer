using Core.Enumerations;

namespace Core.UnpackingToolsIntegration.Enumerations
{
    public class EFile
    {
        private const string _vromfsBin = CharacterString.Period + Vromfs + CharacterString.Period + FileExtension.Bin;

        internal const string Vromfs = "vromfs";
        internal const string warthunder = "warthunder";

        /// <summary>
        /// Files in the root directory of War Thunder.
        /// Public constants are being used to validate provided paths to War Thunder.
        /// </summary>
        public class WarThunder
        {
            private const string _exe = CharacterString.Period + FileExtension.Exe;
            private const string _yup = CharacterString.Period + FileExtension.Yup;

            public const string CoreParameters = "aces" + _vromfsBin;
            public const string GuiParameters = "gui" + _vromfsBin;
            public const string LocalizationParameters = "lang" + _vromfsBin;
            public const string MissionParameters = "mis" + _vromfsBin;
            public const string StatAndBalanceParameters = "char" + _vromfsBin;
            public const string WebUiParameters = "webUi" + _vromfsBin;
            public const string WorldWarParameters = "wwdata" + _vromfsBin;

            public const string CurrentIntallData = warthunder + _yup;
            public const string PreviousVersionInstallData = warthunder + "_old" + _yup;

            public const string Launcher = "launcher" + _exe;
        }
        public class WarThunderUi
        {
            public const string Icons = "atlases" + _vromfsBin;
            public const string Textures = "tex" + _vromfsBin;
        }
        public class CharVromfs
        {
            private const string _blkx = CharacterString.Period + FileExtension.Blkx;

            public const string AdditionalVehicleData = "unittags" + _blkx;
            public const string GeneralVehicleData = "wpcost" + _blkx;
            public const string RankData = "rank" + _blkx;
            public const string ResearchTreeData = "shop" + _blkx;
        }
        public class LangVromfs
        {
            private const string _csv = CharacterString.Period + FileExtension.Csv;

            public const string Units = "units" + _csv;
        }
    }
}