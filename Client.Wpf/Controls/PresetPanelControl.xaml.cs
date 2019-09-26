using Client.Wpf.Commands.Interfaces;
using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
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

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for PresetPanelControl.xaml. </summary>
    public partial class PresetPanelControl : LocalizedUserControl
    {
        #region Fields

        /// <summary> Preset panels on the control. </summary>
        private readonly IDictionary<EPreset, WrapPanel> _presetPanels;

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
        }

        #endregion Constructors

        /// <summary> Applies localization to visible text on the control. </summary>
        public override void Localize()
        {
            base.Localize();

            _swapPresetsButton.ToolTip = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.SwapPrimaryAndFallbackPresets);
            _deletePresetsButton.ToolTip = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.RemovePresets);
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
            _currentPresetInfo.Text = string.Empty;

            RaiseCanExecuteChanged();
        }

        /// <summary> Loads given presets. </summary>
        /// <param name="presets"> Presets to load. </param>
        /// <param name="presets"> The game mode for which to load presets. </param>
        public void LoadPresets(IDictionary<EPreset, Preset> presets, EGameMode gameMode)
        {
            foreach (var presetKeyValuePair in presets)
            {
                if (!_presetPanels.TryGetValue(presetKeyValuePair.Key, out var presetPanel))
                    continue;

                foreach (var vehicle in presetKeyValuePair.Value)
                {
                    var vehicleControl = new ResearchTreeCellVehicleControl(vehicle) { Margin = new Thickness(0, 0, 5, 0) };

                    vehicleControl.DisplayBattleRatingFor(gameMode);

                    presetPanel.Children.Add(vehicleControl);
                }
            }

            var firstVehicle = presets[EPreset.Primary].First();
            _currentPresetInfo.Text = $"{EReference.NationIcons[firstVehicle.Nation.AsEnumerationItem]}{EReference.BranchIcons[firstVehicle.Branch.AsEnumerationItem]} {firstVehicle.BattleRatingFormatted.AsDictionary()[gameMode]}";

            RaiseCanExecuteChanged();
        }

        /// <summary> Displays the specified preset. </summary>
        /// <param name="preset"> The preset to display. </param>
        public void DisplayPreset(EPreset preset)
        {
            if (!_presetPanels.TryGetValue(preset, out var presetPanel))
                return;

            presetPanel.Visibility = Visibility.Visible;

            foreach (var otherPresetPanel in _presetPanels.Values.Except(presetPanel))
                otherPresetPanel.Visibility = Visibility.Hidden;
        }
    }
}