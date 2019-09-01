using System;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="long"/> class. </summary>
    public static class LongExtensions
    {
        #region Methods: Fluency

        /// <summary> Fluently converts the value into a an equivalent 32-bit signed integer. </summary>
        /// <param name="value"> The source value. </param>
        /// <returns></returns>
        public static int ConvertToInt(this long value) =>
            Convert.ToInt32(value);

        #endregion Methods: Fluency
    }
}