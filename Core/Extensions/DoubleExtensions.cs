namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="double"/> structure. </summary>
    public static class DoubleExtensions
    {
        #region Fluency

        #region Type Casting

        /// <summary> Directly casts the number into <see cref="int"/>. </summary>
        /// <param name="number"> The source number. </param>
        /// <returns></returns>
        public static int CastToInt(this double number) => (int)number;

        #endregion Type Casting

        #endregion Fluency
    }
}