using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.Extensions;
using System.Linq;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for VehicleClassColumnToggleControl.xaml. </summary>
    public partial class VehicleClassColumnToggleControl : ColumnToggleControlWithTooltips<EBranch, EVehicleClass>
    {
        #region Fields

        /// <summary> The branch to which vehicle classes belong to. </summary>
        private EBranch _branch;

        #endregion Fields
        #region Properties

        /// <summary> The branch to which vehicle classes belong to. </summary>
        public override EBranch Owner
        {
            get => _branch;
            set
            {
                _branch = value;
                _panel.Children.Clear();

                CreateToggleButtons(_panel, _groupedItems[value], EReference.ClassIcons, EStyleKey.ToggleButton.VehicleClassToggle);
            }
        }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new control. </summary>
        /// <param name="branch"> The branch for which to create the control. </param>
        public VehicleClassColumnToggleControl(EBranch branch)
            : this()
        {
            Owner = branch;
        }

        /// <summary> Creates a new control. </summary>
        public VehicleClassColumnToggleControl()
        {
            var vehicleClasses = typeof(EBranch)
                .GetEnumValues()
                .Cast<EBranch>()
                .Except(EBranch.None)
                .ToDictionary(branch => branch, branch => branch.GetVehicleClasses())
            ;

            _groupedItems.AddRange(vehicleClasses);

            InitializeComponent();
        }

        #endregion Constructors
    }
}