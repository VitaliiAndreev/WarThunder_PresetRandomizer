using System.Globalization;

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
            value < Integer.Number.Zero;

        /// <summary> Checks whether the number is positive. </summary>
        /// <param name="value"> The number to check. </param>
        /// <returns></returns>
        public static bool IsPositive(this int value) =>
            value > Integer.Number.Zero;

        /// <summary> Checks whether the number is zero. </summary>
        /// <param name="value"> The number to check. </param>
        /// <returns></returns>
        public static bool IsZero(this int value) =>
            value == Integer.Number.Zero;

        #endregion Fluency
        #region Formatting

        public static string WithNumberGroupsSeparated(this int number, string separator = " ")
        {
            return number.ToString("N0", new NumberFormatInfo { NumberGroupSeparator = separator });
        }

        #endregion Formatting

        /// <summary> Calculates the amount of bits required to hold the value. </summary>
        /// <param name="value"> The value to work with. </param>
        /// <returns></returns>
        public static int GetBitCount(this int value)
        {
            var bitCount = default(int);

            while (value != Integer.Number.Zero)
            {
                value &= (value - Integer.Number.One);
                bitCount++;
            }
            return bitCount;
        }
    }
}