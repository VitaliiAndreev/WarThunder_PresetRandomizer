using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.Enumerations;
using Core.Extensions;
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
                rank.MaximumRowNumber = rank.RowCount;
                return;
            }

            var previousRank = this[previousRankKey];
            rank.StartingRowNumber = previousRank.StartingRowNumber + previousRank.MaximumRowNumber;
            rank.MaximumRowNumber = previousRank.MaximumRowNumber + rank.RowCount;
        }

        /// <summary> Calculates <see cref="ColumnCount"/>, <see cref="RowCount"/>, and <see cref="PremiumColumnNumbers"/>. </summary>
        /// <param name="columnCount"> The amount of columns in the branch. </param>
        public void InitializeProperties(int columnCount)
        {
            foreach (var rankKey in Keys)
            {
                var rank = this[rankKey];

                rank.InitializeProperties();
                InitializeStartingRankNumber(rankKey, rank);
            }

            ColumnCount = columnCount;
            RowCount = Values.Sum(rank => rank.RowCount);

            PremiumColumnNumbers = Values
                .SelectMany(rank => rank.PremiumColumnNumbers)
                .Distinct()
            ;
        }
    }
}