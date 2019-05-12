using Core.Extensions;
using Newtonsoft.Json;

namespace Core.Json.Extensions
{
    /// <summary> Methods extending the <see cref="string"/> class. </summary>
    public static class StringExtensions
    {
        /// <summary> Creates a new JSON reader with the given string. </summary>
        /// <param name="sourceString"> A source string. </param>
        /// <returns></returns>
        public static JsonReader CreateJsonReader(this string sourceString) =>
            new JsonTextReader(sourceString.CreateTextReader());
    }
}
