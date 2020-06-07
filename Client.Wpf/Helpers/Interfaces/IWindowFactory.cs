using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Windows.Interfaces;

namespace Client.Wpf.Helpers.Interfaces
{
    /// <summary> A class for instantiating windows. </summary>
    public interface IWindowFactory
    {
        /// <summary> Creates an instance of the localisation window. </summary>
        /// <param name="parentWindow"> The window that owns the new instance. </param>
        /// <param name="restartAfterSelection"> Whether the application is to be restarted after selecting a language. </param>
        /// <returns></returns>
        ILocalizationWindow CreateLocalisationWindow(IBaseWindow parentWindow = null, bool restartAfterSelection = false);

        /// <summary> Creates an instance of the settings window. </summary>
        /// <param name="parentWindow"> The window that owns the new instance. </param>
        /// <returns></returns>
        ISettingsWindow CreateSettingsWindow(IBaseWindow parentWindow);

        /// <summary> Creates an instance of the loading window. </summary>
        /// <returns></returns>
        ILoadingWindow CreateLoadingWindow();

        IGuiLoadingWindow CreateGuiLoadingWindow();

        /// <summary> Creates an instance of the main window. </summary>
        /// <param name="guiLoadingWindowPresenter"> An instance of a presenter to communicate with the GUI loading window. </param>
        /// <returns></returns>
        IMainWindow CreateMainWindow(IGuiLoadingWindowPresenter guiLoadingWindowPresenter);

        /// <summary> Creates an instance of the "About" window. </summary>
        /// <param name="parentWindow"> The window that owns the new instance. </param>
        /// <returns></returns>
        IAboutWindow CreateAboutWindow(IBaseWindow parentWindow);
    }
}