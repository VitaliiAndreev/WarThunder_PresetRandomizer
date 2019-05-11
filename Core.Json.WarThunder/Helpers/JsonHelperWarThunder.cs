using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Objects;
using Core.DataBase.WarThunder.Objects.Json;
using Core.Helpers.Logger.Interfaces;
using Core.Json.WarThunder.Helpers.Interfaces;
using System.Collections.Generic;

namespace Core.Json.Helpers
{
    /// <summary> Provide methods to work with JSON data specific to War Thunder. </summary>
    public class JsonHelperWarThunder : JsonHelper, IJsonHelperWarThunder
    {
        #region Constructors

        /// <summary> Creates a new War Thunder JSON helper. </summary>
        /// <param name="logger"> An instance of a logger. </param>
        public JsonHelperWarThunder(IConfiguredLogger logger)
            : base(logger)
        {
        }

        #endregion Constructors
        #region Methods: Deserialization

        /// <summary> Initializes <see cref="DeserializedFromJson.GaijinId"/> values with corresponding keys from the specified dictionary and outputs a collection of resulting objects. </summary>
        /// <typeparam name="T"> A generic JSON mapping type. </typeparam>
        /// <param name="dictionary"> The dictionary to process. </param>
        /// <returns></returns>
        private IEnumerable<T> SetGaijinIdsAndReturnDictionaryValues<T>(IDictionary<string, T> dictionary) where T : DeserializedFromJson
        {
            foreach (var pair in dictionary)
                pair.Value.GaijinId = pair.Key;

            return dictionary.Values;
        }

        /// <summary> Deserializes given JSON data into instances of interim non-persistent objects. </summary>
        /// <typeparam name="T"> A generic type of JSON mapping classes. </typeparam>
        /// <param name="jsonData"> JSON data to deserialize. </param>
        /// <returns></returns>
        public IEnumerable<T> DeserializeList<T>(string jsonData) where T: DeserializedFromJson =>
            SetGaijinIdsAndReturnDictionaryValues(DeserializeDictionary<T>(jsonData));

        /// <summary> Deserializes given JSON data into instances persistent objects. </summary>
        /// <typeparam name="T"> A generic type of persistent objects. </typeparam>
        /// <param name="dataRepository"> The data repository to assign new instances to. </param>
        /// <param name="jsonData"> JSON data to deserialize. </param>
        /// <returns></returns>
        public IEnumerable<T> DeserializeList<T>(IDataRepository dataRepository, string jsonData) where T : PersistentObjectWithIdAndGaijinId
        {
            var deserializedInstances = new List<T>();

            if (typeof(T) == typeof(Vehicle))
            {
                foreach (var deserializedData in DeserializeList<VehicleDeserializedFromJson>(jsonData))
                    deserializedInstances.Add(new Vehicle(dataRepository, deserializedData) as T);
            }
            return deserializedInstances;
        }

        #endregion Methods: Deserialization
    }
}
