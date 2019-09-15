namespace Core.DataBase.WarThunder.Enumerations.DataBase
{
    /// <summary> Tables in the local database. </summary>
    internal class ETable
    {
        private const string _ = "_";
        private const string _objectPrefix = "obj";

        private const string _vehicles = "Vehicles";
        private const string _vehicleTable = Vehicle + _;
        internal const string Branch = "objBranches";
        internal const string Nation = _objectPrefix + "Nations";
        internal const string Vehicle = _objectPrefix + _vehicles;
        internal const string VehicleAverageAward = _vehicleTable + "AverageAward";
        internal const string VehicleBattleRating = _vehicleTable + "BattleRating";
        internal const string VehicleBattleTime = _vehicleTable + "BattleTime";
        internal const string VehicleBattleTimeAward = _vehicleTable + "BattleTimeAward";
        internal const string VehicleEconomicRank = _vehicleTable + "EconomicRank";
        internal const string VehicleNumberOfSpawns = _vehicleTable + "NumberOfSpawns";
        internal const string VehicleRepairCost = _vehicleTable + "RepairCost";
        internal const string VehicleRepairTimeWithCrew = _vehicleTable + "RepairTimeWithCrew";
        internal const string VehicleRepairTimeWithoutCrew = _vehicleTable + "RepairTimeWithoutCrew";
        internal const string VehicleResearchTreeData = _vehicleTable + "ResearchTreeData";
        internal const string VehicleRewardMultiplier = _vehicleTable + "RewardMultiplier";
        internal const string VehicleVisualPremiumRewardMultiplier = _vehicleTable + "VisualPremiumRewardMultiplier";
        internal const string VehicleVisualRewardMultiplier = _vehicleTable + "VisualRewardMultiplier";
    }
}