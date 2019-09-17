using Client.Wpf.Windows.Interfaces;

namespace Client.Wpf.Helpers.Interfaces
{
    /// <summary> A class for instantiating windows. </summary>
    public interface IWindowFactory
    {
        /// <summary> Creates an instance of the localization window. </summary>
        /// <returns></returns>
        ILocalizationWindow CreateLocalizationWindow(bool restartAfterSelection = false);
        /// <summary> Creates an instance of the settings window. </summary>
        /// <param name="parentWindow"> The window that owns the new instance. </param>
        /// <returns></returns>
        ISettingsWindow CreateSettingsWindow(IBaseWindow parentWindow);
        /// <summary> Creates an instance of the loading window. </summary>
        /// <returns></returns>
        ILoadingWindow CreateLoadingWindow();
        /// <summary> Creates an instance of the main window. </summary>
        /// <returns></returns>
        IMainWindow CreateMainWindow();
        /// <summary> Creates an instance of the "About" window. </summary>
        /// <param name="parentWindow"> The window that owns the new instance. </param>
        /// <returns></returns>
        IAboutWindow CreateAboutWindow(IBaseWindow parentWindow);
    }
}