using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects.Interfaces;
using Core.Extensions;

namespace Core.DataBase.Objects
{
    /// <summary> A persistent (stored in a database) object that has an ID and a name. </summary>
    public abstract class PersistentObjectWithIdAndName : PersistentObjectWithId, IPersistentObjectWithIdAndName
    {
        #region Fields

        /// <summary>
        /// The object's name.
        /// This field is used by the <see cref="Name"/> property.
        /// </summary>
        protected string _name;

        #endregion Fields
        #region Properties

        /// <summary> The object's name. </summary>
        public virtual string Name
        {
            get { return _name; }
            protected set { _name = value; }
        }

        #endregion Properties
        #region Constructors

        /// <summary>
        /// Creates a new transient object that can be persisted later.
        /// This constructor is used to maintain inheritance of class composition required for NHibernate mapping.
        /// </summary>
        protected PersistentObjectWithIdAndName()
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="name"> The object's name. </param>
        protected PersistentObjectWithIdAndName(IDataRepository dataRepository, string name)
            : base(dataRepository)
        {
            _name = name;

            LogCreation();
        }

        #endregion Constructors

        /// <summary> Returns a string that represents the instance. </summary>
        /// <returns></returns>
        public override string ToString() => $"{base.ToString().SkipLast(1)}, Name: \"{_name}\")";
    }
}
