using Client.Wpf.Controls.Base;
using Core.DataBase.WarThunder.Enumerations;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for RepairCostControl.xaml. </summary>
    public partial class RepairCostControl : LocalisedUserControl
    {
        #region Constructors

        public RepairCostControl()
        {
            InitializeComponent();

            Tag = ECategory.RepairCost;
        }

        #endregion Constructors
        #region Methods: Overrides

        public override void Localise()
        {
            base.Localise();

            _header.ToolTip = ApplicationHelpers.LocalisationManager.GetLocalisedString(Tag);
        }

        #endregion Methods: Overrides
    }
}