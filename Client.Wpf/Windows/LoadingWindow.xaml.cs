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
        #region Fields

        /// <summary> An exception that has occurred in a separate thread during execution of an asynchronous task. </summary>
        private Exception _asynchronousException;

        /// <summary> Indicates whether a message box is currenly shown. </summary>
        private bool _messageBoxIsUp;

        #endregion Fields
        #region Properties

        /// <summary> An instance of a presenter. </summary>
        new public ILoadingWindowPresenter Presenter => base.Presenter as ILoadingWindowPresenter;

        /// <summary> An instance of a presenter. </summary>
        IPresenter IWindowWithPresenter.Presenter => base.Presenter;

        /// <summary> Indicates whether the initialization has been completed. </summary>
        public bool InitializationComplete { get; set; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new loading window. </summary>
        /// <param name="presenter"> The presenter to attach. </param>
        public LoadingWindow(ILoadingWindowPresenter presenter)
            : base(EWpfClientLogCategory.LoadingWindow, null, presenter)
        {
            InitializeComponent();
            Localise();

            Log.Debug(ECoreLogMessage.Initialised);
        }

        #endregion Constructors
        #region Methods: Event Handlers

        /// <summary> Logs showing of the window and asynchronously prepares data. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Not used. </param>
        private async void OnLoaded(object sender, RoutedEventArgs eventArguments)
        {
            Log.Debug(ECoreLogMessage.Shown);

            var prepareDataTask = new Task(PrepareData);
            Action prepareDataAsynchronously = () => prepareDataTask.Start();

            if (!ApplicationHelpers.SettingsManager.WarThunderLocationIsValid() || !ApplicationHelpers.SettingsManager.KlensysWarThunderToolLocationIsValid())
                Presenter.GetCommand(ECommandName.OpenSettings).Execute(Presenter);

            // Await doesn't work properly here.
            await Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, prepareDataAsynchronously);

            // To work around the not functional await a delay is implemented.
            while (prepareDataTask.Status != TaskStatus.RanToCompletion)
            {
                await Task.Delay(EInteger.Time.MillisecondsInSecond / EInteger.Number.Hundred);

                if (_asynchronousException is Exception)
                    throw _asynchronousException;
            }

            OnDataPrepared();
        }

        /// <summary> Sets the status as "ready" and either closes after five seconds or by user input by closing the window within five seconds or closing the opened message box. </summary>
        private async void OnDataPrepared()
        {
            var secondsBeforeAutoClosure = 5;
            var closeImmediately = false;

            _status.Foreground = new SolidColorBrush(EColor.ValidText);
            _status.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.Ready);

            if (_messageBoxIsUp)
            {
                closeImmediately = true;

                while (_messageBoxIsUp)
                    await Task.Delay(EInteger.Time.MillisecondsInSecond / EInteger.Number.Four);
            }

            if (!closeImmediately)
            {
                _status.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.ReadyWithCountDown).Format(secondsBeforeAutoClosure);

                await Task.Delay(secondsBeforeAutoClosure * EInteger.Time.MillisecondsInSecond);
            }

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
        public override void Localise()
        {
            base.Localise();

            Title = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.ApplicationName);

            _statusLabel.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.Status);

            _status.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.Loading);
            _status.Foreground = new SolidColorBrush(EColor.InvalidText);
        }

        /// <summary> Calls the <see cref="ECommandName.Initialize"/> command. </summary>
        private void PrepareData()
        {
            try
            {
                Presenter.ExecuteCommand(ECommandName.Initialize);
            }
            catch (Exception exception) // The catch block set up to intercept exceptions in WpfClient doesn't catch exceptions in other threads.
            {
                _asynchronousException = exception; // This exception is passed on back to the main thread to be handled by WpfClient.
            }
        }

        /// <summary> Show a dialog warning the user about ongoing initialization and asking for a confirmation to exit. </summary>
        /// <returns></returns>
        private bool ClosureConfirmed()
        {
            var title = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.ApplicationName);
            var message = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.InitialisationUnderwayConfirmClosure);

            _messageBoxIsUp = true;

            var messageBoxResult = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes;

            _messageBoxIsUp = false;

            return messageBoxResult;
        }
    }
}