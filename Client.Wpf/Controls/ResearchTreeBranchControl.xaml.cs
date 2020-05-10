using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Client.Wpf.Presenters.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using Core.Extensions;
using Core.Organization.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for ResearchTreeBranchControl.xaml. </summary>
    public partial class ResearchTreeBranchControl : LocalisedUserControl
    {
        #region Fields

        /// <summary> The map of the rank enumeration onto corresponding style keys. </summary>
        private readonly IDictionary<ERank, string> _styleKeys;

        /// <summary> Cells in the grid. </summary>
        private readonly IDictionary<Tuple<int, int>, ResearchTreeCellControl> _cells;

        /// <summary> Vehicle controls in the grid. </summary>
        private readonly IDictionary<string, ResearchTreeCellVehicleControl> _cellVehicleControls;

        /// <summary> The research tree branch the control has been populated with. </summary>
        private ResearchTreeBranch _researchTreeBranch;

        /// <summary> Indicates whether buttons' state can be altered. </summary>
        private bool _buttonsAreSuspended;

        private bool _initialised;

        private IMainWindowPresenter _presenter;

        #endregion Fields
        #region Properties

        /// <summary> Whether the control has been populated. </summary>
        internal bool IsPopulated => _researchTreeBranch is ResearchTreeBranch;

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public ResearchTreeBranchControl()
        {
            InitializeComponent();

            _styleKeys = new Dictionary<ERank, string>
            {
                { ERank.I, EStyleKey.ResearchTreeCellControl.Rank1 },
                { ERank.II, EStyleKey.ResearchTreeCellControl.Rank2 },
                { ERank.III, EStyleKey.ResearchTreeCellControl.Rank3 },
                { ERank.IV, EStyleKey.ResearchTreeCellControl.Rank4 },
                { ERank.V, EStyleKey.ResearchTreeCellControl.Rank5 },
                { ERank.VI, EStyleKey.ResearchTreeCellControl.Rank6 },
                { ERank.VII, EStyleKey.ResearchTreeCellControl.Rank7 },
            };
            _cells = new Dictionary<Tuple<int, int>, ResearchTreeCellControl>();
            _cellVehicleControls = new Dictionary<string, ResearchTreeCellVehicleControl>();
        }

        #endregion Constructors
        #region Methods: Event Handlers

        private void ToggleAllVehicles(ToggleButton toggleButton, bool onlyNonResearchable = false)
        {
            _buttonsAreSuspended = true;
            {
                foreach (var vehicleCellControl in _cellVehicleControls.Values)
                {
                    if (vehicleCellControl.IsToggled != toggleButton.IsChecked)
                    {
                        if (onlyNonResearchable && vehicleCellControl.Vehicle.IsResearchable)
                            continue;

                        vehicleCellControl.HandleClick();
                    }
                }
                _toggleAllVehiclesButton.IsChecked = AllVehiclesAreToggled();
                _toggleAllNonResearchableVehiclesButton.IsChecked = AllVehiclesAreToggled(true);
            }
            _buttonsAreSuspended = false;
        }

        /// <summary> Toggles all vehicles on/off. </summary>
        /// <param name="sender"> The event sender. <see cref="ToggleButton"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnToggleAllClick(object sender, EventArgs eventArguments)
        {
            if (sender is ToggleButton toggleButton)
                ToggleAllVehicles(toggleButton);
        }

        /// <summary> Toggles all non-researchable vehicles on/off. </summary>
        /// <param name="sender"> The event sender. <see cref="ToggleButton"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnToggleAllNonResearchableClick(object sender, EventArgs eventArguments)
        {
            if (sender is ToggleButton toggleButton)
                ToggleAllVehicles(toggleButton, true);
        }

        /// <summary> Updates the state of the <see cref="_toggleAllVehiclesButton"/> if permitted. </summary>
        /// <param name="sender"></param>
        /// <param name="eventArguments"></param>
        private void OnVehicleClick(object sender, RoutedEventArgs eventArguments)
        {
            if (eventArguments.OriginalSource is ResearchTreeCellVehicleControl vehicleControl && !_buttonsAreSuspended)
            {
                _toggleAllVehiclesButton.IsChecked = vehicleControl.IsToggled && AllVehiclesAreToggled();

                if (!vehicleControl.Vehicle.IsResearchable)
                    _toggleAllNonResearchableVehiclesButton.IsChecked = vehicleControl.IsToggled && AllVehiclesAreToggled(true);
            }
        }

        /// <summary> Applies the highlighting style to a <see cref="ResearchTreeCellVehicleControl"/> containing a vehicle required to unlock the one positioned in the <paramref name="sender"/>. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="ResearchTreeCellVehicleControl"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnMouseEnter(object sender, MouseEventArgs eventArguments)
        {
            if (sender is ResearchTreeCellVehicleControl vehicleControl)
            {
                if (_cellVehicleControls.TryGetValue(vehicleControl.Vehicle.RequiredVehicleGaijinId, out var requiredVehicleControl))
                    requiredVehicleControl.ApplyHighlightStyle();
            }
        }

        /// <summary> Applies the idle style to a <see cref="ResearchTreeCellVehicleControl"/> containing a vehicle required to unlock the one positioned in the <paramref name="sender"/>. </summary>
        /// <param name="sender"> The object that has triggered the event. A <see cref="ResearchTreeCellVehicleControl"/> is expected. </param>
        /// <param name="eventArguments"> Not used. </param>
        private void OnMouseLeave(object sender, MouseEventArgs eventArguments)
        {
            if (sender is ResearchTreeCellVehicleControl vehicleControl)
            {
                if (_cellVehicleControls.TryGetValue(vehicleControl.Vehicle.RequiredVehicleGaijinId, out var requiredVehicleControl))
                    requiredVehicleControl.ApplyIdleStyle();
            }
        }

        #endregion Methods: Event Handlers
        #region Methods: Initialisation

        public void Initialise(IMainWindowPresenter presenter)
        {
            if (!_initialised && presenter is IMainWindowPresenter)
            {
                _presenter = presenter;
                _initialised = true;
            }
        }

        /// <summary> Initialises the state of the <see cref="_toggleAllVehiclesButton"/> depending on vehicle selection. </summary>
        public void InitialiseButtons()
        {
            _toggleAllVehiclesButton.IsChecked = AllVehiclesAreToggled();
            _toggleAllNonResearchableVehiclesButton.IsChecked = AllVehiclesAreToggled(true);
        }

        /// <summary> Attaches event handlers to enable highligting vehicles required for unlocking the currenly highlighted one. </summary>
        /// <param name="cell"> The research tree cell to whose content attach event handlers to. </param>
        private void AttachEventHandlers(ResearchTreeCellControl cell)
        {
            foreach (var vehicleControl in cell.VehicleControls.Values)
            {
                _cellVehicleControls.Add(vehicleControl.Vehicle.GaijinId, vehicleControl);

                if (string.IsNullOrWhiteSpace(vehicleControl.Vehicle.RequiredVehicleGaijinId))
                    continue;

                vehicleControl.MouseEnter += OnMouseEnter;
                vehicleControl.MouseLeave += OnMouseLeave;
            }
        }

        /// <summary> Adds the given cell to the grid, adding a border at the bottom if the current row is the last one in the specified rank. </summary>
        /// <param name="cell"> The research tree cell to add. </param>
        /// <param name="rank"> The current vehicle rank. </param>
        /// <param name="rowIndex"> The index of the current row. </param>
        /// <param name="columnIndex"> The index of the current column. </param>
        private void AddCell(ResearchTreeCellControl cell, ResearchTreeRank rank, int rowIndex, int columnIndex)
        {
            var rowNumber = rowIndex + EInteger.Number.One;

            if (rowNumber == rank.MaximumRowNumber)
            {
                var cellWithBottomBorder = new Border()
                {
                    Style = this.GetStyle(EStyleKey.Border.RankDivider),
                    Child = cell,
                };

                _grid.Add(cellWithBottomBorder, columnIndex, rowIndex);
            }
            else
            {
                _grid.Add(cell, columnIndex, rowIndex);
            }
        }

        private void PopulateRankHeader(ERank rankKey, ResearchTreeRank rank)
        {
            var cellWithBorder = new Border { BorderThickness = new Thickness(EInteger.Number.Zero, EInteger.Number.One, EInteger.Number.Zero, EInteger.Number.One), BorderBrush = new SolidColorBrush(Colors.DarkGray) };

            new TextBlock
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.Bold,
                Text = rankKey.ToString(),
            }.PutInto(cellWithBorder);

            _grid.Add(cellWithBorder, EInteger.Number.Zero, rank.StartingRowNumber.Value - EInteger.Number.One);

            Grid.SetRowSpan(cellWithBorder, rank.MaximumRowNumber - rank.StartingRowNumber.Value + EInteger.Number.One);
        }

        /// <summary> Populates the <see cref="_grid"/> with content cells. </summary>
        /// <param name="branch"> The research tree branch to create cells with. </param>
        /// <param name="enabledVehicleGaijinIds"> Gaijin IDs of vehicles enabled by dafault. </param>
        /// <param name="loadingTracker"> An instance of a presenter to communicate with the GUI loading window. </param>
        internal void Populate(ResearchTreeBranch branch, IEnumerable<string> enabledVehicleGaijinIds, IGuiLoadingWindowPresenter loadingTracker)
        {
            if (branch is null || IsPopulated)
                return;

            _researchTreeBranch = branch;

            loadingTracker.RanksPopulated = EInteger.Number.Zero;
            loadingTracker.RanksToPopulate = _researchTreeBranch.Count;

            foreach (var rankKeyValuePair in _researchTreeBranch)
            {
                var rankKey = rankKeyValuePair.Key;
                var rank = rankKeyValuePair.Value;

                loadingTracker.CurrentlyPopulatedRank = rankKey.ToString();
                loadingTracker.RowsPopulated = EInteger.Number.Zero;
                loadingTracker.RowsToPopulate = rank.MaximumRowNumber - rank.StartingRowNumber.Value + EInteger.Number.One;

                PopulateRankHeader(rankKey, rank);

                for (var rowNumber = rank.StartingRowNumber.Value; rowNumber <= rank.MaximumRowNumber; rowNumber++)
                {
                    var rowIndex = rowNumber - EInteger.Number.One;

                    loadingTracker.ColumnsPopulated = EInteger.Number.Zero;
                    loadingTracker.ColumnsToPopulate = _researchTreeBranch.ColumnCount;

                    for (var columnNumber = EInteger.Number.One; columnNumber <= _researchTreeBranch.ColumnCount; columnNumber++)
                    {
                        var rowNumberRelativeToRank = rowNumber - rank.StartingRowNumber.Value + EInteger.Number.One;
                        var columnIndex = columnNumber;
                        var cell = new ResearchTreeCellControl()
                        {
                            Style = this.GetStyle(_styleKeys[rankKey]),
                        }.With(_presenter);

                        var cellVehicles = rank
                            .GetVehicles(columnNumber, rowNumberRelativeToRank)
                            .OrderBy(vehicle => vehicle.ResearchTreeData.FolderIndex)
                        ;

                        loadingTracker.CurrentlyProcessedVehicle = cellVehicles.FirstOrDefault()?.ResearchTreeName?.GetLocalisation(WpfSettings.LocalizationLanguage);

                        foreach (var vehicle in cellVehicles)
                            cell.AddVehicle(vehicle, vehicle.GaijinId.IsIn(enabledVehicleGaijinIds));

                        AttachEventHandlers(cell);
                        AddCell(cell, rank, rowIndex, columnIndex);

                        _cells.Add(new Tuple<int, int>(columnIndex, rowIndex), cell);

                        loadingTracker.ColumnsPopulated++;
                    }
                    loadingTracker.RowsPopulated++;
                }
                loadingTracker.CurrentlyProcessedVehicle = string.Empty;
                loadingTracker.CurrentlyPopulatedRank = string.Empty;
                loadingTracker.RanksPopulated++;
            }

            _grid.ColumnDefinitions.First().Width = new GridLength(EInteger.Number.Thirty, GridUnitType.Pixel);

            InitialiseButtons();
        }

        #endregion Methods: Initialisation
        #region Methods: Overrides

        public override void Localise()
        {
            base.Localise();

            static string localise(string localisationKey) => ApplicationHelpers.LocalisationManager.GetLocalisedString(localisationKey);

            _toggleAllVehiclesButton.Content = localise(ELocalisationKey.All);
            _toggleAllVehiclesButton.ToolTip = localise(ELocalisationKey.SelectAllVehicles);

            _toggleAllVehiclesButton.Content = localise(ELocalisationKey.AllNonResearchable);
            _toggleAllVehiclesButton.ToolTip = localise(ELocalisationKey.SelectAllNonResearchableVehicles);
        }

        #endregion Methods: Overrides
        #region Methods: Checks

        /// <summary> Checks whether all vehicles in the branch are toggled on. </summary>
        /// <returns></returns>
        private bool AllVehiclesAreToggled(bool onlyNonResearchable = false) =>
            _cellVehicleControls.Values.Where(control => !onlyNonResearchable || !control.Vehicle.IsResearchable).All(vehicleCellControls => vehicleCellControls.IsToggled);

        #endregion Methods: Checks

        /// <summary> Displays vehicle information for the given <paramref name="gameMode"/>. </summary>
        /// <param name="gameMode"> The game mode for which to display the battle rating. </param>
        internal void DisplayVehicleInformation(EGameMode gameMode)
        {
            foreach (var vehicleCell in _cellVehicleControls.Values)
                vehicleCell.UpdateFor(gameMode);
        }

        /// <summary> Scrolls the research tree to bring the specified vehicle into view. </summary>
        /// <param name="vehicle"> The vehicle to bring into view. </param>
        internal void BringIntoView(IVehicle vehicle)
        {
            if (_cellVehicleControls.TryGetValue(vehicle.GaijinId, out var vehicleCellControl))
                Dispatcher.InvokeAsync(() => vehicleCellControl.BringIntoView(), DispatcherPriority.ApplicationIdle);
        }

        /// <summary> Highlights the specified vehicle in the reseatch tree. </summary>
        /// <param name="vehicle"> The vehicle to highlight. </param>
        internal void Highlight(IVehicle vehicle)
        {
            if (_cellVehicleControls.TryGetValue(vehicle.GaijinId, out var vehicleCellControl))
                vehicleCellControl.ApplyHighlightStyle();
        }

        /// <summary> Removes the highlight from the specified vehicle in the reseatch tree. </summary>
        /// <param name="vehicle"> The vehicle to remove highlight from. </param>
        internal void RemoveHighlight(IVehicle vehicle)
        {
            if (_cellVehicleControls.TryGetValue(vehicle.GaijinId, out var vehicleCellControl))
                vehicleCellControl.ApplyIdleStyle();
        }
    }
}