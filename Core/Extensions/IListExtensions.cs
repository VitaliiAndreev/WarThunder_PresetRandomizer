using System.Collections.Generic;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="IList{T}"/> interface. </summary>
    public static class IListExtensions
    {
        #region Methods: Fluency

        /// <summary> Add items from the specified collection into the list in bulk. </summary>
        /// <typeparam name="T"> The type of items in both collections. </typeparam>
        /// <param name="list"> The source list. </param>
        /// <param name="collection"> The donor collection, i.e. whose elements are to be added into the list. </param>
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> collection)
        {
            foreach (var item in collection)
                list.Add(item);
        }

        public static void AddIfNotPresent<T>(this IList<T> list, T item)
        {
            if (!list.Contains(item))
                list.Add(item);
        }

        public static void RemoveSafely<T>(this IList<T> list, T item)
        {
            if (list.Contains(item))
                list.Remove(item);
        }

        #endregion Methods: Fluency
    }
}