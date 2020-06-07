﻿using Core.Enumerations;

namespace Client.Wpf.Enumerations
{
    public partial class EWpfClientFile
    {
        public static readonly string Settings = $"{nameof(Client)}{ECharacterString.Period}{nameof(Wpf)}{ECharacterString.Period}{nameof(Core.WarThunderExtractionToolsIntegration.Settings)}{ECharacterString.Period}{EFileExtension.Xml}";
    }
}