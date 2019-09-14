using Core.Organization.Objects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Core.Organization.Tests.Objects
{
    /// <summary> See <see cref="ResearchTreeCoordinatesWithinRank"/>. </summary>
    [TestClass]
    public class ResearchTreeCoordinatesWithinRankTests
    {
        #region Tests: GetHashCode()

        [TestMethod]
        public void GetHashCode_MustHaveNoCollisionsWithinReasonableLimits()
        {
            // arrange
            var coordinateSets = new List<ResearchTreeCoordinatesWithinRank>();

            for (var rowIndex = 0; rowIndex < 100; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < 10; columnIndex++)
                {
                    coordinateSets.Add(new ResearchTreeCoordinatesWithinRank(columnIndex, rowIndex, null));

                    for (var folderIndex = 0; folderIndex < 10; folderIndex++)
                        coordinateSets.Add(new ResearchTreeCoordinatesWithinRank(columnIndex, rowIndex, folderIndex));
                }
            }

            // act
            var hashCodes = coordinateSets.Select(set => set.GetHashCode());

            // assert
            hashCodes.Distinct().Count().Should().Be(hashCodes.Count());
        }

        #endregion Tests: GetHashCode()
    }
}
