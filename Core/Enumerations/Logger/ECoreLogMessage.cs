namespace Core.Enumerations.Logger
{
    /// <summary> Log message strings related to the "<see cref="Core"/>" assembly. </summary>
    public class ECoreLogMessage : EVocabulary
    {
        #region Dispose()

        public static string AlreadyClosed = $"{_Already} {_closed}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object. </para>
        /// </summary>
        public static string IsNull_DisposalAborted = $"{{0}} {_is} {_NULL}. {_Disposal} {_aborted}.";
        public static string Closing = $"{_Closing}.";
        public static string Closed = $"{_Closed}.";

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object. </para>
        /// </summary>
        public static string PreparingToDisposeOf = $"{_Preparing} {_to} {_dispose} {_of} {{0}}.";
        public static string AlreadyDisposed = $"{_Already} {_disposed} {_of}.";
        public static string Disposing = $"{_Disposing}.";
        public static string SuccessfullyDisposed = $"{_Successfully} {_disposed} {_of}.";

        #endregion Dispose()
        #region Exception Formatter

        public static string ExceptionIsNull = $"{_Exception} {_is} {_NULL}.";

        #endregion Exception Formatter
        #region File Manager

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path / file name / etc. </para>
        /// <para> 2: destination directory path. </para>
        /// </summary>
        public static string Copying = $"{_Copying} \"{{0}}\" {_into} \"{{1}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path / file name / etc. </para>
        /// <para> 2: destination directory path. </para>
        /// </summary>
        public static string Copied = $"\"{{0}}\" {_copied}.";
        public static string Overwriting = $"{_Overwriting}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path / file name / etc. </para>
        /// </summary>
        public static string Deleting = $"{_Deleting} \"{{0}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path. </para>
        /// </summary>
        public static string DeletingEmptyDirectory = $"{_Deleting} {_empty} \"{{0}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path. </para>
        /// </summary>
        public static string Deleted = $"\"{{0}}\" {_deleted}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path. </para>
        /// </summary>
        public static string EmptyingDirectory = $"{_Emptying} \"{{0}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path. </para>
        /// </summary>
        public static string DirectoryEmptied = $"{{0}}\" {_emptied}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path. </para>
        /// </summary>
        public static string SelectingAllFilesFromDirectory = $"{_Selecting} {_all} {_files} {_from} \"{{0}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file extensions. </para>
        /// </summary>
        public static string FilteringFilesFromSelection = $"{_Filtering} \"{{0}}\" {_files} {_from} {_selection}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file count. </para>
        /// </summary>
        public static string SelectedFileCount = $"{_Selected} {{0}} {_file}({_s}).";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: file count. </para>
        /// </summary>
        public static string DeletingFiles = $"{_Deleting} {{0}} {_file}({_s}).";
        public static string FileDeleted = $"{_File} {_deleted}.";
        public static string FilesDeleted = $"{_All} {_files} {_deleted}.";
        public static string CheckingSubdirectories = $"{_Checking} {_for} {_subdirectories}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: subfolder count. </para>
        /// </summary>
        public static string SubdirectoriesFound = $"{{0}} {_subdirectories} {_found}.";

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: directory / file path. </para>
        /// </summary>
        public static string AlreadyExists_CopyingSkipped = $"\"{{0}}\" {_already} {_exists}. {_Copying} {_skipped}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: directory / file path. </para>
        /// </summary>
        public static string DoesNotExist_NoNeedToDelete = $"{DoesntExist} {_No} {_need} {_to} {_delete} {_it}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: directory / file path. </para>
        /// </summary>
        public static string DoesNotExist_CopyingAborted = $"{DoesntExist} {_Copying} {_safely} {_aborted}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: destination directory. </para>
        /// <para> 2: directory / file path. </para>
        /// </summary>
        public static string DoesNotExist_CopyingSomethingAborted = $"{DoesntExist} {_Copying} \"{{0}}\" {_safely} {_aborted}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: directory path. </para>
        /// </summary>
        public static string DirectoryDoesNotExist_DeletingAborted = $"{_The} {_directory} \"{{0}}\" {_doesnt} {_exist}. {_Deleting} {_files} {_safely} {_aborted}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory name. </para>
        /// </summary>
        public static string DirectoryIsEmpty = $"{_The} {_directory} \"{{0}}\" {_is} {_empty}.";
        public static string NoFilesOfSpecifiedFormatToDelete = $"{_No} {_files} {_of} {_specified} {_format} {_to} {_delete}.";
        public static string NoSubdirectories = $"{_No} {_subdirectories}.";

        public static string ErrorDeletingFile = $"{_Error} {_deleting} {_file}.";
        public static string ErrorDeletingFiles = $"{_Error} {_deleting} {_one} {_of} {_the} {_files}.";

        #endregion File Manager
        #region File Reader

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path / file name / etc. </para>
        /// </summary>
        public static string NotFound = $"\"{{0}}\" {_not} {_found}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path / file name / etc. </para>
        /// </summary>
        public static string CreatingStreamReader = $"{_Creating} {_a} {_stream} {_reader} {_from} \"{{0}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path / file name / etc. </para>
        /// </summary>
        public static string Reading = $"{_Reading} \"{{0}}\".";
        public static string StreamReaderCreated = $"{_Stream} {_reader} {_created}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: character count. </para>
        /// </summary>
        public static string ReadCharacters = $"{_Read} {{0}} {_characters}.";

        public static string ErrorReadingFile = $"{_Error} {_reading} {_file}.";

        #endregion File Reader
        #region General

        protected static readonly string _TryingTo = $"{_Trying} {_to}";

        public static string Started = $"{_Started}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: directory / file name. </para>
        /// </summary>
        public static string Creating_InQuotes = $"{_Creating} \"{{0}}\".";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object. </para>
        /// </summary>
        public static string Created = $"{{0}} {_created}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: directory / file name. </para>
        /// </summary>
        public static string Created_InQuotes = $"\"{{0}}\" {_created}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object. </para>
        /// </summary>
        public static string TryingToInitialize = $"{_Trying} {_to} {_initialize} {{0}}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object. </para>
        /// </summary>
        public static string Initializing = $"{_Initializing} {{0}}.";
        public static string Initialized = $"{_Initialized}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object(s). </para>
        /// </summary>
        public static string ObjectInitialized = $"{{0}} {_initialized}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object(s). </para>
        /// </summary>
        public static string NotInitializedProperly = $"{{0}} {_not} {_initialized} {_properly}.";
        public static string Shown = $"{_Shown}.";
        
        public static string AnErrorHasOccurred = $"{_An} {_error} {_has} {_occured}.";
        public static string FatalErrorShutdown = $"{AnErrorHasOccurred} {_The} {_application} {_will} {_be} {_shut} {_down}.";
        public static string SeeLogsForDetails = $"{_See} {_the} {_latest} {_file} {_in} {_the} \"{_Logs}\" {_folder} {_for} {_details}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: directory/file path/name. </para>
        /// </summary>
        public static string DoesntExist = $"\"{{0}}\" {_doesnt} {_exist}.";
        public static string ShuttingDown = $"{_Shutting} {_down}.";

        #endregion General
        #region Parser

        public static string ReadingClientVersion = $"{_Reading} {_the} {_client} {_version}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: client version. </para>
        /// </summary>
        public static string ClientVersionIs = $"{_Client} {_version} {_is} {{0}}.";

        public static string VersionNotFoundInSourceString = $"{_Client} {_version} {_markers} {_not} {_found} {_in} {_the} {_source} {_string}.";
        public static string ErrorReadingRawInstallData = $"{_Error} {_reading} {_raw} {_install} {_data}.";
        public static string ErrorParsingVersionString = $"{_Error} {_parsing} {_version} {_string}.";

        #endregion Parser
        #region Settings Manager

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: settings file name. </para>
        /// </summary>
        public static string SettingsFileNotFound_CreatingNewOne = $"{_The} {_settings} {_file} (\"{{0}}\") {_not} {_found}. {_Creating} {_a} {_new} {_one}.";

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: XML node name. </para>
        /// </summary>
        public static string XmlNodeNotFound = $"{{0}} {_XML} {_node} {_not} {_found}.";
        public static string SettingsCacheIsEmpty = $"{_The} {_settings} {_cache} {_is} {_empty}.";

        #endregion Settings Manager
        #region Unit Tests

        private const string _line = "====================";

        public static string CleanUpAfterUnitTestStartsHere = _line + $"{_A} {_cleanup} {_after} {_the} {_unit} {_test} {_starts} {_here}. " + _line;
        public static string CleanUpAfterIntegrationTestStartsHere = _line + $"{_A} {_cleanup} {_after} {_the} {_integration} {_test} {_starts} {_here}. " + _line;

        #endregion Unit Tests
        #region Unpacker

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file name. </para>
        /// </summary>
        public static string PreparingToUnpack = $"{_Preparing} {_to} {_unpack} \"{{0}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file name. </para>
        /// </summary>
        public static string Unpacking = $"{_Unpacking} \"{{0}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file name. </para>
        /// </summary>
        public static string Unpacked = $"{_Unpacked} \"{{0}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: unpacking tool file name. </para>
        /// </summary>
        public static string UnpackingToolSelected = $"{{0}} {_selected}.";

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file extension. </para>
        /// </summary>
        public static string FileExtensionNotSupportedByUnpackingTools = $"{_No} {_unpacking} {_tools} {_found} {_for} \"{{0}}\" {_files}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file extension. </para>
        /// </summary>
        public static string OutputPathGenerationForFileExtensionNotYetImplemented = $"{_Output} {_path} {_generation} {_for} \"{{0}}\" {_files} {_is} {_not} {_yet} {_implemented}.";
        public static string ErrorMatchingUnpakingToolToFileExtension = $"{_Error} {_matching} {_an} {_unpacking} {_tool} {_to} {_a} {_file} {_extension}.";
        public static string ErrorRunningUnpackingTool = $"{_Error} {_running} {_the} {_unpacking} {_tool}.";

        #endregion Unpacker
    }
}