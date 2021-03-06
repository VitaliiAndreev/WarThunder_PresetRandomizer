﻿using Core.Enumerations.Logger;

namespace Core.DataBase.Enumerations.Logger
{
    /// <summary> Log message strings related to the "<see cref="DataBase"/>" assembly. </summary>
    public class EDatabaseLogMessage : ECoreLogMessage
    {
        #region DataRepository

        private static readonly string _dataRepository = $"{_data} {_repository}";

        public static readonly string TheInMemoryDataRepository = $"{_The} {_inmemory} {_data} {_repository}.";
        public static readonly string CreatingInMemoryDataRepository = $"{_Creating} {_an} {_inmemory} {_data} {_repository}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: database file name. </para>
        /// </summary>
        public static readonly string TheDataRepositoryFor = $"{_the} {_dataRepository} {_for} \"{{0}}\"";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: database file name. </para>
        /// <para>2: ""/"don't" overwrite. </para>
        /// <para>3: assembly name. </para>
        /// </summary>
        public static readonly string CreatingDataRepository = $"{_Creating} {_a} {_data} {_repository} {_forDatabaseUsingAssembly}.";
        public static readonly string DataRepositoryCreated = $"{_Data} {_repository} {_created}.";

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: persistent object type. </para>
        /// </summary>
        public static readonly string QueryingObjectsWithFilter = $"{_Querying} \"{{0}}\" {_objects} {_with} {_a} {_filter}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: persistent object type. </para>
        /// </summary>
        public static readonly string QueryingAllObjects = $"{_Querying} {_all} \"{{0}}\" {_objects}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: filtered LINQ query. </para>
        /// </summary>
        public static readonly string FilteredQueryIs = $"{_The} {_filtered} {_query}: {{0}}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object. </para>
        /// </summary>
        public static readonly string InstantiatedFromQuery = $"{{0}} {_instantiated} {_from} {_query}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: persistent object count. </para>
        /// </summary>
        public static readonly string QueryReturnedObjects = $"{_The} {_query} {_has} {_returned} {{0}} {_object}({_s}).";

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: new object count. </para>
        /// </summary>
        public static readonly string PersistingNewObjects = $"{_Persisting} {{0}} {_new} {_object}({_s}).";
        public static readonly string AllNewObjectsPersisted = $"{_All} {_new} {_objects} {_persisted}.";

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: name of the collection. </para>
        /// </summary>
        public static readonly string NotAllObjectTypesHaveBeenIncludedInSorting = $"{_Not} {_all} {_object} {_types} {_have} {_been} {_included} {_in} {_sorting} {_of} {_the} \"{{0}}\" {_collection}.";

        #endregion DataRepository
        #region DataRepositoryFactory

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: data repository type. </para>
        /// </summary>
        public static readonly string DataRepositoryTypeNotSupported = $"{_Data} {_repository} {_type} {_not} {_supported}.";

        #endregion DataRepositoryFactory
        #region General

        private static readonly string _forDatabaseUsingAssembly = $"{_for} {_the} \"{{0}}\" {_database} ({{1}}{_overwrite}) {_using} {_mapping} {_from} {_assembly} \"{{2}}\"";

        protected static readonly string _databaseFor = $"{_database} {_for}";

        #endregion General
        #region PersistentObject

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object. </para>
        /// </summary>
        public static readonly string PreparingToCommitChangesTo = $"{_Preparing} {_to} {_commit} {_changes} {_to} {{0}}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: string representation of an object. </para>
        /// </summary>
        public static readonly string CommittingChangesTo = $"{_Committing} {_changes} {_to} {{0}}.";
        public static readonly string ChangesCommitted = $"{_Changes} {_committed}.";

        public static readonly string DataRepositoryClosed_CommittingAborted = $"{_The} {_data} {_repository} {_is} {_closed}. {_Committing} {_aborted}.";

        #endregion PersistentObject
        #region SessionFactory

        private static readonly string _DatabaseFile = $"{_Database} {_file}";
        private static readonly string _intoConfiguration = $"{_into} {_configuration}";
        private static readonly string _ormMappingFromAttributes = $"{_ORM} {_mapping} {_from} {_attributes}";
        private static readonly string _sessionFactory = $"{_session} {_factory}";

        public static readonly string TheSessionFactory = $"{_The} {_sessionFactory}";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: database file name. </para>
        /// </summary>
        public static readonly string TheSessionFactoryFor = $"{_the} {_sessionFactory} {_for} \"{{0}}\"";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: database file name. </para>
        /// <para>2: ""/"don't" overwrite. </para>
        /// <para>3: assembly name. </para>
        /// </summary>
        public static readonly string CreatingSessionFactory = $"{_Creating} {_a} {_sessionFactory} {_forDatabaseUsingAssembly}.";
        public static readonly string SessionFactoryCreated = $"{_Session} {_factory} {_created}.";

        public static readonly string WritingMappingAttributes = $"{_Serialising} {_assembly} {_to} {_read} {_ormMappingFromAttributes} {_and} {_write} {_intoConfiguration}.";
        public static readonly string MappingAttributesWritten = $"{_ormMappingFromAttributes} {_written} {_from} {_serialised} {_assembly} {_intoConfiguration}.";

        public static readonly string CreatingSchema = $"{_Creating} {_a} {_database} {_schema}.";
        public static readonly string DataBaseFileFound = $"{_DatabaseFile} {_found}.";
        public static readonly string ShouldNotOverwrite = $"{_DatabaseFile} {_overwriting} {_disabled}.";
        public static readonly string OverwritingSchema = $"{_Overwriting} {_schema}.";
        public static readonly string CreatingFileWithSchema = $"{_Creating} {_a} {_new} {_database} {_file} {_and} {_a} {_schema} {_for} {_it}.";

        public static readonly string ErrorCreaingSessionFactory = $"{_Error} {_creating} {_a} {_sessionFactory}.";

        #endregion SessionFactory
    }
}