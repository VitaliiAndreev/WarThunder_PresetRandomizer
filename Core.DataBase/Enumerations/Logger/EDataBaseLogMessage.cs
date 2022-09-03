namespace Core.DataBase.Enumerations.Logger
{
    public class EDatabaseLogMessage
    {
        #region DataRepository

        public static readonly string CreatingInMemoryDataRepository = $"Creating an inmemory data repository.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: database file name. </para>
        /// <para>2: ""/"don't" overwrite. </para>
        /// <para>3: assembly name. </para>
        /// </summary>
        public static readonly string CreatingDataRepository = $"Creating a data repository for the \"{{0}}\" database ({{1}}overwrite) using mapping from assembly \"{{2}}\".";
        public static readonly string DataRepositoryCreated = $"Data repository created.";

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: persistent object type. </para>
        /// </summary>
        public static readonly string QueryingObjectsWithFilter = $"Querying \"{{0}}\" objects with a filter.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: persistent object type. </para>
        /// </summary>
        public static readonly string QueryingAllObjects = $"Querying all \"{{0}}\" objects.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: filtered LINQ query. </para>
        /// </summary>
        public static readonly string FilteredQueryIs = $"The filtered query: {{0}}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object. </para>
        /// </summary>
        public static readonly string InstantiatedFromQuery = $"{{0}} instantiated from query.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: persistent object count. </para>
        /// </summary>
        public static readonly string QueryReturnedObjects = $"The query has returned {{0}} object(s).";

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: new object count. </para>
        /// </summary>
        public static readonly string PersistingNewObjects = $"Persisting {{0}} new object(s).";
        public static readonly string AllNewObjectsPersisted = $"All new objects persisted.";

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: name of the collection. </para>
        /// </summary>
        public static readonly string NotAllObjectTypesHaveBeenIncludedInSorting = $"Not all object types have been included in sorting of the \"{{0}}\" collection.";

        #endregion DataRepository
        #region DataRepositoryFactory

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: data repository type. </para>
        /// </summary>
        public static readonly string DataRepositoryTypeNotSupported = $"Data repository type not supported.";

        #endregion DataRepositoryFactory
        #region PersistentObject

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object. </para>
        /// </summary>
        public static readonly string PreparingToCommitChangesTo = $"Preparing to commit changes to {{0}}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: string representation of an object. </para>
        /// </summary>
        public static readonly string CommittingChangesTo = $"Committing changes to {{0}}.";
        public static readonly string ChangesCommitted = $"Changes committed.";

        public static readonly string DataRepositoryClosed_CommittingAborted = $"The data repository is closed. Committing aborted.";

        #endregion PersistentObject
        #region SessionFactory
        
        public static readonly string TheSessionFactory = $"The session factory";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: database file name. </para>
        /// <para>2: ""/"don't" overwrite. </para>
        /// <para>3: assembly name. </para>
        /// </summary>
        public static readonly string CreatingSessionFactory = $"Creating a sessionFactory for the \"{{0}}\" database ({{1}}overwrite) using mapping from assembly \"{{2}}\".";
        public static readonly string SessionFactoryCreated = $"Session factory created.";

        public static readonly string WritingMappingAttributes = $"Serialising assembly to read ORM mapping from attributes and write into configuration.";
        public static readonly string MappingAttributesWritten = $"ORM mapping from attributes written from serialised assembly into configuration.";

        public static readonly string CreatingSchema = $"Creating a database schema.";
        public static readonly string DataBaseFileFound = $"Database file found.";
        public static readonly string ShouldNotOverwrite = $"Database file overwriting disabled.";
        public static readonly string OverwritingSchema = $"Overwriting schema.";
        public static readonly string CreatingFileWithSchema = $"Creating a new database file and a schema for it.";

        public static readonly string ErrorCreaingSessionFactory = $"Error creating a session factory.";

        #endregion SessionFactory
    }
}