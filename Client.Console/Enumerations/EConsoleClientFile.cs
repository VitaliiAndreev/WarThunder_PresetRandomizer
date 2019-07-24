using Core.Enumerations;

namespace Client.Console.Enumerations
{
    public partial class EConsoleClientFile
    {
        public const string Settings = nameof(Client) + ECharacterString.Period + nameof(Console) + ECharacterString.Period + nameof(Core.WarThunderExtractionToolsIntegration.Settings) + ECharacterString.Period + EFileExtension.Xml;
    }
}