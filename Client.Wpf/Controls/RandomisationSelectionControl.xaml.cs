using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Core.Extensions;
using Core.Organization.Enumerations;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for GameModeSelectionControl.xaml. </summary>
    public partial class RandomisationSelectionControl : ExclusiveToggleButtonGroupControlWithToolTip<ERandomisation>
    {
        #region Constuctors

        /// <summary> Creates a new control. </summary>
        public RandomisationSelectionControl()
        {
            InitializeComponent();
            CreateToggleButtons(_buttonGrid, typeof(ERandomisation).GetEnumValues().OfType<ERandomisation>(), null, EStyleKey.ToggleButton.CountryToggleAll);
        }

        #endregion Constuctors

        /// <summary> Applies localization to visible text on the control. </summary>
        public override void Localise()
        {
            base.Localise();
            Buttons[ERandomisation.CategoryBased].ToolTip = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.CategoryBasedDescription);
            Buttons[ERandomisation.VehicleBased].ToolTip = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.VehicleBasedDescription);
        }
    }
}