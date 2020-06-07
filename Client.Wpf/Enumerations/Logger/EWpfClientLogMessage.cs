using Core.Organization.Enumerations.Logger;

namespace Client.Wpf.Enumerations.Logger
{
    /// <summary> Log message strings related to the "<see cref="Wpf"/>" assembly. </summary>
    public class EWpfClientLogMessage : EOrganizationLogMessage
    {
        #region WpfClient

        protected static readonly string _closingTheApplication = $"{_Closing} {_the} {_application}.";

        public static readonly string SettingsFileRenegenetated_Closing = $"{_Settings} {_file} {_regenerated} {_for} {_the} {_new} {_version}. {_closingTheApplication}";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: language. </para>
        /// </summary>
        public static readonly string LocalizationFileNotFound_ShowingLocalizationDialog = $"{_The} {_localisation} {_file} {_for} \"{{0}}\" {_not} {_found}. {_Showing} {_the} {_localisation} {_dialog}.";
        public static readonly string InitializationCancelled_ClosingApplication = $"{_Initialisation} {_cancelled}. {_closingTheApplication}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: tag type. </para>
        /// </summary>
        public static readonly string TagTypeNotSupportedYet = $"\"{{0}}\" {_tags} {_are} {_not} {_supported} {_yet}.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: vehicle Gaijin ID. </para>
        /// </summary>
        public static readonly string ImageByteArrayReadFromVehicleIsNull = $"{_Image} {_byte} {_array} {_read} {_from} \"{{0}}\" {_is} {_NULL}.";

        #endregion WpfClient
    }
}