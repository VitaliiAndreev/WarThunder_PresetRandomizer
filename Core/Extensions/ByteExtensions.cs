using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions
{
    public static class ByteExtensions
    {
        /// <summary>
        /// Calculates the <paramref name="stepCount"/> amount of points between the <paramref name="start"/> and the <paramref name="end"/>.
        /// Start and end points are not included in the output.
        /// </summary>
        /// <param name="start"> The starting point. </param>
        /// <param name="end"> The endpoint. </param>
        /// <param name="stepCount"> The amount of steps to interpolate. </param>
        /// <returns></returns>
        public static IEnumerable<byte> InterpolateTo(this byte start, byte end, int stepCount)
        {
            if (start == end)
                return Enumerable.Range(Integer.Number.Zero, stepCount).Select(number => start);

            var reversed = start > end;
            var steps = new List<byte>();

            var actualStart = reversed ? end : start;
            var actualEnd = reversed ? start : end;

            for (var stepNumber = Integer.Number.One; stepNumber <= stepCount; stepNumber++)
            {
                var step = Convert.ToByte(actualStart + ((actualEnd - actualStart) * stepNumber / (stepCount + Integer.Number.One)));
                steps.Add(step);
            }

            if (reversed)
                steps.Reverse();

            return steps;
        }
    }
}