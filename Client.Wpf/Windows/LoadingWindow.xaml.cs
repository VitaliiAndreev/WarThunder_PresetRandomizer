using Client.Wpf.Enumerations;
using Client.Wpf.Enumerations.Logger;
using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Windows.Interfaces;
using Client.Wpf.Windows.Interfaces.Base;
using Core.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Client.Wpf.Windows
{
    /// <summary> Interaction logic for LoadingWindow.xaml. </summary>
    public partial class LoadingWindow : BaseWindow, ILoadingWindow
    {
        #region Properties

        /// <summary> An instance of a presenter. </summary>
        new public ILoadingWindowPresenter Presenter { get; private set; }

        /// <summary> An instance of a presenter. </summary>
        IPresenter IWindowWithPresenter.Presenter => Presenter;

        /// <summary> Indicates whether the initialization has been completed. </summary>
        public bool InitializationComplete { get; set; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new loading window. </summary>
        /// <param name="presenter"> The presenter to attach. </param>
        public LoadingWindow(ILoadingWindowPresenter presenter)
            : base(EWpfClientLogCategory.LoadingWindow)
        {
            Presenter = presenter;
            Presenter.SetParentWindow(this);

            InitializeComponent();
            Localize();
        }

        #endregion Constructors
        #region Methods: Event Handlers

        /// <summary> Logs showing of the window and asynchronously prepares data. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Not used. </param>
        private async void OnLoaded(object sender, RoutedEventArgs eventArguments)
        {
            Log.Debug(ECoreLogMessage.Shown);

            var task = new Task(PrepareData);
            Action prepareDataAsynchronously = () => task.Start();

            if (!ApplicationHelpers.SettingsManager.WarThunderLocationIsValid() || !ApplicationHelpers.SettingsManager.KlensysWarThunderToolLocationIsValid())
                ApplicationHelpers.WindowFactory.CreateSettingsWindow(this).ShowDialog();

            // Await doesn't work properly here.
            await Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, prepareDataAsynchronously);

            // To work around the not functional await a delay is implemented.
            while (task.Status != TaskStatus.RanToCompletion)
                await Task.Delay(EInteger.Time.MillisecondsInSecond / EInteger.Number.Hundred);

            OnDataPrepared();
        }

        /// <summary> Sets the status as "ready" and closes after five seconds or by user input. </summary>
        private async void OnDataPrepared()
        {
            var secondsBeforeAutoClosure = 5;

            _status.Text = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Ready).FormatFluently(secondsBeforeAutoClosure);
            _status.Foreground = new SolidColorBrush(EColor.ValidText);

            await Task.Delay(secondsBeforeAutoClosure * EInteger.Time.MillisecondsInSecond);

            Close();
        }

        /// <summary> Logs closing of the window and shuts the application down if the user closes the window before initialization is finilized. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Event arguments. </param>
        private void OnClosing(object sender, CancelEventArgs eventArguments)
        {
            var exit = false;

            if (!InitializationComplete)
            {
                if (!ClosureConfirmed())
                {
                    eventArguments.Cancel = true;
                    return;
                }

                Log.Info(EWpfClientLogMessage.InitializationCancelled_ClosingApplication);
                exit = true;
            }

            Log.Debug(ECoreLogMessage.Closed);

            if (exit)
                Environment.Exit(0);
        }

        #endregion Methods: Event Handlers

        /// <summary> Applies localization to visible text in the window. </summary>
        public override void Localize()
        {
            Title = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.ApplicationName);

            _statusLabel.Text = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Status);

            _status.Text = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Loading);
            _status.Foreground = new SolidColorBrush(EColor.InvalidText);
        }

        /// <summary> Calls the <see cref="ECommandName.Initialize"/> command. </summary>
        private void PrepareData()
        {
            Presenter.ExecuteCommand(ECommandName.Initialize);
        }

        /// <summary> Show a dialog warning the user about ongoing initialization and asking for a confirmation to exit. </summary>
        /// <returns></returns>
        private bool ClosureConfirmed()
        {
            var title = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.ApplicationName);
            var message = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.InitializationUnderwayConfirmClosure);

            return MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes;
        }
    }
}