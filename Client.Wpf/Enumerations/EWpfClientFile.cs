using Core;

namespace Client.Wpf.Enumerations
{
    public partial class EWpfClientFile
    {
        public static readonly string Settings = $"{nameof(Client)}{CharacterString.Period}{nameof(Wpf)}{CharacterString.Period}{nameof(Core.WarThunderExtractionToolsIntegration.Settings)}{CharacterString.Period}{FileExtension.Xml}";
    }
}