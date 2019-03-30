using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects;
using Core.DataBase.Objects.Interfaces;
using Core.Enumerations.DataBase;
using Core.Extensions;
using Core.Objects.Interfaces;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;

namespace Core.Objects
{
    /// <summary> A nation's military branch. </summary>
    [Class(Table = ETable.Branch)]
    public class Branch : PersistentObjectWithIdAndName, IBranch
    {
        #region Fields

        /// <summary>
        /// The branch's nation.
        /// This field is used by the <see cref="Nation"/> property.
        /// </summary>
        private INation _nation;

        #endregion Fields
        #region Persistent Properties

        /// <summary> The branch's ID. </summary>
        [Id(Column = EColumn.Id, TypeType = typeof(Guid), Name = nameof(Id))]
        public override Guid Id
        {
            get { return _id; }
            protected set { _id = value; }
        }

        /// <summary> The branch's Name. </summary>
        [Property(NotNull = true)]
        public override string Name
        {
            get { return _name; }
            protected set { _name = value; }
        }

        /// <summary> The branch's nation. </summary>
        [ManyToOne(0, Column = ETable.Nation + "_" + EColumn.Id, ClassType = typeof(Nation), Lazy = Laziness.False, NotNull = true)]
        [Key(1)]
        public virtual INation Nation
        {
            get { return _nation; }
            protected set { _nation = value; }
        }

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
        /// <param name="name"> The branch's Name. </param>
        /// <param name="nation"> The branch's nation. </param>
        public Branch(IDataRepository dataRepository, string name, INation nation)
            : this(dataRepository, Guid.NewGuid(), name, nation)
        {
        }

        /// <summary> Creates a new nation. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The branch's ID. </param>
        /// <param name="name"> The branch's Name. </param>
        /// <param name="nation"> The branch's nation. </param>
        public Branch(IDataRepository dataRepository, Guid id, string name, INation nation)
            : base(dataRepository, id, name)
        {
            _nation = nation;

            LogCreation();
        }

        #endregion Constructors

        /// <summary> Returns a string that represents the instance. </summary>
        /// <returns></returns>
        public override string ToString() => $"{base.ToString()} of {_nation?.ToString() ?? "?"})";

        /// <summary> Returns all persistent objects nested in the instance. This method requires overriding implementation to function. </summary>
        /// <returns></returns>
        public override IEnumerable<IPersistentObject> GetAllNestedObjects() => new List<IPersistentObject> { Nation };
    }
}