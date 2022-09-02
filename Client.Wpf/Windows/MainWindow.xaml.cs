using Client.Shared.Wpf.Extensions;
using Client.Wpf.Commands.Interfaces;
using Client.Wpf.Controls;
using Client.Wpf.Controls.Base;
using Client.Wpf.Controls.Base.Interfaces;
using Client.Wpf.Enumerations;
using Client.Wpf.Enumerations.Logger;
using Client.Wpf.Extensions;
using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Windows.Interfaces;
using Client.Wpf.Windows.Interfaces.Base;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects.Connectors;
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

namespace Client.Wpf.Windows
{
    /// <summary> Interaction logic for MainWindow.xaml. </summary>
    public partial class MainWindow : BaseWindow, IMainWindow
    {
        #region Fields

        private readonly IGuiLoadingWindowPresenter _loadingTracker;

        /// <summary> Indicates whether the window is still being initialized. </summary>
        private readonly InitializationStatus _initializationStatus = InitializationStatus.NotInitialized;

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
        /// <param name="guiLoadingWindowPresenter"> An instance of a presenter to communicate with the GUI loading window. </param>
        public MainWindow(IMainWindowPresenter presenter, IGuiLoadingWindowPresenter guiLoadingWindowPresenter)
            : base(EWpfClientLogCategory.MainWindow, null, presenter)
        {
            _initializationStatus = InitializationStatus.Initializing;
            {
                Log.Trace(CoreLogMessage.Initialising);

                _loadingTracker = guiLoadingWindowPresenter;

                static string localise(string key) => ApplicationHelpers.LocalisationManager.GetLocalisedString(key);

                _loadingTracker.CurrentLoadingStage = localise(ELocalisationKey.CreatingPresetCollections);
                InitializeDictionaries();

                _loadingTracker.CurrentLoadingStage = localise(ELocalisationKey.CreatingControls);
                InitializeComponent();

                _loadingTracker.CurrentLoadingStage = localise(ELocalisationKey.LocalisingControls);
                Localise();

                _loadingTracker.CurrentLoadingStage = localise(ELocalisationKey.InitialisingControls);
                InitialiseControls();

                _loadingTracker.CurrentLoadingStage = localise(ELocalisationKey.ApplyingUserConfiguration);
                UpdateControlsBasedOnSettingsAndData();

                ToggleLongOperationIndicator(false);

                Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;

                Log.Debug(CoreLogMessage.Initialised);
            }
            _initializationStatus = InitializationStatus.Initialized;
        }

        #endregion Constructors
        #region Methods: Event Handlers

        /// <summary> Logs showing of the window. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnLoaded(object sender, RoutedEventArgs eventArguments) =>
            Log.Debug(CoreLogMessage.Shown);

        /// <summary> Logs closing of the window. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnClosed(object sender, EventArgs eventArguments)
        {
            Log.Debug(CoreLogMessage.Closed);
            ApplicationHelpers.Manager.Dispose();
        }

        /// <summary> Selects the randomisation method whose button is pressed. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="ToggleButton"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnRandomisationButtonClick(object sender, RoutedEventArgs eventArguments)
        {
            if (!(eventArguments.OriginalSource is ToggleButton toggleButton))
                return;

            Presenter.Randomisation = toggleButton.Tag.CastTo<ERandomisation>();
            Presenter.ExecuteCommand(ECommandName.SelectRandomisation);
        }

        /// <summary> Selects the specified game mode. </summary>
        /// <param name="gameMode"> The game mode to select. </param>
        /// <param name="simulateClick"> Whether to simulate a click on the appropriate button. </param>
        private void SelectGameMode(EGameMode gameMode, bool simulateClick = false)
        {
            if (simulateClick)
                _gameModeSelectionControl.OnClick(_gameModeSelectionControl.Buttons[gameMode], new RoutedEventArgs());

            if (Presenter.CurrentGameMode == gameMode && !simulateClick)
                return;

            _informationControl.ResearchTreeControl.DisplayVehicleInformation(gameMode);

            Presenter.CurrentGameMode = gameMode;
            Presenter.ExecuteCommand(ECommandName.SelectGameMode);
            Presenter.ExecuteCommand(ECommandName.DeletePresets);
        }

        /// <summary> Selects the game mode whose button is pressed. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="ToggleButton"/> is expected. </param>
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

