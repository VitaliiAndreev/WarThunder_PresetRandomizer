﻿using Core.DataBase.Objects.Interfaces;
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

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        int CrewCount { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        int GunnersCount { get; }

        #endregion Crew
        #region General

        /// <summary> The vehicle's nation. </summary>
        string Nation { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        string MoveType { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        string Class { get; }

        /// <summary> Whether this vehicle is hidden. </summary>
        bool? ShowOnlyWhenBought { get; }

        /// <summary> The category of hidden vehicles this one belongs to. </summary>
        string CategoryOfHiddenVehicles { get; }

        /// <summary> The amount of research required to unlock the vehicle. </summary>
        int? UnlockCostInResearch { get; }

        /// <summary>
        /// The price of purchasing the vehicle with Silver Lions.
        /// Zero means that the vehicle cannot be bought for Silver Lions.
        /// </summary>
        int PurchaseCostInSilver { get; }

        /// <summary> The price of purchasing the vehicle with Golden Eagles. </summary>
        int? PurchaseCostInGold { get; }

        /// <summary> The vehicle that has to be researched / unlocked before this one can be purchased. </summary>
        string VehicleRequired { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        string SpawnType { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY, NULL VALUES SEEM TO MEAN 1 YET THERE ARE EXPLICIT ONES] </summary>
        int? NumberOfSpawnsInSimulation { get; }

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
        int? BulletsIconParam { get; }

        #endregion Graphics
        #region Modifications

        /// <summary> The modification that is considered an upgrade of the vehicle's armaments. </summary>
        string WeaponUpgrade1 { get; }

        /// <summary> The modification that is considered an upgrade of the vehicle's armaments. </summary>
        string WeaponUpgrade2 { get; }

        /// <summary> The modification that is considered an upgrade of the vehicle's armaments. </summary>
        string WeaponUpgrade3 { get; }

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

        #endregion Modifications
        #region Performance

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal? Speed { get; }

        /// <summary> The vehicle's turret traverse speeds. </summary>
        List<decimal> TurretTraverseSpeeds { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal? CannonReloadTime { get; }

        /// <summary> Whether the vehicle's main armament comes equipped with an auto-loader (grants fixed reload speed that doesn't depend on the loader and doesn't improve with the loader's skill). </summary>
        bool? PrimaryWeaponHasAutoLoader { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal? MaximumRocketDeltaAngle { get; }

        #endregion Performance
        #region Rank

        /// <summary> The vehicle's research rank. </summary>
        int Rank { get; }

        /// <summary> [OBSOLETE, NOW AN INTERNAL VALUE] The vehicle's rank (the predecessor of the <see cref="BattleRatingInArcade"/>) in Arcade Battles. The battle rating is being calculated from it. </summary>
        int EconomicRankInArcade { get; }

        /// <summary> [OBSOLETE, NOW AN INTERNAL VALUE] The vehicle's rank (the predecessor of the <see cref="BattleRatingInRealistic"/>) in Realistic Battles. The battle rating is being calculated from it. </summary>
        int EconomicRankInRealistic { get; }

        /// <summary> [OBSOLETE, NOW AN INTERNAL VALUE] The vehicle's rank (the predecessor of the <see cref="BattleRatingInSimulation"/>) in Simulator Battles. The battle rating is being calculated from it. </summary>
        int? EconomicRankInSimulation { get; }

        /// <summary> The value used for matchmaking (falling into a ± 1.0 battle rating bracket) in Arcade Battles. </summary>
        string BattleRatingInArcade { get; }

        /// <summary> The value used for matchmaking (falling into a ± 1.0 battle rating bracket) in Realistic Battles. </summary>
        string BattleRatingInRealistic { get; }

        /// <summary> The value used for matchmaking (falling into a ± 1.0 battle rating bracket) in Simulator Battles. </summary>
        string BattleRatingInSimulation { get; }

        #endregion Rank
        #region Repairs

        /// <summary>
        /// The full time needed for the vehicle to be repaired for free while being in the currently selected preset in Arcade Battles.
        /// Reserve vehicles don't need repairs.
        /// </summary>
        decimal RepairTimeWithCrewInArcade { get; }

        /// <summary>
        /// The full time needed for the vehicle to be repaired for free while being in the currently selected preset in Realistic Battles.
        /// Reserve vehicles don't need repairs.
        /// </summary>
        decimal RepairTimeWithCrewInRealistic { get; }

        /// <summary>
        /// The full time needed for the vehicle to be repaired for free while being in the currently selected preset in Simulator Battles.
        /// Reserve vehicles don't need repairs.
        /// </summary>
        decimal RepairTimeWithCrewInSimulation { get; }

        /// <summary>
        /// The full time needed for the vehicle to be repaired for free while not being in the currently selected preset in Arcade Battles.
        /// Reserve vehicles don't need repairs.
        /// </summary>
        decimal RepairTimeWithoutCrewInArcade { get; }

        /// <summary>
        /// The full time needed for the vehicle to be repaired for free while not being in the currently selected preset in Realistic Battles.
        /// Reserve vehicles don't need repairs.
        /// </summary>
        decimal RepairTimeWithoutCrewInRealistic { get; }

        /// <summary>
        /// The full time needed for the vehicle to be repaired for free while not being in the currently selected preset in Simulator Battles.
        /// Reserve vehicles don't need repairs.
        /// </summary>
        decimal RepairTimeWithoutCrewInSimulation { get; }

        /// <summary>
        /// The full Silver Lion cost for repairing or auto-repairing the vehicle in Arcade Battles.
        /// Reserve vehicles don't need repairs.
        /// </summary>
        int RepairCostInArcade { get; }

        /// <summary>
        /// The full Silver Lion cost for repairing or auto-repairing the vehicle in Realistic Battles.
        /// Reserve vehicles don't need repairs.
        /// </summary>
        int RepairCostInRealistic { get; }

        /// <summary>
        /// The full Silver Lion cost for repairing or auto-repairing the vehicle in Simulator Battles.
        /// Reserve vehicles don't need repairs.
        /// </summary>
        int RepairCostInSimulation { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY, ALL PREMIUM (NON-GIFT) VEHICLES HAVE IT] </summary>
        int? FreeRepairs { get; }

        #endregion Repairs
        #region Rewards

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        int BattleTimeAwardInArcade { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        int BattleTimeAwardInRealistic { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        int BattleTimeAwardInSimulation { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        int AverageAwardInArcade { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        int AverageAwardInRealistic { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        int AverageAwardInSimulation { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal RewardMultiplierInArcade { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal RewardMultiplierInRealistic { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal RewardMultiplierInSimulation { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal VisualRewardMultiplierInArcade { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal VisualRewardMultiplierInRealistic { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal VisualRewardMultiplierInSimulation { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal? VisualPremiumRewardMultiplierInArcade { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal? VisualPremiumRewardMultiplierInRealistic { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal? VisualPremiumRewardMultiplierInSimulation { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal ResearchRewardMultiplier { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal GroundKillRewardMultiplier { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal BattleTimeArcade { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal BattleTimeRealistic { get; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        decimal BattleTimeSimulation { get; }

        #endregion Rewards

        #endregion Persistent Properties
    }
}
