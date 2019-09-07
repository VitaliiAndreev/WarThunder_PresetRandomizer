using Core.Objects;
using System;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="IComparable"/> interface. </summary>
    public static class IComparableExtensions
    {
        /// <summary> Checks whether the given value is within the interval. </summary>
        /// <param name="item"> The number to check. </param>
        /// <param name="interval"> The interval to check. </param>
        /// <returns></returns>
        public static bool IsIn<T>(this T item, Interval<T> interval) where T : IComparable =>
            interval.Contains(item);
    }
}