        private void OnTogglableItemClicked<T>(Type keyType, T tag, bool isChecked, bool executeCommand)
        {
            if (!keyType.IsKeyIn(_presenterToggleLists) || !keyType.IsKeyIn(_toggleCommands))
                return;

            if (isChecked)
                _presenterToggleLists[keyType].CastTo<IList<T>>().Add(tag);
            else
                _presenterToggleLists[keyType].CastTo<IList<T>>().Remove(tag);

            if (executeCommand)
                ExecuteToggleCommand(keyType);
        }

        /// <summary> Updates a collection and executes a command associated with with the <typeparamref name="T"/> <see cref="FrameworkElement.Tag"/> of the <paramref name="toggleButton"/> according to its <see cref="ToggleButton.IsChecked"/> status. </summary>
        /// <param name="toggleButton"> The button that has been clicked. </param>
        private void OnToggleButtonGroupControlClick<T>(ToggleButton toggleButton, bool executeCommand = true)
        {
            var keyType = typeof(T);
            var buttonTag = toggleButton.GetTag<T>();

            OnTogglableItemClicked(keyType, buttonTag, toggleButton.IsChecked(), executeCommand);
        }

        /// <summary> Updates a collection and executes a command associated with with the <typeparamref name="T"/> <see cref="FrameworkElement.Tag"/> of the <paramref name="menuItem"/> according to its <see cref="ToggleButton.IsChecked"/> status. </summary>
        /// <param name="menuItem"> The menu item that has been clicked. </param>
        private void OnMenuItemClicked<T>(MenuItem menuItem, bool executeCommand = true)
        {
            var keyType = typeof(T);
            var menuItemTag = menuItem.GetTag<T>();

            OnTogglableItemClicked(keyType, menuItemTag, menuItem.IsChecked, executeCommand);
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

                _vehicleClassControl.Enable(branch, toggleButton.IsChecked());

                if (toggleButton.IsChecked() && !Presenter.BranchHasVehicleClassesEnabled(branch))
                {
                    var vehicleClass = branch.GetVehicleClasses().First();

                    _vehicleClassControl.Toggle(vehicleClass, true);
                    OnVehicleClassToggleControlClick(_vehicleClassControl, new RoutedEventArgs(VehicleClassToggleControl.ClickEvent, _vehicleClassControl.ToggleColumns[branch].Buttons[vehicleClass]));
                }

                UpdateChildControlContextMenu(_branchToggleControl, branch, OnVehicleBranchTagToggled);
                UpdateToggleAllButtonState(_vehicleClassControl, branch);
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
            var toggleColumn = toggleControl.ToggleColumns[ownerEntity];
            var toggleButtonTagType = typeof(U);
            var toggleAllOn = toggleColumn.AllButtonsAreToggledOn() && toggleColumn.AllContextMenuItemsAreToggledOn();

            if (toggleButtonTagType.IsEnum)
            {
                if (toggleButtonTagType.GetEnumValues().OfType<U>().FirstOrDefault(item => item.ToString() == Word.All) is U toggleButtonKey)
                    toggleColumn.Toggle(toggleButtonKey, toggleAllOn);
            }
            else
            {
                if (toggleButtonTagType == typeof(NationCountryPair) && ownerEntity is ENation nation)
                    toggleColumn.Toggle(new NationCountryPair(nation, nation.GetAllCountriesItem()).CastTo<U>(), toggleAllOn);

                else
                    throw new NotImplementedException(EWpfClientLogMessage.TagTypeNotSupportedYet.Format(toggleButtonTagType.ToStringLikeCode()));
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

                if (vehicleClass.ToString().StartsWith(Word.All))
                {
                    var toggleAllButton = toggleButton;
                    var buttons = _vehicleClassControl.ToggleColumns[ownerBranch].Buttons.Values;

                    foreach (var button in buttons.Where(button => button.GetTag<EVehicleClass>().IsValid()))
                    {
                        var currentVehicleClass = button.Tag.CastTo<EVehicleClass>();

                        if (button.IsChecked() != toggleAllButton.IsChecked())
                        {
                            _vehicleClassControl.Toggle(currentVehicleClass, toggleAllButton.IsChecked());
                            OnToggleButtonGroupControlClick<EVehicleClass>(button, false);
                        }

                        if (toggleAllButton.IsChecked())
                            ToggleAllContextMenuItems<EVehicleSubclass>(_vehicleClassControl.ToggleColumns[ownerBranch].Buttons[currentVehicleClass], OnTogglableItemClicked);
                        else
                            button.SetContextMenuState(toggleAllButton.IsChecked());
                    }

                    ExecuteToggleCommand(typeof(EVehicleClass));
                }
                else
                {
                    OnToggleButtonGroupControlClick<EVehicleClass>(toggleButton);
                    UpdateToggleAllButtonState(_vehicleClassControl, ownerBranch);
                }

                UpdateOwnerControl(_branchToggleControl, Presenter.BranchHasVehicleClassesEnabled, ownerBranch, OnBranchToggleControlClick);
                UpdateChildControlContextMenu(_vehicleClassControl.ToggleColumns[ownerBranch], vehicleClass, OnVehicleSubclassToggled);
                RaiseGeneratePresetCommandCanExecuteChanged();
            }
        }

