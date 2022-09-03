using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Core.Tests.Extensions
{
    /// <summary> See <see cref="FileInfoExtensions"/>. </summary>
    [TestClass]
    public class FileInfoExtensionsTests
    {
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
    }
}