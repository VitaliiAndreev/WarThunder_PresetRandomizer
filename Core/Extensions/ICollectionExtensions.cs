using System.Collections.Generic;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="ICollection{T}"/> interface. </summary>
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

        /// <summary> Replaces contents of the collection with those of the given collection. </summary>
        /// <typeparam name="T"> The type of collection elements. </typeparam>
        /// <param name="sourceCollection"> The source collection. </param>
        /// <param name="donorCollection"> The collection whose contents replace those of the source collection. </param>
        public static void ReplaceBy<T>(this ICollection<T> sourceCollection, IEnumerable<T> donorCollection)
        {
            sourceCollection.Clear();

            foreach (var element in donorCollection)
                sourceCollection.Add(element);
        }
    }
}