using Core.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Core.Tests.Extensions
{
    /// <summary> See <see cref="ICollectionExtensions"/>. </summary>
    [TestClass]
    public class ICollectionExtensionsTests
    {
        #region Tests: Copy()

        [TestMethod]
        public void Copy()
        {
            // arrange
            var sourceCollection = new List<int> { 1, 2, 3 };

            // act
            var copiedCollection = sourceCollection.Copy();

            // assert
            copiedCollection.Equals(sourceCollection).Should().BeFalse();
            copiedCollection.Should().BeEquivalentTo(sourceCollection);
        }

        #endregion Tests: Copy()
        #region Tests: ReplaceBy()

        [TestMethod]
        public void ReplaceBy()
        {
            // arrange
            var sourceCollection = new List<int> { 1, 2, 3 };
            var donorCollection = new List<int> { 4, 5, 6 };

            // act
            sourceCollection.ReplaceBy(donorCollection);

            // assert
            sourceCollection.Should().BeEquivalentTo(donorCollection);
        }

        #endregion Tests: Replace()
    }
}