using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using System.Linq;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for BranchToggleControl.xaml. </summary>
    public partial class BranchToggleControl : ToggleButtonGroupControl<EBranch>
    {
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public BranchToggleControl()
        {
            InitializeComponent();
            CreateToggleButtons(_buttonGrid, typeof(EBranch).GetEnumValues().Cast<EBranch>().Where(branch => branch.IsValid()), EReference.BranchIcons, EStyleKey.ToggleButton.BranchToggle);
        }

        #endregion Constructors
    }
}