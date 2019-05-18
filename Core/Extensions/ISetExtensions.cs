using System.Collections.Generic;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="ISet{T}"/> interface. </summary>
    public static class ISetExtensions
    {
        /// <summary> Adds elements from the specified collection to the set. </summary>
        /// <typeparam name="T"> A generic type. </typeparam>
        /// <param name="set"> A set to add to. </param>
        /// <param name="collection"> The collection whose elements to add. </param>
        public static void AddRange<T>(this ISet<T> set, IEnumerable<T> collection)
        {
            foreach (var item in collection)
                set.Add(item);
        }
    }
}
