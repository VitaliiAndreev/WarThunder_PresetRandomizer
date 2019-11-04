namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A set of vehicle information pertaining to performance. </summary>
    public interface IVehiclePerformanceData : IPersistentWarThunderObjectWithId
    {
        #region Persistent Properties

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        string MoveType { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        string SpawnType { get; }

        /// <summary> Whether this vehicle can spawn as a kill streak aircraft in Arcade Battles. </summary>
        bool? CanSpawnAsKillStreak { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal? Speed { get; }

        /// <summary> Maximum flight time (in munutes). Applies only to planes and indicates for how long one can fly with a full tank of fuel. </summary>
        int? MaximumFlightTime { get; }

        #endregion Persistent Properties
        #region Association Properties

        /// <summary> The vehicle the data set belongs to. </summary>
        IVehicle Vehicle { get; }

        /// <summary>
        /// The number of times this vehicle can sortie per match.
        /// This property is necessary for branches that don't have more than one reserve / starter vehicle, like helicopters and navy.
        /// </summary>
        VehicleGameModeParameterSet.Integer.NumberOfSpawns NumberOfSpawns { get; }

        #endregion Association Properties
    }
}