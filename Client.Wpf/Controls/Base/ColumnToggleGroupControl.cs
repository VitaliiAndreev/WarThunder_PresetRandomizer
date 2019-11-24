using Client.Wpf.Controls.Base.Interfaces;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Client.Wpf.Controls.Base
{
    /// <summary> A user control consisting of toggle button columns indexed by <typeparamref name="T"/> values. </summary>
    /// <typeparam name="T"> The type of keys by which to group toggle button columns. </typeparam>
    /// <typeparam name="V"> The type of keys by which to group items in toggle button columns. </typeparam>
    public abstract class ColumnToggleGroupControl<T, V> : LocalizedUserControl, IControlWithToggleColumns<T, V>
    {
        #region Properties

        /// <summary> Toggle button columns indexed by the <typeparamref name="T"/> key. </summary>
        public IDictionary<T, ToggleButtonGroupControl<V>> ToggleClassColumns { get; }

        #endregion Properties
        #region Events

        /// <summary> A routed event for <see cref="Click"/>. </summary>
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(nameof(Click), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ColumnToggleGroupControl<T, V>));

        /// <summary> An event for clicking toggle buttons. </summary>
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        #endregion Events
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public ColumnToggleGroupControl()
        {
            ToggleClassColumns = new Dictionary<T, ToggleButtonGroupControl<V>>();
        }

        #endregion Constructors
        #region Methods: Event Handlers

        /// <summary> Raises the <see cref="ClickEvent"/> for one of the toggle buttons. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="ToggleButton"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        public virtual void OnClick(object sender, RoutedEventArgs eventArguments)
        {
            if (eventArguments.OriginalSource is ToggleButton toggleButton)
                RaiseClickEvent(toggleButton);
        }

        #endregion Methods: Event Handlers

        /// <summary> Applies localization to visible text on the control. </summary>
        public override void Localize()
        {
            base.Localize();

            foreach (var column in ToggleClassColumns.Values)
                column.Localize();
        }

        /// <summary> Raises the <see cref="ClickEvent"/> for the specified toggle button. </summary>
        /// <param name="toggleButton"> The toggle button to raise the event for. </param>
        public void RaiseClickEvent(ToggleButton toggleButton) =>
            RaiseEvent(new RoutedEventArgs(ClickEvent, toggleButton));

        /// <summary> Creates columns of toggle buttons for given vehicle branches, with character icons. </summary>
        /// <param name="keys"> Vehicle branches to create columns of toggle buttons for. </param>
        public abstract void CreateToggleColumns(IEnumerable<T> keys);

        /// <summary> Changes the <see cref="UIElement.IsEnabled"/> status of the toggle column corresponding to the specified outer key. </summary>
        /// <param name="outerKey"> The key by which to access toggle columns. </param>
        /// <param name="enable"> Whether to enable or disable toggle buttons. </param>
        public void Enable(T outerKey, bool enable)
        {
            if (ToggleClassColumns.TryGetValue(outerKey, out var column) && column.IsEnabled != enable)
                column.IsEnabled = enable;
        }

        #region Methods: Toggle()

        /// <summary> Toggles a button corresponding to the specified inner key. </summary>
        /// <param name="innerKey"> The key by which to access buttons. </param>
        /// <param name="newState"> Whether to toggle the button on or off. </param>
        public abstract void Toggle(V innerKey, bool newState);

        /// <summary> Toggles buttons corresponding to specified inner keys. </summary>
        /// <param name="innerKeys"> Keys by which to access buttons. </param>
        /// <param name="newState"> Whether to toggle buttons on or off. </param>
        public void Toggle(IEnumerable<V> innerKeys, bool newState)
        {
            foreach (var innerKey in innerKeys)
                Toggle(innerKey, newState);
        }

        #endregion Methods: Toggle()
    }
}