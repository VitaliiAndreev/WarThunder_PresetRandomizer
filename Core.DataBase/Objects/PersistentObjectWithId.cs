using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects.Interfaces;
using System;

namespace Core.DataBase.Objects
{
    /// <summary> A persistent (stored in a database) object that has an ID. </summary>
    public abstract class PersistentObjectWithId : PersistentObject, IPersistentObjectWithId
    {
        #region Properties

        /// <summary> The object's ID. </summary>
        public virtual long Id { get; protected set; }
        // All persistent properties have to be public/protected virtual and have public/protected setters.
        // Even though we don't want to change IDs after creation,
        // a setter is required by NHibernate to initialize persistent properties of an object after reading data from a database and instantiating the object with the parameterless constructor.

        #endregion Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate deserialized data read from a database. </summary>
        protected PersistentObjectWithId()
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        protected PersistentObjectWithId(IDataRepository dataRepository)
            : this(dataRepository, -1L)
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The object's ID. </param>
        protected PersistentObjectWithId(IDataRepository dataRepository, long id)
            : base(dataRepository)
        {
            Id = id;
        }

        #endregion Constructors

        /// <summary> Returns a string that represents the instance. </summary>
        /// <returns></returns>
        public override string ToString() => $"{base.ToString()} ({Id})";
    }
}