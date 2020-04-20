using Core.DataBase.Objects.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    public interface IVehicleTags : IPersistentObjectWithIdAndVehicle
    {
        #region Indexers

        bool this[EVehicleBranchTag tag] { get; }
        bool this[IEnumerable<EVehicleBranchTag> tags] { get; }

        #endregion Indexers
    }
}