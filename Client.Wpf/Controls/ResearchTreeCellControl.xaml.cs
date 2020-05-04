using Client.Wpf.Controls.Strategies;
using Client.Wpf.Enumerations;
using Client.Wpf.Presenters.Interfaces;
using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for ResearchTreeCellControl.xaml. </summary>
    public partial class ResearchTreeCellControl : UserControl
    {
        #region Fields

        private bool _initialised;

        private IMainWindowPresenter _presenter;

        #endregion Fields
        #region Properties

        /// <summary> Vehicle controls positioned in the cell. </summary>
        internal IDictionary<string, ResearchTreeCellVehicleControl> VehicleControls { get; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public ResearchTreeCellControl()
        {
            InitializeComponent();

            VehicleControls = new Dictionary<string, ResearchTreeCellVehicleControl>();
        }

        #endregion Constructors
        #region Methods: Initialisation

        public ResearchTreeCellControl With(IMainWindowPresenter presenter)
        {
            if (!_initialised && presenter is IMainWindowPresenter)
            {
                _presenter = presenter;
                _initialised = true;
            }
            return this;
        }

        #endregion Methods: Initialisation

        /// <summary> Adds a new control for the specified vehicle and adds it to the the cell. </summary>
        /// <param name="vehicle"> The vehicle to add. </param>
        /// <param name="isToggled"> The vehicle is toggled on/off by default. </param>
        internal void AddVehicle(IVehicle vehicle, bool isToggled)
        {
            var vehicleControl = new ResearchTreeCellVehicleControl(_presenter, vehicle, new DisplayVehicleInformationInResearchTreeStrategy(), EVehicleCard.ResearchTree, isToggled);

            _stackPanel.Children.Add(vehicleControl);
            VehicleControls.Add(vehicle.GaijinId, vehicleControl);
        }
    }
}