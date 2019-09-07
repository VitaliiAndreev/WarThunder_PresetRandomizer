using Core.DataBase.WarThunder.Enumerations;
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

        #endregion Properties

        /// <summary> Calculates <see cref="ColumnCount"/> and <see cref="RowCount"/>. </summary>
        public void CalculateDimensions()
        {
            foreach (var rank in Values)
                rank.CalculateDimensions();

            ColumnCount = Values.Max(rank => rank.MaximumColumnNumber);
            RowCount = Values.Sum(rank => rank.MaximumRowNumber);
        }
    }
}