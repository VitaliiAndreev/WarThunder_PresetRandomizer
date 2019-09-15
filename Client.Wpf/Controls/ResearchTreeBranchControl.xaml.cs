using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using Core.Organization.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

        #endregion Fields
        #region Properties

        /// <summary> Whether the control has been populated. </summary>
        public bool IsPopulated => _researchTreeBranch is ResearchTreeBranch;

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
        }

        #endregion Constructors

        /// <summary> Populates the <see cref="_grid"/> with content cells. </summary>
        /// <param name="branch"></param>
        public void Populate(ResearchTreeBranch branch)
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
                            Style = FindResource(_styleKeys[rankKey]) as Style,
                        };

                        Grid.SetRow(cell, rowIndex);
                        Grid.SetColumn(cell, columnIndex);

                        var cellVehicles = rank
                            .GetVehicles(columnNumber, rowNumberRelativeToRank)
                            .OrderBy(vehicle => vehicle.ResearchTreeData.FolderIndex)
                        ;

                        foreach (var vehicle in cellVehicles)
                            cell.AddVehicle(vehicle);

                        _grid.Children.Add(cell);
                        _cells.Add(new Tuple<int, int>(columnIndex, rowIndex), cell);
                    }
                }
            }
        }

        /// <summary> Displays <see cref="IVehicle.BattleRating"/> values for the given <paramref name="gameMode"/>. </summary>
        /// <param name="gameMode"> The game mode for which to display the battle rating. </param>
        public void DisplayBattleRatingFor(EGameMode gameMode)
        {
            foreach (var vehicleCell in _cells.Values)
                vehicleCell.DisplayBattleRatingFor(gameMode);
        }
    }
}