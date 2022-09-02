using Client.Shared.Wpf.Extensions;
using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
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

        public TextLabelWithGaijinCharacterIcon
        (
            char icon,
            string text,
            Thickness margin,
            MouseButtonEventHandler mouseDownHandler,
            double iconColumnWidth,
            double iconFontSize,
            double? countColumnWidth = null,
            HorizontalAlignment textHorizontalAlignment = HorizontalAlignment.Left,
            bool isBold = false
        ) : base(text, margin, textHorizontalAlignment, isBold)
        {
            InitializeComponent();

            _iconStyle = this.GetStyle(EStyleKey.TextBlock.TextBlockWithSkyQuakeUncondensed);
            _iconColumnDefinition.Width = new GridLength(iconColumnWidth, GridUnitType.Pixel);
            _labelColumnDefinition.Width = countColumnWidth.HasValue
                ? new GridLength(countColumnWidth.Value, GridUnitType.Pixel)
                : new GridLength(default, GridUnitType.Auto);

            var iconControl = new TextBlock
            {
                Style = _iconStyle,
                Margin = new Thickness(Integer.Number.Zero, -Integer.Number.Ten, Integer.Number.Three, -Integer.Number.Ten),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                FontSize = iconFontSize,
                Text = icon.ToString(),
            };

            _grid.Add(iconControl, Integer.Number.Zero, Integer.Number.Zero);
            _grid.Add(_label, Integer.Number.One, Integer.Number.Zero);

            if (!(mouseDownHandler is null))
                MouseDown += mouseDownHandler;
        }

        #endregion Constructors

        public TextLabelWithGaijinCharacterIcon WithTag<T>(T tag)
        {
            Tag = tag;

            return this;
        }
    }
}