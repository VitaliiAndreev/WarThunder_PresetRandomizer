using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Collections.Generic;

namespace Core.Organization.Objects
{
    public class ResearchTreeRank : Dictionary<ResearchTreeCoordinatesWithinRank, IVehicle>
    {
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