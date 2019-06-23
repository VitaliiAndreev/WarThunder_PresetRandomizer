using Core.Helpers.Logger.Enumerations;

namespace Client.Console.Enumerations.Logger
{
    public class EConsoleUiLogMessage : ECoreLogMessage
    {
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: game version. </para>
        /// </summary>
        public const string FoundDatabaseFor = _Found + _database + _for + _SPC_FMT + _FS;
        /// <summary> 
        /// A message with formatting placeholders.
        /// <para> 1: game version. </para>
        /// </summary>
        public const string NotFoundDatabaseFor = _Not + _found + _database + _for + _SPC_FMT + _FS;
    }
}