using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace Client.Shared.Wpf
{
    public static partial class ColorExtensions
    {
        public static IEnumerable<Color> GetInterpolatedRangeTo(this Color startColor, Color endColor, int stepCount)
        {
            if (startColor == endColor)
                return Enumerable.Range(start: 0, stepCount).Select(number => startColor);

            var colors = new List<Color>();
            var rSteps = startColor.R.GetInterpolatedRangeTo(endColor.R, stepCount);
            var gSteps = startColor.G.GetInterpolatedRangeTo(endColor.G, stepCount);
            var bSteps = startColor.B.GetInterpolatedRangeTo(endColor.B, stepCount);

            for (var stepNumber = 0; stepNumber < stepCount; stepNumber++)
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

        /// <summary>
        /// Calculates the <paramref name="stepCount"/> amount of points between the <paramref name="start"/> and the <paramref name="end"/>.
        /// Start and end points are not included in the output.
        /// </summary>
        /// <param name="start"> The starting point. </param>
        /// <param name="end"> The endpoint. </param>
        /// <param name="stepCount"> The amount of steps to interpolate. </param>
        /// <returns></returns>
        private static IEnumerable<byte> GetInterpolatedRangeTo(this byte start, byte end, int stepCount)
        {
            if (start == end)
                return Enumerable.Range(0, stepCount).Select(number => start);

            var reversed = start > end;
            var steps = new List<byte>();

            var actualStart = reversed ? end : start;
            var actualEnd = reversed ? start : end;

            for (var stepNumber = 1; stepNumber <= stepCount; stepNumber++)
            {
                var step = Convert.ToByte(actualStart + ((actualEnd - actualStart) * stepNumber / (stepCount + 1)));
                steps.Add(step);
            }

            if (reversed)
                steps.Reverse();

            return steps;
        }
    }
}