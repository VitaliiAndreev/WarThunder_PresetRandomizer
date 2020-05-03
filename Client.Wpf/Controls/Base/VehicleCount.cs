using System.Windows;

namespace Client.Wpf.Controls.Base
{
    public class VehicleCount : TextLabel
    {
        #region Constructors

        public VehicleCount()
        {
        }

        public VehicleCount(int count, Thickness margin, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left)
            : base(count.ToString(), margin, horizontalAlignment)
        {
        }

        #endregion Constructors
    }
}