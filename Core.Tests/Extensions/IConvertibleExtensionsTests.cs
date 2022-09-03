using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Tests.Extensions
{
    /// <summary> See <see cref="IConvertibleExtensions"/>. </summary>
    [TestClass]
    public class IConvertibleExtensionsTests
    {
        #region Tests: ConvertFromTo()

        [TestMethod]
        public void ConvertFromTo_LongToInt()
        {
            // arrange
            var source = 17L;

            // act
            var output = source.ConvertFromTo<long, int>();

            // assert
            output.Should().Be(17);
        }

        [TestMethod]
        public void ConvertFromTo_DoubleToIntRoundedDown()
        {
            // arrange
            var source = 17.4;

            // act
            var output = source.ConvertFromTo<double, int>();

            // assert
            output.Should().Be(17);
        }

        [TestMethod]
        public void ConvertFromTo_DoubleToIntRoundedUp()
        {
            // arrange
            var source = 17.5;

            // act
            var output = source.ConvertFromTo<double, int>();

            // assert
            output.Should().Be(18);
        }

        #endregion Tests: ConvertFromTo()
    }
}
