using System.Collections.Generic;

namespace Core
{
    public static class ICollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> sourceCollection, IEnumerable<T> elementsToAdd)
        {
            foreach (var element in elementsToAdd)
                sourceCollection.Add(element);
        }

        /// <summary> Creates a shallow copy, i.e. a new collection with same contents. </summary>
        /// <typeparam name="T"> A type of collection elements. </typeparam>
        /// <param name="sourceCollection"> The collection to copy. </param>
        /// <returns></returns>
        public static ICollection<T> Copy<T>(this ICollection<T> sourceCollection) =>
            new List<T>(sourceCollection);

        /// <summary> Removes from this collection first occurrences of the objects present in the specified collection. </summary>
        /// <typeparam name="T"> The type of collection elements. </typeparam>
        /// <param name="sourceCollection"> The source collection to remove from. </param>
        /// <param name="elementsToDelete"> The collection whose contents should be removed from the <paramref name="sourceCollection"/>. </param>
        public static void RemoveRange<T>(this ICollection<T> sourceCollection, IEnumerable<T> elementsToDelete)
        {
            foreach (var element in elementsToDelete)
                sourceCollection.Remove(element);
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