        /// <summary> Updates <see cref="IMainWindowPresenter.EnabledVehicleBranchTags"/> according to the action. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="MenuItem"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnVehicleBranchTagToggled(object sender, RoutedEventArgs eventArguments)
        {
            if (eventArguments.OriginalSource is MenuItem menuItem)
            {
                var vehicleBranchTag = menuItem.GetTag<EVehicleBranchTag>();
                var ownerBranch = vehicleBranchTag.GetBranch();

                OnMenuItemClicked<EVehicleBranchTag>(menuItem);
                _branchToggleControl.UpdateContextMenuItemCount(ownerBranch);
                UpdateOwnerControl(_branchToggleControl, Presenter.BranchHasVehicleBranchTagsEnabled, ownerBranch, OnBranchToggleControlClick);
                RaiseGeneratePresetCommandCanExecuteChanged();
            }
        }

        /// <summary> Updates <see cref="IMainWindowPresenter.EnabledVehicleSubclasses"/> according to the action. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="MenuItem"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnVehicleSubclassToggled(object sender, RoutedEventArgs eventArguments)
        {
            if (eventArguments.OriginalSource is MenuItem menuItem)
            {
                var vehicleSubclass = menuItem.GetTag<EVehicleSubclass>();
                var ownerClass = vehicleSubclass.GetVehicleClass();
                var ownerBranch = ownerClass.GetBranch();

                OnMenuItemClicked<EVehicleSubclass>(menuItem);
                _vehicleClassControl.UpdateContextMenuItemCount(ownerBranch, ownerClass);
                UpdateOwnerControl(_vehicleClassControl.ToggleColumns[ownerBranch], Presenter.VehicleClassHasSubclassesEnabled, ownerClass, OnVehicleClassToggleControlClick);
                UpdateToggleAllButtonState(_vehicleClassControl, ownerBranch);
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
                _countryToggleControl.Enable(nation, toggleButton.IsChecked());
                _battleRatingControl.Enable(nation, toggleButton.IsChecked());

                if (toggleButton.IsChecked() && !Presenter.NationHasCountriesEnabled(nation))
                {
                    var country = nation.GetCountries().First();
                    var nationCountryPair = new NationCountryPair(nation, country);

                    _countryToggleControl.Toggle(nationCountryPair, true);
                    OnCountryToggleControlClick(_countryToggleControl, new RoutedEventArgs(CountryToggleControl.ClickEvent, _countryToggleControl.ToggleColumns[nation].Buttons[nationCountryPair]));
                }

                UpdateToggleAllButtonState(_countryToggleControl, nation);
                RaiseGeneratePresetCommandCanExecuteChanged();
            }
        }

        private void UpdateOwnerControl<T>(ToggleButtonGroupControl<T> ownerControl, T key, RoutedEventHandler eventHandler, bool newState)
        {
            ownerControl.Toggle(key, newState);
            eventHandler(ownerControl, new RoutedEventArgs(ownerControl.ClickEventReference, ownerControl.Buttons[key]));
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
                UpdateOwnerControl(ownerControl, key, eventHandler, false);
            }
            else
            {
                if (!ownerControl.Buttons[key].IsChecked())
                    UpdateOwnerControl(ownerControl, key, eventHandler, true);
            }
        }

