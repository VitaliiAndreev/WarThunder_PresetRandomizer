namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="int"/> class. </summary>
    public static class IntExtensions
    {
        #region Fluency

        #region Type Casting

        /// <summary> Directly casts the number into <see cref="double"/>. </summary>
        /// <param name="number"> The source number. </param>
        /// <returns></returns>
        public static double CastToDouble(this int number) => (double)number;

        #endregion Type Casting

        /// <summary> Checks whether the number is positive. </summary>
        /// <param name="value"> The number to check. </param>
        /// <returns></returns>
        public static bool IsPositive(this int value) =>
            value > 0;

        #endregion Fluency
    }
}