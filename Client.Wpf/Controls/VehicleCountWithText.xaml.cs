using Client.Wpf.Controls.Base;
using System.Windows;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for VehicleCounterWithText.xaml. </summary>
    public partial class VehicleCountWithText : VehicleCount
    {
        #region Constructors

        public VehicleCountWithText()
        {
            InitializeComponent();
        }

        public VehicleCountWithText(string text, int count, Thickness margin)
            : base(count, margin)
        {
            InitializeComponent();

            var textBlock = new TextBlock
            {
                Style = _textStyle,
                Text = text,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 5, 0),
            };

            _panel.Children.Add(textBlock);
            _panel.Children.Add(_count);
        }

        #endregion Constructors
    }
}