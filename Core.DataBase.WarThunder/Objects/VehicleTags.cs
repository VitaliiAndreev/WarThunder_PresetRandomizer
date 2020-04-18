using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects
{
    public abstract class VehicleTags : PersistentObjectWithId, IVehicleTags
    {
        #region Fields

        protected readonly IDictionary<EVehicleBranchTag, bool> _index;

        #endregion Fields
        #region Indexers

        public virtual bool this[EVehicleBranchTag tag]
        {
            get => _index.TryGetValue(tag, out var isTagged) ? isTagged : false;
        }

        public virtual bool this[IEnumerable<EVehicleBranchTag> tags]
        {
            get
            {
                foreach (var tag in tags)
                {
                    if (this[tag])
                        return true;
                }
                return false;
            }
        }

        #endregion Indexers
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected VehicleTags()
        {
            _index = new Dictionary<EVehicleBranchTag, bool>();
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="vehicle"> The vehicle this object belongs to. </param>
        /// <param name="deserializedTags"> Vehicle tags deserialized from JSON data to initialize this instance with. </param>
        public VehicleTags(IDataRepository dataRepository)
            : this(dataRepository, -1L)
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The vehicle's ID. </param>
        /// <param name="vehicle"> The vehicle this object belongs to. </param>
        /// <param name="deserializedTags"> Vehicle tags deserialized from JSON data to initialize this instance with. </param>
        public VehicleTags(IDataRepository dataRepository, long id)
            : base(dataRepository, id)
        {
            _index = new Dictionary<EVehicleBranchTag, bool>();
        }

        #endregion Constructors
        #region Methods: Overrides

        public override void InitializeNonPersistentFields(IDataRepository dataRepository)
        {
            base.InitializeNonPersistentFields(dataRepository);

            InitialiseIndex();
        }

        #endregion Methods: Overrides

        protected abstract void InitialiseIndex();
    }
}