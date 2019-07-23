using Core.Enumerations.Logger;

namespace Client.Console.Enumerations.Logger
{
    public class EConsoleUiLogMessage : ECoreLogMessage
    {
        public const string PreparingGameFiles = _Preparing + _game + _files + _FS;
        public const string GameFilesPrepared = _Game + _files + _prepared + _FS;

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: game version. </para>
        /// </summary>
        public const string FoundDatabaseFor = _Found + _database + _for + _SPC_FMT + _FS;
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: game version. </para>
        /// </summary>
        public const string NotFoundDatabaseFor = _Not + _found + _database + _for + _SPC_FMT + _FS;

        public const string CreatingDatabase = _Creating + _database + _FS;
        public const string DatabaseCreatedConnectionEstablished = _Database + _created + _FS_SPC + _Connection + _established + _FS;
        public const string DataBaseConnectionEstablished = _Database + _connection + _established + _FS;

        public const string InitializingDatabase = _Initializing + _database + _FS;
        public const string DatabaseInitialized = _Database + _initialized + _FS;

        public const string CachingObjects = _Caching + _objects + _FS;
        public const string CachingComplete = _Caching + _complete + _FS;

        public const string InputSpecification = _Specification
            + _SPC_PARENTHESIS_OPEN + _NOSPC_game + _mode + _COMMA + _nation + _COMMA + _branch + _COMMA + _SPC + _BR + _SPC + _MINUS + _separated + _by + _spaces + _PARENTHESIS_CLOSE + _SPC + _MORE + _SPC;

        public const string IncorrectInput = _Incorrect + _input + _SPC_PARENTHESIS_OPEN + _NOSPC_need + _SPC + _4 + _arguments + _separated + _by + _spaces + _PARENTHESIS_CLOSE + _FS;
        public const string IncorrectGameMode = _Incorrect + _game + _mode + _FS;
        public const string IncorrectNation = _Incorrect + _nation + _FS;
        public const string IncorrectBranch = _Incorrect + _branch + _FS;
        public const string IncorrectBattleRating = _Incorrect + _battle + _rating + _SPC_PARENTHESIS_OPEN + _NOSPC_must + _be + _a + _number + _PARENTHESIS_CLOSE + _FS;

        public const string PressAnyKeyToExit = _Press + _any + _key + _to + _exit + _FS;
    }
}