using Core.Randomization.Helpers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Core.Randomization.Extensions
{
    /// <summary> Methods extending the <see cref="IEnumerable{T}"/> class. </summary>
    public static class IEnumerableExtensions
    {
        /// <summary> Randomizes contents of the collection using the given instance of a randomizer. </summary>
        /// <typeparam name="T"> The type of collection elements. </typeparam>
        /// <param name="collection"> The collection to randomize. </param>
        /// <param name="randomizer"> The instance of a randomizer to randomize the collection with. </param>
        /// <returns></returns>
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> collection, IRandomizer randomizer)
        {
            var sourceList = collection.ToList();
            var randomizedList = new List<T>();

            while (randomizedList.Count() < collection.Count())
            {
                var selection = randomizer.GetRandom(sourceList);

                randomizedList.Add(selection);
                sourceList.Remove(selection);
            }

            return randomizedList;
        }
    }
}