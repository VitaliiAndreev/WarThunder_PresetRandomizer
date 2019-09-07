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
    }
}