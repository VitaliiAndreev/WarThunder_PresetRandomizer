namespace Core.Localization.Enumerations.Logger
{
    /// <summary> Log message strings related to the "<see cref="Core"/>" assembly. </summary>
    public class ELocalisationLogMessage : CoreLogMessage
    {
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: localization key. </para>
        /// </summary>
        public static readonly string LocalisationKeyNotFound = $"{_Localisation} {_key} \"{{0}}\" {_not} {_found}.";
    }
}