using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Linq;

namespace Core.DataBase.WarThunder.Objects
{
    /// <summary> A persistent (stored in a database) object that has an ID and a Gaijin ID. </summary>
    public abstract class PersistentObjectWithIdAndGaijinId : PersistentDeserialisedObjectWithId, IPersistentObjectWithIdAndGaijinId
    {
        #region Properties

        /// <summary> The object's Gaijin ID. </summary>
        public abstract string GaijinId { get; protected set; }

        #endregion Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected PersistentObjectWithIdAndGaijinId()
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="gaijinId"> The object's Gaijin ID. </param>
        protected PersistentObjectWithIdAndGaijinId(IDataRepository dataRepository, string gaijinId)
            : this(dataRepository, -1L, gaijinId)
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The object's ID. </param>
        /// <param name="gaijinId"> The object's Gaijin ID. </param>
        protected PersistentObjectWithIdAndGaijinId(IDataRepository dataRepository, long id, string gaijinId)
            : base(dataRepository, id)
        {
            GaijinId = gaijinId;
        }

        #endregion Constructors

        /// <summary> Returns a string that represents the instance. </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var baseRepresentation = base.ToString().Split(' ');
            var type = baseRepresentation.First();
            var id = baseRepresentation.Last();
            return $"{type} [{GaijinId}] {id}";
        }
    }
}