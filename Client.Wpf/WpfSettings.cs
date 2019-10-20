using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using Core.Extensions;
using Core.Localization.Enumerations;
using Core.Objects;
using Core.UnpackingToolsIntegration.Attributes;
using Core.WarThunderExtractionToolsIntegration;
using System;
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
                .Where(branch => branch != EBranch.None)
                .ToList()
            ;
        }

        [RequiredSetting]
        public static string EnabledVehicleClasses
        {
            get => EnabledVehicleClassesCollection.StringJoin(Separator);
            set => EnabledVehicleClassesCollection = value
                .Split(Separator)
                .Select(vehicleClassString => vehicleClassString.TryParseEnumeration<EVehicleClass>(out var vehicleClass) ? vehicleClass : EVehicleClass.None)
                .Where(vehicleClass => vehicleClass != EVehicleClass.None)
                .ToList()
            ;
        }

        [RequiredSetting]
        public static string EnabledNations
        {
            get => EnabledNationsCollection.StringJoin(Separator);
            set => EnabledNationsCollection = value
                .Split(Separator)
                .Select(nationString => nationString.TryParseEnumeration<ENation>(out var nation) ? nation : ENation.None)
                .Where(nation => nation != ENation.None)
                .ToList()
            ;
        }

        [RequiredSetting]
        public static string EnabledEconomicRanks
        {
            get => EnabledEconomicRankIntervals.Values.Select(interval => $"{interval.LeftItem}-{interval.RightBounded}").StringJoin(Separator);
            set => EnabledEconomicRankIntervals = value
                .Split(Separator)
                .Select
                (
                    substring =>
                    {
                        var economicRanks = substring
                            .Split(ECharacter.Minus)
                            .Select(economicRankString => int.Parse(economicRankString))
                        ;
                        return new Interval<int>(true, economicRanks.First(), economicRanks.Last(), true);
                    }
                )
                .Zip
                (
                    Enum
                        .GetValues(typeof(ENation))
                        .Cast<ENation>()
                        .Where(enumerationItem => enumerationItem != ENation.None)
                    ,
                    (interval, enumerationItem) => new { EnumerationItem = enumerationItem, Interval = interval }
                )
                .ToDictionary(item => item.EnumerationItem, item => item.Interval)
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
        /// Currently enabled vehicle branches.
        /// <para> The value of this property is not being saved to <see cref="EWpfClientFile.Settings"/> file. For that refer to <see cref="EnabledBranches"/> instead. </para>
        /// </summary>
        public static IEnumerable<EBranch> EnabledBranchesCollection { get; private set; }

        /// <summary>
        /// Currently enabled vehicle classes.
        /// <para> The value of this property is not being saved to <see cref="EWpfClientFile.Settings"/> file. For that refer to <see cref="EnabledVehicleClasses"/> instead. </para>
        /// </summary>
        public static IEnumerable<EVehicleClass> EnabledVehicleClassesCollection { get; private set; }

        /// <summary>
        /// Currently enabled nations.
        /// <para> The value of this property is not being saved to <see cref="EWpfClientFile.Settings"/> file. For that refer to <see cref="EnabledNations"/> instead. </para>
        /// </summary>
        public static IEnumerable<ENation> EnabledNationsCollection { get; private set; }
        
        /// <summary>
        /// Currently enabled <see cref="IVehicle.EconomicRank"/>s.
        /// <para> The value of this property is not being saved to <see cref="EWpfClientFile.Settings"/> file. For that refer to <see cref="EnabledEconomicRanks"/> instead. </para>
        /// </summary>
        public static IDictionary<ENation, Interval<int>> EnabledEconomicRankIntervals { get; private set; }

        #endregion Properties
    }
}