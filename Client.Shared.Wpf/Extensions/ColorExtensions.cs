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

        #endregion Methods: From()

        public static IEnumerable<Color> InterpolateTo(this Color startColor, Color endColor, int stepCount)
        {
            if (startColor == endColor)
                return Enumerable.Range(Integer.Number.Zero, stepCount).Select(number => startColor);

            var colors = new List<Color>();
            var rSteps = startColor.R.InterpolateTo(endColor.R, stepCount);
            var gSteps = startColor.G.InterpolateTo(endColor.G, stepCount);
            var bSteps = startColor.B.InterpolateTo(endColor.B, stepCount);

            for (var stepNumber = Integer.Number.Zero; stepNumber < stepCount; stepNumber++)
            {
                var stepColor = new Color().From
                (
                    rSteps.At(stepNumber),
                    gSteps.At(stepNumber),
                    bSteps.At(stepNumber)
                );

                colors.Add(stepColor);
            }

            return colors;
        }
    }
}