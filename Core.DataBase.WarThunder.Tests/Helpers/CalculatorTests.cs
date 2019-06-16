using Core.DataBase.WarThunder.Helpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Tests.Helpers
{
    /// <summary> See <see cref="Calculator"/>. </summary>
    [TestClass]
    public class CalculatorTests
    {
        #region [Private]

        private class EconomicRankBattleRatingPair
        {
            public int EconomicRank { get; }
            public decimal BattleRating { get; }

            public EconomicRankBattleRatingPair(int economicRank, decimal battleRating)
            {
                EconomicRank = economicRank;
                BattleRating = battleRating;
            }
        }

        private readonly IList<EconomicRankBattleRatingPair> referenceTable = new List<EconomicRankBattleRatingPair>
        {
            new EconomicRankBattleRatingPair(00, 01.0m),
            new EconomicRankBattleRatingPair(01, 01.3m),
            new EconomicRankBattleRatingPair(02, 01.7m),
            new EconomicRankBattleRatingPair(03, 02.0m),
            new EconomicRankBattleRatingPair(04, 02.3m),
            new EconomicRankBattleRatingPair(05, 02.7m),
            new EconomicRankBattleRatingPair(06, 03.0m),
            new EconomicRankBattleRatingPair(07, 03.3m),
            new EconomicRankBattleRatingPair(08, 03.7m),
            new EconomicRankBattleRatingPair(09, 04.0m),
            new EconomicRankBattleRatingPair(10, 04.3m),
            new EconomicRankBattleRatingPair(11, 04.7m),
            new EconomicRankBattleRatingPair(12, 05.0m),
            new EconomicRankBattleRatingPair(13, 05.3m),
            new EconomicRankBattleRatingPair(14, 05.7m),
            new EconomicRankBattleRatingPair(15, 06.0m),
            new EconomicRankBattleRatingPair(16, 06.3m),
            new EconomicRankBattleRatingPair(17, 06.7m),
            new EconomicRankBattleRatingPair(18, 07.0m),
            new EconomicRankBattleRatingPair(19, 07.3m),
            new EconomicRankBattleRatingPair(20, 07.7m),
            new EconomicRankBattleRatingPair(21, 08.0m),
            new EconomicRankBattleRatingPair(22, 08.3m),
            new EconomicRankBattleRatingPair(23, 08.7m),
            new EconomicRankBattleRatingPair(24, 09.0m),
            new EconomicRankBattleRatingPair(25, 09.3m),
            new EconomicRankBattleRatingPair(26, 09.7m),
            new EconomicRankBattleRatingPair(27, 10.0m),
        };

        #endregion [Private]
        #region Tests: GetBattleRating()

        [TestMethod]
        public void GetBattleRating()
        {
            // arrange
            foreach (var referencePair in referenceTable)
            {
                // act
                var battleRating = Calculator.GetBattleRating(referencePair.EconomicRank);

                // assert
                battleRating.Should().Be(referencePair.BattleRating);
            }
        }

        #endregion Tests: GetBattleRating()
        #region Tests: GetEconomicRank()

        [TestMethod]
        public void GetEconomicRank()
        {
            // arrange
            foreach (var referencePair in referenceTable)
            {
                // act
                var economicRank = Calculator.GetEconomicRank(referencePair.BattleRating);

                // assert
                economicRank.Should().Be(referencePair.EconomicRank);
            }
        }

        #endregion Tests: GetEconomicRank()
    }
}