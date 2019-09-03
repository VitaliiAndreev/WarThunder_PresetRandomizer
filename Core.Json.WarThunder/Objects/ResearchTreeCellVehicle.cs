using System.Collections.Generic;
using Core.DataBase.WarThunder.Objects.Json;

namespace Core.Json.WarThunder.Objects
{
    /// <summary> A research tree cell containing a single vehicle. </summary>
    public class ResearchTreeCellVehicle : ResearchTreeCell
    {
        #region Properties

        /// <summary> The vehicle positioned in the cell. </summary>
        public ResearchTreeVehicleFromJson Vehicle { get; }

        /// <summary> All vehicles postioned in the cell. </summary>
        public override IList<ResearchTreeVehicleFromJson> Vehicles => new List<ResearchTreeVehicleFromJson> { Vehicle };

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new research tree cell. </summary>
        /// <param name="vehicle"> The vehicle to position in the cell. </param>
        public ResearchTreeCellVehicle(ResearchTreeVehicleFromJson vehicle)
            : base(vehicle.Rank)
        {
            Vehicle = vehicle;
        }

        #endregion Constructors
    }
}