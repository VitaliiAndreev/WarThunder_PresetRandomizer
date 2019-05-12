using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Core.Json.Extensions
{
    /// <summary> Methods extending the <see cref="JsonReader"/> class. </summary>
    public static class JsonReaderExtensions
    {
        #region Methods: Reading

        /// <summary> Reads through JSON text until an actual JSON token is found. </summary>
        /// <param name="jsonReader"> A JSON text reader. </param>
        public static void ReadPastNonTokens(this JsonReader jsonReader)
        {
            while (jsonReader.TokenType == JsonToken.None)
                jsonReader.Read();
        }

        #endregion Methods: Reading
        #region Methods: Deserialization into JToken (based on sample code for resolving duplicate properties by Brian Rogers (https://stackoverflow.com/a/20716106))

        /// <summary> Deserializes JSON text into a JSON token. </summary>
        /// <param name="jsonReader"> A JSON text reader. </param>
        /// <returns></returns>
        public static JToken Deserialize(this JsonReader jsonReader) =>
            jsonReader.Deserialize(DeserializeObject);

        /// <summary> Deserializes JSON text into a JSON token while aggregating values of duplicate properties into arrays loosely, i.e. when encountered. </summary>
        /// <param name="jsonReader"> A JSON text reader. </param>
        /// <returns></returns>
        public static JToken DeserializeAndCombineDuplicatesLoosely(this JsonReader jsonReader) =>
            jsonReader.Deserialize(DeserializeObjectAndCombineDuplicatesLoosely);

        /// <summary> Deserializes JSON text into a JSON token. </summary>
        /// <param name="jsonReader"> A JSON text reader. </param>
        /// <param name="processObject"> The method to process JSON object tokens. </param>
        /// <returns></returns>
        private static JToken Deserialize(this JsonReader jsonReader, Action<JObject, string, JToken, JToken> processObject)
        {
            jsonReader.ReadPastNonTokens();

            if (jsonReader.TokenType == JsonToken.StartObject)
                return jsonReader.DeserializeObject(processObject);

            if (jsonReader.TokenType == JsonToken.StartArray)
                return jsonReader.DeserializeArray(processObject);

            return new JValue(jsonReader.Value);
        }

        #region Methods: Object Deserialization

        /// <summary> Deserializes JSON text into a JSON object. </summary>
        /// <param name="jsonReader"> A JSON text reader. </param>
        /// <param name="processObject"> The method to process JSON object tokens. </param>
        /// <returns></returns>
        private static JObject DeserializeObject(this JsonReader jsonReader, Action<JObject, string, JToken, JToken> processObject)
        {
            var jsonObject = new JObject();

            jsonReader.Read();
            while (jsonReader.TokenType != JsonToken.EndObject)
            {
                var jsonPropertyName = (string)jsonReader.Value;

                jsonReader.Read();

                var newJsonToken = jsonReader.Deserialize(processObject);
                var existingJsonToken = jsonObject[jsonPropertyName];

                processObject(jsonObject, jsonPropertyName, existingJsonToken, newJsonToken);

                jsonReader.Read();
            }
            return jsonObject;
        }

        #region Methods: Object Processing

        /// <summary> Checks whether the new JSON token is assigned to a unique key (JSON property name). </summary>
        /// <param name="existingJsonToken"> The JSON token looked up with the same key. </param>
        /// <returns></returns>
        private static bool NewTokenHasUniqueKey(JToken existingJsonToken) =>
            existingJsonToken is null;

        /// <summary> Adds the <paramref name="newJsonToken"/> into <paramref name="jsonObject"/> if the former is assigned to a unique key (JSON property name). </summary>
        /// <param name="jsonObject"> The JSON object to add <paramref name="newJsonToken"/> to. </param>
        /// <param name="jsonPropertyName"> The name of the JSON property (token key) to assign <paramref name="newJsonToken"/> to. </param>
        /// <param name="existingJsonToken"> The JSON token looked up with <paramref name="jsonPropertyName"/> (token key). </param>
        /// <param name="newJsonToken"> The new JSON token. </param>
        /// <returns></returns>
        private static bool AddTokenIfUnique(JObject jsonObject, string jsonPropertyName, JToken existingJsonToken, JToken newJsonToken)
        {
            if (NewTokenHasUniqueKey(existingJsonToken))
            {
                jsonObject.Add(new JProperty(jsonPropertyName, newJsonToken));
                return true;
            }
            return false;
        }

        /// <summary> One of the object processor methods that only deserializes an object from <paramref name="newJsonToken"/> and adds it to <paramref name="jsonObject"/>. </summary>
        /// <param name="jsonObject"> The JSON object to add <paramref name="newJsonToken"/> to. </param>
        /// <param name="jsonPropertyName"> The name of the JSON property (token key) to assign <paramref name="newJsonToken"/> to. </param>
        /// <param name="existingJsonToken"> The JSON token looked up with <paramref name="jsonPropertyName"/> (token key). </param>
        /// <param name="newJsonToken"> The new JSON token. </param>
        private static void DeserializeObject(JObject jsonObject, string jsonPropertyName, JToken existingJsonToken, JToken newJsonToken)
        {
            AddTokenIfUnique(jsonObject, jsonPropertyName, existingJsonToken, newJsonToken);
        }

        /// <summary> Handles the specified duplicate JSON token by adding it to a JSON array. </summary>
        /// <param name="existingJsonToken"> The JSON token looked up with the same JSON property name (token key). </param>
        /// <param name="newJsonToken"> The new JSON token. </param>
        private static void HandleDuplicateToken(JToken existingJsonToken, JToken newJsonToken)
        {
            if (existingJsonToken.Type == JTokenType.Array && existingJsonToken is JArray existingJsonArray)
            {
                existingJsonArray.AddIndividually(newJsonToken);
            }
            else if (existingJsonToken.Type == JTokenType.Object)
            {
                var jsonArray = existingJsonToken.ConvertIntoArray();
                jsonArray.AddIndividually(newJsonToken);
            }
        }

        /// <summary>
        /// One of the object processor methods that deserializes a JSON object from <paramref name="newJsonToken"/>
        /// and adds it to <paramref name="jsonObject"/> directly or via a JSON array (if the <paramref name="jsonPropertyName"/> is not unique).
        /// </summary>
        /// <param name="jsonObject"> The JSON object to add <paramref name="newJsonToken"/> to. </param>
        /// <param name="jsonPropertyName"> The name of the JSON property (token key) to assign <paramref name="newJsonToken"/> to. </param>
        /// <param name="existingJsonToken"> The JSON token looked up with <paramref name="jsonPropertyName"/> (token key). </param>
        /// <param name="newJsonToken"> The new JSON token. </param>
        private static void DeserializeObjectAndCombineDuplicatesLoosely(JObject jsonObject, string jsonPropertyName, JToken existingJsonToken, JToken newJsonToken)
        {
            if (!AddTokenIfUnique(jsonObject, jsonPropertyName, existingJsonToken, newJsonToken))
                HandleDuplicateToken(existingJsonToken, newJsonToken);
        }

        #endregion Methods: Object Processing

        #endregion Methods: Object Deserialization

        /// <summary> Deserializes JSON text into a JSON array. </summary>
        /// <param name="jsonReader"> A JSON text reader. </param>
        /// <param name="processObject"> The method to process JSON object tokens. </param>
        /// <returns></returns>
        private static JArray DeserializeArray(this JsonReader jsonReader, Action<JObject, string, JToken, JToken> processObject)
        {
            var jsonArray = new JArray();

            jsonReader.Read();
            while (jsonReader.TokenType != JsonToken.EndArray)
            {
                var newJsonToken = jsonReader.Deserialize(processObject);

                jsonArray.Add(newJsonToken);
                jsonReader.Read();
            }
            return jsonArray;
        }

        #endregion Methods: Deserialization into JToken (based on sample code for resolving duplicate properties by Brian Rogers (https://stackoverflow.com/a/20716106))
    }
}
