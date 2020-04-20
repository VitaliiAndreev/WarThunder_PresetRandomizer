using Core.DataBase.Enumerations;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects.Interfaces;
using Core.DataBase.WarThunder.Enumerations.DataBase;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Json;
using Core.DataBase.WarThunder.Objects.VehicleGameModeParameterSets;
using NHibernate.Mapping.Attributes;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects
{
    /// <summary> A set of vehicle information pertaining to performance. </summary>
    [Class(Table = ETable.VehiclePerformanceData)]
    public class VehiclePerformanceData : PersistentDeserialisedObjectWithIdAndVehicle, IVehiclePerformanceData
    {
        #region Persistent Properties

        /// <summary> The vehicle's ID. </summary>
        [Id(Column = EColumn.Id, TypeType = typeof(long), Name = nameof(Id), Generator = EIdGenerator.HiLo)]
        public override long Id { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual string MoveType { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual string SpawnType { get; protected set; }

        /// <summary> Whether this vehicle can spawn as a kill streak aircraft in Arcade Battles. </summary>
        [Property()] public virtual bool? CanSpawnAsKillStreak { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual decimal? Speed { get; protected set; }

        /// <summary> Maximum flight time (in munutes). Applies only to planes and indicates for how long one can fly with a full tank of fuel. </summary>
        [Property()] public virtual int? MaximumFlightTime { get; protected set; }

        #endregion Persistent Properties
        #region Association Properties

        /// <summary> The vehicle the data set belongs to. </summary>
        [ManyToOne(0, Column = ETable.Vehicle + "_" + EColumn.Id, ClassType = typeof(Vehicle), NotNull = true, Lazy = Laziness.Proxy)]
        [Key(1, Unique = true, Column = ETable.Vehicle + "_" + EColumn.Id)]
        public override IVehicle Vehicle { get; protected set; }

        /// <summary>
        /// The number of times this vehicle can sortie per match.
        /// This property is necessary for branches that don't have more than one reserve / starter vehicle, like helicopters and navy.
        /// </summary>
        [OneToOne(ClassType = typeof(VehicleGameModeParameterSet.Integer.NumberOfSpawns), PropertyRef = nameof(VehicleGameModeParameterSet.Integer.NumberOfSpawns.Entity), Lazy = Laziness.Proxy)]
        public virtual VehicleGameModeParameterSet.Integer.NumberOfSpawns NumberOfSpawns { get; protected set; }

        #endregion Association Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected VehiclePerformanceData()
        {
        }

        /// <summary> Creates a data set. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="vehicle"> The vehicle this data set belongs to. </param>
        /// <param name="instanceDerializedFromJson"> A non-persistent instance deserialized from JSON data to initialize this instance with. </param>
        public VehiclePerformanceData(IDataRepository dataRepository, IVehicle vehicle, VehicleDeserializedFromJsonWpCost instanceDerializedFromJson)
            : this(dataRepository, -1L, vehicle)
        {
            InitializeGameModeParameterSets();
            InitializeWithDeserializedVehicleDataJson(instanceDerializedFromJson);
        }

        /// <summary> Creates a data set. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The object's ID. </param>
        /// <param name="vehicle"> The vehicle this data set belongs to. </param>
        public VehiclePerformanceData(IDataRepository dataRepository, long id, IVehicle vehicle)
            : base(dataRepository, id, vehicle)
        {
            LogCreation();
        }

        #endregion Constructors
        #region Methods: Initialization

        /// <summary> Initializes game mode parameter sets. </summary>
        public virtual void InitializeGameModeParameterSets()
        {
            NumberOfSpawns = new VehicleGameModeParameterSet.Integer.NumberOfSpawns(_dataRepository, this);
        }

        /// <summary> Fills properties of the object with values deserialized from JSON data read from "wpcost.blkx". </summary>
        /// <param name="deserializedVehicle"> The temporary non-persistent object storing deserialized data. </param>
        protected virtual void InitializeWithDeserializedVehicleDataJson(VehicleDeserializedFromJsonWpCost deserializedVehicle)
        {
            InitializeWithDeserializedJson(deserializedVehicle);
            ConsolidateGameModeParameterPropertiesIntoSets(deserializedVehicle);
        }

        #region Methods: Initialization Helpers

        /// <summary> Consolidates values of JSON properties for <see cref="EGameMode"/> parameters into sets defined in the persistent class. </summary>
        /// <param name="instanceDeserializedFromJson"> The temporary non-persistent object storing deserialized data. </param>
        private void ConsolidateGameModeParameterPropertiesIntoSets(VehicleDeserializedFromJsonWpCost instanceDeserializedFromJson)
        {
            var parameterSets = new Dictionary<string, VehicleGameModeParameterSetBase>
            {
                { nameof(NumberOfSpawns), NumberOfSpawns },
            };
            ConsolidateGameModeParameterPropertiesIntoSets(parameterSets, instanceDeserializedFromJson);
        }

        #endregion Methods: Initialization Helpers

        #endregion Methods: Initialization
        #region Methods: Overrides

        /// <summary> Returns all persistent objects nested in the instance. This method requires overriding implementation to function. </summary>
        /// <returns></returns>
        public override IEnumerable<IPersistentObject> GetAllNestedObjects()
        {
            var nestedObjects = new List<IPersistentObject>()
            {
                NumberOfSpawns,
            };

            return nestedObjects;
        }

        #endregion Methods: Overrides
    }
}