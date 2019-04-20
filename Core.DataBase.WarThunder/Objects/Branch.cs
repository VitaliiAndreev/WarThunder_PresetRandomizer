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
    /// <summary> A nation's military branch. </summary>
    [Class(Table = ETable.Branch)]
    public class Branch : PersistentObjectWithIdAndGaijinId, IBranch
    {
        #region Persistent Properties

        /// <summary> The branch's ID. </summary>
        [Id(Column = EColumn.Id, TypeType = typeof(Guid), Name = nameof(Id))]
        public override Guid Id { get; protected set; }

        /// <summary> The branch's Gaijin ID. </summary>
        [Property(NotNull = true, Unique = true)]
        public override string GaijinId { get; protected set; }

        /// <summary> The branch's nation. </summary>
        [ManyToOne(0, Column = ETable.Nation + "_" + EColumn.Id, ClassType = typeof(Nation), Lazy = Laziness.False, NotNull = true)]
        [Key(1)]
        public virtual INation Nation { get; protected set; }

        #endregion Persistent Properties
        #region Constructors

        /// <summary>
        /// Creates a new transient object that can be persisted later.
        /// This constructor is used by NHibernate to instantiate deserialized data read from a database.
        /// </summary>
        protected Branch()
        {
        }

        /// <summary> Creates a new nation. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="gaijinId"> The branch's Gaijin ID. </param>
        /// <param name="nation"> The branch's nation. </param>
        public Branch(IDataRepository dataRepository, string gaijinId, INation nation)
            : this(dataRepository, Guid.NewGuid(), gaijinId, nation)
        {
        }

        /// <summary> Creates a new nation. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The branch's ID. </param>
        /// <param name="gaijinId"> The branch's Gaijin ID. </param>
        /// <param name="nation"> The branch's nation. </param>
        public Branch(IDataRepository dataRepository, Guid id, string gaijinId, INation nation)
            : base(dataRepository, id, gaijinId)
        {
            Nation = nation;

            LogCreation();
        }

        #endregion Constructors

        /// <summary> Returns a string that represents the instance. </summary>
        /// <returns></returns>
        public override string ToString() => $"{base.ToString()} of {Nation?.ToString() ?? "?"})";

        /// <summary> Returns all persistent objects nested in the instance. This method requires overriding implementation to function. </summary>
        /// <returns></returns>
        public override IEnumerable<IPersistentObject> GetAllNestedObjects() => new List<IPersistentObject> { Nation };
    }
}