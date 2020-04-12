using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace Client.Wpf.Extensions
{
    public static class BitmapExtensions
    {
        public static BitmapSource ToBitmapSource(this Bitmap bitmap, ImageFormat format)
        {
            using (var memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, format);

                memoryStream.Position = 0;

                var bitmapimage = new BitmapImage();

                bitmapimage.BeginInit();
                {
                    bitmapimage.StreamSource = memoryStream;
                    bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                }
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
    }
}