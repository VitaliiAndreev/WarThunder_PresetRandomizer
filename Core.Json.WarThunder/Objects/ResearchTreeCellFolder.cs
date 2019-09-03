using Core.DataBase.WarThunder.Objects.Json;
using System.Collections;
using System.Collections.Generic;

namespace Core.Json.WarThunder.Objects
{
    /// <summary> A research tree cell containing vehicle in a folder. </summary>
    public class ResearchTreeCellFolder : ResearchTreeCell, IEnumerable<ResearchTreeVehicleFromJson>
    {
        #region Constructors

        /// <summary> Creates a new research tree cell. </summary>
        public ResearchTreeCellFolder()
            : base()
        {
        }

        #endregion Constructors

        public IEnumerator<ResearchTreeVehicleFromJson> GetEnumerator() => Vehicles.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}