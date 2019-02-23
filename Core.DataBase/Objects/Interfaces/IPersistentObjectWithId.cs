namespace Core.DataBase.Objects.Interfaces
{
    /// <summary> A persistent (stored in a database) object that has an ID. </summary>
    public interface IPersistentObjectWithId : IPersistentObject
    {
        /// <summary> The object's ID. </summary>
        string Id { get; }
    }
}