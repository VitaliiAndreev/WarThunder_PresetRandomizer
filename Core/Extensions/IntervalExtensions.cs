using Core.Objects;
using System.Collections.Generic;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="Interval{T}"/> class. </summary>
    public static class IntervalExtensions
    {
        /// <summary> Returns integers in the interval. </summary>
        /// <param name="interval"> The interval to extract values from. </param>
        /// <returns></returns>
        public static IEnumerable<int> AsEnumerable(this Interval<int> interval)
        {
            var integers = new List<int>();
            var firstInteger = interval.LeftBounded
                ? interval.LeftItem
                : interval.LeftItem + 1;
            var lastInteger = interval.RightBounded
                ? interval.RightItem
                : interval.RightItem - 1;

            for (var integer = firstInteger; integer <= lastInteger; integer++)
                integers.Add(integer);

            return integers;
        }
    }
}