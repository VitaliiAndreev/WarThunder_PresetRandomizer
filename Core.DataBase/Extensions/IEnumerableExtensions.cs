using Core.DataBase.Objects.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataBase.Extensions
{
    /// <summary> Methods extending the <see cref="IEnumerable{T}"/> class. </summary>
    public static class IEnumerableExtensions
    {
        #region Methods: Equivalence

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