using Core.DataBase.Helpers;
using Core.DataBase.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Localization.Interfaces;
using Core.Extensions;
using Core.Helpers.Logger.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataBase.WarThunder.Helpers
{
    public class DataRepositoryInMemoryWarThunder : DataRepositoryInMemory
    {
        #region Fields

        private readonly IList<INation> _newNations;
        private readonly IList<IBranch> _newBranches;
        private readonly IList<IVehicle> _newVehicles;
        private readonly IList<IVehicleSubclasses> _newVehicleSubclasses;
        private readonly IList<IAircraftTags> _newAircraftTags;
        private readonly IList<IGroundVehicleTags> _newGroundVehicleTags;
        private readonly IList<IVehicleResearchTreeData> _newVehicleResearchTreeData;
        private readonly IList<IVehicleEconomyData> _newVehicleEconomyData;
        private readonly IList<IVehiclePerformanceData> _newVehiclePerformanceData;
        private readonly IList<IVehicleCrewData> _newVehicleCrewData;
        private readonly IList<IVehicleWeaponsData> _newVehicleWeaponsData;
        private readonly IList<IVehicleModificationsData> _newVehicleModificationsData;
        private readonly IList<IVehicleGraphicsData> _newVehicleGraphicsData;
        private readonly IList<IVehicleGameModeParameterSetBase> _newVehicleGameModeParameterSets;
        private readonly IList<ILocalisation> _newLocalizationRecords;
        private readonly IList<IVehicleImages> _newVehicleImages;

        private readonly IList<INation> _nations;
        private readonly IList<IBranch> _branches;
        private readonly IList<IVehicle> _vehicles;
        private readonly IList<IVehicleSubclasses> _vehicleSubclasses;
        private readonly IList<IAircraftTags> _aircraftTags;
        private readonly IList<IGroundVehicleTags> _groundVehicleTags;
        private readonly IList<IVehicleResearchTreeData> _vehicleResearchTreeData;
        private readonly IList<IVehicleEconomyData> _vehicleEconomyData;
        private readonly IList<IVehiclePerformanceData> _vehiclePerformanceData;
        private readonly IList<IVehicleCrewData> _vehicleCrewData;
        private readonly IList<IVehicleWeaponsData> _vehicleWeaponsData;
        private readonly IList<IVehicleModificationsData> _vehicleModificationsData;
        private readonly IList<IVehicleGraphicsData> _vehicleGraphicsData;
        private readonly IList<IVehicleGameModeParameterSetBase> _vehicleGameModeParameterSets;
        private readonly IList<ILocalisation> _localizationRecords;
        private readonly IList<IVehicleImages> _vehicleImages;

        #endregion Fields
        #region Properties

        public override IEnumerable<IPersistentObject> NewObjects
        {
            get
            {
                lock (_lock)
                {
                    var newObjects = new List<IPersistentObject>();

                    newObjects.AddRange(_newNations);
                    newObjects.AddRange(_newBranches);
                    newObjects.AddRange(_newVehicles);
                    newObjects.AddRange(_newVehicleSubclasses);
                    newObjects.AddRange(_newAircraftTags);
                    newObjects.AddRange(_newGroundVehicleTags);
                    newObjects.AddRange(_newVehicleResearchTreeData);
                    newObjects.AddRange(_newVehicleEconomyData);
                    newObjects.AddRange(_newVehiclePerformanceData);
                    newObjects.AddRange(_newVehicleCrewData);
                    newObjects.AddRange(_newVehicleWeaponsData);
                    newObjects.AddRange(_newVehicleModificationsData);
                    newObjects.AddRange(_newVehicleGraphicsData);
                    newObjects.AddRange(_newVehicleGameModeParameterSets);
                    newObjects.AddRange(_newLocalizationRecords);
                    newObjects.AddRange(_newVehicleImages);

                    return newObjects;
                }
            }
        }

        #endregion Properties
        #region Constructors

        public DataRepositoryInMemoryWarThunder(params IConfiguredLogger[] loggers)
            : base(loggers)
        {
            _newNations = new List<INation>();
            _newBranches = new List<IBranch>();
            _newVehicles = new List<IVehicle>();
            _newVehicleSubclasses = new List<IVehicleSubclasses>();
            _newAircraftTags = new List<IAircraftTags>();
            _newGroundVehicleTags = new List<IGroundVehicleTags>();
            _newVehicleResearchTreeData = new List<IVehicleResearchTreeData>();
            _newVehicleEconomyData = new List<IVehicleEconomyData>();
            _newVehiclePerformanceData = new List<IVehiclePerformanceData>();
            _newVehicleCrewData = new List<IVehicleCrewData>();
            _newVehicleWeaponsData = new List<IVehicleWeaponsData>();
            _newVehicleModificationsData = new List<IVehicleModificationsData>();
            _newVehicleGraphicsData = new List<IVehicleGraphicsData>();
            _newVehicleGameModeParameterSets = new List<IVehicleGameModeParameterSetBase>();
            _newLocalizationRecords = new List<ILocalisation>();
            _newVehicleImages = new List<IVehicleImages>();

            _nations = new List<INation>();
            _branches = new List<IBranch>();
            _vehicles = new List<IVehicle>();
            _vehicleSubclasses = new List<IVehicleSubclasses>();
            _aircraftTags = new List<IAircraftTags>();
            _groundVehicleTags = new List<IGroundVehicleTags>();
            _vehicleResearchTreeData = new List<IVehicleResearchTreeData>();
            _vehicleEconomyData = new List<IVehicleEconomyData>();
            _vehiclePerformanceData = new List<IVehiclePerformanceData>();
            _vehicleCrewData = new List<IVehicleCrewData>();
            _vehicleWeaponsData = new List<IVehicleWeaponsData>();
            _vehicleModificationsData = new List<IVehicleModificationsData>();
            _vehicleGraphicsData = new List<IVehicleGraphicsData>();
            _vehicleGameModeParameterSets = new List<IVehicleGameModeParameterSetBase>();
            _localizationRecords = new List<ILocalisation>();
            _vehicleImages = new List<IVehicleImages>();
        }

        #endregion Constructors
        #region Methods: Overrides

        public override IEnumerable<T> GetNewObjects<T>()
        {
            var objectType = typeof(T);

            lock (_lock)
            {
                if (objectType == typeof(INation)) return _newNations.Cast<T>().ToList();
                if (objectType == typeof(IBranch)) return _newBranches.Cast<T>().ToList();
                if (objectType == typeof(IVehicle)) return _newVehicles.Cast<T>().ToList();
                if (objectType == typeof(IVehicleSubclasses)) return _newVehicleSubclasses.Cast<T>().ToList();
                if (objectType == typeof(IAircraftTags)) return _newAircraftTags.Cast<T>().ToList();
                if (objectType == typeof(IGroundVehicleTags)) return _newGroundVehicleTags.Cast<T>().ToList();
                if (objectType == typeof(IVehicleResearchTreeData)) return _newVehicleResearchTreeData.Cast<T>().ToList();
                if (objectType == typeof(IVehicleEconomyData)) return _newVehicleEconomyData.Cast<T>().ToList();
                if (objectType == typeof(IVehiclePerformanceData)) return _newVehiclePerformanceData.Cast<T>().ToList();
                if (objectType == typeof(IVehicleCrewData)) return _newVehicleCrewData.Cast<T>().ToList();
                if (objectType == typeof(IVehicleWeaponsData)) return _newVehicleWeaponsData.Cast<T>().ToList();
                if (objectType == typeof(IVehicleModificationsData)) return _newVehicleModificationsData.Cast<T>().ToList();
                if (objectType == typeof(IVehicleGraphicsData)) return _newVehicleGraphicsData.Cast<T>().ToList();
                if (objectType == typeof(IVehicleGameModeParameterSetBase)) return _newVehicleGameModeParameterSets.Cast<T>().ToList();
                if (objectType == typeof(ILocalisation)) return _newLocalizationRecords.Cast<T>().ToList();
                if (objectType == typeof(IVehicleImages)) return _newVehicleImages.Cast<T>().ToList();
            }
            return new List<T>();
        }

        public override void AddToNewObjects<T>(T newObject)
        {
            lock (_lock)
            {
                if (newObject is INation newNation) _newNations.AddIfNotPresent(newNation);
                else if (newObject is IBranch newBranch) _newBranches.AddIfNotPresent(newBranch);
                else if (newObject is IVehicle newVehicle) _newVehicles.AddIfNotPresent(newVehicle);
                else if (newObject is IVehicleSubclasses newVehicleSubclass) _newVehicleSubclasses.AddIfNotPresent(newVehicleSubclass);
                else if (newObject is IAircraftTags newAircraftTagSet) _newAircraftTags.AddIfNotPresent(newAircraftTagSet);
                else if (newObject is IGroundVehicleTags newGroundVehicleTagSet) _newGroundVehicleTags.AddIfNotPresent(newGroundVehicleTagSet);
                else if (newObject is IVehicleResearchTreeData newVehicleResearchTreeData) _newVehicleResearchTreeData.AddIfNotPresent(newVehicleResearchTreeData);
                else if (newObject is IVehicleEconomyData newVehicleEconomyData) _newVehicleEconomyData.AddIfNotPresent(newVehicleEconomyData);
                else if (newObject is IVehiclePerformanceData newVehiclePerformanceData) _newVehiclePerformanceData.AddIfNotPresent(newVehiclePerformanceData);
                else if (newObject is IVehicleCrewData newVehicleCrewData) _newVehicleCrewData.AddIfNotPresent(newVehicleCrewData);
                else if (newObject is IVehicleWeaponsData newVehicleWeaponsData) _newVehicleWeaponsData.AddIfNotPresent(newVehicleWeaponsData);
                else if (newObject is IVehicleModificationsData newVehicleModificationsData) _newVehicleModificationsData.AddIfNotPresent(newVehicleModificationsData);
                else if (newObject is IVehicleGraphicsData newVehicleGraphicsData) _newVehicleGraphicsData.AddIfNotPresent(newVehicleGraphicsData);
                else if (newObject is IVehicleGameModeParameterSetBase newVehicleGameModeParameterSet) _newVehicleGameModeParameterSets.AddIfNotPresent(newVehicleGameModeParameterSet);
                else if (newObject is ILocalisation newLocalizationSet) _newLocalizationRecords.AddIfNotPresent(newLocalizationSet);
                else if (newObject is IVehicleImages newVehicleImageSet) _newVehicleImages.AddIfNotPresent(newVehicleImageSet);
            }
        }

        protected override IEnumerable<T> GetObjects<T>()
        {
            var objectType = typeof(T);

            if (objectType == typeof(INation)) return _nations.Cast<T>();
            if (objectType == typeof(IBranch)) return _branches.Cast<T>();
            if (objectType == typeof(IVehicle)) return _vehicles.Cast<T>();
            if (objectType == typeof(IVehicleSubclasses)) return _vehicleSubclasses.Cast<T>();
            if (objectType == typeof(IAircraftTags)) return _aircraftTags.Cast<T>();
            if (objectType == typeof(IGroundVehicleTags)) return _groundVehicleTags.Cast<T>();
            if (objectType == typeof(IVehicleResearchTreeData)) return _vehicleResearchTreeData.Cast<T>();
            if (objectType == typeof(IVehicleEconomyData)) return _vehicleEconomyData.Cast<T>();
            if (objectType == typeof(IVehiclePerformanceData)) return _vehiclePerformanceData.Cast<T>();
            if (objectType == typeof(IVehicleCrewData)) return _vehicleCrewData.Cast<T>();
            if (objectType == typeof(IVehicleWeaponsData)) return _vehicleWeaponsData.Cast<T>();
            if (objectType == typeof(IVehicleModificationsData)) return _vehicleModificationsData.Cast<T>();
            if (objectType == typeof(IVehicleGraphicsData)) return _vehicleGraphicsData.Cast<T>();
            if (objectType == typeof(IVehicleGameModeParameterSetBase)) return _vehicleGameModeParameterSets.Cast<T>();
            if (objectType == typeof(ILocalisation)) return _localizationRecords.Cast<T>();
            if (objectType == typeof(IVehicleImages)) return _vehicleImages.Cast<T>();

            return new List<T>();
        }

        public override void PersistNewObjects()
        {
            lock (_lock)
            {
                _nations.AddRange(_newNations);
                _branches.AddRange(_newBranches);
                _vehicles.AddRange(_newVehicles);
                _vehicleSubclasses.AddRange(_newVehicleSubclasses);
                _aircraftTags.AddRange(_newAircraftTags);
                _groundVehicleTags.AddRange(_newGroundVehicleTags);
                _vehicleResearchTreeData.AddRange(_newVehicleResearchTreeData);
                _vehicleEconomyData.AddRange(_newVehicleEconomyData);
                _vehiclePerformanceData.AddRange(_newVehiclePerformanceData);
                _vehicleCrewData.AddRange(_newVehicleCrewData);
                _vehicleWeaponsData.AddRange(_newVehicleWeaponsData);
                _vehicleModificationsData.AddRange(_newVehicleModificationsData);
                _vehicleGraphicsData.AddRange(_newVehicleGraphicsData);
                _vehicleGameModeParameterSets.AddRange(_newVehicleGameModeParameterSets);
                _localizationRecords.AddRange(_newLocalizationRecords);
                _vehicleImages.AddRange(_newVehicleImages);
            }
            ClearNewObjects();
        }

        public override void RemoveFromNewObjects(IPersistentObject @object)
        {
            lock (_lock)
            {
                if (@object is INation nation) _newNations.RemoveSafely(nation);
                else if (@object is IBranch branch) _newBranches.RemoveSafely(branch);
                else if (@object is IVehicle vehicle) _newVehicles.RemoveSafely(vehicle);
                else if (@object is IVehicleSubclasses vehicleSubclass) _newVehicleSubclasses.RemoveSafely(vehicleSubclass);
                else if (@object is IAircraftTags aircraftTagSet) _newAircraftTags.RemoveSafely(aircraftTagSet);
                else if (@object is IGroundVehicleTags groundVehicleTagSet) _newGroundVehicleTags.RemoveSafely(groundVehicleTagSet);
                else if (@object is IVehicleResearchTreeData vehicleResearchTreeData) _newVehicleResearchTreeData.RemoveSafely(vehicleResearchTreeData);
                else if (@object is IVehicleEconomyData vehicleEconomyData) _newVehicleEconomyData.RemoveSafely(vehicleEconomyData);
                else if (@object is IVehiclePerformanceData vehiclePerformanceData) _newVehiclePerformanceData.RemoveSafely(vehiclePerformanceData);
                else if (@object is IVehicleCrewData vehicleCrewData) _newVehicleCrewData.RemoveSafely(vehicleCrewData);
                else if (@object is IVehicleWeaponsData vehicleWeaponsData) _newVehicleWeaponsData.RemoveSafely(vehicleWeaponsData);
                else if (@object is IVehicleModificationsData vehicleModificationsData) _newVehicleModificationsData.RemoveSafely(vehicleModificationsData);
                else if (@object is IVehicleGraphicsData vehicleGraphicsData) _newVehicleGraphicsData.RemoveSafely(vehicleGraphicsData);
                else if (@object is IVehicleGameModeParameterSetBase vehicleGameModeParameterSet) _newVehicleGameModeParameterSets.RemoveSafely(vehicleGameModeParameterSet);
                else if (@object is ILocalisation localizationSet) _newLocalizationRecords.RemoveSafely(localizationSet);
                else if (@object is IVehicleImages vehicleImageSet) _newVehicleImages.RemoveSafely(vehicleImageSet);
            }
        }

        public override void ClearNewObjects()
        {
            _newNations.Clear();
            _newBranches.Clear();
            _newVehicles.Clear();
            _newVehicleSubclasses.Clear();
            _newAircraftTags.Clear();
            _newGroundVehicleTags.Clear();
            _newVehicleResearchTreeData.Clear();
            _newVehicleEconomyData.Clear();
            _newVehiclePerformanceData.Clear();
            _newVehicleCrewData.Clear();
            _newVehicleWeaponsData.Clear();
            _newVehicleModificationsData.Clear();
            _newVehicleGraphicsData.Clear();
            _newVehicleGameModeParameterSets.Clear();
            _newLocalizationRecords.Clear();
            _newVehicleImages.Clear();
        }

        #endregion Methods: Overrides
    }
}