using Client.Wpf.Controls.Strategies;
using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for ResearchTreeCellControl.xaml. </summary>
    public partial class ResearchTreeCellControl : UserControl
    {
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

        /// <summary> Adds a new control for the specified vehicle and adds it to the the cell. </summary>
        /// <param name="vehicle"> The vehicle to add. </param>
        internal void AddVehicle(IVehicle vehicle)
        {
            var vehicleControl = new ResearchTreeCellVehicleControl(vehicle, new DisplayBasicVehicleInformationStrategy());

            _stackPanel.Children.Add(vehicleControl);
            VehicleControls.Add(vehicle.GaijinId, vehicleControl);
        }
    }
}