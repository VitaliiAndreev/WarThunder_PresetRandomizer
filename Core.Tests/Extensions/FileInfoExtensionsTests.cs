using Core.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Core.Tests.Extensions
{
    /// <summary> See <see cref="FileInfoExtensions"/>. </summary>
    [TestClass]
    public class FileInfoExtensionsTests
    {
        #region Tests: GetExtensionWithoutPeriod()

        [TestMethod]
        public void GetExtensionWithoutPeriod()
        {
            // arrange
            var exectedFileExtensionWithoutPeriod = "gif";
            var fileInfo = new FileInfo($@"{Directory.GetCurrentDirectory()}\whatsitsface.{exectedFileExtensionWithoutPeriod}");

            // act
            var actualFileExtensionWithoutPeriod = fileInfo.GetExtensionWithoutPeriod();

            // assert
            actualFileExtensionWithoutPeriod.Should().Be(exectedFileExtensionWithoutPeriod);
        }

        #endregion Tests: GetExtensionWithoutPeriod()
        #region Tests: GetFileNameWithoutExtension()

        [TestMethod]
        public void GetFileNameWithoutExtension()
        {
            // arrange
            var expectedFileNameWithoutExtension = "whatsitsface";
            var fileInfo = new FileInfo($@"{Directory.GetCurrentDirectory()}\{expectedFileNameWithoutExtension}.gif");

            // act
            var actualFileNameWithoutExtension = fileInfo.GetNameWithoutExtension();

            // assert
            actualFileNameWithoutExtension.Should().Be(expectedFileNameWithoutExtension);
        }

        #endregion Tests: GetFileNameWithoutExtension()
    }
}
