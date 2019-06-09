using System.Collections;
using System.Collections.Generic;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="IDictionary{TKey, TValue}"/> interface. </summary>
    public static class IDictionaryExtensions
    {
        #region Methods: Adding

        /// <summary> Safely adds the specified value under its key into the dictionary. </summary>
        /// <typeparam name="T"> A generic type. </typeparam>
        /// <typeparam name="U"> A generic type. </typeparam>
        /// <param name="sourceDictionary"> A source dictionary. </param>
        /// <param name="key"> The key under which to add. </param>
        /// <param name="value"> The value to add. </param>
        public static void AddSafely<T, U>(this IDictionary<T, U> sourceDictionary, T key, U value)
        {
            if (sourceDictionary.ContainsKey(key))
                sourceDictionary[key] = value;
            else
                sourceDictionary.Add(key, value);
        }

        /// <summary> Tries to add the specified value under its key into the dictionary. </summary>
        /// <typeparam name="T"> A generic type. </typeparam>
        /// <typeparam name="U"> A generic type. </typeparam>
        /// <param name="sourceDictionary"> A source dictionary. </param>
        /// <param name="key"> The key under which to add. </param>
        /// <param name="value"> The value to add. </param>
        public static void TryAdding<T, U>(this IDictionary sourceDictionary, T key, U value)
        {
            if (sourceDictionary is IDictionary<T, U> genericDictionary)
                genericDictionary.AddSafely(key, value);

            else
            {
                if (sourceDictionary.Contains(key))
                    sourceDictionary[key] = value;
                else
                    sourceDictionary.Add(key, value);
            }
        }

        #endregion Methods: Adding

        /// <summary> Creates a copy of the dictionary, i.e. a new dictionary with same contents. </summary>
        /// <typeparam name="T"> A generic type. </typeparam>
        /// <typeparam name="U"> A generic type. </typeparam>
        /// <param name="sourceDictionary"> A source dictionary. </param>
        /// <returns></returns>
        public static IDictionary<T, U> Copy<T, U>(this IDictionary<T, U> sourceDictionary)
        {
            var newDictionary = new Dictionary<T, U>();

            foreach (var keyValuePair in sourceDictionary)
                newDictionary.Add(keyValuePair.Key, keyValuePair.Value);

            return newDictionary;
        }
    }
}