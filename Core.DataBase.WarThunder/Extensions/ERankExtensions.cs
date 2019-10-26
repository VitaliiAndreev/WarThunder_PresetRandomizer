using Core.DataBase.WarThunder.Enumerations;
using Core.Enumerations;
using Core.Extensions;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Extensions
{
    /// <summary> Methods extending the <see cref="ERank"/> enumeration. </summary>
    public static class ERankExtensions
    {
        /// <summary> Returns the previous rank enumeration item. </summary>
        /// <param name="currentRank"> The current rank enumeration item. </param>
        /// <returns></returns>
        public static ERank GetPreviousRank(this ERank currentRank)
        {
            var invalidRanks = new List<ERank> { ERank.None, ERank.I };

            if (currentRank.IsIn(invalidRanks))
                return ERank.None;

            return currentRank - EInteger.Number.One;
        }

        /// <summary> Returns the first available rank enumeration item prior to the <paramref name="currentRank"/>. </summary>
        /// <param name="currentRank"> The current rank enumeration item. </param>
        /// <returns></returns>
        public static ERank GetPreviousRank(this ERank currentRank, IEnumerable<ERank> availableRanks)
        {
            var previousRank = currentRank.GetPreviousRank();

            while(previousRank != ERank.None)
            {
                if (previousRank.IsIn(availableRanks))
                    break;

                previousRank = previousRank.GetPreviousRank();
            }

            return previousRank;
        }
    }
}