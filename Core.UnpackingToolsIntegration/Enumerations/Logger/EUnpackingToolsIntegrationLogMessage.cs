using Core.Enumerations.Logger;

namespace UnpackingToolsIntegration.Enumerations.Logger
{
    /// <summary> Log message strings related to the "<see cref="UnpackingToolsIntegration"/>" assembly. </summary>
    public class EUnpackingToolsIntegrationLogMessage : ECoreLogMessage
    {
        #region Parser

        public static readonly string ReadingClientVersion = $"{_Reading} {_the} {_client} {_version}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: client version. </para>
        /// </summary>
        public static readonly string ClientVersionIs = $"{_Client} {_version} {_is} {{0}}.";

        public static readonly string VersionNotFoundInSourceString = $"{_Client} {_version} {_markers} {_not} {_found} {_in} {_the} {_source} {_string}.";
        public static readonly string ErrorReadingRawInstallData = $"{_Error} {_reading} {_raw} {_install} {_data}.";
        public static readonly string ErrorParsingVersionString = $"{_Error} {_parsing} {_version} {_string}.";

        #endregion Parser
        #region Unpacker

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file name. </para>
        /// </summary>
        public static readonly string PreparingToUnpack = $"{_Preparing} {_to} {_unpack} \"{{0}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file name. </para>
        /// </summary>
        public static readonly string Unpacking = $"{_Unpacking} \"{{0}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file name. </para>
        /// </summary>
        public static readonly string Unpacked = $"{_Unpacked} \"{{0}}\".";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: unpacking tool file name. </para>
        /// </summary>
        public static readonly string UnpackingToolSelected = $"{{0}} {_selected}.";

        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file extension. </para>
        /// </summary>
        public static readonly string FileExtensionNotSupportedByUnpackingTools = $"{_No} {_unpacking} {_tools} {_found} {_for} \"{{0}}\" {_files}.";
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: file extension. </para>
        /// </summary>
        public static readonly string OutputPathGenerationForFileExtensionNotYetImplemented = $"{_Output} {_path} {_generation} {_for} \"{{0}}\" {_files} {_is} {_not} {_yet} {_implemented}.";
        public static readonly string ErrorMatchingUnpakingToolToFileExtension = $"{_Error} {_matching} {_an} {_unpacking} {_tool} {_to} {_a} {_file} {_extension}.";
        public static readonly string ErrorRunningUnpackingTool = $"{_Error} {_running} {_the} {_unpacking} {_tool}.";

        #endregion Unpacker
    }
}