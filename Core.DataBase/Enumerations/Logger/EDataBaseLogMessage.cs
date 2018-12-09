using Core.Helpers.Logger.Enumerations;

namespace Core.DataBase.Enumerations.Logger
{
    /// <summary> Log message strings related to the <see cref="DataBase"/> assembly. </summary>
    public class EDataBaseLogMessage : ECoreLogMessage
    {
        #region DataRepository

        /// <summary>
        /// A message with formatting placeholders.
        /// <para>1: database file name.</para>
        /// </summary>
        public const string DataRepositoryFor_noFS = _NOSPC_the + _dataRepository + _for_SPC_FMT_Q;
        /// <summary>
        /// A message with formatting placeholders.
        /// <para>1: database file name.</para>
        /// <para>2: ""/"don't" overwrite.</para>
        /// <para>3: assembly name.</para>
        /// </summary>
        public const string CreatingDataRepository = _CreatingA + _dataRepository + _forDatabaseUsingAssembly + _FS;
        public const string DataRepositoryCreated = _DataRepository + _created + _FS;

        /// <summary>
        /// A message with formatting placeholders.
        /// <para>1: persistent object type.</para>
        /// </summary>
        public const string QueryingObjects = _Querying + _all + _SPC_FMT_Q + _objects + _FS;
        /// <summary>
        /// A message with formatting placeholders.
        /// <para>1: persistent object count.</para>
        /// </summary>
        public const string QueryReturnedObjects = _The + _query + _has + _returned + _SPC_FMT + _object_s + _FS;

        /// <summary>
        /// A message with formatting placeholders.
        /// <para>1: string representation of an object.</para>
        /// </summary>
        public const string CommittingChangesTo = _Committing + _changes + _to + _SPC_FMT_Q + _FS;
        public const string ChangesCommitted = _Changes + _committed + _FS;

        /// <summary>
        /// A message with formatting placeholders.
        /// <para>1: new object count.</para>
        /// </summary>
        public const string PersistingNewObjects = _Persisting + _SPC_FMT + _new + _object_s + _FS;
        public const string AllNewObjectsPersisted = _All + _new + _objects + _persisted + _FS;

        #endregion DataRepository
        #region General

        private const string _forDatabaseUsingAssembly = _for + _the + _SPC_FMT_Q + _database + _SPC + _PARENTHESIS_OPEN + _FMT + _NOSPC_overwrite + _PARENTHESIS_CLOSE
            + _using + _mapping + _from + _theAssembly_FMT_Q;

        #endregion General
        #region PersistentObject

        private const string _CommittingAborted = _Committing + _aborted + _FS;
        /// <summary>
        /// A message with formatting placeholders.
        /// <para>1: object.</para>
        /// </summary>
        public const string PreparingToCommitChangesTo = _PreparingTo + _commit + _changes + _to + _SPC_FMT + _FS;
        public const string NotAssignedToDataRepository_CommittingAborted = _Not + _Assigned + _to + _dataRepository + _FS_SPC + _CommittingAborted;
        public const string DataRepositoryClosed_CommittingAborted = _The + _dataRepository + _is + _closed + _FS_SPC + _CommittingAborted;

        #endregion PersistentObject
        #region SessionFactory

        /// <summary>
        /// A message with formatting placeholders.
        /// <para>1: database file name.</para>
        /// </summary>
        public const string SessionFactoryFor_noFS = _NOSPC_the + _sessionFactory + _for_SPC_FMT_Q;
        /// <summary>
        /// A message with formatting placeholders.
        /// <para>1: database file name.</para>
        /// <para>2: ""/"don't" overwrite.</para>
        /// <para>3: assembly name.</para>
        /// </summary>
        public const string CreatingSessionFactory = _CreatingA + _sessionFactory + _forDatabaseUsingAssembly + _FS;
        public const string SessionFactoryCreated = _SessionFactory + _created + _FS;

        public const string WritingMappingAttributes = _Serializing + _assembly + _to + _read + _SPC_ORM_MappingFromAttributes + _and + _write + _intoConfiguration + _FS;
        public const string MappingAttributesWritten = _ORM_MappingFromAttributes + _written + _from + _serialized + _assembly + _intoConfiguration + _FS;

        public const string CreatingSchema = _CreatingA + _database + _schema + _FS;
        public const string DataBaseFileFound = _DatabaseFile + _found + _FS;
        public const string ShouldNotOverwrite = _DatabaseFile + _overwriting + _disabled + _FS;
        public const string OverwritingSchema = _Overwriting + _schema + _FS;
        public const string CreatingFileWithSchema = _CreatingA + _new + _database + _file + _and + _a + _schema + _for + _it + _FS;
        
        public const string ErrorCreaingSessionFactory = _AnErrorHasOccuredWhile + _creating + _a + _sessionFactory + _FS;

        #endregion SessionFactory
    }
}
