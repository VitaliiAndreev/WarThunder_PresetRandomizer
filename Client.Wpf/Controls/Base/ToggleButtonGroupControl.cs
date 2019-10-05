﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Client.Wpf.Controls.Base
{
    /// <summary> A user control consisting of a row of toggle buttons, indexed by <typeparamref name="T"/> values. </summary>
    /// <typeparam name="T"> The key type. </typeparam>
    public class ToggleButtonGroupControl<T> : LocalizedUserControl
    {
        #region Fields

        /// <summary> The map of the game mode enumeration onto corresponding buttons. </summary>
        protected readonly IDictionary<T, ToggleButton> _buttons;

        #endregion Fields
        #region Events

        /// <summary> A routed event for <see cref="Click"/>. </summary>
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(nameof(Click), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ToggleButtonGroupControl<T>));

        /// <summary> An event for clicking toggle buttons. </summary>
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        #endregion Events
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public ToggleButtonGroupControl()
        {
            _buttons = new Dictionary<T, ToggleButton>();
        }

        #endregion Constructors
        #region Methods: Event Handlers

        /// <summary> Raises the <see cref="ClickEvent"/> for one of the toggle buttons. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="ToggleButton"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        protected void OnClick(object sender, RoutedEventArgs eventArguments)
        {
            if (sender is ToggleButton toggleButton)
                RaiseClickEvent(toggleButton);
        }

        #endregion Methods: Event Handlers

        /// <summary> Raises the <see cref="ClickEvent"/> for the specified toggle button. </summary>
        /// <param name="toggleButton"> The toggle button to raise the event for. </param>
        private void RaiseClickEvent(ToggleButton toggleButton) =>
            RaiseEvent(new RoutedEventArgs(ClickEvent, toggleButton));

        /// <summary> Changes the <see cref="UIElement.IsEnabled"/> status of a button corresponding to the specified key. </summary>
        /// <param name="key"> The key whose toggle button's state to change. </param>
        /// <param name="enable"> Whether to enable or disable the toggle button. </param>
        public void Enable(T key, bool enable)
        {
            if (_buttons.TryGetValue(key, out var toggleButton) && toggleButton.IsEnabled != enable)
                toggleButton.IsEnabled = enable;
        }

        /// <summary> Toggles a button corresponding to the specified key. </summary>
        /// <param name="branches"> The key whose button to toggle. </param>
        /// <param name="newState"> Whether to toggle the button on or off. </param>
        public void Toggle(T key, bool newState)
        {
            if (_buttons.TryGetValue(key, out var toggleButton) && toggleButton.IsChecked != newState)
                toggleButton.IsChecked = newState;
        }

        /// <summary> Toggles buttons corresponding to specified keys. </summary>
        /// <param name="keys"> The keys whose buttons to toggle. </param>
        /// <param name="newState"> Whether to toggle buttons on or off. </param>
        public void Toggle(IEnumerable<T> keys, bool newState)
        {
            foreach (var branch in keys)
                Toggle(branch, newState);
        }
    }
}