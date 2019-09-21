using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.Extensions;
using Core.Localization.Enumerations;
using Core.UnpackingToolsIntegration.Attributes;
using System;

namespace Core.WarThunderExtractionToolsIntegration
{
    /// <summary>
    /// Stores currently loaded settings for the application.
    /// If these settings are imported from XML files, XML node names must match property names here.
    /// </summary>
    public class WpfSettings : Settings
    {
        #region Properties

        [RequiredSetting]
        public static string Localization
        {
            get => LocalizationLanguage == ELanguage.None ? null : LocalizationLanguage.ToString();
            set => LocalizationLanguage = string.IsNullOrWhiteSpace(value) ? ELanguage.None : Enum.Parse(typeof(ELanguage), value).CastTo<ELanguage>();
        }

        [RequiredSetting]
        public static string CurrentGameMode { get; set; }

        /// <summary>
        /// The currently selected localization language.
        /// <para> The value of this property is not being saved to <see cref="EWpfClientFile.Settings"/> file. For that refer to <see cref="Localization"/> instead. </para>
        /// </summary>
        public static ELanguage LocalizationLanguage { get; private set; }

        #endregion Properties
    }
}