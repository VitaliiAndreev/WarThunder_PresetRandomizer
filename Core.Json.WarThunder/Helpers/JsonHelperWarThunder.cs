using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Objects;
using Core.DataBase.WarThunder.Objects.Json;
using Core.Extensions;
using Core.Helpers.Logger.Interfaces;
using Core.Json.Enumerations.Logger;
using Core.Json.Exceptions;
using Core.Json.Extensions;
using Core.Json.WarThunder.Helpers.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

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
        #region Methods: [Private] GetPotentiallyDuplicatePropertyNames()

        /// <summary> Looks through the specified JSON container and records property names that would occur more than once after standardization. </summary>
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former while values assigned to duplicate property names are combined into arrays. </para>
        /// <param name="jsonContainer"> The JSON container to search in. </param>
        /// <returns></returns>
        private ISet<string> GetPotentiallyDuplicatePropertyNamesInContainer(JContainer jsonContainer)
        {
            var duplicatePropertyNames = new HashSet<string>();

            if (jsonContainer is JArray jsonArray)
            {
                var propertyNames = jsonArray
                    .Children()
                    .OfType<JObject>()
                    .SelectMany(fragmentedJsonObject => fragmentedJsonObject.Properties().Select(jsonProperty => jsonProperty.Name))
                ;

                duplicatePropertyNames.AddRange(propertyNames.Where(propertyName => propertyNames.Count(item => item == propertyName) > 1));
            }

            foreach (var jsonToken in jsonContainer)
            {
                if (jsonToken is JContainer childJsonContainer)
                    duplicatePropertyNames.AddRange(GetPotentiallyDuplicatePropertyNamesInContainer(childJsonContainer));
            }

            return duplicatePropertyNames;
        }

        /// <summary> Looks through the specified JSON token and records property names that would occur more than once after standardization. </summary>
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former while values assigned to duplicate property names are combined into arrays. </para>
        /// <param name="jsonToken"> The JSON token to search in. </param>
        /// <returns></returns>
        private ISet<string> GetPotentiallyDuplicatePropertyNames(JToken jsonToken)
        {
            var potentiallyDuplicatePropertyNames = new HashSet<string>();

            if (jsonToken is JContainer jsonContainer)
                potentiallyDuplicatePropertyNames.AddRange(GetPotentiallyDuplicatePropertyNamesInContainer(jsonContainer));

            return potentiallyDuplicatePropertyNames;
        }

        /// <summary> Looks through the specified dictionary of entities and records property names that would occur more than once after standardization. </summary>
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former while values assigned to duplicate property names are combined into arrays. </para>
        /// <param name="entities"> The dictionary of entities to search in. </param>
        /// <returns></returns>
        private ISet<string> GetPotentiallyDuplicatePropertyNames(IDictionary<string, dynamic> entities)
        {
            var potentiallyDuplicatePropertyNames = new HashSet<string>();

            foreach (var keyValuePair in entities)
                potentiallyDuplicatePropertyNames.AddRange(GetPotentiallyDuplicatePropertyNames(keyValuePair.Value) as ISet<string>);

            return potentiallyDuplicatePropertyNames;
        }

        #endregion Methods: [Private] GetPotentiallyDuplicatePropertyNames()
        #region Methods: [Protected] Deserialization with Standardization

        /// <summary> Handles potentially duplicate property names in the specified JSON object. </summary>
        /// <param name="jsonObject"> The JSON object to process. </param>
        /// <param name="duplicatePropertyNames"> A collection of duplicate property names whose values are to be aggregated into arrays. </param>
        /// <returns></returns>
        private JObject StandardizeObject(JObject jsonObject, IEnumerable<string> duplicatePropertyNames)
        {
            foreach (var jsonPropertyName in jsonObject.GetPropertyNames())
            {
                var jsonToken = jsonObject[jsonPropertyName];

                if (jsonPropertyName.IsIn(duplicatePropertyNames))
                    jsonToken.ConvertIntoArray();

                if (jsonToken is JContainer jsonContainer)
                    jsonObject[jsonPropertyName] = StandardizeContainer(jsonContainer, duplicatePropertyNames);
            }
            return jsonObject;
        }

        /// <summary> Flattens a JSON array of objects into a single JSON object. </summary>
        /// <param name="jsonArray"> The JSON array to flatten. </param>
        /// <param name="duplicatePropertyNames"> A collection of duplicate property names whose values are to be aggregated into arrays. </param>
        /// <returns></returns>
        private JObject ReconstructObjectFromArray(JArray jsonArray, IEnumerable<string> duplicatePropertyNames)
        {
            var rebuiltJsonObject = new JObject();

            foreach (var fragmentedJsonToken in jsonArray)
            {
                if (fragmentedJsonToken is JObject fragmentedJsonObject)
                {
                    foreach (var jsonProperty in fragmentedJsonObject)
                    {
                        if (jsonProperty.Key.IsIn(duplicatePropertyNames))
                        {
                            if (rebuiltJsonObject.Properties().Select(property => property.Name).Contains(jsonProperty.Key))
                            {
                                if (rebuiltJsonObject[jsonProperty.Key] is JArray existingJsonArray)
                                    existingJsonArray.Add(jsonProperty.Value is JArray ? jsonProperty.Value.First() : jsonProperty.Value);
                                else
                                    throw new NotImplementedException();
                            }
                            else
                                rebuiltJsonObject.Add(new JProperty(jsonProperty.Key, jsonProperty.Value is JArray ? jsonProperty.Value : new JArray(jsonProperty.Value)));
                        }
                        else
                        {
                            rebuiltJsonObject.Add(new JProperty(jsonProperty.Key, jsonProperty.Value));
                        }
                    }
                }
                else
                    throw new NotImplementedException();
            }

            return rebuiltJsonObject;
        }

        /// <summary> Handles potentially duplicate property names in the specified JSON array. </summary>
        /// <param name="jsonArray"> The JSON array to process. </param>
        /// <param name="duplicatePropertyNames"> A collection of duplicate property names whose values are to be aggregated into arrays. </param>
        /// <returns></returns>
        private JContainer StandardizeArray(JArray jsonArray, IEnumerable<string> duplicatePropertyNames)
        {
            for (var i = 0; i < jsonArray.Count(); i++)
            {
                var jsonToken = jsonArray[i];

                if (jsonToken is JContainer jsonContainer)
                    jsonArray[i] = StandardizeContainer(jsonContainer, duplicatePropertyNames);
            }

            if (jsonArray.HasPotentiallyDuplicateProperties())
                return ReconstructObjectFromArray(jsonArray, duplicatePropertyNames);

            return jsonArray;
        }

        /// <summary>
        /// Standardizes the specified JSON container into a JSON object.
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former. </para>
        /// </summary>
        /// <param name="jsonContainer"> The JSON container to standardize. </param>
        /// <param name="duplicatePropertyNames"> A collection of duplicate property names whose values are to be aggregated into arrays. </param>
        /// <returns></returns>
        private JContainer StandardizeContainer(JContainer jsonContainer, IEnumerable<string> duplicatePropertyNames)
        {
            if (jsonContainer is JObject jsonObject)
                return StandardizeObject(jsonObject, duplicatePropertyNames);
            else if (jsonContainer is JArray jsonArray)
                return StandardizeArray(jsonArray, duplicatePropertyNames);
            else
                throw new NotImplementedException();
        }

        /// <summary>
        /// Standardizes the specified JSON token into a JSON object.
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former. </para>
        /// </summary>
        /// <param name="jsonToken"> The JSON token to standardize. </param>
        /// <param name="duplicatePropertyNames"> A collection of duplicate property names whose values are to be aggregated into arrays. </param>
        /// <returns></returns>
        private JObject StandardizeTokenIntoObject(JToken jsonToken, IEnumerable<string> duplicatePropertyNames)
        {
            if (jsonToken is JContainer container)
                return StandardizeContainer(container, duplicatePropertyNames) as JObject;
            else
                throw new JsonStandardizationException(ECoreJsonLogMessage.ErrorMustBeJsonContainerToStandardize);
        }

        /// <summary>
        /// Deserializes and standardizes JSON text into a JSON object.
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former. </para>
        /// </summary>
        /// <param name="jsonText"> The JSON text to standardize. </param>
        /// <returns></returns>
        protected override JObject StandardizeAndDeserializeObject(string jsonText)
        {
            var entity = DeserializeObject<dynamic>(jsonText);

            return StandardizeTokenIntoObject(entity, GetPotentiallyDuplicatePropertyNames(entity));
        }

        /// <summary>
        /// Deserializes and standardizes JSON text into a dictionary of JSON objects.
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former. </para>
        /// </summary>
        /// <param name="jsonText"> The JSON text to standardize. </param>
        /// <returns></returns>
        protected override IDictionary<string, JObject> StandardizeAndDeserializeObjects(string jsonText)
        {
            var entities = DeserializeDictionary<dynamic>(jsonText);
            var standardizedJsonObjects = new Dictionary<string, JObject>();
            var duplicatePropertyNames = GetPotentiallyDuplicatePropertyNames(entities);

            foreach (var keyValuePair in entities)
                standardizedJsonObjects.Add(keyValuePair.Key, StandardizeTokenIntoObject(keyValuePair.Value, duplicatePropertyNames));

            return standardizedJsonObjects;
        }

        #endregion [Protected] Methods: Deserialization with Standardization
        #region Methods: [Public] Deserialization

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

        /// <summary> Deserializes given JSON text into instances of interim non-persistent objects. </summary>
        /// <typeparam name="T"> A generic type of JSON mapping classes. </typeparam>
        /// <param name="jsonText"> JSON text to deserialize. </param>
        /// <returns></returns>
        public IEnumerable<T> DeserializeList<T>(string jsonText) where T : DeserializedFromJson =>
            SetGaijinIdsAndReturnDictionaryValues(DeserializeDictionary<T>(jsonText));

        /// <summary> Deserializes given JSON text into instances persistent objects. </summary>
        /// <typeparam name="T"> A generic type of persistent objects. </typeparam>
        /// <param name="dataRepository"> The data repository to assign new instances to. </param>
        /// <param name="jsonText"> JSON text to deserialize. </param>
        /// <returns></returns>
        public IEnumerable<T> DeserializeList<T>(IDataRepository dataRepository, string jsonText) where T : PersistentObjectWithIdAndGaijinId
        {
            var deserializedInstances = new List<T>();

            if (typeof(T) == typeof(Vehicle))
            {
                foreach (var deserializedData in DeserializeList<VehicleDeserializedFromJson>(jsonText))
                    deserializedInstances.Add(new Vehicle(dataRepository, deserializedData) as T);
            }
            return deserializedInstances;
        }

        #endregion Methods: [Public] Deserialization
    }
}