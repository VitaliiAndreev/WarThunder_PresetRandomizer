using Newtonsoft.Json;

namespace Core.Json.Extensions
{
    /// <summary> Methods extending the <see cref="JsonReader"/> class. </summary>
    public static class JsonReaderExtensions
    {
        /// <summary> Reads through JSON text until an actual JSON token is found. </summary>
        /// <param name="jsonReader"> A JSON text reader. </param>
        public static void ReadPastNonTokens(this JsonReader jsonReader)
        {
            while (jsonReader.TokenType == JsonToken.None)
                jsonReader.Read();
        }
    }
}
