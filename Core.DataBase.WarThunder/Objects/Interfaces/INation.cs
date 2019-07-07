using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A nation (with their own research trees) in the game. </summary>
    public interface INation : IPersistentObjectWithIdAndGaijinId
    {
        /// <summary> The nation's military branches. </summary>
        IEnumerable<IBranch> Branches { get; }
    }
}