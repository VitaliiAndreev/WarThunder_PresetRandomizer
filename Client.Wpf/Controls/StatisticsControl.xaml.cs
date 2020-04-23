using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Objects;
using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for StatisticsControl.xaml. </summary>
    public partial class StatisticsControl : LocalizedUserControl
    {
        #region Properties

        internal IDictionary<NationCountryPair, IEnumerable<IVehicle>> VehiclesByNationsAndCountries { get; private set; }

        internal IDictionary<NationCountryPair, IEnumerable<IVehicle>> VehiclesByCountriesAndNations { get; private set; }

        #endregion Properties
        #region Constructors

        public StatisticsControl()
        {
            InitializeComponent();
        }

        #endregion Constructors
        #region Methods: Overrides

        public override void Localise()
        {
            base.Localise();

            _vehicleCountsHeader.Text = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.VehicleCounts);

            _vehicleCountsControl.Localise();
        }

        #endregion Methods: Overrides
        #region Methods: Initialisation

        public void Initialise()
        {
            if (_tabControl.SelectedItem == _vehicleCountsTab)
                _vehicleCountsControl.Initialise(this);
        }

        internal void SetVehiclesByNationsAndCountries(IDictionary<NationCountryPair, IEnumerable<IVehicle>> collection) =>
            VehiclesByNationsAndCountries = collection;

        internal void SetVehiclesByCountriesAndNations(IDictionary<NationCountryPair, IEnumerable<IVehicle>> collection) =>
            VehiclesByCountriesAndNations = collection;

        #endregion Methods: Initialisation
        #region Methods: Event Handlers

        private void OnTabChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source == _tabControl)
                Initialise();
        }

        #endregion Methods: Event Handlers
    }
}