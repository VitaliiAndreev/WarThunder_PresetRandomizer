using Core.Enumerations;
using System;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="decimal"/> structure. </summary>
    public static class DecimalExtensions
    {
        #region Fluency

        #region Math

        /// <summary> Returns the largest integer less than or equal to the specified decimal number. </summary>
        /// <param name="number"> The number to process. </param>
        /// <returns></returns>
        public static decimal Floor(this decimal number) =>
            Math.Floor(number);

        public static bool IsPositive(this decimal number) =>
number > Enumerations.Decimal.Number.Zero;

        public static bool IsInteger(this decimal number) =>
            number % Integer.Number.One == Integer.Number.Zero;

        #endregion Math

        #endregion Fluency
    }
}