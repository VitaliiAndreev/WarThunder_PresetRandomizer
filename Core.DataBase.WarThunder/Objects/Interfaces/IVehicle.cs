﻿using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Json;
using Core.DataBase.WarThunder.Objects.Localization.Vehicle.Interfaces;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A vehicle (air, ground, or sea). </summary>
    public interface IVehicle : IPersistentObjectWithIdAndGaijinId
    {
        #region Persistent Properties

        #region Crew

        /// <summary> The crew train cost in Silver Lions that has to be paid before a vehicle can be put into a crew slot (except for reserve vehicles). </summary>
        int BaseCrewTrainCostInSilver { get; }

        /// <summary> The expert crew train cost in Silver Lions. </summary>
        int ExpertCrewTrainCostInSilver { get; }

        /// <summary> The base cost of ace crew training in Golden Eagles. </summary>
        int AceCrewTrainCostInGold { get; }

        /// <summary> The amount of research generated by the vehicle to unlock ace crew qualification for free. </summary>
        int AceCrewTrainCostInResearch { get; }

        /// <summary> The total number of crewmen in the vehicle. </summary>
        int CrewCount { get; }

        /// <summary>
        /// The minimum number of crewmen in the vehicle for it to be operable.
        /// This property is only assigned to naval vessels. Aircraft by default need at least one pilot to stay in the air, while ground vehicles require two.
        /// </summary>
        int? MinumumCrewCountToOperate { get; }

        /// <summary> The number of gunners in the vehicle. </summary>
        int GunnersCount { get; }

        #endregion Crew
        #region General

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        string MoveType { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        string Class { get; }

        /// <summary> Whether this vehicle is hidden from those that don't own it. </summary>
        bool? ShowOnlyWhenBought { get; }

        /// <summary> The category of hidden vehicles this one belongs to. </summary>
        string CategoryOfHiddenVehicles { get; }

        /// <summary> The gift requirement that grants ownerhip of this vehicle. </summary>
        string OwnershipGiftPrerequisite { get; }

        /// <summary> Whether this vehicle is gifted to new players upon selecting their first vehicle branch and completing the tutorial. </summary>
        bool? GiftedToNewPlayersForSelectingTheirFirstBranch { get; }

        /// <summary> The purchase requirement that grants ownerhip of this vehicle. </summary>
        string OwnershipPurchasePrerequisite { get; }

        /// <summary>
        /// The custom research category that this vehicle is unlocked with.
        /// NULL means that standard research is used.
        /// <para>
        /// This property had been introduced with special squadron vehicles that are researched with squadron activity instead of the normal research,
        /// or are purchased with Golden Eagles, with discount (see <see cref="DiscountedPurchaseCostInGold"/>) if some research progress is made.
        /// </para>
        /// </summary>
        string ResearchUnlockType { get; }

        /// <summary> The amount of research required to unlock the vehicle. </summary>
        int? UnlockCostInResearch { get; }

        /// <summary>
        /// The price of purchasing the vehicle with Silver Lions.
        /// Zero means that the vehicle cannot be bought for Silver Lions.
        /// </summary>
        int PurchaseCostInSilver { get; }

        /// <summary> The price of purchasing the vehicle with Golden Eagles. </summary>
        int? PurchaseCostInGold { get; }

        /// <summary> The price of purchasing a squadron-researchable vehicle (see <see cref="ResearchUnlockType"/>) after some progress towards its unlocking is made. </summary>
        int? DiscountedPurchaseCostInGold { get; }

        /// <summary> The vehicle that has to be researched / unlocked before this one can be purchased. </summary>
        string VehicleRequired { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        string SpawnType { get; }

        /// <summary> Whether this vehicle can spawn as a kill streak aircraft in Arcade Battles. </summary>
        bool? CanSpawnAsKillStreak { get; }

        #endregion General
        #region Graphics

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        string CustomClassIco { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        string CustomImage { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        string CustomTooltipImage { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        string CommonWeaponImage { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        int? WeaponMask { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        int? BulletsIconParam { get; }

        #endregion Graphics
        #region Modifications

        /// <summary>
        /// [NOT VISUALLY USED IN GAME CLIENT]
        /// The amount of researched modifications of the zeroth tier required to unlock modifications of the first tier.
        /// </summary>
        int AmountOfModificationsResearchedIn_Tier0_RequiredToUnlock_Tier1 { get; }

        /// <summary> The amount of researched modifications of the first tier required to unlock modifications of the second tier. </summary>
        int AmountOfModificationsResearchedIn_Tier1_RequiredToUnlock_Tier2 { get; }

        /// <summary> The amount of researched modifications of the second tier required to unlock modifications of the third tier. </summary>
        int AmountOfModificationsResearchedIn_Tier2_RequiredToUnlock_Tier3 { get; }

        /// <summary> The amount of researched modifications of the third tier required to unlock modifications of the fourth tier. </summary>
        int AmountOfModificationsResearchedIn_Tier3_RequiredToUnlock_Tier4 { get; }

        /// <summary> The price of purchasing backup sorties for the vehicle (consumable once a match on a vehicle by vehicle basis) with Golden Eagles (a piece). </summary>
        int BackupSortieCostInGold { get; }

        #endregion Modifications
        #region Performance

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal? Speed { get; }

        /// <summary> Maximum flight time (in munutes). Applies only to planes and indicates for how long one can fly with a full tank of fuel. </summary>
        int? MaximumFlightTime { get; }

        /// <summary> The baseline time of fire extinguishing for inexperienced naval crewmen. </summary>
        decimal? MaximumFireExtinguishingTime { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal? HullBreachRepairSpeed { get; }

        #endregion Performance
        #region Rank

        /// <summary> The vehicle's research rank. </summary>
        int Rank { get; }

        #endregion Rank
        #region Repairs

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY, ALL PREMIUM (NON-GIFT) VEHICLES HAVE IT] </summary>
        int? FreeRepairs { get; }

        #endregion Repairs
        #region Rewards

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal ResearchRewardMultiplier { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal GroundKillRewardMultiplier { get; }

        #endregion Rewards
        #region Weapons

        /// <summary> The vehicle's turret traverse speeds. </summary>
        List<decimal?> TurretTraverseSpeeds { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal? MachineGunReloadTime { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal? CannonReloadTime { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal? GunnerReloadTime { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY, VEHICLES WITHOUT PRIMARY ARMAMENT DON"T HAVE THIS PROPERTY] </summary>
        int? MaximumAmmunition { get; }

        /// <summary> Whether the vehicle's main armament comes equipped with an auto-loader (grants fixed reload speed that doesn't depend on the loader and doesn't improve with the loader's skill). </summary>
        bool? PrimaryWeaponHasAutoLoader { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal? MaximumRocketDeltaAngle { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal? MaximumAtgmDeltaAngle { get; }

        /// <summary> The modification that is considered an upgrade of the vehicle's armaments. </summary>
        string WeaponUpgrade1 { get; }

        /// <summary> The modification that is considered an upgrade of the vehicle's armaments. </summary>
        string WeaponUpgrade2 { get; }

        /// <summary> The modification that is considered an upgrade of the vehicle's armaments. </summary>
        string WeaponUpgrade3 { get; }

        /// <summary> The modification that is considered an upgrade of the vehicle's armaments. </summary>
        string WeaponUpgrade4 { get; }

        #endregion Weapons

        #endregion Persistent Properties
        #region Association Properties

        /// <summary> The vehicle's nation. </summary>
        INation Nation { get; }

        /// <summary> The vehicle's branch. </summary>
        IBranch Branch { get; }

        /// <summary> [OBSOLETE, NOW AN INTERNAL VALUES] The vehicle's ranks (the predecessor of the <see cref="BattleRating"/>). The battle rating is being calculated from these. </summary>
        VehicleGameModeParameterSet.Integer.EconomicRank EconomicRank { get; }

        /// <summary> Values used for matchmaking (falling into a ± 1.0 battle rating bracket). </summary>
        VehicleGameModeParameterSet.Decimal.BattleRating BattleRating { get; }

        /// <summary> A set of information pertaining to the research tree. </summary>
        VehicleResearchTreeData ResearchTreeData { get; }

        /// <summary>
        /// The full time needed for the vehicle to be repaired for free while being in the currently selected preset.
        /// Reserve vehicles don't need repairs.
        /// </summary>
        VehicleGameModeParameterSet.Decimal.RepairTimeWithCrew RepairTimeWithCrew { get; }

        /// <summary>
        /// The full time needed for the vehicle to be repaired for free while not being in the currently selected preset.
        /// Reserve vehicles don't need repairs.
        /// </summary>
        VehicleGameModeParameterSet.Decimal.RepairTimeWithoutCrew RepairTimeWithoutCrew { get; }

        /// <summary>
        /// The full Silver Lion cost for repairing or auto-repairing the vehicle.
        /// Reserve vehicles don't need repairs.
        /// </summary>
        VehicleGameModeParameterSet.Integer.RepairCost RepairCost { get; }

        /// <summary>
        /// The number of times this vehicle can sortie per match.
        /// This property is necessary for branches that don't have more than one reserve / starter vehicle, like helicopters and navy.
        /// </summary>
        VehicleGameModeParameterSet.Integer.NumberOfSpawns NumberOfSpawns { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        VehicleGameModeParameterSet.Integer.BattleTimeAward BattleTimeAward { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        VehicleGameModeParameterSet.Integer.AverageAward AverageAward { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        VehicleGameModeParameterSet.Decimal.RewardMultiplier RewardMultiplier { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        VehicleGameModeParameterSet.Decimal.VisualRewardMultiplier VisualRewardMultiplier { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        VehicleGameModeParameterSet.Decimal.VisualPremiumRewardMultiplier VisualPremiumRewardMultiplier { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        VehicleGameModeParameterSet.Decimal.BattleTime BattleTime { get; }

        /// <summary> The full name of the vehicle. </summary>
        IVehicleLocalization FullName { get; }

        /// <summary> The name of the vehicle shown in the research tree. </summary>
        IVehicleLocalization ResearchTreeName { get; }

        /// <summary> The short name of the vehicle. </summary>
        IVehicleLocalization ShortName { get; }

        /// <summary> The name of the vehicle's <see cref="Class"/>. </summary>
        IVehicleLocalization ClassName { get; }

        #endregion Association Properties
        #region Non-Persistent Properties

        /// <summary> Returns the <see cref="Rank"/> as an item of <see cref="ERank"/>. </summary>
        ERank RankAsEnumerationItem { get; }

        /// <summary> Values used for matchmaking (falling into a ± 1.0 battle rating bracket). </summary>
        VehicleGameModeParameterSet.String.BattleRating BattleRatingFormatted { get; }

        /// <summary> Indicates whether the vehicle can be unlocked for free with research. </summary>
        bool NotResearchable { get; }

        #endregion Non-Persistent Properties
        #region Methods: Initialization

        /// <summary> Fills properties of the object with values deserialized from JSON data read from "shop.blkx". </summary>
        /// <param name="deserializedResearchTreeVehicle"> The temporary non-persistent object storing deserialized data. </param>
        void InitializeWithDeserializedResearchTreeJson(ResearchTreeVehicleFromJson deserializedResearchTreeVehicle);

        /// <summary> Performs additional initialization with data deserialized from "unittags.blkx". </summary>
        /// <param name="deserializedVehicleData"></param>
        void InitializeWithDeserializedAdditionalVehicleDataJson(VehicleDeserializedFromJsonUnitTags deserializedVehicleData);

        /// <summary> Initializes localization association properties. </summary>
        /// <param name="fullName"> The full name of the vehicle. </param>
        /// <param name="researchTreeName"> The name of the vehicle shown in the research tree. </param>
        /// <param name="shortName"> The short name of the vehicle. </param>
        /// <param name="className"> The name of the vehicle's <see cref="Class"/>. </param>
        void InitializeLocalization(IList<string> fullName, IList<string> researchTreeName, IList<string> shortName, IList<string> className);

        #endregion Methods: Initialization
    }
}