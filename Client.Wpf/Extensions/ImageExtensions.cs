using Client.Shared.Wpf.Extensions;
using Core.DataBase.WarThunder.Enumerations;
using System.Windows;
using System.Windows.Controls;

namespace Client.Wpf.Extensions
{
    public static class ImageExtensions
    {
        public static void SetFlag(this Image image, ECountry country, double size, Thickness margin)
        {
            if (image.Source is null)
            {
                image.Source = ApplicationHelpers.Manager.GetFlagImageSource(country);
                image.SetSize(size);
                image.Margin = margin;
            }
        }
    }
}