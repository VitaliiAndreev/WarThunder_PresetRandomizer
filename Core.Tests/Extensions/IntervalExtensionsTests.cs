using Core.Objects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Core.Tests.Extensions
{
    /// <summary> See <see cref="IntervalExtensions"/>. </summary>
    [TestClass]
    public class IntervalExtensionsTests
    {
        #region Tests: AsEnumerable()

        [TestMethod]
        public void AsEnumerable_NotBoundedTooShort_ReturnsNone()
        {
            // arrange
            var interval = new Interval<int>(false, 0, 1, false);

            // act
            var actualIntegers = interval.AsEnumerable();

            // assert
            actualIntegers.Should().BeEmpty();
        }

        [TestMethod]
        public void AsEnumerable_NotBoundedShort_ReturnsOneInteger()
        {
            // arrange
            var interval = new Interval<int>(false, 0, 2, false);
            var expectedIntegers = new List<int> { 1 };

            // act
            var actualIntegers = interval.AsEnumerable();

            // assert
            actualIntegers.Should().BeEquivalentTo(expectedIntegers);
        }

        [TestMethod]
        public void AsEnumerable_NotBounded_ReturnsClippedIntegers()
        {
            // arrange
            var interval = new Interval<int>(false, 0, 3, false);
            var expectedIntegers = new List<int> { 1, 2 };

            // act
            var actualIntegers = interval.AsEnumerable();

            // assert
            actualIntegers.Should().BeEquivalentTo(expectedIntegers);
        }

        [TestMethod]
        public void AsEnumerable_BoundedShort_ReturnsOneInteger()
        {
            // arrange
            var interval = new Interval<int>(true, 0, 0, true);
            var expectedIntegers = new List<int> { 0 };

            // act
            var actualIntegers = interval.AsEnumerable();

            // assert
            actualIntegers.Should().BeEquivalentTo(expectedIntegers);
        }

        [TestMethod]
        public void AsEnumerable_Bounded_ReturnsAllIntegers()
        {
            // arrange
            var interval = new Interval<int>(true, 0, 3, true);
            var expectedIntegers = new List<int> { 0, 1, 2, 3 };

            // act
            var actualIntegers = interval.AsEnumerable();

            // assert
            actualIntegers.Should().BeEquivalentTo(expectedIntegers);
        }

        #endregion Tests: AsEnumerable()
    }
}
