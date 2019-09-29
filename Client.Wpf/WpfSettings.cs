using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.Extensions;
using Core.Localization.Enumerations;
using Core.UnpackingToolsIntegration.Attributes;
using Core.WarThunderExtractionToolsIntegration;
using System.Collections.Generic;
using System.Linq;

namespace Client.Wpf
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
            set => LocalizationLanguage = !string.IsNullOrWhiteSpace(value) && value.TryParseEnumeration<ELanguage>(out var language) ? language : ELanguage.None;
        }

        [RequiredSetting]
        public static string CurrentGameMode
        {
            get => CurrentGameModeAsEnumerationItem.ToString();
            set => CurrentGameModeAsEnumerationItem = value.TryParseEnumeration<EGameMode>(out var enumerationItem) ? enumerationItem : EGameMode.Arcade;
        }

        [RequiredSetting]
        public static string EnabledBranches
        {
            get => EnabledBranchesCollection.StringJoin(Separator);
            set => EnabledBranchesCollection = value
                .Split(Separator)
                .Select(branchString => branchString.TryParseEnumeration<EBranch>(out var branch) ? branch : EBranch.None)
                .Where(branch => branch != EBranch.None).ToList()
            ;
        }

        [RequiredSetting]
        public static string EnabledNations
        {
            get => EnabledNationsCollection.StringJoin(Separator);
            set => EnabledNationsCollection = value
                .Split(Separator)
                .Select(nationString => nationString.TryParseEnumeration<ENation>(out var nation) ? nation : ENation.None)
                .Where(nation => nation != ENation.None).ToList()
            ;
        }

        /// <summary>
        /// The currently selected localization language.
        /// <para> The value of this property is not being saved to <see cref="EWpfClientFile.Settings"/> file. For that refer to <see cref="Localization"/> instead. </para>
        /// </summary>
        public static ELanguage LocalizationLanguage { get; private set; }

        /// <summary>
        /// The currently selected game mode.
        /// <para> The value of this property is not being saved to <see cref="EWpfClientFile.Settings"/> file. For that refer to <see cref="CurrentGameMode"/> instead. </para>
        /// </summary>
        public static EGameMode CurrentGameModeAsEnumerationItem { get; private set; }

        /// <summary>
        /// The currently enabled vehicle branches.
        /// <para> The value of this property is not being saved to <see cref="EWpfClientFile.Settings"/> file. For that refer to <see cref="EnabledBranches"/> instead. </para>
        /// </summary>
        public static IEnumerable<EBranch> EnabledBranchesCollection { get; private set; }

        /// <summary>
        /// The currently enabled nations.
        /// <para> The value of this property is not being saved to <see cref="EWpfClientFile.Settings"/> file. For that refer to <see cref="EnabledNations"/> instead. </para>
        /// </summary>
        public static IEnumerable<ENation> EnabledNationsCollection { get; private set; }

        #endregion Properties
    }
}