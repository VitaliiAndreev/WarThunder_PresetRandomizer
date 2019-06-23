using Core.Objects.Abstract;

namespace Core.Objects
{
    /// <summary> An interval of decimal values. </summary>
    public class IntervalDecimal : IntervalBase
    {
        /// <summary> The left endpoint of the interval. </summary>
        public decimal LeftEndpoint { get; }
        /// <summary> The right endpoint of the interval. </summary>
        public decimal RightEndpoint { get; }

        /// <summary> Creates a new interval of decimal values. </summary>
        /// <param name="leftBounded"> Whether the left endpoint is included in the interval. </param>
        /// <param name="leftEndpoint"> The left endpoint of the interval. </param>
        /// <param name="rightEndpoint"> The right endpoint of the interval. </param>
        /// <param name="rightBounded"> Whether the right endpoint is included in the interval. </param>
        public IntervalDecimal(bool leftBounded, decimal leftEndpoint, decimal rightEndpoint, bool rightBounded)
            : base(leftBounded, rightBounded)
        {
            LeftEndpoint = leftEndpoint;
            RightEndpoint = rightEndpoint;
        }

        /// <summary> Checks whether the given value is within the interval. </summary>
        /// <param name="value"> The value to check. </param>
        /// <returns></returns>
        public bool Contains(decimal value)
        {
            return (LeftBounded ? value >= LeftEndpoint : value > LeftEndpoint)
                && (RightBounded ? value <= RightEndpoint : value < RightEndpoint);
        }
    }
}