        /// <summary> Updates <paramref name="ownerControl"/>'s child control (accessed by <paramref name="childControlTag"/>). </summary>
        /// <typeparam name="T"> The tag type. </typeparam>
        /// <param name="ownerControl"> The owner control whose child control to update. </param>
        /// <param name="childControlTag"> The tag of the child control to update. </param>
        /// <param name="eventHandler"> The method to call after updating to control. </param>
        private void UpdateChildControlContextMenu<T>(ToggleButtonGroupControl<T> ownerControl, T childControlTag, RoutedEventHandler eventHandler)
        {
            if (!ownerControl.Buttons.TryGetValue(childControlTag, out var childControl) || !(childControl.ContextMenu is ContextMenu contextMenu))
                return;

            if (!childControl.IsChecked())
            {
                contextMenu.Deactivate();
            }
            else
            {
                contextMenu.Activate();

                if (!contextMenu.HasCheckedItems())
                {
                    var firstMenuItem = contextMenu.GetMenuItems().FirstOrDefault();

                    firstMenuItem.IsChecked = true;
                    eventHandler(firstMenuItem, new RoutedEventArgs(null, firstMenuItem));
                }
            }
        }

        private void ToggleAllContextMenuItems<T>(ToggleButton ownerControl, Action<Type, T, bool, bool> eventHandler)
        {
            if (!(ownerControl.ContextMenu is ContextMenu contextMenu))
                return;

            if (ownerControl.IsChecked())
            {
                contextMenu.Activate();

                foreach (var menuItem in contextMenu.GetTogglableMenuItems())
                {
                    if (!menuItem.IsChecked)
                    {
                        var menuItemTag = menuItem.Tag.CastTo<T>();

                        menuItem.IsChecked = true;
                        eventHandler?.Invoke(typeof(T), menuItemTag, menuItem.IsChecked, true);
                    }
                }
            }
            else
            {
                contextMenu.Deactivate();
            }
            ownerControl.UpdateContextMenuItemCount();
        }

        /// <summary> Updates <see cref="IMainWindowPresenter.EnabledCountries"/> according to the action. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Event arguments. </param>
        private void OnCountryToggleControlClick(object sender, RoutedEventArgs eventArguments)
        {
            if (eventArguments.OriginalSource is ToggleButton toggleButton)
            {
                var nationCountryPair = toggleButton.GetTag<NationCountryPair>();
                var country = nationCountryPair.Country;
                var nation = nationCountryPair.Nation;

                if (country.ToString().StartsWith(Word.All))
                {
                    var disabledButtons = _countryToggleControl.GetButtons(nation, !toggleButton.IsChecked(), false);

                    foreach (var button in disabledButtons)
                    {
                        _countryToggleControl.Toggle(button.Tag.CastTo<NationCountryPair>(), toggleButton.IsChecked());
                        OnToggleButtonGroupControlClick<NationCountryPair>(button, false);
                    }

                    ExecuteToggleCommand(typeof(NationCountryPair));
                }
                else
                {
                    OnToggleButtonGroupControlClick<NationCountryPair>(toggleButton);
                    UpdateToggleAllButtonState(_countryToggleControl, nation);
                }

                UpdateOwnerControl(_nationToggleControl, Presenter.NationHasCountriesEnabled, nation, OnNationToggleControlClick);
                RaiseGeneratePresetCommandCanExecuteChanged();
            }
        }

        /// <summary> Updates <see cref="IMainWindowPresenter.EnabledRanks"/> according to the action. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Event arguments. </param>
        private void OnRankToggleControlClick(object sender, RoutedEventArgs eventArguments)
        {
            if (eventArguments.OriginalSource is ToggleButton toggleButton)
            {
                OnToggleButtonGroupControlClick<ERank>(toggleButton);
                RaiseGeneratePresetCommandCanExecuteChanged();
            }
        }

        /// <summary> Updates <see cref="IMainWindowPresenter.EnabledEconomicRankIntervals"/> and executes the <see cref="ECommandName.ChangeBattleRating"/> command. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnBattleRatingValueChanged(object sender, RoutedEventArgs eventArguments)
        {
            if (_initializationStatus != InitializationStatus.Initialized)
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
                _informationControl.ResearchTreeControl.Highlight(vehicleControl.Vehicle);
        }

