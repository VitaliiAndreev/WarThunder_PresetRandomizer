using Core.DataBase.WarThunder.Enumerations;
using Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for BranchToggleControl.xaml. </summary>
    public partial class BranchToggleControl : UserControl
    {
        #region Fields

        /// <summary> The map of the game mode enumeration onto corresponding buttons. </summary>
        private readonly IDictionary<EBranch, ToggleButton> _buttons;

        #endregion Fields
        #region Events

        /// <summary> A routed event for <see cref="Click"/>. </summary>
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(nameof(Click), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(BranchToggleControl));

        /// <summary> An event for clicking toggle buttons. </summary>
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        #endregion Events
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public BranchToggleControl()
        {
            InitializeComponent();

            _armyButton.Tag = EBranch.Army;
            _aviationButton.Tag = EBranch.Aviation;
            _helicopterButton.Tag = EBranch.Helicopters;
            _fleetButton.Tag = EBranch.Fleet;

            _buttons = new List<ToggleButton> { _armyButton, _aviationButton, _helicopterButton, _fleetButton }.ToDictionary(button => button.Tag.CastTo<EBranch>());

            _armyButton.Click += OnClick;
            _aviationButton.Click += OnClick;
            _helicopterButton.Click += OnClick;
            _fleetButton.Click += OnClick;
        }

        #endregion Constructors
        #region Methods: Event Handlers

        /// <summary> Raises the <see cref="ClickEvent"/> for one of the toggle buttons. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="ToggleButton"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnClick(object sender, RoutedEventArgs eventArguments)
        {
            if (sender is ToggleButton toggleButton)
                RaiseClickEvent(toggleButton);
        }

        #endregion Methods: Event Handlers

        /// <summary> Raises the <see cref="ClickEvent"/> for the specified toggle button. </summary>
        /// <param name="toggleButton"> The toggle button to raise the event for. </param>
        private void RaiseClickEvent(ToggleButton toggleButton) =>
            RaiseEvent(new RoutedEventArgs(ClickEvent, toggleButton));

        /// <summary> Changes the <see cref="UIElement.IsEnabled"/> status of a button corresponding to the specified branch. </summary>
        /// <param name="branch"> The branch whose toggle button's state to change. </param>
        /// <param name="enable"> Whether to enable or disable the toggle button. </param>
        public void Enable(EBranch branch, bool enable)
        {
            if (_buttons.TryGetValue(branch, out var toggleButton) && toggleButton.IsEnabled != enable)
                toggleButton.IsEnabled = enable;
        }

        /// <summary> Toggles a button corresponding to the specified branch. </summary>
        /// <param name="branches"> The branch whose button to toggle. </param>
        /// <param name="newState"> Whether to toggle the button on or off. </param>
        public void Toggle(EBranch branch, bool newState)
        {
            if (_buttons.TryGetValue(branch, out var toggleButton) && toggleButton.IsChecked != newState)
                toggleButton.IsChecked = newState;
        }

        /// <summary> Toggles buttons corresponding to specified branches. </summary>
        /// <param name="branches"> The branches whose buttons to toggle. </param>
        /// <param name="newState"> Whether to toggle buttons on or off. </param>
        public void Toggle(IEnumerable<EBranch> branches, bool newState)
        {
            foreach (var branch in branches)
                Toggle(branch, newState);
        }
    }
}