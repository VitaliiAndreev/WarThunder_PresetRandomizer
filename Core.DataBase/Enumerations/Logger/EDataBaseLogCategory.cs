namespace Core.DataBase.Enumerations.Logger
{
    /// <summary> Categories of events provided to a logger. </summary>
    public class EDatabaseLogCategory
    {
        public const string DataRepository = nameof(DataRepository);
        public const string SessionFactory = nameof(SessionFactory);
    }
}