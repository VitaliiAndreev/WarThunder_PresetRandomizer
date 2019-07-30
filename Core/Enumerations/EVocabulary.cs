namespace Core.Enumerations
{
    /// <summary> Constants used internally to form useable English words and sentences. </summary>
    public class EVocabulary
    {
        #region Characters

        /// <summary> A comma. </summary>
        protected const string _COLON = ":";
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

        protected const string _NOSPC_game = "game";

        protected const string _Game = "Game";
        protected const string _game = _SPC + _NOSPC_game;
        protected const string _generation = " generation";
        protected const string _given = " given";

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
        protected const string _included = _in + "cluded";
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

        protected const string _language = " language";
        protected const string _Localization = "Localization";
        protected const string _location = " location";

        #endregion L
        #region M

        protected const string _NOSPC_must = "must";

        protected const string _mapping = " mapping";
        protected const string _markers = " markers";
        protected const string _matching = " matching";
        protected const string _Method = "Method";
        protected const string _method = " method";
        protected const string _mode = " mode";
        protected const string _Must = "Must";

        #endregion M
        #region N

        protected const string _NOSPC_need = "need";

        protected const string _nation = " nation";
        protected const string _need = _SPC + _NOSPC_need;
        protected const string _new = " new";
        protected const string _No = "No";
        protected const string _no = " no";
        protected const string _node = _no + "de";
        protected const string _Not = _No + "t";
        protected const string _not = _no + "t";
        protected const string _not_found_FS = _not + _found + _FS;
        protected const string _NULL = "NULL";
        protected const string _number = " number";

        protected const string _SPC_NULL = _SPC + _NULL;

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
        protected const string _recognized = " recognized";
        protected const string _repository = " repository";
        protected const string _returned = " returned";
        protected const string _running = " running";

        #endregion R
        #region S

        protected const string _safely = " safely";
        protected const string _safely_aborted = " safely" + _aborted;
        protected const string _Schema = "Schema";
        protected const string _schema = " schema";
        protected const string _Select = "Select";
        protected const string _Selected = _Select + "ed";
        protected const string _selected = " selected";
        protected const string _Selecting = _Select + "ing";
        protected const string _selection = " selection";
        protected const string _separated = " separated";
        protected const string _serialized = " serialized";
        protected const string _Serializing = "Serializing";
        protected const string _Session = "Session";
        protected const string _session = " session";
        protected const string _SessionFactory = _Session + _factory;
        protected const string _sessionFactory = _session + _factory;
        protected const string _sorting = " sorting";
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
        protected const string _types = " types";

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

        protected const string _valid = " valid";
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
    }
}