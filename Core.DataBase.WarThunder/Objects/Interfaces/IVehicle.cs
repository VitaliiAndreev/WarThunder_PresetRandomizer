using Core.DataBase.Objects.Interfaces;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A vehicle (air, ground, or sea). </summary>
    public interface IVehicle : IPersistentObjectWithIdAndGaijinId
    {
        /// <summary> The vehicle's nation. </summary>
        string Nation { get; }

        string MoveType { get; }

        /// <summary>
        /// The purchase cost in Silver Lions.
        /// Zero means that the vehicle cannot be bought for Silver Lions.
        /// </summary>
        int PurchaseCostInSilver { get; }

        /// <summary> The amount of times the vehicle can go on a sortie in Simulator Battles. </summary>
        int? NumberOfSpawnsInSimulation { get; }
    }
}
