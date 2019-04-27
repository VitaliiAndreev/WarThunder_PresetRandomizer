using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Json;
using Core.Enumerations.DataBase;
using NHibernate.Mapping.Attributes;
using System;

namespace Core.DataBase.WarThunder.Objects
{
    /// <summary> A vehicle (air, ground, or sea). </summary>
    [Class(Table = ETable.Vehicle)]
    public class Vehicle : PersistentObjectWithIdAndGaijinId, IVehicle
    {
        #region Persistent Properties

        #region General

        /// <summary> The vehicle's ID. </summary>
        [Id(Column = EColumn.Id, TypeType = typeof(Guid), Name = nameof(Id))]
        public override Guid Id { get; protected set; }

        /// <summary> The vehicle's Gaijin ID. </summary>
        [Property(NotNull = true, Unique = true)]
        public override string GaijinId { get; protected set; }

        /// <summary> The vehicle's nation. </summary>
        [Property()] public virtual string Nation { get; protected set; }

        [Property()] public virtual string MoveType { get; protected set; }

        [Property()] public virtual string Class { get; protected set; }

        /// <summary>
        /// The purchase cost in Silver Lions.
        /// Zero means that the vehicle cannot be bought for Silver Lions.
        /// </summary>
        [Property()] public virtual int PurchaseCostInSilver { get; protected set; }

        /// <summary> The amount of times the vehicle can go on a sortie in Simulator Battles. </summary>
        [Property()] public virtual int? NumberOfSpawnsInSimulation { get; protected set; }

        #endregion General
        #region Crew

        /// <summary> The crew train cost in Silver Lions that has to be paid before a vehicle can be put into a crew slot (except for reserve vehicles). </summary>
        [Property()] public virtual int BaseCrewTrainCostInSilver { get; protected set; }

        /// <summary> The expert crew train cost in Silver Lions. </summary>
        [Property()] public virtual int ExpertCrewTrainCostInSilver { get; protected set; }

        #endregion Crew

        #endregion Persistent Properties
        #region Constructors

        /// <summary>
        /// Creates a new transient object that can be persisted later.
        /// This constructor is used by NHibernate to instantiate deserialized data read from a database.
        /// </summary>
        protected Vehicle()
        {
        }

        /// <summary> Creates a new nation. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="gaijinId"> The nation's Gaijin ID. </param>
        public Vehicle(IDataRepository dataRepository, string gaijinId)
            : this(dataRepository, Guid.NewGuid(), gaijinId)
        {
        }

        /// <summary> Creates a new nation. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="instanceSerializedFromJson"> A non-persistent instance serialized from JSON data to initialize this instance with. </param>
        public Vehicle(IDataRepository dataRepository, VehicleDeserializedFromJson instanceSerializedFromJson)
            : this(dataRepository, Guid.NewGuid(), instanceSerializedFromJson.GaijinId)
        {
            InitializeWithDeserializedJson(instanceSerializedFromJson);
        }

        /// <summary> Creates a new nation. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The nation's ID. </param>
        /// <param name="gaijinId"> The nation's Gaijin ID. </param>
        public Vehicle(IDataRepository dataRepository, Guid id, string gaijinId)
            : base(dataRepository, id, gaijinId)
        {
            LogCreation();
        }

        #endregion Constructors
    }
}
