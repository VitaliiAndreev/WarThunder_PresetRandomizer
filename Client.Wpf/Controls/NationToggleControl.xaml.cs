using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.Extensions;
using System.Linq;

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
            CreateToggleButtons(_buttonGrid, typeof(ENation).GetEnumValues().Cast<ENation>().Except(ENation.None), EReference.NationIcons, EStyleKey.ToggleButton.NationToggle);
        }

        #endregion Constructors
    }
}