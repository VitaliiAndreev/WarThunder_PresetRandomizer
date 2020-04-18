using Core.DataBase.Helpers;
using Core.DataBase.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Localization.Interfaces;
using Core.Helpers.Logger.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.DataBase.WarThunder.Helpers
{
    public class DataRepositorySqliteWarThunder : DataRepositorySqlite
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
        private readonly IList<ILocalization> _newLocalizationRecords;

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

                    return newObjects;
                }
            }
        }

        #endregion Properties
        #region Constructors

        public DataRepositorySqliteWarThunder(string dataBaseFileName, bool overwriteExistingDataBase, Assembly assemblyWithMapping, bool singleSession, params IConfiguredLogger[] loggers)
            : base(dataBaseFileName, overwriteExistingDataBase, assemblyWithMapping, singleSession, loggers)
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
            _newLocalizationRecords = new List<ILocalization>();
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
                if (objectType == typeof(ILocalization)) return _newLocalizationRecords.Cast<T>().ToList();
            }
            return new List<T>();
        }

        public override void AddToNewObjects<T>(T newObject)
        {
            lock (_lock)
            {
                if (newObject is INation newNation && !_newNations.Contains(newNation)) _newNations.Add(newNation);
                else if (newObject is IBranch newBranch && !_newBranches.Contains(newBranch)) _newBranches.Add(newBranch);
                else if (newObject is IVehicle newVehicle && !_newVehicles.Contains(newVehicle)) _newVehicles.Add(newVehicle);
                else if (newObject is IVehicleSubclasses newVehicleSubclass && !_newVehicleSubclasses.Contains(newVehicleSubclass)) _newVehicleSubclasses.Add(newVehicleSubclass);
                else if (newObject is IAircraftTags newAircraftTagSet && !_newAircraftTags.Contains(newAircraftTagSet)) _newAircraftTags.Add(newAircraftTagSet);
                else if (newObject is IGroundVehicleTags newGroundVehicleTagSet && !_newGroundVehicleTags.Contains(newGroundVehicleTagSet)) _newGroundVehicleTags.Add(newGroundVehicleTagSet);
                else if (newObject is IVehicleResearchTreeData newVehicleResearchTreeData && !_newVehicleResearchTreeData.Contains(newVehicleResearchTreeData)) _newVehicleResearchTreeData.Add(newVehicleResearchTreeData);
                else if (newObject is IVehicleEconomyData newVehicleEconomyData && !_newVehicleEconomyData.Contains(newVehicleEconomyData)) _newVehicleEconomyData.Add(newVehicleEconomyData);
                else if (newObject is IVehiclePerformanceData newVehiclePerformanceData && !_newVehiclePerformanceData.Contains(newVehiclePerformanceData)) _newVehiclePerformanceData.Add(newVehiclePerformanceData);
                else if (newObject is IVehicleCrewData newVehicleCrewData && !_newVehicleCrewData.Contains(newVehicleCrewData)) _newVehicleCrewData.Add(newVehicleCrewData);
                else if (newObject is IVehicleWeaponsData newVehicleWeaponsData && !_newVehicleWeaponsData.Contains(newVehicleWeaponsData)) _newVehicleWeaponsData.Add(newVehicleWeaponsData);
                else if (newObject is IVehicleModificationsData newVehicleModificationsData && !_newVehicleModificationsData.Contains(newVehicleModificationsData)) _newVehicleModificationsData.Add(newVehicleModificationsData);
                else if (newObject is IVehicleGraphicsData newVehicleGraphicsData && !_newVehicleGraphicsData.Contains(newVehicleGraphicsData)) _newVehicleGraphicsData.Add(newVehicleGraphicsData);
                else if (newObject is IVehicleGameModeParameterSetBase newVehicleGameModeParameterSet && !_newVehicleGameModeParameterSets.Contains(newVehicleGameModeParameterSet)) _newVehicleGameModeParameterSets.Add(newVehicleGameModeParameterSet);
                else if (newObject is ILocalization newLocalizationSet && !_newLocalizationRecords.Contains(newLocalizationSet)) _newLocalizationRecords.Add(newLocalizationSet);
            }
        }

        public override void RemoveFromNewObjects(IPersistentObject @object)
        {
            lock (_lock)
            {
                if (@object is INation nation && _newNations.Contains(nation)) _newNations.Remove(nation);
                else if (@object is IBranch branch && _newBranches.Contains(branch)) _newBranches.Remove(branch);
                else if (@object is IVehicle vehicle && _newVehicles.Contains(vehicle)) _newVehicles.Remove(vehicle);
                else if (@object is IVehicleSubclasses vehicleSubclass && _newVehicleSubclasses.Contains(vehicleSubclass)) _newVehicleSubclasses.Remove(vehicleSubclass);
                else if (@object is IAircraftTags aircraftTagSet && _newAircraftTags.Contains(aircraftTagSet)) _newAircraftTags.Remove(aircraftTagSet);
                else if (@object is IGroundVehicleTags groundVehicleTagSet && _newGroundVehicleTags.Contains(groundVehicleTagSet)) _newGroundVehicleTags.Remove(groundVehicleTagSet);
                else if (@object is IVehicleResearchTreeData vehicleResearchTreeData && _newVehicleResearchTreeData.Contains(vehicleResearchTreeData)) _newVehicleResearchTreeData.Remove(vehicleResearchTreeData);
                else if (@object is IVehicleEconomyData vehicleEconomyData && _newVehicleEconomyData.Contains(vehicleEconomyData)) _newVehicleEconomyData.Remove(vehicleEconomyData);
                else if (@object is IVehiclePerformanceData vehiclePerformanceData && _newVehiclePerformanceData.Contains(vehiclePerformanceData)) _newVehiclePerformanceData.Remove(vehiclePerformanceData);
                else if (@object is IVehicleCrewData vehicleCrewData && _newVehicleCrewData.Contains(vehicleCrewData)) _newVehicleCrewData.Remove(vehicleCrewData);
                else if (@object is IVehicleWeaponsData vehicleWeaponsData && _newVehicleWeaponsData.Contains(vehicleWeaponsData)) _newVehicleWeaponsData.Remove(vehicleWeaponsData);
                else if (@object is IVehicleModificationsData vehicleModificationsData && _newVehicleModificationsData.Contains(vehicleModificationsData)) _newVehicleModificationsData.Remove(vehicleModificationsData);
                else if (@object is IVehicleGraphicsData vehicleGraphicsData && _newVehicleGraphicsData.Contains(vehicleGraphicsData)) _newVehicleGraphicsData.Remove(vehicleGraphicsData);
                else if (@object is IVehicleGameModeParameterSetBase vehicleGameModeParameterSet && _newVehicleGameModeParameterSets.Contains(vehicleGameModeParameterSet)) _newVehicleGameModeParameterSets.Remove(vehicleGameModeParameterSet);
                else if (@object is ILocalization localizationSet && _newLocalizationRecords.Contains(localizationSet)) _newLocalizationRecords.Remove(localizationSet);
            }
        }

        public override void ClearNewObjects()
        {
            lock (_lock)
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
            }
        }

        #endregion Methods: Overrides
    }
}