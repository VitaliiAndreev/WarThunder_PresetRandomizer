using Core.DataBase.Enumerations;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.VehicleGameModeParameterSets;
using Core.Enumerations.DataBase;
using NHibernate.Mapping;
using NHibernate.Mapping.Attributes;

namespace Core.DataBase.WarThunder.Objects.VehicleGameModeParameterSet.String
{
    /// <summary> A set of string parameters that vary depending on the game mode. </summary>
    [Class(Table = ETable.VehicleBattleRating)]
    public class BattleRating : VehicleGameModeParameterSetBase, IVehicleGameModeParameterSet<string>
    {
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
        public virtual string Arcade { get; protected set; }

        /// <summary> The value in Realistic Battles. </summary>
        [Property(NotNull = false)]
        public virtual string Realistic { get; protected set; }

        /// <summary> The value in Simulator Battles. </summary>
        [Property(NotNull = false)]
        public virtual string Simulator { get; protected set; }

        /// <summary> The value in Event Battles. </summary>
        [Property(NotNull = false)]
        public virtual string Event { get; protected set; }

        #endregion Persistent Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate deserialized data read from a database. </summary>
        protected BattleRating()
        {
        }

        /// <summary> Creates a new set of values. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="vehicle"> The set's vehicle. </param>
        public BattleRating(IDataRepository dataRepository, IVehicle vehicle)
            : this(dataRepository, vehicle, null, null, null, null)
        {
        }

        /// <summary> Creates a new set of values. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The set's ID. </param>
        /// <param name="vehicle"> The set's vehicle. </param>
        public BattleRating(IDataRepository dataRepository, long id, IVehicle vehicle)
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
        public BattleRating(IDataRepository dataRepository, IVehicle vehicle, string valueInArcade, string valueInRealistic, string valueInSimulator, string valueInEvent)
            : this(dataRepository, -1L, vehicle, valueInArcade, valueInRealistic, valueInSimulator, valueInEvent)
        {
        }

        /// <summary> Creates a new set of values. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The set's ID. </param>
        /// <param name="vehicle"> The set's vehicle. </param>
        public BattleRating(IDataRepository dataRepository, long id, IVehicle vehicle, string valueInArcade, string valueInRealistic, string valueInSimulator, string valueInEvent)
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