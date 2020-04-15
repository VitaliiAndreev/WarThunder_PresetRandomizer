using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for BranchToggleControl.xaml. </summary>
    public partial class BranchToggleControl : ToggleButtonGroupControl<EBranch>
    {
        #region Fields

        /// <summary> Togglable vehicle subclass menu items indexed by vehicle subclass keys. </summary>
        private readonly IDictionary<EVehicleBranchTag, MenuItem> _vehicleBranchTagToggleMenuItems;

        #endregion Fields
        #region Events

        /// <summary> A routed event for <see cref="VehicleSubclassToggled"/>. </summary>
        public static readonly RoutedEvent VehicleBranchTagToggledEvent = EventManager.RegisterRoutedEvent(nameof(VehicleBranchTagToggled), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(VehicleClassToggleControl));

        /// <summary> Occurs when any of the <see cref="VehicleSubclassToggleMenuItems"/> is toggled. </summary>
        public event RoutedEventHandler VehicleBranchTagToggled
        {
            add { AddHandler(VehicleBranchTagToggledEvent, value); }
            remove { RemoveHandler(VehicleBranchTagToggledEvent, value); }
        }

        #endregion Events
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public BranchToggleControl()
        {
            _vehicleBranchTagToggleMenuItems = new Dictionary<EVehicleBranchTag, MenuItem>();

            InitializeComponent();
            CreateToggleButtons(_buttonGrid, typeof(EBranch).GetEnumValues().Cast<EBranch>().Where(branch => branch.IsValid()), EReference.BranchIcons, EStyleKey.ToggleButton.BranchToggle);
            CreateContextMenus();
        }

        #endregion Constructors
        #region Methods: Event Handlers

        /// <summary> Raises the <see cref="VehicleBranchTagToggledEvent"/> event. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="MenuItem"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnMenuItemClick(object sender, RoutedEventArgs eventArguments)
        {
            if (sender is MenuItem menuItem)
                RaiseVehicleBranchTagToggled(menuItem);
        }

        #endregion Methods: Event Handlers
        #region Methods: Overrides

        /// <summary> Applies localization to visible text on the control. </summary>
        public override void Localise()
        {
            base.Localise();

            foreach (var pair in _vehicleBranchTagToggleMenuItems)
            {
                var vehicleBranchTag = pair.Key;
                var menuItem = pair.Value;

                menuItem.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(vehicleBranchTag.ToString());
            }
        }

        #endregion Methods: Overrides
        #region Methods: Event Raising

        /// <summary> Raises the <see cref="VehicleBranchTagToggledEvent"/> event. </summary>
        /// <param name="menuItem"> The menu item that has been toggled. </param>
        public void RaiseVehicleBranchTagToggled(MenuItem menuItem) =>
            RaiseEvent(new RoutedEventArgs(VehicleBranchTagToggledEvent, menuItem));

        #endregion Methods: Event Raising
        #region Methods: Initialisation

        /// <summary> Creates context menus for buttons whose assigned vehicle classes have subclasses. </summary>
        private void CreateContextMenus()
        {
            foreach (var vehicleBranchTag in typeof(EVehicleBranchTag).GetEnumValues().OfType<EVehicleBranchTag>().Where(tag => tag.IsValid()))
            {
                var branch = vehicleBranchTag.GetBranch();
                var button = Buttons[branch];
                var contextMenu = button.ContextMenu ?? new ContextMenu() { Tag = vehicleBranchTag };
                var contextMenuItem = contextMenu.CreateTogglableMenuItem(vehicleBranchTag, OnMenuItemClick);

                _vehicleBranchTagToggleMenuItems.Add(vehicleBranchTag, contextMenuItem);

                if (button.ContextMenu is null)
                    button.ContextMenu = contextMenu;
            }
        }

        #endregion Methods: Initialisation
        #region Methods: Toggle()

        /// <summary> Toggles a menu item corresponding to the specified vehicle branch tag key. </summary>
        /// <param name="vehicleBranchTags"> The key by which to access menu items. </param>
        /// <param name="newState"> Whether to toggle the menu item on or off. </param>
        public void Toggle(EVehicleBranchTag vehicleBranchTags, bool newState)
        {
            if (_vehicleBranchTagToggleMenuItems.TryGetValue(vehicleBranchTags, out var subclassMenuItem) && subclassMenuItem.IsChecked != newState)
                subclassMenuItem.IsChecked = newState;
        }

        /// <summary> Toggles menu items corresponding to specified vehicle branch tag keys. </summary>
        /// <param name="vehicleBranchTags"> Keys by which to access menu items. </param>
        /// <param name="newState"> Whether to toggle menu items on or off. </param>
        public void Toggle(IEnumerable<EVehicleBranchTag> vehicleBranchTags, bool newState)
        {
            foreach (var vehicleBranchTag in vehicleBranchTags)
                Toggle(vehicleBranchTag, newState);
        }

        #endregion Methods: Toggle()

        public void UpdateContextMenuItemCount()
        {
            foreach (var button in Buttons.Values)
            {
                if (button.ContextMenu is ContextMenu contextMenu)
                {
                    var toggleMenuItems = contextMenu.Items.OfType<MenuItem>().Where(i => i.IsCheckable);
                    var newTextBlock = new TextBlock
                    {
                        Style = this.GetStyle(EStyleKey.TextBlock.TextBlockWithSkyQuake10pxVerticallyCentered),
                        Text = $"{toggleMenuItems.Where(menuItem => menuItem.IsChecked).Count()}/{toggleMenuItems.Count()}",
                    };

                    if (button.Content is StackPanel stackPanel)
                    {
                        var lastTextBlock = stackPanel.Children.OfType<TextBlock>().Last();

                        stackPanel.Children.Remove(lastTextBlock);
                        stackPanel.Children.Add(newTextBlock);
                    }
                    else
                    {
                        var newStackPanel = new StackPanel { Orientation = Orientation.Horizontal };
                        var iconTextBlock = new TextBlock { Style = this.GetStyle(EStyleKey.TextBlock.TextBlockWithSkyQuake), Text = button.Content.ToString() };

                        newStackPanel.Children.Add(iconTextBlock);
                        newStackPanel.Children.Add(newTextBlock);

                        button.Content = newStackPanel;
                    }
                }
            }
        }
    }
}