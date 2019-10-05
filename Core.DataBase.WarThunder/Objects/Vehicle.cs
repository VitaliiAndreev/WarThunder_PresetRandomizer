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
using Core.DataBase.WarThunder.Objects.Localization.Vehicle;
using Core.DataBase.WarThunder.Objects.Localization.Vehicle.Interfaces;
using Core.DataBase.WarThunder.Objects.VehicleGameModeParameterSets;
using Core.Enumerations;
using Core.Extensions;
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

        /// <summary> The vehicle's broadly defined class with a distict icon. </summary>
        [Property(NotNull = true)]
        public virtual EVehicleClass Class { get; protected internal set; }

        /// <summary> Indicates whether the vehicle is premium or not. </summary>
        [Property(NotNull = true)]
        public virtual bool IsPremium { get; protected set; }

        /// <summary> Whether this vehicle is hidden from those that don't own it. </summary>
        [Property()] public virtual bool? ShowOnlyWhenBought { get; protected set; }

        /// <summary> The category of hidden vehicles this one belongs to. </summary>
        [Property()] public virtual string CategoryOfHiddenVehicles { get; protected set; }

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

        /// <summary> The price of purchasing the vehicle with Golden Eagles. </summary>
        [Property()] public virtual int? PurchaseCostInGold { get; protected set; }

        /// <summary> The price of purchasing a squadron-researchable vehicle (see <see cref="ResearchUnlockType"/>) after some progress towards its unlocking is made. </summary>
        [Property()] public virtual int? DiscountedPurchaseCostInGold { get; protected set; }

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
        [ManyToOne(0, Column = ETable.Nation + "_" + EColumn.Id, ClassType = typeof(Nation), Lazy = Laziness.False, NotNull = true)]
        [Key(1)] public virtual INation Nation { get; protected internal set; }

        /// <summary> The vehicle's branch. </summary>
        [ManyToOne(0, Column = ETable.Branch + "_" + EColumn.Id, ClassType = typeof(Branch), Lazy = Laziness.False, NotNull = true)]
        [Key(1)] public virtual IBranch Branch { get; protected internal set; }

        /// <summary> [OBSOLETE, NOW AN INTERNAL VALUES] The vehicle's economic rank (the predecessor of the <see cref="BattleRating"/>). The battle rating is being calculated from this. Economic ranks start at 0 and go up with a step of 1. </summary>
        [OneToOne(ClassType = typeof(VehicleGameModeParameterSet.Integer.EconomicRank), PropertyRef = nameof(VehicleGameModeParameterSet.Integer.EconomicRank.Vehicle))]
        public virtual VehicleGameModeParameterSet.Integer.EconomicRank EconomicRank { get; protected set; }

        /// <summary> Values used for matchmaking (falling into a ± 1.0 battle rating bracket). </summary>
        [OneToOne(ClassType = typeof(VehicleGameModeParameterSet.Decimal.BattleRating), PropertyRef = nameof(VehicleGameModeParameterSet.Decimal.BattleRating.Vehicle))]
        public virtual VehicleGameModeParameterSet.Decimal.BattleRating BattleRating { get; protected set; }

        /// <summary> A set of information pertaining to the research tree. </summary>
        [OneToOne(ClassType = typeof(VehicleResearchTreeData), PropertyRef = nameof(VehicleResearchTreeData.Vehicle))]
        public virtual VehicleResearchTreeData ResearchTreeData { get; protected set; }
        
        [OneToOne(ClassType = typeof(FullName), PropertyRef = nameof(Localization.Vehicle.FullName.Vehicle))]
        /// <summary> The full name of the vehicle. </summary>
        public virtual IVehicleLocalization FullName { get; protected set; }

        [OneToOne(ClassType = typeof(ResearchTreeName), PropertyRef = nameof(Localization.Vehicle.ResearchTreeName.Vehicle))]
        /// <summary> The name of the vehicle shown in the research tree. </summary>
        public virtual IVehicleLocalization ResearchTreeName { get; protected set; }

        [OneToOne(ClassType = typeof(ShortName), PropertyRef = nameof(Localization.Vehicle.ShortName.Vehicle))]
        /// <summary> The short name of the vehicle. </summary>
        public virtual IVehicleLocalization ShortName { get; protected set; }

        [OneToOne(ClassType = typeof(ClassName), PropertyRef = nameof(Localization.Vehicle.ClassName.Vehicle))]
        /// <summary> The name of the vehicle's <see cref="Class"/>. </summary>
        public virtual IVehicleLocalization ClassName { get; protected set; }

        #endregion Association Properties
        #region Non-Persistent Properties

        /// <summary> Returns the <see cref="Rank"/> as an item of <see cref="ERank"/>. </summary>
        public virtual ERank RankAsEnumerationItem => Rank.CastTo<ERank>();

        /// <summary> Values used for matchmaking (falling into a ± 1.0 battle rating bracket). </summary>
        public virtual VehicleGameModeParameterSet.String.BattleRating BattleRatingFormatted { get; protected set; }

        /// <summary> Indicates whether the vehicle can be unlocked for free with research. </summary>
        public virtual bool NotResearchable => PurchaseCostInGold.HasValue || (ShowOnlyWhenBought ?? false) || !string.IsNullOrWhiteSpace(CategoryOfHiddenVehicles);

        /// <summary> Indicates whether the vehicle can be unlocked with squadron research. </summary>
        public virtual bool IsSquadronVehicle => ResearchUnlockType == "clanVehicle" || DiscountedPurchaseCostInGold.HasValue;

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

        // This class is not supposed to have a constructor that allows injection of most property values.
        // That is done when deserializing instances from JSON.

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

            Nation = _dataRepository.NewObjects.OfType<INation>().FirstOrDefault(notPersistedNation => notPersistedNation.GaijinId == deserializedVehicle.NationGaijinId)
                ?? _dataRepository.Query<INation>(query => query.Where(nation => nation.GaijinId == deserializedVehicle.NationGaijinId)).FirstOrDefault()
                ?? new Nation(_dataRepository, deserializedVehicle.NationGaijinId);

            ConsolidateGameModeParameterPropertiesIntoSets(deserializedVehicle);
        }

        /// <summary> Initializes battle ratings. It has to be done here because they are absent in JSON data. </summary>
        public virtual void InitializeBattleRatings()
        {
            static decimal? getBattleRating(int? economicRank) => economicRank.HasValue ? Calculator.GetBattleRating(economicRank.Value) : default(decimal?);

            BattleRating = new VehicleGameModeParameterSet.Decimal.BattleRating(_dataRepository, this, getBattleRating(EconomicRank.Arcade), getBattleRating(EconomicRank.Realistic), getBattleRating(EconomicRank.Simulator), null);

            InitializeVisualBattleRatings();
        }

        /// <summary> Initializes <see cref="Class"/> based on <paramref name="deserializedTags"/>. </summary>
        /// <param name="deserializedTags"></param>
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

            else if (deserializedTags.IsBoat)
                Class = EVehicleClass.Boat;

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
        }

        /// <summary> Performs additional initialization with data deserialized from "unittags.blkx". </summary>
        /// <param name="deserializedVehicleData"></param>
        public virtual void InitializeWithDeserializedAdditionalVehicleDataJson(VehicleDeserializedFromJsonUnitTags deserializedVehicleData)
        {
            if (deserializedVehicleData.Tags is VehicleTagsDeserializedFromJson tags)
                InitializeClass(tags);

            // From (example) "country_usa" only "usa" is taken and is used as a prefix for (example) "aircraft", so that Gaijin ID becomes (example) "usa_aircraft" that is unique in the scope of the table of branches.
            var branchIdAppended = $"{Nation.GaijinId.Split(ECharacter.Underscore).Last()}{ECharacter.Underscore}{deserializedVehicleData.BranchGaijinId}";

            Branch = _dataRepository.NewObjects.OfType<IBranch>().FirstOrDefault(notPersistedBranch => notPersistedBranch.GaijinId == branchIdAppended)
                ?? _dataRepository.Query<IBranch>(query => query.Where(branch => Nation.GaijinId.Split(ECharacter.Underscore).Last() + ECharacter.Underscore + branch.GaijinId == branchIdAppended)).FirstOrDefault()
                ?? new Branch(_dataRepository, branchIdAppended, Nation);
        }

        /// <summary> Fills properties of the object with values deserialized from JSON data read from "shop.blkx". </summary>
        /// <param name="deserializedResearchTreeVehicle"> The temporary non-persistent object storing deserialized data. </param>
        public virtual void InitializeWithDeserializedResearchTreeJson(ResearchTreeVehicleFromJson deserializedResearchTreeVehicle)
        {
            ResearchTreeData = new VehicleResearchTreeData(_dataRepository, this);
            ResearchTreeData.InitializeWithDeserializedJson(deserializedResearchTreeVehicle);
        }

        /// <summary> Initializes localization association properties. </summary>
        /// <param name="fullName"> The full name of the vehicle. </param>
        /// <param name="researchTreeName"> The name of the vehicle shown in the research tree. </param>
        /// <param name="shortName"> The short name of the vehicle. </param>
        /// <param name="className"> The name of the vehicle's <see cref="Class"/>. </param>
        public virtual void InitializeLocalization(IList<string> fullName, IList<string> researchTreeName, IList<string> shortName, IList<string> className)
        {
            FullName = new FullName(_dataRepository, this, fullName);
            ResearchTreeName = new ResearchTreeName(_dataRepository, this, researchTreeName);
            ShortName = new ShortName(_dataRepository, this, shortName);
            ClassName = new ClassName(_dataRepository, this, className);
        }

        /// <summary> Initializes non-persistent fields of the instance. Use this method to finalize reading from a database. </summary>
        /// <param name="dataRepository"> A data repository to assign the object to. </param>
        public override void InitializeNonPersistentFields(IDataRepository dataRepository)
        {
            base.InitializeNonPersistentFields(dataRepository);

            InitializeVisualBattleRatings();
        }

        #region Methods: Initialization Helpers

        /// <summary> Consolidates values of JSON properties for <see cref="EGameMode"/> parameters into sets defined in the persistent class. </summary>
        /// <param name="instanceDeserializedFromJson"> The temporary non-persistent object storing deserialized data. </param>
        private void ConsolidateGameModeParameterPropertiesIntoSets(VehicleDeserializedFromJsonWpCost instanceDeserializedFromJson)
        {
            var parameterSets = new Dictionary<string, VehicleGameModeParameterSetBase>
            {
                { nameof(EconomicRank), EconomicRank },
            };

            foreach (var jsonProperty in instanceDeserializedFromJson.GetType().GetProperties()) // With a dictionary of game mode parameter set properties now there's only need to look through the JSON mapping class once.
                InsertJsonPropertyValueIntoGameModeParameterSet(instanceDeserializedFromJson, jsonProperty, parameterSets);
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

        /// <summary> Returns all persistent objects nested in the instance. This method requires overriding implementation to function. </summary>
        /// <returns></returns>
        public override IEnumerable<IPersistentObject> GetAllNestedObjects()
        {
            var nestedObjects = new List<IPersistentObject>()
            {
                Nation,
                Branch,
                EconomicRank,
                BattleRating,
                ResearchTreeData,
            };

            return nestedObjects;
        }
    }
}