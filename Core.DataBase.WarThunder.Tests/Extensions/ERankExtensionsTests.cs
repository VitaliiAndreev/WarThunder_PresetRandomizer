using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Tests.Extensions
{
    /// <summary> See <see cref="ERankExtensions"/>. </summary>
    [TestClass]
    public class ERankExtensionsTests
    {
        #region Methods: private

        private void DoTests(IEnumerable<Action> tests)
        {
            tests.ExecuteIfTestCountMatchesEnumerationSize<ERank>("Add newly added ranks to unit tests.");
        }

        #endregion Methods: private
        #region Tests: GetPreviousRank()

        [TestMethod]
        public void GetPreviousRank()
        {
            var tests = new List<Action>
            {
                () => ERank.None.GetPreviousRank().Should().Be(ERank.None),
                () => ERank.All.GetPreviousRank().Should().Be(ERank.None),
                () => ERank.I.GetPreviousRank().Should().Be(ERank.None),
                () => ERank.II.GetPreviousRank().Should().Be(ERank.I),
                () => ERank.III.GetPreviousRank().Should().Be(ERank.II),
                () => ERank.IV.GetPreviousRank().Should().Be(ERank.III),
                () => ERank.V.GetPreviousRank().Should().Be(ERank.IV),
                () => ERank.VI.GetPreviousRank().Should().Be(ERank.V),
                () => ERank.VII.GetPreviousRank().Should().Be(ERank.VI),
            };

            DoTests(tests);
        }

        [TestMethod]
        public void GetPreviousRank_GapInAvailableRanks_ReturnsClosestAvailable()
        {
            // arrange
            var availableRanks = new List<ERank> { ERank.I, ERank.II, ERank.V, };
            var currentRank = ERank.V;
            var expectedPreviousRank = ERank.II;

            // act
            var previousRank = currentRank.GetPreviousRank(availableRanks);

            // assert
            previousRank.Should().Be(expectedPreviousRank);
        }

        #endregion Tests: GetPreviousRank()
        #region Tests: IsValid()

        [TestMethod]
        public void IsValid()
        {
            var tests = new List<Action>
            {
                () => ERank.None.IsValid().Should().BeFalse(),
                () => ERank.All.IsValid().Should().BeFalse(),
                () => ERank.I.IsValid().Should().BeTrue(),
                () => ERank.II.IsValid().Should().BeTrue(),
                () => ERank.III.IsValid().Should().BeTrue(),
                () => ERank.IV.IsValid().Should().BeTrue(),
                () => ERank.V.IsValid().Should().BeTrue(),
                () => ERank.VI.IsValid().Should().BeTrue(),
                () => ERank.VII.IsValid().Should().BeTrue(),
            };

            DoTests(tests);
        }

        #endregion Tests: IsValid()
    }
}