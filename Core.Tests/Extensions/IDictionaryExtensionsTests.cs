using Core.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Core.Tests.Extensions
{
    /// <summary> See <see cref="IDictionaryExtensions"/>. </summary>
    [TestClass]
    public class IDictionaryExtensionsTests
    {
        #region Tests: AddSafely()

        [TestMethod]
        public void AddSafely_KeyAbsent_AddsKeyAndValue()
        {
            // arrange
            var actualDictionary = new Dictionary<int, int>();
            var expectedDictionary = new Dictionary<int, int> { { 1, 1 } };

            // act
            actualDictionary.AddSafely(1, 1);

            // assert
            actualDictionary.Should().BeEquivalentTo(expectedDictionary);
        }

        [TestMethod]
        public void AddSafely_KeyPresent_ReplacesValueOfKey()
        {
            // arrange
            var actualDictionary = new Dictionary<int, int> { { 1, 1 } };
            var expectedDictionary = new Dictionary<int, int> { { 1, 2 } };

            // act
            actualDictionary.AddSafely(1, 2);

            // assert
            actualDictionary.Should().BeEquivalentTo(expectedDictionary);
        }

        #endregion Tests: AddSafely()
    }
}