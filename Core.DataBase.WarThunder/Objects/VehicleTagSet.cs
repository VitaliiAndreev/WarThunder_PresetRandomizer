using Core.DataBase.Enumerations;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Enumerations.DataBase;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Json;
using Core.DataBase.WarThunder.Objects.Json.Interfaces;
using NHibernate.Mapping.Attributes;

namespace Core.DataBase.WarThunder.Objects
{
    /// <summary> A set of vehicle tags. </summary>
    [Class(Table = ETable.VehicleTagSet)]
    public class VehicleTagSet : PersistentWarThunderObjectWithId, IVehicleTagSet
    {
        #region Persistent Properties

        /// <summary> The tag set's ID. </summary>
        [Id(Column = EColumn.Id, TypeType = typeof(long), Name = nameof(Id), Generator = EIdGenerator.HiLo)]
        public override long Id { get; protected set; }

        [Property()] public virtual bool NotAvailableInDynamicCampaign { get; protected set; }

        [Property()] public virtual bool IsAirVehicle { get; protected set; }

        [Property()] public virtual bool IsAllied { get; protected set; }

        [Property()] public virtual bool IsFighter { get; protected set; }

        [Property()] public virtual bool IsJetFighter { get; protected set; }

        [Property()] public virtual bool IsAmerican { get; protected set; }

        [Property()] public virtual bool UsedInPacific { get; protected set; }

        [Property()] public virtual bool IsSoviet { get; protected set; }

        [Property()] public virtual bool UsedInKorea { get; protected set; }

        [Property()] public virtual bool UsedOnWesternFront { get; protected set; }

        [Property()] public virtual bool UsedOnEasternFront { get; protected set; }

        [Property()] public virtual bool IsFrench { get; protected set; }

        [Property()] public virtual bool IsBritish { get; protected set; }

        [Property()] public virtual bool IsGerman { get; protected set; }

        [Property()] public virtual bool IsAxis { get; protected set; }

        [Property()] public virtual bool IsInterceptor { get; protected set; }

        [Property()] public virtual bool IsHeavyFighter { get; protected set; }

        [Property()] public virtual bool UsedInMediterranean { get; protected set; }

        [Property()] public virtual bool UsedAtBerlin { get; protected set; }

        [Property()] public virtual bool UsedOnFarEasternFront { get; protected set; }

        [Property()] public virtual bool IsNavalFighter { get; protected set; }

        [Property()] public virtual bool UsedOnKoreanFront { get; protected set; }

        [Property()] public virtual bool IsUtilityHelicopter { get; protected set; }

        [Property()] public virtual bool IsAttackHelicopter { get; protected set; }

        [Property()] public virtual bool IsItalian { get; protected set; }

        [Property()] public virtual bool IsJapanese { get; protected set; }

        [Property()] public virtual bool IsStrikeFighter { get; protected set; }

        [Property()] public virtual bool CanTakeOffFromCarrier { get; protected set; }

        [Property()] public virtual bool IsBomber { get; protected set; }

        [Property()] public virtual bool IsMediumBomber { get; protected set; }

        [Property()] public virtual bool IsFrontlineBomber { get; protected set; }

        [Property()] public virtual bool HasBomberView { get; protected set; }

        [Property()] public virtual bool UsedInBritain { get; protected set; }

        [Property()] public virtual bool UsedAtMalta { get; protected set; }

        [Property()] public virtual bool UsedAtSicily { get; protected set; }

        [Property()] public virtual bool UsedInBattleOfBulge { get; protected set; }

        [Property()] public virtual bool UsedInRuhr { get; protected set; }

        [Property()] public virtual bool CanCarryTorpedoes { get; protected set; }

        [Property()] public virtual bool IsAttacker { get; protected set; }

        [Property()] public virtual bool IsJetBomber { get; protected set; }

        [Property()] public virtual bool IsLongRangeBomber { get; protected set; }

        [Property()] public virtual bool IsHeavyBomber { get; protected set; }

        [Property()] public virtual bool UsedAtIwoJima { get; protected set; }

        [Property()] public virtual bool UsedInBattleOfMidway { get; protected set; }

        [Property()] public virtual bool IsMediumFighter { get; protected set; }

        [Property()] public virtual bool UsedAtKorsun { get; protected set; }

        [Property()] public virtual bool IsLightBomber { get; protected set; }

        [Property()] public virtual bool IsFw190 { get; protected set; }

        [Property()] public virtual bool UsedAtStalingrad { get; protected set; }

        [Property()] public virtual bool IsLightFighter { get; protected set; }

        [Property()] public virtual bool IsBiplane { get; protected set; }

        [Property()] public virtual bool UsedAtKrymsk { get; protected set; }

        [Property()] public virtual bool IsChinese { get; protected set; }

        [Property()] public virtual bool IsDiveBomber { get; protected set; }

        [Property()] public virtual bool IsNightFighter { get; protected set; }

        [Property()] public virtual bool IsNavalBomber { get; protected set; }

        [Property()] public virtual bool UsedAtGuadalcanal { get; protected set; }

        [Property()] public virtual bool IsHydroplane { get; protected set; }

        [Property()] public virtual bool CannotTakeoff { get; protected set; }

        [Property()] public virtual bool UsedAtHonolulu { get; protected set; }

        [Property()] public virtual bool UsedAtWakeIsland { get; protected set; }

        [Property()] public virtual bool UsedAtKhalkinGol { get; protected set; }

