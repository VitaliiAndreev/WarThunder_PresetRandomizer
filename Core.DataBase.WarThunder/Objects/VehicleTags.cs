using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects
{
    public abstract class VehicleTags : PersistentObjectWithIdAndVehicle, IVehicleTags
    {
        #region Fields

        protected readonly IDictionary<EVehicleBranchTag, bool> _index;

        #endregion Fields
        #region Properties

        public abstract IEnumerable<EVehicleBranchTag> All { get; protected set; }

        #endregion Properties
        #region Indexers

        public virtual bool this[EVehicleBranchTag tag]
        {
            get => _index.TryGetValue(tag, out var isTagged) && isTagged;
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
        public VehicleTags(IDataRepository dataRepository, IVehicle vehicle)
            : this(dataRepository, -1L, vehicle)
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The objects's ID. </param>
        /// <param name="vehicle"> The vehicle this object belongs to. </param>
        public VehicleTags(IDataRepository dataRepository, long id, IVehicle vehicle)
            : base(dataRepository, id, vehicle)
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