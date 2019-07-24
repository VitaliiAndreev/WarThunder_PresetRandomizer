﻿namespace Core.Enumerations.Logger
{
    /// <summary> Log message strings related to the "<see cref="Core"/>" assembly. </summary>
    public class ECoreLogMessage
    {
        #region Language

        #region Characters

        /// <summary> A comma. </summary>
        protected const string _COMMA = ",";
        /// <summary> A full stop. </summary>
        protected const string _FS = ".";
        /// <summary> A full stop followed by a space. </summary>
        protected const string _FS_SPC = ". ";
        /// <summary> A format placeholder. </summary>
        protected const string _FMT = "{0}";
        /// <summary> A format placeholder inside quotation marks. </summary>
        protected const string _FMT_Q = "\"" + _FMT + "\"";
        /// <summary> A minus. </summary>
        protected const string _MINUS = "-";
        /// <summary> A more than character. </summary>
        protected const string _MORE = ">";
        /// <summary> A space. </summary>
        protected const string _SPC = " ";
        /// <summary> A format placeholder preceeded by a space. </summary>
        protected const string _SPC_FMT = _SPC + _FMT;
        /// <summary> A format placeholder inside quotation marks preceeded by a space. </summary>
        protected const string _SPC_FMT_Q = _SPC + _FMT_Q;
        /// <summary> An opening parenthesis with a preceeding space. </summary>
        protected const string _SPC_PARENTHESIS_OPEN = " (";
        /// <summary> An opening parenthesis. </summary>
        protected const string _PARENTHESIS_OPEN = "(";
        /// <summary> A closing parenthesis. </summary>
        protected const string _PARENTHESIS_CLOSE = ")";

        #endregion Characters
        #region Numbers

        protected const string _4 = "4";

        #endregion Numbers
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
        protected const string _an = _a + "n";
        protected const string _AnErrorHasOccured = _An + _error + _has + _occured;
        protected const string _AnErrorHasOccuredWhile = _AnErrorHasOccured + _while;
        protected const string _and = _an + "n";
        protected const string _any = _an + "y";
        protected const string _arguments = _a + "rguments";
        protected const string _assembly = _a + "ssembly";
        protected const string _Assigned = _A + "ssigned";
        protected const string _attributes = _a + "ttributes";

        #endregion A
        #region B

        protected const string _battle = " battle";
        protected const string _be = " be";
        protected const string _been = _be + "en";
        protected const string _BR = "BR";
        protected const string _branch = " branch";
        protected const string _by = " by";

        #endregion B
        #region C

        protected const string _Caching = "Caching";
        protected const string _cancelled = " cancelled";
        protected const string _Changes = "Changes";
        protected const string _changes = " changes";
        protected const string _characters = " characters";
        protected const string _Checking = "Checking";
        protected const string _class = " class";
        protected const string _cleanup = " clean-up";
        protected const string _Client = "Client";
        protected const string _client = " client";
        protected const string _closed = " closed";
        protected const string _Closing = "Closing";
        protected const string _collection = " collection";
        protected const string _commit = " commit";
        protected const string _committed = _commit + "ted";
        protected const string _Committing = "Committing";
        protected const string _complete = " complete";
        protected const string _completed = _complete + "d";
        protected const string _configuration = " configuration";
        protected const string _Connection = "Connection";
        protected const string _connection = " connection";
        protected const string _container = " container";
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
        protected const string _deserialize = " deserialize";
        protected const string _deserialized = _deserialize + "d";
        protected const string _deserializing = " deserializing";
        protected const string _directory = " directory";
        protected const string _disabled = " disabled";
        protected const string _Disposal = "Disposal";
        protected const string _Disposal_aborted_FS = _Disposal + _aborted + _FS;
        protected const string _dispose = " dispose";
        protected const string _disposed = _dispose + "d";
        protected const string _Disposing = "Disposing";
        protected const string _does = " does";
        protected const string _does_not_exist = " does" + _not + _exist;

        protected const string _SPC_Disposal_aborted_FS = _SPC + _Disposal_aborted_FS;

        public const string _NOSPC_dont_SPC = _NOSPC_dont + _SPC;

        #endregion D
        #region E

        protected const string _emptied = " emptied";
        protected const string _empty = " empty";
        protected const string _Emptying = "Emptying";
        protected const string _Error = "Error";
        protected const string _error = " error";
        protected const string _established = " established";
        protected const string _Exception = "Exception";
        protected const string _execution = " execution";
        protected const string _exist = " exist";
        protected const string _exists = _exist + "s";
        protected const string _exit = " exit";
        protected const string _extension = " extension";

        #endregion E
        #region F

        protected const string _Factory = "Factory";
        protected const string _factory = " factory";
        protected const string _File = "File";
        protected const string _file = " file";
        protected const string _file_s = _file + "(s)";
        protected const string _files = _file + "s";
        protected const string _Filtering = "Filtering";
        protected const string _filter = " filter";
        protected const string _filtered = _filter + "ed";
        protected const string _for = " for";
        protected const string _for_SPC_FMT_Q = _for + _SPC_FMT_Q;
        protected const string _format = _for + "mat";
        protected const string _formats = _format + "s";
        protected const string _Found = "Found";
        protected const string _found = " found";
        protected const string _from = " from";

        protected const string _SPC_Factory = _SPC + _Factory;

        #endregion F
        #region G

        protected const string _Game = "Game";
        protected const string _game = _SPC + _NOSPC_game;
        protected const string _generation = " generation";
        protected const string _given = " given";

        protected const string _NOSPC_game = "game";

        #endregion G
        #region H

        protected const string _has = " has";
        protected const string _has_been = _has + _been;
        protected const string _have = " have";
        protected const string _here = " here";

        #endregion H
        #region I

        protected const string _implemented = " implemented";
        protected const string _in = " in";
        protected const string _Incorrect = "Incorrect";
        protected const string _initialized = _in + "itialized";
        protected const string _Initializing = "Initializing";
        protected const string _input = _in + "put";
        protected const string _install = _in + "stall";
        protected const string _instance = _in + "stance";
        protected const string _instances = _instance + "s";
        protected const string _instantiated = _in + "stantiated";
        protected const string _integration = _in + "tegration";
        protected const string _into = _in + "to";
        protected const string _intoConfiguration = _into + _configuration;
        protected const string _is = " is";
        protected const string _isNULL = _is + _SPC_NULL;
        protected const string _it = " it";

        #endregion I
        #region J

        protected const string _JSON = " JSON";

        #endregion J
        #region K

        protected const string _key = " key";

        #endregion K
        #region L

        #endregion L
        #region M

        protected const string _mapping = " mapping";
        protected const string _markers = " markers";
        protected const string _matching = " matching";
        protected const string _Method = "Method";
        protected const string _method = " method";
        protected const string _mode = " mode";
        protected const string _Must = "Must";

        protected const string _NOSPC_must = "must";
        
        #endregion M
        #region N

        protected const string _nation = " nation";
        protected const string _need = _SPC + _NOSPC_need;
        protected const string _new = " new";
        protected const string _No = "No";
        protected const string _no = " no";
        protected const string _node = _no + "de";
        protected const string _Not = _No + "t";
        protected const string _not = _no + "t";
        protected const string _not_found_FS = _not + _found + _FS;
        protected const string _number = " number";

        protected const string _NOSPC_need = "need";

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
        protected const string _Output = "Output";
        protected const string _Overwriting = "Overwriting";
        protected const string _overwriting = " overwriting";

        protected const string _SPC_ORM = _SPC + _ORM;
        protected const string _SPC_ORM_MappingFromAttributes = _SPC + _ORM_MappingFromAttributes;

        #endregion O
        #region P

        protected const string _parsing = " parsing";
        protected const string _path = " path";
        protected const string _persisted = " persisted";
        protected const string _Persisting = "Persisting";
        protected const string _prepared = " prepared";
        protected const string _Preparing = "Preparing";
        protected const string _PreparingTo = _Preparing + _to;
        protected const string _Press = "Press";

        #endregion P
        #region Q

        protected const string _queried = " queried";
        protected const string _query = " query";
        protected const string _Querying = "Querying";

        #endregion Q
        #region R

        protected const string _rating = " rating";
        protected const string _raw = " raw";
        protected const string _Read = "Read";
        protected const string _read = " read";
        protected const string _reader = _read + "er";
        protected const string _Reading = _Read + "ing";
        protected const string _reading = _read + "ing";
        protected const string _repository = " repository";
        protected const string _returned = " returned";
        protected const string _running = " running";

        #endregion R
        #region S

        protected const string _safely = " safely";
        protected const string _safely_aborted = " safely" + _aborted;
        protected const string _Schema = "Schema";
        protected const string _schema = " schema";
        protected const string _Selected = "Selected";
        protected const string _selected = " selected";
        protected const string _Selecting = "Selecting";
        protected const string _selection = " selection";
        protected const string _separated = " separated";
        protected const string _serialized = " serialized";
        protected const string _Serializing = "Serializing";
        protected const string _Session = "Session";
        protected const string _session = " session";
        protected const string _SessionFactory = _Session + _factory;
        protected const string _sessionFactory = _session + _factory;
        protected const string _source = " source";
        protected const string _spaces = " spaces";
        protected const string _Specification = "Specification";
        protected const string _specified = " specified";
        protected const string _standardized = " standardized";
        protected const string _starts = " starts";
        protected const string _stream = " stream";
        protected const string _string = " string";
        protected const string _subdirectories = " subdirectories";
        protected const string _subfolders = " subfolders";
        protected const string _Successfully = "Successfully";
        protected const string _successfully = " successfully";

        public const string W_Schema = _Schema;
        public const string W_TheSessionFactory = _The + _sessionFactory;

        #endregion S
        #region T

        protected const string _NOSPC_the = "the";

        protected const string _test = " test";
        protected const string _text = " text";
        protected const string _The = "The";
        protected const string _TheDirectory_SPC_FMT_Q = _The + _directory + _SPC_FMT_Q;
        protected const string _the = _SPC + _NOSPC_the;
        protected const string _There = _The + "re";
        protected const string _there = _the + "re";
        protected const string _theAssembly_FMT_Q = _the + _assembly + _SPC_FMT_Q;
        protected const string _to = " to";
        protected const string _tool = _to + "ol";
        protected const string _tools = _tool + "s";
        protected const string _Trying = "Trying";
        protected const string _trying = " trying";
        protected const string _TryingTo = _Trying + _to;
        protected const string _tryingTo = _trying + _to;

        #endregion T
        #region U

        protected const string _unfininshed = " unfininshed";
        protected const string _unit = " unit";
        protected const string _unpack = " unpack";
        protected const string _Unpacked = "Unpacked";
        protected const string _Unpacking = "Unpacking";
        protected const string _unpacking = _unpack + "ing";
        protected const string _using = " using";

        #endregion U
        #region V

        protected const string _value = " value";
        protected const string _version = " version";

        #endregion V
        #region W

        protected const string _while = " while";
        protected const string _with = " with";
        protected const string _write = " write";
        protected const string _written = " written";

        #endregion W
        #region X

        protected const string _XML = "XML";

        #endregion X
        #region Y

        protected const string _yet = " yet";

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