        /// <summary> Applies the idle style to the vehicle's conterpart in the research tree. </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="eventArguments"> Event arguments. </param>
        private void OnPresetVehicleMouseLeave(object sender, MouseEventArgs eventArguments)
        {
            if (eventArguments.OriginalSource is ResearchTreeCellVehicleControl vehicleControl)
                _informationControl.ResearchTreeControl.RemoveHighlight(vehicleControl.Vehicle);
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
                    _informationControl.BringIntoView(vehicleControl.Vehicle, true);
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
        #region Methods: Event Raisers

        /// <summary> Checks whether it is possible to generate presets. </summary>
        private void RaiseGeneratePresetCommandCanExecuteChanged() =>
            (_generatePresetButton.Command as ICustomCommand)?.RaiseCanExecuteChanged(Presenter);

        #endregion Methods: Event Raisers
        #region Methods: Initialisation

        /// <summary> Initialized dictionaries. </summary>
        private void InitializeDictionaries()
        {
            _presenterToggleLists = new Dictionary<Type, object>
            {
                { typeof(EBranch), Presenter.EnabledBranches },
                { typeof(EVehicleBranchTag), Presenter.EnabledVehicleBranchTags },
                { typeof(EVehicleClass), Presenter.EnabledVehicleClasses },
                { typeof(EVehicleSubclass), Presenter.EnabledVehicleSubclasses },
                { typeof(ENation), Presenter.EnabledNations },
                { typeof(ERank), Presenter.EnabledRanks },
                { typeof(NationCountryPair), Presenter.EnabledCountries },
            };
            _toggleCommands = new Dictionary<Type, ECommandName>
            {
                { typeof(EBranch), ECommandName.ToggleBranch },
                { typeof(EVehicleBranchTag), ECommandName.ToggleVehicleBranchTag },
                { typeof(EVehicleClass), ECommandName.ToggleVehicleClass },
                { typeof(EVehicleSubclass), ECommandName.ToggleVehicleSubclass },
                { typeof(ENation), ECommandName.ToggleNation },
                { typeof(ERank), ECommandName.ToggleRank },
                { typeof(NationCountryPair), ECommandName.ToggleCountry },
            };
        }

        private void InitialiseControls()
        {
            _presetPanel.Initialise(Presenter);
            _informationControl.Initialise(Presenter);

            SetControlTags();
            AttachCommands();

            _loadingTracker.CurrentLoadingStage = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.PopulatingControls);
            PopulateControls();
        }

        /// <summary> Sets control tags. </summary>
        private void SetControlTags()
        {
            _gameModeSelectionControl.Tag = EGameMode.None;

            _branchToggleControl.Tag = EBranch.None;
            _vehicleClassControl.Tag = EVehicleClass.None;

            _nationToggleControl.Tag = ENation.None;
            _countryToggleControl.Tag = ECountry.None;

            _battleRatingControl.Tag = $"{Word.Battle} {Word.Rating}";
        }

        private void AttachCommands()
        {
            _generatePresetButton.CommandParameter = Presenter;
            _generatePresetButton.Command = Presenter.GetCommand(ECommandName.GeneratePreset);

            _settingsButton.CommandParameter = Presenter;
            _settingsButton.Command = Presenter.GetCommand(ECommandName.OpenSettings);

            _localizationButton.CommandParameter = Presenter;
            _localizationButton.Command = Presenter.GetCommand(ECommandName.ChangeLocalization);

            _youTubeButton.CommandParameter = Presenter;
            _youTubeButton.Command = Presenter.GetCommand(ECommandName.LinkToYouTube);

            _aboutButton.CommandParameter = Presenter;
            _aboutButton.Command = Presenter.GetCommand(ECommandName.About);

            _presetPanel.AttachCommands(Presenter.GetCommand(ECommandName.SwapPresets), Presenter.GetCommand(ECommandName.DeletePresets), Presenter);
        }

