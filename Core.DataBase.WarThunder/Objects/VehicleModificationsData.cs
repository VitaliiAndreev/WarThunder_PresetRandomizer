using Core.DataBase.Enumerations;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Enumerations.DataBase;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Json;
using NHibernate.Mapping.Attributes;

namespace Core.DataBase.WarThunder.Objects
{
    /// <summary> A set of vehicle information pertaining to midifications. </summary>
    [Class(Table = ETable.VehicleModificationsData)]
    public class VehicleModificationsData : PersistentDeserialisedObjectWithIdAndVehicle, IVehicleModificationsData
    {
        #region Association Properties

        /// <summary> The vehicle the data set belongs to. </summary>
        [ManyToOne(0, Column = ETable.Vehicle + "_" + EColumn.Id, ClassType = typeof(Vehicle), NotNull = true, Lazy = Laziness.Proxy)]
        [Key(1, Unique = true, Column = ETable.Vehicle + "_" + EColumn.Id)]
        public override IVehicle Vehicle { get; protected set; }

        #endregion Association Properties
        #region Persistent Properties

        /// <summary> The vehicle's ID. </summary>
        [Id(Column = EColumn.Id, TypeType = typeof(long), Name = nameof(Id), Generator = EIdGenerator.HiLo)]
        public override long Id { get; protected set; }

        /// <summary>
        /// [NOT VISUALLY USED IN GAME CLIENT]
        /// The amount of researched modifications of the zeroth tier required to unlock modifications of the first tier.
        /// </summary>
        [Property()] public virtual int AmountOfModificationsResearchedIn_Tier0_RequiredToUnlock_Tier1 { get; protected set; }

        /// <summary> The amount of researched modifications of the first tier required to unlock modifications of the second tier. </summary>
        [Property()] public virtual int AmountOfModificationsResearchedIn_Tier1_RequiredToUnlock_Tier2 { get; protected set; }

        /// <summary> The amount of researched modifications of the second tier required to unlock modifications of the third tier. </summary>
        [Property()] public virtual int AmountOfModificationsResearchedIn_Tier2_RequiredToUnlock_Tier3 { get; protected set; }

        /// <summary> The amount of researched modifications of the third tier required to unlock modifications of the fourth tier. </summary>
        [Property()] public virtual int AmountOfModificationsResearchedIn_Tier3_RequiredToUnlock_Tier4 { get; protected set; }

        #endregion Persistent Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected VehicleModificationsData()
        {
        }

        /// <summary> Creates a data set. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="vehicle"> The vehicle this data set belongs to. </param>
        /// <param name="instanceDerializedFromJson"> A non-persistent instance deserialized from JSON data to initialize this instance with. </param>
        public VehicleModificationsData(IDataRepository dataRepository, IVehicle vehicle, VehicleDeserializedFromJsonWpCost instanceDerializedFromJson)
            : this(dataRepository, -1L, vehicle)
        {
            InitializeWithDeserializedJson(instanceDerializedFromJson);
        }

        /// <summary> Creates a data set. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The objects's ID. </param>
        /// <param name="vehicle"> The vehicle this data set belongs to. </param>
        public VehicleModificationsData(IDataRepository dataRepository, long id, IVehicle vehicle)
            : base(dataRepository, id, vehicle)
        {
            LogCreation();
        }

        #endregion Constructors
    }
}