using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A set of vehicle information pertaining to weapons. </summary>
    public interface IVehicleWeaponsData : IPersistentWarThunderObjectWithId
    {
        #region Association Properties

        /// <summary> The vehicle the data set belongs to. </summary>
        IVehicle Vehicle { get; }

        #endregion Association Properties
        #region Persistent Properties

        /// <summary> The vehicle's turret traverse speeds. </summary>
        List<decimal?> TurretTraverseSpeeds { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal? MachineGunReloadTime { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal? CannonReloadTime { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal? GunnerReloadTime { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY, VEHICLES WITHOUT PRIMARY ARMAMENT DON"T HAVE THIS PROPERTY] </summary>
        int? MaximumAmmunition { get; }

        /// <summary> Whether the vehicle's main armament comes equipped with an auto-loader (grants fixed reload speed that doesn't depend on the loader and doesn't improve with the loader's skill). </summary>
        bool? PrimaryWeaponHasAutoLoader { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal? MaximumRocketDeltaAngle { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal? MaximumAtgmDeltaAngle { get; }

        /// <summary> The modification that is considered an upgrade of the vehicle's armaments. </summary>
        string WeaponUpgrade1 { get; }

        /// <summary> The modification that is considered an upgrade of the vehicle's armaments. </summary>
        string WeaponUpgrade2 { get; }

        /// <summary> The modification that is considered an upgrade of the vehicle's armaments. </summary>
        string WeaponUpgrade3 { get; }

        /// <summary> The modification that is considered an upgrade of the vehicle's armaments. </summary>
        string WeaponUpgrade4 { get; }

        #endregion Persistent Properties
    }
}