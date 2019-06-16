using Core.DataBase.Enumerations;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Enumerations.DataBase;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.VehicleGameModeParameterSets;
using NHibernate.Mapping;
using NHibernate.Mapping.Attributes;

namespace Core.DataBase.WarThunder.Objects.VehicleGameModeParameterSet.Decimal
{
    /// <summary> A set of nullable integer parameters that vary depending on the game mode. </summary>
    [Class(Table = ETable.VehicleVisualRewardMultiplier)]
    public class VisualRewardMultiplier: VehicleGameModeParameterSetBase, IVehicleGameModeParameterSet<decimal?>
    {
        #region Fields

        /// <summary> An internal value used during initialization. </summary>
        private object _arcade;

        /// <summary> An internal value used during initialization. </summary>
        private object _realistic;

        /// <summary> An internal value used during initialization. </summary>
        private object _simulator;

        /// <summary> An internal value used during initialization. </summary>
        private object _event;

        #endregion Fields
        #region Internal Properties

        /// <summary> An internal value used during initialization. </summary>
        protected internal override object InternalArcade
        {
            get => _arcade;
            set
            {
                _arcade = value;
                Arcade = (decimal?)_arcade;
            }
        }

        /// <summary> An internal value used during initialization. </summary>
        protected internal override object InternalRealistic
        {
            get => _realistic;
            set
            {
                _realistic = value;
                Realistic = (decimal?)_realistic;
            }
        }

        /// <summary> An internal value used during initialization. </summary>
        protected internal override object InternalSimulator
        {
            get => _simulator;
            set
            {
                _simulator = value;
                Simulator = (decimal?)_simulator;
            }
        }

        /// <summary> An internal value used during initialization. </summary>
        protected internal override object InternalEvent
        {
            get => _event;
            set
            {
                _event = value;
                Event = (decimal?)_event;
            }
        }

        #endregion Internal Properties
        #region Persistent Properties

        /// <summary> The object's ID. </summary>
        [Id(Column = EColumn.Id, TypeType = typeof(long), Name = nameof(Id), Generator = EIdGenerator.HiLo)]
        public override long Id { get; protected set; }

        /// <summary> The vehicle this set belongs to. </summary>
        [ManyToOne(0, Column = ETable.Vehicle + "_" + EColumn.GaijinId, ClassType = typeof(Vehicle), Lazy = Laziness.False, NotNull = true)]
        [Key(1, Unique = true, Column = ETable.Vehicle + "_" + EColumn.GaijinId)]
        public override IVehicle Vehicle { get; protected set; }

        /// <summary> The value in Arcade Battles. </summary>
        [Property(NotNull = false)]
        public virtual decimal? Arcade { get; protected set; }

        /// <summary> The value in Realistic Battles. </summary>
        [Property(NotNull = false)]
        public virtual decimal? Realistic { get; protected set; }

        /// <summary> The value in Simulator Battles. </summary>
        [Property(NotNull = false)]
        public virtual decimal? Simulator { get; protected set; }

        /// <summary> The value in Event Battles. </summary>
        [Property(NotNull = false)]
        public virtual decimal? Event { get; protected set; }

        #endregion Persistent Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate deserialized data read from a database. </summary>
        protected VisualRewardMultiplier()
        {
        }

        /// <summary> Creates a new set of values. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="vehicle"> The set's vehicle. </param>
        public VisualRewardMultiplier(IDataRepository dataRepository, IVehicle vehicle)
            : this(dataRepository, vehicle, null, null, null, null)
        {
        }

        /// <summary> Creates a new set of values. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The set's ID. </param>
        /// <param name="vehicle"> The set's vehicle. </param>
        public VisualRewardMultiplier(IDataRepository dataRepository, long id, IVehicle vehicle)
            : this(dataRepository, id, vehicle, null, null, null, null)
        {
        }

        /// <summary> Creates a new set of values. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="vehicle"> The set's vehicle. </param>
        /// <param name="valueInArcade"> The value in Arcade Battles. </param>
        /// <param name="valueInRealistic"> The value in Realistic Battles. </param>
        /// <param name="valueInSimulator"> The value in Simulator Battles. </param>
        /// <param name="valueInEvent"> The value in Event Battles. </param>
        public VisualRewardMultiplier(IDataRepository dataRepository, IVehicle vehicle, decimal? valueInArcade, decimal? valueInRealistic, decimal? valueInSimulator, decimal? valueInEvent)
            : this(dataRepository, -1L, vehicle, valueInArcade, valueInRealistic, valueInSimulator, valueInEvent)
        {
        }

        /// <summary> Creates a new set of values. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The set's ID. </param>
        /// <param name="vehicle"> The set's vehicle. </param>
        public VisualRewardMultiplier(IDataRepository dataRepository, long id, IVehicle vehicle, decimal? valueInArcade, decimal? valueInRealistic, decimal? valueInSimulator, decimal? valueInEvent)
            : base(dataRepository, id)
        {
            Vehicle = vehicle;

            Arcade = valueInArcade;
            Realistic = valueInRealistic;
            Simulator = valueInSimulator;
            Event = valueInEvent;

            LogCreation();
        }

        #endregion Constructors
    }
}