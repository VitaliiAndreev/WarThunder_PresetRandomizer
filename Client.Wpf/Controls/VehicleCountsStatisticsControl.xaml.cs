using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Client.Wpf.Enumerations.ShrinkProfiles;
using Client.Wpf.Extensions;
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

        #endregion Fields
        #region Constructors

        public VehicleCountsStatisticsControl()
        {
            InitializeComponent();
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

                InitialiseVehiclesByNationsAndCountries();

                _initialised = true;
            }
        }

        public void InitialiseVehiclesByNationsAndCountries()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            var nations = typeof(ENation).GetEnumerationItems<ENation>(true);
            var vehiclesCounts = nations
                .SelectMany(nation => nation.GetNationCountryPairs())
                .ToDictionary(nationCountryPair => nationCountryPair, nationCountryPair => new List<IVehicle>())
            ;

            foreach (var vehicle in ApplicationHelpers.Manager?.PlayableVehicles.Values ?? new List<IVehicle>())
                vehicle.AddInto(vehiclesCounts[new NationCountryPair(vehicle.Nation.AsEnumerationItem, vehicle.Country)]);

            static IOrderedEnumerable<IVehicle> reorder(IEnumerable<IVehicle> vehicles)
            {
                return vehicles
                    .OrderBy(vehicle => vehicle.Class)
                    .ThenBy(vehicle => vehicle.Subclasses.First)
                    .ThenBy(vehicle => vehicle.Subclasses.Second)
                    .ThenBy(vehicle => vehicle.Rank)
                    .ThenBy(vehicle => vehicle.GaijinId)
                ;
            }

            var vehiclesByNationsAndCountries = vehiclesCounts
                .OrderBy(item => item.Key.Nation)
                .ThenByDescending(item => item.Value.Count())
                .ThenBy(item => item.Key.Country)
                .Select(item => new KeyValuePair<NationCountryPair, IOrderedEnumerable<IVehicle>>(item.Key, reorder(item.Value)))
                .ToDictionary(item => item.Key, item => item.Value)
            ;
            var vehiclesByCountriesAndNations = vehiclesCounts
                .OrderBy(item => item.Key.Country)
                .ThenByDescending(item => item.Value.Count())
                .ThenBy(item => item.Key.Nation)
                .Select(item => new KeyValuePair<NationCountryPair, IOrderedEnumerable<IVehicle>>(item.Key, reorder(item.Value)))
                .ToDictionary(item => item.Key, item => item.Value)
            ;

            _statisticsControl?.SetVehiclesByNationsAndCountries(vehiclesByNationsAndCountries);
            _statisticsControl?.SetVehiclesByCountriesAndNations(vehiclesByCountriesAndNations);

            var nationIndex = EInteger.Number.Zero;

            foreach (var nation in nations)
            {
                var nationKeys = vehiclesByNationsAndCountries.Keys.Where(key => key.Nation == nation);
                var textStyle = this.GetStyle(EStyleKey.TextBlock.TextBlock12px);
                var margin = EInteger.Number.Five;
                var header = new TextBlock
                {
                    Style = textStyle,
                    Margin = new Thickness(0, 0, margin, 0),
                    FontWeight = FontWeights.Bold,
                    Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(nation.ToString()),
                    Tag = nation,
                };
                var count = new TextBlock
                {
                    Style = textStyle,
                    Margin = new Thickness(0, 0, margin, 0),
                    Text = vehiclesByNationsAndCountries.Where(item => item.Key.IsIn(nationKeys)).Sum(item => item.Value.Count()).ToString(),
                    Tag = nation,
                };
                var countryIndex = EInteger.Number.Two;

                header.MouseDown += OnVehiclesByNationsLeftMouseDown;
                count.MouseDown += OnVehiclesByNationsLeftMouseDown;

                foreach (var nationKey in nationKeys)
                {
                    var countryControl = new VehicleCountWithFlag(nationKey, vehiclesByNationsAndCountries[nationKey].Count(), new Thickness(margin, 0, margin, 0));

                    countryControl.MouseDown += OnVehiclesByNationsLeftMouseDown;

                    _vehiclesByNationsAndCountriesGrid.Add(countryControl, countryIndex++, nationIndex);
                }
                _vehiclesByNationsAndCountriesGrid.Add(header, EInteger.Number.Zero, nationIndex);
                _vehiclesByNationsAndCountriesGrid.Add(count, EInteger.Number.One, nationIndex++);
            }
        }

        #endregion Methods: Initialisation
        #region Methods: Event Handlers

        private void OnVehiclesByNationsLeftMouseDown(object sender, MouseButtonEventArgs eventArguments)
        {
            if (eventArguments.LeftButton == MouseButtonState.Pressed)
            {
                if (sender is FrameworkElement element)
                {
                    var keyPrefix = nameof(_statisticsControl.VehiclesByNationsAndCountries);
                    var language = WpfSettings.LocalizationLanguage;

                    void switchVehicleListTo(string key, IEnumerable<IVehicle> collection, EVehicleProfile shrinkProfile, ELanguage language)
                    {
                        _statisticsControl.SwitchVehicleListTo(key, collection, shrinkProfile, language);
                        eventArguments.Handled = true;
                    }

                    if (element.Tag is NationCountryPair nationCountryPair)
                    {
                        switchVehicleListTo
                        (
                            $"{keyPrefix}_{nationCountryPair}",
                            _statisticsControl.VehiclesByNationsAndCountries[nationCountryPair],
                            EVehicleProfile.NationAndCountry,
                            language
                        );
                    }
                    else if (element.Tag is ENation nation)
                    {
                        switchVehicleListTo
                        (
                            $"{keyPrefix}_{nation}",
                            _statisticsControl.VehiclesByNationsAndCountries.Where(item => item.Key.Nation == nation).SelectMany(item => item.Value),
                            EVehicleProfile.Nation,
                            language
                        );
                    }
                }
            }
        }

        #endregion Methods: Event Handlers
    }
}