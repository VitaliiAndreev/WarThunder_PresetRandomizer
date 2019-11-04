using Core.DataBase.Objects.Interfaces;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A set of vehicle information pertaining to the crew. </summary>
    public interface IVehicleCrewData : IPersistentObjectWithId
    {
        #region Association Properties

        /// <summary> The vehicle the data set belongs to. </summary>
        IVehicle Vehicle { get; }

        #endregion Association Properties
        #region Persistent Properties

        /// <summary> The total number of crewmen in the vehicle. </summary>
        int CrewCount { get; }

        /// <summary>
        /// The minimum number of crewmen in the vehicle for it to be operable.
        /// This property is only assigned to naval vessels. Aircraft by default need at least one pilot to stay in the air, while ground vehicles require two.
        /// </summary>
        int? MinumumCrewCountToOperate { get; }

        /// <summary> The number of gunners in the vehicle. </summary>
        int GunnersCount { get; }

        /// <summary> The baseline time of fire extinguishing for inexperienced naval crewmen. </summary>
        decimal? MaximumFireExtinguishingTime { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal? HullBreachRepairSpeed { get; }

        #endregion Persistent Properties
    }
}