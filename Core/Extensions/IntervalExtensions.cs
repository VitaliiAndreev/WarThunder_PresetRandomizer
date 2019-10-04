using Core.Enumerations;
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

            for (var integer = interval.LeftBounded ? interval.LeftItem : interval.LeftItem + EInteger.Number.One; integer <= (interval.RightBounded ? interval.RightItem : interval.RightItem - EInteger.Number.One); integer++)
                integers.Add(integer);

            return integers;
        }
    }
}