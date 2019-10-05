using Core.Extensions;
using Core.Json.WarThunder.Enumerations.Logger;

namespace Core.Organization.Enumerations.Logger
{
    /// <summary> Log message strings related to the "<see cref="Organization"/>" assembly. </summary>
    public class EOrganizationLogMessage : EJsonWarThunderLogMessage
    {
        public static readonly string PreparingGameFiles = $"{_Preparing} {_game} {_files}.";
        public static readonly string GameFilesPrepared = $"{_Game} {_files} {_prepared}.";

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
        public static readonly string DatabaseCreatedConnectionEstablished = $"{_Database} {_created}. {_Connection} {_established}.";
        public static readonly string DataBaseConnectionEstablished = $"{_Database} {_connection} {_established}.";

        public static readonly string InitializingDatabase = $"{_Initializing} {_database}.";
        public static readonly string DatabaseInitialized = $"{_Database} {_initialized}.";

        public static readonly string CachingObjects = $"{_Caching} {_objects}.";
        public static readonly string CachingComplete = $"{_Caching} {_complete}.";

        public static readonly string InitializingResearchTrees = Initializing.FormatFluently($"{_research} {_trees}");
        public static readonly string ResearchTreesInitialized = ObjectInitialized.FormatFluently($"{_Research} {_trees}");

        public static readonly string DummyNationSelected = $"{_Dummy} {_nation} {_selected}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: nation collection. </para>
        /// <para> 2: branch collection. </para>
        /// </summary>
        public static readonly string NationsHaveNoBranch = $"\"{{0}}\" {_dont}/{_doesnt} {_have} \"{{1}}\".";
        public static readonly string NoVehiclesAvailableForSelectedBranches = $"{_No} {_vehicles} {_available} {_for} {_selected} {_branches}.";
        public static readonly string NoVehiclesAvailableForPreset = $"{_No} {_vehicles} {_available} {_for} {_preset}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: branch. </para>
        /// <para> 2: nation. </para>
        /// </summary>
        public static readonly string NoVehiclesAvailableForSelectedBattleRatings = $"{_No} {_vehicles} {_available} {_among} {{0}}-{{1}} {{2}} {_of} {{3}}.";
    }
}