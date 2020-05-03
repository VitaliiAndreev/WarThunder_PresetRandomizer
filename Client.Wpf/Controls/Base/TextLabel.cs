using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace Client.Wpf.Controls.Base
{
    public class TextLabel : LocalisedUserControl
    {
        #region Fields

        protected readonly Style _textStyle;
        protected readonly TextBlock _label;

        #endregion Fields
        #region Constructors

        public TextLabel()
        {
        }

        public TextLabel(string text, Thickness margin, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left, bool isBold = false)
        {
            _textStyle = this.GetStyle(EStyleKey.TextBlock.TextBlock12px);
            _label = new TextBlock
            {
                Style = _textStyle,
                HorizontalAlignment = horizontalAlignment,
                FontWeight = isBold ? FontWeights.Bold : FontWeights.Normal,
                Text = text,
            };

            Margin = margin;
        }

        #endregion Constructors
    }
}