using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects.Connectors;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using Core.Extensions;
using Core.Objects;
using Core.Organization.Enumerations;
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
        public static string Randomisation
        {
            get => RandomisationAsEnumerationItem.ToString();
            set => RandomisationAsEnumerationItem = value.TryParseEnumeration<ERandomisation>(out var enumerationItem) ? enumerationItem : ERandomisation.CategoryBased;
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
                .Where(branch => branch.IsValid())
                .ToList()
            ;
        }

        [RequiredSetting]
        public static string EnabledVehicleBranchTags
        {
            get => EnabledVehicleBranchTagsCollection.StringJoin(Separator);
            set => EnabledVehicleBranchTagsCollection = value
                .Split(Separator)
                .Select(vehicleBranchTagString => vehicleBranchTagString.TryParseEnumeration<EVehicleBranchTag>(out var vehicleBranchTag) ? vehicleBranchTag : EVehicleBranchTag.None)
                .Where(vehicleBranchTag => vehicleBranchTag.IsValid())
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
                .Where(vehicleClass => vehicleClass.IsValid())
                .ToList()
            ;
        }

        [RequiredSetting]
        public static string EnabledVehicleSubclasses
        {
            get => EnabledVehicleSubclassesCollection.StringJoin(Separator);
            set => EnabledVehicleSubclassesCollection = value
                .Split(Separator)
                .Select(vehicleSubclassString => vehicleSubclassString.TryParseEnumeration<EVehicleSubclass>(out var vehicleSubclass) ? vehicleSubclass : EVehicleSubclass.None)
                .Where(vehicleSubclass => vehicleSubclass.IsValid())
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
                .Where(vehicleClass => vehicleClass.IsValid())
                .ToList()
            ;
        }

        [RequiredSetting]
        public static string EnabledCountries
        {
            get => EnabledCountriesCollection.StringJoin(Separator);
            set => EnabledCountriesCollection = value
                .Split(Separator)
                .Select(@string => new NationCountryPair(@string))
                .Where(nationCountryPair => nationCountryPair.Nation != ENation.None && nationCountryPair.Country != ECountry.None).ToList()
            ;
        }

        [RequiredSetting]
        public static string EnabledRanks
        {
            get => EnabledRanksCollection.StringJoin(Separator);
            set => EnabledRanksCollection = value
                .Split(Separator)
                .Select(rankString => rankString.TryParseEnumeration<ERank>(out var rank) ? rank : ERank.None)
                .Where(rank => rank.IsValid())
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
                        .Where(nation => nation.IsValid())
                    ,
                    (interval, enumerationItem) => new { EnumerationItem = enumerationItem, Interval = interval }
                )
                .ToDictionary(item => item.EnumerationItem, item => item.Interval)
            ;
        }

        [RequiredSetting]
        public static string EnabledVehicles
        {
            get => EnabledVehiclesCollection.StringJoin(Separator);
            set => EnabledVehiclesCollection = value.Split(Separator);
        }

        [RequiredSetting]
        public static string IncludeHeadersOnRowCopy
        {
            get => IncludeHeadersOnRowCopyFlag.ToString();
            set => IncludeHeadersOnRowCopyFlag = bool.TryParse(value, out var flagValue) && flagValue;
        }

        /// <summary>
        /// The currently selected localization language.
        /// <para> The value of this property is not being saved to <see cref="EWpfClientFile.Settings"/> file. For that refer to <see cref="Localization"/> instead. </para>
        /// </summary>
        public static ELanguage LocalizationLanguage { get; private set; }

        /// <summary>
        /// The currently selected randomisation method.
        /// <para> The value of this property is not being saved to <see cref="EWpfClientFile.Settings"/> file. For that refer to <see cref="Randomisation"/> instead. </para>
        /// </summary>
        public static ERandomisation RandomisationAsEnumerationItem { get; private set; }

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
        /// Currently enabled vehicle branch tags.
        /// <para> The value of this property is not being saved to <see cref="EWpfClientFile.Settings"/> file. For that refer to <see cref="EnabledVehicleBranchTags"/> instead. </para>
        /// </summary>
        public static IEnumerable<EVehicleBranchTag> EnabledVehicleBranchTagsCollection { get; private set; }

        /// <summary>
        /// Currently enabled vehicle classes.
        /// <para> The value of this property is not being saved to <see cref="EWpfClientFile.Settings"/> file. For that refer to <see cref="EnabledVehicleClasses"/> instead. </para>
        /// </summary>
        public static IEnumerable<EVehicleClass> EnabledVehicleClassesCollection { get; private set; }

        /// <summary>
        /// Currently enabled vehicle subclasses.
        /// <para> The value of this property is not being saved to <see cref="EWpfClientFile.Settings"/> file. For that refer to <see cref="EnabledVehicleSubclasses"/> instead. </para>
        /// </summary>
        public static IEnumerable<EVehicleSubclass> EnabledVehicleSubclassesCollection { get; private set; }

        /// <summary>
        /// Currently enabled nations.
        /// <para> The value of this property is not being saved to <see cref="EWpfClientFile.Settings"/> file. For that refer to <see cref="EnabledNations"/> instead. </para>
        /// </summary>
        public static IEnumerable<ENation> EnabledNationsCollection { get; private set; }

        /// <summary>
        /// Currently enabled countries.
        /// <para> The value of this property is not being saved to <see cref="EWpfClientFile.Settings"/> file. For that refer to <see cref="EnabledCountries"/> instead. </para>
        /// </summary>
        public static IEnumerable<NationCountryPair> EnabledCountriesCollection { get; private set; }

        /// <summary>
        /// Currently enabled nations.
        /// <para> The value of this property is not being saved to <see cref="EWpfClientFile.Settings"/> file. For that refer to <see cref="EnabledRanks"/> instead. </para>
        /// </summary>
        public static IEnumerable<ERank> EnabledRanksCollection { get; private set; }

        /// <summary>
        /// Currently enabled <see cref="IVehicle.EconomicRank"/>s.
        /// <para> The value of this property is not being saved to <see cref="EWpfClientFile.Settings"/> file. For that refer to <see cref="EnabledEconomicRanks"/> instead. </para>
        /// </summary>
        public static IDictionary<ENation, Interval<int>> EnabledEconomicRankIntervals { get; private set; }

        /// <summary>
        /// Currently enabled vehicle Gaijin IDs.
        /// <para> The value of this property is not being saved to <see cref="EWpfClientFile.Settings"/> file. For that refer to <see cref="EnabledVehicles"/> instead. </para>
        /// </summary>
        public static IEnumerable<string> EnabledVehiclesCollection { get; private set; }

        public static bool IncludeHeadersOnRowCopyFlag { get; private set; }

        #endregion Properties
    }
}