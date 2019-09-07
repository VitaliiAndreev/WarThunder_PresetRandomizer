using Core.DataBase.WarThunder.Objects.Json;
using System.Collections;
using System.Collections.Generic;

namespace Core.Json.WarThunder.Objects
{
    /// <summary> A research tree cell containing vehicle in a folder. </summary>
    public class ResearchTreeCellFolderFromJson : ResearchTreeCellFromJson, IEnumerable<ResearchTreeVehicleFromJson>
    {
        #region Constructors

        /// <summary> Creates a new research tree cell. </summary>
        public ResearchTreeCellFolderFromJson()
            : base()
        {
        }

        #endregion Constructors

        public IEnumerator<ResearchTreeVehicleFromJson> GetEnumerator() => Vehicles.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}