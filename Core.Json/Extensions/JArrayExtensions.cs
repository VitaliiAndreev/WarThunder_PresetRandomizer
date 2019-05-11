using Newtonsoft.Json.Linq;

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
                foreach (var jsonChildToken in jsonToken.Children())
                    jsonArray.Add(jsonChildToken);
            }
            else
            {
                jsonArray.Add(jsonToken);
            }
        }
    }
}
