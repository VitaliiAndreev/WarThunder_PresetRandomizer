using Core.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

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
        #region Tests: GetEnumerationItems()

        [TestMethod]
        public void GetEnumerationItems_GenericIsNotEnum_Throws()
        {
            // arrange, act
            Action getEnumerationItems = () => typeof(Direction).GetEnumerationItems<int>();

            // assert
            getEnumerationItems.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void GetEnumerationItems_ParameterIsNotEnum_Throws()
        {
            // arrange, act
            Action getEnumerationItems = () => typeof(int).GetEnumerationItems<Direction>();

            // assert
            getEnumerationItems.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void GetEnumerationItems_EnumsMismatch_Throws()
        {
            // arrange, act
            Action getEnumerationItems = () => typeof(Direction).GetEnumerationItems<InitializationStatus>();

            // assert
            getEnumerationItems.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void GetEnumerationItems_SkipNone_ReturnsItemsWithoutNone()
        {
            // arrange
            var type = typeof(Direction);
            var expectedEnumerationItems = Enum.GetValues(type).Cast<Direction>().Except(Direction.None);
            
            // act
            var actualEnumerationItems = type.GetEnumerationItems<Direction>(true);

            // assert
            actualEnumerationItems.Should().BeEquivalentTo(expectedEnumerationItems);
        }

        [TestMethod]
        public void GetEnumerationItems_ReturnsAllItems()
        {
            // arrange
            var type = typeof(Direction);
            var expectedEnumerationItems = Enum.GetValues(type).Cast<Direction>();

            // act
            var actualEnumerationItems = type.GetEnumerationItems<Direction>();

            // assert
            actualEnumerationItems.Should().BeEquivalentTo(expectedEnumerationItems);
        }

        #endregion Tests: GetEnumerationItems()
    }
}
