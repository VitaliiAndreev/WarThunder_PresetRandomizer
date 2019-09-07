using Core.Extensions;
using Core.Objects.Abstract;
using System;

namespace Core.Objects
{
    /// <summary> An interval of decimal values. </summary>
    public class Interval<T> : IntervalBase
        where T: IComparable
    {
        /// <summary> The left endpoint of the interval. </summary>
        public T LeftItem { get; }
        /// <summary> The right endpoint of the interval. </summary>
        public T RightItem { get; }

        /// <summary> Creates a new interval of decimal values. </summary>
        /// <param name="leftBounded"> Whether the left endpoint is included in the interval. </param>
        /// <param name="leftItem"> The left endpoint of the interval. </param>
        /// <param name="rightItem"> The right endpoint of the interval. </param>
        /// <param name="rightBounded"> Whether the right endpoint is included in the interval. </param>
        public Interval(bool leftBounded, T leftItem, T rightItem, bool rightBounded)
            : base(leftBounded, rightBounded)
        {
            LeftItem = leftItem;
            RightItem = rightItem;
        }

        /// <summary> Checks whether the given value is within the interval. </summary>
        /// <param name="item"> The value to check. </param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            var itemComparedToLeftItem = item.CompareTo(LeftItem);
            var itemComparedToRightItem = item.CompareTo(RightItem);

            var greaterThanLeftItem = itemComparedToLeftItem.IsPositive();

            if (LeftBounded)
                greaterThanLeftItem |= itemComparedToLeftItem.IsZero();

            var lessThanRightItem = itemComparedToRightItem.IsNegative();

            if (RightBounded)
                lessThanRightItem |= itemComparedToRightItem.IsZero();

            return greaterThanLeftItem && lessThanRightItem;
        }
    }
}