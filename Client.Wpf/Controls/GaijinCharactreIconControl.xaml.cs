using Client.Wpf.Controls.Base;
using Core.DataBase.WarThunder.Enumerations;
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

        public GaijinCharactreIconControl(char icon, object tag, Thickness margin, MouseButtonEventHandler leftMouseDownHandler, double? fontSize = null)
            : this()
        {
            Initialise(icon.ToString(), tag, margin, leftMouseDownHandler, fontSize);
        }

        public GaijinCharactreIconControl(string icon, object tag, Thickness margin, MouseButtonEventHandler leftMouseDownHandler, double? fontSize = null)
            : this()
        {
            Initialise(icon, tag, margin, leftMouseDownHandler, fontSize);
        }

        #endregion Constructors
        #region Methods: Initialisation

        private void Initialise<T>(string icon, T tag, Thickness margin, MouseButtonEventHandler leftMouseDownHandler, double? fontSize = EDouble.Number.Sixteen)
        {
            _icon.Text = icon;
            _icon.ToolTip = ApplicationHelpers.LocalisationManager.GetLocalisedString(tag.ToString());

            if (fontSize.HasValue)
                _icon.FontSize = fontSize.Value;

            Tag = tag;
            Margin = margin;

            if (!(leftMouseDownHandler is null))
                MouseDown += leftMouseDownHandler;
        }

        #endregion Methods: Initialisation
    }
}