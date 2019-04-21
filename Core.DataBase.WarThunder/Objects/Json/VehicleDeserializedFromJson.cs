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
        public string Country { get; set; } // requires impelementation and testing

        [JsonProperty("unitMoveType", Required = Required.DisallowNull)]
        public string UnitMoveType { get; set; } // requires impelementation and testing

        [JsonProperty("unitClass", Required = Required.DisallowNull)]
        public string UnitClass { get; set; } // requires impelementation and testing

        [JsonProperty("spawnType", Required = Required.DisallowNull)]
        public string SpawnType { get; set; } // requires impelementation and testing

        [JsonProperty("value", Required = Required.DisallowNull)]
        public int PurchaseCostInSilver { get; set; } // requires impelementation and testing

        [JsonProperty("numSpawnsPerBattleSimulation", Required = Required.DisallowNull)]
        public int NumSpawnsPerBattleSimulation { get; set; } // requires impelementation and testing

        #region Crew

        [JsonProperty("trainCost", Required = Required.DisallowNull)]
        public int BaseCrewTrainCostInSilver { get; set; } // requires impelementation and testing

        [JsonProperty("train2Cost", Required = Required.DisallowNull)]
        public int ExpertCrewTrainCostInSilver { get; set; } // requires impelementation and testing

        [JsonProperty("train3Cost_gold", Required = Required.DisallowNull)]
        public int Train3CostGold { get; set; } // requires impelementation and testing

        [JsonProperty("train3Cost_exp", Required = Required.DisallowNull)]
        public int Train3CostExp { get; set; } // requires impelementation and testing

        [JsonProperty("crewTotalCount", Required = Required.DisallowNull)]
        public int CrewTotalCount { get; set; } // requires impelementation and testing

        [JsonProperty("gunnersCount", Required = Required.DisallowNull)]
        public int GunnersCount { get; set; } // requires impelementation and testing

        #endregion Crew
        #region Graphics

        [JsonProperty("commonWeaponImage", Required = Required.DisallowNull)]
        public string CommonWeaponImage { get; set; } // requires impelementation and testing

        #endregion Graphics
        #region Modifications

        [JsonProperty("needBuyToOpenNextInTier1", Required = Required.DisallowNull)]
        public int NeedBuyToOpenNextInTier1 { get; set; } // requires impelementation and testing

        [JsonProperty("needBuyToOpenNextInTier2", Required = Required.DisallowNull)]
        public int NeedBuyToOpenNextInTier2 { get; set; } // requires impelementation and testing

        [JsonProperty("needBuyToOpenNextInTier3", Required = Required.DisallowNull)]
        public int NeedBuyToOpenNextInTier3 { get; set; } // requires impelementation and testing

        [JsonProperty("needBuyToOpenNextInTier4", Required = Required.DisallowNull)]
        public int NeedBuyToOpenNextInTier4 { get; set; } // requires impelementation and testing

        [JsonProperty("weaponUpgrade1", Required = Required.DisallowNull)]
        public string WeaponUpgrade1 { get; set; } // requires impelementation and testing

        [JsonProperty("weaponUpgrade2", Required = Required.DisallowNull)]
        public string WeaponUpgrade2 { get; set; } // requires impelementation and testing

        [JsonProperty("weaponUpgrade3", Required = Required.DisallowNull)]
        public string WeaponUpgrade3 { get; set; } // requires impelementation and testing

        #endregion Modifications
        #region Performance

        [JsonProperty("speed", Required = Required.DisallowNull)]
        public decimal Speed { get; set; } // requires impelementation and testing

        #endregion Performance
        #region Rank

        [JsonProperty("rank", Required = Required.DisallowNull)]
        public int Rank { get; set; } // requires impelementation and testing

        [JsonProperty("economicRankArcade", Required = Required.DisallowNull)]
        public int EconomicRankInArcade { get; set; } // requires impelementation and testing

        [JsonProperty("economicRankHistorical", Required = Required.DisallowNull)]
        public int EconomicRankInHistorical { get; set; } // requires impelementation and testing

        [JsonProperty("economicRankSimulation", Required = Required.DisallowNull)]
        public int EconomicRankInSimulation { get; set; } // requires impelementation and testing

        #endregion Rank
        #region Repairs

        [JsonProperty("repairTimeHrsArcade", Required = Required.DisallowNull)]
        public decimal RepairTimeInArcade { get; set; } // requires impelementation and testing

        [JsonProperty("repairTimeHrsHistorical", Required = Required.DisallowNull)]
        public decimal RepairTimeInHistorical { get; set; } // requires impelementation and testing

        [JsonProperty("repairTimeHrsSimulation", Required = Required.DisallowNull)]
        public decimal RepairTimeInSimulation { get; set; } // requires impelementation and testing

        [JsonProperty("repairTimeHrsNoCrewArcade", Required = Required.DisallowNull)]
        public decimal RepairTimeInNoCrewArcade { get; set; } // requires impelementation and testing

        [JsonProperty("repairTimeHrsNoCrewHistorical", Required = Required.DisallowNull)]
        public decimal RepairTimeInNoCrewHistorical { get; set; } // requires impelementation and testing

        [JsonProperty("repairTimeHrsNoCrewSimulation", Required = Required.DisallowNull)]
        public decimal RepairTimeInNoCrewSimulation { get; set; } // requires impelementation and testing

        [JsonProperty("repairCostArcade", Required = Required.DisallowNull)]
        public int RepairCostInArcade { get; set; } // requires impelementation and testing

        [JsonProperty("repairCostHistorical", Required = Required.DisallowNull)]
        public int RepairCostInHistorical { get; set; } // requires impelementation and testing

        [JsonProperty("repairCostSimulation", Required = Required.DisallowNull)]
        public int RepairCostInSimulation { get; set; } // requires impelementation and testing

        #endregion Repairs
        #region Rewards

        [JsonProperty("battleTimeAwardArcade", Required = Required.DisallowNull)]
        public int BattleTimeAwardInArcade { get; set; } // requires impelementation and testing

        [JsonProperty("battleTimeAwardHistorical", Required = Required.DisallowNull)]
        public int BattleTimeAwardInHistorical { get; set; } // requires impelementation and testing

        [JsonProperty("battleTimeAwardSimulation", Required = Required.DisallowNull)]
        public int BattleTimeAwardInSimulation { get; set; } // requires impelementation and testing

        [JsonProperty("avgAwardArcade", Required = Required.DisallowNull)]
        public int AverageAwardinArcade { get; set; } // requires impelementation and testing

        [JsonProperty("avgAwardHistorical", Required = Required.DisallowNull)]
        public int AverageAwardinHistorical { get; set; } // requires impelementation and testing

        [JsonProperty("avgAwardSimulation", Required = Required.DisallowNull)]
        public int AverageAwardinSimulation { get; set; } // requires impelementation and testing

        [JsonProperty("rewardMulArcade", Required = Required.DisallowNull)]
        public decimal RewardMultiplierInArcade { get; set; } // requires impelementation and testing

        [JsonProperty("rewardMulHistorical", Required = Required.DisallowNull)]
        public decimal RewardMultiplierInHistorical { get; set; } // requires impelementation and testing

        [JsonProperty("rewardMulSimulation", Required = Required.DisallowNull)]
        public decimal RewardMultiplierInSimulation { get; set; } // requires impelementation and testing

        [JsonProperty("rewardMulVisualArcade", Required = Required.DisallowNull)]
        public decimal VisualRewardMultiplierInArcade { get; set; } // requires impelementation and testing

        [JsonProperty("rewardMulVisualHistorical", Required = Required.DisallowNull)]
        public decimal VisualRewardMultiplierInHistorical { get; set; } // requires impelementation and testing

        [JsonProperty("rewardMulVisualSimulation", Required = Required.DisallowNull)]
        public decimal VisualRewardMultiplierInSimulation { get; set; } // requires impelementation and testing

        [JsonProperty("expMul", Required = Required.DisallowNull)]
        public decimal ExpMul { get; set; } // requires impelementation and testing

        [JsonProperty("groundKillMul", Required = Required.DisallowNull)]
        public decimal GroundKillMul { get; set; } // requires impelementation and testing

        [JsonProperty("battleTimeArcade", Required = Required.DisallowNull)]
        public decimal BattleTimeArcade { get; set; } // requires impelementation and testing

        [JsonProperty("battleTimeHistorical", Required = Required.DisallowNull)]
        public decimal BattleTimeHistorical { get; set; } // requires impelementation and testing

        [JsonProperty("battleTimeSimulation", Required = Required.DisallowNull)]
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

        #region Graphics

        [JsonProperty("bulletsIconParam")]
        public int? BulletsIconParam { get; set; } // requires impelementation and testing

        [JsonProperty("customClassIco")]
        public string CustomClassIco { get; set; } // requires impelementation and testing

        [JsonProperty("customImage")]
        public string CustomImage { get; set; } // requires impelementation and testing

        [JsonProperty("customTooltipImage")]
        public string CustomTooltipImage { get; set; } // requires impelementation and testing

        #endregion Graphics
        #region Performance

        [JsonProperty("turretSpeed")]
        public List<decimal> TurretSpeed { get; set; } // requires impelementation and testing

        [JsonProperty("reloadTime_cannon")]
        public decimal? ReloadTimeCannon { get; set; } // requires impelementation and testing

        [JsonProperty("primaryWeaponAutoLoader")]
        public bool? PrimaryWeaponAutoLoader { get; set; } // requires impelementation and testing

        [JsonProperty("maxDeltaAngle_rockets")]
        public decimal? MaxDeltaAngleRockets { get; set; } // requires impelementation and testing

        #endregion Performance
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