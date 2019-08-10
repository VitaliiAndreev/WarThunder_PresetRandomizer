using Core.DataBase.Enumerations.Logger;

namespace Core.Organization.Enumerations.Logger
{
    public class EOrganizationLogMessage : EDatabaseLogMessage
    {
        public static string PreparingGameFiles = $"{_Preparing} {_game} {_files}.";
        public static string GameFilesPrepared = $"{_Game} {_files} {_prepared}.";

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: game version. </para>
        /// </summary>
        public static string FoundDatabaseFor = $"{_Found} {_databaseFor} {{0}}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: game version. </para>
        /// </summary>
        public static string NotFoundDatabaseFor = $"{_Not} {_found} {_databaseFor} {{0}}.";

        public static string CreatingDatabase = $"{_Creating} {_database}.";
        public static string DatabaseCreatedConnectionEstablished = $"{_Database} {_created}. {_Connection} {_established}.";
        public static string DataBaseConnectionEstablished = $"{_Database} {_connection} {_established}.";

        public static string InitializingDatabase = $"{_Initializing} {_database}.";
        public static string DatabaseInitialized = $"{_Database} {_initialized}.";

        public static string CachingObjects = $"{_Caching} {_objects}.";
        public static string CachingComplete = $"{_Caching} {_complete}.";
    }
}