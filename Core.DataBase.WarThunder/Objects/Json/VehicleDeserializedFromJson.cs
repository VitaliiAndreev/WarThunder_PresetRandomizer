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
        public int BaseCrewTrainCostInSilver { get; set; } // requires impelementation and testing

        [JsonProperty("train2Cost", Required = Required.Always)]
        public int ExpertCrewTrainCostInSilver { get; set; } // requires impelementation and testing

        [JsonProperty("train3Cost_gold", Required = Required.Always)]
        public int Train3CostGold { get; set; } // requires impelementation and testing

        [JsonProperty("train3Cost_exp", Required = Required.Always)]
        public int Train3CostExp { get; set; } // requires impelementation and testing

        [JsonProperty("crewTotalCount", Required = Required.Always)]
        public int CrewTotalCount { get; set; } // requires impelementation and testing

        [JsonProperty("gunnersCount", Required = Required.Always)]
        public int GunnersCount { get; set; } // requires impelementation and testing

        #endregion Crew
        #region Modifications

        [JsonProperty("needBuyToOpenNextInTier1", Required = Required.Always)]
        public int NeedBuyToOpenNextInTier1 { get; set; } // requires impelementation and testing

        [JsonProperty("needBuyToOpenNextInTier2", Required = Required.Always)]
        public int NeedBuyToOpenNextInTier2 { get; set; } // requires impelementation and testing

        [JsonProperty("needBuyToOpenNextInTier3", Required = Required.Always)]
        public int NeedBuyToOpenNextInTier3 { get; set; } // requires impelementation and testing

        [JsonProperty("needBuyToOpenNextInTier4", Required = Required.Always)]
        public int NeedBuyToOpenNextInTier4 { get; set; } // requires impelementation and testing

        #endregion Modifications
        #region Rank

        [JsonProperty("rank", Required = Required.Always)]
        public int Rank { get; set; } // requires impelementation and testing

        [JsonProperty("economicRankArcade", Required = Required.Always)]
        public int EconomicRankInArcade { get; set; } // requires impelementation and testing

        [JsonProperty("economicRankHistorical", Required = Required.Always)]
        public int EconomicRankInHistorical { get; set; } // requires impelementation and testing

        #endregion Rank
        #region Repairs

        [JsonProperty("repairTimeHrsArcade", Required = Required.Always)]
        public decimal RepairTimeInArcade { get; set; } // requires impelementation and testing

        [JsonProperty("repairTimeHrsHistorical", Required = Required.Always)]
        public decimal RepairTimeInHistorical { get; set; } // requires impelementation and testing

        [JsonProperty("repairTimeHrsSimulation", Required = Required.Always)]
        public decimal RepairTimeInSimulation { get; set; } // requires impelementation and testing

        [JsonProperty("repairTimeHrsNoCrewArcade", Required = Required.Always)]
        public decimal RepairTimeInNoCrewArcade { get; set; } // requires impelementation and testing

        [JsonProperty("repairTimeHrsNoCrewHistorical", Required = Required.Always)]
        public decimal RepairTimeInNoCrewHistorical { get; set; } // requires impelementation and testing

        [JsonProperty("repairTimeHrsNoCrewSimulation", Required = Required.Always)]
        public decimal RepairTimeInNoCrewSimulation { get; set; } // requires impelementation and testing

        [JsonProperty("repairCostArcade", Required = Required.Always)]
        public int RepairCostInArcade { get; set; } // requires impelementation and testing

        [JsonProperty("repairCostHistorical", Required = Required.Always)]
        public int RepairCostInHistorical { get; set; } // requires impelementation and testing

        [JsonProperty("repairCostSimulation", Required = Required.Always)]
        public int RepairCostInSimulation { get; set; } // requires impelementation and testing

        #endregion Repairs
        #region Rewards

        [JsonProperty("battleTimeAwardArcade", Required = Required.Always)]
        public int BattleTimeAwardInArcade { get; set; } // requires impelementation and testing

        [JsonProperty("battleTimeAwardHistorical", Required = Required.Always)]
        public int BattleTimeAwardInHistorical { get; set; } // requires impelementation and testing

        [JsonProperty("battleTimeAwardSimulation", Required = Required.Always)]
        public int BattleTimeAwardInSimulation { get; set; } // requires impelementation and testing

        [JsonProperty("avgAwardArcade", Required = Required.Always)]
        public int AverageAwardinArcade { get; set; } // requires impelementation and testing

        [JsonProperty("avgAwardHistorical", Required = Required.Always)]
        public int AverageAwardinHistorical { get; set; } // requires impelementation and testing

        [JsonProperty("avgAwardSimulation", Required = Required.Always)]
        public int AverageAwardinSimulation { get; set; } // requires impelementation and testing

        [JsonProperty("rewardMulArcade", Required = Required.Always)]
        public decimal RewardMultiplierInArcade { get; set; } // requires impelementation and testing

        [JsonProperty("rewardMulHistorical", Required = Required.Always)]
        public decimal RewardMultiplierInHistorical { get; set; } // requires impelementation and testing

        [JsonProperty("rewardMulSimulation", Required = Required.Always)]
        public decimal RewardMultiplierInSimulation { get; set; } // requires impelementation and testing

        [JsonProperty("rewardMulVisualArcade", Required = Required.Always)]
        public decimal VisualRewardMultiplierInArcade { get; set; } // requires impelementation and testing

        [JsonProperty("rewardMulVisualHistorical", Required = Required.Always)]
        public decimal VisualRewardMultiplierInHistorical { get; set; } // requires impelementation and testing

        [JsonProperty("rewardMulVisualSimulation", Required = Required.Always)]
        public decimal VisualRewardMultiplierInSimulation { get; set; } // requires impelementation and testing

        [JsonProperty("expMul", Required = Required.Always)]
        public decimal ExpMul { get; set; } // requires impelementation and testing

        [JsonProperty("groundKillMul", Required = Required.Always)]
        public decimal GroundKillMul { get; set; } // requires impelementation and testing

        [JsonProperty("battleTimeArcade", Required = Required.Always)]
        public decimal BattleTimeArcade { get; set; } // requires impelementation and testing

        [JsonProperty("battleTimeHistorical", Required = Required.Always)]
        public decimal BattleTimeHistorical { get; set; } // requires impelementation and testing

        [JsonProperty("battleTimeSimulation", Required = Required.Always)]
        public decimal BattleTimeSimulation { get; set; } // requires impelementation and testing

        #endregion Rewards

        #endregion Required
        #region NotRequired

        [JsonProperty("reqExp")]
        public int? UnlockCostInResearch { get; set; } // requires impelementation and testing

        [JsonProperty("costGold")]
        public int? CostGold { get; set; } // requires impelementation and testing

        [JsonProperty("showOnlyWhenBought")]
        public bool? ShowOnlyWhenBought { get; set; } // requires impelementation and testing

        [JsonProperty("freeRepairs")]
        public int? FreeRepairs { get; set; } // requires impelementation and testing

        [JsonProperty("reqAir")]
        public string VehicleRequired { get; set; } // requires impelementation and testing

        [JsonProperty("spawnType")]
        public string SpawnType { get; set; } // requires impelementation and testing

        [JsonProperty("numSpawnsPerBattleSimulation")]
        public int? NumberOfSpawnsInSimulation { get; set; }

        #region Graphics

        [JsonProperty("commonWeaponImage")]
        public string CommonWeaponImage { get; set; } // requires impelementation and testing

        [JsonProperty("bulletsIconParam")]
        public int? BulletsIconParam { get; set; } // requires impelementation and testing

        [JsonProperty("customClassIco")]
        public string CustomClassIco { get; set; } // requires impelementation and testing

        [JsonProperty("customImage")]
        public string CustomImage { get; set; } // requires impelementation and testing

        [JsonProperty("customTooltipImage")]
        public string CustomTooltipImage { get; set; } // requires impelementation and testing

        #endregion Graphics
        #region Modifications

        [JsonProperty("weaponUpgrade1")]
        public string WeaponUpgrade1 { get; set; } // requires impelementation and testing

        [JsonProperty("weaponUpgrade2")]
        public string WeaponUpgrade2 { get; set; } // requires impelementation and testing

        [JsonProperty("weaponUpgrade3")]
        public string WeaponUpgrade3 { get; set; } // requires impelementation and testing

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