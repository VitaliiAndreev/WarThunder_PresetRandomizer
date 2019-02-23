namespace Core.DataBase.Objects.Interfaces
{
    /// <summary> A persistent (stored in a database) object that has an ID and a name. </summary>
    public interface IPersistentObjectWithIdAndName : IPersistentObjectWithId
    {
        /// <summary> The object's name. </summary>
        string Name { get; }
    }
}
