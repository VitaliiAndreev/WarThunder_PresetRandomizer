namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A set of vehicle information pertaining to midifications. </summary>
    public interface IVehicleModificationsData : IPersistentWarThunderObjectWithId
    {
        #region Association Properties

        /// <summary> The vehicle the data set belongs to. </summary>
        IVehicle Vehicle { get; }

        #endregion Association Properties
        #region Persistent Properties

        /// <summary>
        /// [NOT VISUALLY USED IN GAME CLIENT]
        /// The amount of researched modifications of the zeroth tier required to unlock modifications of the first tier.
        /// </summary>
        int AmountOfModificationsResearchedIn_Tier0_RequiredToUnlock_Tier1 { get; }

        /// <summary> The amount of researched modifications of the first tier required to unlock modifications of the second tier. </summary>
        int AmountOfModificationsResearchedIn_Tier1_RequiredToUnlock_Tier2 { get; }

        /// <summary> The amount of researched modifications of the second tier required to unlock modifications of the third tier. </summary>
        int AmountOfModificationsResearchedIn_Tier2_RequiredToUnlock_Tier3 { get; }

        /// <summary> The amount of researched modifications of the third tier required to unlock modifications of the fourth tier. </summary>
        int AmountOfModificationsResearchedIn_Tier3_RequiredToUnlock_Tier4 { get; }

        #endregion Persistent Properties
    }
}