using Core.DataBase.WarThunder.Objects.Json.Interfaces;

namespace Core.DataBase.WarThunder.Objects.Json
{
    public class DeserializedFromJsonWithOwner<T> : DeserializedFromJson, IDeserializedFromJsonWithOwner<T>
    {
        public T Owner { get; set; }
    }
}