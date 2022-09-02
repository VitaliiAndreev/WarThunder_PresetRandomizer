using Core;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Objects;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for UpDownBattleRatingPairControl.xaml. </summary>
    public partial class UpDownBattleRatingPairControl : UserControl
    {
        #region Properties

        /// <summary> The maximum allowed <see cref="IVehicle.EconomicRank"/> defined by the control's state. </summary>
        public int MaximumEconomicRank => _maximumUpDownControl.Value;

        /// <summary> The minimum allowed <see cref="IVehicle.EconomicRank"/> defined by the control's state. </summary>
        public int MinimumEconomicRank => _minimumUpDownControl.Value;

        #endregion Properties
        #region Events

        /// <summary> A routed event for <see cref="ValueChanged"/>. </summary>
        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(nameof(ValueChanged), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UpDownBattleRatingPairControl));

        /// <summary> Occurs when <see cref="Value"/> changes. </summary>
        public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        #endregion Events
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public UpDownBattleRatingPairControl()
        {
            InitializeComponent();

            _maximumUpDownControl.MaximumValue = EReference.MaximumEconomicRank;
            _minimumUpDownControl.MinimumValue = Integer.Number.Zero;
        }

        #endregion Constructors
        #region Methods: Event Handlers

        /// <summary> Adjusts boundaries for the adjacent control. </summary>
        /// <param name="sender"> The object that has triggered the event. One of the <see cref="UpDownBattleRatingControl"/>s is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnValueChanged(object sender, RoutedEventArgs eventArguments)
        {
            if (sender.Equals(_maximumUpDownControl))
            {
                _minimumUpDownControl.MaximumValue = _maximumUpDownControl.Value;
                RaiseValueChanged();
            }

            else if (sender.Equals(_minimumUpDownControl))
            {
                _maximumUpDownControl.MinimumValue = _minimumUpDownControl.Value;
                RaiseValueChanged();
            }
        }

        #endregion Methods: Event Handlers

        /// <summary> Raises the <see cref="ValueChangedEvent"/> event. </summary>
        public void RaiseValueChanged() =>
            RaiseEvent(new RoutedEventArgs(ValueChangedEvent, this));

        /// <summary> Initializes initial values. </summary>
        /// <param name="interval"> The interval to use for initialization. </param>
        public void Initialize(Interval<int> interval)
        {
            _maximumUpDownControl.Value = Math.Min(interval.RightItem, EReference.MaximumEconomicRank);
            _minimumUpDownControl.Value = Math.Max(interval.LeftItem, Integer.Number.Zero);
        }
    }
}