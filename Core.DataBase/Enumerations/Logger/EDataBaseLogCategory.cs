namespace Core.DataBase.Enumerations.Logger
{
    /// <summary> Categories of events provided to a logger. </summary>
    public class EDatabaseLogCategory
    {
        public static string DataRepository = $"{Word.Data} {Word.Repository}";
        public static string DataRepositoryFactory = $"{DataRepository} {Word.Factory}";
        public static string SessionFactory = $"{Word.Session} {Word.Factory}";
    }
}