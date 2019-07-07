using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Json
{
    /// <summary> A mapping entity used for automated deserialization of JSON data before passing it on into persistent objects. </summary>
    public class RankDeserializedFromJson : DeserializedFromJson
    {
        /// <summary> Playable nations in the game. </summary>
        [JsonProperty("country", Required = Required.Always)]
        public List<NationDeserializedFromJson> Nations { get; set; }
    }
}