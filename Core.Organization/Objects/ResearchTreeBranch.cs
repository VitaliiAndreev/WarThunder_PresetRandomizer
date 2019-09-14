using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.Enumerations;
using System.Collections.Generic;
using System.Linq;

namespace Core.Organization.Objects
{
    public class ResearchTreeBranch : Dictionary<ERank, ResearchTreeRank>
    {
        #region Properties

        /// <summary> The amount of columns in the branch. </summary>
        public int ColumnCount { get; private set; }

        /// <summary> The amount of rows in the branch. </summary>
        public int RowCount { get; private set; }

        /// <summary> Numbers of columns reserved for premium / gift vehicles. </summary>
        public IEnumerable<int> PremiumColumnNumbers { get; private set; }

        #endregion Properties

        /// <summary> Initializes the <see cref="ResearchTreeRank.StartingRowNumber"/> for the given reasearch tree rank. </summary>
        /// <param name="rankKey"> The enumeration item for the current rank. </param>
        /// <param name="rank"> The research tree rank to initialize the <see cref="ResearchTreeRank.StartingRowNumber"/> of. </param>
        private void InitializeStartingRankNumber(ERank rankKey, ResearchTreeRank rank)
        {
            var previousRankKey = rankKey.GetPreviousRank();

            if (previousRankKey == ERank.None || !Keys.Contains(previousRankKey))
            {
                rank.StartingRowNumber = EInteger.Number.One;
                return;
            }

            var previousRank = this[previousRankKey];
            rank.StartingRowNumber = previousRank.StartingRowNumber + previousRank.MaximumRowNumber;
        }

        /// <summary> Calculates <see cref="ColumnCount"/>, <see cref="RowCount"/>, and <see cref="PremiumColumnNumbers"/>. </summary>
        public void InitializeProperties()
        {
            foreach (var rankKey in Keys)
            {
                var rank = this[rankKey];

                rank.InitializeProperties();
                InitializeStartingRankNumber(rankKey, rank);
            }

            ColumnCount = Values.Max(rank => rank.MaximumColumnNumber);
            RowCount = Values.Sum(rank => rank.MaximumRowNumber);

            PremiumColumnNumbers = Values
                .SelectMany(rank => rank.PremiumColumnNumbers)
                .Distinct()
            ;
        }
    }
}