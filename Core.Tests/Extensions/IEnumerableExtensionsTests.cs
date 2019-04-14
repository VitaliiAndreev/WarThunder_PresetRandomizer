using Core.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Core.Tests.Extensions
{
    /// <summary> See <see cref="IEnumerableExtensions"/>. </summary>
    [TestClass]
    public class IEnumerableExtensionsTests
    {
        #region Tests: IsEmpty<T>()

        [TestMethod]
        public void IsEmpty_Empty_ReturnsTrue()
        {
            // arrange, act, assert
            new List<bool>().IsEmpty().Should().BeTrue();
        }

        [TestMethod]
        public void IsEmpty_NotEmpty_ReturnsFalse()
        {
            // arrange, act, assert
            new bool[] { true }.IsEmpty().Should().BeFalse();
        }

        #endregion Tests: IsEmpty<T>()
        #region Tests: SkipLast<T>()

        [TestMethod]
        public void SkipLast_OneThroughFive_SkipLast_ReturnsOneThroughFour()
        {
            // arrange
            var originalCollection = new int[] { 1, 2, 3, 4, 5, };

            // act
            var trimmedCollection = originalCollection.SkipLast();

            // assert
            trimmedCollection.Should().BeEquivalentTo(new int[] { 1, 2, 3, 4, });
        }

        [TestMethod]
        public void SkipLast_OneThroughFive_SkipLast2_ReturnsOneThroughThree()
        {
            // arrange
            var originalCollection = new int[] { 1, 2, 3, 4, 5, };

            // act
            var trimmedCollection = originalCollection.SkipLast(2);

            // assert
            trimmedCollection.Should().BeEquivalentTo(new int[] { 1, 2, 3, });
        }

        #endregion Tests: SkipLast<T>()
        #region Tests: StringJoin<T>()

        [TestMethod]
        public void StringJoin_NoSeparator()
        {
            // arrange, act, assert
            new int[] { 1, 2, 3 }.StringJoin().Should().Be("123");
        }

        [TestMethod]
        public void StringJoin_CharacterSeparator()
        {
            // arrange, act, assert
            new int[] { 1, 2, 3 }.StringJoin('-').Should().Be("1-2-3");
        }

        [TestMethod]
        public void StringJoin_StringSeparator()
        {
            // arrange, act, assert
            new int[] { 1, 2, 3 }.StringJoin(" + ").Should().Be("1 + 2 + 3");
        }

        #endregion Tests: StringJoin<T>()
    }
}
