using Core.DataBase.Enumerations;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Enumerations.DataBase;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Json;
using NHibernate.Mapping.Attributes;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects
{
    /// <summary> A set of vehicle information pertaining to weapons. </summary>
    [Class(Table = ETable.VehicleWeaponsData)]
    public class VehicleWeaponsData : PersistentWarThunderObjectWithId, IVehicleWeaponsData
    {
        #region Association Properties

        /// <summary> The vehicle the data set belongs to. </summary>
        [ManyToOne(0, Column = ETable.Vehicle + "_" + EColumn.Id, ClassType = typeof(Vehicle), NotNull = true, Lazy = Laziness.Proxy)]
        [Key(1, Unique = true, Column = ETable.Vehicle + "_" + EColumn.Id)]
        public virtual IVehicle Vehicle { get; protected set; }

        #endregion Association Properties
        #region Persistent Properties

        /// <summary> The vehicle's ID. </summary>
        [Id(Column = EColumn.Id, TypeType = typeof(long), Name = nameof(Id), Generator = EIdGenerator.HiLo)]
        public override long Id { get; protected set; }

        /// <summary> The vehicle's turret traverse speeds. </summary>
        [Property()] public virtual List<decimal?> TurretTraverseSpeeds { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual decimal? MachineGunReloadTime { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual decimal? CannonReloadTime { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual decimal? GunnerReloadTime { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY, VEHICLES WITHOUT PRIMARY ARMAMENT DON"T HAVE THIS PROPERTY] </summary>
        [Property()] public virtual int? MaximumAmmunition { get; protected set; }

        /// <summary> Whether the vehicle's main armament comes equipped with an auto-loader (grants fixed reload speed that doesn't depend on the loader and doesn't improve with the loader's skill). </summary>
        [Property()] public virtual bool? PrimaryWeaponHasAutoLoader { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual decimal? MaximumRocketDeltaAngle { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual decimal? MaximumAtgmDeltaAngle { get; protected set; }

        /// <summary> The modification that is considered an upgrade of the vehicle's armaments. </summary>
        [Property()] public virtual string WeaponUpgrade1 { get; protected set; }

        /// <summary> The modification that is considered an upgrade of the vehicle's armaments. </summary>
        [Property()] public virtual string WeaponUpgrade2 { get; protected set; }

        /// <summary> The modification that is considered an upgrade of the vehicle's armaments. </summary>
        [Property()] public virtual string WeaponUpgrade3 { get; protected set; }

        /// <summary> The modification that is considered an upgrade of the vehicle's armaments. </summary>
        [Property()] public virtual string WeaponUpgrade4 { get; protected set; }

        #endregion Persistent Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected VehicleWeaponsData()
        {
        }

        /// <summary> Creates a data set. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="vehicle"> The vehicle this data set belongs to. </param>
        /// <param name="instanceDerializedFromJson"> A non-persistent instance deserialized from JSON data to initialize this instance with. </param>
        public VehicleWeaponsData(IDataRepository dataRepository, IVehicle vehicle, VehicleDeserializedFromJsonWpCost instanceDerializedFromJson)
            : this(dataRepository, -1L, vehicle)
        {
            InitializeWithDeserializedJson(instanceDerializedFromJson);
        }

        /// <summary> Creates a data set. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The vehicle's ID. </param>
        /// <param name="vehicle"> The vehicle this data set belongs to. </param>
        public VehicleWeaponsData(IDataRepository dataRepository, long id, IVehicle vehicle)
            : base(dataRepository, id)
        {
            Vehicle = vehicle;

            LogCreation();
        }

        #endregion Constructors
    }
}