using Core.Enumerations;
using Core.Extensions;
using Core.Objects.Abstract;
using System;

namespace Core.Objects
{
    /// <summary> An interval of decimal values. </summary>
    public class Interval<T> : IntervalBase
        where T: IComparable
    {
        #region Properties

        /// <summary> The left endpoint of the interval. </summary>
        public T LeftItem { get; }
        /// <summary> The right endpoint of the interval. </summary>
        public T RightItem { get; }

        #endregion Properties
        #region Constructors

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

        #endregion Constructors

        /// <summary> Returns a string that represents the instance. </summary>
        /// <returns></returns>
        public override string ToString() => $"{(LeftBounded ? Character.BracketLeft : Character.ParenthesisLeft)} {LeftItem}; {RightItem} {(RightBounded ? Character.BracketRight : Character.ParenthesisRight)}";

        /// <summary> Determines whether the specified object is equal to the current object. </summary>
        /// <param name="obj"> The object to compare with the current object. </param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Interval<T> otherInterval))
                return false;

            return LeftBounded == otherInterval.LeftBounded
                && LeftItem.CompareTo(otherInterval.LeftItem) == Integer.Number.Zero
                && RightItem.CompareTo(otherInterval.RightItem) == Integer.Number.Zero
                && RightBounded == otherInterval.RightBounded;
        }

        /// <summary> Serves as the default hash function. </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 101;

                hash = hash * 103 + LeftBounded.GetHashCode();
                hash = hash * 107 + LeftItem.GetHashCode();
                hash = hash * 109 + RightItem.GetHashCode();
                hash = hash * 113 + RightBounded.GetHashCode();

                return hash;
            }
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