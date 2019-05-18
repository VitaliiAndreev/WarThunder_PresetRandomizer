using Core.Enumerations;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Enumerations;
using Core.Helpers.Logger.Interfaces;
using Core.Json.Exceptions;
using Core.Json.Extensions;
using Core.Json.Helpers.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Json.Helpers
{
    /// <summary> Provide methods to work with JSON data. </summary>
    public class JsonHelper : LoggerFluency, IJsonHelper
    {
        #region Constructors

        /// <summary> Creates a new JSON helper. </summary>
        /// <param name="logger"> An instance of a logger. </param>
        public JsonHelper(IConfiguredLogger logger)
            : base(logger, ECoreLogCategory.JsonHelper)
        {
            LogDebug(ECoreLogMessage.Created.FormatFluently(ECoreLogCategory.JsonHelper));
        }

        #endregion Constructors
        #region Methods: [Private]

        /// <summary> Throws a <see cref="JsonDeserializationException"/> if specified JSON text is not considered valid. </summary>
        /// <param name="jsonText"> JSON text to check. </param>
        private void ThrowIfJsonTextIsInvalid(string jsonText)
        {
            if (jsonText.IsNullOrWhiteSpaceFluently())
                throw new JsonDeserializationException(ECoreLogMessage.JsonStringEmpty);
        }

        /// <summary> Throws the specified exception after logging it as an error. Note that the compiler does not see throwing in this method from where it is being called. </summary>
        /// <param name="exception"> The exception to throw. </param>
        private void LogAndRethrow(Exception exception) =>
            LogErrorAndThrow(ECoreLogMessage.ErrorDeserializingJsonText, exception);

        #endregion Methods: [Private]
        #region Methods: [Private] GetPotentiallyDuplicatePropertyNames()

        /// <summary> Looks through the specified JSON container and records property names that would occur more than once after standardization. </summary>
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former while values assigned to duplicate property names are combined into arrays. </para>
        /// <param name="jsonContainer"> The JSON container to search in. </param>
        /// <returns></returns>
        private IEnumerable<string> GetPotentiallyDuplicatePropertyNamesInContainer(JContainer jsonContainer)
        {
            var duplicatePropertyNames = new List<string>();

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

            return duplicatePropertyNames.Distinct();
        }

        /// <summary> Looks through the specified entity and records property names that would occur more than once after standardization. </summary>
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former while values assigned to duplicate property names are combined into arrays. </para>
        /// <param name="entity"> The entity to search in. </param>
        /// <returns></returns>
        private IEnumerable<string> GetPotentiallyDuplicatePropertyNames(dynamic entity)
        {
            var duplicatePropertyNames = new List<string>();

            if (entity is JContainer jsonContainer)
                duplicatePropertyNames.AddRange(GetPotentiallyDuplicatePropertyNamesInContainer(jsonContainer));

            return duplicatePropertyNames.Distinct();
        }

        /// <summary> Looks through the specified dictionary of entities and records property names that would occur more than once after standardization. </summary>
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former while values assigned to duplicate property names are combined into arrays. </para>
        /// <param name="entity"> The dictionary of entities to search in. </param>
        /// <returns></returns>
        private IEnumerable<string> GetPotentiallyDuplicatePropertyNames(IDictionary<string, dynamic> entities)
        {
            var duplicatePropertyNames = new List<string>();

            foreach (var keyValuePair in entities)
                duplicatePropertyNames.AddRange(GetPotentiallyDuplicatePropertyNames(keyValuePair.Value));

            return duplicatePropertyNames.Distinct();
        }

        #endregion Methods: [Private] GetPotentiallyDuplicatePropertyNames()
        #region Methods: [Private] Deserialization with Standardization

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
                    jsonToken = StandardizeContainer(jsonContainer, duplicatePropertyNames);
            }
            return jsonObject;
        }

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
        /// Deserializes and standardizes the specified JSON entity into a JSON object.
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former. </para>
        /// </summary>
        /// <param name="entity"> The deserialized JSON entity. </param>
        /// <param name="duplicatePropertyNames"> A collection of duplicate property names whose values are to be aggregated into arrays. </param>
        /// <returns></returns>
        private JObject StandardizeAndDeserializeObject(dynamic entity, IEnumerable<string> duplicatePropertyNames)
        {
            if (entity is JContainer container)
                return StandardizeContainer(container, duplicatePropertyNames) as JObject;
            else
                return entity;
        }

        /// <summary>
        /// Deserializes and standardizes JSON text into a JSON object.
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former. </para>
        /// </summary>
        /// <param name="jsonText"> The JSON text to standardize. </param>
        /// <returns></returns>
        private JObject StandardizeAndDeserializeObject(string jsonText)
        {
            var entity = DeserializeObject<dynamic>(jsonText);

            return StandardizeAndDeserializeObject(entity, GetPotentiallyDuplicatePropertyNames(entity));
        }

        /// <summary>
        /// Deserializes and standardizes JSON text into a dictionary of JSON objects.
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former. </para>
        /// </summary>
        /// <param name="jsonText"> The JSON text to standardize. </param>
        /// <returns></returns>
        private IDictionary<string, JObject> StandardizeAndDeserializeObjects(string jsonText)
        {
            var entities = DeserializeDictionary<dynamic>(jsonText);
            var standardizedJsonObjects = new Dictionary<string, JObject>();
            var duplicatePropertyNames = GetPotentiallyDuplicatePropertyNames(entities);

            foreach (var keyValuePair in entities)
                standardizedJsonObjects.Add(keyValuePair.Key, StandardizeAndDeserializeObject(keyValuePair.Value, duplicatePropertyNames));

            return standardizedJsonObjects;
        }

        #endregion [Private] Methods: Deserialization with Standardization
        #region Methods: Deserialization

        /// <summary> Deserializes JSON text and creates an object instance from it. </summary>
        /// <typeparam name="T"> The object time into which to deserialize. </typeparam>
        /// <param name="jsonText"> The JSON text to deserialize. </param>
        /// <returns></returns>
        public T DeserializeObject<T>(string jsonText)
        {
            LogDebug(ECoreLogMessage.TryingToDeserializeJsonStringIntoObject.ResetFormattingPlaceholders().FormatFluently(jsonText.Count(), typeof(T).Name));
            var deserializedInstance = default(T);

            try
            {
                ThrowIfJsonTextIsInvalid(jsonText);

                if (typeof(T).Name.Contains(EConstants.ObjectClassName.ToString())) // To avoid cyclical calls of DeserializeObject<dynamic>().
                    deserializedInstance = JsonConvert.DeserializeObject<T>(jsonText);
                else
                    deserializedInstance = StandardizeAndDeserializeObject(jsonText).ToObject<T>();
            }
            catch (Exception exception)
            {
                LogAndRethrow(exception);
            }

            LogDebug(ECoreLogMessage.DeserializedInstance);
            return deserializedInstance;
        }

        /// <summary> Deserializes JSON text and creates a collection of object instances from it. </summary>
        /// <typeparam name="T"> The object time into which to deserialize. </typeparam>
        /// <param name="jsonText"> The JSON text to deserialize. </param>
        /// <returns> A collection of object instances. </returns>
        public virtual IDictionary<string, T> DeserializeDictionary<T>(string jsonText)
        {
            LogDebug(ECoreLogMessage.TryingToDeserializeJsonStringIntoCollection.ResetFormattingPlaceholders().FormatFluently(jsonText.Count(), typeof(T).Name));
            var deserializedInstances = new Dictionary<string, T>();

            try
            {
                ThrowIfJsonTextIsInvalid(jsonText);

                if (typeof(T).Name.Contains(EConstants.ObjectClassName.ToString())) // To avoid cyclical calls of DeserializeObject<dynamic>().
                {
                    deserializedInstances = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonText);
                }
                else
                {
                    foreach (var jsonObject in StandardizeAndDeserializeObjects(jsonText))
                    {
                        var deserializedObject = jsonObject.Value.ToObject<T>();
                        deserializedInstances.Add(jsonObject.Key, deserializedObject);
                    }
                }
            }
            catch(Exception exception)
            {
                LogAndRethrow(exception);
            }

            LogDebug(ECoreLogMessage.DeserializedInstances.FormatFluently(deserializedInstances.Count()));
            return deserializedInstances;
        }

        #endregion Methods: Deserialization
    }
}