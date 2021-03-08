namespace Core.Enumerations.Logger
{
    /// <summary> Log message strings related to the "<see cref="Core"/>" assembly. </summary>
    public class ECoreLogMessage : EVocabulary
    {
        #region Dispose()

        public static readonly string AlreadyClosed = $"{_Already} {_closed}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object. </para>
        /// </summary>
        public static readonly string IsNull_DisposalAborted = $"{{0}} {_is} {_NULL}. {_Disposal} {_aborted}.";
        public static readonly string Closing = $"{_Closing}.";
        public static readonly string Closed = $"{_Closed}.";

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object. </para>
        /// </summary>
        public static readonly string PreparingToDisposeOf = $"{_Preparing} {_to} {_dispose} {_of} {{0}}.";
        public static readonly string AlreadyDisposed = $"{_Already} {_disposed} {_of}.";
        public static readonly string Disposing = $"{_Disposing}.";
        public static readonly string SuccessfullyDisposed = $"{_Successfully} {_disposed} {_of}.";

        #endregion Dispose()
        #region Exception Formatter

        public static readonly string ExceptionIsNull = $"{_Exception} {_is} {_NULL}.";

        #endregion Exception Formatter
        #region File Manager

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path / file name / etc. </para>
        /// <para> 2: destination directory path. </para>
        /// </summary>
        public static readonly string Copying = $"{_Copying} \"{{0}}\" {_into} \"{{1}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path / file name / etc. </para>
        /// <para> 2: destination directory path. </para>
        /// </summary>
        public static readonly string Copied = $"\"{{0}}\" {_copied}.";
        public static readonly string Overwriting = $"{_Overwriting}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path / file name / etc. </para>
        /// </summary>
        public static readonly string Deleting = $"{_Deleting} \"{{0}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path. </para>
        /// </summary>
        public static readonly string DeletingEmptyDirectory = $"{_Deleting} {_empty} \"{{0}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path. </para>
        /// </summary>
        public static readonly string Deleted = $"\"{{0}}\" {_deleted}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path. </para>
        /// </summary>
        public static readonly string EmptyingDirectory = $"{_Emptying} \"{{0}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path. </para>
        /// </summary>
        public static readonly string DirectoryEmptied = $"{{0}}\" {_emptied}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path. </para>
        /// </summary>
        public static readonly string SelectingAllFilesFromDirectory = $"{_Selecting} {_all} {_files} {_from} \"{{0}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file extensions. </para>
        /// </summary>
        public static readonly string FilteringFilesFromSelection = $"{_Filtering} \"{{0}}\" {_files} {_from} {_selection}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file count. </para>
        /// </summary>
        public static readonly string SelectedFileCount = $"{_Selected} {{0}} {_file}({_s}).";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: file count. </para>
        /// </summary>
        public static readonly string DeletingFiles = $"{_Deleting} {{0}} {_file}({_s}).";
        public static readonly string FileDeleted = $"{_File} {_deleted}.";
        public static readonly string FilesDeleted = $"{_All} {_files} {_deleted}.";
        public static readonly string CheckingSubdirectories = $"{_Checking} {_for} {_subdirectories}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: subfolder count. </para>
        /// </summary>
        public static readonly string SubdirectoriesFound = $"{{0}} {_subdirectories} {_found}.";

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: directory / file path. </para>
        /// </summary>
        public static readonly string AlreadyExists_CopyingSkipped = $"\"{{0}}\" {_already} {_exists}. {_Copying} {_skipped}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: directory / file path. </para>
        /// </summary>
        public static readonly string DoesNotExist_NoNeedToDelete = $"{DoesntExist} {_No} {_need} {_to} {_delete} {_it}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: directory / file path. </para>
        /// </summary>
        public static readonly string DoesNotExist_CopyingAborted = $"{DoesntExist} {_Copying} {_safely} {_aborted}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: destination directory. </para>
        /// <para> 2: directory / file path. </para>
        /// </summary>
        public static readonly string DoesNotExist_CopyingSomethingAborted = $"{DoesntExist} {_Copying} \"{{0}}\" {_safely} {_aborted}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: directory path. </para>
        /// </summary>
        public static readonly string DirectoryDoesNotExist_DeletingAborted = $"{_The} {_directory} \"{{0}}\" {_doesnt} {_exist}. {_Deleting} {_files} {_safely} {_aborted}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory name. </para>
        /// </summary>
        public static readonly string DirectoryIsEmpty = $"{_The} {_directory} \"{{0}}\" {_is} {_empty}.";
        public static readonly string NoFilesOfSpecifiedFormatToDelete = $"{_No} {_files} {_of} {_specified} {_format} {_to} {_delete}.";
        public static readonly string NoSubdirectories = $"{_No} {_subdirectories}.";

        public static readonly string ErrorDeletingFile = $"{_Error} {_deleting} {_file}.";
        public static readonly string ErrorDeletingFiles = $"{_Error} {_deleting} {_one} {_of} {_the} {_files}.";

        #endregion File Manager
        #region File Reader

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path / file name / etc. </para>
        /// </summary>
        public static readonly string NotFound = $"\"{{0}}\" {_not} {_found}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path / file name / etc. </para>
        /// </summary>
        public static readonly string CreatingStreamReader = $"{_Creating} {_a} {_stream} {_reader} {_from} \"{{0}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path / file name / web page / etc. </para>
        /// </summary>
        public static readonly string Reading = $"{_Reading} \"{{0}}\".";
        public static readonly string StreamReaderCreated = $"{_Stream} {_reader} {_created}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path / file name / web page / etc. </para>
        /// </summary>
        public static readonly string FinishedReading = $"{_Finished} {_reading} \"{{0}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: character count. </para>
        /// </summary>
        public static readonly string ReadCharacters = $"{_Read} {{0}} {_characters}.";

        public static readonly string ErrorReadingFile = $"{_Error} {_reading} {_file}.";

        private static readonly string _bytesFromFile = $"{_bytes} {_from} \"{{0}}\"";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file path. </para>
        /// </summary>
        public static readonly string ReadingBytesFromFile = $"{_Reading} {_bytesFromFile}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file path. </para>
        /// </summary>
        public static readonly string ErrorReadingBytesFromFile = $"{_Error} {_reading} {_bytesFromFile}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: byte count. </para>
        /// <para> 2: file path. </para>
        /// </summary>
        public static readonly string ReadBytesFromFile = $"{_Read} {{0}} {_bytesFromFile}.";

        #endregion File Reader
        #region General

        protected static readonly string _isEmpty = $"{_is} {_empty}";
        protected static readonly string _TryingTo = $"{_Trying} {_to}";

        public static readonly string Started = $"{_Started}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: directory / file name. </para>
        /// </summary>
        public static readonly string Creating_InQuotes = $"{_Creating} \"{{0}}\".";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object. </para>
        /// </summary>
        public static readonly string Created = $"{{0}} {_created}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: directory / file name. </para>
        /// </summary>
        public static readonly string Created_InQuotes = $"\"{{0}}\" {_created}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object. </para>
        /// </summary>
        public static readonly string TryingToInitialise = $"{_Trying} {_to} {_initialise} {{0}}.";
        public static readonly string Initialising = $"{_Initialising}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: instance. </para>
        /// </summary>
        public static readonly string InitialisingInstance = $"{_Initialising} {{0}}.";
        /// <summary> For string interpolation see <see cref="InstanceInitialised"/>.</summary>
        public static readonly string Initialised = $"{_Initialised}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: instance(s). </para>
        /// </summary>
        public static readonly string InstanceInitialised = $"{{0}} {_initialised}.";

        #region Not Initialised Properly

        private static readonly string _notInitialisedProperly = $" {_not} {_initialised} {_properly}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object(s). </para>
        /// </summary>
        public static readonly string NotInitialisedProperly = $"{{0}}{_notInitialisedProperly}";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: class member. </para>
        /// </summary>
        public static readonly string MemberNotInitialisedProperly = $"\"{{0}}\"{_notInitialisedProperly}";

        #endregion Not Initialised Properly
        
        public static readonly string Shown = $"{_Shown}.";

        public static readonly string AnErrorHasOccurred = $"{_An} {_error} {_has} {_occured}.";
        public static readonly string FatalErrorShutdown = $"{AnErrorHasOccurred} {_The} {_application} {_will} {_be} {_shut} {_down}.";
        public static readonly string SeeLogsForDetails = $"{_See} {_the} {_latest} {_file} {_in} {_the} \"{_Logs}\" {_folder} {_for} {_details}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: directory/file path/name. </para>
        /// </summary>
        public static readonly string DoesntExist = $"\"{{0}}\" {_doesnt} {_exist}.";
        public static readonly string NothingToSelectFrom = $"{_Nothing} {_to} {_select} {_from}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: objest(s). </para>
        /// </summary>
        public static readonly string Selected = $"\"{{0}}\" {_selected}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: type. </para>
        /// </summary>
        public static readonly string ExplicitImplementationRequiredForType = $"{_Explicit} {_implementation} {_required} {_for} \"{{0}}\" {_type}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: type. </para>
        /// </summary>
        public static readonly string EnumerationHasNoDefaultItem = $"\"{{0}}\" {_enumeration} {_has} {_no} {_default} {_items}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: cast value. </para>
        /// <para> 1: output type. </para>
        /// </summary>
        public static readonly string EnumValueCouldntBeUpcastTo = $"{_Enum} {_value} \"{{0}}\" {_couldnt} {_be} {_upcast} {_to} \"{{0}}\".";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: type. </para>
        /// </summary>
        public static readonly string EnumValueMustBe = $"{_Enum} {_value} {_must} {_be} \"{{0}}\".";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: type. </para>
        /// </summary>
        public static readonly string TypeIsNotEnumeration = $"\"{{0}}\" {_type} {_is} {_not} {_an} {_enumeration}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: generic type. </para>
        /// <para> 2: type parameter. </para>
        /// </summary>
        public static readonly string GenericTypeParameterAndTypeParameterDontMatch = $"{_Generic} {_type} \"{{0}}\" {_doesnt} {_match} {_type} {_argument} \"{{1}}\".";
        public static readonly string ShuttingDown = $"{_Shutting} {_down}.";

        #endregion General
        #region Settings Manager

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: settings file name. </para>
        /// </summary>
        public static readonly string SettingsFileNotFound_CreatingNewOne = $"{_The} {_settings} {_file} (\"{{0}}\") {_not} {_found}. {_Creating} {_a} {_new} {_one}.";

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: XML node name. </para>
        /// </summary>
        public static readonly string XmlNodeNotFound = $"{{0}} {_XML} {_node} {_not} {_found}.";
        public static readonly string SettingsCacheIsEmpty = $"{_The} {_settings} {_cache} {_is} {_empty}.";

        #endregion Settings Manager
        #region Unit Tests

        private const string _line = "====================";

        public static readonly string CleanUpAfterUnitTestStartsHere = _line + $"{_A} {_cleanup} {_after} {_the} {_unit} {_test} {_starts} {_here}. " + _line;
        public static readonly string CleanUpAfterIntegrationTestStartsHere = _line + $"{_A} {_cleanup} {_after} {_the} {_integration} {_test} {_starts} {_here}. " + _line;

        #endregion Unit Tests
    }
}