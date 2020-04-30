using System.Windows;

namespace Client.Wpf.Controls.Base
{
    public class VehicleCount : TextLabel
    {
        #region Constructors

        public VehicleCount()
        {
        }

        public VehicleCount(int count, Thickness margin)
            : base(count.ToString(), margin)
        {
        }

        #endregion Constructors
    }
}