using Core.DataBase.WarThunder.Objects.Json;
using System.Collections;
using System.Collections.Generic;

namespace Core.Json.WarThunder.Objects
{
    /// <summary> A research tree column. </summary>
    public class ResearchTreeColumn : IEnumerable<ResearchTreeCell>
    {
        #region Properties

        /// <summary> Research tree cells positioned in the column. </summary>
        public IList<ResearchTreeCell> Cells { get; }

        /// <summary> All vehicles postioned in the column. </summary>
        public IEnumerable<ResearchTreeVehicleFromJson> Vehicles
        {
            get
            {
                var vehicles = new List<ResearchTreeVehicleFromJson>();

                foreach (var cell in Cells)
                    vehicles.AddRange(cell.Vehicles);

                return vehicles;
            }
        }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new research tree column. </summary>
        public ResearchTreeColumn()
        {
            Cells = new List<ResearchTreeCell>();
        }

        #endregion Constructors

        public IEnumerator<ResearchTreeCell> GetEnumerator() => Cells.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}