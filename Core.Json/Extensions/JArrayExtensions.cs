using Core.Extensions;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Core.Json.Extensions
{
    /// <summary> Methods extending the <see cref="JArray"/> class. </summary>
    public static class JArrayExtensions
    {
        /// <summary> Adds the specified JSON token into the array. If the token is a JSON array, all its items are added into the source array instead of the collection of the token as a whole. </summary>
        /// <param name="jsonArray"> A source JSON array. </param>
        /// <param name="jsonToken"> The JSON token to add into the source array. </param>
        public static void AddIndividually(this JArray jsonArray, JToken jsonToken)
        {
            if (jsonToken.Type == JTokenType.Array)
            {
                foreach (var jsonChildToken in jsonToken)
                    jsonArray.Add(jsonChildToken);
            }
            else
            {
                jsonArray.Add(jsonToken);
            }
        }

        /// <summary> Checks whether the JSON array has exclusively single-property JSON objects in it that would create duplicate properties if flattenned into a single JSON object. </summary>
        /// <param name="jsonArray"> A source JSON array. </param>
        /// <returns></returns>
        public static bool HasPotentiallyDuplicateProperties(this JArray jsonArray) =>
            jsonArray.All(jsonChildToken => jsonChildToken is JObject jsonChildObject && jsonChildObject.Children().HasSingle());
    }
}
