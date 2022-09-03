using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Core.Tests.Extensions
{
    /// <summary> See <see cref="IntExtensions"/>. </summary>
    [TestClass]
    public class IntExtensionsTests
    {
        [Flags]
        private enum Flags
        {
            Zero = 0,
            One = 1,
            Two = 2,
            Three = 4,
            Four = 8,
        }

        #region Tests: GetBitCount()

        [TestMethod]
        public void GetBitCount_0()
        {
            // arrange
            var value = Flags.Zero.CastTo<int>();

            // act
            var bitCount = value.GetBitCount();

            // assert
            bitCount.Should().Be(0);
        }

        [TestMethod]
        public void GetBitCount_1()
        {
            // arrange
            var value = Flags.One.CastTo<int>();

            // act
            var bitCount = value.GetBitCount();

            // assert
            bitCount.Should().Be(1);
        }

        [TestMethod]
        public void GetBitCount_2()
        {
            // arrange
            var value = (Flags.One | Flags.Two ).CastTo<int>();

            // act
            var bitCount = value.GetBitCount();

            // assert
            bitCount.Should().Be(2);
        }

        [TestMethod]
        public void GetBitCount_3()
        {
            // arrange
            var value = (Flags.One | Flags.Two | Flags.Three).CastTo<int>();

            // act
            var bitCount = value.GetBitCount();

            // assert
            bitCount.Should().Be(3);
        }

        #endregion Tests: GetBitCount()
    }
}