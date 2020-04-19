using Core.Enumerations;

namespace Core.UnpackingToolsIntegration.Enumerations
{
    public class EFile
    {
        private const string _vromfsBin = ECharacterString.Period + Vromfs + ECharacterString.Period + EFileExtension.Bin;

        internal const string Vromfs = "vromfs";
        internal const string warthunder = "warthunder";

        /// <summary>
        /// Files in the root directory of War Thunder.
        /// Public constants are being used to validate provided paths to War Thunder.
        /// </summary>
        public class WarThunder
        {
            private const string _exe = ECharacterString.Period + EFileExtension.Exe;
            private const string _yup = ECharacterString.Period + EFileExtension.Yup;

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
            private const string _blkx = ECharacterString.Period + EFileExtension.Blkx;

            public const string AdditionalVehicleData = "unittags" + _blkx;
            public const string GeneralVehicleData = "wpcost" + _blkx;
            public const string RankData = "rank" + _blkx;
            public const string ResearchTreeData = "shop" + _blkx;
        }
        public class LangVromfs
        {
            private const string _csv = ECharacterString.Period + EFileExtension.Csv;

            public const string Units = "units" + _csv;
        }
    }
}