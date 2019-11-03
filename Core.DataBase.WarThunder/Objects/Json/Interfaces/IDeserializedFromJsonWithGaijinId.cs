namespace Core.DataBase.WarThunder.Objects.Json.Interfaces
{
    /// <summary> A base mapping entity used for automated deserialization of JSON data before passing it on into persistent objects. </summary>
    public interface IDeserializedFromJsonWithGaijinId : IDeserializedFromJson
    {
        /// <summary> The entity's Gaijin ID. </summary>
        string GaijinId { get; set; }
    }
}
