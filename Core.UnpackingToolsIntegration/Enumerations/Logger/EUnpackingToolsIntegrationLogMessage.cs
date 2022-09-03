namespace UnpackingToolsIntegration.Enumerations.Logger
{
    public class EUnpackingToolsIntegrationLogMessage
    {
        #region Parser

        public static readonly string ReadingClientVersion = $"Reading the client version.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: client version. </para>
        /// </summary>
        public static readonly string ClientVersionIs = $"Client version is {{0}}.";

        public static readonly string VersionNotFoundInSourceString = $"Client version markers not found in the source string.";
        public static readonly string ErrorReadingRawInstallData = $"Error reading raw install data.";
        public static readonly string ErrorParsingVersionString = $"Error parsing version string.";

        #endregion Parser
        #region Unpacker

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file name. </para>
        /// </summary>
        public static readonly string PreparingToUnpack = $"Preparing to unpack \"{{0}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file name. </para>
        /// </summary>
        public static readonly string Unpacking = $"Unpacking \"{{0}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file name. </para>
        /// </summary>
        public static readonly string Unpacked = $"Unpacked \"{{0}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: unpacking tool file name. </para>
        /// </summary>
        public static readonly string UnpackingToolSelected = $"{{0}} selected.";

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file extension. </para>
        /// </summary>
        public static readonly string FileExtensionNotSupportedByUnpackingTools = $"No unpacking tools found for \"{{0}}\" files.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file extension. </para>
        /// </summary>
        public static readonly string OutputPathGenerationForFileExtensionNotYetImplemented = $"Output path generation for \"{{0}}\" files is not yet implemented.";
        public static readonly string ErrorMatchingUnpakingToolToFileExtension = $"Error matching an unpacking tool to a file extension.";
        public static readonly string ErrorRunningUnpackingTool = $"Error running the unpacking tool.";

        #endregion Unpacker
    }
}