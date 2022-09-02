using Client.Wpf.Controls.Base;
using System.Windows;
using System.Windows.Input;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for GaijinCharactreIconControl.xaml. </summary>
    public partial class GaijinCharactreIconControl : LocalisedUserControl
    {
        private const double defaultFontSize = 16.0;

        #region Constructors

        public GaijinCharactreIconControl()
        {
            InitializeComponent();
        }

        public GaijinCharactreIconControl(char icon, object iconTag, Thickness margin, MouseButtonEventHandler leftMouseDownHandler, double? fontSize = null, bool iconIsBold = false, bool useStandardVerticalMargin = false)
            : this()
        {
            Initialise(icon.ToString(), iconTag, margin, leftMouseDownHandler, fontSize, iconIsBold, useStandardVerticalMargin);
        }

        public GaijinCharactreIconControl(string icon, object iconTag, Thickness margin, MouseButtonEventHandler leftMouseDownHandler, double? fontSize = null, bool iconIsBold = false, bool useStandardVerticalMargin = false)
            : this()
        {
            Initialise(icon, iconTag, margin, leftMouseDownHandler, fontSize, iconIsBold, useStandardVerticalMargin);
        }

        #endregion Constructors
        #region Methods: Initialisation

        private void Initialise<T>(
            string icon, 
            T iconTag, 
            Thickness margin, 
            MouseButtonEventHandler leftMouseDownHandler, 
            double? fontSize = defaultFontSize, 
            bool iconIsBold = false, 
            bool useStandardVerticalMargin = false)
        {
            _icon.FontWeight = iconIsBold ? FontWeights.Bold : FontWeights.Normal;
            _icon.Text = icon;
            _icon.ToolTip = ApplicationHelpers.LocalisationManager.GetLocalisedString(iconTag);

            if (useStandardVerticalMargin)
                _icon.Margin = default;

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