using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Core.Json.Extensions
{
    /// <summary> Methods extending the <see cref="JObject"/> class. </summary>
    public static class JObjectExtensions
    {
        /// <summary> Returns a collection of the JSON object's property names. It is equivalent to <see cref="IDictionary{TKey, TValue}.Keys"/> </summary>
        /// <param name="jsonObject"> A source JSON object. </param>
        /// <returns></returns>
        public static IEnumerable<string> GetPropertyNames(this JObject jsonObject) =>
            jsonObject.Children<JProperty>().Select(jsonProperty => jsonProperty.Name);
    }
}
