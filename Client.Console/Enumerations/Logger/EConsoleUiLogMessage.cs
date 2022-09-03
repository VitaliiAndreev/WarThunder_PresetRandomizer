using Core.Organization.Enumerations.Logger;

namespace Client.Console.Enumerations.Logger
{
    public class EConsoleUiLogMessage : EOrganizationLogMessage
    {
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: application whose location to select. </para>
        /// </summary>
        public static readonly string SelectValidLocation = $"Select valid {{0}} location: ";

        public static readonly string InputSpecification = $"Specification (game mode, nation, branch, BR - separated by spaces) > ";

        public static readonly string IncorrectInput = $"Incorrect input (need 4 arguments separated by spaces).";
        public static readonly string IncorrectGameMode = $"Incorrect game mode.";
        public static readonly string IncorrectNation = $"Incorrect nation.";
        public static readonly string IncorrectBranch = $"Incorrect branch.";
        public static readonly string IncorrectBattleRating = $"Incorrect battle rating (must be a number).";

        public static readonly string PressAnyKeyToExit = $"Press any key to exit.";
    }
}