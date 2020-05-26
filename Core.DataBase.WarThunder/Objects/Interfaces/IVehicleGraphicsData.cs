using Core.DataBase.WarThunder.Enumerations;

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

        /// <summary> The path to the image file used as an icon. </summary>
        string CustomClassIco { get; }

        /// <summary> The path to the image file used as a banner icon. </summary>
        string BannerImageName { get; }

        /// <summary> The path to the image file used as a portrait. </summary>
        string PortraitName { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        string CommonWeaponImage { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        int? WeaponMask { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        int? BulletsIconParam { get; }

        #endregion Persistent Properties

        string GetInheritedGaijinId(EVehicleImage imageType);
    }
}