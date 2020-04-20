using Core.DataBase.Enumerations;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Enumerations.DataBase;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.VehicleGameModeParameterSets;
using NHibernate.Mapping;
using NHibernate.Mapping.Attributes;

namespace Core.DataBase.WarThunder.Objects.VehicleGameModeParameterSet.Decimal
{
    /// <summary> A set of nullable decimal parameters that vary depending on the game mode. </summary>
    [Class(Table = ETable.VehicleRewardMultiplier)]
    public class RewardMultiplier: VehicleGameModeParameterSetBase, IVehicleGameModeParameterSet<decimal?>
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

        /// <summary> The entity this set belongs to. </summary>
        [ManyToOne(0, Column = ETable.VehicleEconomyData + "_" + EColumn.Id, ClassType = typeof(VehicleEconomyData), NotNull = true, Lazy = Laziness.Proxy)]
        [Key(1, Unique = true, Column = ETable.VehicleEconomyData + "_" + EColumn.Id)]
        public override IPersistentDeserialisedObjectWithId Entity { get; protected set; }

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

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected RewardMultiplier()
        {
        }

        /// <summary> Creates a new set of values. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="economyData"> The entity the set belongs to. </param>
        public RewardMultiplier(IDataRepository dataRepository, IVehicleEconomyData economyData)
            : this(dataRepository, economyData, null, null, null, null)
        {
        }

        /// <summary> Creates a new set of values. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The set's ID. </param>
        /// <param name="economyData"> The entity the set belongs to. </param>
        public RewardMultiplier(IDataRepository dataRepository, long id, IVehicleEconomyData economyData)
            : this(dataRepository, id, economyData, null, null, null, null)
        {
        }

        /// <summary> Creates a new set of values. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="economyData"> The entity the set belongs to. </param>
        /// <param name="valueInArcade"> The value in Arcade Battles. </param>
        /// <param name="valueInRealistic"> The value in Realistic Battles. </param>
        /// <param name="valueInSimulator"> The value in Simulator Battles. </param>
        /// <param name="valueInEvent"> The value in Event Battles. </param>
        public RewardMultiplier(IDataRepository dataRepository, IVehicleEconomyData economyData, decimal? valueInArcade, decimal? valueInRealistic, decimal? valueInSimulator, decimal? valueInEvent)
            : this(dataRepository, -1L, economyData, valueInArcade, valueInRealistic, valueInSimulator, valueInEvent)
        {
        }

        /// <summary> Creates a new set of values. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The set's ID. </param>
        /// <param name="economyData"> The entity the set belongs to. </param>
        /// <param name="valueInArcade"> The value in Arcade Battles. </param>
        /// <param name="valueInRealistic"> The value in Realistic Battles. </param>
        /// <param name="valueInSimulator"> The value in Simulator Battles. </param>
        /// <param name="valueInEvent"> The value in Event Battles. </param>
        public RewardMultiplier(IDataRepository dataRepository, long id, IVehicleEconomyData economyData, decimal? valueInArcade, decimal? valueInRealistic, decimal? valueInSimulator, decimal? valueInEvent)
            : base(dataRepository, id)
        {
            Entity = economyData;

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
        public virtual decimal? this[EGameMode gameMode]
        {
            get
            {
                return gameMode switch
                {
                    EGameMode.Arcade => Arcade,
                    EGameMode.Realistic => Realistic,
                    EGameMode.Simulator => Simulator,
                    EGameMode.Event => Event,
                    _ => null,
                };
            }
        }
    }
}