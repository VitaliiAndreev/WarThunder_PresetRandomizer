using Client.Wpf.Enumerations;
using Client.Wpf.Enumerations.Logger;
using Client.Wpf.Extensions;
using Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Client.Wpf.Controls.Base
{
    /// <summary> A user control consisting of a row or a column of <see cref="ToggleButton"/>s, indexed by <typeparamref name="T"/> values. </summary>
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
        public virtual void OnClick(object sender, RoutedEventArgs eventArguments)
        {
            if (sender is ToggleButton toggleButton)
                RaiseClickEvent(toggleButton);
        }

        #endregion Methods: Event Handlers

        /// <summary> Raises the <see cref="ClickEvent"/> for the specified toggle button. </summary>
        /// <param name="toggleButton"> The toggle button to raise the event for. </param>
        public void RaiseClickEvent(ToggleButton toggleButton) =>
            RaiseEvent(new RoutedEventArgs(ClickEvent, toggleButton));

        /// <summary> Adds the given <paramref name="UiElement"/> to the <paramref name="panel"/>, stacking it horizontally or vertically. </summary>
        /// <param name="panel"> The panel to add <paramref name="UiElement"/> onto. </param>
        /// <param name="horizontal"> Whether to stack the <paramref name="UiElement"/> into a row or into a column. </param>
        /// <param name="UiElement"> The UI element to add onto the <paramref name="panel"/></param>
        private void AddToPanel(Panel panel, bool horizontal, UIElement UiElement)
        {
            if (panel is Grid grid)
                AddToGrid(grid, horizontal, UiElement);

            else if (panel is StackPanel && horizontal)
                throw new ArgumentException(EWpfClientLogMessage.StackPanelCantBeHorizontal);

            else if (panel is WrapPanel && !horizontal)
                throw new ArgumentException(EWpfClientLogMessage.WrapPanelCantBeVertical);

            else
                panel.Children.Add(UiElement);
        }

        /// <summary> Adds the given <paramref name="UiElement"/> to the <paramref name="grid"/>, stacking it horizontally or vertically. </summary>
        /// <param name="grid"> The grid to add <paramref name="UiElement"/> into. </param>
        /// <param name="horizontal"> Whether to stack the <paramref name="UiElement"/> into a row or into a column. </param>
        /// <param name="UiElement"> The UI element to add into the <paramref name="grid"/></param>
        protected void AddToGrid(Grid grid, bool horizontal, UIElement UiElement)
        {
            if (horizontal)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.Children.Add(UiElement);

                Grid.SetColumn(UiElement, grid.ColumnDefinitions.Count() - EInteger.Number.One);
            }
            else
            {
                grid.RowDefinitions.Add(new RowDefinition());
                grid.Children.Add(UiElement);

                Grid.SetRow(UiElement, grid.RowDefinitions.Count() - EInteger.Number.One);
            }
        }

        /// <summary> Creates toggle buttons for given enumeration items, with character icons. </summary>
        /// <param name="panel"> The panel to add buttons into. </param>
        /// <param name="enumerationItems"> Enumeration items to create toggle buttons for. </param>
        /// <param name="icons"> Icons for enumeration items. </param>
        /// <param name="styleKey"> The key of the style (defined in <see cref="WpfClient"/> and referenced by <see cref="EStyleKey"/>) to apply. </param>
        /// <param name="horizontal"> Whether to arrange buttons in a row or in a column. </param>
        public void CreateToggleButtons(Panel panel, IEnumerable<T> enumerationItems, IDictionary<T, char> icons, string styleKey, bool horizontal = true)
        {
            foreach (var enumerationItem in enumerationItems)
            {
                var toggleButton = new ToggleButton
                {
                    Style = this.GetStyle(styleKey),
                    Tag = enumerationItem,
                    Content = icons[enumerationItem],
                };

                toggleButton.Click += OnClick;

                _buttons.Add(enumerationItem, toggleButton);
                AddToPanel(panel, horizontal, toggleButton);
            }
        }

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