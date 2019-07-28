﻿using Core.DataBase.Enumerations;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects.Interfaces;
using Core.DataBase.WarThunder.Attributes;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Enumerations.DataBase;
using Core.DataBase.WarThunder.Helpers;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Json;
using Core.DataBase.WarThunder.Objects.Json.Interfaces;
using Core.DataBase.WarThunder.Objects.VehicleGameModeParameterSets;
using Core.Enumerations;
using NHibernate.Mapping;
using NHibernate.Mapping.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.DataBase.WarThunder.Objects
{
    /// <summary> A vehicle (air, ground, or sea). </summary>
    [Class(Table = ETable.Vehicle)]
    public class Vehicle : PersistentObjectWithIdAndGaijinId, IVehicle
    {
        #region Constants

        /// <summary> The formatting string for output of <see cref="BattleRating"/>. </summary>
        private const string _battleRatingFormat = "#.0";
        /// <summary> The string representing an unknown battle rating. </summary>
        private const string _unknownBattleRating = "?.?";

        /// <summary> The regular experession matching <see cref="_battleRatingFormat"/> to check validity of <see cref="BattleRating"/> values. </summary>
        public const string BattleRatingRegExPattern = "[1-9]{1}[0-9]{0,}.[037]{1}";

        #endregion Constants
        #region Persistent Properties

        #region Crew

        /// <summary> The crew train cost in Silver Lions that has to be paid before a vehicle can be put into a crew slot (except for reserve vehicles). </summary>
        [Property()] public virtual int BaseCrewTrainCostInSilver { get; protected set; }

        /// <summary> The expert crew train cost in Silver Lions. </summary>
        [Property()] public virtual int ExpertCrewTrainCostInSilver { get; protected set; }

        /// <summary> The base cost of ace crew training in Golden Eagles. </summary>
        [Property()] public virtual int AceCrewTrainCostInGold { get; protected set; }

        /// <summary> The amount of research generated by the vehicle to unlock ace crew qualification for free. </summary>
        [Property()] public virtual int AceCrewTrainCostInResearch { get; protected set; }

        /// <summary> The total number of crewmen in the vehicle. </summary>
        [Property()] public virtual int CrewCount { get; protected set; }

        /// <summary>
        /// The minimum number of crewmen in the vehicle for it to be operable.
        /// This property is only assigned to naval vessels. Aircraft by default need at least one pilot to stay in the air, while ground vehicles require two.
        /// </summary>
        [Property()] public virtual int? MinumumCrewCountToOperate { get; protected set; }

        /// <summary> The number of gunners in the vehicle. </summary>
        [Property()] public virtual int GunnersCount { get; protected set; }

        #endregion Crew
        #region General

        /// <summary> The vehicle's ID. </summary>
        [Id(Column = EColumn.Id, TypeType = typeof(long), Name = nameof(Id), Generator = EIdGenerator.HiLo)]
        public override long Id { get; protected set; }

        /// <summary> The vehicle's Gaijin ID. </summary>
        [Property(NotNull = true, Unique = true)]
        public override string GaijinId { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual string MoveType { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual string Class { get; protected set; }

        /// <summary> Whether this vehicle is hidden. </summary>
        [Property()] public virtual bool? ShowOnlyWhenBought { get; protected set; }

        /// <summary> The category of hidden vehicles this one belongs to. </summary>
        [Property()] public virtual string CategoryOfHiddenVehicles { get; protected set; }

        /// <summary> The gift requirement that grants ownerhip of this vehicle. </summary>
        [Property()] public virtual string OwnershipGiftPrerequisite { get; protected set; }

        /// <summary> Whether this vehicle is gifted to new players upon selecting their first vehicle branch and completing the tutorial. </summary>
        [Property()] public virtual bool? GiftedToNewPlayersForSelectingTheirFirstBranch { get; protected set; }

        /// <summary> The purchase requirement that grants ownerhip of this vehicle. </summary>
        [Property()] public virtual string OwnershipPurchasePrerequisite { get; protected set; }

        /// <summary>
        /// The custom research category that this vehicle is unlocked with.
        /// NULL means that standard research is used.
        /// <para>
        /// This property had been introduced with special squadron vehicles that are researched with squadron activity instead of the normal research,
        /// or are purchased with Golden Eagles, with discount (see <see cref="DiscountedPurchaseCostInGold"/>) if some research progress is made.
        /// </para>
        /// </summary>
        [Property()] public virtual string ResearchUnlockType { get; protected set; }

        /// <summary> The amount of research required to unlock the vehicle. </summary>
        [Property()] public virtual int? UnlockCostInResearch { get; protected set; }

        /// <summary>
        /// The price of purchasing the vehicle with Silver Lions.
        /// Zero means that the vehicle cannot be bought for Silver Lions.
        /// </summary>
        [Property()] public virtual int PurchaseCostInSilver { get; protected set; }

        /// <summary> The price of purchasing the vehicle with Golden Eagles. </summary>
        [Property()] public virtual int? PurchaseCostInGold { get; protected set; }

        /// <summary> The price of purchasing a squadron-researchable vehicle (see <see cref="ResearchUnlockType"/>) after some progress towards its unlocking is made. </summary>
        [Property()] public virtual int? DiscountedPurchaseCostInGold { get; protected set; }

        /// <summary> The vehicle that has to be researched / unlocked before this one can be purchased. </summary>
        [Property()] public virtual string VehicleRequired { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual string SpawnType { get; protected set; }

        /// <summary>
        /// The number of times this vehicle can sortie per match.
        /// This property is necessary for branches that don't have more than one reserve / starter vehicle, like helicopters and navy.
        /// </summary>
        [OneToOne(ClassType = typeof(VehicleGameModeParameterSet.Integer.NumberOfSpawns), PropertyRef = nameof(VehicleGameModeParameterSet.Integer.NumberOfSpawns.Vehicle))]
        public virtual VehicleGameModeParameterSet.Integer.NumberOfSpawns NumberOfSpawns { get; protected set; }

        /// <summary> Whether this vehicle can spawn as a kill streak aircraft in Arcade Battles. </summary>
        [Property()] public virtual bool? CanSpawnAsKillStreak { get; protected set; }

        #endregion General
        #region Graphics

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual string CustomClassIco { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual string CustomImage { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual string CustomTooltipImage { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual string CommonWeaponImage { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual int? WeaponMask { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual int? BulletsIconParam { get; protected set; }

        #endregion Graphics
        #region Modifications

        /// <summary>
        /// [NOT VISUALLY USED IN GAME CLIENT]
        /// The amount of researched modifications of the zeroth tier required to unlock modifications of the first tier.
        /// </summary>
        [Property()] public virtual int AmountOfModificationsResearchedIn_Tier0_RequiredToUnlock_Tier1 { get; protected set; }

        /// <summary> The amount of researched modifications of the first tier required to unlock modifications of the second tier. </summary>
        [Property()] public virtual int AmountOfModificationsResearchedIn_Tier1_RequiredToUnlock_Tier2 { get; protected set; }

        /// <summary> The amount of researched modifications of the second tier required to unlock modifications of the third tier. </summary>
        [Property()] public virtual int AmountOfModificationsResearchedIn_Tier2_RequiredToUnlock_Tier3 { get; protected set; }

        /// <summary> The amount of researched modifications of the third tier required to unlock modifications of the fourth tier. </summary>
        [Property()] public virtual int AmountOfModificationsResearchedIn_Tier3_RequiredToUnlock_Tier4 { get; protected set; }

        /// <summary> The price of purchasing backup sorties for the vehicle (consumable once a match on a vehicle by vehicle basis) with Golden Eagles (a piece). </summary>
        [Property()] public virtual int BackupSortieCostInGold { get; protected set; }

        #endregion Modifications
        #region Performance

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual decimal? Speed { get; protected set; }

        /// <summary> Maximum flight time (in munutes). Applies only to planes and indicates for how long one can fly with a full tank of fuel. </summary>
        [Property()] public virtual int? MaximumFlightTime { get; protected set; }

        /// <summary> The baseline time of fire extinguishing for inexperienced naval crewmen. </summary>
        [Property()] public virtual decimal? MaximumFireExtinguishingTime { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual decimal? HullBreachRepairSpeed { get; protected set; }

        #endregion Performance
        #region Rank

        /// <summary> The vehicle's research rank. </summary>
        [Property()] public virtual int Rank { get; protected set; }

        /// <summary> [OBSOLETE, NOW AN INTERNAL VALUES] The vehicle's ranks (the predecessor of the <see cref="BattleRating"/>). The battle rating is being calculated from these. </summary>
        [OneToOne(ClassType = typeof(VehicleGameModeParameterSet.Integer.EconomicRank), PropertyRef = nameof(VehicleGameModeParameterSet.Integer.EconomicRank.Vehicle))]
        public virtual VehicleGameModeParameterSet.Integer.EconomicRank EconomicRank { get; protected set; }

        /// <summary> Values used for matchmaking (falling into a ± 1.0 battle rating bracket). </summary>
        [OneToOne(ClassType = typeof(VehicleGameModeParameterSet.Decimal.BattleRating), PropertyRef = nameof(VehicleGameModeParameterSet.Decimal.BattleRating.Vehicle))]
        public virtual VehicleGameModeParameterSet.Decimal.BattleRating BattleRating { get; protected set; }

        /// <summary> Values used for matchmaking (falling into a ± 1.0 battle rating bracket). </summary>
        public virtual VehicleGameModeParameterSet.String.BattleRating BattleRatingFormatted { get; protected set; }

        #endregion Rank
        #region Repairs

        /// <summary>
        /// The full time needed for the vehicle to be repaired for free while being in the currently selected preset.
        /// Reserve vehicles don't need repairs.
        /// </summary>
        [OneToOne(ClassType = typeof(VehicleGameModeParameterSet.Decimal.RepairTimeWithCrew), PropertyRef = nameof(VehicleGameModeParameterSet.Decimal.RepairTimeWithCrew.Vehicle))]
        public virtual VehicleGameModeParameterSet.Decimal.RepairTimeWithCrew RepairTimeWithCrew { get; protected set; }

        /// <summary>
        /// The full time needed for the vehicle to be repaired for free while not being in the currently selected preset.
        /// Reserve vehicles don't need repairs.
        /// </summary>
        [OneToOne(ClassType = typeof(VehicleGameModeParameterSet.Decimal.RepairTimeWithoutCrew), PropertyRef = nameof(VehicleGameModeParameterSet.Decimal.RepairTimeWithoutCrew.Vehicle))]
        public virtual VehicleGameModeParameterSet.Decimal.RepairTimeWithoutCrew RepairTimeWithoutCrew { get; protected set; }

        /// <summary>
        /// The full Silver Lion cost for repairing or auto-repairing the vehicle.
        /// Reserve vehicles don't need repairs.
        /// </summary>
        [OneToOne(ClassType = typeof(VehicleGameModeParameterSet.Integer.RepairCost), PropertyRef = nameof(VehicleGameModeParameterSet.Integer.RepairCost.Vehicle))]
        public virtual VehicleGameModeParameterSet.Integer.RepairCost RepairCost { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY, ALL PREMIUM (NON-GIFT) VEHICLES HAVE IT] </summary>
        [Property()] public virtual int? FreeRepairs { get; protected set; }

        #endregion Repairs
        #region Rewards

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [OneToOne(ClassType = typeof(VehicleGameModeParameterSet.Integer.BattleTimeAward), PropertyRef = nameof(VehicleGameModeParameterSet.Integer.BattleTimeAward.Vehicle))]
        public virtual VehicleGameModeParameterSet.Integer.BattleTimeAward BattleTimeAward { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [OneToOne(ClassType = typeof(VehicleGameModeParameterSet.Integer.AverageAward), PropertyRef = nameof(VehicleGameModeParameterSet.Integer.AverageAward.Vehicle))]
        public virtual VehicleGameModeParameterSet.Integer.AverageAward AverageAward { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [OneToOne(ClassType = typeof(VehicleGameModeParameterSet.Decimal.RewardMultiplier), PropertyRef = nameof(VehicleGameModeParameterSet.Decimal.RewardMultiplier.Vehicle))]
        public virtual VehicleGameModeParameterSet.Decimal.RewardMultiplier RewardMultiplier { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [OneToOne(ClassType = typeof(VehicleGameModeParameterSet.Decimal.VisualRewardMultiplier), PropertyRef = nameof(VehicleGameModeParameterSet.Decimal.VisualRewardMultiplier.Vehicle))]
        public virtual VehicleGameModeParameterSet.Decimal.VisualRewardMultiplier VisualRewardMultiplier { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [OneToOne(ClassType = typeof(VehicleGameModeParameterSet.Decimal.VisualPremiumRewardMultiplier), PropertyRef = nameof(VehicleGameModeParameterSet.Decimal.VisualPremiumRewardMultiplier.Vehicle))]
        public virtual VehicleGameModeParameterSet.Decimal.VisualPremiumRewardMultiplier VisualPremiumRewardMultiplier { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual decimal ResearchRewardMultiplier { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual decimal GroundKillRewardMultiplier { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [OneToOne(ClassType = typeof(VehicleGameModeParameterSet.Decimal.BattleTime), PropertyRef = nameof(VehicleGameModeParameterSet.Decimal.BattleTime.Vehicle))]
        public virtual VehicleGameModeParameterSet.Decimal.BattleTime BattleTime { get; protected set; }

        #endregion Rewards
        #region Weapons

        /// <summary> The vehicle's turret traverse speeds. </summary>
        [Property()] public virtual List<decimal?> TurretTraverseSpeeds { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual decimal? MachineGunReloadTime { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual decimal? CannonReloadTime { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual decimal? GunnerReloadTime { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY, VEHICLES WITHOUT PRIMARY ARMAMENT DON"T HAVE THIS PROPERTY] </summary>
        [Property()] public virtual int? MaximumAmmunition { get; protected set; }

        /// <summary> Whether the vehicle's main armament comes equipped with an auto-loader (grants fixed reload speed that doesn't depend on the loader and doesn't improve with the loader's skill). </summary>
        [Property()] public virtual bool? PrimaryWeaponHasAutoLoader { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual decimal? MaximumRocketDeltaAngle { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual decimal? MaximumAtgmDeltaAngle { get; protected set; }

        /// <summary> The modification that is considered an upgrade of the vehicle's armaments. </summary>
        [Property()] public virtual string WeaponUpgrade1 { get; protected set; }

        /// <summary> The modification that is considered an upgrade of the vehicle's armaments. </summary>
        [Property()] public virtual string WeaponUpgrade2 { get; protected set; }

        /// <summary> The modification that is considered an upgrade of the vehicle's armaments. </summary>
        [Property()] public virtual string WeaponUpgrade3 { get; protected set; }

        /// <summary> The modification that is considered an upgrade of the vehicle's armaments. </summary>
        [Property()] public virtual string WeaponUpgrade4 { get; protected set; }

        #endregion Weapons

        #endregion Persistent Properties
        #region Association Properties

        /// <summary> The vehicle's nation. </summary>
        [ManyToOne(0, Column = ETable.Nation + "_" + EColumn.Id, ClassType = typeof(Nation), Lazy = Laziness.False, NotNull = true)]
        [Key(1)] public virtual INation Nation { get; protected internal set; }

        /// <summary> The vehicle's branch. </summary>
        [ManyToOne(0, Column = ETable.Branch + "_" + EColumn.Id, ClassType = typeof(Branch), Lazy = Laziness.False, NotNull = true)]
        [Key(1)] public virtual IBranch Branch { get; protected internal set; }

        #endregion Association Properties
        #region Non-Persistent Properties

        /// <summary> Checks whether the vehicle can be unlocked for free with research. </summary>
        public virtual bool NotResearchable => PurchaseCostInGold.HasValue || ShowOnlyWhenBought.HasValue || !(CategoryOfHiddenVehicles is null) && CategoryOfHiddenVehicles.Any();

        #endregion Non-Persistent Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate deserialized data read from a database. </summary>
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
            InitializeWithDeserializedJson(instanceDerializedFromJson);
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

        // This class is not supposed to have a constructor that allows injection of most property values.
        // That is done when deserializing instances from JSON.

        #endregion Constructors
        #region Methods: Initialization

        /// <summary> Initializes non-persistent fields of the instance. Use this method to finalize reading from a database. </summary>
        /// <param name="dataRepository"> A data repository to assign the object to. </param>
        public override void InitializeNonPersistentFields(IDataRepository dataRepository)
        {
            base.InitializeNonPersistentFields(dataRepository);

            InitializeVisualBattleRatings();
        }

        /// <summary> Tries to insert the value of the specified jSON property into the relevant game mode parameter set. </summary>
        /// <param name="instanceDeserializedFromJson"> The temporary non-persistent object storing deserialized data. </param>
        /// <param name="jsonProperty"> The JSON property whose value is being inserted. </param>
        /// <param name="parameterSets"> The dictionary of available game mode parameter sets. </param>
        private void InsertJsonPropertyValueIntoGameModeParameterSet(IDeserializedFromJson instanceDeserializedFromJson, PropertyInfo jsonProperty, Dictionary<string, VehicleGameModeParameterSetBase> parameterSets)
        {
            var persistAsDictionaryItemAttribute = jsonProperty.GetCustomAttribute<PersistAsDictionaryItemAttribute>();

            if (persistAsDictionaryItemAttribute is null) // We are not interested in any properties not marked for consolidation via PersistAsDictionaryItemAttribute.
                return;

            var jsonPropertyValue = jsonProperty.GetValue(instanceDeserializedFromJson);

            #region Adjust value inputs for nullability of dictionary values (in case of non-required JSON properties)

            if (jsonProperty.PropertyType == typeof(int))
                jsonPropertyValue = new int?((int)jsonPropertyValue);

            else if (jsonProperty.PropertyType == typeof(decimal))
                jsonPropertyValue = new decimal?((decimal)jsonPropertyValue);

            #endregion Adjust value inputs for nullability of dictionary values (in case of non-required JSON properties

            var parameterSet = parameterSets[persistAsDictionaryItemAttribute.Key];

            switch (persistAsDictionaryItemAttribute.GameMode)
            {
                case EGameMode.Arcade:
                    parameterSet.InternalArcade = jsonPropertyValue;
                    break;
                case EGameMode.Realistic:
                    parameterSet.InternalRealistic = jsonPropertyValue;
                    break;
                case EGameMode.Simulator:
                    parameterSet.InternalSimulator = jsonPropertyValue;
                    break;
                case EGameMode.Event:
                    parameterSet.InternalEvent = jsonPropertyValue;
                    break;
            };
        }

        /// <summary> Consolidates values of JSON properties for <see cref="EGameMode"/> parameters into sets defined in the persistent class. </summary>
        /// <param name="instanceDeserializedFromJson"> The temporary non-persistent object storing deserialized data. </param>
        private void ConsolidateGameModeParameterPropertiesIntoSets(IDeserializedFromJson instanceDeserializedFromJson)
        {
            var parameterSets = new Dictionary<string, VehicleGameModeParameterSetBase>
            {
                { nameof(AverageAward), AverageAward },
                { nameof(BattleTime), BattleTime },
                { nameof(BattleTimeAward), BattleTimeAward },
                { nameof(EconomicRank), EconomicRank },
                { nameof(NumberOfSpawns), NumberOfSpawns },
                { nameof(RepairCost), RepairCost },
                { nameof(RepairTimeWithCrew), RepairTimeWithCrew },
                { nameof(RepairTimeWithoutCrew), RepairTimeWithoutCrew },
                { nameof(RewardMultiplier), RewardMultiplier },
                { nameof(VisualPremiumRewardMultiplier), VisualPremiumRewardMultiplier },
                { nameof(VisualRewardMultiplier), VisualRewardMultiplier },
            };

            foreach (var jsonProperty in instanceDeserializedFromJson.GetType().GetProperties()) // With a dictionary of game mode parameter set properties now there's only need to look through the JSON mapping class once.
                InsertJsonPropertyValueIntoGameModeParameterSet(instanceDeserializedFromJson, jsonProperty, parameterSets);
        }

        /// <summary> Fills properties of the object with values deserialized from JSON data. </summary>
        /// <param name="instanceDeserializedFromJson"> The temporary non-persistent object storing deserialized data. </param>
        protected override void InitializeWithDeserializedJson(IDeserializedFromJson instanceDeserializedFromJson)
        {
            base.InitializeWithDeserializedJson(instanceDeserializedFromJson);

            if (instanceDeserializedFromJson is VehicleDeserializedFromJsonWpCost deserializedVehicle)
            {
                Nation = _dataRepository.NewObjects.OfType<INation>().FirstOrDefault(notPersistedNation => notPersistedNation.GaijinId == deserializedVehicle.NationGaijinId)
                    ?? _dataRepository.Query<INation>(query => query.Where(nation => nation.GaijinId == deserializedVehicle.NationGaijinId)).FirstOrDefault()
                    ?? new Nation(_dataRepository, deserializedVehicle.NationGaijinId);

                PatchSpawnType(deserializedVehicle);
                BackupSortieCostInGold = deserializedVehicle.BackupSortie.PurchaseCostInGold;
            }

            #region Instantialize all game mode parameter set properties.

            AverageAward = new VehicleGameModeParameterSet.Integer.AverageAward(_dataRepository, this);
            BattleTime = new VehicleGameModeParameterSet.Decimal.BattleTime(_dataRepository, this);
            BattleTimeAward = new VehicleGameModeParameterSet.Integer.BattleTimeAward(_dataRepository, this);
            EconomicRank = new VehicleGameModeParameterSet.Integer.EconomicRank(_dataRepository, this);
            NumberOfSpawns = new VehicleGameModeParameterSet.Integer.NumberOfSpawns(_dataRepository, this);
            RepairCost = new VehicleGameModeParameterSet.Integer.RepairCost(_dataRepository, this);
            RepairTimeWithCrew = new VehicleGameModeParameterSet.Decimal.RepairTimeWithCrew(_dataRepository, this);
            RepairTimeWithoutCrew = new VehicleGameModeParameterSet.Decimal.RepairTimeWithoutCrew(_dataRepository, this);
            RewardMultiplier = new VehicleGameModeParameterSet.Decimal.RewardMultiplier(_dataRepository, this);
            VisualPremiumRewardMultiplier = new VehicleGameModeParameterSet.Decimal.VisualPremiumRewardMultiplier(_dataRepository, this);
            VisualRewardMultiplier = new VehicleGameModeParameterSet.Decimal.VisualRewardMultiplier(_dataRepository, this);

            #endregion All game mode parameter set properties have been instantialized.

            ConsolidateGameModeParameterPropertiesIntoSets(instanceDeserializedFromJson);

            #region Battle ratings have to be initialized explicitly because they are absent in JSON data.

            decimal? getBattleRating(int? economicRank) => economicRank.HasValue ? Calculator.GetBattleRating(economicRank.Value) : default(decimal?);

            BattleRating = new VehicleGameModeParameterSet.Decimal.BattleRating(_dataRepository, this, getBattleRating(EconomicRank.Arcade), getBattleRating(EconomicRank.Realistic), getBattleRating(EconomicRank.Simulator), null);

            InitializeVisualBattleRatings();

            #endregion Battle ratings have been initialized.
        }

        /// <summary>
        /// Gets an existing branch or creates a new one with the specified <see cref="IPersistentObjectWithIdAndGaijinId.GaijinId"/>.
        /// <para>
        /// First not-yet-persisted objects are checked for the given branch, after that the database is queried, and only then a new branch is created.
        /// </para>
        /// </summary>
        /// <param name="branchGaijinId"> The <see cref="IPersistentObjectWithIdAndGaijinId.GaijinId"/> of the branch to look for or create. </param>
        /// <returns></returns>
        private IBranch GetOrCreateBranch(string branchGaijinId)
        {
            var newBranch = _dataRepository.NewObjects.OfType<IBranch>().FirstOrDefault(notPersistedBranch => notPersistedBranch.GaijinId == branchGaijinId);

            if (newBranch is null)
                newBranch = _dataRepository.Query<IBranch>(query => query.Where(branch => Nation.GaijinId.Split(ECharacter.Underscore).Last() + ECharacter.Underscore + branch.GaijinId == branchGaijinId)).FirstOrDefault();

            if (newBranch is null)
                newBranch = new Branch(_dataRepository, branchGaijinId, Nation);

            return newBranch;
        }

        /// <summary> Performs additional initialization with data deserialized from "unittags.blkx". </summary>
        /// <param name="deserializedVehicleData"></param>
        public virtual void DoPostInitalization(VehicleDeserializedFromJsonUnitTags deserializedVehicleData)
        {
            // From (example) "country_usa" only "usa" is taken and is used as a prefix for (example) "aircraft", so that Gaijin ID becomes (example) "usa_aircraft" that is unique in the scope of the table of branches.
            var branchIdAppended = $"{Nation.GaijinId.Split(ECharacter.Underscore).Last()}{ECharacter.Underscore}{deserializedVehicleData.BranchGaijinId}";

            Branch = GetOrCreateBranch(branchIdAppended);
        }

        /// <summary> Initializes formatted string representations of <see cref="BattleRating"/>. </summary>
        private void InitializeVisualBattleRatings()
        {
            string formatBattleRating(decimal? nullableValue) => nullableValue.HasValue ? nullableValue.Value.ToString(_battleRatingFormat) : _unknownBattleRating;

            if (BattleRating is null)
                return;

            BattleRatingFormatted = new VehicleGameModeParameterSet.String.BattleRating(formatBattleRating(BattleRating.Arcade), formatBattleRating(BattleRating.Realistic), formatBattleRating(BattleRating.Simulator), _unknownBattleRating);
        }

        /// <summary> Clarifies <see cref="SpawnType"/> values. </summary>
        /// <param name="deserializedVehicle"> The temporary non-persistent object storing deserialized data. </param>
        private void PatchSpawnType(VehicleDeserializedFromJsonWpCost deserializedVehicle)
        {
            if (deserializedVehicle.SpawnType == "ah")
                SpawnType = "walker (ah)";
            else if (deserializedVehicle.SpawnType == null)
                SpawnType = "default";
        }

        #endregion Methods: Initialization

        /// <summary> Returns all persistent objects nested in the instance. This method requires overriding implementation to function. </summary>
        /// <returns></returns>
        public override IEnumerable<IPersistentObject> GetAllNestedObjects()
        {
            var nestedObjects = new List<IPersistentObject>()
            {
                Nation,
            };

            return nestedObjects;
        }
    }
}