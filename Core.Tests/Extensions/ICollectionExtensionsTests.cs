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
        #region Tests: RemoveRange()

        [TestMethod]
        public void RemoveRange_StructureElements()
        {
            // arrange
            var sourceCollection = new List<int> { 1, 2, 3, 4 };
            var elementsToRemove = new List<int> { 1, 4, 5 };

            // act
            sourceCollection.RemoveRange(elementsToRemove);

            // assert
            sourceCollection.Should().BeEquivalentTo(new List<int> { 2, 3 });
        }

        [TestMethod]
        public void RemoveRange_ClassElements()
        {
            // arrange
            var sourceCollection = new List<string> { "A", "B", "C", "D" };
            var elementsToRemove = new List<string> { "A", "D", "E" };

            // act
            sourceCollection.RemoveRange(elementsToRemove);

            // assert
            sourceCollection.Should().BeEquivalentTo(new List<string> { "B", "C" });
        }

        #endregion Tests: RemoveRange()
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