using Client.Shared.Wpf.Extensions;
using Client.Wpf.Controls.Base;
using Client.Wpf.Extensions;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.Enumerations;
using Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for VehicleClassToggleControl.xaml. </summary>
    public partial class VehicleClassToggleControl : ColumnToggleGroupControl<EBranch, EVehicleClass>
    {
        #region Constants

        private const double _vehicleClassIconFontSize = EInteger.Number.Eight;
        private const double _buttonLeftColumnWidth = EInteger.Number.Thirteen;
        private const double _buttonRightColumnWidth = EInteger.Number.Fifteen;

        #endregion Constants
        #region Fields

        /// <summary> Togglable vehicle subclass menu items indexed by vehicle subclass keys. </summary>
        private readonly IDictionary<EVehicleSubclass, MenuItem> _vehicleSubclassToggleMenuItems;

        #endregion Fields
        #region Properties

        /// <summary> The main panel of the control. </summary>
        internal override Panel MainPanel => _grid;

        #endregion Properties
        #region Events

        /// <summary> A routed event for <see cref="VehicleSubclassToggled"/>. </summary>
        public static readonly RoutedEvent VehicleSubclassToggledEvent = EventManager.RegisterRoutedEvent(nameof(VehicleSubclassToggled), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(VehicleClassToggleControl));

        /// <summary> Occurs when any of the <see cref="_vehicleSubclassToggleMenuItems"/> is toggled. </summary>
        public event RoutedEventHandler VehicleSubclassToggled
        {
            add { AddHandler(VehicleSubclassToggledEvent, value); }
            remove { RemoveHandler(VehicleSubclassToggledEvent, value); }
        }

        #endregion Events
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public VehicleClassToggleControl()
        {
            _vehicleSubclassToggleMenuItems = new Dictionary<EVehicleSubclass, MenuItem>();

            AttachKeyConverter(vehicleClass => vehicleClass.GetBranch());

            var branches = typeof(EBranch).GetEnumValues().Cast<EBranch>().Where(branch => branch.IsValid());

            InitializeComponent();
            CreateToggleColumns(branches);
            CreateContextMenus();
        }

        #endregion Constructors
        #region Methods: Event Handlers

        /// <summary> Raises the <see cref="VehicleSubclassToggledEvent"/> event. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="MenuItem"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnMenuItemClick(object sender, RoutedEventArgs eventArguments)
        {
            if (sender is MenuItem menuItem)
                RaiseVehicleSubclassToggled(menuItem);
        }

        #endregion Methods: Event Handlers
        #region Methods: Overrides

        /// <summary> Applies localisation to visible text on the control. </summary>
        public override void Localise()
        {
            base.Localise();

            foreach (var pair in _vehicleSubclassToggleMenuItems)
            {
                var vehicleSubclass = pair.Key;
                var menuItem = pair.Value;

                menuItem.Header = ApplicationHelpers.LocalisationManager.GetLocalisedString(vehicleSubclass.ToString());
            }
        }

        #endregion Methods: Overrides
        #region Methods: Event Raising

        /// <summary> Raises the <see cref="VehicleSubclassToggledEvent"/> event. </summary>
        /// <param name="menuItem"> The menu item that has been toggled. </param>
        public void RaiseVehicleSubclassToggled(MenuItem menuItem) =>
            RaiseEvent(new RoutedEventArgs(VehicleSubclassToggledEvent, menuItem));

        #endregion Methods: Event Raising
        #region Methods: Initialisation

        /// <summary> Creates columns of toggle buttons for given vehicle <paramref name="branches"/>, with character icons. </summary>
        /// <param name="branches"> Vehicle branches to create columns of toggle buttons for. </param>
        public void CreateToggleColumns(IEnumerable<EBranch> branches) =>
            CreateToggleColumnsBase(typeof(VehicleClassColumnToggleControl), branches);

        /// <summary> Creates context menus for buttons whose assigned vehicle classes have subclasses. </summary>
        private void CreateContextMenus()
        {
            foreach (var vehicleSubclass in typeof(EVehicleSubclass).GetEnumValues().OfType<EVehicleSubclass>().Where(subclass => subclass.IsValid()))
            {
                var vehicleClass = vehicleSubclass.GetVehicleClass();
                var branch = vehicleClass.GetBranch();

                var contextMenu = ToggleColumns[branch].Buttons[vehicleClass].ContextMenu ?? new ContextMenu() { Tag = vehicleClass };
                var contextMenuItem = contextMenu.CreateTogglableMenuItem(vehicleSubclass, OnMenuItemClick);

                _vehicleSubclassToggleMenuItems.Add(vehicleSubclass, contextMenuItem);

                if (ToggleColumns[branch].Buttons[vehicleClass].ContextMenu is null)
                    ToggleColumns[branch].Buttons[vehicleClass].ContextMenu = contextMenu;
            }
        }

        #endregion Methods: Initialisation
        #region Methods: Toggle()

        /// <summary> Toggles a menu item corresponding to the specified vehicle subclass key. </summary>
        /// <param name="vehicleSubclass"> The key by which to access menu items. </param>
        /// <param name="newState"> Whether to toggle the menu item on or off. </param>
        public void Toggle(EVehicleSubclass vehicleSubclass, bool newState)
        {
            if (_vehicleSubclassToggleMenuItems.TryGetValue(vehicleSubclass, out var subclassMenuItem) && subclassMenuItem.IsChecked != newState)
                subclassMenuItem.IsChecked = newState;
        }

        /// <summary> Toggles menu items corresponding to specified vehicle subclass keys. </summary>
        /// <param name="vehicleSubclasses"> Keys by which to access menu items. </param>
        /// <param name="newState"> Whether to toggle menu items on or off. </param>
        public void Toggle(IEnumerable<EVehicleSubclass> vehicleSubclasses, bool newState)
        {
            foreach (var vehicleSubclass in vehicleSubclasses)
                Toggle(vehicleSubclass, newState);
        }

        #endregion Methods: Toggle()
        #region Methods: UpdateContextMenuItemCount()

        public void UpdateContextMenuItemCount()
        {
            foreach (var toggleColumn in ToggleColumns)
            {
                if (toggleColumn.Value.Buttons.Values.All(button => button.ContextMenu is null))
                    continue;

                foreach (var button in toggleColumn.Value.Buttons.Values)
                {
                    if (button.ContextMenu is null && button.Tag.CastTo<EVehicleClass>().IsValid())
                        button.ReplaceContentWithGrid(_buttonLeftColumnWidth, _buttonRightColumnWidth);

                    else
                        button.UpdateContextMenuItemCount(_buttonLeftColumnWidth, _buttonRightColumnWidth, _vehicleClassIconFontSize);
                }
            }
        }

        public void UpdateContextMenuItemCount(EBranch branch, EVehicleClass vehicleClass)
        {
            if (ToggleColumns.TryGetValue(branch, out var toggleColumn) && toggleColumn.Buttons.TryGetValue(vehicleClass, out var toggleButton))
                toggleButton.UpdateContextMenuItemCount(_buttonLeftColumnWidth, _buttonRightColumnWidth, _vehicleClassIconFontSize);
        }

        #endregion Methods: UpdateContextMenuItemCount()
    }
}