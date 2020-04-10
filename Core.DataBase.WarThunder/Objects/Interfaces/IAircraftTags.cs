using Core.DataBase.Objects.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A set of aircraft tags. </summary>
    public interface IAircraftTags : IPersistentObjectWithId
    {
        #region Persistent Properties

        bool IsUntagged { get; }

        bool IsNavalAircraft { get; }

        bool IsHydroplane { get; }

        bool IsTorpedoBomber { get; }

        #endregion Persistent Properties
        #region Indexers

        bool this[EVehicleBranchTag tag] { get; }
        bool this[IEnumerable<EVehicleBranchTag> tags] { get; }

        #endregion Indexers
    }
}