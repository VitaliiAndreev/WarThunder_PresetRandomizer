namespace Core.DataBase.WarThunder.Enumerations.DataBase
{
    /// <summary> Tables in the local database. </summary>
    internal class ETable
    {
        private const string _ = "_";
        private const string _localizationPrefix = "loc";
        private const string _objectPrefix = "obj";

        private const string _vehicles = "Vehicles";

        private const string _vehicleTable = Vehicle + _;
        private const string _vehicleLocalizationTable = _localizationPrefix + _vehicles + _;

        internal const string Branch = "objBranches";

        internal const string LocalizationVehicleClassName = _vehicleLocalizationTable + "ClassName";
        internal const string LocalizationVehicleFullName = _vehicleLocalizationTable + "FullName";
        internal const string LocalizationVehicleResearchTreeName = _vehicleLocalizationTable + "ResearchTreeName";
        internal const string LocalizationVehicleShortName = _vehicleLocalizationTable + "ShortName";

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
        internal const string VehicleTagSet = _vehicleTable + "TagSet";
        internal const string VehicleVisualPremiumRewardMultiplier = _vehicleTable + "VisualPremiumRewardMultiplier";
        internal const string VehicleVisualRewardMultiplier = _vehicleTable + "VisualRewardMultiplier";
    }
}