using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Core.DataBase.WarThunder.Tests.Extensions
{
    /// <summary> See <see cref="EnumExtensions"/>. </summary>
    [TestClass]
    public class EnumExtensionsTests
    {
        [TestMethod]
        public void ValueIsPositive_ReturnsFalse_IfNegativeOne()
        {
            // arrange
            var enumerationitem = TestEnumerationOfIntegers.NegativeOne;

            // act
            var isPositive = enumerationitem.ValueIsPositive();

            // assert
            isPositive.Should().BeFalse();
        }

        [TestMethod]
        public void ValueIsPositive_ReturnsFalse_IfZero()
        {
            // arrange
            var enumerationitem = TestEnumerationOfIntegers.Zero;

            // act
            var isPositive = enumerationitem.ValueIsPositive();

            // assert
            isPositive.Should().BeFalse();
        }

        [TestMethod]
        public void ValueIsPositive_ReturnsTrue_IfPositiveOne()
        {
            // arrange
            var enumerationitem = TestEnumerationOfIntegers.PositiveOne;

            // act
            var isPositive = enumerationitem.ValueIsPositive();

            // assert
            isPositive.Should().BeTrue();
        }

        [TestMethod]
        public void ValueIsPositive_ThrowsArgumentException_IfNotEnumeration()
        {
            // arrange
            var value = 17;

            // act
            Action checkValue = () => value.ValueIsPositive();

            // assert
            checkValue.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void ValueIsPositive_ThrowsNotImplementedException_IfUnderlyingTypeNotSupported()
        {
            // arrange
            var value = TestEnumerationOfUnsignedIntegers.Item;

            // act
            Action checkValue = () => value.ValueIsPositive();

            // assert
            checkValue.Should().Throw<NotImplementedException>();
        }

        private enum TestEnumerationOfIntegers
        {
            NegativeOne = -1,
            Zero,
            PositiveOne,
        }

        private enum TestEnumerationOfUnsignedIntegers : uint
        {
            Item,
        }
    }
}