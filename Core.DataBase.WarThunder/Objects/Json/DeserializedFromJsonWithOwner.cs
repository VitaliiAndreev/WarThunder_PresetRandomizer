namespace Core.DataBase.WarThunder.Objects.Json
{
    public class DeserializedFromJsonWithOwner<T> : DeserializedFromJson
    {
        public T Owner { get; set; }
    }
}