using Client.Wpf.Commands.Interfaces;
using Client.Wpf.Controls;
using Client.Wpf.Enumerations;
using Client.Wpf.Enumerations.Logger;
using Client.Wpf.Extensions;
using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Windows.Interfaces;
using Client.Wpf.Windows.Interfaces.Base;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Organization.Enumerations;
using Core.WarThunderExtractionToolsIntegration;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
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

            _generatePresetButton.Command = Presenter.GetCommand(ECommandName.GeneratePreset);
            _generatePresetButton.CommandParameter = Presenter;

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

            _presetPanel.AttachCommands(Presenter.GetCommand(ECommandName.SwapPresets), Presenter.GetCommand(ECommandName.DeletePresets), Presenter);

            RaiseGeneratePresetCommandCanExecuteChanged();

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
            RaiseGeneratePresetCommandCanExecuteChanged();
        }

        /// <summary> Updates <see cref="IMainWindowPresenter.EnabledBranches"/> according to the action. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="ToggleButton"/> is expected. </param>
        /// <param name="eventArguments"> Event arguments. </param>
        private void OnBranchToggleControlClick(object sender, RoutedEventArgs eventArguments)
        {
            if (eventArguments.OriginalSource is ToggleButton toggleButton)
            {
                var branch = toggleButton.Tag.CastTo<EBranch>();

                if (toggleButton.IsChecked.Value)
                    Presenter.EnabledBranches.Add(branch);
                else
                    Presenter.EnabledBranches.Remove(branch);

                Presenter.ExecuteCommand(ECommandName.ToggleBranch);
                Presenter.ExecuteCommand(ECommandName.DeletePresets);
                RaiseGeneratePresetCommandCanExecuteChanged();
            }
        }

        /// <summary> Applies the highlighting style to the vehicle's conterpart in the research tree. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="ResearchTreeCellVehicleControl"/> is expected. </param>
        /// <param name="eventArguments"> Event arguments. </param>
        private void OnPresetVehicleMouseEnter(object sender, MouseEventArgs eventArguments)
        {
            if (eventArguments.OriginalSource is ResearchTreeCellVehicleControl vehicleControl)
                _researchTreeControl.Highlight(vehicleControl.Vehicle);
        }

        /// <summary> Applies the idle style to the vehicle's conterpart in the research tree. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="ResearchTreeCellVehicleControl"/> is expected. </param>
        /// <param name="eventArguments"> Event arguments. </param>
        private void OnPresetVehicleMouseLeave(object sender, MouseEventArgs eventArguments)
        {
            if (eventArguments.OriginalSource is ResearchTreeCellVehicleControl vehicleControl)
                _researchTreeControl.RemoveHighlight(vehicleControl.Vehicle);
        }

        /// <summary> Bring the vehicle into view. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="ResearchTreeCellVehicleControl"/> is expected. </param>
        /// <param name="eventArguments"> Event arguments. </param>
        private void OnPresetVehicleClick(object sender, RoutedEventArgs eventArguments)
        {
            if (eventArguments.OriginalSource is ResearchTreeCellVehicleControl vehicleControl)
            {
                _researchTreeControl.BringIntoView(vehicleControl.Vehicle, true);
                _researchTreeControl.Highlight(vehicleControl.Vehicle); // If tabs change, the vehicle needs to be highlighted since the mouse is over it.
            }
        }

        #endregion Methods: Event Handlers

        /// <summary> Applies localization to visible text in the window. </summary>
        public override void Localize()
        {
            base.Localize();

            Title = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.ApplicationName);

            _generatePresetButton.Content = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.GeneratePreset);
            _aboutButton.Caption = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.AboutWtpr);

            _gameModeSelectionControl.Localize();
            _presetPanel.Localize();
            _researchTreeControl.Localize();
        }

        /// <summary> Checks whether it is possible to generate presets. </summary>
        private void RaiseGeneratePresetCommandCanExecuteChanged() => (_generatePresetButton.Command as ICustomCommand)?.RaiseCanExecuteChanged(Presenter);

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

        /// <summary> Selects the specified game mode. </summary>
        /// <param name="gameMode"> The game mode to select. </param>
        /// <param name="simulateClick"> Whether to simulate a click on the appropriate button. </param>
        private void SelectGameMode(EGameMode gameMode, bool simulateClick = false)
        {
            if (simulateClick)
                _gameModeSelectionControl.OnClick(_gameModeSelectionControl.Buttons[gameMode], new RoutedEventArgs());

            if (Presenter.CurrentGameMode == gameMode)
                return;

            _researchTreeControl.DisplayBattleRatingFor(gameMode);

            Presenter.CurrentGameMode = gameMode;
            Presenter.ExecuteCommand(ECommandName.SelectGameMode);
            Presenter.ExecuteCommand(ECommandName.DeletePresets);
        }

        /// <summary> Gets all empty branches (their tabs should be disabled). </summary>
        /// <returns></returns>
        public IDictionary<ENation, IEnumerable<EBranch>> GetEmptyBranches() => _researchTreeControl.GetEmptyBranches();

        /// <summary> Resets preset control to their default states. </summary>
        public void ResetPresetControls()
        {
            Presenter.CurrentPreset = EPreset.Primary;
            _presetPanel.ResetControls();
        }

        /// <summary> Loads <see cref="IMainWindowPresenter.GeneratedPresets"/>. </summary>
        public void LoadPresets() => _presetPanel.LoadPresets(Presenter.GeneratedPresets, Presenter.CurrentGameMode);

        /// <summary> Displays the message that no vehicles suit the criteria. </summary>
        public void ShowNoResults() => _presetPanel.ShowNoResults();

        /// <summary> Displays the specified preset from <see cref="IMainWindowPresenter.GeneratedPresets"/>. </summary>
        /// <param name="preset"> The preset to display. </param>
        public void DisplayPreset(EPreset preset) => _presetPanel.DisplayPreset(preset);

        /// <summary> Resets <see cref="UIElement.IsEnabled"/> statuses of nation and branch tabs of the research tree. </summary>
        public void ResetResearchTreeTabRestrictions() => _researchTreeControl.ResetTabRestrictions();

        /// <summary> Disables all nation and branch tabs of the research tree not specified in the parameters. </summary>
        /// <param name="nation"> The nation tab to keep enabled. </param>
        /// <param name="branches"> Branch tabs to keep enabled. </param>
        public void EnableOnly(ENation nation, IEnumerable<EBranch> branches) => _researchTreeControl.EnableOnly(nation, branches);

        /// <summary> Focuses on a research tree by given parameters. </summary>
        /// <param name="nation"> The nation whose <paramref name="branch"/> to put into focus. </param>
        /// <param name="branch"> The branch to put into focus. </param>
        public void FocusResearchTree(ENation nation, EBranch branch) => _researchTreeControl.FocusResearchTree(nation, branch);

        /// <summary> Scrolls the research tree to bring the specified vehicle into view. </summary>
        /// <param name="vehicle"> The vehicle to bring into view. </param>
        public void BringIntoView(IVehicle vehicle) => _researchTreeControl.BringIntoView(vehicle);

        /// <summary> Highlights the specified vehicle in the reseatch tree. </summary>
        /// <param name="vehicle"> The vehicle to highlight. </param>
        public void Highlight(IVehicle vehicle) => _researchTreeControl.Highlight(vehicle);

        /// <summary> Removes the highlight from the specified vehicle in the reseatch tree. </summary>
        /// <param name="vehicle"> The vehicle to remove highlight from. </param>
        public void RemoveHighlight(IVehicle vehicle) => _researchTreeControl.RemoveHighlight(vehicle);
    }
}