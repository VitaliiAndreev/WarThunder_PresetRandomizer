using Core.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Tests.Extensions
{
    /// <summary> See <see cref="IEnumerableExtensions"/>. </summary>
    [TestClass]
    public class IEnumerableExtensionsTests
    {
        #region Tests: SkipLast<T>()

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
    }
}
