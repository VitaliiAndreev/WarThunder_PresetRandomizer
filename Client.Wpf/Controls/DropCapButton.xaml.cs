using Core.Enumerations;
using Core.Extensions;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for DropCapButton.xaml. </summary>
    public partial class DropCapButton : UserControl
    {
        #region Properties

        /// <summary> The button embedded in the control. </summary>
        public Button EmbeddedButton => _button;

        /// <summary> The font size of the drop cap. The following text is written with a smaller font size.</summary>
        public double DropCapFontSize
        {
            get => _dropCap.FontSize;
            set
            {
                _dropCap.FontSize = value;
                _otherText.FontSize = value > EInteger.Number.Five
                    ? value - EInteger.Number.Five
                    : EInteger.Number.One;
            }
        }

        /// <summary> The text contents of the button. </summary>
        public string Caption
        {
            get => $"{_dropCap.Text}{_otherText.Text}";
            set
            {
                _dropCap.Text = value.Take(1).StringJoin();
                _otherText.Text = value.Skip(1).StringJoin();
            }
        }

        #endregion Properties
        #region Events

        /// <summary> Occurs when the button is clicked. </summary>
        public event RoutedEventHandler Click
        {
            add => _button.Click += value;
            remove => _button.Click -= value;
        }

        #endregion Events
        #region Constuctors

        /// <summary> Creates a new control. </summary>
        public DropCapButton()
        {
            InitializeComponent();
        }

        #endregion Constuctors
    }
}