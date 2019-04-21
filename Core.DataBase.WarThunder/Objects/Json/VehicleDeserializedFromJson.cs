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

        [JsonProperty("country", Required = Required.DisallowNull)]
        public string Country { get; set; }

        [JsonProperty("unitMoveType", Required = Required.DisallowNull)]
        public string UnitMoveType { get; set; }

        [JsonProperty("unitClass", Required = Required.DisallowNull)]
        public string UnitClass { get; set; }

        [JsonProperty("spawnType", Required = Required.DisallowNull)]
        public string SpawnType { get; set; }

        [JsonProperty("value", Required = Required.DisallowNull)]
        public long PurchaseCostInSilver { get; set; }

        [JsonProperty("numSpawnsPerBattleSimulation", Required = Required.DisallowNull)]
        public long NumSpawnsPerBattleSimulation { get; set; }

        #region Crew

        [JsonProperty("trainCost", Required = Required.DisallowNull)]
        public long BaseCrewTrainCostInSilver { get; set; }

        [JsonProperty("train2Cost", Required = Required.DisallowNull)]
        public long ExpertCrewTrainCostInSilver { get; set; }

        [JsonProperty("train3Cost_gold", Required = Required.DisallowNull)]
        public long Train3CostGold { get; set; }

        [JsonProperty("train3Cost_exp", Required = Required.DisallowNull)]
        public long Train3CostExp { get; set; }

        [JsonProperty("crewTotalCount", Required = Required.DisallowNull)]
        public int CrewTotalCount { get; set; }

        [JsonProperty("gunnersCount", Required = Required.DisallowNull)]
        public int GunnersCount { get; set; }

        #endregion Crew
        #region Graphics

        [JsonProperty("commonWeaponImage", Required = Required.DisallowNull)]
        public string CommonWeaponImage { get; set; }

        #endregion Graphics
        #region Modifications

        [JsonProperty("needBuyToOpenNextInTier1", Required = Required.DisallowNull)]
        public int NeedBuyToOpenNextInTier1 { get; set; }

        [JsonProperty("needBuyToOpenNextInTier2", Required = Required.DisallowNull)]
        public int NeedBuyToOpenNextInTier2 { get; set; }

        [JsonProperty("needBuyToOpenNextInTier3", Required = Required.DisallowNull)]
        public int NeedBuyToOpenNextInTier3 { get; set; }

        [JsonProperty("needBuyToOpenNextInTier4", Required = Required.DisallowNull)]
        public int NeedBuyToOpenNextInTier4 { get; set; }

        [JsonProperty("weaponUpgrade1", Required = Required.DisallowNull)]
        public string WeaponUpgrade1 { get; set; }

        [JsonProperty("weaponUpgrade2", Required = Required.DisallowNull)]
        public string WeaponUpgrade2 { get; set; }

        [JsonProperty("weaponUpgrade3", Required = Required.DisallowNull)]
        public string WeaponUpgrade3 { get; set; }

        #endregion Modifications
        #region Performance

        [JsonProperty("speed", Required = Required.DisallowNull)]
        public decimal Speed { get; set; }

        #endregion Performance
        #region Rank

        [JsonProperty("rank", Required = Required.DisallowNull)]
        public int Rank { get; set; }

        [JsonProperty("economicRankArcade", Required = Required.DisallowNull)]
        public int EconomicRankInArcade { get; set; }

        [JsonProperty("economicRankHistorical", Required = Required.DisallowNull)]
        public int EconomicRankInHistorical { get; set; }

        [JsonProperty("economicRankSimulation", Required = Required.DisallowNull)]
        public int EconomicRankInSimulation { get; set; }

        #endregion Rank
        #region Repairs

        [JsonProperty("repairTimeHrsArcade", Required = Required.DisallowNull)]
        public decimal RepairTimeInArcade { get; set; }

        [JsonProperty("repairTimeHrsHistorical", Required = Required.DisallowNull)]
        public decimal RepairTimeInHistorical { get; set; }

        [JsonProperty("repairTimeHrsSimulation", Required = Required.DisallowNull)]
        public decimal RepairTimeInSimulation { get; set; }

        [JsonProperty("repairTimeHrsNoCrewArcade", Required = Required.DisallowNull)]
        public decimal RepairTimeInNoCrewArcade { get; set; }

        [JsonProperty("repairTimeHrsNoCrewHistorical", Required = Required.DisallowNull)]
        public decimal RepairTimeInNoCrewHistorical { get; set; }

        [JsonProperty("repairTimeHrsNoCrewSimulation", Required = Required.DisallowNull)]
        public decimal RepairTimeInNoCrewSimulation { get; set; }

        [JsonProperty("repairCostArcade", Required = Required.DisallowNull)]
        public int RepairCostInArcade { get; set; }

        [JsonProperty("repairCostHistorical", Required = Required.DisallowNull)]
        public int RepairCostInHistorical { get; set; }

        [JsonProperty("repairCostSimulation", Required = Required.DisallowNull)]
        public int RepairCostInSimulation { get; set; }

        #endregion Repairs
        #region Rewards

        [JsonProperty("battleTimeAwardArcade", Required = Required.DisallowNull)]
        public int BattleTimeAwardInArcade { get; set; }

        [JsonProperty("battleTimeAwardHistorical", Required = Required.DisallowNull)]
        public int BattleTimeAwardInHistorical { get; set; }

        [JsonProperty("battleTimeAwardSimulation", Required = Required.DisallowNull)]
        public int BattleTimeAwardInSimulation { get; set; }

        [JsonProperty("avgAwardArcade", Required = Required.DisallowNull)]
        public int AverageAwardinArcade { get; set; }

        [JsonProperty("avgAwardHistorical", Required = Required.DisallowNull)]
        public int AverageAwardinHistorical { get; set; }

        [JsonProperty("avgAwardSimulation", Required = Required.DisallowNull)]
        public int AverageAwardinSimulation { get; set; }

        [JsonProperty("rewardMulArcade", Required = Required.DisallowNull)]
        public decimal RewardMultiplierInArcade { get; set; }

        [JsonProperty("rewardMulHistorical", Required = Required.DisallowNull)]
        public decimal RewardMultiplierInHistorical { get; set; }

        [JsonProperty("rewardMulSimulation", Required = Required.DisallowNull)]
        public decimal RewardMultiplierInSimulation { get; set; }

        [JsonProperty("rewardMulVisualArcade", Required = Required.DisallowNull)]
        public decimal VisualRewardMultiplierInArcade { get; set; }

        [JsonProperty("rewardMulVisualHistorical", Required = Required.DisallowNull)]
        public decimal VisualRewardMultiplierInHistorical { get; set; }

        [JsonProperty("rewardMulVisualSimulation", Required = Required.DisallowNull)]
        public decimal VisualRewardMultiplierInSimulation { get; set; }

        [JsonProperty("expMul", Required = Required.DisallowNull)]
        public decimal ExpMul { get; set; }

        [JsonProperty("groundKillMul", Required = Required.DisallowNull)]
        public decimal GroundKillMul { get; set; }

        [JsonProperty("battleTimeArcade", Required = Required.DisallowNull)]
        public decimal BattleTimeArcade { get; set; }

        [JsonProperty("battleTimeHistorical", Required = Required.DisallowNull)]
        public decimal BattleTimeHistorical { get; set; }

        [JsonProperty("battleTimeSimulation", Required = Required.DisallowNull)]
        public decimal BattleTimeSimulation { get; set; }

        #endregion Rewards

        #endregion Required
        #region NotRequired

        [JsonProperty("reqExp")]
        public long? UnlockCostInResearch { get; set; }

        [JsonProperty("costGold")]
        public long? CostGold { get; set; }

        [JsonProperty("showOnlyWhenBought")]
        public bool? ShowOnlyWhenBought { get; set; }

        [JsonProperty("freeRepairs")]
        public long? FreeRepairs { get; set; }

        [JsonProperty("reqAir")]
        public string VehicleRequired { get; set; }

        #region Graphics

        [JsonProperty("bulletsIconParam")]
        public long? BulletsIconParam { get; set; }

        [JsonProperty("customClassIco")]
        public string CustomClassIco { get; set; }

        [JsonProperty("customImage")]
        public string CustomImage { get; set; }

        [JsonProperty("customTooltipImage")]
        public string CustomTooltipImage { get; set; }

        #endregion Graphics
        #region Performance

        [JsonProperty("turretSpeed")]
        public List<decimal> TurretSpeed { get; set; }

        [JsonProperty("reloadTime_cannon")]
        public decimal? ReloadTimeCannon { get; set; }

        [JsonProperty("primaryWeaponAutoLoader")]
        public bool? PrimaryWeaponAutoLoader { get; set; }

        [JsonProperty("maxDeltaAngle_rockets")]
        public decimal? MaxDeltaAngleRockets { get; set; }

        #endregion Performance
        #region Rewards

        [JsonProperty("premRewardMulVisualArcade")]
        public decimal? PremRewardMulVisualArcade { get; set; }

        [JsonProperty("premRewardMulVisualHistorical")]
        public decimal? PremRewardMulVisualHistorical { get; set; }

        [JsonProperty("premRewardMulVisualSimulation")]
        public decimal? PremRewardMulVisualSimulation { get; set; }

        #endregion Rewards

        #endregion NotRequired

        /*[JsonProperty("weapons")]
        public Weapons Weapons { get; set; }

        [JsonProperty("modifications")]
        public Modifications Modifications { get; set; }

        [JsonProperty("spare")]
        public Spare Spare { get; set; }*/

        /*[JsonProperty("gift")]
        public Gift? Gift { get; set; }*/
    }
}