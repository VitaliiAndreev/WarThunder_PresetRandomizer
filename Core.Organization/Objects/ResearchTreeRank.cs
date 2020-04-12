using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using System.Collections.Generic;
using System.Linq;

namespace Core.Organization.Objects
{
    public class ResearchTreeRank : Dictionary<ResearchTreeCoordinatesWithinRank, IVehicle>
    {
        #region Properties

        /// <summary> The number of the row this rank starts at in relation to the overall <see cref="ResearchTreeBranch"/> it's in. </summary>
        public int? StartingRowNumber { get; internal set; }

        /// <summary> The amount of rows of vehicle cells within the rank. </summary>
        public int RowCount { get; private set; }

        /// <summary> The maximum row number of vehicle cells within the rank. </summary>
        public int MaximumRowNumber { get; internal set; }

        /// <summary> The maximum column number of vehicle cells within the rank. </summary>
        public int MaximumColumnNumber { get; private set; }

        /// <summary> Numbers of columns reserved for premium / gift vehicles. </summary>
        public IEnumerable<int> PremiumColumnNumbers { get; private set; }

        #endregion Properties

        /// <summary> Calculates <see cref="MaximumColumnNumber"/>, <see cref="RowCount"/>, and <see cref="PremiumColumnNumbers"/>. </summary>
        public void InitializeProperties()
        {
            MaximumColumnNumber = Values.Max(vehicle => vehicle.ResearchTreeData.CellCoordinatesWithinRank.First());
            RowCount = Values.Max(vehicle => vehicle.ResearchTreeData.CellCoordinatesWithinRank.Last());
            
            PremiumColumnNumbers = Enumerable
                .Range(EInteger.Number.One, MaximumColumnNumber)
                .Where(columnNumber => GetVehiclesInColumn(columnNumber).Any(vehicle => !vehicle.IsResearchable))
            ;
        }

        /// <summary> Returns all vehicles positioned in the column of the specified number. </summary>
        /// <param name="columnNumber"> The number of the column from which to gather vehicles. </param>
        /// <returns></returns>
        public IEnumerable<IVehicle> GetVehiclesInColumn(int columnNumber) =>
            Values.Where(vehicle => vehicle.ResearchTreeData.CellCoordinatesWithinRank.First() == columnNumber);

        /// <summary> Returns vehicles in the research tree cell with the given coordinates. </summary>
        /// <param name="columnNumber"> The column number of the research tree cell containing the vehicles. </param>
        /// <param name="rowNumber"> The row number of the research tree cell containing the vehicles. </param>
        public IEnumerable<IVehicle> GetVehicles(int columnNumber, int rowNumber)
        {
            if (TryGetValue(new ResearchTreeCoordinatesWithinRank(columnNumber, rowNumber, null), out var vehicle))
                return new List<IVehicle> { vehicle };

            var vehicles = new List<IVehicle>();
            var folderIndex = EInteger.Number.Zero;

            while (TryGetValue(new ResearchTreeCoordinatesWithinRank(columnNumber, rowNumber, folderIndex++), out var folderVehicle))
                vehicles.Add(folderVehicle);

            return vehicles;
        }
    }
}