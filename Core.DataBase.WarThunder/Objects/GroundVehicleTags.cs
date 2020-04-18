using Core.DataBase.Enumerations;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Enumerations.DataBase;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Json;
using Core.Extensions;
using NHibernate.Mapping.Attributes;

namespace Core.DataBase.WarThunder.Objects
{
    /// <summary> A set of aircraft tags. </summary>
    [Class(Table = ETable.GroundVehicleTags)]
    public class GroundVehicleTags : VehicleTags, IGroundVehicleTags
    {
        #region Persistent Properties

        /// <summary> The vehicle's ID. </summary>
        [Id(Column = EColumn.Id, TypeType = typeof(long), Name = nameof(Id), Generator = EIdGenerator.HiLo)]
        public override long Id { get; protected set; }

        [Property(NotNull = true)]
        public virtual bool IsUntagged { get; protected set; }

        [Property(NotNull = true)]
        public virtual bool IsWheeled { get; protected set; }

        [Property(NotNull = true)]
        public virtual bool CanScout { get; protected set; }

        #endregion Persistent Properties
        #region Association Properties

        /// <summary> The vehicle the data set belongs to. </summary>
        [ManyToOne(0, Column = ETable.Vehicle + "_" + EColumn.Id, ClassType = typeof(Vehicle), NotNull = true, Lazy = Laziness.Proxy)]
        [Key(1, Unique = true, Column = ETable.Vehicle + "_" + EColumn.Id)]
        public virtual IVehicle Vehicle { get; protected set; }

        #endregion Association Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected GroundVehicleTags()
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="vehicle"> The vehicle this object belongs to. </param>
        /// <param name="deserializedTags"> Vehicle tags deserialized from JSON data to initialise this instance with. </param>
        /// <param name="vehiclePerformanceData"> Vehicle performance data to initialise this instance with. </param>
        public GroundVehicleTags(IDataRepository dataRepository, IVehicle vehicle, VehicleTagsDeserializedFromJson deserializedTags, VehiclePerformanceData vehiclePerformanceData)
            : this(dataRepository, -1L, vehicle, deserializedTags, vehiclePerformanceData)
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The vehicle's ID. </param>
        /// <param name="vehicle"> The vehicle this object belongs to. </param>
        /// <param name="deserializedTags"> Vehicle tags deserialized from JSON data to initialise this instance with. </param>
        /// <param name="vehiclePerformanceData"> Vehicle performance data to initialise this instance with. </param>
        public GroundVehicleTags(IDataRepository dataRepository, long id, IVehicle vehicle, VehicleTagsDeserializedFromJson deserializedTags, VehiclePerformanceData vehiclePerformanceData)
            : base(dataRepository, id)
        {
            Vehicle = vehicle;

            InitialiseProperties(deserializedTags, vehiclePerformanceData, Vehicle.Branch.AsEnumerationItem);
            LogCreation();
        }

        #endregion Constructors
        #region Methods: Initialisation

        /// <summary> Initialises class properties. </summary>
        /// <param name="deserializedTags"> Vehicle tags deserialized from JSON data to select subclasses from. </param>
        /// <param name="vehiclePerformanceData"> Vehicle performance data to initialise this instance with. </param>
        /// <param name="branch"> The vehicle branch for which to select subclasses. </param>
        private void InitialiseProperties(VehicleTagsDeserializedFromJson deserializedTags, VehiclePerformanceData vehiclePerformanceData, EBranch branch)
        {
            if (branch == EBranch.Army)
            {
                IsWheeled = vehiclePerformanceData.MoveType.Contains("wheeled");
                CanScout = deserializedTags.CanScout;
            }
            InitialiseIndex();
        }

        protected override void InitialiseIndex()
        {
            if (IsWheeled)
                _index.Add(EVehicleBranchTag.Wheeled, IsWheeled);

            if (CanScout)
                _index.Add(EVehicleBranchTag.Scout, CanScout);

            if (_index.IsEmpty())
            {
                IsUntagged = true;
                _index.Add(EVehicleBranchTag.UntaggedGroundVehicle, IsUntagged);
            }
        }

        #endregion Methods: Initialisation
    }
}