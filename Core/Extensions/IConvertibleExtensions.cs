using System;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="IConvertible"/> interface. </summary>
    public static class IConvertibleExtensions
    {
        /// <summary> Coverts the value into the equivalent one of the given type. </summary>
        /// <typeparam name="T"> The type of the source value. </typeparam>
        /// <typeparam name="U"> The output type. </typeparam>
        /// <param name="value"> The value to convert. </param>
        /// <returns></returns>
        public static U ConvertFromTo<T, U>(this T value) where T : IConvertible =>
            Convert.ChangeType(value, typeof(U)).CastTo<U>();
    }
}