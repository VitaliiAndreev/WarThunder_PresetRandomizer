using Client.Wpf.Controls.Base;
using Core.DataBase.WarThunder.Enumerations;
using Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for GameModeSelectionControl.xaml. </summary>
    public partial class GameModeSelectionControl : ToggleButtonGroupControlWithDropCap<EGameMode>
    {
        #region Constuctors

        /// <summary> Creates a new control. </summary>
        public GameModeSelectionControl()
        {
            InitializeComponent();

            _dropCapToggleButtons.AddRange
            (
                new Dictionary<EGameMode, DropCapToggleButton>
                {
                    { EGameMode.Arcade, _arcadeButton },
                    { EGameMode.Realistic, _realisticButton },
                    { EGameMode.Simulator, _simulatorButton },
                }
            );
            Buttons.AddRange
            (
                _dropCapToggleButtons.ToDictionary
                (
                    keyValuePair => keyValuePair.Key,
                    keyValuePair => keyValuePair.Value.EmbeddedButton
                )
            );

            foreach (var buttonKeyValuePair in _dropCapToggleButtons)
            {
                var gameMode = buttonKeyValuePair.Key;
                var button = buttonKeyValuePair.Value;

                button.Tag = gameMode;
            }
            foreach (var buttonKeyValuePair in Buttons)
            {
                var gameMode = buttonKeyValuePair.Key;
                var button = buttonKeyValuePair.Value;

                button.Tag = gameMode;
                button.Click += OnClick;
            }
        }

        #endregion Constuctors
        #region Methods: Event Handlers

        /// <summary> Adjust font weights to indicate the button pressed last. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="Button"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        public override void OnClick(object sender, RoutedEventArgs eventArguments)
        {
            if (!(sender is ToggleButton clickedButton))
            {
                return;
            }

            var buttons = Buttons.Values;
            var allButtonsAreToggledOff = buttons.All(button => !button.IsChecked.HasValue || !button.IsChecked.Value);

            if (allButtonsAreToggledOff)
            {
                clickedButton.IsChecked = true;
                return;
            }

            buttons
                .Except(new ToggleButton[] { clickedButton })
                .ToList()
                .ForEach(button => button.IsChecked = false)
            ;

            RaiseClickEvent(clickedButton);
        }

        #endregion Methods: Event Handlers
    }
}