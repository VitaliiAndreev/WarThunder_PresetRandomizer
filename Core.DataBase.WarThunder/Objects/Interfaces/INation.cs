using Core.DataBase.Objects.Interfaces;
using System.Collections.Generic;

namespace Core.Objects.Interfaces
{
    /// <summary> A nation in the game. </summary>
    public interface INation : IPersistentObjectWithIdAndGaijinId
    {
        /// <summary> The nation's military branches. </summary>
        IEnumerable<IBranch> Branches { get; }
    }
}