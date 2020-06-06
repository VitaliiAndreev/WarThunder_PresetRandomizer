using Core.Tests;
using Core.Web.Helpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Web.Tests.Helpers
{
    [TestClass]
    public class HtmlParserTests
    {
        [TestMethod]
        public void GetHtmlDocumentNode()
        {
            // arrange
            var parser = new HtmlParser(Presets.Logger);

            // act
            var result = parser.GetHtmlDocumentNode("https://google.com");

            // assert
            result.Should().NotBeNull();
            result.ChildNodes.Should().NotBeEmpty();
        }
    }
}