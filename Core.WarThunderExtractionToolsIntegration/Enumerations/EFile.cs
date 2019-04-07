using Core.Enumerations;

namespace Core.WarThunderUnpackingToolsIntegration.Enumerations
{
    public class EFile
    {
        internal const string Vromfs = "vromfs";
        internal const string warthunder = "warthunder";

        #region Root Folder

        private const string _vromfsBin = ECharacterString.Period + Vromfs + ECharacterString.Period + EFileExtension.Bin;
        private const string _yup = ECharacterString.Period + EFileExtension.Yup;

        public const string CoreParameters = "aces" + _vromfsBin;
        public const string StatAndBalanceParameters = "char" + _vromfsBin;
        public const string GuiParameters = "gui" + _vromfsBin;
        public const string LocalizationParameters = "lang" + _vromfsBin;
        public const string MissionParameters = "mis" + _vromfsBin;
        public const string WebUiParameters = "webgUi" + _vromfsBin;

        public const string CurrentIntallData = warthunder + _yup;
        public const string PreviousVersionInstallData = warthunder + "_old" + _yup;

        #endregion Root Folder
    }
}
