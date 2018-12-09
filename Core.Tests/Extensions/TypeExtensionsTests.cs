using Core.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Core.Tests.Extensions
{
    /// <summary> See <see cref="ObjectExtensions"/>. </summary>
    [TestClass]
    public class TypeExtensionsTests
    {
        #region Tests: ToStringLikeCode()

        [TestMethod]
        public void ToStringLikeCode_NotGeneric_ReturnsName()
        {
            // arrange
            var type = typeof(string);

            // act
            var typeString = type.ToStringLikeCode();

            // assert
            typeString.Should().Be("String");
        }

        [TestMethod]
        public void ToStringLikeCode_Generic_One_ReturnsNameAndTypeArguments()
        {
            // arrange
            var type = typeof(List<string>);

            // act
            var typeString = type.ToStringLikeCode();

            // assert
            typeString.Should().Be("List<String>");
        }

        [TestMethod]
        public void ToStringLikeCode_Generic_Two_ReturnsNameAndTypeArguments()
        {
            // arrange
            var type = typeof(Dictionary<int, string>);

            // act
            var typeString = type.ToStringLikeCode();

            // assert
            typeString.Should().Be("Dictionary<Int32, String>");
        }

        [TestMethod]
        public void ToStringLikeCode_Generic_Composite_ReturnsNameAndTypeArguments()
        {
            // arrange
            var type = typeof(Dictionary<int, List<string>>);

            // act
            var typeString = type.ToStringLikeCode();

            // assert
            typeString.Should().Be("Dictionary<Int32, List<String>>");
        }

        #endregion Tests: GetTypeString()
    }
}
