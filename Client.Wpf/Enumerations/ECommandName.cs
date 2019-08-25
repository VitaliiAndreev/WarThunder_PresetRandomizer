using Client.Wpf.Commands.LoadingWindow;
using Client.Wpf.Commands.LocalizationWindow;
using Client.Wpf.Commands.SettingsWindow;

namespace Client.Wpf.Enumerations
{
    /// <summary> Names of available commands. </summary>
    public enum ECommandName
    {
        /// <summary> See <see cref="InitializeCommand"/>. </summary>
        Initialize,
        /// <summary> See <see cref="OkCommand"/>. </summary>
        Ok,
        /// <summary> See <see cref="SelectLocalizationCommand"/>. </summary>
        SelectLocalization,
    }
}