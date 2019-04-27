using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Json
{
    /// <summary>
    /// A mapping entity used for automated deserialization of JSON data before passing it on into persistent objects.
    /// <para> Partially generated with QuickType (https://app.quicktype.io). </para>
    /// </summary>
    public class VehicleDeserializedFromJson : DeserializedFromJson
    {
        #region Required

        [JsonProperty("country", Required = Required.Always)]
        public string Nation { get; set; }

        [JsonProperty("unitMoveType", Required = Required.Always)]
        public string MoveType { get; set; }

        [JsonProperty("unitClass", Required = Required.Always)]
        public string Class { get; set; }

        [JsonProperty("value", Required = Required.Always)]
        public int PurchaseCostInSilver { get; set; }

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
        #region Modifications

        [JsonProperty("needBuyToOpenNextInTier1", Required = Required.Always)]
        public int AmountOfModificationsResearchedIn_Tier0_RequiredToUnlock_Tier1 { get; set; }

        [JsonProperty("needBuyToOpenNextInTier2", Required = Required.Always)]
        public int AmountOfModificationsResearchedIn_Tier1_RequiredToUnlock_Tier2 { get; set; }

        [JsonProperty("needBuyToOpenNextInTier3", Required = Required.Always)]
        public int AmountOfModificationsResearchedIn_Tier2_RequiredToUnlock_Tier3 { get; set; }

        [JsonProperty("needBuyToOpenNextInTier4", Required = Required.Always)]
        public int AmountOfModificationsResearchedIn_Tier3_RequiredToUnlock_Tier4 { get; set; }

        #endregion Modifications
        #region Rank

        [JsonProperty("rank", Required = Required.Always)]
        public int Rank { get; set; }

        [JsonProperty("economicRankArcade", Required = Required.Always)]
        public int EconomicRankInArcade { get; set; }

        [JsonProperty("economicRankHistorical", Required = Required.Always)]
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

        #endregion Required
        #region NotRequired

        [JsonProperty("showOnlyWhenBought")]
        public bool? ShowOnlyWhenBought { get; set; }

        [JsonProperty("reqExp")]
        public int? UnlockCostInResearch { get; set; }

        [JsonProperty("costGold")]
        public int? PurchaseCostInGold { get; set; }

        [JsonProperty("reqAir")]
        public string VehicleRequired { get; set; }

        [JsonProperty("spawnType")]
        public string SpawnType { get; set; }

        [JsonProperty("numSpawnsPerBattleSimulation")]
        public int? NumberOfSpawnsInSimulation { get; set; }

        #region Graphics

        [JsonProperty("customClassIco")]
        public string CustomClassIco { get; set; }

        [JsonProperty("customImage")]
        public string CustomImage { get; set; }

        [JsonProperty("customTooltipImage")]
        public string CustomTooltipImage { get; set; }

        [JsonProperty("commonWeaponImage")]
        public string CommonWeaponImage { get; set; }

        [JsonProperty("bulletsIconParam")]
        public int? BulletsIconParam { get; set; }

        #endregion Graphics
        #region Modifications

        [JsonProperty("weaponUpgrade1")]
        public string WeaponUpgrade1 { get; set; }

        [JsonProperty("weaponUpgrade2")]
        public string WeaponUpgrade2 { get; set; }

        [JsonProperty("weaponUpgrade3")]
        public string WeaponUpgrade3 { get; set; }

        #endregion Modifications
        #region Performance

        [JsonProperty("speed")]
        public decimal? Speed { get; set; } // requires impelementation and testing

        [JsonProperty("turretSpeed")]
        public List<decimal> TurretSpeed { get; set; } // requires impelementation and testing

        [JsonProperty("reloadTime_cannon")]
        public decimal? ReloadTimeCannon { get; set; } // requires impelementation and testing

        [JsonProperty("primaryWeaponAutoLoader")]
        public bool? PrimaryWeaponAutoLoader { get; set; } // requires impelementation and testing

        [JsonProperty("maxDeltaAngle_rockets")]
        public decimal? MaxDeltaAngleRockets { get; set; } // requires impelementation and testing

        #endregion Performance
        #region Rank

        [JsonProperty("economicRankSimulation")]
        public int? EconomicRankInSimulation { get; set; } // requires impelementation and testing

        #endregion Rank
        #region Repairs

        [JsonProperty("freeRepairs")]
        public int? FreeRepairs { get; set; }

        #endregion Repairs
        #region Rewards

        [JsonProperty("premRewardMulVisualArcade")]
        public decimal? PremRewardMulVisualArcade { get; set; } // requires impelementation and testing

        [JsonProperty("premRewardMulVisualHistorical")]
        public decimal? PremRewardMulVisualHistorical { get; set; } // requires impelementation and testing

        [JsonProperty("premRewardMulVisualSimulation")]
        public decimal? PremRewardMulVisualSimulation { get; set; } // requires impelementation and testing

        #endregion Rewards

        #endregion NotRequired

        /*[JsonProperty("weapons")]
        public Weapons Weapons { get; set; } // requires impelementation and testing

        [JsonProperty("modifications")]
        public Modifications Modifications { get; set; } // requires impelementation and testing

        [JsonProperty("spare")]
        public Spare Spare { get; set; } // requires impelementation and testing*/

        /*[JsonProperty("gift")]
        public Gift? Gift { get; set; } // requires impelementation and testing*/
    }
}