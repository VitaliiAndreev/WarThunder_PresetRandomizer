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

        /// <summary> Numbers of columns reserved for premium / gift vehicles. </summary>
        public IEnumerable<int> PremiumColumnNumbers { get; private set; }

        #endregion Properties

        /// <summary> Calculates <see cref="ColumnCount"/>, <see cref="RowCount"/>, and <see cref="PremiumColumnNumbers"/>. </summary>
        public void InitializeProperties()
        {
            foreach (var rank in Values)
                rank.InitializeProperties();

            ColumnCount = Values.Max(rank => rank.MaximumColumnNumber);
            RowCount = Values.Sum(rank => rank.MaximumRowNumber);

            PremiumColumnNumbers = Values
                .SelectMany(rank => rank.PremiumColumnNumbers)
                .Distinct()
            ;
        }
    }
}