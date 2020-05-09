using Client.Shared.LiteObjectProfiles;
using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;
using Core.DataBase.WarThunder.Objects.Connectors;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for StatisticsControl.xaml. </summary>
    public partial class StatisticsControl : LocalisedUserControl
    {
        #region Fields

        private bool _initialised;

        private IMainWindowPresenter _presenter;

        #endregion Fields
        #region Properties

        internal IDictionary<NationAvailablityPair, IOrderedEnumerable<IVehicle>> VehiclesByAvailabilityAndNations { get; private set; }

        internal IDictionary<NationCountryPair, IOrderedEnumerable<IVehicle>> VehiclesByNationsAndCountries { get; private set; }

        internal IDictionary<NationCountryPair, IOrderedEnumerable<IVehicle>> VehiclesByCountriesAndNations { get; private set; }

        internal IDictionary<NationBranchPair, IOrderedEnumerable<IVehicle>> VehiclesByBranchesAndNations { get; private set; }

        internal IDictionary<BranchCountryPair, IOrderedEnumerable<IVehicle>> VehiclesByCountriesAndBranches { get; private set; }

        internal IDictionary<BranchClassPair, IOrderedEnumerable<IVehicle>> VehiclesByBranchesAndClasses { get; private set; }

        internal IDictionary<NationClassPair, IOrderedEnumerable<IVehicle>> VehiclesByClassesAndNations { get; private set; }

        internal IDictionary<NationTagPair, IOrderedEnumerable<IVehicle>> VehiclesByTagsAndNations { get; private set; }

        internal IDictionary<NationSubclassPair, IOrderedEnumerable<IVehicle>> VehiclesBySubclassesAndNations { get; private set; }

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

            _vehicleCountsHeader.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.VehicleCounts);
            _vehicleListHeader.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.VehicleList);

            _vehicleCountsControl.Localise();
            _vehicleListControl.Localise();
        }

        #endregion Methods: Overrides
        #region Methods: Initialisation

        public void Initialise(IMainWindowPresenter presenter)
        {
            if (!_initialised && presenter is IMainWindowPresenter)
            {
                _presenter = presenter;
                _initialised = true;
            }

            if (_tabControl.SelectedItem == _vehicleCountsTab)
            {
                _vehicleCountsControl.Initialise(this);
                _vehicleListControl.Initialise(_presenter);
            }
        }

        internal void SetVehiclesByAvailabilityAndNations(IDictionary<NationAvailablityPair, IOrderedEnumerable<IVehicle>> collection) =>
            VehiclesByAvailabilityAndNations = collection;

        internal void SetVehiclesByNationsAndCountries(IDictionary<NationCountryPair, IOrderedEnumerable<IVehicle>> collection) =>
            VehiclesByNationsAndCountries = collection;

        internal void SetVehiclesByCountriesAndNations(IDictionary<NationCountryPair, IOrderedEnumerable<IVehicle>> collection) =>
            VehiclesByCountriesAndNations = collection;

        internal void SetVehiclesByBranchesAndNations(IDictionary<NationBranchPair, IOrderedEnumerable<IVehicle>> collection) =>
            VehiclesByBranchesAndNations = collection;

        internal void SetVehiclesByCountriesAndBranches(IDictionary<BranchCountryPair, IOrderedEnumerable<IVehicle>> collection) =>
            VehiclesByCountriesAndBranches = collection;

        internal void SetVehiclesByBranchesAndClasses(IDictionary<BranchClassPair, IOrderedEnumerable<IVehicle>> collection) =>
            VehiclesByBranchesAndClasses = collection;

        internal void SetVehiclesByClassesAndNations(IDictionary<NationClassPair, IOrderedEnumerable<IVehicle>> collection) =>
            VehiclesByClassesAndNations = collection;

        internal void SetVehiclesByTagsAndNations(IDictionary<NationTagPair, IOrderedEnumerable<IVehicle>> collection) =>
            VehiclesByTagsAndNations = collection;

        internal void SetVehiclesBySubclassesAndNations(IDictionary<NationSubclassPair, IOrderedEnumerable<IVehicle>> collection) =>
            VehiclesBySubclassesAndNations = collection;

        #endregion Methods: Initialisation
        #region Methods: Event Handlers

        private void OnTabChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source == _tabControl)
            {
                Initialise(_presenter);

                e.Handled = true;
            }
        }

        #endregion Methods: Event Handlers

        internal void SwitchVehicleListTo(string key, IEnumerable<IVehicle> vehicles, EVehicleProfile vehicleProfile, ELanguage language)
        {
            _presenter.ToggleLongOperationIndicator(true);

            _vehicleListControl.VehicleProfile = vehicleProfile;
            _vehicleListControl.SetDataSource(key, vehicles, language);
            _vehicleListControl.AdjustControlVisibility();
            _vehicleListControl.ResetScrollPosition();

            _tabControl.SelectedItem = _vehicleListTab;

            _presenter.ToggleLongOperationIndicator(false);
        }
    }
}