using Core.DataBase.Enumerations.Logger;

namespace Core.Json.WarThunder.Enumerations.Logger
{
    public class EJsonWarThunderLogMessage : EDatabaseLogMessage
    {
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: branch name. </para>
        /// </summary>
        public static readonly string BranchIsEmpty = $"Branch \"{{0}}\" is empty.";
        public static readonly string ColumnIsEmpty = $"Column is empty.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: JSON text. </para>
        /// </summary>
        public static readonly string ObjectNotRecognizedAsResearchTreeVehicle = $"Object \"{{0}}\" not recognised as research tree vehicle.";
        public static readonly string SeveralRequiredVehicleIsNotSupportedYet = $"Several required vehicles is not supported yet.";
    }
}