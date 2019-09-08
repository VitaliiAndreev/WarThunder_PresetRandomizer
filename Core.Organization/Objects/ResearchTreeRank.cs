using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using System.Collections.Generic;
using System.Linq;

namespace Core.Organization.Objects
{
    public class ResearchTreeRank : Dictionary<ResearchTreeCoordinatesWithinRank, IVehicle>
    {
        #region Properties

        /// <summary> The maximum column number of vehicle cells within the rank. </summary>
        public int MaximumColumnNumber { get; private set; }

        /// <summary> The maximum row number of vehicle cells within the rank. </summary>
        public int MaximumRowNumber { get; private set; }

        /// <summary> Numbers of columns reserved for premium / gift vehicles. </summary>
        public IEnumerable<int> PremiumColumnNumbers { get; private set; }

        #endregion Properties

        /// <summary> Calculates <see cref="MaximumColumnNumber"/>, <see cref="MaximumRowNumber"/>, and <see cref="PremiumColumnNumbers"/>. </summary>
        public void InitializeProperties()
        {
            MaximumColumnNumber = Values.Max(vehicle => vehicle.ResearchTreeData.CellCoordinatesWithinRank.First());
            MaximumRowNumber = Values.Max(vehicle => vehicle.ResearchTreeData.CellCoordinatesWithinRank.Last());
            
            PremiumColumnNumbers = Enumerable
                .Range(EInteger.Number.One, MaximumColumnNumber)
                .Where(columnNumber => GetVehiclesInColumn(columnNumber).Any(vehicle => vehicle.NotResearchable))
            ;
        }

        /// <summary> Returns all vehicles positioned in the column of the specified number. </summary>
        /// <param name="columnNumber"> The number of the column from which to gather vehicles. </param>
        /// <returns></returns>
        public IEnumerable<IVehicle> GetVehiclesInColumn(int columnNumber) =>
            Values.Where(vehicle => vehicle.ResearchTreeData.CellCoordinatesWithinRank.First() == columnNumber);

        /// <summary> Returns the vehicle in the research tree cell with the given coordinates, or NULL if the cell is empty. </summary>
        /// <param name="columnNumber"></param>
        /// <param name="rowNumber"></param>
        /// <returns></returns>
        public IVehicle GetVehicle(int columnNumber, int rowNumber)
        {
            if (TryGetValue(new ResearchTreeCoordinatesWithinRank(columnNumber, rowNumber), out var vehicle))
                return vehicle;

            return null;
        }
    }
}