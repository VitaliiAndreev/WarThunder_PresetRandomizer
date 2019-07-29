using Core.Enumerations.Logger;

namespace Client.Console.Enumerations.Logger
{
    public class EConsoleUiLogMessage : ECoreLogMessage
    {
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: application whose location to select. </para>
        /// </summary>
        public const string SelectValidLocation = _Select + _valid + _SPC_FMT + _location + _COLON + _SPC;

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