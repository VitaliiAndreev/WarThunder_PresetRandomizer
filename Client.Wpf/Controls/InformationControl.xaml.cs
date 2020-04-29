using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;
using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for InformationControl.xaml. </summary>
    public partial class InformationControl : LocalisedUserControl
    {
        #region Fields

        private bool _initialised;

        private IMainWindowPresenter _presenter;

        #endregion Fields
        #region Properties

        public ResearchTreeControl ResearchTreeControl => _researchTreeControl;

        #endregion Properties
        #region Constructors

        public InformationControl()
        {
            InitializeComponent();
        }

        #endregion Constructors
        #region Methods: Overrides

        public override void Localise()
        {
            base.Localise();

            _researchTreeHeader.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.ResearchTrees);
            _statisticsHeader.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.Statistics);
            _vehicleCardHeader.Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.VehicleInformation);

            _researchTreeControl.Localise();
            _statisticsControl.Localise();
        }

        #endregion Methods: Overrides
        #region Methods: Initialisation

        public void Initialise(IMainWindowPresenter presenter)
        {
            if (!_initialised && presenter is IMainWindowPresenter)
            {
                _presenter = presenter;
                _researchTreeControl.Initialise(_presenter);

                _vehicleInformationTab.ToolTip = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.UnderConstruction);

                ToolTipService.SetShowOnDisabled(_vehicleInformationTab, true);

                _initialised = true;
            }
        }

        #endregion Methods: Initialisation
        #region Methods: Navigation

        public void BringIntoView(IVehicle vehicle, bool changeTabs = false)
        {
            if (changeTabs)
                _tabControl.SelectedItem = _researchTreeTab;

            _researchTreeControl.BringIntoView(vehicle, changeTabs);
        }

        #endregion Methods: Navigation
        #region Methods: Event Handlers

        private void OnTabChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source == _tabControl)
            {
                if (_tabControl.SelectedItem == _statisticsTab)
                    _statisticsControl.Initialise(_presenter);
            }
        }

        #endregion Methods: Event Handlers
    }
}