using Core.DataBase.Enumerations.Logger;

namespace Core.DataBase.WarThunder.Enumerations.Logger
{
    public class EDatabaseWarThunderLogMessage : EDatabaseLogMessage
    {
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: the invalid string. </para>
        /// </summary>
        public static string NationCountryFormatIsInvalid { get; } = $"The nation-country string format of \"{{0}}\" is invalid. There must be 2 substrings separated by an underscore.";
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: the invalid string. </para>
        /// </summary>
        public static string NeedMoreSubclassSlots { get; } = $"Need more vehicle subclass slots for \"{{0}}\".";
    }
}