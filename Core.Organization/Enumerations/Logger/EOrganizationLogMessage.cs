using Core.Enumerations;
using Core.Extensions;
using Core.Json.WarThunder.Enumerations.Logger;

namespace Core.Organization.Enumerations.Logger
{
    /// <summary> Log message strings related to the "<see cref="Organization"/>" assembly. </summary>
    public class EOrganizationLogMessage : EJsonWarThunderLogMessage
    {
        private static readonly string _noVehicles = $"{_No} {_vehicles}";
        private static readonly string _noVehiclesAvailable = $"{_noVehicles} {_available}";

        public static readonly string ClearingTempDirectory = $"{_Clearing} {_temp} {_folder}.";
        public static readonly string TempDirectoryCleared = $"{_Temp} {_folder} {_cleared}.";

        public static readonly string SeveralCacheDirectoriesFound = $"{_Several} {_cache} {_directories} {_matching} {_the} {_given} {_version} {_have} {_been} {_found}: {{0}}.";

        public static readonly string PreparingGameFiles = $"{_Preparing} {_game} {_files}.";
        public static readonly string GameFilesPrepared = $"{_Game} {_files} {_prepared}.";

        public static readonly string PreparingGameDataFiles = $"{_Preparing} {_game} {_data} {_files}.";
        public static readonly string GameDataFilesPrepared = $"{_Game} {_data} {_files} {_prepared}.";

        public static readonly string PreparingGameImageFiles = $"{_Preparing} {_game} {_image} {_files}.";
        public static readonly string GameImageFilesPrepared = $"{_Game} {_image} {_files} {_prepared}.";

        public static readonly string PreparingBlkxFiles = $"{_Preparing} {EFileExtension.Blkx.ToUpper()} {_files}.";
        public static readonly string BlkxFilesPrepared = $"{EFileExtension.Blkx.ToUpper()} {_files} {_prepared}.";

        public static readonly string PreparingCsvFiles = $"{_Preparing} {EFileExtension.Csv.ToUpper()} {_files}.";
        public static readonly string CsvFilesPrepared = $"{EFileExtension.Csv.ToUpper()} {_files} {_prepared}.";

        public static readonly string PreparingVehicleIcons = $"{_Preparing} {_vehicle} {_icons} {_files}.";
        public static readonly string VehicleIconsPrepared = $"{_Vehicle} {_icons} {_prepared}.";

        public static readonly string PreparingVehiclePortraits = $"{_Preparing} {_vehicle} {_portraits} {_files}.";
        public static readonly string VehiclePortraitsPrepared = $"{_Vehicle} {_portraits} {_prepared}.";

        public static readonly string DeserialisingMainVehicleData = $"{_Deserialising} {_main} {_vehicle} {_data}.";
        public static readonly string MainVehicleDataDeserialised = $"{_Main} {_vehicle} {_data} {_deserialised}.";

        public static readonly string DeserialisingAdditionalVehicleData = $"{_Deserialising} {_additional} {_vehicle} {_data}.";
        public static readonly string AdditionalVehicleDataDeserialised = $"{_Additional} {_vehicle} {_data} {_deserialised}.";

        public static readonly string DeserialisingResearchTrees = $"{_Deserialising} {_research} {_trees}.";
        public static readonly string ResearchTreesDeserialised = $"{_Research} {_trees} {_deserialised}.";

        public static readonly string DeserialisingVehicleLocalisation = $"{_Deserialising} {_vehicle} {_localisation}.";
        public static readonly string VehicleLocalisationDeserialised = $"{_Vehicle} {_localisation} {_deserialised}.";

        public static readonly string DeserialisingGameFiles = $"{_Deserialising} {_game} {_files}.";
        public static readonly string DeserialisationComplete = $"{_Deserialisation} {_complete}.";

        public static readonly string InitialisingVehicles = $"{_Initialising} {_vehicles}.";
        public static readonly string VehiclesInitialised = $"{_Vehicles} {_initialised}.";

        public static readonly string ProcessingVehicleImages = $"{_Processing} {_vehicle} {_images}.";
        public static readonly string VehicleImagesProcessed = $"{_Vehicle} {_images} {_processed}.";

        public static readonly string AttachingImageToVehicle = $"{_Attaching} {_image} {_to} \"{{0}}\".";
        public static readonly string ImageLocatedAndAttachedToVehicle = $"{_Image} {_located} {_and} {_attached} {_to} \"{{0}}\".";
        public static readonly string ImageNotFoundForVehicle = $"{_Image} {_not} {_found} {_for} \"{{0}}\".";

        public static readonly string WarThunderVersionNotInitialised = $"{_War} {_Thunder} {_version} {_not} {_initialised}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: game version. </para>
        /// </summary>
        public static readonly string FoundDatabaseFor = $"{_Found} {_databaseFor} {{0}}.";
        public static readonly string FoundDatabaseMatchesWithWarThunderVersion = $"{_Found} {_database} {_matches} {_War} {_Thunder} {_version}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: game version. </para>
        /// </summary>
        public static readonly string NotFoundDatabaseFor = $"{_Not} {_found} {_databaseFor} {{0}}.";
        public static readonly string DatabaseNotFound = $"{_Database} {_not} {_found}.";

        public static readonly string CreatingBlankDatabase = $"{_Creating} {_blank} {_database}.";
        public static readonly string BlankDatabaseCreated = $"{_Blank} {_database} {_created}.";
        public static readonly string DatabaseConnectionEstablished = $"{_Database} {_connection} {_established}.";

        public static readonly string InitialisingDatabase = $"{_Initialising} {_database}.";
        public static readonly string DatabaseInitialized = $"{_Database} {_initialised}.";

        public static readonly string CachingObjects = $"{_Caching} {_objects}.";
        public static readonly string CachingComplete = $"{_Caching} {_complete}.";

        public static readonly string InitialisingResearchTrees = InitialisingInstance.FormatFluently($"{_research} {_trees}");
        public static readonly string ResearchTreesInitialized = InstanceInitialised.FormatFluently($"{_Research} {_trees}");

        private static readonly string _vehicleUsageStatisticsFromThunderSkill = $" {_vehicle} {_usage} {_statistics} {_from} {_ThunderSkill}.";
        public static readonly string ReadingVehicleUsageStatisticsFromThunderSkill = $"{_Reading}{_vehicleUsageStatisticsFromThunderSkill}";
        public static readonly string FinishedReadingVehicleUsageStatisticsFromThunderSkill = $"{_Finished} {_reading}{_vehicleUsageStatisticsFromThunderSkill}";
        public static readonly string FailedToReadVehicleUsageStatisticsFromThunderSkill = $"{_Failed} {_to} {_read}{_vehicleUsageStatisticsFromThunderSkill}{_See} {_logs} {_for} {_details}.";

        public static readonly string AggregatingVehicleUsageStatistics = $"{_Aggregating} {_vehicle} {_usage} {_statistics}.";
        public static readonly string FinishedAggregatingVehicleUsageStatistics = $"{_Finished} {_aggregating} {_vehicle} {_usage} {_statistics}.";
        public static readonly string NoUsefulStatisticsHaveBeenFound = $"{_No} {_useful} {_statistics} {_have} {_been} {_found}.";

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
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object </para>
        /// </summary>
        public static readonly string NoEconomiRankSetForVehicleInGameMode = $"{_Economic} {_rank} {_is} {_set} {_for} {{0}}.";
    }
}