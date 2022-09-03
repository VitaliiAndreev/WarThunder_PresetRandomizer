using Client.Shared.Wpf.Extensions;
using Client.Wpf.Commands.Interfaces;
using Client.Wpf.Controls.Base;
using Client.Wpf.Controls.Strategies;
using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.Extensions;
using Core.Organization.Collections;
using Core.Organization.Enumerations;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for PresetPanelControl.xaml. </summary>
    public partial class PresetPanelControl : LocalisedUserControl
    {
        #region Fields

        /// <summary> Preset panels on the control. </summary>
        private readonly IDictionary<EPreset, WrapPanel> _presetPanels;
        private readonly IDictionary<EPreset, Grid> _usageStatisticsGrids;

        private readonly IDictionary<string, ResearchTreeCellVehicleControl> _vehicleCards;

        private bool _initialised;

        private IMainWindowPresenter _presenter;

        #endregion Fields
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public PresetPanelControl()
        {
            InitializeComponent();

            _primaryPresetPanel.Tag = EPreset.Primary;
            _fallbackPresetPanel.Tag = EPreset.Fallback;

            _presetPanels = new Dictionary<EPreset, WrapPanel>
            {
                { EPreset.Primary, _primaryPresetPanel },
                { EPreset.Fallback, _fallbackPresetPanel },
            };
            _usageStatisticsGrids = new Dictionary<EPreset, Grid>
            {
                { EPreset.Primary, _primaryPresetUsageStatisticsGrid },
                { EPreset.Fallback, _fallbackPresetUsageStatisticsGrid },
            };
            _vehicleCards = new Dictionary<string, ResearchTreeCellVehicleControl>();
        }

        #endregion Constructors
        #region Methods: Initialisation

        public void Initialise(IMainWindowPresenter presenter)
        {
            if (!_initialised)
            {
                _presenter = presenter;
                _initialised = true;
            }
        }

        #endregion Methods: Initialisation
        #region Methods: Event Handlers

        /// <summary> Raises the <see cref="UIElement.MouseEnterEvent"/> for one of the vehicle cards. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="ResearchTreeCellVehicleControl"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnMouseEnter(object sender, RoutedEventArgs eventArguments)
        {
            if (sender is ResearchTreeCellVehicleControl vehicleControl)
                RaiseMouseEnterEvent(vehicleControl);
        }

        /// <summary> Raises the <see cref="UIElement.MouseLeaveEvent"/> for one of the vehicle cards. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="ResearchTreeCellVehicleControl"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnMouseLeave(object sender, RoutedEventArgs eventArguments)
        {
            if (sender is ResearchTreeCellVehicleControl vehicleControl)
                RaiseMouseLeaveEvent(vehicleControl);
        }

        #endregion Methods: Event Handlers
        #region Methods: Event Raisers

        private MouseEventArgs GetMouseEventArgs(RoutedEvent routedEvent, ResearchTreeCellVehicleControl vehicleControl) =>
            new MouseEventArgs(Mouse.PrimaryDevice, timestamp: 0) { RoutedEvent = routedEvent, Source = vehicleControl };

        /// <summary> Raises the <see cref="UIElement.MouseEnterEvent"/> for the given vehicle control. </summary>
        /// <param name="vehicleControl"> The vehicle control to raise the event for. </param>
        private void RaiseMouseEnterEvent(ResearchTreeCellVehicleControl vehicleControl) =>
            RaiseEvent(GetMouseEventArgs(Mouse.MouseEnterEvent, vehicleControl));

        /// <summary> Raises the <see cref="UIElement.MouseEnterEvent"/> for the given vehicle control. </summary>
        /// <param name="vehicleControl"> The vehicle control to raise the event for. </param>
        private void RaiseMouseLeaveEvent(ResearchTreeCellVehicleControl vehicleControl) =>
            RaiseEvent(GetMouseEventArgs(Mouse.MouseLeaveEvent, vehicleControl));

        #endregion Methods: Event Raisers

        /// <summary> Applies localisation to visible text on the control. </summary>
        public override void Localise()
        {
            base.Localise();

            _swapPresetsButton.ToolTip = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.SwapPrimaryAndFallbackPresets);
            _deletePresetsButton.ToolTip = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.RemovePresets);
        }

        /// <summary> Attaches specified commands to buttons on the control. </summary>
        /// <param name="swapPresetsCommand"> The command for swapping primary and fallback presets. </param>
        /// <param name="deletePresetsCommand"> The command for deleting generated presets. </param>
        /// <param name="commandParameter"> The command parameter. </param>
        public void AttachCommands(ICommand swapPresetsCommand, ICommand deletePresetsCommand, object commandParameter)
        {
            _swapPresetsButton.Command = swapPresetsCommand;
            _swapPresetsButton.CommandParameter = commandParameter;

            _deletePresetsButton.Command = deletePresetsCommand;
            _deletePresetsButton.CommandParameter = commandParameter;
        }

        /// <summary> Prompts button commands to check if they can be executed. </summary>
        private void RaiseCanExecuteChanged()
        {
            static void raiseCanExecuteChanged(Button button)
            {
                if (button.Command is ICustomCommand command)
                    command.RaiseCanExecuteChanged(button.CommandTarget);
            }

            raiseCanExecuteChanged(_swapPresetsButton);
            raiseCanExecuteChanged(_deletePresetsButton);
        }

        /// <summary> Resets controls to their default states. </summary>
        public void ResetControls()
        {
            _primaryPresetPanel.Children.Clear();
            _fallbackPresetPanel.Children.Clear();

            _primaryPresetUsageStatisticsGrid.Children.Clear();
            _fallbackPresetUsageStatisticsGrid.Children.Clear();

            _currentPresetNationFlag.Source = null;
            _currentPresetInfoTextBlock.Text = string.Empty;

            RaiseCanExecuteChanged();
        }

        /// <summary> Loads given presets. </summary>
        /// <param name="presets"> Presets to load. </param>
        /// <param name="presets"> The game mode for which to load presets. </param>
        public void LoadPresets(IDictionary<EPreset, Preset> presets, EGameMode gameMode)
        {
            foreach (var presetKeyValuePair in presets)
            {
                if (!_presetPanels.TryGetValue(presetKeyValuePair.Key, out var presetPanel) || !_usageStatisticsGrids.TryGetValue(presetKeyValuePair.Key, out var usageStatisticsGrid))
                    continue;

                var preset = presetKeyValuePair.Value;

                foreach (var vehicle in preset)
                {
                    var vehicleControl = default(ResearchTreeCellVehicleControl);

                    if (_vehicleCards.TryGetValue(vehicle.GaijinId, out var alreadyCreatedVehicleControl))
                    {
                        vehicleControl = alreadyCreatedVehicleControl;
                    }
                    else
                    {
                        vehicleControl = new ResearchTreeCellVehicleControl(_presenter, vehicle, new DisplayVehicleInformationStandaloneStrategy(), EVehicleCard.Preset, true) { Margin = new Thickness(0, 0, 5, 0) };

                        vehicleControl.MouseEnter += OnMouseEnter;
                        vehicleControl.MouseLeave += OnMouseLeave;
                        vehicleControl.UpdateFor(gameMode);

                        _vehicleCards.Add(vehicle.GaijinId, vehicleControl);
                    }

                    presetPanel.Children.Add(vehicleControl);
                }

                usageStatisticsGrid.Children.Add(new BattleRatingUsageControl().Reset().Populate(preset).Localise());
            }

            var primaryPreset = presets[EPreset.Primary];

            _currentPresetNationFlag.Source = Application.Current.MainWindow.FindResource(EReference.CountryIconKeys[primaryPreset.Nation.GetBaseCountry()]) as ImageSource;
            _currentPresetInfoTextBlock.Text = $"{EReference.BranchIcons[primaryPreset.MainBranch]} {primaryPreset.BattleRating}";

            RaiseCanExecuteChanged();
        }

        private void DisplayText(string text)
        {
            foreach (var presetPanel in _presetPanels.Values)
            {
                var textBlock = new TextBlock()
                {
                    Style = this.GetStyle(EStyleKey.TextBlock.TextBlockFontSize16),
                    Margin = new Thickness(0, 5, 0, 0),
                    Text = text,
                };
                presetPanel.Children.Add(textBlock);
            }
        }

        /// <summary> Displays the message that no vehicles suit the criteria. </summary>
        public void ShowNoResults() =>
            DisplayText(ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.SomethingWentWrongNoVehiclesFitTheCriteria));

        /// <summary> Displays a message that no vehicles suit the criteria with additional information. </summary>
        /// <param name="nation"> The nation. </param>
        /// <param name="mainBranch"> The branch. </param>
        public void ShowNoVehicles(ENation nation, EBranch mainBranch) =>
            DisplayText
            (
                ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.NoVehiclesAvailableWithinSpecifiedParameters)
                    .Format(ApplicationHelpers.LocalisationManager.GetLocalisedString(mainBranch.ToString()), ApplicationHelpers.LocalisationManager.GetLocalisedString(nation.ToString()))
            );

        /// <summary> Displays the specified preset. </summary>
        /// <param name="preset"> The preset to display. </param>
        public void DisplayPreset(EPreset preset)
        {
            if (!_presetPanels.TryGetValue(preset, out var presetPanel) || !_usageStatisticsGrids.TryGetValue(preset, out var usageStatisticsGrid))
                return;

            presetPanel.Visibility = Visibility.Visible;
            usageStatisticsGrid.Visibility = Visibility.Visible;

            foreach (var otherPresetPanel in _presetPanels.Values.Except(presetPanel))
                otherPresetPanel.Visibility = Visibility.Hidden;

            foreach (var otherusageStatisticsGrid in _usageStatisticsGrids.Values.Except(usageStatisticsGrid))
                otherusageStatisticsGrid.Visibility = Visibility.Hidden;
        }
    }
}