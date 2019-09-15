using Core.DataBase.WarThunder.Objects.Json;
using Core.Extensions;
using System;
using System.Collections.Generic;

namespace Core.Json.WarThunder.Extensions
{
    public static class IDictionaryExtensions
    {
        public static IDictionary<string, T> SetOwners<T, U>(this IDictionary<string, T> dictionary, U owner)
            where T : DeserializedFromJsonWithOwner<U>
            where U : DeserializedFromJson
        {
            foreach (var keyValuePair in dictionary)
                keyValuePair.Value.Owner = owner;

            return dictionary;
        }

        public static IDictionary<string, T> FinalizeDeserialization<T>(this IDictionary<string, T> dictionary) where T : DeserializedFromJson
        {
            if (dictionary.IsEmpty())
                throw new NotImplementedException();

            foreach (var keyValuePair in dictionary)
            {
                var entity = keyValuePair.Value;

                entity.GaijinId = keyValuePair.Key;

                if (entity is VehicleDeserializedFromJsonWpCost vehicle)
                    vehicle.Weapons.SetOwners(vehicle).FinalizeDeserialization();
            }

            return dictionary;
        }
    }
}