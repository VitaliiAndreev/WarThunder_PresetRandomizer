using System.Windows.Media;

namespace Client.Shared.Wpf
{
    public static partial class ColorExtensions
    {
        public static Color From(this Color color, byte allColors) => color.From(byte.MaxValue, allColors, allColors, allColors);

        public static Color From(this Color color, byte red, byte green, byte blue) => color.From(byte.MaxValue, red, green, blue);

        public static Color From(this Color color, byte alpha, byte red, byte green, byte blue)
        {
            color.A = alpha;
            color.R = red;
            color.G = green;
            color.B = blue;

            return color;
        }
    }
}