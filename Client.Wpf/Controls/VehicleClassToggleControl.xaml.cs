using Client.Wpf.Controls.Base;
using Client.Wpf.Extensions;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for VehicleClassToggleControl.xaml. </summary>
    public partial class VehicleClassToggleControl : ColumnToggleGroupControl<EBranch, EVehicleClass>
    {
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
        public override void CreateToggleColumns(IEnumerable<EBranch> branches)
        {
            foreach (var branch in branches)
            {
                var columnControl = new VehicleClassColumnToggleControl(branch)
                {
                    Tag = branch,
                };

                columnControl.Click += OnClick;
                columnControl.AddToPanel(_grid, true);

                ToggleClassColumns.Add(branch, columnControl);
            }
        }
    }
}