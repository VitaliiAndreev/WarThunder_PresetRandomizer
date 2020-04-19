using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace Client.Wpf.Extensions
{
    public static class ByteArrayExtensions
    {
        public static BitmapSource ToBitmapSource(this byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
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
