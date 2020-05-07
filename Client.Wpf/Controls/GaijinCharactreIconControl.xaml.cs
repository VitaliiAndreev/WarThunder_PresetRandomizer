using Client.Wpf.Controls.Base;
using Core.Enumerations;
using System.Windows;
using System.Windows.Input;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for GaijinCharactreIconControl.xaml. </summary>
    public partial class GaijinCharactreIconControl : LocalisedUserControl
    {
        #region Constructors

        public GaijinCharactreIconControl()
        {
            InitializeComponent();
        }

        public GaijinCharactreIconControl(char icon, object iconTag, Thickness margin, MouseButtonEventHandler leftMouseDownHandler, double? fontSize = null, bool iconIsBold = false)
            : this()
        {
            Initialise(icon.ToString(), iconTag, margin, leftMouseDownHandler, fontSize, iconIsBold);
        }

        public GaijinCharactreIconControl(string icon, object iconTag, Thickness margin, MouseButtonEventHandler leftMouseDownHandler, double? fontSize = null, bool iconIsBold = false)
            : this()
        {
            Initialise(icon, iconTag, margin, leftMouseDownHandler, fontSize, iconIsBold);
        }

        #endregion Constructors
        #region Methods: Initialisation

        private void Initialise<T>(string icon, T iconTag, Thickness margin, MouseButtonEventHandler leftMouseDownHandler, double? fontSize = EDouble.Number.Sixteen, bool iconIsBold = false)
        {
            _icon.FontWeight = iconIsBold ? FontWeights.Bold : FontWeights.Normal;
            _icon.Text = icon;
            _icon.ToolTip = ApplicationHelpers.LocalisationManager.GetLocalisedString(iconTag);

            if (fontSize.HasValue)
                _icon.FontSize = fontSize.Value;

            Margin = margin;

            if (!(leftMouseDownHandler is null))
                MouseDown += leftMouseDownHandler;
        }

        public GaijinCharactreIconControl WithTag<T>(T controlTag)
        {
            Tag = controlTag;

            return this;
        }

        #endregion Methods: Initialisation
    }
}