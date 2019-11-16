using Core.Extensions;
using Core.Json.WarThunder.Enumerations.Logger;

namespace Core.Organization.Enumerations.Logger
{
    /// <summary> Log message strings related to the "<see cref="Organization"/>" assembly. </summary>
    public class EOrganizationLogMessage : EJsonWarThunderLogMessage
    {
        private static readonly string _noVehicles = $"{_No} {_vehicles}";
        private static readonly string _noVehiclesAvailable = $"{_noVehicles} {_available}";

        public static readonly string PreparingGameFiles = $"{_Preparing} {_game} {_files}.";
        public static readonly string GameFilesPrepared = $"{_Game} {_files} {_prepared}.";

        public static readonly string DeserialisingGameFiles = $"{_Deserializing} {_game} {_files}.";
        public static readonly string DeserialisationComplete = $"{_Deserialization} {_complete}.";

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: game version. </para>
        /// </summary>
        public static readonly string FoundDatabaseFor = $"{_Found} {_databaseFor} {{0}}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: game version. </para>
        /// </summary>
        public static readonly string NotFoundDatabaseFor = $"{_Not} {_found} {_databaseFor} {{0}}.";

        public static readonly string CreatingDatabase = $"{_Creating} {_database}.";
        public static readonly string DatabaseCreated = $"{_Database} {_created}.";
        public static readonly string DatabaseConnectionEstablished = $"{_Database} {_connection} {_established}.";

        public static readonly string InitializingDatabase = $"{_Initializing} {_database}.";
        public static readonly string DatabaseInitialized = $"{_Database} {_initialized}.";

        public static readonly string CachingObjects = $"{_Caching} {_objects}.";
        public static readonly string CachingComplete = $"{_Caching} {_complete}.";

        public static readonly string InitializingResearchTrees = Initializing.FormatFluently($"{_research} {_trees}");
        public static readonly string ResearchTreesInitialized = ObjectInitialized.FormatFluently($"{_Research} {_trees}");

        public static readonly string DummyNationSelected = $"{_Dummy} {_nation} {_selected}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: main branch. </para>
        /// </summary>
        public static readonly string MainBranchHasNoVehicleClassesEnabled = $"{_Main} {_branch} \"{{0}}\" {_has} {_no} {_vehicle} {_classes} {_enabled}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: nation collection. </para>
        /// <para> 2: branch collection. </para>
        /// </summary>
        public static readonly string NationsHaveNoBranch = $"\"{{0}}\" {_dont}/{_doesnt} {_have} \"{{1}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: filter criteria. </para>
        /// </summary>
        public static readonly string NoVehiclesAvailableFor = $"{_noVehiclesAvailable} {_for} \"{{0}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: filter criteria. </para>
        /// </summary>
        public static readonly string NoVehiclesSelected = $"{_noVehicles} {_selected}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: branch. </para>
        /// <para> 2: nation. </para>
        /// </summary>
        public static readonly string NoVehiclesAvailableForSelectedBattleRatings = $"{_noVehiclesAvailable} {_among} {{0}}-{{1}} {{2}} {_of} {{3}}.";
    }
}