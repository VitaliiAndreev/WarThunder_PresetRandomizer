using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls.Primitives;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for NationToggleControl.xaml. </summary>
    public partial class NationToggleControl : ToggleButtonGroupControl<ENation>
    {
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public NationToggleControl()
        {
            InitializeComponent();

            _usaButton.Tag = ENation.Usa;
            _germanyButton.Tag = ENation.Germany;
            _ussrButton.Tag = ENation.Ussr;
            _commonwealthButton.Tag = ENation.Commonwealth;
            _japanButton.Tag = ENation.Japan;
            _chinaButton.Tag = ENation.China;
            _italyButton.Tag = ENation.Italy;
            _franceButton.Tag = ENation.France;

            _buttons.AddRange(new List<ToggleButton> { _usaButton, _germanyButton, _ussrButton, _commonwealthButton, _japanButton, _chinaButton, _italyButton, _franceButton }.ToDictionary(button => button.Tag.CastTo<ENation>()));

            _usaButton.Click += OnClick;
            _germanyButton.Click += OnClick;
            _ussrButton.Click += OnClick;
            _commonwealthButton.Click += OnClick;
            _japanButton.Click += OnClick;
            _chinaButton.Click += OnClick;
            _italyButton.Click += OnClick;
            _franceButton.Click += OnClick;
        }

        #endregion Constructors

        /// <summary> Applies localization to visible text on the control. </summary>
        public override void Localize()
        {
            base.Localize();

            #region Button Tooltips

            _usaButton.ToolTip = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Usa);
            _germanyButton.ToolTip = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Germany);
            _ussrButton.ToolTip = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Ussr);
            _commonwealthButton.ToolTip = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Britain);
            _japanButton.ToolTip = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Japan);
            _chinaButton.ToolTip = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.China);
            _italyButton.ToolTip = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Italy);
            _franceButton.ToolTip = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.France);

            #endregion Button Tooltips
        }
    }
}