using Core.DataBase.Objects.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataBase.Extensions
{
    /// <summary> Methods extending the <see cref="IEnumerable{T}"/> class. </summary>
    public static class IEnumerableExtensions
    {
        #region Methods: Equivalence

        /// <summary> Checks whether the collection can be considered equivalent to the given one. </summary>
        /// <param name="sourceCollection"> The source collection. </param>
        /// <param name="comparedCollection"> The compared collection. </param>
        /// <param name="recursionLevel">
        /// The level of recursion up to which to compare nested objects. Use with CAUTION in case of cyclic links.
        /// <para>Set to zero to disable recursion. It also prevents the method from cheking <see cref="IPersistentObject"/> members and their <see cref="IEnumerable{T}"/> collections for equivalence.</para>
        /// <para>Set to one to check <see cref="IPersistentObject"/> members and their <see cref="IEnumerable{T}"/> collections for equivalence using the same recursion rules as here.</para>
        /// </param>
        /// <param name="ignoredPropertyNames"> Property names ignored during comparison. </param>
        /// <returns></returns>
        public static bool IsEquivalentTo(this IEnumerable<IPersistentObject> sourceCollection, IEnumerable<IPersistentObject> comparedCollection, int recursionLevel = 0, IEnumerable<string> ignoredPropertyNames = null)
        {
            if (sourceCollection.Count() == comparedCollection.Count())
            {
                return sourceCollection
                    .Zip(comparedCollection, (source, target) => source.IsEquivalentTo(target, recursionLevel - 1, ignoredPropertyNames))
                    .All(isEquivalent => isEquivalent)
                ;
            }
            return false;
        }

        #endregion Methods: Equivalence
    }
}