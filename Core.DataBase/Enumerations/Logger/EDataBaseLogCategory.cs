using Core.Enumerations;

namespace Core.DataBase.Enumerations.Logger
{
    /// <summary> Categories of events provided to a logger. </summary>
    public class EDatabaseLogCategory
    {
        public static string DataRepository = $"{EWord.Data} {EWord.Repository}";
        public static string DataRepositoryFactory = $"{DataRepository} {EWord.Factory}";
        public static string SessionFactory = $"{EWord.Session} {EWord.Factory}";
    }
}