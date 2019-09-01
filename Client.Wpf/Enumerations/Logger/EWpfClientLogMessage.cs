using Core.Organization.Enumerations.Logger;

namespace Client.Wpf.Enumerations.Logger
{
    /// <summary> Log message strings related to the "<see cref="Wpf"/>" assembly. </summary>
    public class EWpfClientLogMessage : EOrganizationLogMessage
    {
        #region WpfClient

        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: language. </para>
        /// </summary>
        public static readonly string LocalizationFileNotFound_ShowingLocalizationDialog = $"{_The} {_localization} {_file} {_for} \"{{0}}\" {_not} {_found}. {_Showing} {_the} {_localization} {_dialog}.";
        public static readonly string InitializationCancelled_ClosingApplication = $"{_Initialization} {_cancelled}. {_Closing} {_the} {_application}.";

        #endregion WpfClient
    }
}