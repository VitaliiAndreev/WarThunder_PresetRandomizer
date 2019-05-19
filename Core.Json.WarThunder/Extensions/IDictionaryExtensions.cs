using Core.DataBase.WarThunder.Objects.Json;
using Core.Extensions;
using System;
using System.Collections.Generic;

namespace Core.Json.WarThunder.Extensions
{
    public static class IDictionaryExtensions
    {
        /// <summary> Initializes <see cref="DeserializedFromJson.GaijinId"/> values with corresponding keys from the specified dictionary and outputs a collection of resulting objects. </summary>
        /// <typeparam name="T"> A generic JSON mapping type. </typeparam>
        /// <param name="dictionary"> The dictionary to process. </param>
        /// <returns></returns>
        public static IDictionary<string, T> SetGaijinIds<T>(this IDictionary<string, T> dictionary) where T : DeserializedFromJson
        {
            foreach (var keyValuePair in dictionary)
                keyValuePair.Value.GaijinId = keyValuePair.Key;

            return dictionary;
        }

        public static IDictionary<string, T> SetOwners<T, U>(this IDictionary<string, T> dictionary, U owner) where T : DeserializedFromJsonWithOwner<U>
        {
            foreach (var keyValuePair in dictionary)
                keyValuePair.Value.Owner = owner;

            return dictionary;
        }

        public static IDictionary<string, T> FinalizeDeserialization<T>(this IDictionary<string, T> dictionary) where T : DeserializedFromJson
        {
            if (dictionary.IsEmpty())
                throw new NotImplementedException();

            dictionary.SetGaijinIds();

            foreach (var keyValuePair in dictionary)
            {
                if (keyValuePair.Value is VehicleDeserializedFromJson vehicle)
                    vehicle.Weapons.SetGaijinIds().SetOwners(vehicle);
            }
            return dictionary;
        }
    }
}