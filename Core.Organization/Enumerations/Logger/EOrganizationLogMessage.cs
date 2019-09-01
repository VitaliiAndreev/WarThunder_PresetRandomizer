using Core.DataBase.Enumerations.Logger;

namespace Core.Organization.Enumerations.Logger
{
    /// <summary> Log message strings related to the "<see cref="Organization"/>" assembly. </summary>
    public class EOrganizationLogMessage : EDatabaseLogMessage
    {
        public static readonly string PreparingGameFiles = $"{_Preparing} {_game} {_files}.";
        public static readonly string GameFilesPrepared = $"{_Game} {_files} {_prepared}.";

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: game version. </para>
        /// </summary>
        public static readonly string FoundDatabaseFor = $"{_Found} {_databaseFor} {{0}}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: game version. </para>
        /// </summary>
        public static readonly string NotFoundDatabaseFor = $"{_Not} {_found} {_databaseFor} {{0}}.";

        public static readonly string CreatingDatabase = $"{_Creating} {_database}.";
        public static readonly string DatabaseCreatedConnectionEstablished = $"{_Database} {_created}. {_Connection} {_established}.";
        public static readonly string DataBaseConnectionEstablished = $"{_Database} {_connection} {_established}.";

        public static readonly string InitializingDatabase = $"{_Initializing} {_database}.";
        public static readonly string DatabaseInitialized = $"{_Database} {_initialized}.";

        public static readonly string CachingObjects = $"{_Caching} {_objects}.";
        public static readonly string CachingComplete = $"{_Caching} {_complete}.";
    }
}