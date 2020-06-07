using Core.Enumerations;
using Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace Client.Shared.Wpf.Extensions
{
    public static class ColorExtensions
    {
        #region Methods: From()

        public static Color From(this Color color, byte allColors) => color.From(allColors, allColors, allColors);

        public static Color From(this Color color, byte red, byte green, byte blue)
        {
            color.R = red;
            color.G = green;
            color.B = blue;

            return color;
        }

        #endregion Methods: From()
    }
}