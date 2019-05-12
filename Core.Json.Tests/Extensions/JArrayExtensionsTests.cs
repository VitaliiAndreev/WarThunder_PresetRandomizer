using Core.Json.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Core.Json.Tests.Extensions
{
    /// <summary> See <see cref="JArrayExtensions"/>. </summary>
    [TestClass]
    public class JArrayExtensionsTests
    {
        #region Tests: AddIndividually()

        [TestMethod]
        public void AddNonIndividually()
        {
            // arrange
            var targetJsonArray = new JArray(1, 2, 3);
            var sourceJsonArray = new JArray(4, 5, 6);
            var initialCount = sourceJsonArray.Count();

            // act
            targetJsonArray.Add(sourceJsonArray);

            // assert
            targetJsonArray.Count().Should().Be(initialCount + 1);
            targetJsonArray.Last().Should().BeEquivalentTo(sourceJsonArray);
        }

        [TestMethod]
        public void AddIndividually()
        {
            // arrange
            var targetJsonArray = new JArray(1, 2, 3);
            var sourceJsonArray = new JArray(4, 5, 6);

            // act
            targetJsonArray.AddIndividually(sourceJsonArray);

            // assert
            targetJsonArray.Should().BeEquivalentTo(new JArray(1, 2, 3, 4, 5, 6));
        }

        #endregion Tests: AddIndividually()
    }
}
