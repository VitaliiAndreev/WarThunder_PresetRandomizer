using Client.Wpf.Helpers.Interfaces;
using Client.Wpf.Presenters;
using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Strategies;
using Client.Wpf.Windows;
using Client.Wpf.Windows.Interfaces;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;

namespace Client.Wpf.Helpers
{
    /// <summary> A class for instantiating windows. </summary>
    public class WindowFactory : LoggerFluency, IWindowFactory
    {
        #region Constructors

        /// <summary> Creates a new windows factory. </summary>
        /// <param name="loggers"> Instances of loggers. </param>
        public WindowFactory(params IConfiguredLogger[] loggers)
            : base(nameof(WindowFactory), loggers)
        {
            LogDebug($"{nameof(WindowFactory)} created.");
        }

        #endregion Constructors

        /// <summary> Creates an instance of the localization window. </summary>
        /// <param name="parentWindow"> The window that owns the new instance. </param>
        /// <param name="restartAfterSelection"> Whether the application is to be restarted after selecting a language. </param>
        /// <returns></returns>
        public ILocalizationWindow CreateLocalisationWindow(IBaseWindow parentWindow = null, bool restartAfterSelection = false)
        {
            var strategy = new LocalizationWindowStrategy();
            var presenter = new LocalizationWindowPresenter(strategy);
            var window = new LocalizationWindow(presenter, parentWindow, restartAfterSelection);

            return window;
        }

        /// <summary> Creates an instance of the settings window. </summary>
        /// <param name="parentWindow"> The window that owns the new instance. </param>
        /// <returns></returns>
        public ISettingsWindow CreateSettingsWindow(IBaseWindow parentWindow)
        {
            var strategy = new SettingsWindowStrategy();
            var presenter = new SettingsWindowPresenter(strategy);
            var window = new SettingsWindow(presenter, parentWindow);

            return window;
        }

        /// <summary> Creates an instance of the loading window. </summary>
        /// <returns></returns>
        public ILoadingWindow CreateLoadingWindow()
        {
            var strategy = new LoadingWindowStrategy();
            var presenter = new LoadingWindowPresenter(strategy);
            var window = new LoadingWindow(presenter);

            return window;
        }

        public IGuiLoadingWindow CreateGuiLoadingWindow()
        {
            var presenter = new GuiLoadingWindowPresenter();
            var window = new GuiLoadingWindow(presenter);

            return window;
        }

        /// <summary> Creates an instance of the main window. </summary>
        /// <param name="guiLoadingWindowPresenter"> An instance of a presenter to communicate with the GUI loading window. </param>
        /// <returns></returns>
        public IMainWindow CreateMainWindow(IGuiLoadingWindowPresenter guiLoadingWindowPresenter)
        {
            var strategy = new MainWindowStrategy();
            var presenter = new MainWindowPresenter(strategy);
            var window = new MainWindow(presenter, guiLoadingWindowPresenter);

            return window;
        }

        /// <summary> Creates an instance of the "About" window. </summary>
        /// <param name="parentWindow"> The window that owns the new instance. </param>
        /// <returns></returns>
        public IAboutWindow CreateAboutWindow(IBaseWindow parentWindow)
        {
            var strategy = new AboutWindowStrategy();
            var presenter = new AboutWindowPresenter(strategy);
            var window = new AboutWindow(presenter, parentWindow);

            return window;
        }
    }
}