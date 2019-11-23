using Core.Enumerations;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="int"/> class. </summary>
    public static class IntExtensions
    {
        #region Fluency

        /// <summary> Checks whether the number is negative. </summary>
        /// <param name="value"> The number to check. </param>
        /// <returns></returns>
        public static bool IsNegative(this int value) =>
            value < EInteger.Number.Zero;

        /// <summary> Checks whether the number is positive. </summary>
        /// <param name="value"> The number to check. </param>
        /// <returns></returns>
        public static bool IsPositive(this int value) =>
            value > EInteger.Number.Zero;

        /// <summary> Checks whether the number is zero. </summary>
        /// <param name="value"> The number to check. </param>
        /// <returns></returns>
        public static bool IsZero(this int value) =>
            value == EInteger.Number.Zero;

        #endregion Fluency

        /// <summary> Calculates the amount of bits required to hold the value. </summary>
        /// <param name="value"> The value to work with. </param>
        /// <returns></returns>
        public static int GetBitCount(this int value)
        {
            var bitCount = default(int);

            while (value != EInteger.Number.Zero)
            {
                value &= (value - EInteger.Number.One);
                bitCount++;
            }
            return bitCount;
        }
    }
}