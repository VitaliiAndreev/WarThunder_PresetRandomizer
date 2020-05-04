using Core.Enumerations;
using Core.Enumerations.Logger;
using System;
using System.Collections;
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

        public static bool AllEqual<T>(this IEnumerable<T> collection) =>
            collection.Distinct().Count() == EInteger.Number.One;

        #region At()

        public static T At<T>(this IEnumerable<T> collection, int position)
        {
            if (collection.IsEmpty())
                throw new ArgumentException();

            if (position.IsNegative() || position >= collection.Count())
                throw new ArgumentOutOfRangeException();

            return collection.Take(position).Last();
        }

        public static T Second<T>(this IEnumerable<T> collection) => collection.At(EInteger.Number.One);
        public static T Third<T>(this IEnumerable<T> collection) => collection.At(EInteger.Number.Two);

        #endregion At()

        /// <summary> Returns a dictionary produced from the given collection of key-value pairs. </summary>
        /// <typeparam name="T"> The type of keys. </typeparam>
        /// <typeparam name="U"> The type of values. </typeparam>
        /// <param name="collection"> The collection to extract a dictionary from. </param>
        public static IDictionary<T, U> ToDictionary<T, U>(this IEnumerable<KeyValuePair<T, U>> collection) =>
            collection.ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value);

        #region Any(), IsEmpty(), HasSingle(), HasMany()

        public static bool Any(this IEnumerable collection)
        {
            foreach (var _ in collection)
            {
                return true;
            }
            return false;
        }

        public static bool IsEmpty(this IEnumerable collection) =>
            !collection.Any();

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
        #region Exclusion and Inclusion

        /// <summary> Returns a new enumeration excluding the given <paramref name="element"/>. </summary>
        /// <typeparam name="T"> The type of collection items. </typeparam>
        /// <param name="collection"> The collection to exclude <paramref name="element"/> from. </param>
        /// <param name="element"> The element to exclude from the <paramref name="collection"/>. </param>
        /// <returns></returns>
        public static IEnumerable<T> Except<T>(this IEnumerable<T> collection, T element) =>
            collection.Except(new List<T> { element });

        /// <summary> Returns a new collection with the given <paramref name="item"/> added to the <paramref name="collection"/>. </summary>
        /// <typeparam name="T"> The type of collection items. </typeparam>
        /// <param name="collection"> The collection to add the <paramref name="item"/> to. </param>
        /// <param name="item"> The item to add to the <paramref name="collection"/>. </param>
        public static IEnumerable<T> Including<T>(this IEnumerable<T> collection, T item) =>
            collection.Concat(new T[] { item });

        #endregion Exclusion and Inclusion
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
        #region Methods: Testing

        public static void ExecuteIfTestCountMatchesEnumerationSize<T>(this IEnumerable<Action> tests, string cancellationMessage) where T : struct
        {
            var type = typeof(T);

            if (!type.IsEnum)
                throw new ArgumentException(ECoreLogMessage.TypeIsNotEnumeration.FormatFluently(type.Name));

            if (type.GetEnumerationItems<T>().Count() != tests.Count())
                throw new NotImplementedException(cancellationMessage);

            foreach (var executeTest in tests)
                executeTest();
        }

        #endregion Methods: Testing
    }
}