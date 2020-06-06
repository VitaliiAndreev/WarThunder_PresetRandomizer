namespace Core.Enumerations
{
    /// <summary> Constants used internally to form useable English words and sentences. </summary>
    public class EVocabulary
    {
        #region Characters

        /// <summary> A format placeholder. </summary>
        protected const string _FMT = "{0}";

        #endregion Characters
        #region Numbers

        protected const string _4 = "4";

        #endregion Numbers
        #region Words

        #region A

        private const string _ag = _a + _g;
        private const string _al = _a + _l;
        private const string _ame = _a + _me;
        private const string _ap = _a + _p;
        private const string _ar = _a + _r;
        private const string _aw = _a + _w;

        protected const string _A = "A";
        protected const string _a = "a";
        protected const string _ab = _a + _b;
        protected const string _able = _ab + "le";
        protected const string _aborted = _a + _b + _or + _t + _ed;
        protected const string _About = _A + _b + _out;
        protected const string _act = _a + _c + _t;
        protected const string _Ad = _A + _d;
        protected const string _ad = _a + _d;
        protected const string _Add = _Ad + _d;
        protected const string _add = _ad + _d;
        protected const string _Additional = _Add + _d + _it + _ion + _al;
        protected const string _additional = _add + _d + _it + _ion + _al;
        protected const string _adapt = _ad + _apt;
        protected const string _adapted = _adapt + _ed;
        protected const string _aft = _a + _f + _t;
        protected const string _after = _aft + _er;
        protected const string _age = _ag + _e;
        protected const string _air = _a + _i + _r;
        protected const string _aircraft = _air + _craft;
        protected const string _All = _A + _ll;
        protected const string _all = _a + _ll;
        protected const string _Already = _A + _lready;
        protected const string _already = _a + _lready;
        protected const string _am = _a + _m;
        protected const string _among = _a + _mong;
        protected const string _An = _A + _n;
        protected const string _an = _a + _n;
        protected const string _and = _an + _d;
        protected const string _ant = _an + _t;
        protected const string _any = _an + _y;
        protected const string _Application = _A + _pplication;
        protected const string _application = _a + _pplication;
        protected const string _apt = _ap + _t;
        protected const string _are = _a + _re;
        protected const string _argument = _ar + _g + _u + _ment;
        protected const string _arguments = _argument + _s;
        protected const string _array = _ar + _ray;
        protected const string _art = _ar + _t;
        protected const string _As = _a + _s;
        protected const string _as = _a + _s;
        protected const string _assembly = _as + "semb" + _ly;
        protected const string _Assigned = _As + _sign + _ed;
        protected const string _At = _A + _t;
        protected const string _at = _a + _t;
        protected const string _ate = _at + _e;
        protected const string _attached = _at + _t + _a + _ch + _ed;
        protected const string _Attaching = _At + _t + _a + _ch + _ing;
        protected const string _attributes = _at + _tribute + _s;
        protected const string _avail = _a + "vail";
        protected const string _available = _avail + _able;

        #endregion A
        #region B

        private const string _B = "B";
        private const string _b = "b";
        private const string _bu = _b + _u;

        protected const string _base = _b + _a + _s + _e;
        protected const string _Bat = _B + _at;
        protected const string _bat = _b + _at;
        protected const string _Battle = _Bat + _t + _l + _e;
        protected const string _battle = _bat + _t + _l + _e;
        protected const string _be = _b + _e;
        protected const string _been = _be + _e + _n;
        protected const string _Blank = _B + _l + _an + _k;
        protected const string _blank = _b + _l + _an + _k;
        protected const string _BR = _B + "R";
        protected const string _Branch = _B + _ranch;
        protected const string _branch = _b + _ranch;
        protected const string _branches = _branch + _es;
        protected const string _Brit = _B + _ri + _t;
        protected const string _Britain = _Brit + _a + _in;
        protected const string _built = _bu + _ilt;
        protected const string _by = _b + "y";
        protected const string _byte = _by + _t + _e;
        protected const string _bytes = _byte + _s;

        #endregion B
        #region C

        private const string _C = "C";
        private const string _c = "c";
        private const string _cc = _c + _c;
        private const string _ch = _c + _h;
        private const string _chema = _ch + _e + _m + _a;
        private const string _ck = _c + _k;
        private const string _cla = _c + _l + _a;
        private const string _co = _c + _o;
        private const string _ct = _c + _t;

        protected const string _cache = "cache";
        protected const string _Caching = "Cach" + _ing;
        protected const string _can = _c + _an;
        protected const string _cant = _can + "'" + _t;
        protected const string _cancelled = _can + _cell + _ed;
        protected const string _cat = _c + _at;
        protected const string _cell = "cell";
        protected const string _Change = "Change";
        protected const string _change = "change";
        protected const string _Changes = _Change + _s;
        protected const string _changes = _change + _s;
        protected const string _character = "character";
        protected const string _characters = _character + _s;
        protected const string _Check = "Check";
        protected const string _Checking = _Check + _ing;
        protected const string _class = _cla + _ss;
        protected const string _classes = _class + _es;
        protected const string _clean = "clean";
        protected const string _cleanup = _clean + "-up";
        protected const string _cleared = _c + _le + _ar + _ed;
        protected const string _Clearing = _C + _le + _ar + _ing;
        protected const string _Client = "Client";
        protected const string _client = "client";
        protected const string _Close = "Close";
        protected const string _close = "close";
        protected const string _Closed = _Close + _d;
        protected const string _closed = _close + _d;
        protected const string _Closing = "Clos" + _ing;
        protected const string _collect = "collect";
        protected const string _collection = _collect + _ion;
        protected const string _Column = "Column";
        protected const string _commit = "commit";
        protected const string _committed = _commit + "t" + _ed;
        protected const string _Committing = "Committ" + _ing;
        protected const string _complete = "complete";
        protected const string _completed = _complete + _d;
        protected const string _Con = _C + _on;
        protected const string _con = _c + _on;
        protected const string _configuration = _con + "figurat" + _ion;
        protected const string _Connect = _Con + "nect";
        protected const string _connect = _con + "nect";
        protected const string _Connection = _Connect + _ion;
        protected const string _connection = _connect + _ion;
        protected const string _contain = _con + "tain";
        protected const string _container = _contain + _er;
        protected const string _converted = _con + _vert + _ed;
        protected const string _Converter = _Con + _vert + _ed;
        protected const string _Copied = "Copi" + _ed;
        protected const string _copied = "copi" + _ed;
        protected const string _Copying = "Copy" + _ing;
        protected const string _core = _co + _re;
        protected const string _correct = "correct";
        protected const string _couldnt = "couldn'" + _t;
        protected const string _count = _co + _un + _t;
        protected const string _country = _count + _ry;
        protected const string _countries = _count + _ri + _es;
        protected const string _craft = _c + _raft;
        protected const string _create = "create";
        protected const string _created = _create + _d;
        protected const string _Creating = "Creat" + _ing;
        protected const string _creating = "creat" + _ing;
        protected const string _CSV = "CSV";
        protected const string _cut = "cut";

        #endregion C
        #region D

        private const string _D = "D";
        private const string _d = "d";
        private const string _De = _D + _e;
        private const string _de = _d + _e;
        private const string _di = _d + _i;

        protected const string _Data = "Data";
        protected const string _data = _d + "ata";
        protected const string _Database = _Data + _base;
        protected const string _database = _data + _base;
        protected const string _default = _de + _fault;
        protected const string _delete = _d + "elete";
        protected const string _deleted = _delete + "d";
        protected const string _Deleting = "Delet" + _ing;
        protected const string _deleting = _d + "elet" + _ing;
        protected const string _der = _de + _r;
        protected const string _Deserialisation = _De + _serial + _is + _at + _ion;
        protected const string _deserialise = _d + "eserialise";
        protected const string _deserialised = _deserialise + "d";
        protected const string _Deserialiser = "Deserialiser";
        protected const string _Deserialising = _De + _serial + _is + _ing;
        protected const string _deserialising = _de + _serial + _is + _ing;
        protected const string _details = _d + "etails";
        protected const string _dialog = _d + "ia" + _log;
        protected const string _direct = _di + _rect;
        protected const string _directories = _direct + _or + _i + _es;
        protected const string _directory = _direct + _ory;
        protected const string _disabled = _d + "isable" + _d;
        protected const string _Disposal = "Disposal";
        protected const string _dispose = _d + "ispose";
        protected const string _disposed = _dispose + "d";
        protected const string _Disposing = "Dispos" + _ing;
        protected const string _does = _d + "oes";
        protected const string _doesnt = _does + _n + ECharacterString.Apostrophe + _t;
        protected const string _Dont = "D" + _on + ECharacterString.Apostrophe + _t;
        protected const string _dont = _d + _on + ECharacterString.Apostrophe + _t;
        protected const string _down = _d + _own;
        protected const string _Dummy = "Dum" + _my;

        #endregion D
        #region E

        private const string _E = "E";
        private const string _e = "e";
        private const string _ea = _e + _a;
        private const string _ect = _e + _c + _t;
        private const string _ed = _e + _d;
        private const string _em = _e + _m;
        private const string _en = _e + _n;
        private const string _ene = _en + _e;
        private const string _er = _e + _r;
        private const string _eri = _er + _i;
        private const string _erial = _eri + _al;
        private const string _es = _e + _s;
        private const string _ess = _es + _s;
        private const string _est = _es + _t;
        private const string _ett = _e + _t + _t;
        private const string _Ex = _E + _x;
        private const string _ey = _e + _y;

        protected const string _Eco = "E" + _co;
        protected const string _Economic = _Eco + _no + _mic;
        protected const string _eh = _e + _h;
        protected const string _el = _e + _l;
        protected const string _elect = _el + _ect;
        protected const string _emptied = _e + "mpti" + _ed;
        protected const string _empty = _e + "mpty";
        protected const string _Emptying = _E + "mpty" + _ing;
        protected const string _enabled = _e + _n + _able + _d;
        protected const string _enumeration = _e + "numer" + _at + _ion;
        protected const string _Error = _E + "rr" + _or;
        protected const string _error = _e + "rr" + _or;
        protected const string _established = _e + _stab + "l" + _ish + _ed;
        protected const string _Exception = _E + "xcept" + _ion;
        protected const string _execution = _e + "xe" + _cut + _ion;
        protected const string _exist = _e + "xist";
        protected const string _exists = _exist + _s;
        protected const string _exit = _e + _XML + _it;
        protected const string _Explicit = _Ex + "plic" + _it;
        protected const string _extension = _e + "xtens" + _ion;

        #endregion E
        #region F

        private const string _F = "F";
        private const string _f = "f";
        private const string _fa = _f + _a;

        protected const string _Factory = _F + _act + _or + _y;
        protected const string _factory = _f + _act + _or + _y;
        protected const string _fault = _fa + _ult;
        protected const string _File = _F + "ile";
        protected const string _file = _f + "ile";
        protected const string _files = _file + _s;
        protected const string _filter = _f + _ilt + _er;
        protected const string _filtered = _filter + _ed;
        protected const string _Filtering = _F + _ilt + _er + _ing;
        protected const string _finish = _f + _in + _ish;
        protected const string _folder = _f + _old + _er;
        protected const string _for = _f + _or;
        protected const string _format = _for + _mat;
        protected const string _formats = _format + _s;
        protected const string _Found = _F + _ound;
        protected const string _found = _f + _ound;
        protected const string _from = _f + "rom";
        protected const string _fully = _f + "ully";

        #endregion F
        #region G

        private const string _G = "G";
        private const string _g = "g";
        private const string _gr = _g + _r;

        protected const string _Game = _G + _ame;
        protected const string _game = _g + _ame;
        protected const string _Gene = _G + _ene;
        protected const string _gene = _g + _ene;
        protected const string _generate = _gene + _rat + _e;
        protected const string _generation = _gene + _rat + _ion;
        protected const string _Generator = _Gene + _rat + _or;
        protected const string _Generic = _gene + _r + _i + _c;
        protected const string _given = _g + "iven";
        protected const string _GUI = _G + _UI;

        #endregion G
        #region H

        private const string _H = "H";
        private const string _h = "h";

        protected const string _has = _h + _as;
        protected const string _have = _h + "ave";
        protected const string _he = _h + _e;
        protected const string _Help = "Help";
        protected const string _Helper = _Help + _er;
        protected const string _Helpers = _Helper + _s;
        protected const string _helpers = _h + "el" + _per + _s;
        protected const string _here = _h + "ere";
        protected const string _hi = _h + _i;
        protected const string _hip = _hi + _p;
        protected const string _horizontal = _h + "orizontal";
        protected const string _how = _h + _ow;
        protected const string _HTML = _H + _T + _M + _L;
        protected const string _hun = _h + _un;

        #endregion H
        #region I

        private const string _i = "i";
        private const string _ialis = _i + _a + "lis";
        private const string _ic = _i + _c;
        private const string _ilt = _i + "lt";
        private const string _ing = _i + "ng";
        private const string _ise = _is + _e;
        private const string _itialis = _it + _ialis;
        private const string _iz = _i + _z;
        private const string _ize = _iz + _e;

        protected const string _I = "I";
        protected const string _icons = _i + _con + _s;
        protected const string _ill = _i + _ll;
        protected const string _Image = _I + _mage;
        protected const string _image = _i + _mage;
        protected const string _images = _image + _s;
        protected const string _implementation = _i + "mplement" + _at + _ion;
        protected const string _implemented = _i + "mplement" + _ed;
        protected const string _In = _I + _n;
        protected const string _in = _i + _n;
        protected const string _included = _in + "clude" + _d;
        protected const string _Incorrect = _In + _correct;
        protected const string _Initialisation = _In + _itialis + _at + _ion;
        protected const string _Initialise = _In + _itialis + _e;
        protected const string _initialise = _in + _itialis + _e;
        protected const string _Initialised = _Initialise + _d;
        protected const string _initialised = _initialise + _d;
        protected const string _Initialising = _In + _it + "ialis" + _ing;
        protected const string _inmemory = _in + ECharacterString.Minus + _memory;
        protected const string _input = _in + _put;
        protected const string _install = _in + _s + _t + _all;
        protected const string _Instance = _In + _stance;
        protected const string _instance = _in + _stance;
        protected const string _instances = _instance + _s;
        protected const string _instant = _in + "st" + _ant;
        protected const string _instantiated = _instant + _i + _ate + _d;
        protected const string _Integration = _In + _tegrat + _ion;
        protected const string _integration = _in + _tegrat + _ion;
        protected const string _into = _in + _to;
        protected const string _invalid = _in + _valid;
        protected const string _ion = _i + _on;
        protected const string _is = _i + _s;
        protected const string _ish = _i + _s + _h;
        protected const string _it = _i + _t;
        protected const string _item = _it + _em;
        protected const string _items = _item + _s;

        #endregion I
        #region J

        protected const string _JSON = "JSON";

        #endregion J
        #region K

        private const string _K = "K";
        private const string _k = "k";

        protected const string _Key = _K + _ey;
        protected const string _key = _k + _ey;

        #endregion K
        #region L

        private const string _L = "L";
        private const string _l = "l";
        private const string _ll = _l + _l;
        private const string _lo = _l + _o;
        private const string _lready = _l + _ready;
        private const string _ly = _l + _y;

        protected const string _language = "language";
        protected const string _latest = "latest";
        protected const string _le = _l + _e;
        protected const string _Line = "Line";
        protected const string _Loading = "Load" + _ing;
        protected const string _Localisation = "Localisat" + _ion;
        protected const string _localisation = _lo + "calisat" + _ion;
        protected const string _located = _lo + _cat + _ed;
        protected const string _location = _lo + _cat + _ion;
        protected const string _Log = "Log";
        protected const string _log = _lo + "g";
        protected const string _Logger = _Log + "ger";
        protected const string _Loggers = _Logger + _s;
        protected const string _Logs = _Log + _s;
        protected const string _lot = _lo + _t;

        #endregion L
        #region M

        private const string _M = "M";
        private const string _m = "m";
        private const string _mem = _me + _m;
        private const string _mo = _m + _o;

        protected const string _mage = _m + _age;
        protected const string _Main = _M + _a + _in;
        protected const string _main = _m + _a + _in;
        protected const string _Man = _M + _an;
        protected const string _Manager = _Man + _a + "ger";
        protected const string _Manual = _Man + _u + _al;
        protected const string _mapping = "mapp" + _ing;
        protected const string _markers = "marker" + _s;
        protected const string _mat = "mat";
        protected const string _match = _mat + _ch;
        protected const string _matches = _match + _es;
        protected const string _matching = _match + _ing;
        protected const string _may = _m + _a + _y;
        protected const string _me = _m + _e;
        protected const string _memo = _mem + _o;
        protected const string _memory = _memo + _ry;
        protected const string _men = "men";
        protected const string _ment = _men + _t;
        protected const string _Method = _M + _e + _tho + _d;
        protected const string _method = _m + _e + _tho + _d;
        protected const string _mic = _m + _i + _c;
        protected const string _mig = _m + _i + _g;
        protected const string _migration = _mig + _ration;
        protected const string _mode = _mo + _d + _e;
        protected const string _mong = _mo + _n + _g;
        protected const string _more = _mo + _re;
        protected const string _Must = "Mu" + _s + _t;
        protected const string _must = "mu" + _s + _t;
        protected const string _my = "my";

        #endregion M
        #region N

        private const string _N = "N";
        private const string _n = "n";
        private const string _ne = _n + _e;

        protected const string _nation = _n + "ation";
        protected const string _Need = _N + _e + _ed;
        protected const string _need = _n + _e + _ed;
        protected const string _new = _n + "ew";
        protected const string _No = "No";
        protected const string _no = _n + "o";
        protected const string _node = _no + "de";
        protected const string _Non_ = _No + _n + ECharacterString.Minus;
        protected const string _None = _No + _ne;
        protected const string _Not = _No + _t;
        protected const string _not = _no + _t;
        protected const string _Nothing = _No + _thing;
        protected const string _NULL = "NULL";
        protected const string _number = _n + "umber";

        #endregion N
        #region O

        private const string _o = "o";
        private const string _om = _o + _m;
        private const string _os = _o + _s;
        private const string _ory = _o + _ry;
        private const string _ound = _o + _und;

        protected const string _Object = _o + "bject";
        protected const string _object = _o + "bject";
        protected const string _objects = _object + _s;
        protected const string _occured = _o + "ccurr" + _ed;
        protected const string _of = _o + _f;
        protected const string _old = _o + "ld";
        protected const string _on = _o + _n;
        protected const string _one = _on + _e;
        protected const string _or = _o + _r;
        protected const string _order = _or + _d + _er;
        protected const string _ORM = "ORM";
        protected const string _out = _o + "ut";
        protected const string _Output = "Output";
        protected const string _over = _o + "ver";
        protected const string _overwrite = _over + _write;
        protected const string _Overwriting = "Overwrit" + _ing;
        protected const string _overwriting = _over + "writ" + _ing;
        protected const string _ow = _o + _w;
        protected const string _own = _ow + _n;

        #endregion O
        #region P

        private const string _P = "P";
        private const string _p = "p";
        private const string _pos = _p + _os;
        private const string _pp = _p + _p;
        private const string _pplication = _pp + "li" + _cat + _ion;
        private const string _Pr = _P + _r;
        private const string _pr = _p + _r;
        private const string _Pre = _Pr + _e;
        private const string _pre = _pr + _e;
        private const string _Pro = _Pr + _o;
        private const string _pro = _pr + _o;

        protected const string _Pack = _P + _a + _ck;
        protected const string _pack = _p + _a + _ck;
        protected const string _pan = _p + _an;
        protected const string _panel = _pan + _el;
        protected const string _Par = "Par";
        protected const string _par = "par";
        protected const string _Parser = _Par + _s + _er;
        protected const string _parsing = _par + _s + _ing;
        protected const string _path = "path";
        protected const string _per = "per";
        protected const string _persisted = _per + "sist" + _ed;
        protected const string _Persisting = "Persist" + _ing;
        protected const string _Plea = _P + _l + _ea;
        protected const string _Please = _Plea + _se;
        protected const string _port = "port";
        protected const string _portrait = _port + _r + _a + _it;
        protected const string _portraits = _portrait + _s;
        protected const string _prepared = "prepare" + _d;
        protected const string _Preparing = _Pre + _par + _ing;
        protected const string _Preset = _Pre + _set;
        protected const string _preset = _pre + _set;
        protected const string _Press = _Pre + _s + _s;
        protected const string _processed = _pro + _c + _ess + _ed;
        protected const string _Processing = _Pro + _c + _ess + _ing;
        protected const string _proper = _pro + _per;
        protected const string _properly = _proper + _ly;
        protected const string _Proxy = _Pro + _xy;
        protected const string _put = "put";

        #endregion P
        #region Q

        protected const string _queried = "queried";
        protected const string _query = "query";
        protected const string _Querying = "Querying";

        #endregion Q
        #region R

        private const string _R = "R";
        private const string _r = "r";
        private const string _Re = _R + _e;
        private const string _re = _r + _e;
        private const string _rect = _re + _ct;
        private const string _ri = _r + _i;
        private const string _ry = _r + _y;

        protected const string _raft = _r + _aft;
        protected const string _ranch = _r + _an + _ch;
        protected const string _Randomiser = _R + _and + _om + _ise + _r;
        protected const string _Rank = _R + _an + _k;
        protected const string _rank = _r + _an + _k;
        protected const string _ranks = _rank + _s;
        protected const string _rap = _r + _a + _p;
        protected const string _Rat = _R + _at;
        protected const string _rat = _r + _at;
        protected const string _Rating = _Rat + _ing;
        protected const string _rating = _rat + _ing;
        protected const string _ratings = _rating + _s;
        protected const string _ration = _rat + _ion;
        protected const string _raw = _r + _aw;
        protected const string _ray = _r + _a + _y;
        protected const string _Read = _Re + _ad;
        protected const string _read = _re + _ad;
        protected const string _ready = _read + _y;
        protected const string _Reader = _Read + _er;
        protected const string _reader = _read + _er;
        protected const string _Reading = _Read + _ing;
        protected const string _reading = _read + _ing;
        protected const string _recognized = _re + "cognize" + _d;
        protected const string _regenerated = _re + _generate + _d;
        protected const string _Repository = _Re + _pos + _it + _ory;
        protected const string _repository = _re + _pos + _it + _ory;
        protected const string _required = _re + "quire" + _d;
        protected const string _Research = _Re + _search;
        protected const string _research = _re + _search;
        protected const string _Reserve = _Re + _serve;
        protected const string _restart = _re + _start;
        protected const string _returned = _re + "turn" + _ed;
        protected const string _rite = _r + _i + _t + _e;
        protected const string _running = _r + "unn" + _ing;

        #endregion R
        #region S

        private const string _S = "S";
        private const string _se = _s + _e;

        protected const string _s = "s";
        protected const string _ss = _s + _s;

        protected const string _safely = _s + "afe" + _ly;
        protected const string _Schema = _S + _chema;
        protected const string _schema = _s + _chema;
        protected const string _score = _s + _core;
        protected const string _search = _s + _e + _a + "rch";
        protected const string _See = _S + "ee";
        protected const string _Select = _S + _elect;
        protected const string _select = _s + _elect;
        protected const string _Selected = _Select + _ed;
        protected const string _selected = _select + _ed;
        protected const string _Selecting = _Select + _ing;
        protected const string _selection = _select + _ion;
        protected const string _Selector = _Select + _or;
        protected const string _separated = _s + "eparate" + _d;
        protected const string _Serial = _S + _erial;
        protected const string _serial = _s + _erial;
        protected const string _serialized = _serial + _ize + _d;
        protected const string _Serializing = _Serial + "iz" + _ing;
        protected const string _serve = _se + _r + _ve;
        protected const string _Session = _S + _ess + _ion;
        protected const string _session = _s + _ess + _ion;
        protected const string _set = _s + _e + _t;
        protected const string _Settings = _S + _ett + _ing + _s;
        protected const string _settings = _set + _t + _ing + _s;
        protected const string _Several = _S + "everal";
        protected const string _ship = _s + _hip;
        protected const string _ships = _ship + _s;
        protected const string _Showing = _S + _how + _ing;
        protected const string _Shown = _S + _how + _n;
        protected const string _shown = _s + _how + _n;
        protected const string _shut = _s + "hut";
        protected const string _Shutting = _S + "hutt" + _ing;
        protected const string _sign = _s + "ign";
        protected const string _skipped = _s + "kipp" + _ed;
        protected const string _slots = _s + _lot + _s;
        protected const string _sorting = _s + "ort" + _ing;
        protected const string _source = _s + "ource";
        protected const string _spaces = _s + "pace" + _s;
        protected const string _Specification = _S + "pecificat" + _ion;
        protected const string _specified = _s + "pecifi" + _ed;
        protected const string _stab = _s + _tab;
        protected const string _Stack = _S + _tack;
        protected const string _stance = _s + "tance";
        protected const string _standardized = _s + "tandardize" + _d;
        protected const string _start = _s + _tart;
        protected const string _Started = _S + _tart + _ed;
        protected const string _Starter = _S + _tart + _er;
        protected const string _starts = _s + _tart + _s;
        protected const string _Stream = _S + _tre + _am;
        protected const string _stream = _s + _tre + _am;
        protected const string _string = _s + "tr" + _ing;
        protected const string _Style = _S + "tyle";
        protected const string _sub = _s + _u + _b;
        protected const string _subclass = _sub + _class;
        protected const string _subdirectories = _sub + _direct + "orie" + _s;
        protected const string _subfolders = _sub + _folder + _s;
        protected const string _substrings = _sub + _string + _s;
        protected const string _Successfully = _S + _uccess + _fully;
        protected const string _successfully = _s + _uccess + _fully;
        protected const string _supported = _s + _up + _port + _ed;

        #endregion S
        #region T

        private const string _T = "T";
        private const string _t = "t";
        private const string _tegrat = _t + _e + _gr + _a + _t;
        private const string _th = _t + _h;
        private const string _tre = _t + _re;

        protected const string _tab = _t + _a + _b;
        protected const string _tack = _t + _a + _c + _k;
        protected const string _Tag = _T + _ag;
        protected const string _tag = _t + _ag;
        protected const string _tagged = _tag + _g + _ed;
        protected const string _tags = _tag + _s;
        protected const string _tan = _t + _an;
        protected const string _tank = _tan + _k;
        protected const string _tanks = _tank + _s;
        protected const string _tart = _t + _art;
        protected const string _Temp = _T + _em + _p;
        protected const string _temp = _t + _em + _p;
        protected const string _ten = _t + _e + _n;
        protected const string _test = _t + _est;
        protected const string _Tests = _T + _est + _s;
        protected const string _text = _t + "ext";
        protected const string _The = _T + _he;
        protected const string _the = _t + _he;
        protected const string _There = _The + _re;
        protected const string _there = _the + _re;
        protected const string _thin = _t + _h + _in;
        protected const string _thing = _thin + _g;
        protected const string _tho = _th + _o;
        protected const string _Thunder = _T + _hun + _der;
        protected const string _to = _t + _o;
        protected const string _token = _to + "ken";
        protected const string _tool = _to + "ol";
        protected const string _tools = _tool + _s;
        protected const string _tree = _t + "ree";
        protected const string _trees = _tree + _s;
        protected const string _tribute = _t + "ribute";
        protected const string _Try = "Try";
        protected const string _try = _t + _ry;
        protected const string _Trying = _Try + _ing;
        protected const string _trying = _try + _ing;
        protected const string _two = _t + _w + _o;
        protected const string _type = _t + "ype";
        protected const string _types = _type + _s;

        #endregion T
        #region U

        private const string _U = "U";
        private const string _u = "u";
        private const string _uccess = _u + _cc + _e + _ss;
        private const string _ult = _u + _l + _t;
        private const string _Un = "U" + _n;
        private const string _un = _u + _n;
        private const string _und = _un + _d;

        protected const string _UI = _U + _I;
        protected const string _under = _und + _er;
        protected const string _underscore = _under + _score;
        protected const string _unfinished = _un + _finish + _ed;
        protected const string _Unit = _Un + _it;
        protected const string _unit = _un + _it;
        protected const string _units = _un + _it + _s;
        protected const string _unpack = _un + _pack;
        protected const string _Unpacked = _Un + _pack + _ed;
        protected const string _Unpacker = _Un + _pack + _er;
        protected const string _Unpacking = _Un + _pack + _ing;
        protected const string _unpacking = _un + _pack + _ing;
        protected const string _Untagged = _Un + _tagged;
        protected const string _up = "up";
        protected const string _us = "us";
        protected const string _using = _us + _ing;

        #endregion U
        #region V

        private const string _V = "V";
        private const string _v = "v";
        private const string _ve = _v + _e;
        private const string _vert = _ve + _r + _t;

        protected const string _valid = "valid";
        protected const string _Value = "Value";
        protected const string _value = "value";
        protected const string _Vehicle = _V + _eh + _ic + _le;
        protected const string _vehicle = _v + _eh + _ic + _le;
        protected const string _Vehicles = _Vehicle + _s;
        protected const string _vehicles = _vehicle + _s;
        protected const string _version = "vers" + _ion;
        protected const string _vertical = _vert + "ical";

        #endregion V
        #region W

        private const string _W = "W";
        private const string _w = "w";

        protected const string _War = _W + _ar;
        protected const string _while = _w + "hile";
        protected const string _will = _w + _ill;
        protected const string _Window = "Window";
        protected const string _with = _w + "ith";
        protected const string _WPF = "WPF";
        protected const string _Wrap = _W + _rap;
        protected const string _write = _w + _rite;
        protected const string _written = _w + "rit" + _ten;

        #endregion W
        #region X

        private const string _x = "x";
        private const string _xy = _x + _y;

        protected const string _XML = "XML";

        #endregion X
        #region Y

        private const string _y = "y";

        protected const string _yet = _y + "et";

        #endregion Y
        #region Z

        private const string _z = "z";

        #endregion Z

        #endregion Words
    }
}