using Client.Wpf.Controls.Base;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for VehicleClassToggleControl.xaml. </summary>
    public partial class VehicleClassToggleControl : ColumnToggleGroupControl<EBranch, EVehicleClass>
    {
        #region Properties

        /// <summary> The main panel of the control. </summary>
        internal override Panel MainPanel => _grid;

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public VehicleClassToggleControl()
        {
            AttachKeyConverter(vehicleClass => vehicleClass.GetBranch());

            var branches = typeof(EBranch).GetEnumValues().Cast<EBranch>().Where(branch => branch.IsValid());

            InitializeComponent();
            CreateToggleColumns(branches);
        }

        #endregion Constructors

        /// <summary> Creates columns of toggle buttons for given vehicle <paramref name="branches"/>, with character icons. </summary>
        /// <param name="branches"> Vehicle branches to create columns of toggle buttons for. </param>
        public void CreateToggleColumns(IEnumerable<EBranch> branches) =>
            CreateToggleColumnsBase(typeof(VehicleClassColumnToggleControl), branches);
    }
}