        [Property()] public virtual bool IsAustralian { get; protected set; }

        [Property()] public virtual bool IsSwedish { get; protected set; }

        [Property()] public virtual bool UsedInChina { get; protected set; }

        [Property()] public virtual bool UsedAtPortMoresby { get; protected set; }

        [Property()] public virtual bool UsedInGuam { get; protected set; }

        [Property()] public virtual bool IsTank { get; protected set; }

        [Property()] public virtual bool IsMediumTank { get; protected set; }

        [Property()] public virtual bool IsLightTank { get; protected set; }

        [Property()] public virtual bool CanScout { get; protected set; }

        [Property()] public virtual bool CanRepairTeammates { get; protected set; }

        [Property()] public virtual bool IsHeavyTank { get; protected set; }

        [Property()] public virtual bool IsTankDestroyer { get; protected set; }

        [Property()] public virtual bool IsMissileTank { get; protected set; }

        [Property()] public virtual bool IsSpaa { get; protected set; }

        [Property()] public virtual bool HasProximityFuseRocket { get; protected set; }

        [Property()] public virtual bool IsShip { get; protected set; }

        [Property()] public virtual bool IsCarrier { get; protected set; }

        [Property()] public virtual bool HasMaximumRatio { get; protected set; }

        [Property()] public virtual bool IsArmoredBoat { get; protected set; }

        [Property()] public virtual bool IsHeavyBoat { get; protected set; }

        [Property()] public virtual bool IsArmoredSubmarineChaser { get; protected set; }

        [Property()] public virtual bool IsBoat { get; protected set; }

        [Property()] public virtual bool IsTorpedoBoat { get; protected set; }

        [Property()] public virtual bool IsGunBoat { get; protected set; }

        [Property()] public virtual bool IsFerry { get; protected set; }

        [Property()] public virtual bool IsBarge { get; protected set; }

        [Property()] public virtual bool IsTorpedoGunBoat { get; protected set; }

        [Property()] public virtual bool IsDestroyer { get; protected set; }

        [Property()] public virtual bool IsMinelayer { get; protected set; }

        [Property()] public virtual bool IsHydrofoilTorpedoBoat { get; protected set; }

        [Property()] public virtual bool IsMinesweeper { get; protected set; }

        [Property()] public virtual bool IsMissileBoat { get; protected set; }

        [Property()] public virtual bool IsAaFerry { get; protected set; }

        [Property()] public virtual bool IsSubmarineChaser { get; protected set; }

        [Property()] public virtual bool IsCruiser { get; protected set; }

        [Property()] public virtual bool IsHeavyCruiser { get; protected set; }

        [Property()] public virtual bool IsLightCruiser { get; protected set; }

        [Property()] public virtual bool IsHeavyGunBoat { get; protected set; }

        [Property()] public virtual bool IsSmallSubmarineChaser { get; protected set; }

        #endregion Persistent Properties
        #region Association Properties

        /// <summary> The vehicle the set belongs to. </summary>
        [ManyToOne(0, Column = ETable.Vehicle + "_" + EColumn.Id, ClassType = typeof(Vehicle), NotNull = true, Lazy = Laziness.Proxy)]
        [Key(1, Unique = true, Column = ETable.Vehicle + "_" + EColumn.Id)]
        public virtual IVehicle Vehicle { get; protected set; }

        #endregion Association Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected VehicleTagSet()
        {
        }

        /// <summary> Creates a new tag set. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="vehicle"> The set's vehicle. </param>
        public VehicleTagSet(IDataRepository dataRepository, IVehicle vehicle)
            : this(dataRepository, -1L, vehicle)
        {
        }

        /// <summary> Creates a new tag set. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The set's ID. </param>
        /// <param name="vehicle"> The set's vehicle. </param>
        public VehicleTagSet(IDataRepository dataRepository, long id, IVehicle vehicle)
            : base(dataRepository, id)
        {
            Vehicle = vehicle;

            LogCreation();
        }

        #endregion Constructors
        #region Methods: Overrides

        /// <summary> Fills properties of the object with values deserialized from JSON data. </summary>
        /// <param name="instanceDeserializedFromJson"> The temporary non-persistent object storing deserialized data. </param>
        public override void InitializeWithDeserializedJson(IDeserializedFromJsonWithGaijinId instanceDeserializedFromJson)
        {
            base.InitializeWithDeserializedJson(instanceDeserializedFromJson);

            if (instanceDeserializedFromJson is VehicleDeserializedFromJsonUnitTags additionalVehicleData)
            {
                var tags = additionalVehicleData.Tags;

                UsedOnWesternFront = tags.UsedOnWesternFront || tags.UsedOnWesternFront_;
                UsedOnEasternFront = tags.UsedOnEasternFront || tags.UsedOnEasternFront_;
                UsedAtStalingrad = tags.UsedAtStalingrad || tags.UsedAtStalingrad_;
                UsedInMediterranean = tags.UsedInMediterranean || tags.UsedInMediterranean_;

                IsHydroplane = tags.IsHydroplane || tags.IsHydroplane_;

                IsBoat = tags.IsBoat || tags.IsBoat_;
                IsDestroyer = tags.IsDestroyer || tags.IsDestroyer_;
                IsCruiser = tags.IsCruiser || tags.IsCruiser_;
                IsLightCruiser = tags.IsLightCruiser || tags.IsLightCruiser_;
            }
        }

        #endregion Methods: Overrides
    }
}