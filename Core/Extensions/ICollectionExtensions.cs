using System.Collections.Generic;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="ICollection{T}"/> class. </summary>
    public static class ICollectionExtensions
    {
        /// <summary> Creates a copy of the collection, i.e. a new collection with same contents. </summary>
        /// <typeparam name="T"> A generic type. </typeparam>
        /// <param name="sourceCollection"> A source collection. </param>
        /// <returns></returns>
        public static ICollection<T> Copy<T>(this ICollection<T> sourceCollection)
        {
            var newCollection = new List<T>();

            foreach (var item in sourceCollection)
                newCollection.Add(item);

            return newCollection;
        }
    }
}