        /// <summary> Populates controls after initialization. </summary>
        private void PopulateControls()
        {
            _localizationButton.Content = new Image()
            {
                Style = this.GetStyle(EStyleKey.Image.LocalizationIcon),
                Source = this.GetBitmapSource(WpfSettings.LocalizationLanguage.GetFlagResourceKey()),
            };

            _loadingTracker.CurrentLoadingStage = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.PopulatingResearchTreeControls);
            _informationControl.ResearchTreeControl.Populate(Presenter.EnabledVehicleGaijinIds, _loadingTracker);
        }

        /// <summary> Updates controls on the window base on saved user settings and loaded data. </summary>
        private void UpdateControlsBasedOnSettingsAndData()
        {
            SelectGameMode(WpfSettings.CurrentGameModeAsEnumerationItem, true);

            _nationToggleControl.RemoveUnavailableNations();
            _countryToggleControl.RemoveCountriesForUnavailableNations();
            _battleRatingControl.RemoveControlsForUnavailableNations();

            _randomisationSelectionControl.Toggle(Presenter.Randomisation, true);
            _nationToggleControl.Toggle(Presenter.EnabledNations, true);
            _countryToggleControl.Toggle(Presenter.EnabledCountries, true);

            _branchToggleControl.Toggle(Presenter.EnabledBranches, true);
            _branchToggleControl.Toggle(Presenter.EnabledVehicleBranchTags, true);
            _branchToggleControl.UpdateContextMenuItemCount();

            _vehicleClassControl.Toggle(Presenter.EnabledVehicleClasses, true);
            _vehicleClassControl.Toggle(Presenter.EnabledVehicleSubclasses, true);
            _vehicleClassControl.UpdateContextMenuItemCount();

            _rankToggleControl.Toggle(Presenter.EnabledRanks, true);

            AdjustBranchTogglesAvailability();
            AdjustChildControlContextMenuVisibility(_vehicleClassControl, OnVehicleSubclassToggled);
            AdjustCountryControlsAvailability();

            _battleRatingControl.InitializeControls();
            AdjustBattleRatingControlsAvailability();

            foreach (var branch in Presenter.EnabledBranches)
                UpdateToggleAllButtonState(_vehicleClassControl, branch);

            foreach (var nation in Presenter.EnabledNations)
                UpdateToggleAllButtonState(_countryToggleControl, nation);

            RaiseGeneratePresetCommandCanExecuteChanged();
        }

        #endregion Methods: Initialisation
        #region Methods: Overrides

        /// <summary> Applies localization to visible text in the window. </summary>
        public override void Localise()
        {
            base.Localise();

            Title = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.ApplicationName);

            _generatePresetButton.Content = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.GeneratePreset);
            _youTubeButton.ToolTip = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.OpenYouTubePlaylist);
            _statusBarMessage.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.OperationUnderwayPleaseWait);

            _randomisationSelectionControl.Localise();
            _gameModeSelectionControl.Localise();
            _branchToggleControl.Localise();
            _vehicleClassControl.Localise();
            _nationToggleControl.Localise();
            _countryToggleControl.Localise();
            _battleRatingControl.Localise();

            _presetPanel.Localise();
            _informationControl.Localise();
        }

        #endregion Methods: Overrides
        #region Methods: Status Bar

        public void ToggleLongOperationIndicator(bool show)
        {
            _statusBarIcon.Visibility = show ? Visibility.Visible : Visibility.Hidden;
            _statusBarMessage.Visibility = show ? Visibility.Visible : Visibility.Hidden;

            ApplicationHelpers.ProcessUiTasks();
        }

        #endregion Methods: Status Bar
        #region Methods: Adjusting Availability

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

        #endregion Methods: Adjusting Availability
        #region Methods: Adjusting Visibility

        #region Methods: Adjusting Branch Toggle Availability

        /// <summary> Enables or disables branch toggles depending on whether any of <see cref="IMainWindowPresenter.EnabledNations"/> have associated branches implemented.</summary>
        private void AdjustBranchTogglesAvailability()
        {
            var allBranches = typeof(EBranch).GetEnumValues().Cast<EBranch>().Where(branch => branch.IsValid());
            var validBranches = Presenter.GetValidBraches();

            foreach (var branch in allBranches)
            {
                var enabled = branch.IsIn(validBranches);

                if (branch.GetVehicleCategory() == EVehicleCategory.Fleet)
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
        /// <param name="enable"> Whether to enable the toggle or not. </param>
        private void AdjustBranchToggleAvailability(EBranch branch, bool enable)
        {
            if (!enable)
            {
                Presenter.EnabledBranches.Remove(branch);

                _branchToggleControl.Toggle(branch, false);
            }

            _branchToggleControl.Enable(branch, enable);
            _vehicleClassControl.Enable(branch, enable && Presenter.EnabledBranches.Contains(branch));

            UpdateChildControlContextMenu(_branchToggleControl, branch, OnVehicleBranchTagToggled);

            Presenter.ExecuteCommand(ECommandName.ToggleBranch);
        }

        /// <summary> Enables or disables the fleet toggle depending on the specified <paramref name="gameMode"/>. </summary>
        /// <param name="gameMode"> The game mode to adjust for. </param>
        /// <param name="validBranches"> A collection of valid branches. </param>
        private void AdjustFleetAvailability(EGameMode gameMode, IEnumerable<EBranch> validBranches = null)
        {
            void adjustAvailability(EBranch branch) => AdjustBranchToggleAvailability(branch, gameMode != EGameMode.Simulator && branch.IsIn(validBranches is null ? Presenter.GetValidBraches() : validBranches));

            adjustAvailability(EBranch.BluewaterFleet);
            adjustAvailability(EBranch.CoastalFleet);
        }

        #endregion Methods: Adjusting Branch Toggle Availability

        /// <summary> Adjusts availability of context menus of <see cref="ToggleButtonGroupControl{T}.Buttons"/> in <paramref name="toggleButtonGroupControl"/>. </summary>
        /// <typeparam name="T"> The outer key type. </typeparam>
        /// <typeparam name="U"> The inner key type. </typeparam>
        /// <param name="toggleButtonGroupControl"> The toggle button group control, whose buttons' context menu to update. </param>
        /// <param name="eventHandler"> The method to call after updating to control. </param>
        private void AdjustChildControlContextMenuVisibility<T, U>(ColumnToggleGroupControl<T, U> toggleButtonGroupControl, RoutedEventHandler eventHandler)
        {
            foreach (var toggleButtonGroup in toggleButtonGroupControl.ToggleColumns.Values)
            {
                foreach (var indexedButton in toggleButtonGroup.Buttons)
                {
                    var tag = indexedButton.Key;
                    var button = indexedButton.Value;

                    UpdateChildControlContextMenu(toggleButtonGroup, tag, eventHandler);
                }
            }
        }

        #endregion Methods: Adjusting Visibility
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
            _informationControl.ResearchTreeControl.GetEmptyBranches();

        /// <summary> Resets <see cref="UIElement.IsEnabled"/> statuses of nation and branch tabs of the research tree. </summary>
        public void ResetResearchTreeTabRestrictions() =>
            _informationControl.ResearchTreeControl.ResetTabRestrictions();

        /// <summary> Disables all nation and branch tabs of the research tree not specified in the parameters. </summary>
        /// <param name="nation"> The nation tab to keep enabled. </param>
        /// <param name="branches"> Branch tabs to keep enabled. </param>
        public void EnableOnly(ENation nation, IEnumerable<EBranch> branches) =>
            _informationControl.ResearchTreeControl.EnableOnly(nation, branches);

        /// <summary> Focuses on a research tree by given parameters. </summary>
        /// <param name="nation"> The nation whose <paramref name="branch"/> to put into focus. </param>
        /// <param name="branch"> The branch to put into focus. </param>
        public void FocusResearchTree(ENation nation, EBranch branch) =>
            _informationControl.ResearchTreeControl.FocusResearchTree(nation, branch);

        /// <summary> Scrolls the research tree to bring the specified vehicle into view. </summary>
        /// <param name="vehicle"> The vehicle to bring into view. </param>
        public void BringIntoView(IVehicle vehicle) =>
            _informationControl.BringIntoView(vehicle, true);

        /// <summary> Highlights the specified vehicle in the reseatch tree. </summary>
        /// <param name="vehicle"> The vehicle to highlight. </param>
        public void Highlight(IVehicle vehicle) =>
            _informationControl.ResearchTreeControl.Highlight(vehicle);

        /// <summary> Removes the highlight from the specified vehicle in the reseatch tree. </summary>
        /// <param name="vehicle"> The vehicle to remove highlight from. </param>
        public void RemoveHighlight(IVehicle vehicle) =>
            _informationControl.ResearchTreeControl.RemoveHighlight(vehicle);

        #endregion Methods: Calls to _researchTreeControl
    }
}