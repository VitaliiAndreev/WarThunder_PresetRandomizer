using System.Collections;
using System.Collections.Generic;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="IDictionary{TKey, TValue}"/> interface. </summary>
    public static class IDictionaryExtensions
    {
        #region Methods: Adding

        /// <summary> Adds contents of the specified dictionary into this one. </summary>
        /// <typeparam name="T"> The type of dictionary keys. </typeparam>
        /// <typeparam name="U"> The type of dictionary items. </typeparam>
        /// <param name="sourceDictionary"> The source dictionary. </param>
        /// <param name="donorDictionary"> The dictionary whose contents to add into this one. </param>
        public static void AddRange<T, U>(this IDictionary<T, U> sourceDictionary, IDictionary<T, U> donorDictionary)
        {
            foreach (var keyValuePair in donorDictionary)
                sourceDictionary.Add(keyValuePair);
        }

        /// <summary> Safely adds the specified value under its key into the dictionary. </summary>
        /// <typeparam name="T"> The type of dictionary keys. </typeparam>
        /// <typeparam name="U"> The type of dictionary items. </typeparam>
        /// <param name="sourceDictionary"> The source dictionary. </param>
        /// <param name="key"> The key under which to add. </param>
        /// <param name="value"> The value to add. </param>
        private static void AddSafely<T, U>(this IDictionary<T, U> sourceDictionary, T key, U value)
        {
            if (sourceDictionary.ContainsKey(key))
                sourceDictionary[key] = value;
            else
                sourceDictionary.Add(key, value);
        }

        /// <summary> Tries to add the specified value under its key into the dictionary. </summary>
        /// <typeparam name="T"> The type of dictionary keys. </typeparam>
        /// <typeparam name="U"> The type of dictionary items. </typeparam>
        /// <param name="sourceDictionary"> The source dictionary. </param>
        /// <param name="key"> The key under which to add. </param>
        /// <param name="value"> The value to add. </param>
        public static void AddSafely<T, U>(this IDictionary sourceDictionary, T key, U value)
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

        /// <summary> Gets an item with the specified key. A new item is instantiated if none are bound to the key. As such, the item type (<typeparamref name="U"/>) has to be a reference type with a parameterless constructor. </summary>
        /// <typeparam name="T"> The type of dictionary keys. </typeparam>
        /// <typeparam name="U"> The type of dictionary items. </typeparam>
        /// <param name="sourceDictionary"> The source dictionary. </param>
        /// <param name="key"> The key by which to look. </param>
        /// <returns></returns>
        public static U GetWithInstantiation<T, U>(this IDictionary<T, U> sourceDictionary, T key) where U : class
        {
            if (sourceDictionary.ContainsKey(key))
                return sourceDictionary[key];

            else
            {
                sourceDictionary.Add(key, typeof(U).CreateInstance<U>());
                return sourceDictionary[key];
            }
        }

        /// <summary> Increments the value assigned to the specified key. </summary>
        /// <typeparam name="T"> The type of dictionary keys. </typeparam>
        /// <param name="sourceDictionary"> The source dictionary. </param>
        /// <param name="keyWhoseValueToIncrement"> The key whose value to increment. </param>
        public static void Increment<T>(this IDictionary<T, int> sourceDictionary, T keyWhoseValueToIncrement)
        {
            if (sourceDictionary.ContainsKey(keyWhoseValueToIncrement))
                sourceDictionary[keyWhoseValueToIncrement]++;
            else
                sourceDictionary.Add(keyWhoseValueToIncrement, 1);
        }

        #endregion Methods: Adding
    }
}