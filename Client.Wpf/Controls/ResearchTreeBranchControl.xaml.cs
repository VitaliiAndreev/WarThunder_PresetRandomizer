using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using Core.Organization.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for ResearchTreeBranchControl.xaml. </summary>
    public partial class ResearchTreeBranchControl : UserControl
    {
        #region Fields

        /// <summary> The research tree branch the control has been populated with. </summary>
        private ResearchTreeBranch _researchTreeBranch;

        /// <summary> The map of the rank enumeration onto corresponding style keys. </summary>
        private readonly IDictionary<ERank, string> _styleKeys;

        /// <summary> Cells in the grid. </summary>
        private readonly IDictionary<Tuple<int, int>, ResearchTreeCellControl> _cells;

        /// <summary> Vehicle controls in the grid. </summary>
        private readonly IDictionary<string, ResearchTreeCellVehicleControl> _cellVehicleControls;

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

                Grid.SetRow(cellWithBottomBorder, rowIndex);
                Grid.SetColumn(cellWithBottomBorder, columnIndex);

                _grid.Children.Add(cellWithBottomBorder);
            }
            else
            {
                Grid.SetRow(cell, rowIndex);
                Grid.SetColumn(cell, columnIndex);

                _grid.Children.Add(cell);
            }
        }

        /// <summary> Populates the <see cref="_grid"/> with content cells. </summary>
        /// <param name="branch"></param>
        internal void Populate(ResearchTreeBranch branch)
        {
            if (branch is null || IsPopulated)
                return;

            _researchTreeBranch = branch;

            Enumerable.Range(EInteger.Number.Zero, _researchTreeBranch.ColumnCount).ToList().ForEach(number => _grid.ColumnDefinitions.Add(new ColumnDefinition()));
            Enumerable.Range(EInteger.Number.Zero, _researchTreeBranch.RowCount).ToList().ForEach(number => _grid.RowDefinitions.Add(new RowDefinition()));

            foreach (var rankKeyValuePair in _researchTreeBranch)
            {
                var rankKey = rankKeyValuePair.Key;
                var rank = rankKeyValuePair.Value;

                for (var rowNumber = rank.StartingRowNumber.Value; rowNumber <= rank.MaximumRowNumber; rowNumber++)
                {
                    var rowIndex = rowNumber - EInteger.Number.One;

                    for (var columnNumber = EInteger.Number.One; columnNumber <= _researchTreeBranch.ColumnCount; columnNumber++)
                    {
                        var rowNumberRelativeToRank = rowNumber - rank.StartingRowNumber.Value + EInteger.Number.One;
                        var columnIndex = columnNumber - EInteger.Number.One;
                        var cell = new ResearchTreeCellControl()
                        {
                            Style = this.GetStyle(_styleKeys[rankKey]),
                        };

                        var cellVehicles = rank
                            .GetVehicles(columnNumber, rowNumberRelativeToRank)
                            .OrderBy(vehicle => vehicle.ResearchTreeData.FolderIndex)
                        ;

                        foreach (var vehicle in cellVehicles)
                            cell.AddVehicle(vehicle);

                        AttachEventHandlers(cell);
                        AddCell(cell, rank, rowIndex, columnIndex);

                        _cells.Add(new Tuple<int, int>(columnIndex, rowIndex), cell);
                    }
                }
            }
        }

        /// <summary> Displays vehicle information for the given <paramref name="gameMode"/>. </summary>
        /// <param name="gameMode"> The game mode for which to display the battle rating. </param>
        internal void DisplayVehicleInformation(EGameMode gameMode)
        {
            foreach (var vehicleCell in _cellVehicleControls.Values)
                vehicleCell.DisplayVehicleInformation(gameMode);
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