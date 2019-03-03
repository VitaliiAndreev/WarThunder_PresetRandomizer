namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="int"/> class. </summary>
    public static class IntExtensions
    {
        /// <summary> Checks whether the number is positive. </summary>
        /// <param name="value"> The number to check. </param>
        /// <returns></returns>
        public static bool IsPositive(this int value) =>
            value > 0;
    }
}
