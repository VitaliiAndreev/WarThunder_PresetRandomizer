using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="IEnumerable{T}"/> interface. </summary>
    public static class IEnumerableExtensions
    {
        #region Methods: Fluency

        /// <summary> Adds the elements of the collection to the end of the specified list. </summary>
        /// <typeparam name="T"> A generic type. </typeparam>
        /// <param name="sourceCollection"> A source collection. </param>
        /// <param name="targetCollection"> The list to which to add into. </param>
        public static void AddInto<T>(this IEnumerable<T> sourceCollection, List<T> targetCollection) =>
            targetCollection.AddRange(sourceCollection);

        #region IsEmpty(), HasSingle(), HasMany()

        /// <summary> Fluently checks whether a collection is empty. </summary>
        /// <typeparam name="T"> A generic type. </typeparam>
        /// <param name="collection"> A source collection. </param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this IEnumerable<T> collection) =>
            !collection.Any();

        /// <summary> Fluently checks whether a collection contains exactly one item. </summary>
        /// <typeparam name="T"> A generic type. </typeparam>
        /// <param name="collection"> A source collection. </param>
        /// <returns></returns>
        public static bool HasSingle<T>(this IEnumerable<T> collection) =>
            collection.Count() == 1;

        /// <summary> Fluently checks whether a collection contains more than one item. </summary>
        /// <typeparam name="T"> A generic type. </typeparam>
        /// <param name="collection"> A source collection. </param>
        /// <returns></returns>
        public static bool HasSeveral<T>(this IEnumerable<T> collection) =>
            collection.Count() > 1;

        #endregion IsEmpty(), HasSingle(), HasMany()
        #region SkipLast()

        /// <summary> Returns all items from a collection except the last one. </summary>
        /// <typeparam name="T"> A generic type. </typeparam>
        /// <param name="collection"> A source collection. </param>
        /// <returns></returns>
        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> collection) =>
            collection.SkipLast(1);

        /// <summary> Returns all items from a collection except the specified number of trailing items. </summary>
        /// <typeparam name="T"> A generic type. </typeparam>
        /// <param name="collection"> A source collection. </param>
        /// <param name="amountOfItemsToSkip"> The amount of trailing items to skip. </param>
        /// <returns></returns>
        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> collection, int amountOfItemsToSkip) =>
            collection.Take(collection.Count() - amountOfItemsToSkip);

        #endregion SkipLast()
        #region StringJoin<T>()

        /// <summary> Fluently concatenates members of a collection using the specified separator between each member. </summary>
        /// <typeparam name="T"> A generic type. </typeparam>
        /// <param name="collection"> A source collection. </param>
        /// <param name="separator"> A separator character to insert between each member. </param>
        /// <returns></returns>
        public static string StringJoin<T>(this IEnumerable<T> collection, char separator) =>
            collection.StringJoin(separator.ToString());

        /// <summary> Fluently concatenates members of a collection. </summary>
        /// <typeparam name="T"> A generic type. </typeparam>
        /// <param name="collection"> A source collection. </param>
        /// <returns></returns>
        public static string StringJoin<T>(this IEnumerable<T> collection) =>
            string.Join(string.Empty, collection);

        /// <summary> Fluently concatenates members of a collection using the specified separator between each member. </summary>
        /// <typeparam name="T"> A generic type. </typeparam>
        /// <param name="collection"> A source collection. </param>
        /// <param name="separator"> A separator string to insert between each member. </param>
        /// <returns></returns>
        public static string StringJoin<T>(this IEnumerable<T> collection, string separator) =>
            string.Join(separator, collection);

        #endregion StringJoin<T>()

        #endregion Methods: Fluency
    }
}