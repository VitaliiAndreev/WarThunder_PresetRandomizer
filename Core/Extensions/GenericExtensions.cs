using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions
{
    /// <summary> Methods extending a generic class. </summary>
    public static class GenericExtensions
    {
        /// <summary> Fluently determines whether a collection contains the specified object. </summary>
        /// <typeparam name="T"> The object type. </typeparam>
        /// <param name="source"> The source object. </param>
        /// <param name="collection"> A collection to check. </param>
        /// <returns></returns>
        public static bool IsIn<T>(this T source, IEnumerable<T> collection) =>
            collection.Contains(source);

        /// <summary> Fluently determines whether the given dictionary contains the object as a key. </summary>
        /// <typeparam name="T"> The object type. </typeparam>
        /// <typeparam name="U"> The dictionary value type. </typeparam>
        /// <param name="source"> The source object. </param>
        /// <param name="collection"> The dictionary to check. </param>
        /// <returns></returns>
        public static bool IsKeyIn<T, U>(this T source, IDictionary<T, U> collection) =>
            collection.ContainsKey(source);
    }
}