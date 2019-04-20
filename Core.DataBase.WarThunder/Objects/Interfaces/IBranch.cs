using Core.DataBase.WarThunder.Objects.Interfaces;

namespace Core.Objects.Interfaces
{
    /// <summary> A nation's military branch. </summary>
    public interface IBranch : IPersistentObjectWithIdAndGaijinId
    {
        /// <summary> The branch's nation. </summary>
        INation Nation { get; }
    }
}