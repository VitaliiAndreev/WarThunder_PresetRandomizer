using Core.DataBase.WarThunder.Objects.Json;
using Core.DataBase.WarThunder.Objects.Json.Interfaces;
using Core.Extensions;
using System;
using System.Collections.Generic;

namespace Core.Json.WarThunder.Extensions
{
    public static class IDictionaryExtensions
    {
        /// <summary> Initializes <see cref="DeserializedFromJsonWithOwner{T}.Owner"/> references of items in the given dictionary with the specified owner instance. </summary>
        /// <typeparam name="T"> The type of owned instances. </typeparam>
        /// <typeparam name="U"> The type of the owner instance. </typeparam>
        /// <param name="dictionary"> The dictionary of owned instances indexed by their <see cref="DeserializedFromJsonWithGaijinId.GaijinId"/>. </param>
        /// <param name="owner"> The owner instance to attribute items in the given dictionary to. </param>
        public static IDictionary<string, T> SetOwners<T, U>(this IDictionary<string, T> dictionary, U owner)
            where T : DeserializedFromJsonWithOwner<U>
            where U : DeserializedFromJsonWithGaijinId
        {
            foreach (var keyValuePair in dictionary)
                keyValuePair.Value.Owner = owner;

            return dictionary;
        }

        /// <summary> Finalizes deserialization of items in the given dictionary by initializing properties not involved in deserialization. </summary>
        /// <typeparam name="T"> The type of items in the dictionary. </typeparam>
        /// <param name="dictionary"> The dictionary of instances to finalize deserialization of. </param>
        /// <returns></returns>
        public static IDictionary<string, T> FinalizeDeserialization<T>(this IDictionary<string, T> dictionary) where T : IDeserializedFromJsonWithGaijinId
        {
            if (dictionary.IsEmpty())
                throw new NotImplementedException();

            foreach (var keyValuePair in dictionary)
            {
                var entity = keyValuePair.Value;

                entity.GaijinId = keyValuePair.Key;

                if (entity is VehicleDeserializedFromJsonWpCost vehicle)
                {
                    vehicle.Weapons.SetOwners(vehicle);
                    vehicle.Modifications.SetOwners(vehicle);

                    vehicle.IsPremium = vehicle.Modifications.ContainsKey("allowPremDecals");
                }
            }

            return dictionary;
        }
    }
}