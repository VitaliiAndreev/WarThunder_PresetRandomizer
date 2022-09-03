using Core;
using System.Linq;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for DropCapTextBlock.xaml. </summary>
    public partial class DropCapTextBlock : UserControl
    {
        #region Properties

        /// <summary> The font size of the drop cap. The following text is written with a smaller font size.</summary>
        public double DropCapFontSize
        {
            get => _dropCap.FontSize;
            set
            {
                _dropCap.FontSize = value;
                _otherText.FontSize = value > 5
                    ? value - 5
                    : 1;
            }
        }

        /// <summary> Text contents. </summary>
        public string Text
        {
            get => $"{_dropCap.Text}{_otherText.Text}";
            set
            {
                _dropCap.Text = value.Take(1).StringJoin();
                _otherText.Text = value.Skip(1).StringJoin();
            }
        }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public DropCapTextBlock()
        {
            InitializeComponent();
        }

        #endregion Constructors
    }
}