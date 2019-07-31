using Core.Enumerations.Logger;

namespace Client.Console.Enumerations.Logger
{
    public class EConsoleUiLogMessage : ECoreLogMessage
    {
        private static readonly string _gameMode = $"{_game} {_mode}";
        private static readonly string _separatedBySpaces = $"{_separated} {_by} {_spaces}";

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: application whose location to select. </para>
        /// </summary>
        public static string SelectValidLocation => $"{_Select} {_valid} " + _FMT + $"{_location}: ";

        public static string InputSpecification => $"{_Specification} ({_gameMode}, {_nation}, {_branch}, {_BR} - {_separatedBySpaces}) > ";

        public static string IncorrectInput => $"{_Incorrect} {_input} ({_need} {_4} {_arguments} {_separatedBySpaces}).";
        public static string IncorrectGameMode => $"{_Incorrect} {_gameMode}.";
        public static string IncorrectNation => $"{_Incorrect} {_nation}.";
        public static string IncorrectBranch => $"{_Incorrect} {_branch}.";
        public static string IncorrectBattleRating => $"{_Incorrect} {_battle} {_rating} ({_must} {_be} {_a} {_number}).";

        public static string PressAnyKeyToExit => $"{_Press} {_any} {_key} {_to} {_exit}.";
    }
}