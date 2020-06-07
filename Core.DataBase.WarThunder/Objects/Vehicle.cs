using Core.DataBase.Enumerations;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Enumerations.DataBase;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Helpers;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Json;
using Core.DataBase.WarThunder.Objects.Localization.Vehicle;
using Core.DataBase.WarThunder.Objects.Localization.Vehicle.Interfaces;
using Core.DataBase.WarThunder.Objects.VehicleGameModeParameterSets;
using Core.Enumerations;
using Core.Extensions;
using NHibernate.Mapping;
using NHibernate.Mapping.Attributes;
using NHibernate.Type;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataBase.WarThunder.Objects
{
    /// <summary> A vehicle (air, ground, or sea). </summary>
    [Class(Table = ETable.Vehicle)]
    public class Vehicle : PersistentObjectWithIdAndGaijinId, IVehicle
    {
        #region Constants

        /// <summary> The string representing an unknown battle rating. </summary>
        private const string _unknownBattleRating = "?.?";

        /// <summary> The regular experession matching <see cref="_battleRatingFormat"/> to check validity of <see cref="BattleRating"/> values. </summary>
        public const string BattleRatingRegExPattern = "[1-9]{1}[0-9]{0,}.[037]{1}";

        #endregion Constants
        #region Persistent Properties

        #region General

        /// <summary> The vehicle's ID. </summary>
        [Id(Column = EColumn.Id, TypeType = typeof(long), Name = nameof(Id), Generator = EIdGenerator.HiLo)]
        public override long Id { get; protected set; }

        /// <summary> The vehicle's Gaijin ID. </summary>
        [Property(NotNull = true, Unique = true)]
        public override string GaijinId { get; protected set; }

        /// <summary> The vehicle's country of origin (not the nation in whose research tree the vehicles is in). </summary>
        [Property(NotNull = true, TypeType = typeof(EnumStringType<ECountry>))]
        public virtual ECountry Country { get; protected set; }

        /// <summary> The vehicle's broadly defined class with a distict icon. </summary>
        [Property(NotNull = true, TypeType = typeof(EnumStringType<EVehicleClass>))]
        public virtual EVehicleClass Class { get; protected internal set; }

        /// <summary> Indicates whether the vehicle can be unlocked for free with research. </summary>
        [Property(NotNull = true)]
        public virtual bool IsResearchable { get; protected set; }

        [Property(NotNull = true)]
        public virtual bool IsReserve { get; protected set; }

        /// <summary> Indicates whether the vehicle can be unlocked for free with research. </summary>
        [Property(NotNull = true)]
        public virtual bool IsPurchasableForGoldenEagles { get; protected set; }

        /// <summary> Indicates whether the vehicle can be unlocked with squadron research. </summary>
        [Property(NotNull = true)]
        public virtual bool IsSquadronVehicle { get; protected set; }

        [Property(NotNull = true)]
        public virtual bool IsSoldOnTheMarket { get; protected set; }

        [Property(NotNull = true)]
        public virtual bool IsSoldInTheStore { get; protected set; }

        /// <summary> Indicates whether the vehicle is premium or not. </summary>
        [Property(NotNull = true)]
        public virtual bool IsPremium { get; protected set; }

        /// <summary> Whether this vehicle is hidden from those that don't own it. </summary>
        [Property(NotNull = true)] public virtual bool IsHiddenUnlessOwned { get; protected set; }

        [Property(NotNull = true)] public virtual bool IsAvailableOnlyOnConsoles { get; protected set; }

        /// <summary> The category of hidden vehicles this one belongs to. </summary>
        [Property()] public virtual string CategoryOfHiddenVehicles { get; protected set; }

        /// <summary> Whether this vehicle is gifted to new players upon selecting their first vehicle branch and completing the tutorial. </summary>
        [Property()] public virtual bool GiftedToNewPlayersForSelectingTheirFirstBranch { get; protected set; }

        /// <summary> The Gaijin ID of the vehicle that has to be researched / unlocked before this one can be purchased. </summary>
        [Property()] public virtual string RequiredVehicleGaijinId { get; protected set; }

        #endregion General
        #region Rank

        /// <summary> The vehicle's research rank. </summary>
        [Property()] public virtual int Rank { get; protected set; }

        #endregion Rank

        #endregion Persistent Properties
        #region Association Properties

        /// <summary> The vehicle's nation. </summary>
        [ManyToOne(0, Column = ETable.Nation + "_" + EColumn.Id, ClassType = typeof(Nation), NotNull = true, Lazy = Laziness.Proxy)]
        [Key(1)] public virtual INation Nation { get; protected internal set; }

        /// <summary> The vehicle's branch. </summary>
        [ManyToOne(0, Column = ETable.Branch + "_" + EColumn.Id, ClassType = typeof(Branch), NotNull = true, Lazy = Laziness.Proxy)]
        [Key(1)] public virtual IBranch Branch { get; protected internal set; }

        /// <summary> The vehicle's subclasses. </summary>
        [OneToOne(ClassType = typeof(VehicleSubclasses), PropertyRef = nameof(VehicleSubclasses.Vehicle), Lazy = Laziness.Proxy)]
        public virtual IVehicleSubclasses Subclasses { get; protected internal set; }

        /// <summary> The vehicle's economic rank (the predecessor of the <see cref="BattleRating"/>). The battle rating is being calculated from this. Economic ranks start at 0 and go up with a step of 1. </summary>
        [OneToOne(ClassType = typeof(VehicleGameModeParameterSet.Integer.EconomicRank), PropertyRef = nameof(VehicleGameModeParameterSet.Integer.EconomicRank.Entity), Lazy = Laziness.Proxy)]
        public virtual VehicleGameModeParameterSet.Integer.EconomicRank EconomicRank { get; protected set; }

        /// <summary> Values used for matchmaking (falling into a ± 1.0 battle rating bracket). </summary>
        [OneToOne(ClassType = typeof(VehicleGameModeParameterSet.Decimal.BattleRating), PropertyRef = nameof(VehicleGameModeParameterSet.Decimal.BattleRating.Entity), Lazy = Laziness.Proxy)]
        public virtual VehicleGameModeParameterSet.Decimal.BattleRating BattleRating { get; protected set; }

        /// <summary> A set of vehicle branch tags. </summary>
        [OneToOne(ClassType = typeof(AircraftTags), PropertyRef = nameof(Objects.AircraftTags.Vehicle), Lazy = Laziness.Proxy)]
        public virtual IAircraftTags AircraftTags { get; protected internal set; }

        /// <summary> A set of vehicle branch tags. </summary>
        [OneToOne(ClassType = typeof(GroundVehicleTags), PropertyRef = nameof(Objects.GroundVehicleTags.Vehicle), Lazy = Laziness.Proxy)]
        public virtual IGroundVehicleTags GroundVehicleTags { get; protected internal set; }

        /// <summary> A set of information pertaining to the research tree. </summary>
        [OneToOne(ClassType = typeof(VehicleResearchTreeData), PropertyRef = nameof(VehicleResearchTreeData.Vehicle), Lazy = Laziness.Proxy)]
        public virtual VehicleResearchTreeData ResearchTreeData { get; protected set; }
        
        [OneToOne(ClassType = typeof(VehicleEconomyData), PropertyRef = nameof(VehicleEconomyData.Vehicle), Lazy = Laziness.Proxy)]
        public virtual VehicleEconomyData EconomyData { get; protected set; }

        [OneToOne(ClassType = typeof(VehiclePerformanceData), PropertyRef = nameof(VehiclePerformanceData.Vehicle), Lazy = Laziness.Proxy)]
        public virtual VehiclePerformanceData PerformanceData { get; protected set; }

        [OneToOne(ClassType = typeof(VehicleCrewData), PropertyRef = nameof(VehicleCrewData.Vehicle), Lazy = Laziness.Proxy)]
        public virtual VehicleCrewData CrewData { get; protected set; }

        [OneToOne(ClassType = typeof(VehicleWeaponsData), PropertyRef = nameof(VehicleWeaponsData.Vehicle), Lazy = Laziness.Proxy)]
        public virtual VehicleWeaponsData WeaponsData { get; protected set; }

        [OneToOne(ClassType = typeof(VehicleModificationsData), PropertyRef = nameof(VehicleModificationsData.Vehicle), Lazy = Laziness.Proxy)]
        public virtual VehicleModificationsData ModificationsData { get; protected set; }

        [OneToOne(ClassType = typeof(VehicleGraphicsData), PropertyRef = nameof(VehicleGraphicsData.Vehicle), Lazy = Laziness.Proxy)]
        public virtual VehicleGraphicsData GraphicsData { get; protected set; }

        [OneToOne(ClassType = typeof(VehicleImages), PropertyRef = nameof(VehicleImages.Vehicle), Lazy = Laziness.Proxy)]
        public virtual IVehicleImages Images { get; protected set; }

        [OneToOne(ClassType = typeof(FullName), PropertyRef = nameof(Localization.Vehicle.FullName.Vehicle), Lazy = Laziness.Proxy)]
        /// <summary> The full name of the vehicle. </summary>
        public virtual IVehicleLocalisation FullName { get; protected set; }

        [OneToOne(ClassType = typeof(ResearchTreeName), PropertyRef = nameof(Localization.Vehicle.ResearchTreeName.Vehicle), Lazy = Laziness.Proxy)]
        /// <summary> The name of the vehicle shown in the research tree. </summary>
        public virtual IVehicleLocalisation ResearchTreeName { get; protected set; }

        [OneToOne(ClassType = typeof(ShortName), PropertyRef = nameof(Localization.Vehicle.ShortName.Vehicle), Lazy = Laziness.Proxy)]
        /// <summary> The short name of the vehicle. </summary>
        public virtual IVehicleLocalisation ShortName { get; protected set; }

        #endregion Association Properties
        #region Non-Persistent Properties

        /// <summary> Returns the <see cref="Rank"/> as an item of <see cref="ERank"/>. </summary>
        public virtual ERank RankAsEnumerationItem => Rank.CastTo<ERank>();

        public virtual IEnumerable<EVehicleBranchTag> Tags
        {
            get
            {
                if (Branch?.AsEnumerationItem == EBranch.Army)
                    return GroundVehicleTags?.All;

                else if (Branch?.AsEnumerationItem == EBranch.Aviation)
                    return AircraftTags?.All;

                return new List<EVehicleBranchTag>();
            }
            protected set { }
        }

        /// <summary> Values used for matchmaking (falling into a ± 1.0 battle rating bracket). </summary>
        public virtual VehicleGameModeParameterSet.String.BattleRating BattleRatingFormatted { get; protected set; }

        public virtual bool IsInternal
        {
            get => GaijinId.Contains("_tutorial");
            protected set { }
        }

        #endregion Non-Persistent Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected Vehicle()
        {
        }

        /// <summary> Creates a new vehicle. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="gaijinId"> The vehicle's Gaijin ID. </param>
        public Vehicle(IDataRepository dataRepository, string gaijinId)
            : this(dataRepository, -1L, gaijinId)
        {
        }

        /// <summary> Creates a new vehicle. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="instanceDerializedFromJson"> A non-persistent instance deserialized from JSON data to initialize this instance with. </param>
        public Vehicle(IDataRepository dataRepository, VehicleDeserializedFromJsonWpCost instanceDerializedFromJson)
            : this(dataRepository, -1L, instanceDerializedFromJson.GaijinId)
        {
            Images = new VehicleImages(_dataRepository, this);

            InitializeGameModeParameterSets();
            InitializeWithDeserializedVehicleDataJson(instanceDerializedFromJson);
            InitializeBattleRatings();
        }

        /// <summary> Creates a new vehicle. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The vehicle's ID. </param>
        /// <param name="gaijinId"> The vehicle's Gaijin ID. </param>
        public Vehicle(IDataRepository dataRepository, long id, string gaijinId)
            : base(dataRepository, id, gaijinId)
        {
            LogCreation();
        }

        #endregion Constructors
        #region Methods: Initialization

        /// <summary> Initializes game mode parameter sets. </summary>
        public virtual void InitializeGameModeParameterSets()
        {
            EconomicRank = new VehicleGameModeParameterSet.Integer.EconomicRank(_dataRepository, this);
        }

        /// <summary> Fills properties of the object with values deserialized from JSON data read from "wpcost.blkx". </summary>
        /// <param name="deserializedVehicle"> The temporary non-persistent object storing deserialized data. </param>
        protected virtual void InitializeWithDeserializedVehicleDataJson(VehicleDeserializedFromJsonWpCost deserializedVehicle)
        {
            InitializeWithDeserializedJson(deserializedVehicle);

            lock(_dataRepository.TransactionalLock)
            {
                Nation = _dataRepository.GetNewObjects<INation>().OfType<INation>().FirstOrDefault(notPersistedNation => notPersistedNation.GaijinId == deserializedVehicle.NationGaijinId)
                    ?? _dataRepository.Query<INation>(query => query.Where(nation => nation.GaijinId == deserializedVehicle.NationGaijinId)).FirstOrDefault()
                    ?? new Nation(_dataRepository, deserializedVehicle.NationGaijinId);
            }
            ConsolidateGameModeParameterPropertiesIntoSets(deserializedVehicle);

            EconomyData = new VehicleEconomyData(_dataRepository, this, deserializedVehicle);
            PerformanceData = new VehiclePerformanceData(_dataRepository, this, deserializedVehicle);
            CrewData = new VehicleCrewData(_dataRepository, this, deserializedVehicle);
            ModificationsData = new VehicleModificationsData(_dataRepository, this, deserializedVehicle);
            WeaponsData = new VehicleWeaponsData(_dataRepository, this, deserializedVehicle);
            GraphicsData = new VehicleGraphicsData(_dataRepository, this, deserializedVehicle);

            IsSquadronVehicle = deserializedVehicle.ResearchUnlockType == "clanVehicle" || EconomyData.PurchaseCostInGoldAsSquadronVehicle.HasValue && EconomyData.DiscountedPurchaseCostInGoldAsSquadronVehicle.HasValue;
        }

        /// <summary> Initializes battle ratings. It has to be done here because they are absent in JSON data. </summary>
        public virtual void InitializeBattleRatings()
        {
            static decimal? getBattleRating(int? economicRank) => economicRank.HasValue ? Calculator.GetBattleRating(economicRank.Value) : default(decimal?);

            BattleRating = new VehicleGameModeParameterSet.Decimal.BattleRating(_dataRepository, this, getBattleRating(EconomicRank.Arcade), getBattleRating(EconomicRank.Realistic), getBattleRating(EconomicRank.Simulator), null);

            InitializeVisualBattleRatings();
        }

        /// <summary> Initializes the <see cref="Class"/> based on <paramref name="deserializedTags"/>. Order of conditions is important because vehicles may have overlapping tags. </summary>
        /// <param name="deserializedTags"> An instance of deserialized vehicle tags. </param>
        private void InitializeClass(VehicleTagsDeserializedFromJson deserializedTags)
        {
            if (deserializedTags.IsLightTank)
                Class = EVehicleClass.LightTank;

            else if (deserializedTags.IsMediumTank)
                Class = EVehicleClass.MediumTank;

            else if (deserializedTags.IsHeavyTank)
                Class = EVehicleClass.HeavyTank;

            else if (deserializedTags.IsTankDestroyer)
                Class = EVehicleClass.TankDestroyer;

            else if (deserializedTags.IsSpaa)
                Class = EVehicleClass.Spaa;

            else if (deserializedTags.IsAttackHelicopter)
                Class = EVehicleClass.AttackHelicopter;

            else if (deserializedTags.IsUtilityHelicopter)
                Class = EVehicleClass.UtilityHelicopter;

            else if (deserializedTags.IsFighter)
                Class = EVehicleClass.Fighter;

            else if (deserializedTags.IsAttacker)
                Class = EVehicleClass.Attacker;

            else if (deserializedTags.IsBomber)
                Class = EVehicleClass.Bomber;

            else if (deserializedTags.IsHeavyBoat)
                Class = EVehicleClass.HeavyBoat;

            else if (deserializedTags.IsBarge)
                Class = EVehicleClass.Barge;

            else if (deserializedTags.IsDestroyer)
                Class = EVehicleClass.Destroyer;

            else if (deserializedTags.IsLightCruiser)
                Class = EVehicleClass.LightCruiser;

            else if (deserializedTags.IsHeavyCruiser)
                Class = EVehicleClass.HeavyCruiser;

            else if (deserializedTags.IsFrigate)
                Class = EVehicleClass.Frigate;

            else if (deserializedTags.IsBoat)
                Class = EVehicleClass.Boat;

            else
                Class = EVehicleClass.None;
        }

        /// <summary> Performs additional initialization with data deserialized from "unittags.blkx". </summary>
        /// <param name="deserializedVehicleData"></param>
        public virtual void InitializeWithDeserializedAdditionalVehicleDataJson(VehicleDeserializedFromJsonUnitTags deserializedVehicleData)
        {
            if (!string.IsNullOrWhiteSpace(deserializedVehicleData.CountryGaijinId) && EReference.CountriesFromString.TryGetValue(deserializedVehicleData.CountryGaijinId, out var country))
                Country = country;
            else
                Country = Nation.AsEnumerationItem.GetBaseCountry();

            // From (example) "country_usa" only "usa" is taken and is used as a prefix for (example) "aircraft", so that Gaijin ID becomes (example) "usa_aircraft" that is unique in the scope of the table of branches.
            var branchIdAppended = $"{Nation.GaijinId.Split(ECharacter.Underscore).Last()}{ECharacter.Underscore}{deserializedVehicleData.BranchGaijinId}";

            lock (_dataRepository.TransactionalLock)
            {
                Branch = _dataRepository.GetNewObjects<IBranch>().FirstOrDefault(notPersistedBranch => notPersistedBranch.GaijinId == branchIdAppended)
                ?? _dataRepository.Query<IBranch>(query => query.Where(branch => Nation.GaijinId.Split(ECharacter.Underscore).Last() + ECharacter.Underscore + branch.GaijinId == branchIdAppended)).FirstOrDefault()
                ?? new Branch(_dataRepository, branchIdAppended, Nation);
            }

            if (deserializedVehicleData.Tags is VehicleTagsDeserializedFromJson tags)
            {
                InitializeClass(tags);

                Subclasses = new VehicleSubclasses(_dataRepository, this, tags);
                
                if (Branch.AsEnumerationItem == EBranch.Aviation)
                    AircraftTags = new AircraftTags(_dataRepository, this, tags);

                if (Branch.AsEnumerationItem == EBranch.Army)
                    GroundVehicleTags = new GroundVehicleTags(_dataRepository, this, tags, PerformanceData);
            }
        }

        /// <summary> Fills properties of the object with values deserialized from JSON data read from "shop.blkx". </summary>
        /// <param name="deserializedResearchTreeVehicle"> The temporary non-persistent object storing deserialized data. </param>
        public virtual void InitializeWithDeserializedResearchTreeJson(ResearchTreeVehicleFromJson deserializedResearchTreeVehicle)
        {
            ResearchTreeData = new VehicleResearchTreeData(_dataRepository, this);
            ResearchTreeData.InitializeWithDeserializedJson(deserializedResearchTreeVehicle);

            if (string.IsNullOrWhiteSpace(CategoryOfHiddenVehicles))
                CategoryOfHiddenVehicles = deserializedResearchTreeVehicle.CategoryOfHiddenVehiclesInResearchTree;

            IsAvailableOnlyOnConsoles = !string.IsNullOrWhiteSpace(deserializedResearchTreeVehicle.PlatformGaijinIdVehicleIsAvailableOn);
            IsHiddenUnlessOwned = IsHiddenUnlessOwned
                || deserializedResearchTreeVehicle.IsHiddenUnlessBought
                || deserializedResearchTreeVehicle.IsHiddenUnlessResearched
            ;
            IsResearchable = !EconomyData.PurchaseCostInGold.HasValue && !IsHiddenUnlessOwned && string.IsNullOrWhiteSpace(CategoryOfHiddenVehicles);
            IsReserve = IsResearchable && EconomyData.PurchaseCostInSilver.IsZero();
            IsSoldOnTheMarket = ResearchTreeData.MarketplaceId.HasValue;
            IsSoldInTheStore = !IsHiddenUnlessOwned && IsPremium && !IsSoldOnTheMarket && !string.IsNullOrWhiteSpace(CategoryOfHiddenVehicles);
            IsPurchasableForGoldenEagles =
                !IsHiddenUnlessOwned
                && !IsSoldOnTheMarket
                && !IsSoldInTheStore
                && (IsPremium && EconomyData.PurchaseCostInGold.HasValue || IsSquadronVehicle && EconomyData.PurchaseCostInGoldAsSquadronVehicle.HasValue && EconomyData.DiscountedPurchaseCostInGoldAsSquadronVehicle.HasValue);
        }

        /// <summary> Initializes localization association properties. </summary>
        /// <param name="fullName"> The full name of the vehicle. </param>
        /// <param name="researchTreeName"> The name of the vehicle shown in the research tree. </param>
        /// <param name="shortName"> The short name of the vehicle. </param>
        public virtual void InitializeLocalization(IList<string> fullName, IList<string> researchTreeName, IList<string> shortName)
        {
            FullName = new FullName(_dataRepository, this, fullName);
            ResearchTreeName = new ResearchTreeName(_dataRepository, this, researchTreeName);
            ShortName = new ShortName(_dataRepository, this, shortName);
        }

        /// <summary> Initializes non-persistent fields of the instance. Use this method to finalize reading from a database. </summary>
        /// <param name="dataRepository"> A data repository to assign the object to. </param>
        public override void InitializeNonPersistentFields(IDataRepository dataRepository)
        {
            base.InitializeNonPersistentFields(dataRepository);

            InitializeVisualBattleRatings();
        }

        public virtual void SetIcon(byte[] bytes) => Images.SetIcon(bytes);

        public virtual void SetPortrait(byte[] bytes) => Images.SetPortrait(bytes);

        #region Methods: Initialization Helpers

        /// <summary> Consolidates values of JSON properties for <see cref="EGameMode"/> parameters into sets defined in the persistent class. </summary>
        /// <param name="instanceDeserializedFromJson"> The temporary non-persistent object storing deserialized data. </param>
        private void ConsolidateGameModeParameterPropertiesIntoSets(VehicleDeserializedFromJsonWpCost instanceDeserializedFromJson)
        {
            var parameterSets = new Dictionary<string, VehicleGameModeParameterSetBase>
            {
                { nameof(EconomicRank), EconomicRank },
            };
            ConsolidateGameModeParameterPropertiesIntoSets(parameterSets, instanceDeserializedFromJson);
        }

        /// <summary> Initializes formatted string representations of <see cref="BattleRating"/>. </summary>
        private void InitializeVisualBattleRatings()
        {
            static string formatBattleRating(decimal? nullableValue) => nullableValue.HasValue ? nullableValue.Value.ToString(VehicleGameModeParameterSet.String.BattleRating.Format) : _unknownBattleRating;

            if (BattleRating is null)
                return;

            BattleRatingFormatted = new VehicleGameModeParameterSet.String.BattleRating(formatBattleRating(BattleRating.Arcade), formatBattleRating(BattleRating.Realistic), formatBattleRating(BattleRating.Simulator), _unknownBattleRating);
        }

        #endregion Methods: Initialization Helpers

        #endregion Methods: Initialization
        #region Methods: Overrides

        /// <summary> Returns all persistent objects nested in the instance. This method requires overriding implementation to function. </summary>
        /// <returns></returns>
        public override IEnumerable<IPersistentObject> GetAllNestedObjects()
        {
            var nestedObjects = new List<IPersistentObject>()
            {
                Nation,
                Branch,
                Subclasses,
                AircraftTags,
                GroundVehicleTags,
                EconomicRank,
                BattleRating,
                ResearchTreeData,
                EconomyData,
                PerformanceData,
                CrewData,
                WeaponsData,
                ModificationsData,
                GraphicsData,
                Images,
            };

            return nestedObjects;
        }

        #endregion Methods: Overrides

        public virtual IEnumerable<EVehicleAvailability> GetAvailabilityCategories()
        {
            yield return EVehicleAvailability.All;

            if (IsResearchable)
                yield return EVehicleAvailability.Researchable;

            if (IsReserve)
                yield return EVehicleAvailability.Reserve;

            if (IsSquadronVehicle)
                yield return EVehicleAvailability.ResearchableInSquadron;

            if (IsPurchasableForGoldenEagles && !IsSquadronVehicle)
                yield return EVehicleAvailability.PurchasableForGoldenEagles;

            if (GiftedToNewPlayersForSelectingTheirFirstBranch)
                yield return EVehicleAvailability.StarterGift;

            if (IsSoldInTheStore)
                yield return EVehicleAvailability.PurchasableInTheStore;

            if (IsSoldOnTheMarket)
                yield return EVehicleAvailability.PurchasableOnTheMarket;

            if (IsPremium)
                yield return EVehicleAvailability.Premium;

            if (IsAvailableOnlyOnConsoles)
                yield return EVehicleAvailability.ConsoleExclusive;

            if (IsHiddenUnlessOwned)
                yield return EVehicleAvailability.Hidden;
        }
    }
}