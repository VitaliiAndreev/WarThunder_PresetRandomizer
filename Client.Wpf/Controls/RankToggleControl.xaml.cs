using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using System.Linq;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for RankToggleControl.xaml. </summary>
    public partial class RankToggleControl : ToggleButtonGroupControl<ERank>
    {
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public RankToggleControl()
        {
            InitializeComponent();
            CreateToggleButtons(_buttonGrid, typeof(ERank).GetEnumValues().Cast<ERank>().Where(rank => rank.IsValid()), EReference.RankIcons, EStyleKey.ToggleButton.RankToggle);
        }

        #endregion Constructors
    }
}