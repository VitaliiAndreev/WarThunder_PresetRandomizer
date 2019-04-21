using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Json
{
    /// <summary>
    /// A mapping entity used for automated deserialization of JSON data before passing it on into persistent objects.
    /// <para> Generated with QuickType (https://app.quicktype.io). </para>
    /// </summary>
    public class VehicleDeserializedFromJson : DeserializedFromJson
    {
        #region Required

        [JsonProperty("value", Required = Required.AllowNull)]
        public long? PurchaseCostInSilver { get; set; }

        #region Crew Costs

        [JsonProperty("trainCost", Required = Required.DisallowNull)]
        public long? BaseCrewTrainCostInSilver { get; set; }

        [JsonProperty("train2Cost", Required = Required.DisallowNull)]
        public long? ExpertCrewTrainCostInSilver { get; set; }

        [JsonProperty("train3Cost_gold", Required = Required.DisallowNull)]
        public long? Train3CostGold { get; set; }

        [JsonProperty("train3Cost_exp", Required = Required.AllowNull)]
        public long? Train3CostExp { get; set; }

        #endregion Crew Costs
        #region Repairs
        #endregion Repairs

        [JsonProperty("repairTimeHrsArcade", Required = Required.DisallowNull)]
        public decimal? RepairTimeInArcade { get; set; }

        [JsonProperty("repairTimeHrsHistorical", Required = Required.DisallowNull)]
        public decimal? RepairTimeInHistorical { get; set; }

        [JsonProperty("repairTimeHrsSimulation", Required = Required.DisallowNull)]
        public decimal? RepairTimeInSimulation { get; set; }

        [JsonProperty("repairTimeHrsNoCrewArcade", Required = Required.DisallowNull)]
        public decimal? RepairTimeInNoCrewArcade { get; set; }

        [JsonProperty("repairTimeHrsNoCrewHistorical", Required = Required.DisallowNull)]
        public decimal? RepairTimeInNoCrewHistorical { get; set; }

        [JsonProperty("repairTimeHrsNoCrewSimulation", Required = Required.DisallowNull)]
        public decimal? RepairTimeInNoCrewSimulation { get; set; }

        [JsonProperty("repairCostArcade", Required = Required.DisallowNull)]
        public long? RepairCostInArcade { get; set; }

        [JsonProperty("repairCostHistorical", Required = Required.DisallowNull)]
        public long? RepairCostInHistorical { get; set; }

        [JsonProperty("repairCostSimulation", Required = Required.DisallowNull)]
        public long? RepairCostInSimulation { get; set; }

        #endregion Required
        #region NotRequired

        [JsonProperty("reqExp")]
        public long? UnlockCostInResearch { get; set; }

        #endregion NotRequired

        [JsonProperty("battleTimeAwardArcade", Required = Required.DisallowNull)]
        public long? BattleTimeAwardInArcade { get; set; }

        [JsonProperty("battleTimeAwardHistorical", Required = Required.DisallowNull)]
        public long? BattleTimeAwardInHistorical { get; set; }

        [JsonProperty("battleTimeAwardSimulation", Required = Required.DisallowNull)]
        public long? BattleTimeAwardInSimulation { get; set; }

        [JsonProperty("avgAwardArcade", Required = Required.DisallowNull)]
        public long? AverageAwardinArcade { get; set; }

        [JsonProperty("avgAwardHistorical", Required = Required.DisallowNull)]
        public long? AverageAwardinHistorical { get; set; }

        [JsonProperty("avgAwardSimulation", Required = Required.DisallowNull)]
        public long? AverageAwardinSimulation { get; set; }

        [JsonProperty("rewardMulArcade", Required = Required.DisallowNull)]
        public decimal? RewardMultiplierInArcade { get; set; }

        [JsonProperty("rewardMulHistorical", Required = Required.DisallowNull)]
        public decimal? RewardMultiplierInHistorical { get; set; }

        [JsonProperty("rewardMulSimulation", Required = Required.DisallowNull)]
        public decimal? RewardMultiplierInSimulation { get; set; }

        [JsonProperty("rewardMulVisualArcade", Required = Required.DisallowNull)]
        public decimal? VisualRewardMultiplierInArcade { get; set; }

        [JsonProperty("rewardMulVisualHistorical", Required = Required.DisallowNull)]
        public decimal? VisualRewardMultiplierInHistorical { get; set; }

        [JsonProperty("rewardMulVisualSimulation", Required = Required.DisallowNull)]
        public decimal? VisualRewardMultiplierInSimulation { get; set; }

        [JsonProperty("expMul", Required = Required.DisallowNull)]
        public decimal? ExpMul { get; set; }

        [JsonProperty("groundKillMul", Required = Required.AllowNull)]
        public decimal? GroundKillMul { get; set; }

        [JsonProperty("battleTimeArcade", Required = Required.DisallowNull)]
        public decimal? BattleTimeArcade { get; set; }

        [JsonProperty("battleTimeHistorical", Required = Required.DisallowNull)]
        public decimal? BattleTimeHistorical { get; set; }

        [JsonProperty("battleTimeSimulation", Required = Required.DisallowNull)]
        public decimal? BattleTimeSimulation { get; set; }

        [JsonProperty("rank", Required = Required.DisallowNull)]
        public long? Rank { get; set; }

        [JsonProperty("economicRankArcade", Required = Required.DisallowNull)]
        public long? EconomicRankInArcade { get; set; }

        [JsonProperty("economicRankHistorical", Required = Required.DisallowNull)]
        public long? EconomicRankInHistorical { get; set; }

        [JsonProperty("economicRankSimulation", Required = Required.DisallowNull)]
        public long? EconomicRankInSimulation { get; set; }

        /*[JsonProperty("country")]
        public Country? Country { get; set; }*/

        [JsonProperty("gunnersCount", Required = Required.AllowNull)]
        public long? GunnersCount { get; set; }

        /*[JsonProperty("unitClass")]
        public UnitClass? UnitClass { get; set; }

        [JsonProperty("spawnType")]
        public UnitMoveTypeEnum? SpawnType { get; set; }

        [JsonProperty("unitMoveType")]
        public UnitMoveTypeEnum? UnitMoveType { get; set; }*/

        [JsonProperty("speed", Required = Required.DisallowNull)]
        public long? Speed { get; set; }

        [JsonProperty("reqAir")]
        public string VehicleRequired { get; set; }

        /*[JsonProperty("commonWeaponImage")]
        public CommonWeaponImage? CommonWeaponImage { get; set; }*/
        
        [JsonProperty("crewTotalCount", Required = Required.DisallowNull)]
        public long? CrewTotalCount { get; set; }

        [JsonProperty("turretSpeed")]
        public List<decimal> TurretSpeed { get; set; }

        [JsonProperty("reloadTime_cannon")]
        public decimal? ReloadTimeCannon { get; set; }

        [JsonProperty("primaryWeaponAutoLoader")]
        public bool? PrimaryWeaponAutoLoader { get; set; }

        [JsonProperty("numSpawnsPerBattleSimulation", Required = Required.DisallowNull)]
        public long? NumSpawnsPerBattleSimulation { get; set; }

        /*[JsonProperty("weaponUpgrade1")]
        public WeaponUpgrade? WeaponUpgrade1 { get; set; }

        [JsonProperty("weaponUpgrade2")]
        public WeaponUpgrade? WeaponUpgrade2 { get; set; }

        [JsonProperty("weaponUpgrade3")]
        public WeaponUpgrade3? WeaponUpgrade3 { get; set; }*/

        [JsonProperty("needBuyToOpenNextInTier1", Required = Required.DisallowNull)]
        public long? NeedBuyToOpenNextInTier1 { get; set; }

        [JsonProperty("needBuyToOpenNextInTier2", Required = Required.DisallowNull)]
        public long? NeedBuyToOpenNextInTier2 { get; set; }

        [JsonProperty("needBuyToOpenNextInTier3", Required = Required.DisallowNull)]
        public long? NeedBuyToOpenNextInTier3 { get; set; }

        [JsonProperty("needBuyToOpenNextInTier4", Required = Required.DisallowNull)]
        public long? NeedBuyToOpenNextInTier4 { get; set; }

        [JsonProperty("bulletsIconParam")]
        public long? BulletsIconParam { get; set; }

        /*[JsonProperty("weapons")]
        public Weapons Weapons { get; set; }

        [JsonProperty("modifications")]
        public Modifications Modifications { get; set; }

        [JsonProperty("spare")]
        public Spare Spare { get; set; }*/

        [JsonProperty("premRewardMulVisualArcade")]
        public decimal? PremRewardMulVisualArcade { get; set; }

        [JsonProperty("premRewardMulVisualHistorical")]
        public decimal? PremRewardMulVisualHistorical { get; set; }

        [JsonProperty("premRewardMulVisualSimulation")]
        public decimal? PremRewardMulVisualSimulation { get; set; }

        [JsonProperty("costGold")]
        public long? CostGold { get; set; }

        [JsonProperty("freeRepairs")]
        public long? FreeRepairs { get; set; }

        /*[JsonProperty("gift")]
        public Gift? Gift { get; set; }*/

        [JsonProperty("maxDeltaAngle_rockets")]
        public decimal? MaxDeltaAngleRockets { get; set; }

        [JsonProperty("showOnlyWhenBought")]
        public bool? ShowOnlyWhenBought { get; set; }

        [JsonProperty("customClassIco")]
        public string CustomClassIco { get; set; }

        [JsonProperty("customImage")]
        public string CustomImage { get; set; }

        [JsonProperty("customTooltipImage")]
        public string CustomTooltipImage { get; set; }
    }
}