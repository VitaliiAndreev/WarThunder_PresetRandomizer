using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects;
using Core.Enumerations.DataBase;
using Core.Objects.Interfaces;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;

namespace Core.Objects
{
    /// <summary> A nation in the game. </summary>
    [Class(Table = ETable.Nation)]
    public class Nation : PersistentObjectWithIdAndGaijinId, INation
    {
        #region Persistent Properties

        /// <summary> The nation's ID. </summary>
        [Id(Column = EColumn.Id, TypeType = typeof(long), Name = nameof(Id), Generator = "hilo")] // The type and name of the identificator column have to be explicitly specified.
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

        #endregion Association Properties
        #region Constructors

        /// <summary>
        /// Creates a new transient object that can be persisted later.
        /// This constructor is used by NHibernate to instantiate deserialized data read from a database.
        /// </summary>
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
        public override IEnumerable<IPersistentObject> GetAllNestedObjects() => Branches;
    }
}