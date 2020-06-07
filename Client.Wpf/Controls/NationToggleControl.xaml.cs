using Client.Shared.Wpf.Extensions;
using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.Organization.Extensions;
using System.Linq;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for NationToggleControl.xaml. </summary>
    public partial class NationToggleControl : ToggleButtonGroupControlWithToolTip<ENation>
    {
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public NationToggleControl()
        {
            InitializeComponent();
            CreateToggleButtons(_buttonGrid, typeof(ENation).GetEnumValues().Cast<ENation>().Where(nation => nation.IsValid()), EReference.NationIcons, EStyleKey.ToggleButton.NationToggle);
        }

        #endregion Constructors

        /// <summary> Removes nations that have no vehicles. </summary>
        public void RemoveUnavailableNations()
        {
            foreach (var buttonKeyValuePair in Buttons)
            {
                var nation = buttonKeyValuePair.Key;
                var button = buttonKeyValuePair.Value;

                if (!ApplicationHelpers.Manager.ResearchTrees.Has(nation))
                {
                    if (button.Parent is Grid grid)
                        grid.Remove(button);
                }
            }
        }
    }
}