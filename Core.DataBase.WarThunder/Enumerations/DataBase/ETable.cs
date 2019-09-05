namespace Core.DataBase.WarThunder.Enumerations.DataBase
{
    /// <summary> Tables in the local database. </summary>
    internal class ETable
    {
        private const string _vehicle = Vehicle + "_";

        internal const string Branch = "objBranches";
        internal const string Nation = "objNations";
        internal const string Vehicle = "objVehicles";
        internal const string VehicleAverageAward = _vehicle + "AverageAward";
        internal const string VehicleBattleRating = _vehicle + "BattleRating";
        internal const string VehicleBattleTime = _vehicle + "BattleTime";
        internal const string VehicleBattleTimeAward = _vehicle + "BattleTimeAward";
        internal const string VehicleEconomicRank = _vehicle + "EconomicRank";
        internal const string VehicleNumberOfSpawns = _vehicle + "NumberOfSpawns";
        internal const string VehicleRepairCost = _vehicle + "RepairCost";
        internal const string VehicleRepairTimeWithCrew = _vehicle + "RepairTimeWithCrew";
        internal const string VehicleRepairTimeWithoutCrew = _vehicle + "RepairTimeWithoutCrew";
        internal const string VehicleResearchTreeData = _vehicle + "ResearchTreeData";
        internal const string VehicleRewardMultiplier = _vehicle + "RewardMultiplier";
        internal const string VehicleVisualPremiumRewardMultiplier = _vehicle + "VisualPremiumRewardMultiplier";
        internal const string VehicleVisualRewardMultiplier = _vehicle + "VisualRewardMultiplier";
    }
}