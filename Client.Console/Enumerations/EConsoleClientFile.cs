using Core.Enumerations;

namespace Client.Console.Enumerations
{
    public partial class EConsoleClientFile
    {
        public const string Settings = nameof(Client) + CharacterString.Period + nameof(Console) + CharacterString.Period + nameof(Core.WarThunderExtractionToolsIntegration.Settings) + CharacterString.Period + FileExtension.Xml;
    }
}