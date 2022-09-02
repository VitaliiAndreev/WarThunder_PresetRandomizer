using Core.DataBase.WarThunder.Enumerations;
using Core.Enumerations;
using Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataBase.WarThunder.Extensions
{
    /// <summary> Methods extending the <see cref="ERank"/> enumeration. </summary>
    public static class ERankExtensions
    {
        /// <summary> Checks whether the rank is valid. </summary>
        /// <param name="rank"> The rank to check. </param>
        /// <returns></returns>
        public static bool IsValid(this ERank rank) =>
            rank.EnumerationItemValueIsPositive();

        /// <summary> Returns the previous rank enumeration item. </summary>
        /// <param name="currentRank"> The current rank enumeration item. </param>
        /// <returns></returns>
        public static ERank GetPreviousRank(this ERank currentRank)
        {
            var invalidRanks = typeof(ERank).GetEnumerationItems<ERank>().Where(item => item == ERank.I || !item.IsValid());

            if (currentRank.IsIn(invalidRanks))
                return ERank.None;

            return currentRank - Integer.Number.One;
        }

        /// <summary> Returns the first available rank enumeration item prior to the <paramref name="currentRank"/>. </summary>
        /// <param name="currentRank"> The current rank enumeration item. </param>
        /// <returns></returns>
        public static ERank GetPreviousRank(this ERank currentRank, IEnumerable<ERank> availableRanks)
        {
            var previousRank = currentRank.GetPreviousRank();

            while(previousRank.IsValid())
            {
                if (previousRank.IsIn(availableRanks))
                    break;

                previousRank = previousRank.GetPreviousRank();
            }

            return previousRank;
        }
    }
}