using Core.Enumerations.Logger;

namespace Core.Organization.Enumerations.Logger
{
    public class EOrganizationLogMessage : ECoreLogMessage
    {
        public const string PreparingGameFiles = _Preparing + _game + _files + _FS;
        public const string GameFilesPrepared = _Game + _files + _prepared + _FS;

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: game version. </para>
        /// </summary>
        public const string FoundDatabaseFor = _Found + _database + _for + _SPC_FMT + _FS;
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: game version. </para>
        /// </summary>
        public const string NotFoundDatabaseFor = _Not + _found + _database + _for + _SPC_FMT + _FS;

        public const string CreatingDatabase = _Creating + _database + _FS;
        public const string DatabaseCreatedConnectionEstablished = _Database + _created + _FS_SPC + _Connection + _established + _FS;
        public const string DataBaseConnectionEstablished = _Database + _connection + _established + _FS;

        public const string InitializingDatabase = _Initializing + _database + _FS;
        public const string DatabaseInitialized = _Database + _initialized + _FS;

        public const string CachingObjects = _Caching + _objects + _FS;
        public const string CachingComplete = _Caching + _complete + _FS;
    }
}