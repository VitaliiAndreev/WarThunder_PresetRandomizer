using System;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="IComparable"/> interface. </summary>
    public static class IComparableExtensions
    {
        /// <summary> Checks whether the item is included in the interval defined by parameters. </summary>
        /// <param name="item"> The item to check. </param>
        /// <param name="includeLeftItem"> Whether to include the left item. </param>
        /// <param name="leftItem"> The left item of the interval. </param>
        /// <param name="rightItem"> Whether to include the left item. </param>
        /// <param name="includeRightItem"> The right item of the interval. </param>
        /// <returns></returns>
        public static bool IsBetween<T>(this T item, bool includeLeftItem, T leftItem, T rightItem, bool includeRightItem) where T : IComparable
        {
            var itemComparedToLeftItem = item.CompareTo(leftItem);
            var itemComparedToRightItem = item.CompareTo(rightItem);

            var greaterThanLeftItem = itemComparedToLeftItem.IsPositive();

            if (includeLeftItem)
                greaterThanLeftItem |= itemComparedToLeftItem.IsZero();

            var lessThanRightItem = itemComparedToRightItem.IsNegative();

            if (includeRightItem)
                lessThanRightItem |= itemComparedToRightItem.IsZero();

            return greaterThanLeftItem && lessThanRightItem;
        }
    }
}