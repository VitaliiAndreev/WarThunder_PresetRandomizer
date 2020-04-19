using Core.DataBase.Enumerations;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects;
using Core.DataBase.WarThunder.Enumerations.DataBase;
using Core.DataBase.WarThunder.Objects.Interfaces;
using NHibernate.Mapping.Attributes;

namespace Core.DataBase.WarThunder.Objects
{
    [Class(Table = ETable.VehicleImages)]
    public class VehicleImages : PersistentObjectWithId, IVehicleImages
    {
        #region Persistent Properties

        /// <summary> The vehicle's ID. </summary>
        [Id(Column = EColumn.Id, TypeType = typeof(long), Name = nameof(Id), Generator = EIdGenerator.HiLo)]
        public override long Id { get; protected set; }

        [Property()] public virtual byte[] IconBytes { get; protected set; }

        [Property()] public virtual byte[] PortraitBytes { get; protected set; }

        #endregion Persistent Properties
        #region Association Properties

        /// <summary> The vehicle the data set belongs to. </summary>
        [ManyToOne(0, Column = ETable.Vehicle + "_" + EColumn.Id, ClassType = typeof(Vehicle), NotNull = true, Lazy = Laziness.Proxy)]
        [Key(1, Unique = true, Column = ETable.Vehicle + "_" + EColumn.Id)]
        public virtual IVehicle Vehicle { get; protected set; }

        #endregion Association Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected VehicleImages()
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="vehicle"> The vehicle this object belongs to. </param>
        /// <param name="deserializedTags"> Vehicle tags deserialized from JSON data to initialize this instance with. </param>
        public VehicleImages(IDataRepository dataRepository, IVehicle vehicle)
            : this(dataRepository, -1L, vehicle)
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The vehicle's ID. </param>
        /// <param name="vehicle"> The vehicle this object belongs to. </param>
        /// <param name="deserializedTags"> Vehicle tags deserialized from JSON data to initialize this instance with. </param>
        public VehicleImages(IDataRepository dataRepository, long id, IVehicle vehicle)
            : base(dataRepository, id)
        {
            Vehicle = vehicle;

            LogCreation();
        }

        #endregion Constructors
        #region Methods: Initialisation

        public virtual void SetIcon(byte[] bytes)
        {
            IconBytes = bytes;
        }

        public virtual void SetPortrait(byte[] bytes)
        {
            PortraitBytes = bytes;
        }

        #endregion Methods: Initialisation
    }
}