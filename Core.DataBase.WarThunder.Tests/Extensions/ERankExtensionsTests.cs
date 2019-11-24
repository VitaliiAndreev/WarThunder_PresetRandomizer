using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Tests.Extensions
{
    /// <summary> See <see cref="ERankExtensions"/>. </summary>
    [TestClass]
    public class ERankExtensionsTests
    {
        #region Tests: GetPreviousRank()

        [TestMethod]
        public void GetPreviousRank_None_None()
        {
            // arrange
            var currentRank = ERank.None;
            var expectedPreviousRank = ERank.None;

            // act
            var previousRank = currentRank.GetPreviousRank();

            // assert
            previousRank.Should().Be(expectedPreviousRank);
        }

        [TestMethod]
        public void GetPreviousRank_I_None()
        {
            // arrange
            var currentRank = ERank.I;
            var expectedPreviousRank = ERank.None;

            // act
            var previousRank = currentRank.GetPreviousRank();

            // assert
            previousRank.Should().Be(expectedPreviousRank);
        }

        [TestMethod]
        public void GetPreviousRank_II_I()
        {
            // arrange
            var currentRank = ERank.II;
            var expectedPreviousRank = ERank.I;

            // act
            var previousRank = currentRank.GetPreviousRank();

            // assert
            previousRank.Should().Be(expectedPreviousRank);
        }

        [TestMethod]
        public void GetPreviousRank_GapInAvailableRanks()
        {
            // arrange
            var availableRanks = new List<ERank> { ERank.II, ERank.V, };
            var currentRank = ERank.V;
            var expectedPreviousRank = ERank.II;

            // act
            var previousRank = currentRank.GetPreviousRank(availableRanks);

            // assert
            previousRank.Should().Be(expectedPreviousRank);
        }

        #endregion Tests: GetPreviousRank()
    }
}