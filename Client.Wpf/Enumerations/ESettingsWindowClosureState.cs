using Client.Wpf.Windows.Interfaces;

namespace Client.Wpf.Enumerations
{
    /// <summary> States of closure of an <see cref="ISettingsWindow"/>. </summary>
    public enum ESettingsWindowClosureState
    {
        NotClosing,
        ClosingExplicitly,
        ClosingFromCommand,
        ClosingCancelled,
    }
}