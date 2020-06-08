using Core.DataBase.Enumerations.Logger;

namespace Core.Json.WarThunder.Enumerations.Logger
{
    /// <summary> Log message strings related to the "<see cref="WarThunder"/>" assembly. </summary>
    public class EJsonWarThunderLogMessage : EDatabaseLogMessage
    {
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: branch name. </para>
        /// </summary>
        public static readonly string BranchIsEmpty = $"{_Branch} \"{{0}}\" {_isEmpty}.";
        public static readonly string ColumnIsEmpty = $"{_Column} {_isEmpty}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: JSON text. </para>
        /// </summary>
        public static readonly string ObjectNotRecognizedAsResearchTreeVehicle = $"{_Object} \"{{0}}\" {_not} {_recognised} {_as} {_research} {_tree} {_vehicle}.";
        public static readonly string SeveralRequiredVehicleIsNotSupportedYet = $"{_Several} {_required} {_vehicles} {_is} {_not} {_supported} {_yet}.";
    }
}