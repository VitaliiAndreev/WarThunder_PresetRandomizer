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
        #region Methods: Private

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

        #endregion Methods: Private

        /// <summary>
        /// Standardizes JSON text of a single object.
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former. </para>
        /// </summary>
        /// <param name="jsonData"> The JSON data to standardize. </param>
        /// <returns></returns>
        private JObject StandardizeObject(string jsonData) =>
            StandardizeEntity(DeserializeObject<dynamic>(jsonData));

        /// <summary>
        /// Standardizes JSON text.
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
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
        /// Standardizes JSON text of a single object.
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
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
        /// Standardizes JSON text of a collection of objects.
        /// <para> In some instances (when duplicate JSON propery names are present) JSON objects are being presented not as a set of properties and their values, but as an array key-value pairs. </para>
        /// <para> To deserialize both implementations as instances of one type, the latter case is converted to look like the former. </para>
        /// </summary>
        /// <param name="container"> The JSON text to standardize. </param>
        /// <returns></returns>
        private IDictionary<string, JObject> StandardizeObjects(string jsonText)
        {
            var entities = DeserializeDictionary<dynamic>(jsonText);
            var standardizedJsonObjects = new Dictionary<string, JObject>();

            foreach (var entity in entities)
                standardizedJsonObjects.Add(entity.Key, StandardizeEntity(entity.Value));

            return standardizedJsonObjects;
        }

        #region Methods: Deserialization

        /// <summary> Deserializes JSON text and creates an object instance from it. </summary>
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
                    foreach (var jsonObject in StandardizeObjects(jsonText))
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
