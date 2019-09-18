using Client.Wpf.Enumerations.Logger;
using Client.Wpf.Helpers.Interfaces;
using Client.Wpf.Presenters;
using Client.Wpf.Strategies;
using Client.Wpf.Windows;
using Client.Wpf.Windows.Interfaces;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;

namespace Client.Wpf.Helpers
{
    /// <summary> A class for instantiating windows. </summary>
    public class WindowFactory : LoggerFluency, IWindowFactory
    {
        #region Constructors

        /// <summary> Creates a new manager and loads settings stored in the settings file. </summary>
        /// <param name="loggers"> Instances of loggers. </param>
        public WindowFactory(params IConfiguredLogger[] loggers)
            : base(EWpfClientLogCategory.WindowFactory, loggers)
        {
            LogDebug(ECoreLogMessage.Created.FormatFluently(EWpfClientLogCategory.WindowFactory));
        }

        #endregion Constructors

        /// <summary> Creates an instance of the localization window. </summary>
        /// <param name="parentWindow"> The window that owns the new instance. </param>
        /// <param name="restartAfterSelection"> Whether the application is to be restarted after selecting a language. </param>
        /// <returns></returns>
        public ILocalizationWindow CreateLocalizationWindow(IBaseWindow parentWindow = null, bool restartAfterSelection = false)
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

        /// <summary> Creates an instance of the main window. </summary>
        /// <returns></returns>
        public IMainWindow CreateMainWindow()
        {
            var strategy = new MainWindowStrategy();
            var presenter = new MainWindowPresenter(strategy);
            var window = new MainWindow(presenter);

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