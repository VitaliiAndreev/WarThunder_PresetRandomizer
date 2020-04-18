using Core.DataBase.WarThunder.Enumerations;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A nation (with their own research trees) in the game. </summary>
    public interface INation : IPersistentObjectWithIdAndGaijinId
    {
        #region Persistent Properties

        ENation AsEnumerationItem { get; }

        #endregion Persistent Properties
        #region Association Properties

        /// <summary> The nation's military branches. </summary>
        IEnumerable<IBranch> Branches { get; }

        /// <summary> The nation's vehicles. </summary>
        IEnumerable<IVehicle> Vehicles { get; }

        #endregion Association Properties
    }
}