using Core.DataBase.Enumerations;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Enumerations.DataBase;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Json;
using Core.Extensions;
using NHibernate.Mapping.Attributes;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects
{
    /// <summary> A nation (with their own research trees) in the game. </summary>
    [Class(Table = ETable.Nation)]
    public class Nation : PersistentObjectWithIdAndGaijinId, INation
    {
        #region Persistent Properties

        /// <summary> The nation's ID. </summary>
        [Id(Column = EColumn.Id, TypeType = typeof(long), Name = nameof(Id), Generator = EIdGenerator.HiLo)] // The type and name of the identificator column have to be explicitly specified.
        public override long Id { get; protected set; }

        /// <summary> The nation's Gaijin ID. </summary>
        [Property(NotNull = true, Unique = true)]
        public override string GaijinId { get; protected set; }

        #endregion Persistent Properties
        #region Association Properties

        /// <summary> The nation's military branches. </summary>
        [Bag(0, Name = nameof(Branches), Lazy = CollectionLazy.False, Inverse = true, Generic = true)]
        [Key(1, Column = ETable.Nation + "_" + EColumn.Id, NotNull = true)]
        [OneToMany(1, ClassType = typeof(Branch))]
        public virtual IEnumerable<IBranch> Branches { get; protected internal set; } = new List<IBranch>();

        /// <summary> The nation's vehicles. </summary>
        [Bag(0, Name = nameof(Vehicles), Lazy = CollectionLazy.False, Inverse = true, Generic = true)]
        [Key(1, Column = ETable.Nation + "_" + EColumn.Id, NotNull = true)]
        [OneToMany(1, ClassType = typeof(Vehicle))]
        public virtual IEnumerable<IVehicle> Vehicles { get; protected set; } = new List<IVehicle>();

        #endregion Association Properties
        #region Non-Persistent Properties

        /// <summary> Parses the Gaijin ID of the nation as an item of <see cref="ENation"/>. </summary>
        public virtual ENation AsEnumerationItem => GaijinId.ParseEnumeration<ENation>();

        #endregion Non-Persistent Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected Nation()
        {
        }

        /// <summary> Creates a new nation. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="gaijinId"> The nation's Gaijin ID. </param>
        public Nation(IDataRepository dataRepository, string gaijinId)
            : this(dataRepository, -1L, gaijinId)
        {
        }

        /// <summary> Creates a new nation. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="instanceDerializedFromJson"> A non-persistent instance deserialized from JSON data to initialize this instance with. </param>
        public Nation(IDataRepository dataRepository, NationDeserializedFromJson instanceDerializedFromJson)
            : this(dataRepository, -1L, instanceDerializedFromJson.GaijinId)
        {
        }

        /// <summary> Creates a new nation. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The nation's ID. </param>
        /// <param name="gaijinId"> The nation's Gaijin ID. </param>
        public Nation(IDataRepository dataRepository, long id, string gaijinId)
            : base(dataRepository, id, gaijinId)
        {
            LogCreation();
        }

        #endregion Constructors

        /// <summary> Returns all persistent objects nested in the instance. This method requires overriding implementation to function. </summary>
        /// <returns></returns>
        public override IEnumerable<IPersistentObject> GetAllNestedObjects()
        {
            var nestedObjects = new List<IPersistentObject>();

            nestedObjects.AddRange(Branches);
            nestedObjects.AddRange(Vehicles);

            return nestedObjects;
        }
    }
}