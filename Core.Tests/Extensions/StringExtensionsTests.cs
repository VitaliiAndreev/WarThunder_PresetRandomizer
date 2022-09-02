using Core.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
            var searchedCharacter = ',';

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
            var searchedCharacter = '.';

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
        #region Tests: IsPartiallyIn()

        [TestMethod]
        public void IsPartiallyIn()
        {
            // arrange
            var stringList = new List<string>
            {
                "Upheaval",
                "Placeholder",
                "Maintenance",
            };

            // act
            var isIn = "holder".IsPartiallyIn(stringList);

            // assert
            isIn.Should().BeTrue();
        }

        #endregion Tests: IsPartiallyIn()
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
        public void SkipLast_Minus1_OutOf_3()
        {
            // arrange
            var originalString = "ABC";

            // act
            var trimmedString = originalString.SkipLast(-1);

            // assert
            trimmedString.Should().Be("ABC");
        }

        [TestMethod]
        public void SkipLast_0_OutOf_3()
        {
            // arrange
            var originalString = "ABC";

            // act
            var trimmedString = originalString.SkipLast(0);

            // assert
            trimmedString.Should().Be("ABC");
        }

        [TestMethod]
        public void SkipLast_1_OutOf_3()
        {
            // arrange
            var originalString = "ABC";

            // act
            var trimmedString = originalString.SkipLast(1);

            // assert
            trimmedString.Should().Be("AB");
        }

        [TestMethod]
        public void SkipLast_2_OutOf_3()
        {
            // arrange
            var originalString = "ABC";

            // act
            var trimmedString = originalString.SkipLast(2);

            // assert
            trimmedString.Should().Be("A");
        }

        [TestMethod]
        public void SkipLast_3_OutOf_3()
        {
            // arrange
            var originalString = "ABC";

            // act
            var trimmedString = originalString.SkipLast(3);

            // assert
            trimmedString.Should().Be(string.Empty);
        }

        [TestMethod]
        public void SkipLast_4_OutOf_3()
        {
            // arrange
            var originalString = "ABC";

            // act
            var trimmedString = originalString.SkipLast(4);

            // assert
            trimmedString.Should().Be(string.Empty);
        }

        #endregion Tests: SkipLast()
        #region Tests: TakeLast()

        [TestMethod]
        public void TakeLast_Minus1_OutOf_3()
        {
            // arrange
            var originalString = "ABC";

            // act
            var trimmedString = originalString.TakeLast(-1);

            // assert
            trimmedString.Should().Be(string.Empty);
        }

        [TestMethod]
        public void TakeLast_0_OutOf_3()
        {
            // arrange
            var originalString = "ABC";

            // act
            var trimmedString = originalString.TakeLast(0);

            // assert
            trimmedString.Should().Be(string.Empty);
        }

        [TestMethod]
        public void TakeLast_1_OutOf_3()
        {
            // arrange
            var originalString = "ABC";

            // act
            var trimmedString = originalString.TakeLast(1);

            // assert
            trimmedString.Should().Be("C");
        }

        [TestMethod]
        public void TakeLast_2_OutOf_3()
        {
            // arrange
            var originalString = "ABC";

            // act
            var trimmedString = originalString.TakeLast(2);

            // assert
            trimmedString.Should().Be("BC");
        }

        [TestMethod]
        public void TakeLast_3_OutOf_3()
        {
            // arrange
            var originalString = "ABC";

            // act
            var trimmedString = originalString.TakeLast(3);

            // assert
            trimmedString.Should().Be("ABC");
        }

        [TestMethod]
        public void TakeLast_4_OutOf_3()
        {
            // arrange
            var originalString = "ABC";

            // act
            var trimmedString = originalString.TakeLast(4);

            // assert
            trimmedString.Should().Be("ABC");
        }

        #endregion Tests: TakeLast()
    }
}