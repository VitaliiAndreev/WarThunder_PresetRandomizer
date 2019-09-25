using Client.Wpf.Enumerations;
using Client.Wpf.Enumerations.Logger;
using Client.Wpf.Extensions;
using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Windows.Interfaces;
using Client.Wpf.Windows.Interfaces.Base;
using Core.DataBase.WarThunder.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.WarThunderExtractionToolsIntegration;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;

namespace Client.Wpf.Windows
{
    /// <summary> Interaction logic for MainWindow.xaml. </summary>
    public partial class MainWindow : BaseWindow, IMainWindow
    {
        #region Properties

        /// <summary> An instance of a presenter. </summary>
        new public IMainWindowPresenter Presenter { get; private set; }

        /// <summary> An instance of a presenter. </summary>
        IPresenter IWindowWithPresenter.Presenter => Presenter;

        #endregion Properties
        #region Constructor

        /// <summary> Creates a new main window. </summary>
        /// <param name="presenter"> The presenter to attach. </param>
        public MainWindow(IMainWindowPresenter presenter)
            : base(EWpfClientLogCategory.MainWindow)
        {
            Presenter = presenter;
            Presenter.SetParentWindow(this);

            InitializeComponent();
            Localize();

            Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;

            _gameModeSelectionControl.ArcadeButtonClick += OnGameModeButtonClick;
            _gameModeSelectionControl.RealisticButtonClick += OnGameModeButtonClick;
            _gameModeSelectionControl.SimulatorButtonClick += OnGameModeButtonClick;

            _settingsButton.CommandParameter = Presenter;
            _settingsButton.Command = Presenter.GetCommand(ECommandName.OpenSettings);

            _localizationButton.CommandParameter = Presenter;
            _localizationButton.Command = Presenter.GetCommand(ECommandName.ChangeLocalization);
            _localizationButton.Content = new Image()
            {
                Style = this.GetStyle(EStyleKey.Image.FlagIcon),
                Source = FindResource(WpfSettings.LocalizationLanguage.GetFlagResourceKey()) as BitmapSource,
            };

            _aboutButton.EmbeddedButton.CommandParameter = Presenter;
            _aboutButton.EmbeddedButton.Command = Presenter.GetCommand(ECommandName.About);

            _researchTreeControl.Populate();

            SelectGameMode(string.IsNullOrWhiteSpace(WpfSettings.CurrentGameMode) ? EGameMode.Arcade : WpfSettings.CurrentGameMode.ParseEnumeration<EGameMode>(), true);

            _branchToggleControl.Toggle(Presenter.EnabledBranches, true);

            Log.Debug(ECoreLogMessage.Initialized);
        }

        #endregion Constructor
        #region Methods: Event Handlers

        /// <summary> Logs showing of the window. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnLoaded(object sender, RoutedEventArgs eventArguments) =>
            Log.Debug(ECoreLogMessage.Shown);

        /// <summary> Logs closing of the window. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnClosed(object sender, EventArgs eventArguments) =>
            Log.Debug(ECoreLogMessage.Closed);

        /// <summary> Enables or disables the fleet depending on the specified game mode. </summary>
        /// <param name="gameMode"> The game mode to adjust for. </param>
        private void AdjustFleetAvailability(EGameMode gameMode)
        {
            var enableFleet = gameMode != EGameMode.Simulator;

            if (!enableFleet)
            {
                Presenter.EnabledBranches.Remove(EBranch.Fleet);

                _branchToggleControl.Toggle(EBranch.Fleet, false);
            }

            _branchToggleControl.Enable(EBranch.Fleet, enableFleet);
        }

        /// <summary> Selects the game mode whose button is pressed. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="Button"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnGameModeButtonClick(object sender, RoutedEventArgs eventArguments)
        {
            if (!(sender is ToggleButton button))
                return;

            if (!(button.Tag is EGameMode buttonGameMode))
                return;

            AdjustFleetAvailability(buttonGameMode);
            SelectGameMode(buttonGameMode);
        }

        /// <summary> Updates <see cref="IMainWindowPresenter.EnabledBranches"/> according to the action. </summary>
        /// <param name="sender"></param>
        /// <param name="eventArguments"></param>
        private void OnBranchToggleControlClick(object sender, RoutedEventArgs eventArguments)
        {
            if (eventArguments.Source is ToggleButton toggleButton)
            {
                var branch = toggleButton.Tag.CastTo<EBranch>();

                if (toggleButton.IsEnabled)
                    Presenter.EnabledBranches.Add(branch);
                else
                    Presenter.EnabledBranches.Remove(branch);

                Presenter.ExecuteCommand(ECommandName.ToggleBranch);
            }
        }

        #endregion Methods: Event Handlers

        /// <summary> Applies localization to visible text in the window. </summary>
        public override void Localize()
        {
            base.Localize();

            Title = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.ApplicationName);

            _generatePresetButton.Content = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.GeneratePreset);
            _generatePresetButton.ToolTip = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.WillBeAvailableInAlphaReleases);

            _aboutButton.Caption = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.AboutWtpr);

            _gameModeSelectionControl.Localize();
            _researchTreeControl.Localize();
        }

        /// <summary> Selects the specified game mode. </summary>
        /// <param name="gameMode"> The game mode to select. </param>
        /// <param name="simulateClick"> Whether to simulate a click on the appropriate button. </param>
        private void SelectGameMode(EGameMode gameMode, bool simulateClick = false)
        {
            if (simulateClick)
                _gameModeSelectionControl.OnClick(_gameModeSelectionControl.Buttons[gameMode], new RoutedEventArgs());

            _researchTreeControl.DisplayBattleRatingFor(gameMode);

            Presenter.CurrentGameMode = gameMode;
            Presenter.ExecuteCommand(ECommandName.SelectGameMode);
        }
    }
}