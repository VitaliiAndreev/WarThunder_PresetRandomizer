using Client.Wpf.Controls.Base.Interfaces;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Client.Wpf.Controls.Base
{
    /// <summary> A user control consisting of <typeparamref name="U"/> toggle button columns indexed by <typeparamref name="T"/> values. </summary>
    /// <typeparam name="T"> The type of keys by which to group <typeparamref name="U"/> toggle button columns. </typeparam>
    /// <typeparam name="U"> The type of toggle button columns. </typeparam>
    /// <typeparam name="V"> The type of keys by which to group items in <typeparamref name="U"/> toggle button columns. </typeparam>
    public abstract class ColumnToggleGroupControl<T, U, V> : LocalizedUserControl, IControlWithSubcontrols<T>
        where U : LocalizedUserControl
    {
        #region Fields

        /// <summary> The map of the vehicle branch enumeration onto corresponding toggle button columns. </summary>
        protected internal readonly IDictionary<T, U> ToggleClassColumns;

        #endregion Fields
        #region Events

        /// <summary> A routed event for <see cref="Click"/>. </summary>
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(nameof(Click), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ColumnToggleGroupControl<T, U, V>));

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
            ToggleClassColumns = new Dictionary<T, U>();
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

        /// <summary> Toggles a button corresponding to the specified inner key. </summary>
        /// <param name="innerKey"> The key by which to access buttons. </param>
        /// <param name="newState"> Whether to toggle the button on or off. </param>
        public abstract void Toggle(V innerKey, bool newState);

        /// <summary> Toggles buttons corresponding to specified inner keys. </summary>
        /// <param name="inneryKeys"> Keys by which to access buttons. </param>
        /// <param name="newState"> Whether to toggle buttons on or off. </param>
        public void Toggle(IEnumerable<V> inneryKeys, bool newState)
        {
            foreach (var vehicleClass in inneryKeys)
                Toggle(vehicleClass, newState);
        }
    }
}