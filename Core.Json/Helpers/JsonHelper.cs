using Core.Enumerations;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Enumerations;
using Core.Helpers.Logger.Interfaces;
using Core.Json.Exceptions;
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
        #region Methods: Deserialization

        private void ThrowIfJsonDataIsInvalid(string jsonData)
        {
            if (jsonData.IsNullOrWhiteSpaceFluently())
                throw new JsonDeserializationException(ECoreLogMessage.JsonStringEmpty);
        }

        private void LogAndRethrow(Exception exception)
        {
            LogError(ECoreLogMessage.ErrorDeserializingJsonData, exception);
            throw exception;
        }

        /// <summary>
        /// Standardizes JSON data of a single object.
        /// <para> In some instances JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former. </para>
        /// </summary>
        /// <param name="jsonData"> The JSON data to standardize. </param>
        /// <returns></returns>
        private JObject StandardizeObject(string jsonData) =>
            StandardizeEntity(DeserializeObject<dynamic>(jsonData));

        /// <summary>
        /// Standardizes JSON data.
        /// <para> In some instances JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former. </para>
        /// </summary>
        /// <param name="entity"> The deserialized JSON entity. </param>
        /// <returns></returns>
        private JObject StandardizeEntity(dynamic entity)
        {
            if (entity is JContainer container)
                return StandardizeContainer(container);
            else
                return null;
        }

        /// <summary>
        /// Standardizes JSON data of a single object.
        /// <para> In some instances JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former. </para>
        /// </summary>
        /// <param name="container"> The JSON container to standardize. </param>
        /// <returns></returns>
        private JObject StandardizeContainer(JContainer container)
        {
            if (container is JObject jsonObject)
            {
                return jsonObject;
            }
            else if (container is JArray jsonArray)
            {
                var rebuiltJsonObject = new JObject();

                foreach (var fragmentedEntity in jsonArray)
                {
                    foreach (var childToken in fragmentedEntity.Children())
                    {
                        var key = childToken.Path.Split(ECharacter.Period).Last();
                        if (!rebuiltJsonObject.Properties().Select(property => property.Name).Contains(key))
                            rebuiltJsonObject.Add(key, childToken.First());
                    }
                }
                return rebuiltJsonObject;
            }
            else
                return null;
        }

        /// <summary>
        /// Standardizes JSON data of a collection of objects.
        /// <para> In some instances JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former. </para>
        /// </summary>
        /// <param name="container"> The JSON data to standardize. </param>
        /// <returns></returns>
        private Dictionary<string, JObject> StandardizeObjects(string jsonData)
        {
            var entities = DeserializeDictionary<dynamic>(jsonData);
            var standardizedJsonObjects = new Dictionary<string, JObject>();

            foreach (var entity in entities)
                standardizedJsonObjects.Add(entity.Key, StandardizeEntity(entity.Value));

            return standardizedJsonObjects;
        }

        /// <summary> Deserializes JSON data and creates an object instance from it. </summary>
        /// <typeparam name="T"> The object time into which to deserialize. </typeparam>
        /// <param name="jsonData"> The JSON data to deserialize. </param>
        /// <returns></returns>
        public T DeserializeObject<T>(string jsonData)
        {
            LogDebug(ECoreLogMessage.TryingToDeserializeJsonStringIntoObject.ResetFormattingPlaceholders().FormatFluently(jsonData.Count(), typeof(T).Name));
            var deserializedInstance = default(T);

            try
            {
                ThrowIfJsonDataIsInvalid(jsonData);

                var standardizedJsonData = typeof(T).Name.Contains(EConstants.ObjectClassName.ToString()) // To avoid cyclical calls of DeserializeObject<dynamic>().
                    ? jsonData
                    : StandardizeObject(jsonData).ToString();

                deserializedInstance = JsonConvert.DeserializeObject<T>(standardizedJsonData);
            }
            catch (Exception exception)
            {
                LogAndRethrow(exception);
            }

            LogDebug(ECoreLogMessage.DeserializedInstance);
            return deserializedInstance;
        }

        /// <summary> Deserializes JSON data and creates a collection of object instances from it. </summary>
        /// <typeparam name="T"> The object time into which to deserialize. </typeparam>
        /// <param name="jsonData"> The JSON data to deserialize. </param>
        /// <returns> A collection of object instances. </returns>
        public virtual Dictionary<string, T> DeserializeDictionary<T>(string jsonData)
        {
            LogDebug(ECoreLogMessage.TryingToDeserializeJsonStringIntoCollection.ResetFormattingPlaceholders().FormatFluently(jsonData.Count(), typeof(T).Name));
            var deserializedInstances = new Dictionary<string, T>();

            try
            {
                ThrowIfJsonDataIsInvalid(jsonData);

                if (typeof(T).Name.Contains(EConstants.ObjectClassName.ToString())) // To avoid cyclical calls of DeserializeObject<dynamic>().
                {
                    deserializedInstances = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonData);
                }
                else
                {
                    foreach (var jsonObject in StandardizeObjects(jsonData))
                    {
                        var deserializedObject = JsonConvert.DeserializeObject<T>(jsonObject.Value.ToString());
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
