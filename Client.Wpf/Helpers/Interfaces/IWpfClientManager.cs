using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Organization.Helpers.Interfaces;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Client.Wpf.Helpers.Interfaces
{
    /// <summary> Controls the flow of the application. </summary>
    public interface IWpfClientManager : IManager
    {
        #region Methods: Working with Caches

        ImageSource GetFlagImageSource(ECountry country);

        BitmapSource GetIconBitmapSource(IVehicle vehicle);

        BitmapSource GetPortraitBitmapSource(IVehicle vehicle);

        #endregion Methods: Working with Caches
    }
}