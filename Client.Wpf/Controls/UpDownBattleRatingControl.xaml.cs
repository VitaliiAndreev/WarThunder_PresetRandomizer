using Client.Wpf.Controls.Base;
using Core.DataBase.WarThunder.Helpers;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.VehicleGameModeParameterSet.String;
using Core.Enumerations;
using Core.Extensions;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for UpDownBattleRatingControl.xaml. </summary>
    public partial class UpDownBattleRatingControl : UpDownIntegerControl
    {
        #region Properties

        /// <summary> The collection of definitions of rows containing arrow <see cref="Button"/>s and the <see cref="TextBlock"/>. </summary>
        private RowDefinitionCollection RowDefinitions => _grid.RowDefinitions;

        /// <summary> The height of arrow <see cref="Button"/>s. </summary>
        public double ArrowButtonHeight
        {
            get => RowDefinitions.First().Height.Value;
            set
            {
                var newHeight = new GridLength(value, GridUnitType.Pixel);

                _grid.RowDefinitions.First().Height = newHeight;
                _grid.RowDefinitions.Last().Height = newHeight;
            }
        }

        /// <summary> The height of the <see cref="TextBlock"/>. </summary>
        public double TextBlockHeight
        {
            get => RowDefinitions[1].Height.Value;
            set => RowDefinitions[1].Height = new GridLength(value, GridUnitType.Pixel);
        }

        /// <summary> The current integer. </summary>
        public override int Value
        {
            get => _currentValue;
            set
            {
                if (value == _currentValue)
                    return;

                _currentValue = value;
                _textBlock.Text = GetFormattedBattleRating(_currentValue);
                RaiseValueChanged();
            }
        }

        #endregion Properties
        #region Events

        /// <summary> A routed event for <see cref="ValueChanged"/>. </summary>
        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UpDownBattleRatingControl));

        /// <summary> Occurs when <see cref="Value"/> changes. </summary>
        public event RoutedEventHandler ValueChanged;

        #endregion Events
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public UpDownBattleRatingControl()
        {
            InitializeComponent();

            _upButton.Tag = EDirection.Up;
            _downButton.Tag = EDirection.Down;

            _textBlock.Text = GetFormattedBattleRating(Value);
        }

        #endregion Constructors
        #region Methods: Event Handlers

        /// <summary> Adjusts <see cref="Value"/> based on which button was pressed. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="Button"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnButtonClick(object sender, RoutedEventArgs eventArguments)
        {
            if (sender is Button button && button.Tag is EDirection direction)
                AdjustValue(direction);
        }

        /// <summary> Adjusts <see cref="Value"/> based on which direction the wheel was rotated. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="Button"/> is expected. </param>
        /// <param name="eventArguments"> Event arguments. </param>
        private void OnMouseWheel(object sender, MouseWheelEventArgs eventArguments)
        {
            if (eventArguments.Delta.IsPositive())
                AdjustValue(EDirection.Up);

            else if (eventArguments.Delta.IsNegative())
                AdjustValue(EDirection.Down);
        }

        #endregion Methods: Event Handlers

        /// <summary> Raises the <see cref="ValueChangedEvent"/> event. </summary>
        public void RaiseValueChanged() =>
            RaiseEvent(new RoutedEventArgs(ValueChangedEvent, this));

        /// <summary> Get a formatted <see cref="IVehicle.BattleRating"/> string from the given <see cref="IVehicle.EconomicRank"/>. </summary>
        /// <param name="economicRank"> The economic rank to convert into a battle rating. </param>
        /// <returns></returns>
        private string GetFormattedBattleRating(int economicRank) =>
            Calculator.GetBattleRating(economicRank).ToString(BattleRating.Format);
    }
}