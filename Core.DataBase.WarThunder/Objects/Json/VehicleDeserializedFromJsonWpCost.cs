using Core.DataBase.WarThunder.Attributes;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Json
{
    /// <summary>
    /// A mapping entity used for automated deserialization of JSON data before passing it on into persistent objects.
    /// <para> Partially generated with QuickType (https://app.quicktype.io). </para>
    /// </summary>
    public class VehicleDeserializedFromJsonWpCost : DeserializedFromJsonWithGaijinId
    {
        #region Required

        #region Crew

        [JsonProperty("trainCost", Required = Required.Always)]
        public int BaseCrewTrainCostInSilver { get; set; }

        [JsonProperty("train2Cost", Required = Required.Always)]
        public int ExpertCrewTrainCostInSilver { get; set; }

        [JsonProperty("train3Cost_gold", Required = Required.Always)]
        public int AceCrewTrainCostInGold { get; set; }

        [JsonProperty("train3Cost_exp", Required = Required.Always)]
        public int AceCrewTrainCostInResearch { get; set; }

        [JsonProperty("crewTotalCount", Required = Required.Always)]
        public int CrewCount { get; set; }

        [JsonProperty("gunnersCount", Required = Required.Always)]
        public int GunnersCount { get; set; }

        #endregion Crew
        #region General

        [JsonProperty("country", Required = Required.Always)]
        public string NationGaijinId { get; set; }

        public bool IsPremium { get; set; }

        [JsonProperty("unitMoveType", Required = Required.Always)]
        public string MoveType { get; set; }

        /// <summary> The vehicle's target class, reward multipliers for whom are defined in warpoints.blkx. </summary>
        [JsonProperty("unitClass", Required = Required.Always)]
        public string TargetClassGaijinId { get; set; }

        [JsonProperty("value", Required = Required.Always)]
        public int PurchaseCostInSilver { get; set; }

        #endregion General
        #region Modifications

        [JsonProperty("modifications", Required = Required.Always)]
        public Dictionary<string, ModificationDeserializedFromJson> Modifications { get; set; }

        [JsonProperty("needBuyToOpenNextInTier1", Required = Required.Always)]
        public int AmountOfModificationsResearchedIn_Tier0_RequiredToUnlock_Tier1 { get; set; }

        [JsonProperty("needBuyToOpenNextInTier2", Required = Required.Always)]
        public int AmountOfModificationsResearchedIn_Tier1_RequiredToUnlock_Tier2 { get; set; }

        [JsonProperty("needBuyToOpenNextInTier3", Required = Required.Always)]
        public int AmountOfModificationsResearchedIn_Tier2_RequiredToUnlock_Tier3 { get; set; }

        [JsonProperty("needBuyToOpenNextInTier4", Required = Required.Always)]
        public int AmountOfModificationsResearchedIn_Tier3_RequiredToUnlock_Tier4 { get; set; }

        [JsonProperty("spare", Required = Required.Always)]
        public BackupSortieDeserializedFromJson BackupSortie { get; set; }

        #endregion Modifications
        #region Rank

        [JsonProperty("rank", Required = Required.Always)]
        public int Rank { get; set; }

        [JsonProperty("economicRankArcade", Required = Required.Always)]
        [PersistAsDictionaryItem(nameof(IVehicle.EconomicRank), EGameMode.Arcade)]
        public int EconomicRankInArcade { get; set; }

        [JsonProperty("economicRankHistorical", Required = Required.Always)]
        [PersistAsDictionaryItem(nameof(IVehicle.EconomicRank), EGameMode.Realistic)]
        public int EconomicRankInRealistic { get; set; }

        #endregion Rank
        #region Repairs

        [JsonProperty("repairTimeHrsArcade", Required = Required.Always)]
        public decimal RepairTimeWithCrewInArcade { get; set; }

        [JsonProperty("repairTimeHrsHistorical", Required = Required.Always)]
        public decimal RepairTimeWithCrewInRealistic { get; set; }

        [JsonProperty("repairTimeHrsSimulation", Required = Required.Always)]
        public decimal RepairTimeWithCrewInSimulation { get; set; }

        [JsonProperty("repairTimeHrsNoCrewArcade", Required = Required.Always)]
        public decimal RepairTimeWithoutCrewInArcade { get; set; }

        [JsonProperty("repairTimeHrsNoCrewHistorical", Required = Required.Always)]
        public decimal RepairTimeWithoutCrewInRealistic { get; set; }

        [JsonProperty("repairTimeHrsNoCrewSimulation", Required = Required.Always)]
        public decimal RepairTimeWithoutCrewInSimulation { get; set; }

        [JsonProperty("repairCostArcade", Required = Required.Always)]
        public int RepairCostInArcade { get; set; }

        [JsonProperty("repairCostHistorical", Required = Required.Always)]
        public int RepairCostInRealistic { get; set; }

        [JsonProperty("repairCostSimulation", Required = Required.Always)]
        public int RepairCostInSimulation { get; set; }

        #endregion Repairs
        #region Rewards

        [JsonProperty("battleTimeAwardArcade", Required = Required.Always)]
        public int BattleTimeAwardInArcade { get; set; }

        [JsonProperty("battleTimeAwardHistorical", Required = Required.Always)]
        public int BattleTimeAwardInRealistic { get; set; }

        [JsonProperty("battleTimeAwardSimulation", Required = Required.Always)]
        public int BattleTimeAwardInSimulation { get; set; }

        [JsonProperty("avgAwardArcade", Required = Required.Always)]
        public int AverageAwardInArcade { get; set; }

        [JsonProperty("avgAwardHistorical", Required = Required.Always)]
        public int AverageAwardInRealistic { get; set; }

        [JsonProperty("avgAwardSimulation", Required = Required.Always)]
        public int AverageAwardInSimulation { get; set; }

        [JsonProperty("rewardMulArcade", Required = Required.Always)]
        public decimal RewardMultiplierInArcade { get; set; }

        [JsonProperty("rewardMulHistorical", Required = Required.Always)]
        public decimal RewardMultiplierInRealistic { get; set; }

        [JsonProperty("rewardMulSimulation", Required = Required.Always)]
        public decimal RewardMultiplierInSimulation { get; set; }

        [JsonProperty("rewardMulVisualArcade", Required = Required.Always)]
        public decimal VisualRewardMultiplierInArcade { get; set; }

        [JsonProperty("rewardMulVisualHistorical", Required = Required.Always)]
        public decimal VisualRewardMultiplierInRealistic { get; set; }

        [JsonProperty("rewardMulVisualSimulation", Required = Required.Always)]
        public decimal VisualRewardMultiplierInSimulation { get; set; }

        [JsonProperty("expMul", Required = Required.Always)]
        public decimal ResearchRewardMultiplier { get; set; }

        [JsonProperty("groundKillMul", Required = Required.Always)]
        public decimal GroundKillRewardMultiplier { get; set; }

        [JsonProperty("battleTimeArcade", Required = Required.Always)]
        public decimal BattleTimeArcade { get; set; }

        [JsonProperty("battleTimeHistorical", Required = Required.Always)]
        public decimal BattleTimeRealistic { get; set; }

        [JsonProperty("battleTimeSimulation", Required = Required.Always)]
        public decimal BattleTimeSimulation { get; set; }

        #endregion Rewards
        #region Weapons

        [JsonProperty("weapons", Required = Required.Always)]
        public Dictionary<string, WeaponDeserializedFromJson> Weapons { get; set; }

        #endregion Weapons

        #endregion Required
        #region NotRequired

        #region Crew

        [JsonProperty("minCrewMemberAliveCount")]
        public int? MinumumCrewCountToOperate { get; set; }

        #endregion Crew
        #region General

        [JsonProperty("showOnlyWhenBought")]
        public bool ShowOnlyWhenBought { get; set; }

        [JsonProperty("gift")]
        public string CategoryOfHiddenVehicles { get; set; }

        [JsonProperty("giftParam")]
        public string OwnershipGiftPrerequisite { get; set; }

        [JsonProperty("isFirstBattleAward")]
        public bool GiftedToNewPlayersForSelectingTheirFirstBranch { get; set; }

        [JsonProperty("purchaseTrophyGiftOnce")]
        public string OwnershipPurchasePrerequisite { get; set; }

        [JsonProperty("researchType")]
        public string ResearchUnlockType { get; set; }

        [JsonProperty("reqExp")]
        public int? UnlockCostInResearch { get; set; }

        [JsonProperty("costGold")]
        public int? PurchaseCostInGold { get; set; }

        [JsonProperty("minOpenCostGold")]
        public int? DiscountedPurchaseCostInGold { get; set; }

        [JsonProperty("reqAir")]
        public string RequiredVehicleGaijinId { get; set; }

        [JsonProperty("spawnType")]
        public string SpawnType { get; set; }

        /// <summary> This property is used only by walking tanks introduced in 1st April 2015 and later used in Operation S.U.M.M.E.R. </summary>
        [JsonProperty("numSpawnsPerBattle")]
        public int? NumberOfSpawnsInEvents { get; set; }

        [JsonProperty("numSpawnsPerBattleArcade")]
        public int? NumberOfSpawnsInArcade { get; set; }

        [JsonProperty("numSpawnsPerBattleHistorical")]
        public int? NumberOfSpawnsInRealistic { get; set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY, NULL VALUES SEEM TO MEAN 1 YET THERE ARE EXPLICIT ONES FOR NAVY, HELICOPTERS, AND SOME HEAVY TANKS] </summary>
        [JsonProperty("numSpawnsPerBattleSimulation")]
        public int? NumberOfSpawnsInSimulation { get; set; }

        [JsonProperty("killStreak")]
        public bool CanSpawnAsKillStreak { get; set; }

        #endregion General
        #region Graphics

        [JsonProperty("customClassIco")]
        public string CustomClassIco { get; set; }

        [JsonProperty("customImage")]
        public string CustomImage { get; set; }

        [JsonProperty("customTooltipImage")]
        public string CustomTooltipImage { get; set; }

        [JsonProperty("commonWeaponImage")]
        public string CommonWeaponImage { get; set; }

        [JsonProperty("weaponmask")]
        public int? WeaponMask { get; set; }

        [JsonProperty("bulletsIconParam")]
        public int? BulletsIconParam { get; set; }

        #endregion Graphics
        #region Performance

        // "Shop"

        [JsonProperty("speed")]
        public decimal? Speed { get; set; }

        [JsonProperty("maxFlightTimeMinutes")]
        public int? MaximumFlightTime { get; set; }

        [JsonProperty("extinguisherMinTime")]
        public decimal? MaximumFireExtinguishingTime { get; set; }

        [JsonProperty("breachRepairSpeed")]
        public decimal? HullBreachRepairSpeed { get; set; }

        #endregion Performance
        #region Rank

        [JsonProperty("economicRankSimulation")]
        [PersistAsDictionaryItem(nameof(IVehicle.EconomicRank), EGameMode.Simulator)]
        public int? EconomicRankInSimulation { get; set; }

        #endregion Rank
        #region Repairs

        [JsonProperty("freeRepairs")]
        public int? FreeRepairs { get; set; }

        #endregion Repairs
        #region Rewards

        [JsonProperty("premRewardMulVisualArcade")]
        public decimal? VisualPremiumRewardMultiplierInArcade { get; set; }

        [JsonProperty("premRewardMulVisualHistorical")]
        public decimal? VisualPremiumRewardMultiplierInRealistic { get; set; }

        [JsonProperty("premRewardMulVisualSimulation")]
        public decimal? VisualPremiumRewardMultiplierInSimulation { get; set; }

        #endregion Rewards
        #region Weapons

        [JsonProperty("turretSpeed")]
        public List<decimal?> TurretTraverseSpeeds { get; set; }

        [JsonProperty("reloadTime_mgun")]
        public decimal? MachineGunReloadTime { get; set; }

        [JsonProperty("reloadTime_cannon")]
        public decimal? CannonReloadTime { get; set; }

        [JsonProperty("reloadTime_gunner")]
        public decimal? GunnerReloadTime { get; set; }

        [JsonProperty("maxAmmo")]
        public int? MaximumAmmunition { get; set; }

        [JsonProperty("primaryWeaponAutoLoader")]
        public bool PrimaryWeaponHasAutoLoader { get; set; }

        [JsonProperty("maxDeltaAngle_rockets")]
        public decimal? MaximumRocketDeltaAngle { get; set; }

        [JsonProperty("maxDeltaAngle_atgm")]
        public decimal? MaximumAtgmDeltaAngle { get; set; }

        [JsonProperty("weaponUpgrade1")]
        public string WeaponUpgrade1 { get; set; }

        [JsonProperty("weaponUpgrade2")]
        public string WeaponUpgrade2 { get; set; }

        [JsonProperty("weaponUpgrade3")]
        public string WeaponUpgrade3 { get; set; }

        [JsonProperty("weaponUpgrade4")]
        public string WeaponUpgrade4 { get; set; }

        #region Ship Cannon Data

        // Properties in this group below are here to match JSON object composition, even though these should not be here.

        [JsonProperty("shipMainCaliberReloadTime_76mm_F34_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_76mm_F34_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_76mm_F34_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_76mm_F34_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_12_7mm_DShK_naval_user_machinegun")]
        public decimal? ShipAuxCaliberReloadTime_12_7mm_DShK_naval_user_machinegun { get; set; }

        [JsonProperty("shipAuxMaxAngle_12_7mm_DShK_naval_user_machinegun")]
        public decimal? ShipAuxMaxAngle_12_7mm_DShK_naval_user_machinegun { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_12_7mm_DShK_naval_user_machinegun")]
        public decimal? ShipAntiAirCaliberReloadTime_12_7mm_DShK_naval_user_machinegun { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_12_7mm_DShK_naval_user_machinegun")]
        public decimal? ShipAntiAirMaxAngle_12_7mm_DShK_naval_user_machinegun { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_37mm_70_K_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_37mm_70_K_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_37mm_70_K_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_37mm_70_K_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_45mm_21K_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_45mm_21K_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_45mm_21K_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_45mm_21K_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_37mm_70_K_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_37mm_70_K_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_37mm_70_K_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_37mm_70_K_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_12_7mm_M2_HB_naval_user_machinegun")]
        public decimal? ShipAntiAirCaliberReloadTime_12_7mm_M2_HB_naval_user_machinegun { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_12_7mm_M2_HB_naval_user_machinegun")]
        public decimal? ShipAntiAirMaxAngle_12_7mm_M2_HB_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_30mm_MK103_38_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_30mm_MK103_38_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_30mm_MK103_38_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_30mm_MK103_38_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_20mm_FlaK38_vierling_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_20mm_FlaK38_vierling_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_20mm_FlaK38_vierling_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_20mm_FlaK38_vierling_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_20mm_zwilling_MG_C38_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_20mm_zwilling_MG_C38_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_20mm_zwilling_MG_C38_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_20mm_zwilling_MG_C38_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_7_92mm_MG15_navy_user_machinegun")]
        public decimal? ShipAntiAirCaliberReloadTime_7_92mm_MG15_navy_user_machinegun { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_7_92mm_MG15_navy_user_machinegun")]
        public decimal? ShipAntiAirMaxAngle_7_92mm_MG15_navy_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_15mm_MG151_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_15mm_MG151_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_15mm_MG151_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_15mm_MG151_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_20mm_MG151_20_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_20mm_MG151_20_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_20mm_MG151_20_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_20mm_MG151_20_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_40mm_QF_2pdr_mk_XV_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_40mm_QF_2pdr_mk_XV_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_40mm_QF_2pdr_mk_XV_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_40mm_QF_2pdr_mk_XV_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_40mm_2pdr_Rolls_Royce_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_40mm_2pdr_Rolls_Royce_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_40mm_2pdr_Rolls_Royce_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_40mm_2pdr_Rolls_Royce_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_12_7mm_05_Vickers_mk_V_naval_user_machinegun")]
        public decimal? ShipAuxCaliberReloadTime_12_7mm_05_Vickers_mk_V_naval_user_machinegun { get; set; }

        [JsonProperty("shipAuxMaxAngle_12_7mm_05_Vickers_mk_V_naval_user_machinegun")]
        public decimal? ShipAuxMaxAngle_12_7mm_05_Vickers_mk_V_naval_user_machinegun { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_7_7mm_Vikkers_GO_No5_naval_user_machinegun")]
        public decimal? ShipAntiAirCaliberReloadTime_7_7mm_Vikkers_GO_No5_naval_user_machinegun { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_7_7mm_Vikkers_GO_No5_naval_user_machinegun")]
        public decimal? ShipAntiAirMaxAngle_7_7mm_Vikkers_GO_No5_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_12_7mm_M2_HB_naval_user_machinegun")]
        public decimal? ShipMainCaliberReloadTime_12_7mm_M2_HB_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_12_7mm_M2_HB_naval_user_machinegun")]
        public decimal? ShipMainCaliberMaxAngle_12_7mm_M2_HB_naval_user_machinegun { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_20mm_Oerlikon_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_20mm_Oerlikon_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_20mm_Oerlikon_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_20mm_Oerlikon_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_37mm_AN_M4_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_37mm_AN_M4_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_37mm_AN_M4_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_37mm_AN_M4_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_40mm_l60_bofors_mk3_single_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_40mm_l60_bofors_mk3_single_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_40mm_l60_bofors_mk3_single_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_40mm_l60_bofors_mk3_single_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_85mm_ZiS_S_53_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_85mm_ZiS_S_53_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_85mm_ZiS_S_53_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_85mm_ZiS_S_53_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_88mm_SKC_35_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_88mm_SKC_35_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_88mm_SKC_35_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_88mm_SKC_35_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_37mm_SKC_30_naval_user_cannon")]
        public decimal? ShipAntiAirCaliberReloadTime_37mm_SKC_30_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_37mm_SKC_30_naval_user_cannon")]
        public decimal? ShipAntiAirMaxAngle_37mm_SKC_30_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_15mm_MG_M38_t_naval_user_machinegun")]
        public decimal? ShipAuxCaliberReloadTime_15mm_MG_M38_t_naval_user_machinegun { get; set; }

        [JsonProperty("shipAuxMaxAngle_15mm_MG_M38_t_naval_user_machinegun")]
        public decimal? ShipAuxMaxAngle_15mm_MG_M38_t_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_57mm_6pdr_7CWT_QF_Mk_11A_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_57mm_6pdr_7CWT_QF_Mk_11A_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_57mm_6pdr_7CWT_QF_Mk_11A_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_57mm_6pdr_7CWT_QF_Mk_11A_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_20mm_Oerlikon_mk_v_twin_mount_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_20mm_Oerlikon_mk_v_twin_mount_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_20mm_Oerlikon_mk_v_twin_mount_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_20mm_Oerlikon_mk_v_twin_mount_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_12_7mm_05_Vickers_mk_V_naval_user_machinegun")]
        public decimal? ShipAntiAirCaliberReloadTime_12_7mm_05_Vickers_mk_V_naval_user_machinegun { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_12_7mm_05_Vickers_mk_V_naval_user_machinegun")]
        public decimal? ShipAntiAirMaxAngle_12_7mm_05_Vickers_mk_V_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_12_7mm_DShK_naval_user_machinegun")]
        public decimal? ShipMainCaliberReloadTime_12_7mm_DShK_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_12_7mm_DShK_naval_user_machinegun")]
        public decimal? ShipMainCaliberMaxAngle_12_7mm_DShK_naval_user_machinegun { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_37mm_FlaKM42_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_37mm_FlaKM42_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_37mm_FlaKM42_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_37mm_FlaKM42_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_20mm_zwilling_MG_C38_naval_user_cannon")]
        public decimal? ShipAntiAirCaliberReloadTime_20mm_zwilling_MG_C38_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_20mm_zwilling_MG_C38_naval_user_cannon")]
        public decimal? ShipAntiAirMaxAngle_20mm_zwilling_MG_C38_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_20mm_FlaK38_vierling_naval_user_cannon")]
        public decimal? ShipAntiAirCaliberReloadTime_20mm_FlaK38_vierling_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_20mm_FlaK38_vierling_naval_user_cannon")]
        public decimal? ShipAntiAirMaxAngle_20mm_FlaK38_vierling_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_25mm_2M_3_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_25mm_2M_3_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_25mm_2M_3_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_25mm_2M_3_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_40mm_L70_Bofors_MEL_58_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_40mm_L70_Bofors_MEL_58_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_40mm_L70_Bofors_MEL_58_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_40mm_L70_Bofors_MEL_58_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_114mm_8cwt_QF_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_114mm_8cwt_QF_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_114mm_8cwt_QF_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_114mm_8cwt_QF_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_40mm_qf_bofors_mk7_single_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_40mm_qf_bofors_mk7_single_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_40mm_qf_bofors_mk7_single_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_40mm_qf_bofors_mk7_single_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_40mm_qf_bofors_mk7_single_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_40mm_qf_bofors_mk7_single_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_40mm_qf_bofors_mk7_single_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_40mm_qf_bofors_mk7_single_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_120mm_Mark_XII_CP_XIX_mount_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_120mm_Mark_XII_CP_XIX_mount_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_120mm_Mark_XII_CP_XIX_mount_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_120mm_Mark_XII_CP_XIX_mount_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_102mm_Mark_XVI_Mark_XIX_mount_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_102mm_Mark_XVI_Mark_XIX_mount_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_102mm_Mark_XVI_Mark_XIX_mount_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_102mm_Mark_XVI_Mark_XIX_mount_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_40mm_QF_2pdr_mk_XV_naval_user_cannon")]
        public decimal? ShipAntiAirCaliberReloadTime_40mm_QF_2pdr_mk_XV_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_40mm_QF_2pdr_mk_XV_naval_user_cannon")]
        public decimal? ShipAntiAirMaxAngle_40mm_QF_2pdr_mk_XV_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_20mm_Oerlikon_naval_user_cannon")]
        public decimal? ShipAntiAirCaliberReloadTime_20mm_Oerlikon_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_20mm_Oerlikon_naval_user_cannon")]
        public decimal? ShipAntiAirMaxAngle_20mm_Oerlikon_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_14_5mm_KPVT_naval_user_machinegun")]
        public decimal? ShipAuxCaliberReloadTime_14_5mm_KPVT_naval_user_machinegun { get; set; }

        [JsonProperty("shipAuxMaxAngle_14_5mm_KPVT_naval_user_machinegun")]
        public decimal? ShipAuxMaxAngle_14_5mm_KPVT_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_130mm_B_13_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_130mm_B_13_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_130mm_B_13_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_130mm_B_13_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_76mm_34K_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_76mm_34K_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_76mm_34K_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_76mm_34K_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_37mm_70_K_naval_user_cannon")]
        public decimal? ShipAntiAirCaliberReloadTime_37mm_70_K_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_37mm_70_K_naval_user_cannon")]
        public decimal? ShipAntiAirMaxAngle_37mm_70_K_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_20mm_Oerlikon_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_20mm_Oerlikon_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_20mm_Oerlikon_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_20mm_Oerlikon_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_40mm_l60_bofors_mk3_single_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_40mm_l60_bofors_mk3_single_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_40mm_l60_bofors_mk3_single_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_40mm_l60_bofors_mk3_single_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_127mm_5_38_Mark_12_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_127mm_5_38_Mark_12_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_127mm_5_38_Mark_12_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_127mm_5_38_Mark_12_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_40mm_l60_bofors_mk1_twin_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_40mm_l60_bofors_mk1_twin_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_40mm_l60_bofors_mk1_twin_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_40mm_l60_bofors_mk1_twin_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_127mm_skc34_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_127mm_skc34_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_127mm_skc34_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_127mm_skc34_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_37mm_SKC_30_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_37mm_SKC_30_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_37mm_SKC_30_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_37mm_SKC_30_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_20mm_FlaK38_naval_user_cannon")]
        public decimal? ShipAntiAirCaliberReloadTime_20mm_FlaK38_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_20mm_FlaK38_naval_user_cannon")]
        public decimal? ShipAntiAirMaxAngle_20mm_FlaK38_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_20mm_MG_C38_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_20mm_MG_C38_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_20mm_MG_C38_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_20mm_MG_C38_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_20mm_zwilling_MG_C38_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_20mm_zwilling_MG_C38_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_20mm_zwilling_MG_C38_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_20mm_zwilling_MG_C38_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_7_92mm_MG34_naval_user_machinegun")]
        public decimal? ShipAntiAirCaliberReloadTime_7_92mm_MG34_naval_user_machinegun { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_7_92mm_MG34_naval_user_machinegun")]
        public decimal? ShipAntiAirMaxAngle_7_92mm_MG34_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_105mm_SK_C32_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_105mm_SK_C32_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_105mm_SK_C32_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_105mm_SK_C32_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_20mm_MGC30_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_20mm_MGC30_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_20mm_MGC30_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_20mm_MGC30_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_76mm_Melara_76_62_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_76mm_Melara_76_62_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_76mm_Melara_76_62_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_76mm_Melara_76_62_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_7_92mm_MG34_naval_user_machinegun")]
        public decimal? ShipAuxCaliberReloadTime_7_92mm_MG34_naval_user_machinegun { get; set; }

        [JsonProperty("shipAuxMaxAngle_7_92mm_MG34_naval_user_machinegun")]
        public decimal? ShipAuxMaxAngle_7_92mm_MG34_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_20mm_FlaK38_vierling_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_20mm_FlaK38_vierling_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_20mm_FlaK38_vierling_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_20mm_FlaK38_vierling_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_88mm_Flak36_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_88mm_Flak36_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_88mm_Flak36_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_88mm_Flak36_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_20mm_FlaK38_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_20mm_FlaK38_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_20mm_FlaK38_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_20mm_FlaK38_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_81mm_mk_2_mortar_naval_user_cannon")]
        public decimal? ShipAntiAirCaliberReloadTime_81mm_mk_2_mortar_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_81mm_mk_2_mortar_naval_user_cannon")]
        public decimal? ShipAntiAirMaxAngle_81mm_mk_2_mortar_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_12_7mm_M2_HB_naval_user_machinegun")]
        public decimal? ShipAuxCaliberReloadTime_12_7mm_M2_HB_naval_user_machinegun { get; set; }

        [JsonProperty("shipAuxMaxAngle_12_7mm_M2_HB_naval_user_machinegun")]
        public decimal? ShipAuxMaxAngle_12_7mm_M2_HB_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_76mm_50cal_mk34_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_76mm_50cal_mk34_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_76mm_50cal_mk34_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_76mm_50cal_mk34_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_76mm_3_50_mk10_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_76mm_3_50_mk10_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_76mm_3_50_mk10_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_76mm_3_50_mk10_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_40mm_l60_bofors_mk1_twin_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_40mm_l60_bofors_mk1_twin_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_40mm_l60_bofors_mk1_twin_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_40mm_l60_bofors_mk1_twin_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_76mm_3_50_mk10_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_76mm_3_50_mk10_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_76mm_3_50_mk10_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_76mm_3_50_mk10_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_152mm_6inch_45_bl_mk_xii_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_152mm_6inch_45_bl_mk_xii_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_152mm_6inch_45_bl_mk_xii_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_152mm_6inch_45_bl_mk_xii_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_102mm_Mark_V_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_102mm_Mark_V_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_102mm_Mark_V_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_102mm_Mark_V_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_150mm_l_48_c_36_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_150mm_l_48_c_36_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_150mm_l_48_c_36_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_150mm_l_48_c_36_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_105mm_SK_C32_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_105mm_SK_C32_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_105mm_SK_C32_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_105mm_SK_C32_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_40mm_4cm_flak_bofors_28_naval_user_cannon")]
        public decimal? ShipAntiAirCaliberReloadTime_40mm_4cm_flak_bofors_28_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_40mm_4cm_flak_bofors_28_naval_user_cannon")]
        public decimal? ShipAntiAirMaxAngle_40mm_4cm_flak_bofors_28_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_152mm_6inch_53_mk_12_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_152mm_6inch_53_mk_12_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_152mm_6inch_53_mk_12_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_152mm_6inch_53_mk_12_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_28mm_75_mk_1_chicago_piano_naval_user_cannon")]
        public decimal? ShipAntiAirCaliberReloadTime_28mm_75_mk_1_chicago_piano_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_28mm_75_mk_1_chicago_piano_naval_user_cannon")]
        public decimal? ShipAntiAirMaxAngle_28mm_75_mk_1_chicago_piano_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_180mm_b1k_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_180mm_b1k_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_180mm_b1k_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_180mm_b1k_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_100mm_50_manizini_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_100mm_50_manizini_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_100mm_50_manizini_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_100mm_50_manizini_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_30mm_ak_230_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_30mm_ak_230_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_30mm_ak_230_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_30mm_ak_230_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_102mm_50_4inch_mk9_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_102mm_50_4inch_mk9_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_102mm_50_4inch_mk9_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_102mm_50_4inch_mk9_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_76mm_3_23_mk4_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_76mm_3_23_mk4_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_76mm_3_23_mk4_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_76mm_3_23_mk4_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_120mm_l_45_mark_8_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_120mm_l_45_mark_8_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_120mm_l_45_mark_8_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_120mm_l_45_mark_8_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_20mm_shvak_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_20mm_shvak_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_20mm_shvak_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_20mm_shvak_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_76mm_34K_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_76mm_34K_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_76mm_34K_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_76mm_34K_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_76mm_ak_726_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_76mm_ak_726_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_76mm_ak_726_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_76mm_ak_726_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_45mm_21K_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_45mm_21K_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_45mm_21K_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_45mm_21K_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_14_5mm_KPVT_naval_user_machinegun")]
        public decimal? ShipMainCaliberReloadTime_14_5mm_KPVT_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_14_5mm_KPVT_naval_user_machinegun")]
        public decimal? ShipMainCaliberMaxAngle_14_5mm_KPVT_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_57mm_ak_725_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_57mm_ak_725_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_57mm_ak_725_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_57mm_ak_725_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_25mm_2M_3_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_25mm_2M_3_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_25mm_2M_3_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_25mm_2M_3_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_85mm_90_K_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_85mm_90_K_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_85mm_90_K_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_85mm_90_K_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_76mm_D56_TS_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_76mm_D56_TS_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_76mm_D56_TS_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_76mm_D56_TS_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_30mm_bp_30_mortar_naval_user_cannon")]
        public decimal? ShipAntiAirCaliberReloadTime_30mm_bp_30_mortar_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_30mm_bp_30_mortar_naval_user_cannon")]
        public decimal? ShipAntiAirMaxAngle_30mm_bp_30_mortar_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_102mm_60_1911_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_102mm_60_1911_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_102mm_60_1911_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_102mm_60_1911_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_76mm_1914_lender_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_76mm_1914_lender_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_76mm_1914_lender_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_76mm_1914_lender_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_7_7mm_lewis_amg_1916_naval_user_machinegun")]
        public decimal? ShipMainCaliberReloadTime_7_7mm_lewis_amg_1916_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_7_7mm_lewis_amg_1916_naval_user_machinegun")]
        public decimal? ShipMainCaliberMaxAngle_7_7mm_lewis_amg_1916_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_7_7mm_mg_lewis_naval_user_machinegun")]
        public decimal? ShipMainCaliberReloadTime_7_7mm_mg_lewis_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_7_7mm_mg_lewis_naval_user_machinegun")]
        public decimal? ShipMainCaliberMaxAngle_7_7mm_mg_lewis_naval_user_machinegun { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_76mm_QQF_3in_20cwt_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_76mm_QQF_3in_20cwt_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_76mm_QQF_3in_20cwt_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_76mm_QQF_3in_20cwt_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_102mm_Mark_V_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_102mm_Mark_V_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_102mm_Mark_V_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_102mm_Mark_V_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_7_7mm_mg_lewis_naval_user_machinegun")]
        public decimal? ShipAntiAirCaliberReloadTime_7_7mm_mg_lewis_naval_user_machinegun { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_7_7mm_mg_lewis_naval_user_machinegun")]
        public decimal? ShipAntiAirMaxAngle_7_7mm_mg_lewis_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_47mm_qf3pdr_hotchkiss_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_47mm_qf3pdr_hotchkiss_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_47mm_qf3pdr_hotchkiss_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_47mm_qf3pdr_hotchkiss_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_7_7mm_lewis_amg_1916_naval_user_machinegun")]
        public decimal? ShipAntiAirCaliberReloadTime_7_7mm_lewis_amg_1916_naval_user_machinegun { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_7_7mm_lewis_amg_1916_naval_user_machinegun")]
        public decimal? ShipAntiAirMaxAngle_7_7mm_lewis_amg_1916_naval_user_machinegun { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_7_7mm_lewis_amg_1916_naval_user_machinegun")]
        public decimal? ShipAuxCaliberReloadTime_7_7mm_lewis_amg_1916_naval_user_machinegun { get; set; }

        [JsonProperty("shipAuxMaxAngle_7_7mm_lewis_amg_1916_naval_user_machinegun")]
        public decimal? ShipAuxMaxAngle_7_7mm_lewis_amg_1916_naval_user_machinegun { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_40mm_2pdr_Rolls_Royce_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_40mm_2pdr_Rolls_Royce_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_40mm_2pdr_Rolls_Royce_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_40mm_2pdr_Rolls_Royce_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_12_7mm_05_Vickers_mk_V_naval_user_machinegun")]
        public decimal? ShipMainCaliberReloadTime_12_7mm_05_Vickers_mk_V_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_12_7mm_05_Vickers_mk_V_naval_user_machinegun")]
        public decimal? ShipMainCaliberMaxAngle_12_7mm_05_Vickers_mk_V_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_114mm_45_QF_MkIV_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_114mm_45_QF_MkIV_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_114mm_45_QF_MkIV_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_114mm_45_QF_MkIV_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_102mm_33_4inch_qf_mkXXIII_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_102mm_33_4inch_qf_mkXXIII_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_102mm_33_4inch_qf_mkXXIII_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_102mm_33_4inch_qf_mkXXIII_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_40mm_qf_bofors_mk5_twin_naval_user_cannon")]
        public decimal? ShipAntiAirCaliberReloadTime_40mm_qf_bofors_mk5_twin_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_40mm_qf_bofors_mk5_twin_naval_user_cannon")]
        public decimal? ShipAntiAirMaxAngle_40mm_qf_bofors_mk5_twin_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_40mm_qf_bofors_staag_mk2_twin_naval_user_cannon")]
        public decimal? ShipAntiAirCaliberReloadTime_40mm_qf_bofors_staag_mk2_twin_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_40mm_qf_bofors_staag_mk2_twin_naval_user_cannon")]
        public decimal? ShipAntiAirMaxAngle_40mm_qf_bofors_staag_mk2_twin_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_40mm_qf_bofors_mk7_single_naval_user_cannon")]
        public decimal? ShipAntiAirCaliberReloadTime_40mm_qf_bofors_mk7_single_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_40mm_qf_bofors_mk7_single_naval_user_cannon")]
        public decimal? ShipAntiAirMaxAngle_40mm_qf_bofors_mk7_single_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_102mm_45_BL_Mk_IX_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_102mm_45_BL_Mk_IX_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_102mm_45_BL_Mk_IX_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_102mm_45_BL_Mk_IX_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_40mm_QF_2pdr_mk_VIII_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_40mm_QF_2pdr_mk_VIII_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_40mm_QF_2pdr_mk_VIII_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_40mm_QF_2pdr_mk_VIII_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_76mm_3_40_12cwt_qf_mk5_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_76mm_3_40_12cwt_qf_mk5_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_76mm_3_40_12cwt_qf_mk5_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_76mm_3_40_12cwt_qf_mk5_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_37mm_AN_M4_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_37mm_AN_M4_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_37mm_AN_M4_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_37mm_AN_M4_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_40mm_l60_bofors_mk2_quad_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_40mm_l60_bofors_mk2_quad_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_40mm_l60_bofors_mk2_quad_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_40mm_l60_bofors_mk2_quad_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_130mm_55_obr1913_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_130mm_55_obr1913_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_130mm_55_obr1913_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_130mm_55_obr1913_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_76mm_39K_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_76mm_39K_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_76mm_39K_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_76mm_39K_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_150mm_60_skc_25_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_150mm_60_skc_25_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_150mm_60_skc_25_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_150mm_60_skc_25_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_88mm_76_skc_32_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_88mm_76_skc_32_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_88mm_76_skc_32_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_88mm_76_skc_32_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_20mm_MGC30_naval_user_cannon")]
        public decimal? ShipAntiAirCaliberReloadTime_20mm_MGC30_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_20mm_MGC30_naval_user_cannon")]
        public decimal? ShipAntiAirMaxAngle_20mm_MGC30_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_133mm_5_25inch_50_qf_mk1_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_133mm_5_25inch_50_qf_mk1_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_133mm_5_25inch_50_qf_mk1_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_133mm_5_25inch_50_qf_mk1_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_28mm_75_mk_1_chicago_piano_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_28mm_75_mk_1_chicago_piano_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_28mm_75_mk_1_chicago_piano_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_28mm_75_mk_1_chicago_piano_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_152mm_6inch_47_mk_16_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_152mm_6inch_47_mk_16_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_152mm_6inch_47_mk_16_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_152mm_6inch_47_mk_16_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_127mm_5inch_25_Mark_13_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_127mm_5inch_25_Mark_13_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_127mm_5inch_25_Mark_13_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_127mm_5inch_25_Mark_13_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_152mm_6inch_50_bl_mk_xxiii_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_152mm_6inch_50_bl_mk_xxiii_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_152mm_6inch_50_bl_mk_xxiii_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_152mm_6inch_50_bl_mk_xxiii_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_40mm_QF_2pdr_mk_VIII_naval_user_cannon")]
        public decimal? ShipAntiAirCaliberReloadTime_40mm_QF_2pdr_mk_VIII_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_40mm_QF_2pdr_mk_VIII_naval_user_cannon")]
        public decimal? ShipAntiAirMaxAngle_40mm_QF_2pdr_mk_VIII_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_180mm_b1p_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_180mm_b1p_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_180mm_b1p_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_180mm_b1p_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_100mm_56_b34_1940_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_100mm_56_b34_1940_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_100mm_56_b34_1940_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_100mm_56_b34_1940_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_45mm_21K_naval_user_cannon")]
        public decimal? ShipAntiAirCaliberReloadTime_45mm_21K_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_45mm_21K_naval_user_cannon")]
        public decimal? ShipAntiAirMaxAngle_45mm_21K_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_152mm_6inch_50_qf_mk_5_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_152mm_6inch_50_qf_mk_5_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_152mm_6inch_50_qf_mk_5_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_152mm_6inch_50_qf_mk_5_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_76mm_70_mark_6_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_76mm_70_mark_6_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_76mm_70_mark_6_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_76mm_70_mark_6_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_76mm_Type_88_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_76mm_Type_88_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_76mm_Type_88_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_76mm_Type_88_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_13_2mm_Type93_naval_user_machinegun")]
        public decimal? ShipAntiAirCaliberReloadTime_13_2mm_Type93_naval_user_machinegun { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_13_2mm_Type93_naval_user_machinegun")]
        public decimal? ShipAntiAirMaxAngle_13_2mm_Type93_naval_user_machinegun { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_25mm_Type_96_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_25mm_Type_96_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_25mm_Type_96_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_25mm_Type_96_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_7_7mm_80_type_92_naval_user_machinegun")]
        public decimal? ShipMainCaliberReloadTime_7_7mm_80_type_92_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_7_7mm_80_type_92_naval_user_machinegun")]
        public decimal? ShipMainCaliberMaxAngle_7_7mm_80_type_92_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_25mm_Type_96_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_25mm_Type_96_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_25mm_Type_96_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_25mm_Type_96_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_37mm_type_94_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_37mm_type_94_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_37mm_type_94_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_37mm_type_94_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_20mm_type_98_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_20mm_type_98_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_20mm_type_98_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_20mm_type_98_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_37mm_type_11_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_37mm_type_11_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_37mm_type_11_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_37mm_type_11_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_6_5mm_type_38_1907_naval_user_machinegun")]
        public decimal? ShipAuxCaliberReloadTime_6_5mm_type_38_1907_naval_user_machinegun { get; set; }

        [JsonProperty("shipAuxMaxAngle_6_5mm_type_38_1907_naval_user_machinegun")]
        public decimal? ShipAuxMaxAngle_6_5mm_type_38_1907_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_57mm_type_97_1937_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_57mm_type_97_1937_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_57mm_type_97_1937_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_57mm_type_97_1937_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_7_7mm_80_type_92_naval_user_machinegun")]
        public decimal? ShipAuxCaliberReloadTime_7_7mm_80_type_92_naval_user_machinegun { get; set; }

        [JsonProperty("shipAuxMaxAngle_7_7mm_80_type_92_naval_user_machinegun")]
        public decimal? ShipAuxMaxAngle_7_7mm_80_type_92_naval_user_machinegun { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_20mm_type_98_twin_type4_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_20mm_type_98_twin_type4_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_20mm_type_98_twin_type4_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_20mm_type_98_twin_type4_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_13_2mm_Type93_naval_user_machinegun")]
        public decimal? ShipMainCaliberReloadTime_13_2mm_Type93_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_13_2mm_Type93_naval_user_machinegun")]
        public decimal? ShipMainCaliberMaxAngle_13_2mm_Type93_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_75mm_type_88_aa_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_75mm_type_88_aa_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_75mm_type_88_aa_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_75mm_type_88_aa_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_40mm_62_hi_type91_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_40mm_62_hi_type91_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_40mm_62_hi_type91_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_40mm_62_hi_type91_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_20mm_JM61_RFS_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_20mm_JM61_RFS_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_20mm_JM61_RFS_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_20mm_JM61_RFS_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_76mm_50cal_mk33_twin_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_76mm_50cal_mk33_twin_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_76mm_50cal_mk33_twin_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_76mm_50cal_mk33_twin_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_127mm_5_50_type_3_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_127mm_5_50_type_3_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_127mm_5_50_type_3_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_127mm_5_50_type_3_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_13_2mm_Type93_naval_user_machinegun")]
        public decimal? ShipAuxCaliberReloadTime_13_2mm_Type93_naval_user_machinegun { get; set; }

        [JsonProperty("shipAuxMaxAngle_13_2mm_Type93_naval_user_machinegun")]
        public decimal? ShipAuxMaxAngle_13_2mm_Type93_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_100mm_65_type_98_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_100mm_65_type_98_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_100mm_65_type_98_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_100mm_65_type_98_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_120mm_45_type_3_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_120mm_45_type_3_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_120mm_45_type_3_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_120mm_45_type_3_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_140mm_50_3rd_year_type_14_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_140mm_50_3rd_year_type_14_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_140mm_50_3rd_year_type_14_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_140mm_50_3rd_year_type_14_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_7_7mm_80_type_92_naval_user_machinegun")]
        public decimal? ShipAntiAirCaliberReloadTime_7_7mm_80_type_92_naval_user_machinegun { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_7_7mm_80_type_92_naval_user_machinegun")]
        public decimal? ShipAntiAirMaxAngle_7_7mm_80_type_92_naval_user_machinegun { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_200mm_50_3rd_year_type_no1_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_200mm_50_3rd_year_type_no1_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_200mm_50_3rd_year_type_no1_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_200mm_50_3rd_year_type_no1_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_120mm_40_10th_year_type_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_120mm_40_10th_year_type_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_120mm_40_10th_year_type_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_120mm_40_10th_year_type_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirCaliberReloadTime_25mm_Type_96_naval_user_cannon")]
        public decimal? ShipAntiAirCaliberReloadTime_25mm_Type_96_naval_user_cannon { get; set; }

        [JsonProperty("shipAntiAirMaxAngle_25mm_Type_96_naval_user_cannon")]
        public decimal? ShipAntiAirMaxAngle_25mm_Type_96_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_203mm_50_3rd_year_type_no2_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_203mm_50_3rd_year_type_no2_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_203mm_50_3rd_year_type_no2_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_203mm_50_3rd_year_type_no2_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberReloadTime_152mm_50_type_41_naval_user_cannon")]
        public decimal? ShipMainCaliberReloadTime_152mm_50_type_41_naval_user_cannon { get; set; }

        [JsonProperty("shipMainCaliberMaxAngle_152mm_50_type_41_naval_user_cannon")]
        public decimal? ShipMainCaliberMaxAngle_152mm_50_type_41_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxCaliberReloadTime_80mm_60_type_98_naval_user_cannon")]
        public decimal? ShipAuxCaliberReloadTime_80mm_60_type_98_naval_user_cannon { get; set; }

        [JsonProperty("shipAuxMaxAngle_80mm_60_type_98_naval_user_cannon")]
        public decimal? ShipAuxMaxAngle_80mm_60_type_98_naval_user_cannon { get; set; }

        #endregion Ship Cannon Data

        #endregion Weapons

        #endregion NotRequired
    }
}