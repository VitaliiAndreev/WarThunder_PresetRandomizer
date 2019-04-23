using Core.DataBase.Objects.Interfaces;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A vehicle (air, ground, or sea). </summary>
    public interface IVehicle : IPersistentObjectWithIdAndGaijinId
    {
        /// <summary>
        /// The purchase cost in Silver Lions.
        /// Zero means that the vehicle cannot be bought for Silver Lions.
        /// </summary>
        int PurchaseCostInSilver { get; }
    }
}
