using Core.DataBase.Objects.Interfaces;

namespace Core.Objects.Interfaces
{
    /// <summary> A nation's military branch. </summary>
    public interface IBranch : IPersistentObjectWithIdAndName
    {
        /// <summary> The branch's nation. </summary>
        INation Nation { get; }
    }
}