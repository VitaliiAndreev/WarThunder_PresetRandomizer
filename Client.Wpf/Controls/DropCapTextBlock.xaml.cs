using Core;
using Core.Extensions;
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
                _otherText.FontSize = value > Integer.Number.Five
                    ? value - Integer.Number.Five
                    : Integer.Number.One;
            }
        }

        /// <summary> Text contents. </summary>
        public string Text
        {
            get => $"{_dropCap.Text}{_otherText.Text}";
            set
            {
                _dropCap.Text = value.Take(Integer.Number.One).StringJoin();
                _otherText.Text = value.Skip(Integer.Number.One).StringJoin();
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