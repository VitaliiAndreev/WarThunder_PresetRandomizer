using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Objects.Interfaces;

namespace Core.DataBase.WarThunder.Objects
{
    public abstract class PersistentDeserialisedObjectWithIdAndVehicle : PersistentDeserialisedObjectWithId, IPersistentDeserialisedObjectWithIdAndVehicle
    {
        #region Properties

        public abstract IVehicle Vehicle { get; protected set; }

        #endregion Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected PersistentDeserialisedObjectWithIdAndVehicle()
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The object's ID. </param>
        /// <param name="vehicle"> The vehicle this object belongs to. </param>
        public PersistentDeserialisedObjectWithIdAndVehicle(IDataRepository dataRepository, long id, IVehicle vehicle)
            : base(dataRepository, id)
        {
            Vehicle = vehicle;
        }

        #endregion Constructors
        #region Methods: Overrides

        public override string ToString()
        {
            return $"{base.ToString()} of {Vehicle}";
        }

        #endregion Methods: Overrides
    }
}