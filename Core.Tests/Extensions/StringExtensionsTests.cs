using Core.Enumerations;
using Core.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Tests.Extensions
{
    /// <summary> See<see cref="StringExtensions"/>. </summary>
    [TestClass]
    public class StringExtensionsTests
    {
        #region Tests: Contains()

        [TestMethod]
        public void Contains_CharacterNotPresent_ReturnsFalse()
        {
            // arrange
            var sourceString = "whatsitsface.gif";
            var searchedCharacter = ECharacter.Comma;

            // act
            var containsCharacter = sourceString.Contains(searchedCharacter);

            // assert
            containsCharacter.Should().BeFalse();
        }

        [TestMethod]
        public void Contains_CharacterPresent_ReturnsTrue()
        {
            // arrange
            var sourceString = "whatsitsface.gif";
            var searchedCharacter = ECharacter.Period;

            // act
            var containsCharacter = sourceString.Contains(searchedCharacter);

            // assert
            containsCharacter.Should().BeTrue();
        }

        #endregion Tests: Contains()
        #region Tests: Flatten()

        [TestMethod]
        public void Flatten_ShouldNotContainNewLineCarriageReturnRedundantWhitespaceAndPunctuationCharacters()
        {
            // arrange
            var sourceString = " AAA,\n\r,\n BBB \nCC C \n   \r,\nD\n";
            var expectedString = "AAA, BBB CC C D";

            // act
            var actualString = sourceString.Flatten();

            // assert
            actualString.Should().Be(expectedString);
        }

        #endregion Tests: Flatten()
        #region Tests: ResetFormattingPlaceholders()

        [TestMethod]
        public void ResetFormattingPlaceholders_CorrectInput()
        {
            // arrange
            var sourceString = "AAA{10} BBB{1} CCC{9}";
            var expectedString = "AAA{0} BBB{1} CCC{2}";

            // act
            var actualString = sourceString.ResetFormattingPlaceholders();

            // assert
            actualString.Should().Be(expectedString);
        }

        [TestMethod]
        public void ResetFormattingPlaceholders_InputCorruptedWithLeftBrace()
        {
            // arrange
            var sourceString = " {AAA{10}{0} BBB{1} CCC{9}{  ";
            var expectedString = " {0}AAA{1}{2} BBB{3} CCC{4}{5}  ";

            // act
            var actualString = sourceString.ResetFormattingPlaceholders();

            // assert
            actualString.Should().Be(expectedString);
        }

        [TestMethod]
        public void ResetFormattingPlaceholders_InputCorruptedWithBothBraces()
        {
            // arrange
            var sourceString = " }{AA}A{10}{0} B}BB{1} CCC{9}{  ";
            var expectedString = " }{0}A{1}{2} B}BB{3} CCC{4}{5}  ";

            // act
            var actualString = sourceString.ResetFormattingPlaceholders();

            // assert
            actualString.Should().Be(expectedString);
        }

        #endregion Tests: ResetFormattingPlaceholders()
        #region Tests: SkipLast()

        [TestMethod]
        public void SkipLast_Carramba_SkipLast5_ReturnsCar()
        {
            // arrange
            var originalString = "carramba";

            // act
            var trimmedString = originalString.SkipLast(5);

            // assert
            trimmedString.Should().Be("car");
        }

        #endregion Tests: SkipLast()
    }
}