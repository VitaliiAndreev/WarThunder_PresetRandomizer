using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using Core.Extensions;
using NHibernate.Criterion;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for VehicleCountsStatisticsControl.xaml. </summary>
    public partial class VehicleCountsStatisticsControl : LocalizedUserControl
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

            _vehiclesByNationsAndCountriesHeader.Text = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.VehiclesByNationsAndCountries);
        }

        #endregion Methods: Overrides
        #region Methods: Initialisation

        public void Initialise(StatisticsControl statisticsControl)
        {
            if (!_initialised)
            {
                _statisticsControl = statisticsControl;

                InitialiseVehiclesByNationsAndCountries();

                _initialised = true;
            }
        }

        public void InitialiseVehiclesByNationsAndCountries()
        {
            var nations = typeof(ENation).GetEnumerationItems<ENation>(true);
            var vehiclesCounts = nations
                .SelectMany(nation => nation.GetNationCountryPairs())
                .ToDictionary(nationCountryPair => nationCountryPair, nationCountryPair => new List<IVehicle>())
            ;

            foreach (var vehicle in ApplicationHelpers.Manager.PlayableVehicles)
                vehicle.AddInto(vehiclesCounts[new NationCountryPair(vehicle.Nation.AsEnumerationItem, vehicle.Country)]);

            var vehiclesByNationsAndCountries = vehiclesCounts
                .OrderBy(item => item.Key.Nation)
                .ThenByDescending(item => item.Value.Count())
                .ThenBy(item => item.Key.Country)
                .ToDictionary(item => item.Key, item => item.Value.AsEnumerable())
            ;
            var vehiclesByCountriesAndNations = vehiclesByNationsAndCountries
                .OrderBy(item => item.Key.Country)
                .ThenByDescending(item => item.Value.Count())
                .ThenBy(item => item.Key.Nation)
                .ToDictionary(item => item.Key, item => item.Value.AsEnumerable())
            ;

            _statisticsControl.SetVehiclesByNationsAndCountries(vehiclesByNationsAndCountries);
            _statisticsControl.SetVehiclesByCountriesAndNations(vehiclesByCountriesAndNations);

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
                    Text = ApplicationHelpers.LocalizationManager.GetLocalizedString(nation.ToString()),
                };
                var count = new TextBlock
                {
                    Style = textStyle,
                    Margin = new Thickness(0, 0, margin, 0),
                    Text = vehiclesByNationsAndCountries.Where(item => item.Key.IsIn(nationKeys)).Sum(item => item.Value.Count()).ToString(),
                };
                var countryIndex = EInteger.Number.Two;

                foreach (var nationKey in nationKeys)
                {
                    var countryControl = new VehicleCounterWithFlag(nationKey.Country, vehiclesByNationsAndCountries[nationKey].Count(), new Thickness(margin, 0, margin, 0));

                    _vehiclesByNationsAndCountriesGrid.Add(countryControl, countryIndex++, nationIndex);
                }
                _vehiclesByNationsAndCountriesGrid.Add(header, EInteger.Number.Zero, nationIndex);
                _vehiclesByNationsAndCountriesGrid.Add(count, EInteger.Number.One, nationIndex++);
            }
        }

        #endregion Methods: Initialisation
    }
}