using Client.Wpf.Controls.Base;
using Client.Wpf.Extensions;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for VehicleClassToggleControl.xaml. </summary>
    public partial class VehicleClassToggleControl : LocalizedUserControl
    {
        #region Fields

        /// <summary> The map of the vehicle branch enumeration onto corresponding toggle button columns. </summary>
        internal readonly IDictionary<EBranch, VehicleClassColumnToggleControl> VehicleClassColumns;

        #endregion Fields
        #region Events

        /// <summary> A routed event for <see cref="Click"/>. </summary>
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent(nameof(Click), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(VehicleClassToggleControl));

        /// <summary> An event for clicking toggle buttons. </summary>
        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        #endregion Events
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public VehicleClassToggleControl()
        {
            VehicleClassColumns = new Dictionary<EBranch, VehicleClassColumnToggleControl>();

            var branches = typeof(EBranch).GetEnumValues().Cast<EBranch>().Except(EBranch.None);

            InitializeComponent();
            CreateToggleColumns(branches);
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

        /// <summary> Raises the <see cref="ClickEvent"/> for the specified toggle button. </summary>
        /// <param name="toggleButton"> The toggle button to raise the event for. </param>
        public void RaiseClickEvent(ToggleButton toggleButton) =>
            RaiseEvent(new RoutedEventArgs(ClickEvent, toggleButton));

        /// <summary> Creates columns of toggle buttons for given vehicle branches, with character icons. </summary>
        /// <param name="branches"> Vehicle branches to create columns of toggle buttons for. </param>
        public void CreateToggleColumns(IEnumerable<EBranch> branches)
        {
            foreach (var branch in branches)
            {
                var columnControl = new VehicleClassColumnToggleControl(branch)
                {
                    Tag = branch,
                };

                columnControl.Click += OnClick;
                columnControl.AddToPanel(_grid, true);

                VehicleClassColumns.Add(branch, columnControl);
            }
        }

        /// <summary> Changes the <see cref="UIElement.IsEnabled"/> status of the toggle column corresponding to the specified vehicle branch. </summary>
        /// <param name="branch"> The vehicle branch whose toggle buttons' state to change. </param>
        /// <param name="enable"> Whether to enable or disable toggle buttons. </param>
        public void Enable(EBranch branch, bool enable)
        {
            if (VehicleClassColumns.TryGetValue(branch, out var column) && column.IsEnabled != enable)
                column.IsEnabled = enable;
        }

        /// <summary> Toggles a button corresponding to the specified vehicle class. </summary>
        /// <param name="vehicleClass"> The vehicle class whose button to toggle. </param>
        /// <param name="newState"> Whether to toggle the button on or off. </param>
        public void Toggle(EVehicleClass vehicleClass, bool newState)
        {
            if (VehicleClassColumns.TryGetValue(vehicleClass.GetBranch(), out var column))
                column.Toggle(vehicleClass, newState);
        }

        /// <summary> Toggles buttons corresponding to specified vehicle classes. </summary>
        /// <param name="vehicleClasses"> The vehicle classes whose buttons to toggle. </param>
        /// <param name="newState"> Whether to toggle buttons on or off. </param>
        public void Toggle(IEnumerable<EVehicleClass> vehicleClasses, bool newState)
        {
            foreach (var vehicleClass in vehicleClasses)
                Toggle(vehicleClass, newState);
        }
    }
}