namespace Core.Helpers.Logger.Enumerations
{
    /// <summary> Log message strings related to the <see cref="Core"/> assembly. </summary>
    public class ECoreLogMessage
    {
        #region Language

        #region Characters

        /// <summary> A full stop. </summary>
        protected const string _FS = ".";
        /// <summary> A full stop followed by a space. </summary>
        protected const string _FS_SPC = ". ";
        /// <summary> A format placeholder. </summary>
        protected const string _FMT = "{0}";
        /// <summary> A format placeholder inside quotation marks. </summary>
        protected const string _FMT_Q = "\"" + _FMT + "\"";
        /// <summary> A space. </summary>
        protected const string _SPC = " ";
        /// <summary> A format placeholder preceeded by a space. </summary>
        protected const string _SPC_FMT = _SPC + _FMT;
        /// <summary> A format placeholder inside quotation marks preceeded by a space. </summary>
        protected const string _SPC_FMT_Q = _SPC + _FMT_Q;
        /// <summary> An opening parenthesis. </summary>
        protected const string _PARENTHESIS_OPEN = "(";
        /// <summary> A closing parenthesis. </summary>
        protected const string _PARENTHESIS_CLOSE = ")";

        #endregion Characters
        #region Words

        #region A

        protected const string _A = "A";
        protected const string _a = " a";
        protected const string _aborted = _a + "borted";
        protected const string _after = _a + "fter";
        protected const string _All = _A + "ll";
        protected const string _all = _a + "ll";
        protected const string _Already = _A + "lready";
        protected const string _already = _a + "lready";
        protected const string _An = _A + "n";
        protected const string _AnErrorHasOccured = _An + _error + _has + _occured;
        protected const string _AnErrorHasOccuredWhile = _AnErrorHasOccured + _while;
        protected const string _and = _a + "nd";
        protected const string _assembly = _a + "ssembly";
        protected const string _Assigned = _A + "ssigned";
        protected const string _attributes = _a + "ttributes";

        #endregion A
        #region B

        protected const string _been = " been";

        #endregion B
        #region C

        protected const string _cancelled = " cancelled";
        protected const string _Changes = "Changes";
        protected const string _changes = " changes";
        protected const string _Checking = "Checking";
        protected const string _class = " class";
        protected const string _cleanup = " clean-up";
        protected const string _closed = " closed";
        protected const string _Closing = "Closing";
        protected const string _commit = " commit";
        protected const string _committed = _commit + "ted";
        protected const string _Committing = "Committing";
        protected const string _completed = " completed";
        protected const string _configuration = " configuration";
        protected const string _Copied = "Copied";
        protected const string _copied = " copied";
        protected const string _Copying = "Copying";
        protected const string _created = " created";
        protected const string _Creating = "Creating";
        protected const string _CreatingA = _Creating + _a;
        protected const string _creating = " creating";

        #endregion C
        #region D

        protected const string _NOSPC_dont = "don't";

        protected const string _Data = "Data";
        protected const string _data = " data";
        protected const string _Database = "Database";
        protected const string _database = _data + "base";
        protected const string _DatabaseFile = _Database + _file;
        protected const string _DataRepository = _Data + _repository;
        protected const string _dataRepository = _data + _repository;
        protected const string _delete = " delete";
        protected const string _deleted = _delete + "d";
        protected const string _Deleting = "Deleting";
        protected const string _deleting = " deleting";
        protected const string _directory = " directory";
        protected const string _disabled = " disabled";
        protected const string _Disposal = "Disposal";
        protected const string _Disposal_aborted_FS = _Disposal + _aborted + _FS;
        protected const string _dispose = " dispose";
        protected const string _disposed = _dispose + "d";
        protected const string _Disposing = "Disposing";
        protected const string _does = " does";

        protected const string _SPC_Disposal_aborted_FS = _SPC + _Disposal_aborted_FS;

        public const string W_dont_SPC = _NOSPC_dont + _SPC;

        #endregion D
        #region E

        protected const string _emptied = " emptied";
        protected const string _empty = " empty";
        protected const string _Emptying = "Emptying";
        protected const string _error = " error";
        protected const string _Exception = "Exception";
        protected const string _execution = " execution";
        protected const string _exist = " exist";
        protected const string _exists = _exist + "s";

        #endregion E
        #region F

        protected const string _Factory = "Factory";
        protected const string _factory = " factory";
        protected const string _File = "File";
        protected const string _file = " file";
        protected const string _file_s = _file + "(s)";
        protected const string _files = _file + "s";
        protected const string _Filtering = "Filtering";
        protected const string _for = " for";
        protected const string _for_SPC_FMT_Q = _for + _SPC_FMT_Q;
        protected const string _format = _for + "mat";
        protected const string _formats = _format + "s";
        protected const string _found = " found";
        protected const string _from = " from";

        protected const string _SPC_Factory = _SPC + _Factory;

        #endregion F
        #region G

        protected const string _given = " given";

        #endregion G
        #region H

        protected const string _has = " has";
        protected const string _has_been = _has + _been;
        protected const string _here = " here";

        #endregion H
        #region I

        protected const string _instantiated = " instantiated";
        protected const string _into = " into";
        protected const string _intoConfiguration = _into + _configuration;
        protected const string _is = " is";
        protected const string _isNULL = _is + _SPC_NULL;
        protected const string _it = " it";

        #endregion I
        #region J

        #endregion J
        #region K

        #endregion K
        #region L

        #endregion L
        #region M

        protected const string _mapping = " mapping";
        protected const string _Method = "Method";
        protected const string _method = " method";

