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
    }
}
