using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Client.Wpf.Enumerations.ShrinkProfiles;
using Client.Wpf.Extensions;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects.Connectors;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using Core.Extensions;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for VehicleCountsStatisticsControl.xaml. </summary>
    public partial class VehicleCountsStatisticsControl : LocalisedUserControl
    {
        private double _savedVerticalScrollOffset = EInteger.Number.Zero;
        private double _savedHorizontalScrollOffset = EInteger.Number.Zero;

        #region Fields

        private bool _initialised;

        private StatisticsControl _statisticsControl;

        private readonly int _categoryHorizontalMargin;
        private readonly int _categoryVerticalMargin;
        private readonly int _internalDividerMarginRightMargin;

        private readonly Thickness _internalDividerMargin;
        private readonly Thickness _categoryColumnHeaderMarginDoubled;
        private readonly Thickness _categoryRowHeaderMargin;
        private readonly Thickness _categoryMargin;
        private readonly Thickness _categoryMarginDoubled;
        private readonly Thickness _categoryMarginRightSided;

        private readonly Style _categoryTextStyle;

        private readonly double _flagColumnWidth;

        private readonly double _countColumnWidthForThreeDigits;

        private readonly double _branchIconFontSize;

        private readonly double _availabilityCategoryIconFontSize;
        private readonly double _classIconFontSize;

        private readonly double _availabilityCategoryIconColumnWidth;
        private readonly double _classIconColumnWidth;

        #endregion Fields
        #region Constructors

        public VehicleCountsStatisticsControl()
        {
            InitializeComponent();

            _categoryHorizontalMargin = EInteger.Number.Five;
            _categoryVerticalMargin = EInteger.Number.Five;
            _internalDividerMarginRightMargin = EInteger.Number.Five;

            _internalDividerMargin = new Thickness(EInteger.Number.Zero, EInteger.Number.Zero, _internalDividerMarginRightMargin, EInteger.Number.Zero);
            _categoryColumnHeaderMarginDoubled = new Thickness(_categoryHorizontalMargin * EInteger.Number.Two, EInteger.Number.Zero, _categoryHorizontalMargin * EInteger.Number.Two, _categoryVerticalMargin);
            _categoryRowHeaderMargin = new Thickness(EInteger.Number.Zero, EInteger.Number.Zero, _categoryHorizontalMargin * EInteger.Number.Two, EInteger.Number.Zero);
            _categoryMargin = new Thickness(_categoryHorizontalMargin, EInteger.Number.Zero, _categoryHorizontalMargin, EInteger.Number.Zero);
            _categoryMarginDoubled = new Thickness(_categoryHorizontalMargin * EInteger.Number.Two, EInteger.Number.Zero, _categoryHorizontalMargin * EInteger.Number.Two, EInteger.Number.Zero);
            _categoryMarginRightSided = new Thickness(EInteger.Number.Zero, EInteger.Number.Zero, _categoryHorizontalMargin * EInteger.Number.Two, EInteger.Number.Zero);

            _categoryTextStyle = this.GetStyle(EStyleKey.TextBlock.TextBlock12px);

            _flagColumnWidth = EInteger.Number.Seventeen;

            _countColumnWidthForThreeDigits = EInteger.Number.Twenty;

            _branchIconFontSize = EInteger.Number.Twenty;

            _availabilityCategoryIconFontSize = EInteger.Number.Eighteen;
            _availabilityCategoryIconColumnWidth = EInteger.Number.Thirty;

            _classIconFontSize = EInteger.Number.Twelve;
            _classIconColumnWidth = EInteger.Number.Twenty;
        }

        #endregion Constructors
        #region Methods: Overrides

        public override void Localise()
        {
            base.Localise();

            _hint.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.ClickOnCategoryToOpenList);
            _vehiclesByAvailabilityHeader.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.VehiclesByAvailability);
            _vehiclesByNationsAndCountriesHeader.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.VehiclesByNationsAndCountries);
            _vehiclesByCountriesAndNationsHeader.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.VehiclesByCountriesAndNations);
            _vehiclesByBranchesAndNationsHeader.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.VehiclesByBranchesAndNations);
            _vehiclesByCountriesAndBranchesHeader.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.VehiclesByCountriesAndBranches);
            _vehiclesByBranchesAndClassesHeader.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.VehiclesByBranchesAndClasses);
            _vehiclesByTagsAndNationsHeader.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.VehiclesByTagsAndNations);
        }

        #endregion Methods: Overrides
        #region Methods: Initialisation

        public void Initialise(StatisticsControl statisticsControl)
        {
            if (!_initialised && statisticsControl is StatisticsControl)
            {
                _statisticsControl = statisticsControl;

                InitialiseCategories();

                _initialised = true;
            }
        }

        private void InitialiseCategories()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            var availabilityCategories = typeof(EVehicleAvailability).GetEnumerationItems<EVehicleAvailability>(true, true);
            var availabilityVehicleCounts = availabilityCategories
                .ToDictionary(availabilityCategory => availabilityCategory, availabilityCategory => new List<IVehicle>())
            ;
            var nations = typeof(ENation).GetEnumerationItems<ENation>(true);
            var nationCountryVehicleCounts = nations
                .SelectMany(nation => nation.GetNationCountryPairs())
                .ToDictionary(nationCountryPair => nationCountryPair, nationCountryPair => new List<IVehicle>())
            ;
            var branches = typeof(EBranch).GetEnumerationItems<EBranch>(true);
            var nationBranchVehicleCounts = branches
                .SelectMany(branch => nations.Select(nation => new NationBranchPair(nation, branch)))
                .ToDictionary(nationBranchPair => nationBranchPair, nationBranchPair => new List<IVehicle>())
            ;
            var countries = typeof(ECountry).GetEnumerationItems<ECountry>(true);
            var countryBranchVehicleCounts = branches
                .SelectMany(branch => countries.Select(country => new BranchCountryPair(branch, country)))
                .ToDictionary(branchCountryPair => branchCountryPair, branchCountryPair => new List<IVehicle>())
            ;
            var branchClassVehicleCounts = branches
                .SelectMany(branch => branch.GetVehicleClasses().Select(vehicleClass => new BranchClassPair(branch, vehicleClass)))
                .ToDictionary(branchClassPair => branchClassPair, branchClassPair => new List<IVehicle>())
            ;
            var classes = typeof(EVehicleClass).GetEnumerationItems<EVehicleClass>(true);
            var countryClassVehicleCounts = nations
                .SelectMany(nation => classes.Select(vehicleClass => new NationClassPair(nation, vehicleClass)))
                .ToDictionary(nationClassPair => nationClassPair, nationClassPair => new List<IVehicle>())
            ;
            var tags = typeof(EVehicleBranchTag).GetEnumerationItems<EVehicleBranchTag>(true);
            var tagNationVehicleCounts = nations
                .SelectMany(nation => tags.Select(tag => new NationTagPair(nation, tag)))
                .ToDictionary(nationTagPair => nationTagPair, nationTagPair => new List<IVehicle>())
            ;

            foreach (var vehicle in ApplicationHelpers.Manager?.PlayableVehicles?.Values ?? new List<IVehicle>())
            {
                foreach (var availabilityCategory in vehicle.GetAvailabilityCategories())
                    vehicle.AddInto(availabilityVehicleCounts[availabilityCategory]);

                vehicle.AddInto(nationCountryVehicleCounts[new NationCountryPair(vehicle.Nation.AsEnumerationItem, vehicle.Country)]);
                vehicle.AddInto(nationBranchVehicleCounts[new NationBranchPair(vehicle.Nation.AsEnumerationItem, vehicle.Branch.AsEnumerationItem)]);
                vehicle.AddInto(countryBranchVehicleCounts[new BranchCountryPair(vehicle.Branch.AsEnumerationItem, vehicle.Country)]);
                vehicle.AddInto(branchClassVehicleCounts[new BranchClassPair(vehicle.Branch.AsEnumerationItem, vehicle.Class)]);
                vehicle.AddInto(countryClassVehicleCounts[new NationClassPair(vehicle.Nation.AsEnumerationItem, vehicle.Class)]);

                foreach (var tag in vehicle.Tags)
                    vehicle.AddInto(tagNationVehicleCounts[new NationTagPair(vehicle.Nation.AsEnumerationItem, tag)]);
            }

            InitialiseVehiclesByAvailability(availabilityVehicleCounts);
            InitialiseVehiclesByNationsAndCountries(nationCountryVehicleCounts);
            InitialiseVehiclesByCountriesAndNations(nationCountryVehicleCounts);
            InitialiseVehiclesByBranchesAndNations(nationBranchVehicleCounts);
            InitialiseVehiclesByCountriesAndBranches(countryBranchVehicleCounts);
            InitialiseVehiclesByBranchesAndClasses(branchClassVehicleCounts);
            InitialiseVehiclesByClassesAndNations(countryClassVehicleCounts);
            InitialiseVehiclesByTagsAndNations(tagNationVehicleCounts);

            PopulateVehiclesByAvailabilityControls(availabilityCategories);
            PopulateVehiclesByNationsAndCountriesControls(nations);
            PopulateVehiclesByCountriesAndNationsControls(nations);
            PopulateVehiclesByBranchesAndNationsControls(branches, nations);
            PopulateVehiclesByCountriesAndBranchesControls(countries, branches);
            PopulateVehiclesByBranchesAndClassesControls(branches);
            PopulateVehiclesByClassesAndNationsControls(classes, nations);
            PopulateVehiclesByTagsAndNationsControls(tags, nations);
        }

        private void InitialiseVehiclesByAvailability(IDictionary<EVehicleAvailability, List<IVehicle>> vehiclesCounts)
        {
            var vehiclesByAvailability = vehiclesCounts
                .Select(item => new KeyValuePair<EVehicleAvailability, IOrderedEnumerable<IVehicle>>(item.Key, item.Value.OrderByNationCountryBranchClassSubclassRankId()))
                .ToDictionary(item => item.Key, item => item.Value)
            ;
            _statisticsControl?.SetVehiclesByAvailability(vehiclesByAvailability);
        }

        private void InitialiseVehiclesByNationsAndCountries(IDictionary<NationCountryPair, List<IVehicle>> vehiclesCounts)
        {
            var vehiclesByNationsAndCountries = vehiclesCounts
                .OrderBy(item => item.Key.Nation)
                .ThenByDescending(item => item.Value.Count())
                .ThenBy(item => item.Key.Country)
                .Select(item => new KeyValuePair<NationCountryPair, IOrderedEnumerable<IVehicle>>(item.Key, item.Value.OrderByClassSubclassRankId()))
                .ToDictionary(item => item.Key, item => item.Value)
            ;
            _statisticsControl?.SetVehiclesByNationsAndCountries(vehiclesByNationsAndCountries);
        }

        private void InitialiseVehiclesByCountriesAndNations(IDictionary<NationCountryPair, List<IVehicle>> vehiclesCounts)
        {
            var vehiclesByCountriesAndNations = vehiclesCounts
                .OrderBy(item => item.Key.Country)
                .ThenBy(item => item.Key.Nation)
                .Select(item => new KeyValuePair<NationCountryPair, IOrderedEnumerable<IVehicle>>(item.Key, item.Value.OrderByClassSubclassRankId()))
                .ToDictionary(item => item.Key, item => item.Value)
            ;
            _statisticsControl?.SetVehiclesByCountriesAndNations(vehiclesByCountriesAndNations);
        }

        private void InitialiseVehiclesByBranchesAndNations(IDictionary<NationBranchPair, List<IVehicle>> vehiclesCounts)
        {
            var vehiclesByBranchesAndNations = vehiclesCounts
                .Select(item => new KeyValuePair<NationBranchPair, IOrderedEnumerable<IVehicle>>(item.Key, item.Value.OrderByClassSubclassRankId()))
                .ToDictionary(item => item.Key, item => item.Value)
            ;
            _statisticsControl?.SetVehiclesByBranchesAndNations(vehiclesByBranchesAndNations);
        }

        private void InitialiseVehiclesByCountriesAndBranches(IDictionary<BranchCountryPair, List<IVehicle>> vehiclesCounts)
        {
            var vehiclesByCountriesAndBranches = vehiclesCounts
                .Select(item => new KeyValuePair<BranchCountryPair, IOrderedEnumerable<IVehicle>>(item.Key, item.Value.OrderByClassSubclassRankId()))
                .ToDictionary(item => item.Key, item => item.Value)
            ;
            _statisticsControl?.SetVehiclesByCountriesAndBranches(vehiclesByCountriesAndBranches);
        }

        private void InitialiseVehiclesByBranchesAndClasses(IDictionary<BranchClassPair, List<IVehicle>> vehiclesCounts)
        {
            var vehiclesByBranchesAndClasses = vehiclesCounts
                .Select(item => new KeyValuePair<BranchClassPair, IOrderedEnumerable<IVehicle>>(item.Key, item.Value.OrderByNationSubclassRankId()))
                .ToDictionary(item => item.Key, item => item.Value)
            ;
            _statisticsControl?.SetVehiclesByBranchesAndClasses(vehiclesByBranchesAndClasses);
        }

        private void InitialiseVehiclesByClassesAndNations(IDictionary<NationClassPair, List<IVehicle>> vehiclesCounts)
        {
            var vehiclesByClassesAndNations = vehiclesCounts
                .Select(item => new KeyValuePair<NationClassPair, IOrderedEnumerable<IVehicle>>(item.Key, item.Value.OrderBySubclassRankId()))
                .ToDictionary(item => item.Key, item => item.Value)
            ;
            _statisticsControl?.SetVehiclesByClassesAndNations(vehiclesByClassesAndNations);
        }

        private void InitialiseVehiclesByTagsAndNations(IDictionary<NationTagPair, List<IVehicle>> vehiclesCounts)
        {
            var vehiclesByTagsAndNations = vehiclesCounts
                .Select(item => new KeyValuePair<NationTagPair, IOrderedEnumerable<IVehicle>>(item.Key, item.Value.OrderByClassSubclassRankId()))
                .ToDictionary(item => item.Key, item => item.Value)
            ;
            _statisticsControl?.SetVehiclesByTagsAndNations(vehiclesByTagsAndNations);
        }

        #endregion Methods: Initialisation
        #region Methods: Control Population

        #region Helpers

        #region Control Creation

        private Border CreateGridCell()
        {
            return new Border
            {
                BorderThickness = new Thickness(EInteger.Number.One, EInteger.Number.One, EInteger.Number.One, EInteger.Number.One),
                BorderBrush = new SolidColorBrush(Colors.Black) { Opacity = EDouble.Number.PointOne },
            };
        }

        private TextBlock CreateTextBlock<T>
        (
            string text,
            T tag,
            Thickness margin,
            MouseButtonEventHandler mouseDownHandler,
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left,
            TextAlignment textAlignment = TextAlignment.Left,
            double? width = null,
            bool isBold = false
        )
        {
            var textBlock = new TextBlock
            {
                Style = _categoryTextStyle,
                Margin = margin,
                HorizontalAlignment = horizontalAlignment,
                TextAlignment = textAlignment,
                FontWeight = isBold ? FontWeights.Bold : FontWeights.Normal,
                Text = text,
                Tag = tag,
            };

            if (width.HasValue)
                textBlock.Width = width.Value;

            textBlock.MouseDown += mouseDownHandler;

            return textBlock;
        }

        private TextBlock CreateTextBlock<T>
        (
            int number,
            T tag,
            Thickness margin,
            MouseButtonEventHandler mouseDownHandler,
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left,
            TextAlignment textAlignment = TextAlignment.Left,
            double? width = null,
            bool isBold = false
        )
        {
            return CreateTextBlock(number.ToString(), tag, margin, mouseDownHandler, horizontalAlignment, textAlignment, width, isBold);
        }

        private TextBlock CreateNameRowHeader<T>(T tag, MouseButtonEventHandler leftMouseDownHandler)
        {
            return CreateTextBlock(ApplicationHelpers.LocalisationManager.GetLocalisedString(tag.ToString()), tag, _categoryRowHeaderMargin, leftMouseDownHandler, isBold: true);
        }

        private TextBlock CreateBranchNameRowHeader(EBranch branch)
        {
            return CreateNameRowHeader(branch, OnVehiclesByBranchesLeftMouseDown);
        }

        private TextBlock CreateTagNameRowHeader(EVehicleBranchTag tag)
        {
            return CreateNameRowHeader(tag, OnVehiclesByTagsLeftMouseDown);
        }

        [SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "Implicit variable declarations.")]
        private TextLabelWithGaijinCharacterIcon CreateGaijinCharacterIconAndNameRowHeader<T, U>(T controlTag, U iconTag, Thickness margin, MouseButtonEventHandler leftMouseDownHandler)
        {
            var icon = default(char);
            var iconFontSize = default(double);

            if (iconTag is EVehicleClass vehicleClass)
            {
                icon = EReference.ClassIcons[vehicleClass];
                iconFontSize = _classIconFontSize;
            }
            else
            {
                throw new NotImplementedException();
            }

            return new TextLabelWithGaijinCharacterIcon(icon, ApplicationHelpers.LocalisationManager.GetLocalisedString(iconTag.ToString()), margin, leftMouseDownHandler, _classIconColumnWidth, iconFontSize: iconFontSize)
                .WithTag(controlTag)
            ;
        }

        private TextBlock CreateVehicleCountHeader<T>(int vehicleCount, T tag, MouseButtonEventHandler leftMouseDownHandler)
        {
            return CreateTextBlock(vehicleCount.ToString(), tag, _categoryRowHeaderMargin, leftMouseDownHandler, HorizontalAlignment.Right, isBold: true);
        }

        private TextBlock CreateVehicleCount<T>(int vehicleCount, T tag, MouseButtonEventHandler leftMouseDownHandler, bool useDoubleMargin)
        {
            return CreateTextBlock(vehicleCount, tag, useDoubleMargin ? _categoryMarginDoubled : _categoryMargin, leftMouseDownHandler, HorizontalAlignment.Right, TextAlignment.Right, _countColumnWidthForThreeDigits);
        }

        private TextLabelWithFlag CreateCountryFlagAndNameRowHeader(ECountry country)
        {
            return new TextLabelWithFlag(country, ApplicationHelpers.LocalisationManager.GetLocalisedString(country.ToString()), _categoryRowHeaderMargin, OnVehiclesByCountriesLeftMouseDown, isBold: true, createTooltip: false);
        }

        [SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "Implicit variable declarations.")]
        private VehicleCountWithGaijinCharacterIcon CreateVehicleCountWithGaijinCharacterIcon<T, U>(T controlTag, U iconTag, int vehicleCount, Thickness margin, MouseButtonEventHandler leftMouseDownHandler)
        {
            var icon = default(string);
            var iconIsBold = default(bool);
            var iconFontSize = default(double);
            var iconColumnWidth = default(double);
            var countColumnWidth = default(double?);

            if (iconTag is EVehicleAvailability availabilityCategory)
            {
                icon = EReference.VehicleAvailabilityIcons[availabilityCategory];
                iconIsBold = availabilityCategory == EVehicleAvailability.PurchasableInTheStore;
                iconFontSize = availabilityCategory switch
                {
                    EVehicleAvailability.All => _availabilityCategoryIconFontSize + EInteger.Number.One,
                    EVehicleAvailability.Researchable => _availabilityCategoryIconFontSize + EInteger.Number.Four,
                    EVehicleAvailability.ResearchableInSquadron => _availabilityCategoryIconFontSize + EInteger.Number.Four,
                    EVehicleAvailability.PurchasableForGoldenEagles => _availabilityCategoryIconFontSize + EInteger.Number.Two,
                    EVehicleAvailability.Premium => _availabilityCategoryIconFontSize + EInteger.Number.One,
                    _ => _availabilityCategoryIconFontSize
                };
                iconColumnWidth = _availabilityCategoryIconColumnWidth;
                countColumnWidth = default;
            }
            else if (iconTag is EVehicleClass vehicleClass)
            {
                icon = EReference.ClassIcons[vehicleClass].ToString();
                iconIsBold = false;
                iconFontSize = _classIconFontSize;
                iconColumnWidth = _classIconColumnWidth;
                countColumnWidth = _countColumnWidthForThreeDigits;
            }
            else
            {
                throw new NotImplementedException();
            }

            return new VehicleCountWithGaijinCharacterIcon(icon, iconTag, vehicleCount, margin, leftMouseDownHandler, iconColumnWidth, countColumnWidth, iconFontSize, iconIsBold: iconIsBold).WithTag(controlTag);
        }

        #endregion Control Creation
        #region Column Headers

        private void PopulateNationFlagColumnHeaders(IEnumerable<ENation> nations, Grid grid, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Right, double? width = null)
        {
            var nationHeaderIndex = EInteger.Number.Two;

            foreach (var nation in nations)
            {
                var nationHeader = new FlagControl(nation, _categoryColumnHeaderMarginDoubled, OnVehiclesByNationsLeftMouseDown, horizontalAlignment, width ?? _flagColumnWidth);

                grid.Add(nationHeader, nationHeaderIndex++, EInteger.Number.Zero);
            }
        }

        private void PopulateBranchIconColumnHeaders(IEnumerable<EBranch> branches, Grid grid, bool doubleMargin)
        {
            var nationHeaderIndex = EInteger.Number.Two;

            foreach (var branch in branches)
            {
                var branchHeader = new GaijinCharactreIconControl(EReference.BranchIcons[branch], branch, doubleMargin ? _categoryMarginDoubled : _categoryMargin, OnVehiclesByBranchesLeftMouseDown, _branchIconFontSize);

                grid.Add(branchHeader, nationHeaderIndex++, EInteger.Number.Zero);
            }
        }

        #endregion Column Headers
        #region Row Headers

        private void PopulateCountryFlagNameCountRowHeader(ECountry country, int vehicleCount, Grid grid, int rowIndex)
        {
            var header = CreateCountryFlagAndNameRowHeader(country);
            var count = CreateVehicleCountHeader(vehicleCount, country, OnVehiclesByCountriesLeftMouseDown);

            grid.Add(header, EInteger.Number.Zero, rowIndex);
            grid.Add(count, EInteger.Number.One, rowIndex);
        }

        private void PopulateBranchNameCountRowHeader(EBranch branch, int vehicleCount, Grid grid, int rowIndex)
        {
            var header = CreateBranchNameRowHeader(branch);
            var count = CreateVehicleCountHeader(vehicleCount, branch, OnVehiclesByBranchesLeftMouseDown);

            grid.Add(header, EInteger.Number.Zero, rowIndex);
            grid.Add(count, EInteger.Number.One, rowIndex);
        }

        private void PopulateVehicleClassIconAndNameRowHeader(EVehicleClass vehicleClass, int vehicleCount, Grid grid, int rowIndex)
        {
            var header = CreateGaijinCharacterIconAndNameRowHeader(vehicleClass, vehicleClass, _internalDividerMargin, OnVehiclesByClassLeftMouseDown);
            var count = CreateVehicleCountHeader(vehicleCount, vehicleClass, OnVehiclesByClassLeftMouseDown);

            grid.Add(header, EInteger.Number.Zero, rowIndex);
            grid.Add(count, EInteger.Number.One, rowIndex);
        }

        private void PopulateTagNameCountRowHeader(EVehicleBranchTag tag, int vehicleCount, Grid grid, int rowIndex)
        {
            var header = CreateTagNameRowHeader(tag);
            var count = CreateVehicleCountHeader(vehicleCount, tag, OnVehiclesByBranchesLeftMouseDown);

            grid.Add(header, EInteger.Number.Zero, rowIndex);
            grid.Add(count, EInteger.Number.One, rowIndex);
        }

        #endregion Row Headers

        #endregion Helpers
        #region Population

        private void PopulateVehiclesByAvailabilityControls(IEnumerable<EVehicleAvailability> availabilityCategories)
        {
            var vehiclesByAvailability = _statisticsControl.VehiclesByAvailability;
            var categoryIndex = EInteger.Number.Zero;

            foreach (var category in availabilityCategories)
            {
                var categoryControl = CreateVehicleCountWithGaijinCharacterIcon(category, category, vehiclesByAvailability[category].Count(), _categoryMarginRightSided, OnVehiclesByAvailabilityLeftMouseDown);

                _vehiclesByAvailabilityGrid.Add(categoryControl, categoryIndex++, EInteger.Number.Zero);
            }
        }

        private void PopulateVehiclesByNationsAndCountriesControls(IEnumerable<ENation> nations)
        {
            var vehiclesByNationsAndCountries = _statisticsControl.VehiclesByNationsAndCountries;
            var nationIndex = EInteger.Number.Zero;

            foreach (var nation in nations)
            {
                var nationKeys = vehiclesByNationsAndCountries.Keys.Where(key => key.Nation == nation);
                var header = CreateTextBlock
                (
                    ApplicationHelpers.LocalisationManager.GetLocalisedString(nation.ToString()),
                    nation,
                    _categoryRowHeaderMargin,
                    OnVehiclesByNationsLeftMouseDown,
                    isBold: true
                );
                var count = CreateTextBlock
                (
                    vehiclesByNationsAndCountries.Where(item => item.Key.IsIn(nationKeys)).Sum(item => item.Value.Count()).ToString(),
                    nation,
                    _categoryRowHeaderMargin,
                    OnVehiclesByNationsLeftMouseDown,
                    HorizontalAlignment.Right,
                    isBold: true
                );
                var countryIndex = EInteger.Number.Two;

                foreach (var nationKey in nationKeys)
                {
                    var countryControl = new VehicleCountWithFlag(nationKey, vehiclesByNationsAndCountries[nationKey].Count(), _categoryMargin, OnVehiclesByNationsAndCountriesLeftMouseDown);

                    _vehiclesByNationsAndCountriesGrid.Add(countryControl, countryIndex++, nationIndex);
                }
                _vehiclesByNationsAndCountriesGrid.Add(header, EInteger.Number.Zero, nationIndex);
                _vehiclesByNationsAndCountriesGrid.Add(count, EInteger.Number.One, nationIndex++);
            }
        }

        private void PopulateVehiclesByCountriesAndNationsControls(IEnumerable<ENation> nations)
        {
            var vehiclesByCountriesAndNations = _statisticsControl.VehiclesByCountriesAndNations;
            var countries = vehiclesByCountriesAndNations.Keys.Select(nationCountryPair => nationCountryPair.Country).Distinct();
            var countryIndex = EInteger.Number.One;

            PopulateNationFlagColumnHeaders(nations, _vehiclesByCountriesAndNationsGrid, HorizontalAlignment.Center, 17);

            foreach (var country in countries)
            {
                var countryKeys = vehiclesByCountriesAndNations.Keys.Where(key => key.Country == country);
                var countryVehicleCount = vehiclesByCountriesAndNations.Where(item => item.Key.IsIn(countryKeys)).Sum(item => item.Value.Count());
                var nationIndex = EInteger.Number.Two;
                var nationsByCountires = countryKeys.GroupBy(item => item.Country, item => item.Nation).ToDictionary(group => group.Key, group => group.AsEnumerable());

                foreach (var nation in nations)
                {
                    var nationsWithVehicles = nationsByCountires[country];
                    var cellWithBorder = CreateGridCell();

                    if (nation.IsIn(nationsWithVehicles))
                    {
                        var countryKey = countryKeys.First(item => item.Nation == nation);

                        new VehicleCountWithFlag
                        (
                            countryKey,
                            vehiclesByCountriesAndNations[countryKey].Count(),
                            _categoryMargin,
                            OnVehiclesByCountriesAndNationsLeftMouseDown,
                            useNationFlags: true
                        ).PutInto(cellWithBorder);
                    }
                    _vehiclesByCountriesAndNationsGrid.Add(cellWithBorder, nationIndex++, countryIndex);
                }
                PopulateCountryFlagNameCountRowHeader(country, countryVehicleCount, _vehiclesByCountriesAndNationsGrid, countryIndex++);
            }
        }

        private void PopulateVehiclesByBranchesAndNationsControls(IEnumerable<EBranch> branches, IEnumerable<ENation> nations)
        {
            var vehicleByBranchesAndCountries = _statisticsControl.VehiclesByBranchesAndNations;
            var branchIndex = EInteger.Number.One;

            PopulateNationFlagColumnHeaders(nations, _vehiclesByBranchesAndNationsGrid);

            foreach (var branch in branches)
            {
                var branchKeys = vehicleByBranchesAndCountries.Keys.Where(key => key.Branch == branch);
                var headerVehicleCount = vehicleByBranchesAndCountries.Where(item => item.Key.IsIn(branchKeys)).Sum(item => item.Value.Count());
                var nationIndex = EInteger.Number.Two;

                foreach (var branchKey in branchKeys)
                {
                    var vehicles = vehicleByBranchesAndCountries[branchKey];
                    var cellWithBorder = CreateGridCell();

                    if (vehicles.Any())
                    {
                        CreateVehicleCount(vehicles.Count(), branchKey, OnVehiclesByBranchesAndNationsLeftMouseDown, true)
                            .PutInto(cellWithBorder);
                    }
                    _vehiclesByBranchesAndNationsGrid.Add(cellWithBorder, nationIndex++, branchIndex);
                }
                PopulateBranchNameCountRowHeader(branch, headerVehicleCount, _vehiclesByBranchesAndNationsGrid, branchIndex++);
            }
        }

        private void PopulateVehiclesByCountriesAndBranchesControls(IEnumerable<ECountry> countries, IEnumerable<EBranch> branches)
        {
            var vehiclesByCountriesAndBranches = _statisticsControl.VehiclesByCountriesAndBranches;
            var countryIndex = EInteger.Number.One;

            PopulateBranchIconColumnHeaders(branches, _vehiclesByCountriesAndBranchesGrid, false);

            foreach (var country in countries)
            {
                var countryKeys = vehiclesByCountriesAndBranches.Keys.Where(key => key.Country == country);
                var countryVehicleCount = vehiclesByCountriesAndBranches.Where(item => item.Key.IsIn(countryKeys)).Sum(item => item.Value.Count());

                if (countryVehicleCount.IsZero()) continue;

                var branchIndex = EInteger.Number.Two;
                var branchesByCountires = countryKeys.GroupBy(item => item.Country, item => item.Branch).ToDictionary(group => group.Key, group => group.AsEnumerable());

                foreach (var branch in branches)
                {
                    var cellWithBorder = CreateGridCell();
                    var countryKey = countryKeys.First(item => item.Branch == branch);
                    var vehicles = vehiclesByCountriesAndBranches[countryKey];

                    if (vehicles.Any())
                    {
                        CreateVehicleCount(vehicles.Count(), countryKey, OnVehiclesByCountriesAndBranchesLeftMouseDown, false)
                            .PutInto(cellWithBorder)
                        ;
                    }
                    _vehiclesByCountriesAndBranchesGrid.Add(cellWithBorder, branchIndex++, countryIndex);
                }
                PopulateCountryFlagNameCountRowHeader(country, countryVehicleCount, _vehiclesByCountriesAndBranchesGrid, countryIndex++);
            }
        }

        private void PopulateVehiclesByBranchesAndClassesControls(IEnumerable<EBranch> branches)
        {
            var vehicleByBranchesAndClasses = _statisticsControl.VehiclesByBranchesAndClasses;
            var branchIndex = EInteger.Number.One;

            foreach (var branch in branches)
            {
                var branchKeys = vehicleByBranchesAndClasses.Keys.Where(key => key.Branch == branch);
                var headerVehicleCount = vehicleByBranchesAndClasses.Where(item => item.Key.IsIn(branchKeys)).Sum(item => item.Value.Count());
                var classIndex = EInteger.Number.Two;

                foreach (var branchKey in branchKeys)
                {
                    var vehicles = vehicleByBranchesAndClasses[branchKey];

                    if (vehicles.Any())
                    {
                        var classControl = CreateVehicleCountWithGaijinCharacterIcon(branchKey, branchKey.Class, vehicles.Count(), _categoryMargin, OnVehiclesByBranchesAndClassesLeftMouseDown);

                        _vehiclesByBranchesAndClassesGrid.Add(classControl, classIndex++, branchIndex);

                        continue;
                    }
                    classIndex++;
                }
                PopulateBranchNameCountRowHeader(branch, headerVehicleCount, _vehiclesByBranchesAndClassesGrid, branchIndex++);
            }
        }

        private void PopulateVehiclesByClassesAndNationsControls(IEnumerable<EVehicleClass> classes, IEnumerable<ENation> nations)
        {
            var vehicleByClassesAndNations = _statisticsControl.VehiclesByClassesAndNations;
            var classIndex = EInteger.Number.One;

            PopulateNationFlagColumnHeaders(nations, _vehiclesByClassesAndNationsGrid);

            foreach (var vehicleClass in classes)
            {
                var classKeys = vehicleByClassesAndNations.Keys.Where(key => key.Class == vehicleClass);
                var headerVehicleCount = vehicleByClassesAndNations.Where(item => item.Key.IsIn(classKeys)).Sum(item => item.Value.Count());
                var nationIndex = EInteger.Number.Two;

                foreach (var classKey in classKeys)
                {
                    var vehicles = vehicleByClassesAndNations[classKey];
                    var cellWithBorder = CreateGridCell();

                    if (vehicles.Any())
                    {
                        CreateVehicleCount(vehicles.Count(), classKey, OnVehiclesByClassesAndNationsLeftMouseDown, true)
                            .PutInto(cellWithBorder);
                    }
                    _vehiclesByClassesAndNationsGrid.Add(cellWithBorder, nationIndex++, classIndex);
                }
                PopulateVehicleClassIconAndNameRowHeader(vehicleClass, headerVehicleCount, _vehiclesByClassesAndNationsGrid, classIndex++);
            }
        }

        private void PopulateVehiclesByTagsAndNationsControls(IEnumerable<EVehicleBranchTag> tags, IEnumerable<ENation> nations)
        {
            var vehicleByTagsAndNations = _statisticsControl.VehiclesByTagsAndNations;
            var tagIndex = EInteger.Number.One;

            PopulateNationFlagColumnHeaders(nations, _vehiclesByTagsAndNationsGrid);

            foreach (var tag in tags)
            {
                var tagKeys = vehicleByTagsAndNations.Keys.Where(key => key.Tag == tag);
                var headerVehicleCount = vehicleByTagsAndNations.Where(item => item.Key.IsIn(tagKeys)).Sum(item => item.Value.Count());
                var nationIndex = EInteger.Number.Two;

                foreach (var tagKey in tagKeys)
                {
                    var vehicles = vehicleByTagsAndNations[tagKey];
                    var cellWithBorder = CreateGridCell();

                    if (vehicles.Any())
                    {
                        CreateVehicleCount(vehicles.Count(), tagKey, OnVehiclesByTagsAndNationsLeftMouseDown, true)
                            .PutInto(cellWithBorder);
                    }
                    _vehiclesByTagsAndNationsGrid.Add(cellWithBorder, nationIndex++, tagIndex);
                }
                PopulateTagNameCountRowHeader(tag, headerVehicleCount, _vehiclesByTagsAndNationsGrid, tagIndex++);
            }
        }

        #endregion Population

        #endregion Methods: Control Population
        #region Methods: Event Handlers

        private void OnScrollViewerLoaded(object sender, RoutedEventArgs e)
        {
            if (_scroll.VerticalOffset != _savedVerticalScrollOffset || _scroll.HorizontalOffset != _savedHorizontalScrollOffset)
                RestoreScrollOffset();
        }

        private void OnScrollViewerUnloaded(object sender, RoutedEventArgs e)
        {
            SaveScrollOffset();
        }

        private bool CategoryMouseDownIsHandled(object sender, MouseButtonEventArgs eventArguments, out FrameworkElement senderElement)
        {
            if (eventArguments.LeftButton == MouseButtonState.Pressed)
            {
                if (sender is FrameworkElement element)
                {
                    senderElement = element;

                    return false;
                }
            }
            eventArguments.Handled = true;
            senderElement = null;

            return true;
        }

        private void SwitchVehicleListTo(string key, IEnumerable<IVehicle> collection, EVehicleProfile shrinkProfile)
        {
            _statisticsControl.SwitchVehicleListTo(key, collection, shrinkProfile, WpfSettings.LocalizationLanguage);
        }

        private void OnVehiclesByAvailabilityLeftMouseDown(object sender, MouseButtonEventArgs eventArguments)
        {
            if (CategoryMouseDownIsHandled(sender, eventArguments, out var element))
                return;

            var listCachePrefix = nameof(_statisticsControl.VehiclesByAvailability);

            if (element.Tag is EVehicleAvailability availabilityCategory)
            {
                SwitchVehicleListTo
                (
                    $"{listCachePrefix}_{availabilityCategory}",
                    _statisticsControl.VehiclesByAvailability.Where(item => item.Key == availabilityCategory).SelectMany(item => item.Value),
                    EVehicleProfile.Full
                );
                eventArguments.Handled = true;
            }
        }

        private void OnVehiclesByNationsLeftMouseDown(object sender, MouseButtonEventArgs eventArguments)
        {
            if (CategoryMouseDownIsHandled(sender, eventArguments, out var element))
                return;

            var listCachePrefix = nameof(_statisticsControl.VehiclesByNationsAndCountries);

            if (element.Tag is ENation nation)
            {
                SwitchVehicleListTo
                (
                    $"{listCachePrefix}_{nation}",
                    _statisticsControl.VehiclesByNationsAndCountries.Where(item => item.Key.Nation == nation).SelectMany(item => item.Value),
                    EVehicleProfile.Nation
                );
                eventArguments.Handled = true;
            }
        }

        private void OnVehiclesByNationsAndCountriesLeftMouseDown(object sender, MouseButtonEventArgs eventArguments)
        {
            if (CategoryMouseDownIsHandled(sender, eventArguments, out var element))
                return;
            
            var listCachePrefix = nameof(_statisticsControl.VehiclesByNationsAndCountries);

            if (element.Tag is NationCountryPair nationCountryPair)
            {
                SwitchVehicleListTo
                (
                    $"{listCachePrefix}_{nationCountryPair}",
                    _statisticsControl.VehiclesByNationsAndCountries[nationCountryPair],
                    EVehicleProfile.NationAndCountry
                );
                eventArguments.Handled = true;
            }
        }

        private void OnVehiclesByCountriesLeftMouseDown(object sender, MouseButtonEventArgs eventArguments)
        {
            if (CategoryMouseDownIsHandled(sender, eventArguments, out var element))
                return;

            var listCachePrefix = nameof(_statisticsControl.VehiclesByCountriesAndNations);

            if (element.Tag is ECountry country)
            {
                SwitchVehicleListTo
                (
                    $"{listCachePrefix}_{country}",
                    _statisticsControl.VehiclesByCountriesAndNations.Where(item => item.Key.Country == country).SelectMany(item => item.Value),
                    EVehicleProfile.Country
                );
                eventArguments.Handled = true;
            }
        }

        private void OnVehiclesByCountriesAndNationsLeftMouseDown(object sender, MouseButtonEventArgs eventArguments)
        {
            if (CategoryMouseDownIsHandled(sender, eventArguments, out var element))
                return;

            var listCachePrefix = nameof(_statisticsControl.VehiclesByCountriesAndNations);

            if (element.Tag is NationCountryPair nationCountryPair)
            {
                SwitchVehicleListTo
                (
                    $"{listCachePrefix}_{nationCountryPair}",
                    _statisticsControl.VehiclesByCountriesAndNations[nationCountryPair],
                    EVehicleProfile.NationAndCountry
                );
                eventArguments.Handled = true;
            }
        }

        private void OnVehiclesByBranchesLeftMouseDown(object sender, MouseButtonEventArgs eventArguments)
        {
            if (CategoryMouseDownIsHandled(sender, eventArguments, out var element))
                return;

            var listCachePrefix = nameof(_statisticsControl.VehiclesByBranchesAndNations);

            if (element.Tag is EBranch branch)
            {
                SwitchVehicleListTo
                (
                    $"{listCachePrefix}_{branch}",
                    _statisticsControl.VehiclesByBranchesAndNations.Where(item => item.Key.Branch == branch).SelectMany(item => item.Value),
                    EVehicleProfile.Branch
                );
                eventArguments.Handled = true;
            }
        }

        private void OnVehiclesByBranchesAndNationsLeftMouseDown(object sender, MouseButtonEventArgs eventArguments)
        {
            if (CategoryMouseDownIsHandled(sender, eventArguments, out var element))
                return;

            var listCachePrefix = nameof(_statisticsControl.VehiclesByBranchesAndNations);

            if (element.Tag is NationBranchPair nationBranchPair)
            {
                SwitchVehicleListTo
                (
                    $"{listCachePrefix}_{nationBranchPair}",
                    _statisticsControl.VehiclesByBranchesAndNations[nationBranchPair],
                    EVehicleProfile.NationAndBranch
                );
                eventArguments.Handled = true;
            }
        }

        private void OnVehiclesByCountriesAndBranchesLeftMouseDown(object sender, MouseButtonEventArgs eventArguments)
        {
            if (CategoryMouseDownIsHandled(sender, eventArguments, out var element))
                return;

            var listCachePrefix = nameof(_statisticsControl.VehiclesByCountriesAndBranches);

            if (element.Tag is BranchCountryPair branchCountryPair)
            {
                SwitchVehicleListTo
                (
                    $"{listCachePrefix}_{branchCountryPair}",
                    _statisticsControl.VehiclesByCountriesAndBranches[branchCountryPair],
                    EVehicleProfile.BranchAndCountry
                );
                eventArguments.Handled = true;
            }
        }

        private void OnVehiclesByBranchesAndClassesLeftMouseDown(object sender, MouseButtonEventArgs eventArguments)
        {
            if (CategoryMouseDownIsHandled(sender, eventArguments, out var element))
                return;

            var listCachePrefix = nameof(_statisticsControl.VehiclesByBranchesAndClasses);

            if (element.Tag is BranchClassPair branchClassPair)
            {
                SwitchVehicleListTo
                (
                    $"{listCachePrefix}_{branchClassPair}",
                    _statisticsControl.VehiclesByBranchesAndClasses[branchClassPair],
                    EVehicleProfile.BranchAndClass
                );
                eventArguments.Handled = true;
            }
        }

        private void OnVehiclesByClassLeftMouseDown(object sender, MouseButtonEventArgs eventArguments)
        {
            if (CategoryMouseDownIsHandled(sender, eventArguments, out var element))
                return;

            var listCachePrefix = nameof(_statisticsControl.VehiclesByClassesAndNations);

            if (element.Tag is EVehicleClass vehicleClass)
            {
                SwitchVehicleListTo
                (
                    $"{listCachePrefix}_{vehicleClass}",
                    _statisticsControl.VehiclesByClassesAndNations.Where(item => item.Key.Class == vehicleClass).SelectMany(item => item.Value),
                    EVehicleProfile.Class
                );
                eventArguments.Handled = true;
            }
        }

        private void OnVehiclesByClassesAndNationsLeftMouseDown(object sender, MouseButtonEventArgs eventArguments)
        {
            if (CategoryMouseDownIsHandled(sender, eventArguments, out var element))
                return;

            var listCachePrefix = nameof(_statisticsControl.VehiclesByClassesAndNations);

            if (element.Tag is NationClassPair nationClassPair)
            {
                SwitchVehicleListTo
                (
                    $"{listCachePrefix}_{nationClassPair}",
                    _statisticsControl.VehiclesByClassesAndNations[nationClassPair],
                    EVehicleProfile.NationAndClass
                );
                eventArguments.Handled = true;
            }
        }

        private void OnVehiclesByTagsLeftMouseDown(object sender, MouseButtonEventArgs eventArguments)
        {
            if (CategoryMouseDownIsHandled(sender, eventArguments, out var element))
                return;

            var listCachePrefix = nameof(_statisticsControl.VehiclesByTagsAndNations);

            if (element.Tag is EVehicleBranchTag tag)
            {
                SwitchVehicleListTo
                (
                    $"{listCachePrefix}_{tag}",
                    _statisticsControl.VehiclesByTagsAndNations.Where(item => item.Key.Tag == tag).SelectMany(item => item.Value),
                    EVehicleProfile.Tag
                );
                eventArguments.Handled = true;
            }
        }

        private void OnVehiclesByTagsAndNationsLeftMouseDown(object sender, MouseButtonEventArgs eventArguments)
        {
            if (CategoryMouseDownIsHandled(sender, eventArguments, out var element))
                return;

            var listCachePrefix = nameof(_statisticsControl.VehiclesByTagsAndNations);

            if (element.Tag is NationTagPair nationTagPair)
            {
                SwitchVehicleListTo
                (
                    $"{listCachePrefix}_{nationTagPair}",
                    _statisticsControl.VehiclesByTagsAndNations[nationTagPair],
                    EVehicleProfile.NationAndTag
                );
                eventArguments.Handled = true;
            }
        }

        #endregion Methods: Event Handlers
        #region Methods: Scrolling

        internal void SaveScrollOffset()
        {
            _savedVerticalScrollOffset = _scroll.VerticalOffset;
            _savedHorizontalScrollOffset = _scroll.HorizontalOffset;
        }

        public void RestoreScrollOffset()
        {
            _scroll.ScrollToVerticalOffset(_savedVerticalScrollOffset);
            _scroll.ScrollToHorizontalOffset(_savedHorizontalScrollOffset);
        }

        #endregion Methods: Scrolling
    }
}