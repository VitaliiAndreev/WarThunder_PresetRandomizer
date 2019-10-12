using Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="IEnumerable{T}"/> interface. </summary>
    public static class IEnumerableExtensions
    {
        #region Methods: Fluency

        /// <summary> Adds the elements of the <paramref name="collection"/> to the end of the specified <paramref name="list"/>. </summary>
        /// <typeparam name="T"> The type of collection items. </typeparam>
        /// <param name="collection"> The collection to add into the <paramref name="list"/>. </param>
        /// <param name="list"> The list to which to add items from <paramref name="collection"/> into. </param>
        public static void AddInto<T>(this IEnumerable<T> collection, List<T> list) =>
            list.AddRange(collection);

        /// <summary> Returns a new enumeration excluding the given <paramref name="element"/>. </summary>
        /// <typeparam name="T"> The type of collection items. </typeparam>
        /// <param name="collection"> The collection to exclude <paramref name="element"/> from. </param>
        /// <param name="element"> The element to exclude from the <paramref name="collection"/>. </param>
        /// <returns></returns>
        public static IEnumerable<T> Except<T>(this IEnumerable<T> collection, T element) =>
            collection.Except(new List<T> { element });

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
            collection.Count() == EInteger.Number.One;

        /// <summary> Fluently checks whether a collection contains more than one item. </summary>
        /// <typeparam name="T"> A generic type. </typeparam>
        /// <param name="collection"> A source collection. </param>
        /// <param name="predicate"> The predicate to filter what items to count. </param>
        /// <returns></returns>
        public static bool HasSeveral<T>(this IEnumerable<T> collection, Func<T, bool> predicate = null)
        {
            if (predicate is null)
                return collection.Count() > EInteger.Number.One;
            else
                return collection.Count(predicate) > EInteger.Number.One;
        }

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