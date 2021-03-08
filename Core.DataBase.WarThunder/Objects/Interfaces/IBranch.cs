using Core.DataBase.WarThunder.Enumerations;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A nation's military branch. </summary>
    public interface IBranch : IPersistentObjectWithIdAndGaijinId
    {
        #region Persistent Properties

        EVehicleCategory AsEnumerationItem { get; }

        #endregion Persistent Properties
        #region Association Properties

        /// <summary> The branch's nation. </summary>
        INation Nation { get; }

        /// <summary> The branch's vehicles. </summary>
        IEnumerable<IVehicle> Vehicles { get; }

        #endregion Association Properties
    }
}