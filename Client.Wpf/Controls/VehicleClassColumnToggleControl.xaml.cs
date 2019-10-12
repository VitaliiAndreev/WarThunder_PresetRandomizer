using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for VehicleClassColumnToggleControl.xaml. </summary>
    public partial class VehicleClassColumnToggleControl : ToggleButtonGroupControlWithToolTip<EVehicleClass>
    {
        #region Fields

        /// <summary> Vehicle classes grouped by their owner branches. </summary>
        private readonly IDictionary<EBranch, IEnumerable<EVehicleClass>> _vehicleClasses;

        /// <summary> The branch to which vehicle classes belong to. </summary>
        private EBranch _branch;

        #endregion Fields
        #region Properties

        /// <summary> The branch to which vehicle classes belong to. </summary>
        public EBranch Branch
        {
            get => _branch;
            set
            {
                _branch = value;
                _panel.Children.Clear();

                CreateToggleButtons(_panel, _vehicleClasses[value], EReference.ClassIcons, EStyleKey.ToggleButton.VehicleClassToggle, false);
            }
        }

        /// <summary> Enabled vehicle classes. </summary>
        public IEnumerable<EVehicleClass> EnabledClasses { get; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new control. </summary>
        /// <param name="branch"> The branch for which to create the control. </param>
        public VehicleClassColumnToggleControl(EBranch branch)
            : this()
        {
            Branch = branch;
        }

        /// <summary> Creates a new control. </summary>
        public VehicleClassColumnToggleControl()
        {
            _vehicleClasses = typeof(EBranch)
                .GetEnumValues()
                .Cast<EBranch>()
                .Except(EBranch.None)
                .ToDictionary(branch => branch, branch => branch.GetVehicleClasses())
            ;

            InitializeComponent();

            EnabledClasses = new List<EVehicleClass>();
        }

        #endregion Constructors
    }
}