using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Core.Tests.Extensions
{
    /// <summary> See <see cref="GenericExtensions"/>. </summary>
    [TestClass]
    public class GenericExtensionsTests
    {
        #region Tests: EnumerationItemValueIsPositive<T>()

        #region Test Classes

        private enum TestEnumerationOfIntegers
        {
            _1Neg = -1,
            _0,
            _1,
        }

        private enum TestEnumerationOfUnsignedIntegers : uint
        {
            Item,
        }

        #endregion Test Classes

        [TestMethod]
        public void EnumerationItemValueIsPositive_TestEnum_Negative1_ReturnsFalse()
        {
            // arrange
            var enumerationitem = TestEnumerationOfIntegers._1Neg;

            // act
            var isPositive = enumerationitem.EnumerationItemValueIsPositive();

            // assert
            isPositive.Should().BeFalse();
        }

        [TestMethod]
        public void EnumerationItemValueIsPositive_TestEnum_0_ReturnsFalse()
        {
            // arrange
            var enumerationitem = TestEnumerationOfIntegers._0;

            // act
            var isPositive = enumerationitem.EnumerationItemValueIsPositive();

            // assert
            isPositive.Should().BeFalse();
        }

        [TestMethod]
        public void EnumerationItemValueIsPositive_TestEnum_1_ReturnsTrue()
        {
            // arrange
            var enumerationitem = TestEnumerationOfIntegers._1;

            // act
            var isPositive = enumerationitem.EnumerationItemValueIsPositive();

            // assert
            isPositive.Should().BeTrue();
        }

        [TestMethod]
        public void EnumerationItemValueIsPositive_NotEnumeration_ThrowsArgumentException()
        {
            // arrange
            var value = 17;

            // act
            Action checkValue = () => value.EnumerationItemValueIsPositive();

            // assert
            checkValue.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void EnumerationItemValueIsPositive_UnderlyingTypeNotSupported_ThrowsNotImplementedException()
        {
            // arrange
            var value = TestEnumerationOfUnsignedIntegers.Item;

            // act
            Action checkValue = () => value.EnumerationItemValueIsPositive();

            // assert
            checkValue.Should().Throw<NotImplementedException>();
        }

        #endregion Tests: EnumerationItemValueIsPositive<T>()
    }
}