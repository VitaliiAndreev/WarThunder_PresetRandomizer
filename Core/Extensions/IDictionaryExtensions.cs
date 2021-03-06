﻿using Core.Enumerations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="IDictionary{TKey, TValue}"/> interface. </summary>
    public static class IDictionaryExtensions
    {
        #region Methods: Adding

        /// <summary> Add the given key-value pair into the <paramref name="dictionary"/>. </summary>
        /// <typeparam name="T"> The type of dictionary keys. </typeparam>
        /// <typeparam name="U"> The type of dictionary values. </typeparam>
        /// <param name="dictionary"> The dictionary to add the <paramref name="keyValuePair"/> into. </param>
        /// <param name="keyValuePair"> The key-value pair to add into the <paramref name="dictionary"/>. </param>
        public static void Add<T, U>(this IDictionary<T, U> dictionary, KeyValuePair<T, U> keyValuePair) =>
            dictionary.Add(keyValuePair);

        /// <summary> Adds contents of the specified dictionary into this one. </summary>
        /// <typeparam name="T"> The type of dictionary keys. </typeparam>
        /// <typeparam name="U"> The type of dictionary items. </typeparam>
        /// <param name="recipientDictionary"> The dictionary to add <paramref name="donorDictionary"/> into. </param>
        /// <param name="donorDictionary"> The dictionary whose contents to add into the <paramref name="recipientDictionary"/>. </param>
        public static void AddRange<T, U>(this IDictionary<T, U> recipientDictionary, IDictionary<T, U> donorDictionary)
        {
            foreach (var keyValuePair in donorDictionary)
                recipientDictionary.Add(keyValuePair);
        }

        /// <summary> Safely adds the specified value under its key into the dictionary. </summary>
        /// <typeparam name="T"> The type of dictionary keys. </typeparam>
        /// <typeparam name="U"> The type of dictionary items. </typeparam>
        /// <param name="dictionary"> The dictionary to add the <paramref name="key"/> and the <paramref name="value"/> into. </param>
        /// <param name="key"> The key under which to add into the <paramref name="dictionary"/>. </param>
        /// <param name="value"> The value to add into the <paramref name="dictionary"/>. </param>
        private static void AddSafely<T, U>(this IDictionary<T, U> dictionary, T key, U value)
        {
            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary.Add(key, value);
        }

        /// <summary> Tries to add the specified value under its key into the dictionary. </summary>
        /// <typeparam name="T"> The type of dictionary keys. </typeparam>
        /// <typeparam name="U"> The type of dictionary items. </typeparam>
        /// <param name="dictionary"> The dictionary to add the <paramref name="key"/> and the <paramref name="value"/> into. </param>
        /// <param name="key"> The key under which to add into the <paramref name="dictionary"/>. </param>
        /// <param name="value"> The value to add into the <paramref name="dictionary"/>. </param>
        public static void AddSafely<T, U>(this IDictionary dictionary, T key, U value)
        {
            if (dictionary is IDictionary<T, U> genericDictionary)
            {
                genericDictionary.AddSafely(key, value);
            }
            else
            {
                if (dictionary.Contains(key))
                    dictionary[key] = value;
                else
                    dictionary.Add(key, value);
            }
        }

        /// <summary> Appends the collection indexed by a <paramref name="key"/> in the <paramref name="dictionary"/> with the given <paramref name="item"/>. </summary>
        /// <typeparam name="T"> The type of dictionary keys. </typeparam>
        /// <typeparam name="U"> The type of dictionary value collection items. </typeparam>
        /// <param name="dictionary"> The dictionary a collection in which to append. </param>
        /// <param name="key"> The key by which to look in the <paramref name="dictionary"/>. </param>
        /// <param name="item"> The item to append a collection in the <paramref name="dictionary"/> with. </param>
        /// <param name="unique"> Whether to append a collection in the <paramref name="dictionary"/> only with unique items. </param>
        public static void Append<T, U>(this IDictionary<T, IEnumerable<U>> dictionary, T key, U item, bool unique = false)
        {
            var collection = dictionary.GetWithEnumerableInstantiation(key);

            if (!unique || !collection.Contains(item))
                dictionary[key] = collection.Including(item);
        }

        /// <summary> Gets an item with the specified key. A new item is instantiated if none are bound to the key. As such, the item type (<typeparamref name="U"/>) has to be a reference type with a parameterless constructor. </summary>
        /// <typeparam name="T"> The type of dictionary keys. </typeparam>
        /// <typeparam name="U"> The type of dictionary items. </typeparam>
        /// <param name="dictionary"> The dictionary to read by <paramref name="key"/> from. </param>
        /// <param name="key"> The key by which to look in the <paramref name="dictionary"/>. </param>
        /// <returns></returns>
        public static U GetWithInstantiation<T, U>(this IDictionary<T, U> dictionary, T key) where U : class
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            else
            {
                dictionary.Add(key, typeof(U).CreateInstance<U>());
                return dictionary[key];
            }
        }

        /// <summary> Gets an item with the specified key. A new item is instantiated if none are bound to the key. As such, the item type (<typeparamref name="U"/>) has to be a reference type with a parameterless constructor. </summary>
        /// <typeparam name="T"> The type of dictionary keys. </typeparam>
        /// <typeparam name="U"> The type of dictionary items. </typeparam>
        /// <param name="dictionary"> The dictionary to read by <paramref name="key"/> from. </param>
        /// <param name="key"> The key by which to look in the <paramref name="dictionary"/>. </param>
        /// <returns></returns>
        public static IEnumerable<U> GetWithEnumerableInstantiation<T, U>(this IDictionary<T, IEnumerable<U>> dictionary, T key)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            else
            {
                dictionary.Add(key, new List<U>());
                return dictionary[key];
            }
        }

        /// <summary> Increments the value assigned to the specified <paramref name="key"/>. </summary>
        /// <typeparam name="T"> The type of dictionary keys. </typeparam>
        /// <param name="dictionary"> The dictionary to increment a value of. </param>
        /// <param name="key"> The key whose value to increment in the <paramref name="dictionary"/>. </param>
        /// <param name="incrementValue"> The value by which to inrement. </param>
        public static void Increment<T>(this IDictionary<T, int> dictionary, T key, int incrementValue = 1)
        {
            if (dictionary.ContainsKey(key))
                dictionary[key] += incrementValue;
            else
                dictionary.Add(key, EInteger.Number.One);
        }

        #endregion Methods: Adding

        /// <summary> Creates a shallow copy, i.e. a new dictionary with same contents. </summary>
        /// <typeparam name="T"> The type of dictionary keys. </typeparam>
        /// <typeparam name="U"> The type of dictionary values. </typeparam>
        /// <param name="dictionary"> The dictionary to copy. </param>
        /// <returns></returns>
        public static IDictionary<T, U> Copy<T, U>(this IDictionary<T, U> dictionary) =>
            new Dictionary<T, U>(dictionary);

        public static IEnumerable<T> GetKeyWhereValue<T, U>(this IDictionary<T, U> dictionary, Predicate<U> valuePredicate) =>
            dictionary
                .Where(keyValuePair => valuePredicate(keyValuePair.Value))
                .Select(keyValuePair => keyValuePair.Key)
            ;
    }
}