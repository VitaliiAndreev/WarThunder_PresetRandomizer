using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.IO;

namespace Core.UnpackingToolsIntegration.Objects
{
    /// <summary>
    /// A class copied from the DDS reader project (https://github.com/andburn/dds-reader) by Andrew Burnett (https://github.com/andburn).
    /// Source code is copied (with minor edits) because there is no NuGet package available.
    /// </summary>
    public class DdsImage
    {
        #region Fields

        private readonly Pfim.IImage _image;

        #endregion Fields
        #region Properties

        public byte[] Data
        {
            get
            {
                if (_image != null)
                    return _image.Data;

                else
                    return new byte[0];
            }
        }

        #endregion Properties
        #region Constructors

        public DdsImage(string file)
        {
            _image = Pfim.Pfim.FromFile(file);

            Process();
        }

        public DdsImage(Stream stream)
        {
            if (stream == null)
                throw new Exception("DdsImage(Stream): the stream is null.");

            _image = Pfim.Dds.Create(stream, new Pfim.PfimConfig());

            Process();
        }

        public DdsImage(byte[] data)
        {
            if (data == null || data.Length <= 0)
                throw new Exception("DdsImage(byte[]): no data.");

            _image = Pfim.Dds.Create(data, new Pfim.PfimConfig());

            Process();
        }

        #endregion Constructors

        private void Process()
        {
            if (_image == null)
                throw new Exception("DdsImage image creation failed.");

            if (_image.Compressed)
                _image.Decompress();
        }

        public void Save(string file)
        {
            if (_image.Format == Pfim.ImageFormat.Rgba32)
                Save<Bgra32>(file);

            else if (_image.Format == Pfim.ImageFormat.Rgb24)
                Save<Bgr24>(file);

            else
                throw new Exception("Unsupported pixel format (" + _image.Format + ").");
        }

        private void Save<T>(string file) where T : struct, IPixel<T>
        {
            Image
                .LoadPixelData<T>(_image.Data, _image.Width, _image.Height)
                .Save(file)
            ;
        }
    }
}