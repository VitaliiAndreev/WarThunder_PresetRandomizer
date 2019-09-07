using Core.DataBase.WarThunder.Enumerations;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A nation (with their own research trees) in the game. </summary>
    public interface INation : IPersistentObjectWithIdAndGaijinId
    {
        #region Association Properties

        /// <summary> The nation's military branches. </summary>
        IEnumerable<IBranch> Branches { get; }

        /// <summary> The nation's vehicles. </summary>
        IEnumerable<IVehicle> Vehicles { get; }

        #endregion Association Properties
        #region Non-Persistent Properties

        /// <summary> Parses the Gaijin ID of the nation as an item of <see cref="ENation"/>. </summary>
        ENation AsEnumerationItem { get; }

        #endregion Non-Persistent Properties
    }
}