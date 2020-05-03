using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Connectors;
using Core.Enumerations;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for VehicleCounterWithFlag.xaml. </summary>
    public partial class TextLabelWithGaijinCharacterIcon : TextLabel
    {
        #region Fields

        private readonly Style _iconStyle;

        #endregion Fields
        #region Constructors

        public TextLabelWithGaijinCharacterIcon()
        {
            InitializeComponent();
        }

        public TextLabelWithGaijinCharacterIcon(string gaijinIcon, string text, Thickness margin, MouseButtonEventHandler mouseDownHandler, HorizontalAlignment textHorizontalAlignment = HorizontalAlignment.Left, bool isBold = false)
            : base(text, margin, textHorizontalAlignment, isBold)
        {
            InitializeComponent();

            _iconStyle = this.GetStyle(EStyleKey.TextBlock.TextBlockWithSkyQuake16px);

            var icon = new TextBlock
            {
                Margin = new Thickness(EInteger.Number.Zero, EInteger.Number.Zero, EInteger.Number.Five, EInteger.Number.Zero),
                Style = _iconStyle,
                Text = gaijinIcon.ToString(),
            };

            _panel.Children.Add(icon);
            _panel.Children.Add(_label);

            MouseDown += mouseDownHandler;
        }

        public TextLabelWithGaijinCharacterIcon(char gaijinIcon, string text, Thickness margin, MouseButtonEventHandler mouseDownHandler, HorizontalAlignment textHorizontalAlignment = HorizontalAlignment.Left, bool isBold = false)
            : this(gaijinIcon.ToString(), text, margin, mouseDownHandler, textHorizontalAlignment, isBold)
        {
        }

        #endregion Constructors

        public void SetTag<T>(T tag)
        {
            Tag = tag;
        }
    }
}