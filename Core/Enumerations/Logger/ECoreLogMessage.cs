namespace Core.Enumerations.Logger
{
    /// <summary> Log message strings related to the "<see cref="Core"/>" assembly. </summary>
    public class ECoreLogMessage : EVocabulary
    {
        #region Dispose()

        public const string AlreadyClosed = _Already + _closed + _FS;
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object. </para>
        /// </summary>
        public const string IsNull_DisposalAborted = IsNull + _SPC_Disposal_aborted_FS;
        public const string Closing = _Closing + _FS;

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object. </para>
        /// </summary>
        public const string PreparingToDisposeOf = _PreparingTo + _dispose + _of + _SPC_FMT + _FS;
        public const string AlreadyDisposed = _Already + _disposed + _of + _FS;
        public const string Disposing = _Disposing + _FS;
        public const string SuccessfullyDisposed = _Successfully + _disposed + _of + _FS;

        #endregion Dispose()
        #region Exception Formatter

        public const string WarnExceptionIsNull = _Exception + _isNULL + _FS;

        #endregion Exception Formatter
        #region File Manager

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path / file name / etc. </para>
        /// <para> 2: destination directory path. </para>
        /// </summary>
        public const string Copying = _Copying + _SPC_FMT_Q + _into + _SPC_FMT_Q + _FS;
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path / file name / etc. </para>
        /// <para> 2: destination directory path. </para>
        /// </summary>
        public const string Copied = _FMT_Q + _copied + _FS;
        public const string Overwriting = _Overwriting + _FS;
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path / file name / etc. </para>
        /// </summary>
        public const string Deleting = _Deleting + _SPC_FMT_Q + _FS;
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path. </para>
        /// </summary>
        public const string DeletingEmptyDirectory = _Deleting + _empty + _SPC_FMT_Q + _FS;
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path. </para>
        /// </summary>
        public const string Deleted = _FMT_Q + _deleted + _FS;
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path. </para>
        /// </summary>
        public const string EmptyingDirectory = _Emptying + _SPC_FMT_Q + _FS;
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path. </para>
        /// </summary>
        public const string DirectoryEmptied = _FMT_Q + _has_been + _emptied + _FS;
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path. </para>
        /// </summary>
        public const string SelectingAllFilesFromDirectory = _Selecting + _all + _files + _from + _SPC_FMT_Q + _FS;
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file extensions. </para>
        /// </summary>
        public const string FilteringFilesFromSelection = _Filtering + _SPC_FMT_Q + _files + _from + _selection + _FS;
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file count. </para>
        /// </summary>
        public const string SelectedFileCount = _Selected + _SPC_FMT + _file_s + _FS;
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: file count. </para>
        /// </summary>
        public const string DeletingFiles = _Deleting + _SPC_FMT + _file_s + _FS;
        public const string FileDeleted = _File + _deleted + _FS;
        public const string FilesDeleted = _All + _files + _deleted + _FS;
        public const string CheckingSubdirectories = _Checking + _for + _subdirectories + _FS;
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: subfolder count. </para>
        /// </summary>
        public const string SubdirectoriesFound = _FMT + _subdirectories + _found + _FS;

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: directory / file path. </para>
        /// </summary>
        public const string WarnAlreadyExists_CopyingAborted =
            _FMT_Q + _already + _exists + _FS_SPC
            + _Copying + _safely + _aborted + _FS;
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: directory / file path. </para>
        /// </summary>
        public const string DoesNotExist_NoNeedToDelete = DoesNotExist + _SPC + _No + _need + _to + _delete + _it + _FS;
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: directory / file path. </para>
        /// </summary>
        public const string DoesNotExist_CopyingAborted = DoesNotExist + _SPC + _Copying + _safely_aborted + _FS;
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: destination directory. </para>
        /// <para> 2: directory / file path. </para>
        /// </summary>
        public const string DoesNotExist_CopyingSomethingAborted = DoesNotExist + _SPC + _Copying + _SPC_FMT_Q + _safely_aborted + _FS;
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: directory path. </para>
        /// </summary>
        public const string DirectoryDoesNotExist_DeletingAborted = _TheDirectory_SPC_FMT_Q + _does_not_exist + _FS_SPC + _Deleting + _files + _safely_aborted + _FS;
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory name. </para>
        /// </summary>
        public const string WarnEmptyDirectory = _TheDirectory_SPC_FMT_Q + _is + _empty + _FS;
        public const string WarnNoFilesOfSpecifiedFormatToDelete = _No + _files + _of + _specified + _format + _to + _delete + _FS;
        public const string WarnNoSubdirectories = _No + _subdirectories + _FS;

        public const string ErrorDeletingFile = _AnErrorHasOccuredWhile + _deleting + _file + _FS;
        public const string ErrorDeletingFiles = _AnErrorHasOccuredWhile + _deleting + _one + _of + _the + _files + _FS;

        #endregion File Manager
        #region File Reader

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path / file name / etc. </para>
        /// </summary>
        public const string NotFound = _FMT_Q + _not_found_FS;
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path / file name / etc. </para>
        /// </summary>
        public const string CreatingStreamReader = _Creating + _a + _stream + _reader + _from + _SPC_FMT_Q + _FS;
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path / file name / etc. </para>
        /// </summary>
        public const string Reading = _Reading + _SPC_FMT_Q + _FS;
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: directory path / file name / etc. </para>
        /// </summary>
        public const string CreatedStreamReader = _A + _stream + _reader + _has_been + _created + _from + _SPC_FMT_Q + _FS;
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: character count. </para>
        /// </summary>
        public const string ReadCharacters = _Read + _SPC_FMT + _characters + _FS;

        public const string ErrorReadingFile = _Error + _reading + _a + _file + _FS;

        #endregion File Reader
        #region General

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object. </para>
        /// </summary>
        public const string Creating = _Creating + _SPC_FMT + _FS;
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object. </para>
        /// </summary>
        public const string Creating_InQuotes = _Creating + _SPC_FMT_Q + _FS;
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object. </para>
        /// </summary>
        public const string Created = _FMT + _created + _FS;
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object. </para>
        /// </summary>
        public const string Created_InQuotes = _FMT_Q + _created + _FS;

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object. </para>
        /// </summary>
        public const string IsNull = _FMT + _isNULL + _FS;
        public const string Success = _Successfully + _completed + _FS;
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: method name. </para>
        /// <para> 2: class name. </para>
        /// </summary>
        public const string WarnNullGiven =
            _A + _SPC_NULL + _value + _has + _been + _given + _to + _the + _SPC_FMT_Q + _method + _from + _SPC_FMT_Q + _class + _FS_SPC
            + _Method + _execution + _safely + _cancelled + _FS;

        public const string AnErrorHasOccurred = _AnErrorHasOccured + _FS;
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: directory/file path/name. </para>
        /// </summary>
        public const string DoesNotExist = _FMT_Q + _does_not_exist + _FS;

        #endregion General
        #region Parser

        public const string ReadingClientVersion = _Reading + _the + _client + _version + _FS;
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: client version. </para>
        /// </summary>
        public const string ClientVersionIs = _Client + _version + _is + _SPC_FMT + _FS;

        public const string ErrorVersionNotFoundInSourceString = _Client + _version + _markers + _have + _not + _been + _found + _in + _the + _source + _string + _FS;
        public const string ErrorReadingRawInstallData = _Error + _reading + _raw + _install + _data + _FS;
        public const string ErrorParsingVersionString = _Error + _parsing + _version + _string + _FS;

        #endregion Parser
        #region Settings Manager

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: XML node name. </para>
        /// </summary>
        public const string XmlNodeNotFound = _FMT_Q + _SPC + _XML + _node + _not_found_FS;

        #endregion Settings Manager
        #region Unit Tests

        private const string _LINE = "====================";

        public const string CleanUpAfterUnitTestStartsHere = _LINE + _SPC + _A + _cleanup + _after + _the + _unit + _test + _starts + _here + _FS + _SPC + _LINE;
        public const string CleanUpAfterIntegrationTestStartsHere = _LINE + _SPC + _A + _cleanup + _after + _the + _integration + _test + _starts + _here + _FS + _SPC + _LINE;

        #endregion Unit Tests
        #region Unpacker

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file name. </para>
        /// </summary>
        public const string PreparingToUnpack = _PreparingTo + _unpack + _SPC_FMT_Q + _FS;
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file name. </para>
        /// </summary>
        public const string Unpacking = _Unpacking + _SPC_FMT_Q + _FS;
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file name. </para>
        /// </summary>
        public const string Unpacked = _Unpacked + _SPC_FMT_Q + _FS;
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: unpacking tool file name. </para>
        /// </summary>
        public const string UnpackingToolSelected = _FMT_Q + _selected + _FS;

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file extension. </para>
        /// </summary>
        public const string FileExtensionNotSupportedByUnpackingTools = _No + _unpacking + _tools + _found + _for + _SPC_FMT_Q + _files + _FS;
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file extension. </para>
        /// </summary>
        public const string FileExtensionNotYetSupported = _Output + _path + _generation + _for + _SPC_FMT_Q + _files + _is + _not + _yet + _implemented + _FS;
        public const string ErrorMatchingUnpakingToolToFileExtension = _Error + _matching + _an + _unpacking + _tool + _to + _a + _file + _extension + _FS;
        public const string ErrorRunningUnpackingTool = _Error + _running + _the + _unpacking + _tool + _FS;

        #endregion Unpacker
    }
}