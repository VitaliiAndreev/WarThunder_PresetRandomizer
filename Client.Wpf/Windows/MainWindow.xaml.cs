﻿using Client.Wpf.Commands.Interfaces;
using Client.Wpf.Controls;
using Client.Wpf.Controls.Base.Interfaces;
using Client.Wpf.Enumerations;
using Client.Wpf.Enumerations.Logger;
using Client.Wpf.Extensions;
using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Windows.Interfaces;
using Client.Wpf.Windows.Interfaces.Base;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Objects;
using Core.Organization.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
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
        #region Fields

        /// <summary> Indicates whether the window is still being initialized. </summary>
        private readonly EInitializationStatus _initializationStatus = EInitializationStatus.NotInitialized;

        /// <summary> A collection of boxed instances of <see cref="IList{T}"/> accessed by their generic types. </summary>
        private IDictionary<Type, object> _presenterToggleLists;

        /// <summary> A collection of command names accesed by generic types appropriate to them. </summary>
        private IDictionary<Type, ECommandName> _toggleCommands;

        #endregion Fields
        #region Properties

        /// <summary> An instance of a presenter. </summary>
        new public IMainWindowPresenter Presenter => base.Presenter as IMainWindowPresenter;

        /// <summary> An instance of a presenter. </summary>
        IPresenter IWindowWithPresenter.Presenter => base.Presenter;

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new main window. </summary>
        /// <param name="presenter"> The presenter to attach. </param>
        public MainWindow(IMainWindowPresenter presenter)
            : base(EWpfClientLogCategory.MainWindow, null, presenter)
        {
            _initializationStatus = EInitializationStatus.Initializing;

            InitializeDictionaries();
            InitializeComponent();
            Localize();

            SetControlTags();
            AttachCommands();
            PopulateControls();
            UpdateControlsBasedOnSettingsAndData();

            Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;

            Log.Debug(ECoreLogMessage.Initialized);

            _initializationStatus = EInitializationStatus.Initialized;
        }

        #endregion Constructors
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
            if (!(eventArguments.OriginalSource is ToggleButton button))
                return;

            if (!(button.Tag is EGameMode buttonGameMode))
                return;

            AdjustFleetAvailability(buttonGameMode);
            SelectGameMode(buttonGameMode);
            RaiseGeneratePresetCommandCanExecuteChanged();
        }

        /// <summary> Executes a command associated with with the given type. </summary>
        /// <param name="key"> The type whose associated command to execute. </param>
        private void ExecuteToggleCommand(Type key) =>
            Presenter.ExecuteCommand(_toggleCommands[key]);

        /// <summary> Updates a collection and executes a command associated with with the <typeparamref name="T"/> <see cref="FrameworkElement.Tag"/> of the <paramref name="toggleButton"/> according to its <see cref="ToggleButton.IsChecked"/> status. </summary>
        /// <param name="toggleButton"> The button that has been clicked. </param>
        private void OnToggleButtonGroupControlClick<T>(ToggleButton toggleButton, bool executeCommand = true)
        {
            var keyType = typeof(T);
            var buttonTag = toggleButton.GetTag<T>();

            if (!keyType.IsKeyIn(_presenterToggleLists) || !keyType.IsKeyIn(_toggleCommands))
                return;

            if (toggleButton.IsChecked.Value)
                _presenterToggleLists[keyType].CastTo<IList<T>>().Add(buttonTag);
            else
                _presenterToggleLists[keyType].CastTo<IList<T>>().Remove(buttonTag);

            if (executeCommand)
                ExecuteToggleCommand(keyType);
        }

        /// <summary> Updates <see cref="IMainWindowPresenter.EnabledBranches"/> according to the action. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Event arguments. </param>
        private void OnBranchToggleControlClick(object sender, RoutedEventArgs eventArguments)
        {
            if (eventArguments.OriginalSource is ToggleButton toggleButton)
            {
                OnToggleButtonGroupControlClick<EBranch>(toggleButton);

                var branch = toggleButton.GetTag<EBranch>();

                _vehicleClassControl.Enable(branch, toggleButton.IsChecked.Value);

                if (toggleButton.IsChecked.Value && !Presenter.BranchHasVehicleClassesEnabled(branch))
                {
                    var vehicleClass = branch.GetVehicleClasses().First(vehicleClass => vehicleClass.IsValid());

                    _vehicleClassControl.Toggle(vehicleClass, true);
                    OnVehicleClassToggleControlClick(_vehicleClassControl, new RoutedEventArgs(VehicleClassToggleControl.ClickEvent, _vehicleClassControl.ToggleClassColumns[branch].Buttons[vehicleClass]));
                }

                RaiseGeneratePresetCommandCanExecuteChanged();
            }
        }

        /// <summary> Adjusts the state of the toggle all button. </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="toggleControl"></param>
        /// <param name="ownerEntity"></param>
        private void UpdateToggleAllButtonState<T, U>(IControlWithToggleColumns<T, U> toggleControl, T ownerEntity)
        {
            var toggleColumn = toggleControl.ToggleClassColumns[ownerEntity];
            var toggleButtonTagType = typeof(U);
            var toggleAllOn = toggleColumn.AllButtonsAreToggledOn();

            if (toggleButtonTagType.IsEnum)
            {
                if (toggleButtonTagType.GetEnumValues().OfType<U>().FirstOrDefault(item => item.ToString() == EWord.All) is U toggleButtonKey)
                    toggleColumn.Toggle(toggleButtonKey, toggleAllOn);
            }
            else
            {
                throw new NotImplementedException(EWpfClientLogMessage.NonEnumerationTagsAreNotSupportedYet);
            }
        }

        /// <summary> Updates <see cref="IMainWindowPresenter.EnabledVehicleClasses"/> according to the action. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Event arguments. </param>
        private void OnVehicleClassToggleControlClick(object sender, RoutedEventArgs eventArguments)
        {
            if (eventArguments.OriginalSource is ToggleButton toggleButton)
            {
                var vehicleClass = toggleButton.GetTag<EVehicleClass>();
                var ownerBranch = vehicleClass.GetBranch();

                if (vehicleClass.ToString().StartsWith(EWord.All))
                {
                    var disabledButtons = _vehicleClassControl.GetButtons(ownerBranch, !toggleButton.IsChecked.Value, false);

                    foreach (var button in disabledButtons)
                    {
                        _vehicleClassControl.Toggle(button.Tag.CastTo<EVehicleClass>(), toggleButton.IsChecked.Value);
                        OnToggleButtonGroupControlClick<EVehicleClass>(button, false);
                    }

                    ExecuteToggleCommand(typeof(EVehicleClass));
                }
                else
                {
                    OnToggleButtonGroupControlClick<EVehicleClass>(toggleButton);
                    UpdateToggleAllButtonState(_vehicleClassControl, ownerBranch);
                }

                UpdateOwnerControl(_branchToggleControl, Presenter.BranchHasVehicleClassesEnabled, ownerBranch, OnBranchToggleControlClick);
                RaiseGeneratePresetCommandCanExecuteChanged();
            }
        }

        /// <summary> Updates <see cref="IMainWindowPresenter.EnabledNations"/> according to the action. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Event arguments. </param>
        private void OnNationToggleControlClick(object sender, RoutedEventArgs eventArguments)
        {
            if (eventArguments.OriginalSource is ToggleButton toggleButton)
            {
                OnToggleButtonGroupControlClick<ENation>(toggleButton);

                var nation = toggleButton.GetTag<ENation>();

                AdjustBranchTogglesAvailability();
                _countryToggleControl.Enable(nation, toggleButton.IsChecked.Value);
                _battleRatingControl.Enable(nation, toggleButton.IsChecked.Value);

                if (toggleButton.IsChecked.Value && !Presenter.NationHasCountriesEnabled(nation))
                {
                    var country = nation.GetCountries().First();
                    var nationCountryPair = new NationCountryPair(nation, country);

                    _countryToggleControl.Toggle(nationCountryPair, true);
                    OnCountryToggleControlClick(_countryToggleControl, new RoutedEventArgs(CountryToggleControl.ClickEvent, _countryToggleControl.ToggleClassColumns[nation].Buttons[nationCountryPair]));
                }

                RaiseGeneratePresetCommandCanExecuteChanged();
            }
        }

        /// <summary> Updates the state of the given owner control based on whether the specified subordinate control has any buttons toggle on or not. </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ownerControl"> The owner control to update. </param>
        /// <param name="thereAreButtonsToggled"> The predicate to check if there are any toggled on buttons with. </param>
        /// <param name="key"> The key by which to look up buttons on the <paramref name="ownerControl"/>. </param>
        /// <param name="eventHandler"> The handler to process the state change further with. </param>
        private void UpdateOwnerControl<T>(ToggleButtonGroupControl<T> ownerControl, Predicate<T> thereAreButtonsToggled, T key, RoutedEventHandler eventHandler)
        {
            if (!thereAreButtonsToggled(key))
            {
                ownerControl.Toggle(key, false);
                eventHandler(ownerControl, new RoutedEventArgs(ownerControl.ClickEventReference, ownerControl.Buttons[key]));
            }
        }

        /// <summary> Updates <see cref="IMainWindowPresenter.EnabledCountries"/> according to the action. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Event arguments. </param>
        private void OnCountryToggleControlClick(object sender, RoutedEventArgs eventArguments)
        {
            if (eventArguments.OriginalSource is ToggleButton toggleButton)
            {
                OnToggleButtonGroupControlClick<NationCountryPair>(toggleButton);

                var nationCountryPair = toggleButton.GetTag<NationCountryPair>();

                if (!toggleButton.IsChecked.Value && !Presenter.NationHasCountriesEnabled(nationCountryPair.Nation))
                {
                    _nationToggleControl.Toggle(nationCountryPair.Nation, false);
                    OnNationToggleControlClick(_nationToggleControl, new RoutedEventArgs(NationToggleControl.ClickEvent, _nationToggleControl.Buttons[nationCountryPair.Nation]));
                }

                RaiseGeneratePresetCommandCanExecuteChanged();
            }
        }

        /// <summary> Updates <see cref="IMainWindowPresenter.EnabledEconomicRankIntervals"/> and executes the <see cref="ECommandName.ChangeBattleRating"/> command. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnBattleRatingValueChanged(object sender, RoutedEventArgs eventArguments)
        {
            if (_initializationStatus != EInitializationStatus.Initialized)
                return;

            var savingNeeded = false;

            foreach(var control in _battleRatingControl.BattleRatingControls.Values)
            {
                if (control.Tag is ENation nation)
                {
                    var previousInterval = Presenter.EnabledEconomicRankIntervals[nation];
                    var newInterval = new Interval<int>(true, control.MinimumEconomicRank, control.MaximumEconomicRank, true);

                    if (newInterval != previousInterval)
                    {
                        Presenter.EnabledEconomicRankIntervals[nation] = newInterval;
                        savingNeeded = true;
                    }
                }
            }

            if (savingNeeded)
                Presenter.ExecuteCommand(ECommandName.ChangeBattleRating);
        }

        /// <summary> Applies the highlighting style to the vehicle's conterpart in the research tree. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Event arguments. </param>
        private void OnPresetVehicleMouseEnter(object sender, MouseEventArgs eventArguments)
        {
            if (eventArguments.OriginalSource is ResearchTreeCellVehicleControl vehicleControl)
                _researchTreeControl.Highlight(vehicleControl.Vehicle);
        }

        /// <summary> Applies the idle style to the vehicle's conterpart in the research tree. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Event arguments. </param>
        private void OnPresetVehicleMouseLeave(object sender, MouseEventArgs eventArguments)
        {
            if (eventArguments.OriginalSource is ResearchTreeCellVehicleControl vehicleControl)
                _researchTreeControl.RemoveHighlight(vehicleControl.Vehicle);
        }

        /// <summary> Handles clicking on vehicle cards in the research tree or in presets. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Event arguments. </param>
        private void OnVehicleCardClick(object sender, RoutedEventArgs eventArguments)
        {
            if (eventArguments.OriginalSource is ResearchTreeCellVehicleControl vehicleControl)
            {
                if (vehicleControl.Type == EVehicleCard.Preset)
                {
                    _researchTreeControl.BringIntoView(vehicleControl.Vehicle, true);
                }
                else if (vehicleControl.Type == EVehicleCard.ResearchTree)
                {
                    var vehicleGaijinId = vehicleControl.Vehicle.GaijinId;

                    if (vehicleControl.IsToggled)
                        Presenter.EnabledVehicleGaijinIds.Add(vehicleGaijinId);
                    else
                        Presenter.EnabledVehicleGaijinIds.Remove(vehicleGaijinId);

                    Presenter.ExecuteCommand(ECommandName.ToggleVehicle);
                }
            }
        }

        #endregion Methods: Event Handlers
        #region Methods: Initialization

        /// <summary> Initialized dictionaries. </summary>
        private void InitializeDictionaries()
        {
            _presenterToggleLists = new Dictionary<Type, object>
            {
                { typeof(EBranch), Presenter.EnabledBranches },
                { typeof(EVehicleClass), Presenter.EnabledVehicleClasses },
                { typeof(ENation), Presenter.EnabledNations },
                { typeof(NationCountryPair), Presenter.EnabledCountries },
            };
            _toggleCommands = new Dictionary<Type, ECommandName>
            {
                { typeof(EBranch), ECommandName.ToggleBranch },
                { typeof(EVehicleClass), ECommandName.ToggleVehicleClass },
                { typeof(ENation), ECommandName.ToggleNation },
                { typeof(NationCountryPair), ECommandName.ToggleCountry },
            };
        }

        private void AttachCommands()
        {
            _generatePresetButton.CommandParameter = Presenter;
            _generatePresetButton.Command = Presenter.GetCommand(ECommandName.GeneratePreset);

            _settingsButton.CommandParameter = Presenter;
            _settingsButton.Command = Presenter.GetCommand(ECommandName.OpenSettings);

            _localizationButton.CommandParameter = Presenter;
            _localizationButton.Command = Presenter.GetCommand(ECommandName.ChangeLocalization);

            _aboutButton.EmbeddedButton.CommandParameter = Presenter;
            _aboutButton.EmbeddedButton.Command = Presenter.GetCommand(ECommandName.About);

            _presetPanel.AttachCommands(Presenter.GetCommand(ECommandName.SwapPresets), Presenter.GetCommand(ECommandName.DeletePresets), Presenter);
        }

        /// <summary> Sets control tags. </summary>
        private void SetControlTags()
        {
            _gameModeSelectionControl.Tag = EGameMode.None;
            _branchToggleControl.Tag = EBranch.None;
            _vehicleClassControl.Tag = EVehicleClass.None;
            _nationToggleControl.Tag = ENation.None;
            _countryToggleControl.Tag = ECountry.None;
            _battleRatingControl.Tag = $"{EWord.Battle} {EWord.Rating}";
        }

        /// <summary> Populates controls after initialization. </summary>
        private void PopulateControls()
        {
            _localizationButton.Content = new Image()
            {
                Style = this.GetStyle(EStyleKey.Image.LocalizationIcon),
                Source = FindResource(WpfSettings.LocalizationLanguage.GetFlagResourceKey()) as BitmapSource,
            };
            _researchTreeControl.Populate(Presenter.EnabledVehicleGaijinIds);
        }

        /// <summary> Updates controls on the window base on saved user settings and loaded data. </summary>
        private void UpdateControlsBasedOnSettingsAndData()
        {
            SelectGameMode(string.IsNullOrWhiteSpace(WpfSettings.CurrentGameMode) ? EGameMode.Arcade : WpfSettings.CurrentGameMode.ParseEnumeration<EGameMode>(), true);

            _nationToggleControl.RemoveUnavailableNations();
            _countryToggleControl.RemoveCountriesForUnavailableNations();
            _battleRatingControl.RemoveControlsForUnavailableNations();

            _branchToggleControl.Toggle(Presenter.EnabledBranches, true);
            _vehicleClassControl.Toggle(Presenter.EnabledVehicleClasses, true);
            _nationToggleControl.Toggle(Presenter.EnabledNations, true);
            _countryToggleControl.Toggle(Presenter.EnabledCountries, true);

            AdjustCountryControlsAvailability();

            _battleRatingControl.InitializeControls();
            AdjustBattleRatingControlsAvailability();

            foreach (var branch in Presenter.EnabledBranches)
                UpdateToggleAllButtonState(_vehicleClassControl, branch);

            AdjustBranchTogglesAvailability();
            RaiseGeneratePresetCommandCanExecuteChanged();
        }

        #endregion Methods: Initialization
        #region Methods: Overrides

        /// <summary> Applies localization to visible text in the window. </summary>
        public override void Localize()
        {
            base.Localize();

            Title = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.ApplicationName);

            _generatePresetButton.Content = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.GeneratePreset);
            _aboutButton.Caption = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.AboutWtpr);

            _gameModeSelectionControl.Localize();
            _vehicleClassControl.Localize();
            _nationToggleControl.Localize();
            _countryToggleControl.Localize();
            _battleRatingControl.Localize();

            _presetPanel.Localize();
            _researchTreeControl.Localize();
        }

        #endregion Methods: Overrides
        #region Methods: Adjusting Branch Toggle Availability

        /// <summary> Enables or disables branch toggles depending on whether any of <see cref="IMainWindowPresenter.EnabledNations"/> have associated branches implemented.</summary>
        private void AdjustBranchTogglesAvailability()
        {
            var allBranches = typeof(EBranch).GetEnumValues().Cast<EBranch>().Where(branch => branch.IsValid());
            var validBranches = Presenter.GetValidBraches();

            foreach (var branch in allBranches)
            {
                var enabled = branch.IsIn(validBranches);

                if (branch == EBranch.Fleet)
                    AdjustFleetAvailability(Presenter.CurrentGameMode, validBranches);
                else
                    AdjustBranchToggleAvailability(branch, enabled);
            }
        }

        /// <summary>
        /// Enables or disables the branch toggle for the given <paramref name="branch"/>.
        /// Disabling the toggle also disables the associated branch, but enabling the toggle doesn't enable the branch.
        /// </summary>
        /// <param name="branch"> The branch whose toggle to adjust. </param>
        /// <param name="branch"> Whether to enable the toggle or not. </param>
        private void AdjustBranchToggleAvailability(EBranch branch, bool enable)
        {
            if (!enable)
            {
                Presenter.EnabledBranches.Remove(branch);

                _branchToggleControl.Toggle(branch, false);
            }
            _branchToggleControl.Enable(branch, enable);
            _vehicleClassControl.Enable(branch, enable && Presenter.EnabledBranches.Contains(branch));

            Presenter.ExecuteCommand(ECommandName.ToggleBranch);
        }

        /// <summary> Enables or disables the fleet toggle depending on the specified game mode. </summary>
        /// <param name="gameMode"> The game mode to adjust for. </param>
        /// <param name="validBranches"> A collection of valid branches. </param>
        private void AdjustFleetAvailability(EGameMode gameMode, IEnumerable<EBranch> validBranches = null) =>
            AdjustBranchToggleAvailability(EBranch.Fleet, gameMode != EGameMode.Simulator && EBranch.Fleet.IsIn(validBranches is null ? Presenter.GetValidBraches() : validBranches));

        #endregion Methods: Adjusting Branch Toggle Availability

        /// <summary> Checks whether it is possible to generate presets. </summary>
        private void RaiseGeneratePresetCommandCanExecuteChanged() =>
            (_generatePresetButton.Command as ICustomCommand)?.RaiseCanExecuteChanged(Presenter);

        /// <summary> Selects the specified game mode. </summary>
        /// <param name="gameMode"> The game mode to select. </param>
        /// <param name="simulateClick"> Whether to simulate a click on the appropriate button. </param>
        private void SelectGameMode(EGameMode gameMode, bool simulateClick = false)
        {
            if (simulateClick)
                _gameModeSelectionControl.OnClick(_gameModeSelectionControl.Buttons[gameMode], new RoutedEventArgs());

            if (Presenter.CurrentGameMode == gameMode && !simulateClick)
                return;

            _researchTreeControl.DisplayVehicleInformation(gameMode);

            Presenter.CurrentGameMode = gameMode;
            Presenter.ExecuteCommand(ECommandName.SelectGameMode);
            Presenter.ExecuteCommand(ECommandName.DeletePresets);
        }

        /// <summary> Gets disabled nations. </summary>
        /// <returns></returns>
        private IEnumerable<ENation> GetDisabledNations() =>
            typeof(ENation)
                .GetEnumValues()
                .Cast<ENation>()
                .Where(nation => nation.IsValid())
                .Except(Presenter.EnabledNations)
            ;

        /// <summary> Disables <see cref="UpDownBattleRatingPairControl"/>s in <see cref="_battleRatingControl"/> corresponding to <paramref name="disabledNations"/>. </summary>
        /// <param name="disabledNations"> Nations whose <see cref="UpDownBattleRatingPairControl"/>s to disable. </param>
        private void DisableControls(IEnumerable<ENation> disabledNations, IControlWithSubcontrols<ENation> control)
        {
            foreach (var disabledNation in disabledNations)
                control.Enable(disabledNation, false);
        }

        /// <summary> Adjusts availability of <see cref="_countryToggleControl"/> according to <see cref="IMainWindowPresenter.EnabledNations"/>. </summary>
        private void AdjustCountryControlsAvailability() =>
            DisableControls(GetDisabledNations(), _countryToggleControl);

        /// <summary> Adjusts availability of <see cref="UpDownBattleRatingPairControl"/>s in <see cref="_battleRatingControl"/> according to <see cref="IMainWindowPresenter.EnabledNations"/>. </summary>
        private void AdjustBattleRatingControlsAvailability() =>
            DisableControls(GetDisabledNations(), _battleRatingControl);

        #region Methods: Calls to _presetPanel

        /// <summary> Resets preset control to their default states. </summary>
        public void ResetPresetControls()
        {
            Presenter.CurrentPreset = EPreset.Primary;
            _presetPanel.ResetControls();
        }

        /// <summary> Loads <see cref="IMainWindowPresenter.GeneratedPresets"/>. </summary>
        public void LoadPresets() =>
            _presetPanel.LoadPresets(Presenter.GeneratedPresets, Presenter.CurrentGameMode);

        /// <summary> Displays a message that no vehicles suit the criteria. </summary>
        public void ShowNoResults() =>
            _presetPanel.ShowNoResults();

        /// <summary> Displays a message that no vehicles suit the criteria with additional information. </summary>
        /// <param name="nation"> The nation. </param>
        /// <param name="mainBranch"> The branch. </param>
        public void ShowNoVehicles(ENation nation, EBranch mainBranch) =>
            _presetPanel.ShowNoVehicles(nation, mainBranch);

        /// <summary> Displays the specified preset from <see cref="IMainWindowPresenter.GeneratedPresets"/>. </summary>
        /// <param name="preset"> The preset to display. </param>
        public void DisplayPreset(EPreset preset) =>
            _presetPanel.DisplayPreset(preset);

        #endregion Methods: Calls to _presetPanel
        #region Methods: Calls to _researchTreeControl

        /// <summary> Gets all empty branches (their tabs should be disabled). </summary>
        /// <returns></returns>
        public IDictionary<ENation, IEnumerable<EBranch>> GetEmptyBranches() =>
            _researchTreeControl.GetEmptyBranches();

        /// <summary> Resets <see cref="UIElement.IsEnabled"/> statuses of nation and branch tabs of the research tree. </summary>
        public void ResetResearchTreeTabRestrictions() =>
            _researchTreeControl.ResetTabRestrictions();

        /// <summary> Disables all nation and branch tabs of the research tree not specified in the parameters. </summary>
        /// <param name="nation"> The nation tab to keep enabled. </param>
        /// <param name="branches"> Branch tabs to keep enabled. </param>
        public void EnableOnly(ENation nation, IEnumerable<EBranch> branches) =>
            _researchTreeControl.EnableOnly(nation, branches);

        /// <summary> Focuses on a research tree by given parameters. </summary>
        /// <param name="nation"> The nation whose <paramref name="branch"/> to put into focus. </param>
        /// <param name="branch"> The branch to put into focus. </param>
        public void FocusResearchTree(ENation nation, EBranch branch) =>
            _researchTreeControl.FocusResearchTree(nation, branch);

        /// <summary> Scrolls the research tree to bring the specified vehicle into view. </summary>
        /// <param name="vehicle"> The vehicle to bring into view. </param>
        public void BringIntoView(IVehicle vehicle) =>
            _researchTreeControl.BringIntoView(vehicle);

        /// <summary> Highlights the specified vehicle in the reseatch tree. </summary>
        /// <param name="vehicle"> The vehicle to highlight. </param>
        public void Highlight(IVehicle vehicle) =>
            _researchTreeControl.Highlight(vehicle);

        /// <summary> Removes the highlight from the specified vehicle in the reseatch tree. </summary>
        /// <param name="vehicle"> The vehicle to remove highlight from. </param>
        public void RemoveHighlight(IVehicle vehicle) =>
            _researchTreeControl.RemoveHighlight(vehicle);

        #endregion Methods: Calls to _researchTreeControl
    }
}