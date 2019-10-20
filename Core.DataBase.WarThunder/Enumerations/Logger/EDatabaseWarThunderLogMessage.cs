using Core.DataBase.Enumerations.Logger;

namespace Core.DataBase.WarThunder.Enumerations.Logger
{
    /// <summary> Log message strings related to the "Core.Database.<see cref="WarThunder"/>" assembly. </summary>
    public class EDatabaseWarThunderLogMessage : EDatabaseLogMessage
    {
        /// <summary>
        /// A message with formatting placeholders.
        /// <para> 1: the invalid string. </para>
        /// </summary>
        public static string NationCountryFormatIsInvalid { get; } = $"{_The} {_nation}-{_country} {_string} {_format} {_of} \"{{0}}\" {_is} {_invalid}. {_There} {_must} {_be} {_two} {_substrings} {_separated} {_by} {_an} {_underscore}.";
    }
}