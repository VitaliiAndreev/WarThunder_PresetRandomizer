using Core.Enumerations.Logger;

namespace Core.Localization.Enumerations.Logger
{
    /// <summary> Log message strings related to the "<see cref="Localization"/>" assembly. </summary>
    public class ELocalizationLogMessage : ECoreLogMessage
    {
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: localization language name. </para>
        /// </summary>
        public static string LocalizationLanguageNotRecognized => $"{_Localization} {_language} \"{{0}}\" {_not} {_recognized}.";
    }
}