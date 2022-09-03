using Core.Organization.Enumerations.Logger;

namespace Client.Wpf.Enumerations.Logger
{
    public class EWpfClientLogMessage : EOrganizationLogMessage
    {
        #region WpfClient

        public static readonly string SettingsFileRenegenetated_Closing = $"Settings file regenerated for the new version. Closing the application.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: language. </para>
        /// </summary>
        public static readonly string LocalizationFileNotFound_ShowingLocalizationDialog = $"The localisation file for \"{{0}}\" not found. Showing the localisation dialog.";
        public static readonly string InitializationCancelled_ClosingApplication = $"Initialisation cancelled. Closing the application.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: tag type. </para>
        /// </summary>
        public static readonly string TagTypeNotSupportedYet = $"\"{{0}}\" tags are not supported yet.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: vehicle Gaijin ID. </para>
        /// </summary>
        public static readonly string ImageByteArrayReadFromVehicleIsNull = $"Image byte array read from \"{{0}}\" is NULL.";

        #endregion WpfClient
    }
}