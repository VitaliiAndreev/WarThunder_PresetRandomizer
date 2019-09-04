using Core.DataBase.Enumerations;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Enumerations.DataBase;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.VehicleGameModeParameterSets;
using NHibernate.Mapping;
using NHibernate.Mapping.Attributes;

namespace Core.DataBase.WarThunder.Objects.VehicleGameModeParameterSet.Integer
{
    /// <summary> A set of nullable integer parameters that vary depending on the game mode. </summary>
    [Class(Table = ETable.VehicleNumberOfSpawns)]
    public class NumberOfSpawns: VehicleGameModeParameterSetBase, IVehicleGameModeParameterSet<int?>
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
                Arcade = (int?)_arcade;
            }
        }

        /// <summary> An internal value used during initialization. </summary>
        protected internal override object InternalRealistic
        {
            get => _realistic;
            set
            {
                _realistic = value;
                Realistic = (int?)_realistic;
            }
        }

        /// <summary> An internal value used during initialization. </summary>
        protected internal override object InternalSimulator
        {
            get => _simulator;
            set
            {
                _simulator = value;
                Simulator = (int?)_simulator;
            }
        }

        /// <summary> An internal value used during initialization. </summary>
        protected internal override object InternalEvent
        {
            get => _event;
            set
            {
                _event = value;
                Event = (int?)_event;
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
        public virtual int? Arcade { get; protected set; }

        /// <summary> The value in Realistic Battles. </summary>
        [Property(NotNull = false)]
        public virtual int? Realistic { get; protected set; }

        /// <summary> The value in Simulator Battles. </summary>
        [Property(NotNull = false)]
        public virtual int? Simulator { get; protected set; }

        /// <summary> The value in Event Battles. </summary>
        [Property(NotNull = false)]
        public virtual int? Event { get; protected set; }

        #endregion Persistent Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected NumberOfSpawns()
        {
        }

        /// <summary> Creates a new set of values. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="vehicle"> The set's vehicle. </param>
        public NumberOfSpawns(IDataRepository dataRepository, IVehicle vehicle)
            : this(dataRepository, vehicle, null, null, null, null)
        {
        }

        /// <summary> Creates a new set of values. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The set's ID. </param>
        /// <param name="vehicle"> The set's vehicle. </param>
        public NumberOfSpawns(IDataRepository dataRepository, long id, IVehicle vehicle)
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
        public NumberOfSpawns(IDataRepository dataRepository, IVehicle vehicle, int? valueInArcade, int? valueInRealistic, int? valueInSimulator, int? valueInEvent)
            : this(dataRepository, -1L, vehicle, valueInArcade, valueInRealistic, valueInSimulator, valueInEvent)
        {
        }

        /// <summary> Creates a new set of values. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The set's ID. </param>
        /// <param name="vehicle"> The set's vehicle. </param>
        public NumberOfSpawns(IDataRepository dataRepository, long id, IVehicle vehicle, int? valueInArcade, int? valueInRealistic, int? valueInSimulator, int? valueInEvent)
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

        /// <summary> Return value of the game mode parameter corresponding to the given enumeration value. </summary>
        /// <param name="gameMode"> The game mode the value for which to get. </param>
        /// <returns></returns>
        public virtual int? this[EGameMode gameMode]
        {
            get
            {
                switch (gameMode)
                {
                    case EGameMode.Arcade:
                        return Arcade;
                    case EGameMode.Realistic:
                        return Realistic;
                    case EGameMode.Simulator:
                        return Simulator;
                    case EGameMode.Event:
                        return Event;
                    default:
                        return null;
                }
            }
        }
    }
}