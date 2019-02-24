using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects.Interfaces;
using System;

namespace Core.DataBase.Objects
{
    /// <summary> A persistent (stored in a database) object that has an ID. </summary>
    public abstract class PersistentObjectWithId : PersistentObject, IPersistentObjectWithId
    {
        #region Fields

        /// <summary>
        /// The object's ID.
        /// This field is used by the <see cref="Id"/> property.
        /// </summary>
        protected Guid _id;

        #endregion Fields
        #region Properties

        /// <summary> The object's ID. </summary>
        public virtual Guid Id
        {
            get { return _id; }
            protected set { _id = value; }
        }

        #endregion Properties
        #region Constructors

        /// <summary>
        /// Creates a new transient object that can be persisted later.
        /// This constructor is used to maintain inheritance of class composition required for NHibernate mapping.
        /// </summary>
        protected PersistentObjectWithId()
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        protected PersistentObjectWithId(IDataRepository dataRepository)
            : base(dataRepository)
        {
            _id = Guid.NewGuid();

            LogCreation();
        }

        #endregion Constructors

        /// <summary> Returns a string that represents the instance. </summary>
        /// <returns></returns>
        public override string ToString() => $"{base.ToString()} (ID: \"{_id}\")";
    }
}