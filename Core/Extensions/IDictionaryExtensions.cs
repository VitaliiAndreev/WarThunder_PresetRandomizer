using System.Collections.Generic;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="IDictionary{TKey, TValue}"/> interface. </summary>
    public static class IDictionaryExtensions
    {
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

        /// <summary> Merges two dictionaries in a way that integer values corresponding to keys are added together in case of duplicates. </summary>
        /// <typeparam name="T"> A generic type. </typeparam>
        /// <param name="sourceDictionary"> A source dictionary. </param>
        /// <param name="targetDictionary"> The dictionary to merge with. </param>
        /// <returns></returns>
        public static IDictionary<T, int> MergeIntegerValues<T>(this IDictionary<T, int> sourceDictionary, IDictionary<T, int> targetDictionary)
        {
            var newDictionary = sourceDictionary.Copy();

            foreach (var keyValuePair in targetDictionary)
            {
                if (sourceDictionary.ContainsKey(keyValuePair.Key))
                    sourceDictionary[keyValuePair.Key] += keyValuePair.Value;
                else
                    sourceDictionary.Add(keyValuePair);
            }
            return newDictionary;
        }
    }
}