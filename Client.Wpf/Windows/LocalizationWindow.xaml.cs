using Client.Shared.Enumerations;
using Client.Shared.Wpf.Extensions;
using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Windows.Interfaces;
using Client.Wpf.Windows.Interfaces.Base;
using Core;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Client.Wpf.Windows
{
    /// <summary> Interaction logic for LocalizationWindow.xaml. </summary>
    public partial class LocalizationWindow : BaseWindow, ILocalizationWindow
    {
        #region Fields

        /// <summary> Indicates whether the application is to be restarted after selecting a language. </summary>
        private readonly bool _restartAfterSelection;

        #endregion Fields
        #region Properties

        /// <summary> An instance of a presenter. </summary>
        new public ILocalizationWindowPresenter Presenter => base.Presenter as ILocalizationWindowPresenter;

        /// <summary> An instance of a presenter. </summary>
        IPresenter IWindowWithPresenter.Presenter => base.Presenter;

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new localization window. </summary>
        /// <param name="presenter"> The presenter to attach. </param>
        /// <param name="parentWindow"> The window that owns this one. </param>
        /// <param name="restartAfterSelection"> Whether the application is to be restarted after selecting a language. </param>
        public LocalizationWindow(ILocalizationWindowPresenter presenter, IBaseWindow parentWindow, bool restartAfterSelection = false)
            : base(nameof(LocalizationWindow), parentWindow, presenter)
        {
            _restartAfterSelection = restartAfterSelection;

            Presenter.Language = WpfSettings.Localization;

            InitializeComponent();
            Localise();

            this._buttonEnglishUsa.Tag = Core.Language.English;
            this._buttonRussian.Tag = Core.Language.Russian;

            Log.Debug(CoreLogMessage.Initialised);
        }

        #endregion Constructors
        #region Methods: Event Handlers

        /// <summary> Logs showing of the window. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnLoaded(object sender, RoutedEventArgs eventArguments) =>
            Log.Debug(CoreLogMessage.Shown);

        /// <summary> Select US English as the localization language. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Event arguments. </param>
        private void OnClick(object sender, RoutedEventArgs eventArguments)
        {
            if (eventArguments.OriginalSource is Button button)
                SelectLocalization(button.GetTag<Language>());
        }

        /// <summary> Logs closing of the window and shuts the application down if no localization is selected (in case of the first launch). </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs eventArguments)
        {
            Log.Debug(CoreLogMessage.Closed);

            if (string.IsNullOrWhiteSpace(Presenter.Language) && string.IsNullOrWhiteSpace(WpfSettings.Localization))
                Environment.Exit(0);
        }

        #endregion Methods: Event Handlers

        /// <summary> Applies localization to visible text in the window. </summary>
        public override void Localise()
        {
            base.Localise();

            Title = ApplicationHelpers.LocalisationManager is null
                ? EClientApplicationName.WarThunderPresetRandomizerAbbreviated
                : ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.ApplicationNameAbbreviated);
        }

        /// <summary> Selects the given language for localization and restarts the application if it's not the first start. </summary>
        /// <param name="language"> The language to select for localization. </param>
        private void SelectLocalization(Language language)
        {
            if (Presenter.Language == language.ToString())
            {
                Close();
                return;
            }

            if (_restartAfterSelection && !RestartApproved())
                return;

            Presenter.Language = language.ToString();
            Presenter.ExecuteCommand(ECommandName.SelectLocalization);

            if (_restartAfterSelection)
            {
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location, WpfClient.StartupArguments);

                Log.Debug(CoreLogMessage.Closed);
                Environment.Exit(0);
            }
            else
            {
                Close();
            }
        }

        /// <summary> Show a dialog warning the user about restarting the application and asking for confirmation. </summary>
        /// <returns></returns>
        private bool RestartApproved()
        {
            var title = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.ApplicationName);
            var message = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.ApplicationWillBeRestarted);

            return MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes;
        }
    }
}