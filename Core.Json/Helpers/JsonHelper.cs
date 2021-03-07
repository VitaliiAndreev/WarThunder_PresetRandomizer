using Core.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using Core.Json.Enumerations.Logger;
using Core.Json.Exceptions;
using Core.Json.Helpers.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Json.Helpers
{
    /// <summary> Provides methods to work with JSON data. </summary>
    public abstract class JsonHelper : LoggerFluency, IJsonHelper
    {
        #region Constructors

        /// <summary> Creates a new JSON helper. </summary>
        /// <param name="loggers"> Instances of loggers. </param>
        public JsonHelper(params IConfiguredLogger[] loggers)
            : base(EJsonLogCategory.JsonHelper, loggers)
        {
            LogDebug(ECoreLogMessage.Created.Format(EJsonLogCategory.JsonHelper));
        }

        #endregion Constructors
        #region Methods: [Protected]

        /// <summary> Throws a <see cref="JsonDeserializationException"/> if specified JSON text is not considered valid. </summary>
        /// <param name="jsonText"> JSON text to check. </param>
        protected void ThrowIfJsonTextIsInvalid(string jsonText)
        {
            if (string.IsNullOrWhiteSpace(jsonText))
                throw new JsonDeserializationException(EJsonLogMessage.JsonStringEmpty);
        }

        /// <summary> Throws the specified exception after logging it as an error. Note that the compiler does not see throwing in this method from where it is being called. </summary>
        /// <param name="exception"> The exception to throw. </param>
        protected void LogAndRethrow(Exception exception) =>
            LogErrorAndThrow(EJsonLogMessage.ErrorDeserializingJsonText, exception);

        #endregion Methods: [Protected]
        #region Methods: [Protected Virtual] Standardization

        /// <summary> Deserializes and standardizes JSON text into a JSON object. </summary>
        /// <param name="jsonText"> The JSON text to standardize. </param>
        /// <returns></returns>
        protected virtual JObject StandardizeAndDeserializeObject(string jsonText) =>
            throw new NotImplementedException();

        /// <summary> Deserializes and standardizes JSON text into a dictionary of JSON objects. </summary>
        /// <param name="jsonText"> The JSON text to standardize. </param>
        /// <returns></returns>
        protected virtual IDictionary<string, JObject> StandardizeAndDeserializeObjects(string jsonText) =>
            throw new NotImplementedException();

        #endregion Methods: [Protected Virtual] Standardization
        #region Methods: [Public] Deserialization

        /// <summary> Deserializes JSON text and creates an object instance from it. </summary>
        /// <typeparam name="T"> The object time into which to deserialize. </typeparam>
        /// <param name="jsonText"> The JSON text to deserialize. </param>
        /// <param name="suppressStandardization"> Whether to avoid doing any pre-processing of JSON entities. </param>
        /// <returns></returns>
        public T DeserializeObject<T>(string jsonText, bool suppressStandardization = false)
        {
            LogDebug(EJsonLogMessage.TryingToDeserializeJsonStringIntoObject.ResetFormattingPlaceholders().Format(jsonText.Count(), typeof(T).Name));
            var deserializedInstance = default(T);

            try
            {
                ThrowIfJsonTextIsInvalid(jsonText);

                deserializedInstance = suppressStandardization || typeof(T).Name.Contains(EConstants.ObjectClassName.ToString()) // To skip standardization or to avoid cyclical calls of DeserializeObject<dynamic>().
                    ? JsonConvert.DeserializeObject<T>(jsonText)
                    : StandardizeAndDeserializeObject(jsonText).ToObject<T>();
            }
            catch (Exception exception)
            {
                LogAndRethrow(exception);
            }

            LogDebug(EJsonLogMessage.InstanceDeserialized);
            return deserializedInstance;
        }

        /// <summary> Deserializes JSON text and creates a collection of object instances from it. </summary>
        /// <typeparam name="T"> The object time into which to deserialize. </typeparam>
        /// <param name="jsonText"> The JSON text to deserialize. </param>
        /// <returns> A collection of object instances. </returns>
        public virtual IDictionary<string, T> DeserializeDictionary<T>(string jsonText)
        {
            LogDebug(EJsonLogMessage.TryingToDeserializeJsonStringIntoCollection.ResetFormattingPlaceholders().Format(jsonText.Count(), typeof(T).Name));
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
            catch (Exception exception)
            {
                LogAndRethrow(exception);
            }

            LogDebug(EJsonLogMessage.InstancesDeserialized.Format(deserializedInstances.Count()));
            return deserializedInstances;
        }

        #endregion Methods: [Public] Deserialization
    }
}