        #endregion M
        #region N

        protected const string _need = " need";
        protected const string _new = " new";
        protected const string _No = "No";
        protected const string _no = " no";
        protected const string _Not = _No + "t";
        protected const string _not = _no + "t";

        protected const string _SPC_NULL = _SPC + NULL;

        public const string NULL = "NULL";

        #endregion N
        #region O

        protected const string _NOSPC_overwrite = "overwrite";

        protected const string _object = " object";
        protected const string _object_s = _object + "(s)";
        protected const string _objects = _object + "s";
        protected const string _occured = " occurred";
        protected const string _of = " of";
        protected const string _one = " one";
        protected const string _ORM = "ORM";
        protected const string _ORM_MappingFromAttributes = _ORM + _mapping + _from + _attributes;
        protected const string _Overwriting = "Overwriting";
        protected const string _overwriting = " overwriting";

        protected const string _SPC_ORM = _SPC + _ORM;
        protected const string _SPC_ORM_MappingFromAttributes = _SPC + _ORM_MappingFromAttributes;

        #endregion O
        #region P

        protected const string _persisted = " persisted";
        protected const string _Persisting = "Persisting";
        protected const string _Preparing = "Preparing";
        protected const string _PreparingTo = _Preparing + _to;

        #endregion P
        #region Q

        protected const string _queried = " queried";
        protected const string _query = " query";
        protected const string _Querying = "Querying";

        #endregion Q
        #region R

        protected const string _read = " read";
        protected const string _repository = " repository";
        protected const string _returned = " returned";

        #endregion R
        #region S

        protected const string _safely = " safely";
        protected const string _Schema = "Schema";
        protected const string _schema = " schema";
        protected const string _Selected = "Selected";
        protected const string _Selecting = "Selecting";
        protected const string _selection = " selection";
        protected const string _serialized = " serialized";
        protected const string _Serializing = "Serializing";
        protected const string _Session = "Session";
        protected const string _session = " session";
        protected const string _SessionFactory = _Session + _factory;
        protected const string _sessionFactory = _session + _factory;
        protected const string _specified = " specified";
        protected const string _starts = " starts";
        protected const string _subdirectories = " subdirectories";
        protected const string _subfolders = " subfolders";
        protected const string _Successfully = "Successfully";

        public const string W_Schema = _Schema;
        public const string W_TheSessionFactory = _The + _sessionFactory;

        #endregion S
        #region T

        protected const string _NOSPC_the = "the";

        protected const string _test = " test";
        protected const string _The = "The";
        protected const string _TheDirectory_SPC_FMT_Q = _The + _directory + _SPC_FMT_Q;
        protected const string _the = _SPC + _NOSPC_the;
        protected const string _There = _The + "re";
        protected const string _there = _the + "re";
        protected const string _theAssembly_FMT_Q = _the + _assembly + _SPC_FMT_Q;
        protected const string _to = " to";
        protected const string _Trying = "Trying";
        protected const string _TryingTo = _Trying + _to;

        #endregion T
        #region U

        protected const string _unit = " unit";
        protected const string _using = " using";

        #endregion U
        #region V

        protected const string _value = " value";

        #endregion V
        #region W

        protected const string _while = " while";
        protected const string _write = " write";
        protected const string _written = " written";

        #endregion W
        #region X

        #endregion X
        #region Y

        #endregion Y
        #region Z

        #endregion Z

        #endregion Words

        #endregion Language
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
        public const string Deleted = _FMT_Q + _has_been + _deleted + _FS;
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
        /// <para> 1: directory path. </para>
        /// </summary>
        public const string WarnAlreadyDeleted = _FMT_Q + _does + _not + _exist + _FS_SPC + _There + _is + _no + _need + _to + _delete + _it + _FS;
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: directory / file path. </para>
        /// </summary>
        public const string WarnDoestExist_CopyingAborted =
            _FMT_Q + _does + _not + _exist + _FS_SPC
            + _Copying + _safely + _aborted + _FS;
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: destination directory. </para>
        /// <para> 2: directory / file path. </para>
        /// </summary>
        public const string WarnDoestExist_CopyingSomethingAborted =
            _FMT_Q + _does + _not + _exist + _FS_SPC
            + _Copying + _SPC_FMT_Q + _safely + _aborted + _FS;
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: directory path. </para>
        /// </summary>
        public const string WarnDirectoryDoestExist_DeletingAborted =
            _TheDirectory_SPC_FMT_Q + _does + _not + _exist + _FS_SPC
            + _Deleting + _files + _safely + _aborted + _FS;
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
        public const string Created = _FMT + _created + _FS;

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object. </para>
        /// </summary>
        public const string IsNull = _FMT + _isNULL + _FS;
        public const string Success = _Successfully + _completed + _FS;
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: method name. </para>
        /// <para>2: class name. </para>
        /// </summary>
        public const string WarnNullGiven =
            _A + _SPC_NULL + _value + _has + _been + _given + _to + _the + _SPC_FMT_Q + _method + _from + _SPC_FMT_Q + _class + _FS_SPC
            + _Method + _execution + _safely + _cancelled + _FS;

        public const string Error = _AnErrorHasOccured + _FS;

        #endregion General
        #region Unit Tests

        private const string _LINE = "====================";

        public const string CleanUpAfterUnitTestStartsHere = _LINE + _SPC + _A + _cleanup + _after + _the + _unit + _test + _starts + _here + _FS + _SPC + _LINE;

        #endregion Unit Tests
    }
}