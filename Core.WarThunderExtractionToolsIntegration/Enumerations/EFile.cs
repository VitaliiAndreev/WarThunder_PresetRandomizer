using Core.Enumerations;

namespace Core.WarThunderUnpackingToolsIntegration.Enumerations
{
    public class EFile
    {
        internal const string Vromfs = "vromfs";

        #region Root Folder

        private const string _vromfsBin = ECharacterString.Period + Vromfs + ECharacterString.Period + EFileExtension.Bin;

        public const string CoreParameters = "aces" + _vromfsBin;
        public const string StatAndBalanceParameters = "char" + _vromfsBin;
        public const string GuiParameters = "gui" + _vromfsBin;
        public const string LocalizationParameters = "lang" + _vromfsBin;
        public const string MissionParameters = "mis" + _vromfsBin;
        public const string WebUiParameters = "webgUi" + _vromfsBin;

        #endregion Root Folder
    }
}
