using Core.DataBase.Enumerations.Logger;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Localization.Interfaces;
using Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataBase.WarThunder.Helpers
{
    public static class DataRepositoryWarThunder
    {
        public static void ReorderNewObjectsToAdhereToForeignKeys(IDataRepository dataRepository)
        {
            var sortedNewObjects = new List<IPersistentObject>();

            sortedNewObjects.AddRange(dataRepository.NewObjects.OfType<INation>());
            sortedNewObjects.AddRange(dataRepository.NewObjects.OfType<IBranch>());
            sortedNewObjects.AddRange(dataRepository.NewObjects.OfType<IVehicle>());
            sortedNewObjects.AddRange(dataRepository.NewObjects.OfType<IVehicleSubclasses>());
            sortedNewObjects.AddRange(dataRepository.NewObjects.OfType<IAircraftTags>());
            sortedNewObjects.AddRange(dataRepository.NewObjects.OfType<IGroundVehicleTags>());
            sortedNewObjects.AddRange(dataRepository.NewObjects.OfType<IVehicleResearchTreeData>());
            sortedNewObjects.AddRange(dataRepository.NewObjects.OfType<IVehicleEconomyData>());
            sortedNewObjects.AddRange(dataRepository.NewObjects.OfType<IVehiclePerformanceData>());
            sortedNewObjects.AddRange(dataRepository.NewObjects.OfType<IVehicleCrewData>());
            sortedNewObjects.AddRange(dataRepository.NewObjects.OfType<IVehicleWeaponsData>());
            sortedNewObjects.AddRange(dataRepository.NewObjects.OfType<IVehicleModificationsData>());
            sortedNewObjects.AddRange(dataRepository.NewObjects.OfType<IVehicleGraphicsData>());
            sortedNewObjects.AddRange(dataRepository.NewObjects.OfType<IVehicleGameModeParameterSetBase>());
            sortedNewObjects.AddRange(dataRepository.NewObjects.OfType<ILocalization>());

            if (sortedNewObjects.Count() != dataRepository.NewObjects.Count())
                throw new ArgumentException(EDatabaseLogMessage.NotAllObjectTypesHaveBeenIncludedInSorting.FormatFluently(nameof(dataRepository.NewObjects)));

            dataRepository.NewObjects.ReplaceBy(sortedNewObjects);
        }
    }
}