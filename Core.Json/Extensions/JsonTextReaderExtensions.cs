using Newtonsoft.Json;

namespace Core.Json.Extensions
{
    /// <summary> Methods extending the <see cref="JsonTextReader"/> class. </summary>
    public static class JsonTextReaderExtensions
    {
        /// <summary> Reads through JSON text until an actual JSON token is found. </summary>
        /// <param name="jsonTextReader"> A JSON text reader. </param>
        public static void ReadPastNonTokens(this JsonTextReader jsonTextReader)
        {
            while (jsonTextReader.TokenType == JsonToken.None)
                jsonTextReader.Read();
        }
    }
}
