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
        #region Tests: GetFileNameWithoutExtension()

        [TestMethod]
        public void GetFileNameWithoutExtension()
        {
            // arrange
            var expectedFileNameWithoutExtension = "whatsitsface";
            var fileInfo = new FileInfo($@"{Directory.GetCurrentDirectory()}\{expectedFileNameWithoutExtension}.gif");

            // act
            var actualFileNameWithoutExtension = fileInfo.GetFileNameWithoutExtension();

            // assert
            actualFileNameWithoutExtension.Should().Be(expectedFileNameWithoutExtension);
        }

        #endregion Tests: GetFileNameWithoutExtension()
    }
}
