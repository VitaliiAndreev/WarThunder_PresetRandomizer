using Core.Enumerations;
using System;
using System.Linq;
using System.Text;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="string"/> class. </summary>
    public static class StringExtensions
    {
        #region Methods: Fluency

        /// <summary> Fluently replaces format placeholders in a string with members of an argument array. </summary>
        /// <param name="source"> A source string. </param>
        /// <param name="arguments"> An array of objects whose string representations to insert into the source string. </param>
        /// <returns></returns>
        public static string FormatFluently(this string source, params object[] arguments) =>
            string.Format(source, arguments);

        #endregion Methods: Fluency
        #region Methods: Formatting

        /// <summary> Flattens a string by removing line breaks and inserting spaces in their stead, redundant space and punctuation marks are also removed. </summary>
        /// <param name="source"> A source string. </param>
        /// <returns></returns>
        public static string Flatten(this string source) =>
            source
                .Split
                (
                    new char[] { ECharacter.CarriageReturn, ECharacter.NewLine },
                    StringSplitOptions.RemoveEmptyEntries
                )
                .Where(line => !line.All(character => character.IsWhiteSpaceFluently() || character.IsPunctuationFluently()))
                .Select(line => line.Trim())
                .StringJoin(ECharacter.Space)
            ;

        /// <summary>
        /// Resets formatting placeholder ({N}) numbers to be in a correct ascending order.
        /// Use this method before filling placeholders when a formatting string is being formed with different chunks of formatting strings.
        /// In a case of mismatching left and right braces expect the result string to be corrupted.
        /// </summary>
        /// <param name="source"> A source string. </param>
        /// <returns></returns>
        public static string ResetFormattingPlaceholders(this string source)
        {
            var substrings = source
                .Split(new char[] { ECharacter.BraceLeft }, StringSplitOptions.RemoveEmptyEntries)
                .ToList()
            ;

            var firstSubstring = substrings.First();
            var substringsWithoutFirst = substrings
                .Skip(1)
                .Select(substring => substring.Skip(substring.IndexOf(ECharacter.BraceRight) + 1).StringJoin())
            ;

            substrings = new string[] { firstSubstring }
                .Concat(substringsWithoutFirst)
                .ToList();

            var formatPlaceholderCounter = default(int);
            var stringBuilder = new StringBuilder();

            for (var i = 0; i < substrings.Count; i++)
            {
                stringBuilder.Append(substrings[i]);
                stringBuilder.Append(i == substrings.Count - 1? string.Empty : $"{ECharacter.BraceLeft}{formatPlaceholderCounter++}{ECharacter.BraceRight}");
            }

            return stringBuilder.ToString();
        }

        #endregion Methods: Formatting
    }
}
