using Core.DataBase.WarThunder.Enumerations;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A set of aircraft tags. </summary>
    public interface IGroundVehicleTags : IVehicleTags
    {
        #region Persistent Properties

        bool IsUntagged { get; }

        bool IsWheeled { get; }

        bool CanScout { get; }

        bool CanRepairAllies { get; }

        #endregion Persistent Properties
        #region Indexers

        bool this[EVehicleBranchTag tag] { get; }
        bool this[IEnumerable<EVehicleBranchTag> tags] { get; }

        #endregion Indexers
    }
}