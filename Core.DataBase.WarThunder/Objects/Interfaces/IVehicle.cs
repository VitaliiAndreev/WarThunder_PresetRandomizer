using Core.DataBase.Objects.Interfaces;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A vehicle (air, ground, or sea). </summary>
    public interface IVehicle : IPersistentObjectWithIdAndGaijinId
    {
        #region Persistent Properties

        #region General

        /// <summary> The vehicle's nation. </summary>
        string Nation { get; }

        string MoveType { get; }

        string Class { get; }

        /// <summary>
        /// The purchase cost in Silver Lions.
        /// Zero means that the vehicle cannot be bought for Silver Lions.
        /// </summary>
        int PurchaseCostInSilver { get; }

        /// <summary> The amount of times the vehicle can go on a sortie in Simulator Battles. </summary>
        int? NumberOfSpawnsInSimulation { get; }

        #endregion General
        #region Crew

        /// <summary> The crew train cost in Silver Lions that has to be paid before a vehicle can be put into a crew slot (except for reserve vehicles). </summary>
        int BaseCrewTrainCostInSilver { get; }

        /// <summary> The expert crew train cost in Silver Lions. </summary>
        int ExpertCrewTrainCostInSilver { get; }

        #endregion Crew

        #endregion Persistent Properties
    }
}
