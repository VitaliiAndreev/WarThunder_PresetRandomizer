using Client.Wpf.Commands.Interfaces;
using Client.Wpf.Enumerations;
using Client.Wpf.Enumerations.Logger;
using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Windows.Interfaces;
using Client.Wpf.Windows.Interfaces.Base;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.UnpackingToolsIntegration.Enumerations;
using Core.WarThunderExtractionToolsIntegration;
using System;
using System.ComponentModel;
using System.Windows;

namespace Client.Wpf.Windows
{
    /// <summary> Interaction logic for SettingsWindow.xaml. </summary>
    public partial class SettingsWindow : BaseWindow, ISettingsWindow
    {
        #region Properties

        /// <summary> An instance of a presenter. </summary>
        new public ISettingsWindowPresenter Presenter => base.Presenter as ISettingsWindowPresenter;

        /// <summary> An instance of a presenter. </summary>
        IPresenter IWindowWithPresenter.Presenter => base.Presenter;

        /// <summary> Indicates whether the location of the War Thunder directory is valid. </summary>
        public bool WarThunderLocationIsValid => _warThunderLocationControl.AddressIsValid;

        /// <summary> Indicates whether the location of the Klensy's War Thunder Tools directory is valid. </summary>
        public bool KlensysWarThunderToolsLocationIsValid => _klensysWarThunderToolsLocationControl.AddressIsValid;

        /// <summary> Location of the War Thunder directory. </summary>
        public string WarThunderLocation => _warThunderLocationControl.TextBoxText;

        /// <summary> Location of the Klensy's War Thunder Tools directory. </summary>
        public string KlensysWarThunderToolsLocation => _klensysWarThunderToolsLocationControl.TextBoxText;

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new settings window. </summary>
        /// <param name="presenter"> The presenter to attach. </param>
        /// <param name="parentWindow"> The window that owns this one. </param>
        public SettingsWindow(ISettingsWindowPresenter presenter, IBaseWindow parentWindow)
            : base(EWpfClientLogCategory.SettingsWindow, parentWindow, presenter)
        {
            Presenter.ClosingState = ESettingsWindowClosureState.NotClosing;

            InitializeComponent();
            Localise();

            _warThunderLocationControl.AddressValidator = ApplicationHelpers.FileManager.WarThunderLocationIsValid;
            _warThunderLocationControl.TextBoxText = Settings.WarThunderLocation;
            _warThunderLocationControl.AddressValidityChanged += AddressValidityChanged;

            if (_warThunderLocationControl.AddressIsValid)
                Presenter.PreviousValidWarThunderLocation = _warThunderLocationControl.TextBoxText;

            _klensysWarThunderToolsLocationControl.AddressValidator = ApplicationHelpers.FileManager.KlensysWarThunderToolLocationIsValid;
            _klensysWarThunderToolsLocationControl.TextBoxText = Settings.KlensysWarThunderToolsLocation;
            _klensysWarThunderToolsLocationControl.AddressValidityChanged += AddressValidityChanged;

            if (_klensysWarThunderToolsLocationControl.AddressIsValid)
                Presenter.PreviousValidKlensysWarThunderToolsLocation = _klensysWarThunderToolsLocationControl.TextBoxText;

            _okButton.CommandParameter = Presenter;
            _okButton.Command = Presenter.GetCommand(ECommandName.Ok);

            _cancelButton.CommandParameter = Presenter;
            _cancelButton.Command = Presenter.GetCommand(ECommandName.Cancel);

            Log.Debug(ECoreLogMessage.Initialized);
        }

        #endregion Constructors
        #region Methods: Event Handlers

        /// <summary> Logs showing of the window. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnLoaded(object sender, RoutedEventArgs eventArguments) =>
            Log.Debug(ECoreLogMessage.Shown);

        /// <summary> Checks whether the OK button can be enabled. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void AddressValidityChanged(object sender, EventArgs eventArguments) =>
            (_okButton.Command as ICustomCommand)?.RaiseCanExecuteChanged(Presenter);

        /// <summary> Handles closing the window. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Event arguments. </param>
        private void OnClosing(object sender, CancelEventArgs eventArguments)
        {
            if (Presenter.ClosingState != ESettingsWindowClosureState.ClosingFromCommand)
            {
                Presenter.ClosingState = ESettingsWindowClosureState.ClosingExplicitly;
                _cancelButton.Command.Execute(Presenter);
            }
            eventArguments.Cancel = Presenter.ClosingState == ESettingsWindowClosureState.ClosingCancelled;
        }
        
        /// <summary> Logs closing of the window. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnClosed(object sender, EventArgs eventArguments) =>
            LogClosure();

        #endregion Methods: Event Handlers

        /// <summary> Applies localization to visible text in the window. </summary>
        public override void Localise()
        {
            base.Localise();

            Title = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.ApplicationName);

            _warThunderLocationControl.Localize();
            _warThunderLocationControl.LabelText = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Location).FormatFluently(EApplicationName.WarThunder);

            _klensysWarThunderToolsLocationControl.Localize();
            _klensysWarThunderToolsLocationControl.LabelText = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Location).FormatFluently(EApplicationName.KlensysWarThunderTools);

            _cancelButton.Content = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Cancel);
            _okButton.Content = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Ok);
        }

        /// <summary> Logs closing of the window. </summary>
        public void LogClosure() =>
            Log.Debug(ECoreLogMessage.Closed);
    }
}