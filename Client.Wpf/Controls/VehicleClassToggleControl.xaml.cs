using Client.Wpf.Controls.Base;
using Client.Wpf.Extensions;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for VehicleClassToggleControl.xaml. </summary>
    public partial class VehicleClassToggleControl : ColumnToggleGroupControl<EBranch, VehicleClassColumnToggleControl, EVehicleClass>
    {
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public VehicleClassToggleControl()
        {
            var branches = typeof(EBranch).GetEnumValues().Cast<EBranch>().Except(EBranch.None);

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

        /// <summary> Toggles a button corresponding to the specified <paramref name="vehicleClass"/>. </summary>
        /// <param name="vehicleClass"> The vehicle class whose button to toggle. </param>
        /// <param name="newState"> Whether to toggle the button on or off. </param>
        public override void Toggle(EVehicleClass vehicleClass, bool newState)
        {
            if (ToggleClassColumns.TryGetValue(vehicleClass.GetBranch(), out var column))
                column.Toggle(vehicleClass, newState);
        }
    }
}