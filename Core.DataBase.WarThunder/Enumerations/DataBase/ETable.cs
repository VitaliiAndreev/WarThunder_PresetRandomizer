namespace Core.DataBase.WarThunder.Enumerations.DataBase
{
    /// <summary> Tables in the local database. </summary>
    internal class ETable
    {
        private const string _ = "_";
        private const string _s = "s";
        private const string _es = "e" + _s;
        private const string _localisationPrefix = "loc";
        private const string _objectPrefix = "obj";

        private const string _aircraft = "Aircraft";
        private const string _battle = "Battle";
        private const string _branch = "Branch";
        private const string _data = "Data";
        private const string _crew = "Crew";
        private const string _gain = "Gain";
        private const string _ground = "Ground";
        private const string _images = "Images";
        private const string _multiplier = "Multiplier";
        private const string _name = "Name";
        private const string _repair = "Repair";
        private const string _research = "Research";
        private const string _reward = "Reward";
        private const string _silver = "Silver";
        private const string _subclass = "Subclass";
        private const string _tags = "Tags";
        private const string _time = "Time";
        private const string _tree = "Tree";
        private const string _vehicle = "Vehicle";
        private const string _vehicles = _vehicle + _s;
        private const string _visual = "Visual";

        private const string _vehicleTable = Vehicle + _;
        private const string _vehicleLocalisationTable = _localisationPrefix + _vehicles + _;

        internal const string AircraftTags = _objectPrefix + _aircraft + _tags;

        internal const string Branch = _objectPrefix + _branch + _es;

        internal const string GroundVehicleTags = _objectPrefix + _ground + _vehicle + _tags;

        internal const string LocalisationVehicleClassName = _vehicleLocalisationTable + "Class" + _name;
        internal const string LocalisationVehicleFullName = _vehicleLocalisationTable + "Full" + _name;
        internal const string LocalisationVehicleResearchTreeName = _vehicleLocalisationTable + _research + _tree + _name;
        internal const string LocalisationVehicleShortName = _vehicleLocalisationTable + "Short" + _name;

        internal const string Nation = _objectPrefix + "Nations";

        internal const string Vehicle = _objectPrefix + _vehicles;
        internal const string VehicleAverageReward = _vehicleTable + "Average" + _reward;
        internal const string VehicleBattleRating = _vehicleTable + _battle + "Rating";
        internal const string VehicleBattleTime = _vehicleTable + _battle + _time;
        internal const string VehicleBattleTimeReward = _vehicleTable + _battle + _time + _reward;
        internal const string VehicleCrewData = _vehicleTable + _crew + _data;
        internal const string VehicleEconomicRank = _vehicleTable + "EconomicRank";
        internal const string VehicleEconomyData = _vehicleTable + "Economy" + _data;
        internal const string VehicleGraphicsData = _vehicleTable + "Graphics" + _data;
        internal const string VehicleImages = _vehicleTable + _images;
        internal const string VehicleModificationsData = _vehicleTable + "Modifications" + _data;
        internal const string VehicleNumberOfSpawns = _vehicleTable + "NumberOfSpawns";
        internal const string VehiclePerformanceData = _vehicleTable + "Performance" + _data;
        internal const string VehicleRepairCost = _vehicleTable + _repair + "Cost";
        internal const string VehicleRepairTimeWithCrew = _vehicleTable + _repair + _time + "With" + _crew;
        internal const string VehicleRepairTimeWithoutCrew = _vehicleTable + _repair + _time + "Without" + _crew;
        internal const string VehicleResearchTreeData = _vehicleTable + _research + _tree + _data;
        internal const string VehicleSilverGainMultiplier = _vehicleTable + _silver + _gain + _multiplier;
        internal const string VehicleSubclass = _vehicleTable + _subclass;
        internal const string VehicleVisualPremiumRewardMultiplier = _vehicleTable + _visual + "Premium" + _reward + _multiplier;
        internal const string VehicleVisualRewardMultiplier = _vehicleTable + _visual + _reward + _multiplier;
        internal const string VehicleWeaponsData = _vehicleTable + "Weapons" + _data;
    }
}