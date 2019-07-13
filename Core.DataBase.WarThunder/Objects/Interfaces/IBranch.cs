
namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A nation's military branch. </summary>
    public interface IBranch : IPersistentObjectWithIdAndGaijinId
    {
        /// <summary> The branch's nation. </summary>
        INation Nation { get; }
    }
}