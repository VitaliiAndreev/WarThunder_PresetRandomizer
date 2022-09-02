using Client.Shared.Attributes;
using Client.Shared.LiteObjectProfiles;
using Client.Shared.Wpf.Extensions;
using Client.Wpf.Commands.MainWindow.Interfaces;
using Client.Wpf.Controls.Base;
using Client.Wpf.Controls.Strategies;
using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Client.Wpf.Presenters.Interfaces;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using Core.Extensions;
using System.Collections.Generic;
using System.ComponentModel;
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
        #region Properties

        public EVehicleProfile VehicleProfile { get; set; }

        #endregion Properties
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
            _includeHeadersOnCopyLabel.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.IncludeColumnHeadersWhenCopyingSelectedRows);
        }

        #endregion Methods: Overrides
        #region Methods: Initialisation

        public void Initialise(IMainWindowPresenter presenter)
        {
            if (!_initialised && presenter is IMainWindowPresenter)
            {
                _presenter = presenter;

                if (_includeHeadersOnCopyCheckBox.IsChecked != _presenter.IncludeHeadersOnRowCopy)
                {
                    _includeHeadersOnCopyCheckBox.IsChecked = _presenter.IncludeHeadersOnRowCopy;

                    SetHeaderCopyBehaviour(_includeHeadersOnCopyCheckBox.IsChecked());
                }

                _includeHeadersOnCopyCheckBox.CommandParameter = _presenter;
                _includeHeadersOnCopyCheckBox.Command = _presenter.GetCommand(ECommandName.SwitchIncludeHeadersOnRowCopyFlag);

                _initialised = true;
            }
        }

        public void SetDataSource(string key, IEnumerable<IVehicle> collection, Language language)
        {
            if (_previousGridSourceKey != key)
            {
                _previousGridSourceKey = key;
                _grid.ItemsSource = collection.Select(item => item.AsLite(language)).AsParallel().ToList();
            }
        }

        public void AdjustControlVisibility()
        {
            if (_grid.ItemsSource is null || _grid.ItemsSource.IsEmpty())
            {
                _panel.Children.Insert(Integer.Number.Zero, _hint);
                _grid.Visibility
                    = _includeHeadersOnCopyPanel.Visibility
                    = Visibility.Hidden;
            }
            else
            {
                _panel.Children.Remove(_hint);
                _grid.Visibility
                    = _includeHeadersOnCopyPanel.Visibility
                    = Visibility.Visible;
            }
        }

        #endregion Methods: Initialisation
        #region Methods: Event Handlers

        private void OnGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs eventArguments)
        {
            if (eventArguments.PropertyDescriptor is PropertyDescriptor property && property.Attributes.OfType<ShowVehiclePropertyAttribute>().First() is ShowVehiclePropertyAttribute displayAttribute)
            {
                if (displayAttribute.ProhibitedProfiles.HasFlag(VehicleProfile))
                {
                    eventArguments.Cancel = true;
                    return;
                }
            }
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

                newTooltip.Localise();
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
                CommandParameter = _presenter,
                Command = genericGoToResearchTreeCommand is ISwitchToResearchTreeCommand goToResearchTreeCommand
                    ? goToResearchTreeCommand.With(vehicle)
                    : genericGoToResearchTreeCommand,
            };
            var goToWikiLinkMenuItem = new MenuItem
            {
                IsCheckable = false,
                StaysOpenOnClick = false,
                Header = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.GoToWiki),
                Tag = vehicle,
                CommandParameter = _presenter,
                Command = _presenter.GetCommand(ECommandName.GoToWiki),
            };
            var contextMenu = new ContextMenu
            {
                StaysOpen = false,
            };

            goToResearchTreeMenuItem.ToolTipOpening += OnMenuItemTooltipOpening;
            goToResearchTreeMenuItem.Click += OnContextMenuItemClick;
            goToWikiLinkMenuItem.Click += OnContextMenuItemClick;

            contextMenu.Items.Add(goToResearchTreeMenuItem);
            contextMenu.Items.Add(goToWikiLinkMenuItem);

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
                    if (!tooltip.IsInitialised && vehicle is IVehicle)
                        tooltip.Initialise(_presenter, vehicle, new DisplayVehicleInformationStandaloneStrategy());

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

        private void OnContextMenuItemClick(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is IVehicle vehicle)
                _presenter.ReferencedVehicle = vehicle;
        }

        private void OnGridPreviewKeyDown(object sender, KeyEventArgs eventArguments)
        {
            if (eventArguments.Key == Key.C && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                Clipboard.Clear();
        }

        private void SetHeaderCopyBehaviour(bool includeHeaders)
        {
            _grid.ClipboardCopyMode = includeHeaders
                ? DataGridClipboardCopyMode.IncludeHeader
                : DataGridClipboardCopyMode.ExcludeHeader;
        }

        private void OnIncludeHeadersOnCopyClicked(object sender, RoutedEventArgs eventArguments)
        {
            if (eventArguments.Source is CheckBox checkBox)
            {
                var isChecked = checkBox.IsChecked();

                if (_presenter.IncludeHeadersOnRowCopy != isChecked)
                {
                    _presenter.IncludeHeadersOnRowCopy = isChecked;
                    SetHeaderCopyBehaviour(isChecked);
                }
                eventArguments.Handled = true;
            }
        }

        #endregion Methods: Event Handlers

        public void ResetScrollPosition()
        {
            FrameworkElementExtensions.GetChild<ScrollViewer>(_grid)?.ScrollToTop();
        }
    }
}