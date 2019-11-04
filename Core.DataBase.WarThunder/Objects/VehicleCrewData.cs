using Core.DataBase.Enumerations;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Enumerations.DataBase;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Json;
using NHibernate.Mapping.Attributes;

namespace Core.DataBase.WarThunder.Objects
{
    /// <summary> A set of vehicle information pertaining to the crew. </summary>
    [Class(Table = ETable.VehicleCrewData)]
    public class VehicleCrewData : PersistentWarThunderObjectWithId, IVehicleCrewData
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

        /// <summary> The total number of crewmen in the vehicle. </summary>
        [Property()] public virtual int CrewCount { get; protected set; }

        /// <summary>
        /// The minimum number of crewmen in the vehicle for it to be operable.
        /// This property is only assigned to naval vessels. Aircraft by default need at least one pilot to stay in the air, while ground vehicles require two.
        /// </summary>
        [Property()] public virtual int? MinumumCrewCountToOperate { get; protected set; }

        /// <summary> The number of gunners in the vehicle. </summary>
        [Property()] public virtual int GunnersCount { get; protected set; }

        /// <summary> The baseline time of fire extinguishing for inexperienced naval crewmen. </summary>
        [Property()] public virtual decimal? MaximumFireExtinguishingTime { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual decimal? HullBreachRepairSpeed { get; protected set; }

        #endregion Persistent Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected VehicleCrewData()
        {
        }

        /// <summary> Creates a data set. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="vehicle"> The vehicle this data set belongs to. </param>
        /// <param name="instanceDerializedFromJson"> A non-persistent instance deserialized from JSON data to initialize this instance with. </param>
        public VehicleCrewData(IDataRepository dataRepository, IVehicle vehicle, VehicleDeserializedFromJsonWpCost instanceDerializedFromJson)
            : this(dataRepository, -1L, vehicle)
        {
            InitializeWithDeserializedJson(instanceDerializedFromJson);
        }

        /// <summary> Creates a data set. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The vehicle's ID. </param>
        /// <param name="vehicle"> The vehicle this data set belongs to. </param>
        public VehicleCrewData(IDataRepository dataRepository, long id, IVehicle vehicle)
            : base(dataRepository, id)
        {
            Vehicle = vehicle;

            LogCreation();
        }

        #endregion Constructors
    }
}