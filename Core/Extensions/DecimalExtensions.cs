using System;

namespace Core
{
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
            number > 0m;

        public static bool IsInteger(this decimal number) =>
            number % 1 == 0;

        #endregion Math

        #endregion Fluency
    }
}