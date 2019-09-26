using Client.Wpf.Presenters.Interfaces;
using Core.Organization.Enumerations;

namespace Client.Wpf.Windows.Interfaces
{
    /// <summary> The main window. </summary>
    public interface IMainWindow : IBaseWindow
    {
        /// <summary> An instance of a presenter. </summary>
        new IMainWindowPresenter Presenter { get; }

        /// <summary> Resets preset control to their default states. </summary>
        void ResetPresetControls();

        /// <summary> Loads <see cref="IMainWindowPresenter.GeneratedPresets"/>. </summary>
        void LoadPresets();

        /// <summary> Displays the specified preset from <see cref="IMainWindowPresenter.GeneratedPresets"/>. </summary>
        /// <param name="preset"> The preset to display. </param>
        void DisplayPreset(EPreset preset);
    }
}