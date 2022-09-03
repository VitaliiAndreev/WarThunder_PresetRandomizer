using Core.DataBase.WarThunder.Objects.Json;
using Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Core.Json.WarThunder.Objects
{
    /// <summary> A research tree cell. </summary>
    public abstract class ResearchTreeCellFromJson
    {
        #region Fields

        /// <summary>
        /// The number of the row the cell occupies within its rank.
        /// <para> Important: ignore for helicopter and ship branches. </para>
        /// </summary>
        private int _rowWithinRank;

        #endregion Fields
        #region Properties

        /// <summary> The rank the cell occupies. </summary>
        public int Rank { get; internal set; }

        /// <summary> All vehicles postioned in the cell. </summary>
        public virtual IList<ResearchTreeVehicleFromJson> Vehicles { get; }

        /// <summary>
        /// The number of the row the cell occupies within its rank.
        /// <para> When set, row numbers are assigned to vehicles in the cell as well. </para>
        /// <para> Important: ignore for helicopter and ship branches. </para>
        /// </summary>
        public int RowWithinRank
        {
            get => _rowWithinRank;
            set
            {
                _rowWithinRank = value;
                SetVehicleRowWithinRank();
            }
        }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new research tree cell. </summary>
        public ResearchTreeCellFromJson()
        {
            Vehicles = new List<ResearchTreeVehicleFromJson>();
        }

        /// <summary> Creates a new research tree cell. </summary>
        /// <param name="rank"> The rank the cell occupies. </param>
        public ResearchTreeCellFromJson(int rank)
            : this()
        {
            Rank = rank;
        }

        #endregion Constructors

        /// <summary>
        /// Passes the number of the row the cell occupies within its rank to the vehicles in the cell.
        /// Vehicles with preset coordinates (helicopters and ships) are ignored.
        /// </summary>
        private void SetVehicleRowWithinRank()
        {
            void SetVehicleRowWithinRank(ResearchTreeVehicleFromJson vehicle)
            {
                if (vehicle.CellCoordinatesWithinRank.Count < 2)
                    vehicle.CellCoordinatesWithinRank.Add(_rowWithinRank);
            }

            if (Vehicles.HasSingle())
            {
                SetVehicleRowWithinRank(Vehicles.First());
                return;
            }

            for (var vehicleIndex = 0; vehicleIndex < Vehicles.Count; vehicleIndex++)
            {
                var vehicle = Vehicles[vehicleIndex];
                vehicle.FolderIndex = vehicleIndex;
                SetVehicleRowWithinRank(vehicle);
            }
        }

        /// <summary> Sets the specified cell's row number within its rank. </summary>
        /// <param name="previousCells"> Previous cells in the research tree column. </param>
        public void SetRowWithinRank(IEnumerable<ResearchTreeCellFromJson> previousCells)
        {
            void setOne() => RowWithinRank = 1;

            if (previousCells.Any())
            {
                var lastCell = previousCells.Last();

                if (lastCell.Rank < Rank)
                {
                    setOne();
                }
                else if (lastCell.Rank == Rank)
                {
                    RowWithinRank = lastCell.RowWithinRank + 1;
                }
                else
                {
                    var previousCellsOfSameRank = previousCells.Where(cell => cell.Rank == Rank);

                    RowWithinRank = previousCellsOfSameRank.Any()
                        ? previousCellsOfSameRank.Max(cell => cell.RowWithinRank) + 1
                        : 1;
                }
            }
            else
                setOne();
        }
    }
}