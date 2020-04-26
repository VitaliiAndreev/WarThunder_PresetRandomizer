using Client.Wpf.Commands.MainWindow.Interfaces;
using Client.Wpf.Controls.Base;
using Client.Wpf.Controls.Strategies;
using Client.Wpf.Enumerations;
using Client.Wpf.Enumerations.ShrinkProfiles;
using Client.Wpf.Extensions;
using Client.Wpf.Presenters.Interfaces;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for VehicleListCountrol.xaml. </summary>
    public partial class VehicleListCountrol : LocalisedUserControl
    {
        #region Fields

        private readonly IDictionary<IVehicle, VehicleTooltipControl> _createdTooltips;

        private bool _initialised;
        private string _previousGridSourceKey;

        private IMainWindowPresenter _presenter;

        #endregion Fields
        #region Constructors

        public VehicleListCountrol()
        {
            _createdTooltips = new Dictionary<IVehicle, VehicleTooltipControl>();

            InitializeComponent();
        }

        #endregion Constructors
        #region Methods: Overrides

        public override void Localise()
        {
            base.Localise();

            _hint.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.ClickOnCategoryOnVehicleCountsTab);
        }

        #endregion Methods: Overrides
        #region Methods: Initialisation

        public void Initialise(IMainWindowPresenter presenter)
        {
            if (!_initialised && presenter is IMainWindowPresenter)
            {
                _presenter = presenter;
                _initialised = true;
            }
        }

        public void SetDataSource(string key, IEnumerable<IVehicle> collection, EVehicleProfile vehicleProfile, ELanguage language)
        {
            if (_previousGridSourceKey != key)
            {
                _previousGridSourceKey = key;
                _grid.ItemsSource = collection.Select(item => item.AsLite(vehicleProfile, language)).AsParallel().ToList();
            }
        }

        public void AdjustControlVisibility()
        {
            if (_grid.ItemsSource is null || _grid.ItemsSource.IsEmpty())
            {
                _panel.Children.Insert(EInteger.Number.Zero, _hint);
                _grid.Visibility = Visibility.Hidden;
            }
            else
            {
                _panel.Children.Remove(_hint);
                _grid.Visibility = Visibility.Visible;
            }
        }

        #endregion Methods: Initialisation
        #region Methods: Event Handlers

        private void OnMouseWheel(object sender, MouseWheelEventArgs eventArguments)
        {
            _scrollView.ScrollToVerticalOffset(_scrollView.VerticalOffset - eventArguments.Delta * EDouble.Number.Half);

            eventArguments.Handled = true;
        }

        private void OnGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs eventArguments)
        {
            eventArguments.Column.Header = ApplicationHelpers.LocalisationManager.GetLocalisedString(eventArguments.Column.Header.ToString());
        }

        private void ProcessVehicle(DataGridRow row, IVehicle vehicle)
        {
            if (_createdTooltips.TryGetValue(vehicle, out var tooltip))
            {
                row.ToolTip = tooltip;
            }
            else
            {
                var newTooltip = new VehicleTooltipControl();

                _createdTooltips.Add(vehicle, newTooltip);
                row.ToolTip = newTooltip;
            }

            row.SetRowBackground(vehicle);
            row.ToolTipOpening += OnRowTooltipOpening;
        }

        private void OnLoadingRow(object sender, DataGridRowEventArgs eventArguments)
        {
            var row = eventArguments.Row;
            var vehicleFound = row.TryGetVehicle(out var vehicle);

            var genericGoToResearchTreeCommand = _presenter.GetCommand(ECommandName.SwitchToResearchTree);
            var goToResearchTreeMenuItem = new MenuItem
            {
                IsCheckable = false,
                StaysOpenOnClick = false,
                Header = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.ShowInResearchTree),
                ToolTip = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.ScrapGeneratedPresets),
                Tag = vehicle,
            };
            var contextMenu = new ContextMenu
            {
                StaysOpen = false,
            };

            goToResearchTreeMenuItem.ToolTipOpening += OnMenuItemTooltipOpening;
            goToResearchTreeMenuItem.Click += OnShowResearchTreeClick;
            goToResearchTreeMenuItem.CommandParameter = _presenter;
            goToResearchTreeMenuItem.Command = genericGoToResearchTreeCommand is ISwitchToResearchTreeCommand goToResearchTreeCommand
                ? goToResearchTreeCommand.With(vehicle)
                : genericGoToResearchTreeCommand;

            contextMenu.Items.Add(goToResearchTreeMenuItem);

            row.ContextMenu = contextMenu;
            row.MouseDown += OnRowMouseDown;

            if (vehicleFound)
                ProcessVehicle(row, vehicle);
        }

        private void OnRowTooltipOpening(object sender, ToolTipEventArgs eventArguments)
        {
            if (eventArguments.Source is DataGridRow row)
            {
                if (row.ToolTip is VehicleTooltipControl tooltip && row.TryGetVehicle(out var vehicle))
                {
                    if (!tooltip.IsInitialised)
                        tooltip.Initialise(_presenter, vehicle, new DisplayExtendedVehicleInformationStrategy());

                    tooltip.UpdateFor(_presenter.CurrentGameMode);
                }
            }
        }

        private void OnMenuItemTooltipOpening(object sender, ToolTipEventArgs eventArguments)
        {
            if (eventArguments.Source is MenuItem menuItem)
                eventArguments.Handled = menuItem.IsEnabled;
        }

        private void OnRowMouseDown(object sender, MouseButtonEventArgs eventArguments)
        {
            if (eventArguments.RightButton == MouseButtonState.Pressed)
            {
                if (sender is DataGridRow row)
                {
                    row.ContextMenu.IsOpen = true;
                    eventArguments.Handled = true;
                }
            }
        }

        private void OnShowResearchTreeClick(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is IVehicle vehicle)
                _presenter.ReferencedVehicle = vehicle;
        }

        #endregion Methods: Event Handlers
    }
}