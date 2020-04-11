using Core.DataBase.Enumerations;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Enumerations.DataBase;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Json;
using Core.Extensions;
using NHibernate.Mapping.Attributes;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects
{
    /// <summary> A set of aircraft tags. </summary>
    [Class(Table = ETable.AircraftTags)]
    public class AircraftTags : PersistentObjectWithId, IAircraftTags
    {
        #region Fields

        private readonly IDictionary<EVehicleBranchTag, bool> _index;

        #endregion Fields
        #region Persistent Properties

        /// <summary> The vehicle's ID. </summary>
        [Id(Column = EColumn.Id, TypeType = typeof(long), Name = nameof(Id), Generator = EIdGenerator.HiLo)]
        public override long Id { get; protected set; }

        [Property(NotNull = true)]
        public virtual bool IsUntagged { get; protected set; }

        [Property(NotNull = true)]
        public virtual bool IsNavalAircraft { get; protected set; }

        [Property(NotNull = true)]
        public virtual bool IsHydroplane { get; protected set; }

        [Property(NotNull = true)]
        public virtual bool IsTorpedoBomber { get; protected set; }

        #endregion Persistent Properties
        #region Association Properties

        /// <summary> The vehicle the data set belongs to. </summary>
        [ManyToOne(0, Column = ETable.Vehicle + "_" + EColumn.Id, ClassType = typeof(Vehicle), NotNull = true, Lazy = Laziness.Proxy)]
        [Key(1, Unique = true, Column = ETable.Vehicle + "_" + EColumn.Id)]
        public virtual IVehicle Vehicle { get; protected set; }

        #endregion Association Properties
        #region Indexers

        public virtual bool this[EVehicleBranchTag tag]
        {
            get => _index.TryGetValue(tag, out var isTagged) ? isTagged : false;
        }

        public virtual bool this[IEnumerable<EVehicleBranchTag> tags]
        {
            get
            {
                foreach (var tag in tags)
                {
                    if (this[tag])
                        return true;
                }
                return false;
            }
        }

        #endregion Indexers
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected AircraftTags()
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="vehicle"> The vehicle this object belongs to. </param>
        /// <param name="deserializedTags"> Vehicle tags deserialized from JSON data to initialize this instance with. </param>
        public AircraftTags(IDataRepository dataRepository, IVehicle vehicle, VehicleTagsDeserializedFromJson deserializedTags)
            : this(dataRepository, -1L, vehicle, deserializedTags)
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The vehicle's ID. </param>
        /// <param name="vehicle"> The vehicle this object belongs to. </param>
        /// <param name="deserializedTags"> Vehicle tags deserialized from JSON data to initialize this instance with. </param>
        public AircraftTags(IDataRepository dataRepository, long id, IVehicle vehicle, VehicleTagsDeserializedFromJson deserializedTags)
            : base(dataRepository, id)
        {
            _index = new Dictionary<EVehicleBranchTag, bool>();

            Vehicle = vehicle;

            InitialiseProperties(deserializedTags, Vehicle.Branch.AsEnumerationItem);
            LogCreation();
        }

        #endregion Constructors
        #region Methods: Initialisation

        /// <summary> Initialises class properties. </summary>
        /// <param name="deserializedTags"> Vehicle tags deserialized from JSON data to select subclasses from. </param>
        /// <param name="branch"> The vehicle branch for which to select subclasses. </param>
        private void InitialiseProperties(VehicleTagsDeserializedFromJson deserializedTags, EBranch branch)
        {
            if (branch == EBranch.Aviation)
            {
                IsNavalAircraft = deserializedTags.IsNavalAircraft;
                IsHydroplane = deserializedTags.IsHydroplane;
                IsTorpedoBomber = deserializedTags.CanCarryTorpedoes;
            }

            if (IsNavalAircraft)
                _index.Add(EVehicleBranchTag.NavalAircraft, IsNavalAircraft);

            if (IsHydroplane)
                _index.Add(EVehicleBranchTag.Hydroplane, IsHydroplane);

            if (IsTorpedoBomber)
                _index.Add(EVehicleBranchTag.TorpedoBomber, IsTorpedoBomber);

            if (_index.IsEmpty())
            {
                IsUntagged = true;
                _index.Add(EVehicleBranchTag.UntaggedAircraft, IsUntagged);
            }
        }

        #endregion Methods: Initialisation
    }
}