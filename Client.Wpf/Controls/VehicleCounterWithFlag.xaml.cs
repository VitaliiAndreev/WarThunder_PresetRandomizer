using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Core.DataBase.WarThunder.Enumerations;
using System.Windows;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for VehicleCounterWithFlag.xaml. </summary>
    public partial class VehicleCounterWithFlag : VehicleCounter
    {
        #region Constructors

        public VehicleCounterWithFlag()
        {
            InitializeComponent();
        }

        public VehicleCounterWithFlag(ECountry country, int count, Thickness margin)
            : base(count, margin)
        {
            InitializeComponent();

            var image = new Image
            {
                Style = this.GetStyle(EStyleKey.Image.FlagIcon16px),
                Source = ApplicationHelpers.Manager.GetFlagImageSource(country),
                Margin = new Thickness(0, 0, 5, 0),
                ToolTip = ApplicationHelpers.LocalizationManager.GetLocalizedString(country.ToString()),
            };

            _panel.Children.Add(image);
            _panel.Children.Add(_count);
        }

        #endregion Constructors
    }
}