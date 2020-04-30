using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Client.Wpf.Enumerations.ShrinkProfiles;
using Client.Wpf.Extensions;
using Client.Wpf.Presenters.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using Core.Extensions;
using NHibernate.Criterion;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for VehicleCountsStatisticsControl.xaml. </summary>
    public partial class VehicleCountsStatisticsControl : LocalisedUserControl
    {
        #region Fields

        private bool _initialised;

        private StatisticsControl _statisticsControl;

        private readonly int _categorySideMargin;
        private readonly Thickness _categoryHeaderMargin;
        private readonly Thickness _categoryMargin;
        private readonly Style _categoryTextStyle;

        #endregion Fields
        #region Constructors

        public VehicleCountsStatisticsControl()
        {
            InitializeComponent();

            _categorySideMargin = EInteger.Number.Five;
            _categoryHeaderMargin = new Thickness(EInteger.Number.Zero, EInteger.Number.Zero, _categorySideMargin, EInteger.Number.Zero);
            _categoryMargin = new Thickness(_categorySideMargin, EInteger.Number.Zero, _categorySideMargin, EInteger.Number.Zero);
            _categoryTextStyle = this.GetStyle(EStyleKey.TextBlock.TextBlock12px);
        }

        #endregion Constructors
        #region Methods: Overrides

        public override void Localise()
        {
            base.Localise();

            _hint.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.ClickOnCategoryToOpenList);
            _vehiclesByNationsAndCountriesHeader.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.VehiclesByNationsAndCountries);
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

            var nations = typeof(ENation).GetEnumerationItems<ENation>(true);
            var vehiclesCounts = nations
                .SelectMany(nation => nation.GetNationCountryPairs())
                .ToDictionary(nationCountryPair => nationCountryPair, nationCountryPair => new List<IVehicle>())
            ;

            foreach (var vehicle in ApplicationHelpers.Manager?.PlayableVehicles?.Values ?? new List<IVehicle>())
                vehicle.AddInto(vehiclesCounts[new NationCountryPair(vehicle.Nation.AsEnumerationItem, vehicle.Country)]);

            InitialiseVehiclesByNationsAndCountries(vehiclesCounts);
            InitialiseVehiclesByCountriesAndNations(vehiclesCounts);

            PopulateVehiclesByNationsAndCountriesControls(nations);
            PopulateVehiclesByCountriesAndNationsControls();
        }

        private IOrderedEnumerable<IVehicle> Reorder(IEnumerable<IVehicle> vehicles)
        {
            return vehicles
                .OrderBy(vehicle => vehicle.Class)
                .ThenBy(vehicle => vehicle.Subclasses.First)
                .ThenBy(vehicle => vehicle.Subclasses.Second)
                .ThenBy(vehicle => vehicle.Rank)
                .ThenBy(vehicle => vehicle.GaijinId)
            ;
        }

        private void InitialiseVehiclesByNationsAndCountries(IDictionary<NationCountryPair, List<IVehicle>> vehiclesCounts)
        {
            var vehiclesByNationsAndCountries = vehiclesCounts
                .OrderBy(item => item.Key.Nation)
                .ThenByDescending(item => item.Value.Count())
                .ThenBy(item => item.Key.Country)
                .Select(item => new KeyValuePair<NationCountryPair, IOrderedEnumerable<IVehicle>>(item.Key, Reorder(item.Value)))
                .ToDictionary(item => item.Key, item => item.Value)
            ;
            _statisticsControl?.SetVehiclesByNationsAndCountries(vehiclesByNationsAndCountries);
        }

        private void InitialiseVehiclesByCountriesAndNations(IDictionary<NationCountryPair, List<IVehicle>> vehiclesCounts)
        {
            var vehiclesByCountriesAndNations = vehiclesCounts
                .OrderBy(item => item.Key.Country)
                .ThenByDescending(item => item.Value.Count())
                .ThenBy(item => item.Key.Nation)
                .Select(item => new KeyValuePair<NationCountryPair, IOrderedEnumerable<IVehicle>>(item.Key, Reorder(item.Value)))
                .ToDictionary(item => item.Key, item => item.Value)
            ;
            _statisticsControl?.SetVehiclesByCountriesAndNations(vehiclesByCountriesAndNations);
        }

        private TextBlock CreateTextBlock<T>(string text, T tag, MouseButtonEventHandler mouseDownHandler, bool isBold = false) where T : struct
        {
            var textBlock = new TextBlock
            {
                Style = _categoryTextStyle,
                Margin = _categoryHeaderMargin,
                FontWeight = isBold ? FontWeights.Bold : FontWeights.Normal,
                Text = text,
                Tag = tag,
            };

            textBlock.MouseDown += mouseDownHandler;

            return textBlock;
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
                    OnVehiclesByNationsLeftMouseDown,
                    true
                );
                var count = CreateTextBlock
                (
                    vehiclesByNationsAndCountries.Where(item => item.Key.IsIn(nationKeys)).Sum(item => item.Value.Count()).ToString(),
                    nation,
                    OnVehiclesByNationsLeftMouseDown
                );
                var countryIndex = EInteger.Number.Two;

                foreach (var nationKey in nationKeys)
                {
                    var countryControl = new VehicleCountWithFlag(nationKey, vehiclesByNationsAndCountries[nationKey].Count(), _categoryMargin, OnVehiclesByNationsLeftMouseDown);

                    _vehiclesByNationsAndCountriesGrid.Add(countryControl, countryIndex++, nationIndex);
                }
                _vehiclesByNationsAndCountriesGrid.Add(header, EInteger.Number.Zero, nationIndex);
                _vehiclesByNationsAndCountriesGrid.Add(count, EInteger.Number.One, nationIndex++);
            }
        }

        private void PopulateVehiclesByCountriesAndNationsControls()
        {
            var vehiclesByCountriesAndNations = _statisticsControl.VehiclesByCountriesAndNations;
            var countries = vehiclesByCountriesAndNations.Keys.Select(nationCountryPair => nationCountryPair.Country).Distinct();
            var countryIndex = EInteger.Number.Zero;

            foreach (var country in countries)
            {
                var countryKeys = vehiclesByCountriesAndNations.Keys.Where(key => key.Country == country);
                var header = new TextLabelWithFlag(country, ApplicationHelpers.LocalisationManager.GetLocalisedString(country.ToString()), _categoryHeaderMargin, OnVehiclesByCountriesLeftMouseDown, true);
                var count = CreateTextBlock
                (
                    vehiclesByCountriesAndNations.Where(item => item.Key.IsIn(countryKeys)).Sum(item => item.Value.Count()).ToString(),
                    country,
                    OnVehiclesByCountriesLeftMouseDown
                );
                var nationIndex = EInteger.Number.Two;

                foreach (var countryKey in countryKeys)
                {
                    var nationControl = new VehicleCountWithFlag(countryKey, vehiclesByCountriesAndNations[countryKey].Count(), _categoryMargin, OnVehiclesByCountriesLeftMouseDown, true);

                    _vehiclesByCountriesAndNationsGrid.Add(nationControl, nationIndex++, countryIndex);
                }
                _vehiclesByCountriesAndNationsGrid.Add(header, EInteger.Number.Zero, countryIndex);
                _vehiclesByCountriesAndNationsGrid.Add(count, EInteger.Number.One, countryIndex++);
            }
        }

        #endregion Methods: Initialisation
        #region Methods: Event Handlers

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

        private void OnVehiclesByNationsLeftMouseDown(object sender, MouseButtonEventArgs eventArguments)
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
            else if (element.Tag is ENation nation)
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

        private void OnVehiclesByCountriesLeftMouseDown(object sender, MouseButtonEventArgs eventArguments)
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
            else if (element.Tag is ECountry country)
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

        #endregion Methods: Event Handlers
    }
}