using Core.DataBase.WarThunder.Attributes;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Json.Interfaces;
using Core.DataBase.WarThunder.Objects.VehicleGameModeParameterSets;
using System.Collections.Generic;
using System.Reflection;

namespace Core.DataBase.WarThunder.Extensions
{
    public static class IDictionaryExtensions
    {
        public static void InsertJsonPropertyValueIntoGameModeParameterSet(this IDictionary<string, VehicleGameModeParameterSetBase> parameterSets, IDeserializedFromJsonWithGaijinId instanceDeserializedFromJson, PropertyInfo jsonProperty)
        {
            var persistAsDictionaryItemAttribute = jsonProperty.GetCustomAttribute<PersistAsDictionaryItemAttribute>();

            if (persistAsDictionaryItemAttribute is null) // We are not interested in any properties not marked for consolidation via PersistAsDictionaryItemAttribute.
                return;

            if (!parameterSets.TryGetValue(persistAsDictionaryItemAttribute.Key, out var parameterSet))
                return;

            var jsonPropertyValue = jsonProperty.GetValue(instanceDeserializedFromJson);

            #region Adjust value inputs for nullability of dictionary values (in case of non-required JSON properties)

            if (jsonProperty.PropertyType == typeof(int))
                jsonPropertyValue = new int?((int)jsonPropertyValue);

            else if (jsonProperty.PropertyType == typeof(decimal))
                jsonPropertyValue = new decimal?((decimal)jsonPropertyValue);

            #endregion Adjust value inputs for nullability of dictionary values (in case of non-required JSON properties

            switch (persistAsDictionaryItemAttribute.GameMode)
            {
                case EGameMode.Arcade:
                    parameterSet.InternalArcade = jsonPropertyValue;
                    break;
                case EGameMode.Realistic:
                    parameterSet.InternalRealistic = jsonPropertyValue;
                    break;
                case EGameMode.Simulator:
                    parameterSet.InternalSimulator = jsonPropertyValue;
                    break;
                case EGameMode.Event:
                    parameterSet.InternalEvent = jsonPropertyValue;
                    break;
            };
        }
    }
}