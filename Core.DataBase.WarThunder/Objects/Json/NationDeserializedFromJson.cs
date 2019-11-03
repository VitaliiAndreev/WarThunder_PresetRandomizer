using Newtonsoft.Json;

namespace Core.DataBase.WarThunder.Objects.Json
{
    /// <summary> A mapping entity used for automated deserialization of JSON data before passing it on into persistent objects. </summary>
    public class NationDeserializedFromJson : DeserializedFromJsonWithGaijinId
    {
        /// <summary> The nation's Gaijin ID. </summary>
        [JsonProperty("c", Required = Required.Always)]
        public override string GaijinId { get; set; }
    }
}