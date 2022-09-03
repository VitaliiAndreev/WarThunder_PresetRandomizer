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
        public void EnumerationItemValueIsPositive_TestEnum_Negative1_ReturnsFalse()
        {
            // arrange
            var enumerationitem = TestEnumerationOfIntegers.NegativeOne;

            // act
            var isPositive = enumerationitem.ValueIsPositive();

            // assert
            isPositive.Should().BeFalse();
        }

        [TestMethod]
        public void ValueIsPositive_TestEnum_0_ReturnsFalse()
        {
            // arrange
            var enumerationitem = TestEnumerationOfIntegers.Zero;

            // act
            var isPositive = enumerationitem.ValueIsPositive();

            // assert
            isPositive.Should().BeFalse();
        }

        [TestMethod]
        public void ValueIsPositive_TestEnum_1_ReturnsTrue()
        {
            // arrange
            var enumerationitem = TestEnumerationOfIntegers.PositiveOne;

            // act
            var isPositive = enumerationitem.ValueIsPositive();

            // assert
            isPositive.Should().BeTrue();
        }

        [TestMethod]
        public void ValueIsPositive_NotEnumeration_ThrowsArgumentException()
        {
            // arrange
            var value = 17;

            // act
            Action checkValue = () => value.ValueIsPositive();

            // assert
            checkValue.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void ValueIsPositive_UnderlyingTypeNotSupported_ThrowsNotImplementedException()
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