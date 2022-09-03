using Core.Json.Enumerations.Logger;
using Newtonsoft.Json.Linq;
using System;

namespace Core.Json.Extensions
{
    /// <summary> Methods extending the <see cref="JToken"/> class. </summary>
    public static class JTokenExtensions
    {
        /// <summary> Converts the JSON token into a JSON array with the token put into it. </summary>
        /// <param name="jsonToken"> A JSON token to convert. </param>
        /// <returns></returns>
        public static JArray ConvertIntoArray(this JToken jsonToken)
        {
            var jsonArray = new JArray();

            if (jsonToken.Parent is JProperty jsonProperty)
            {
                jsonProperty.Value = jsonArray;
                jsonArray.Add(jsonToken);
            }
            else
            {
                throw new NotImplementedException();
            }

            return jsonArray;
        }

        /// <summary> Extracts a value of the specified type from the given JSON token. </summary>
        /// <typeparam name="T"> The type of the value to extract. </typeparam>
        /// <param name="jsonToken"> The token to extract a value from. </param>
        /// <returns></returns>
        public static T GetAsValueOf<T>(this JToken jsonToken)
        {
            if (!(jsonToken is JValue jsonValue))
                throw new ArgumentException(EJsonLogMessage.JsonTokenDoesntContainJasonValue.Format(jsonToken.ToString()));

            if (!(jsonValue is T value))
                throw new ArgumentException(EJsonLogMessage.JsonValueCouldNotBeConverted.Format(jsonValue.ToString(), typeof(T).Name));

            return value;
        }
    }
}