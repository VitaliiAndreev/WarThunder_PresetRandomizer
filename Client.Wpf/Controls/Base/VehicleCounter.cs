using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace Client.Wpf.Controls.Base
{
    public class VehicleCounter : LocalizedUserControl
    {
        #region Fields

        protected readonly Style _textStyle;
        protected readonly TextBlock _count;

        #endregion Fields
        #region Constructors

        public VehicleCounter()
        {
        }

        public VehicleCounter(int count, Thickness margin)
        {
            _textStyle = this.GetStyle(EStyleKey.TextBlock.TextBlock12px);
            _count = new TextBlock
            {
                Style = _textStyle,
                Text = count.ToString(),
            };

            Margin = margin;
        }

        #endregion Constructors
    }
}