namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A set of vehicle information pertaining to graphics. </summary>
    public interface IVehicleGraphicsData : IPersistentDeserialisedObjectWithId
    {
        #region Association Properties

        /// <summary> The vehicle the data set belongs to. </summary>
        IVehicle Vehicle { get; }

        #endregion Association Properties
        #region Persistent Properties

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        string CustomClassIco { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        string CustomImage { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        string CustomTooltipImage { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        string CommonWeaponImage { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        int? WeaponMask { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        int? BulletsIconParam { get; }

        #endregion Persistent Properties
    }
}