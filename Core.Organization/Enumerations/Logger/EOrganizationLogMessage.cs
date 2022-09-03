using Core.Json.WarThunder.Enumerations.Logger;

namespace Core.Organization.Enumerations.Logger
{
    public class EOrganizationLogMessage : EJsonWarThunderLogMessage
    {
        public static readonly string ClearingTempDirectory = $"Clearing temp folder.";
        public static readonly string TempDirectoryCleared = $"Temp folder cleared.";

        public static readonly string SeveralCacheDirectoriesFound = $"Several cache directories matching the given version have been found: {{0}}.";

        public static readonly string PreparingGameFiles = $"Preparing game files.";
        public static readonly string GameFilesPrepared = $"Game files prepared.";

        public static readonly string PreparingGameDataFiles = $"Preparing game data files.";
        public static readonly string GameDataFilesPrepared = $"Game data files prepared.";

        public static readonly string PreparingGameImageFiles = $"Preparing game image files.";
        public static readonly string GameImageFilesPrepared = $"Game image files prepared.";

        public static readonly string PreparingBlkxFiles = $"Preparing {FileExtension.Blkx.ToUpper()} files.";
        public static readonly string BlkxFilesPrepared = $"{FileExtension.Blkx.ToUpper()} files prepared.";

        public static readonly string PreparingCsvFiles = $"Preparing {FileExtension.Csv.ToUpper()} files.";
        public static readonly string CsvFilesPrepared = $"{FileExtension.Csv.ToUpper()} files prepared.";

        public static readonly string PreparingVehicleIcons = $"Preparing vehicle icons files.";
        public static readonly string VehicleIconsPrepared = $"Vehicle icons prepared.";

        public static readonly string PreparingVehiclePortraits = $"Preparing vehicle portraits files.";
        public static readonly string VehiclePortraitsPrepared = $"Vehicle portraits prepared.";

        public static readonly string DeserialisingMainVehicleData = $"Deserialising main vehicle data.";
        public static readonly string MainVehicleDataDeserialised = $"Main vehicle data deserialised.";

        public static readonly string DeserialisingAdditionalVehicleData = $"Deserialising additional vehicle data.";
        public static readonly string AdditionalVehicleDataDeserialised = $"Additional vehicle data deserialised.";

        public static readonly string DeserialisingResearchTrees = $"Deserialising research trees.";
        public static readonly string ResearchTreesDeserialised = $"Research trees deserialised.";

        public static readonly string DeserialisingVehicleLocalisation = $"Deserialising vehicle localisation.";
        public static readonly string VehicleLocalisationDeserialised = $"vehicle localisation deserialised.";

        public static readonly string DeserialisingGameFiles = $"Deserialising game files.";
        public static readonly string DeserialisationComplete = $"Deserialisation complete.";

        public static readonly string InitialisingVehicles = $"Initialising vehicles.";
        public static readonly string VehiclesInitialised = $"Vehicles initialised.";

        public static readonly string ProcessingVehicleImages = $"Processing vehicle images.";
        public static readonly string VehicleImagesProcessed = $"vehicle images processed.";

        public static readonly string AttachingImageToVehicle = $"Attaching image to \"{{0}}\".";
        public static readonly string ImageLocatedAndAttachedToVehicle = $"image located and attached to \"{{0}}\".";
        public static readonly string ImageNotFoundForVehicle = $"image not found for \"{{0}}\".";

        public static readonly string WarThunderVersionNotInitialised = $"War Thunder version not initialised.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: game version. </para>
        /// </summary>
        public static readonly string FoundDatabaseFor = $"Found database for {{0}}.";
        public static readonly string FoundDatabaseMatchesWithWarThunderVersion = $"Found database matches War Thunder version.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: game version. </para>
        /// </summary>
        public static readonly string NotFoundDatabaseFor = $"Not found database for {{0}}.";
        public static readonly string DatabaseNotFound = $"Database not found.";

        public static readonly string CreatingBlankDatabase = $"Creating blank database.";
        public static readonly string BlankDatabaseCreated = $"Blank database created.";
        public static readonly string DatabaseConnectionEstablished = $"Database connection established.";

        public static readonly string InitialisingDatabase = $"Initialising database.";
        public static readonly string DatabaseInitialized = $"Database initialised.";

        public static readonly string CachingObjects = $"Caching objects.";
        public static readonly string CachingComplete = $"Caching complete.";

        public static readonly string InitialisingResearchTrees = $"Initialising research trees.";
        public static readonly string ResearchTreesInitialized = "Research trees initialised.";
        
        public static readonly string ReadingVehicleUsageStatisticsFromThunderSkill = $"Reading vehicle usage statistics from ThunderSkill.";
        public static readonly string FinishedReadingVehicleUsageStatisticsFromThunderSkill = $"Finished reading vehicle usage statistics from ThunderSkill.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: error message. </para>
        /// </summary>
        public static readonly string FailedToReadVehicleUsageStatisticsFromThunderSkill = $"Failed to read vehicle usage statistics from ThunderSkill.: {{0}}";

        public static readonly string AggregatingVehicleUsageStatistics = $"Aggregating vehicle usage statistics.";
        public static readonly string FinishedAggregatingVehicleUsageStatistics = $"Finished aggregating vehicle usage statistics.";
        public static readonly string NoUsefulStatisticsHaveBeenFound = $"No useful statistics have been found.";

        public static readonly string DummyNationSelected = $"Dummy nation selected.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: main branch. </para>
        /// </summary>
        public static readonly string MainBranchHasNoVehicleClassesEnabled = $"Main branch \"{{0}}\" has no vehicle classes enabled.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: nation collection. </para>
        /// <para> 2: branch collection. </para>
        /// </summary>
        public static readonly string NationsHaveNoBranch = $"\"{{0}}\" don't/doesn't have \"{{1}}\".";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: filter criteria. </para>
        /// </summary>
        public static readonly string NoVehiclesAvailableFor = $"No vehicles available for \"{{0}}\".";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: filter criteria. </para>
        /// </summary>
        public static readonly string NoVehiclesSelected = $"No vehicles selected.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: branch. </para>
        /// <para> 2: nation. </para>
        /// </summary>
        public static readonly string NoVehiclesAvailableForSelectedBattleRatings = $"No vehicles available among {{0}}-{{1}} {{2}} of {{3}}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: object </para>
        /// </summary>
        public static readonly string NoEconomiRankSetForVehicleInGameMode = $"Economic rank is set for {{0}}.";
    }
}