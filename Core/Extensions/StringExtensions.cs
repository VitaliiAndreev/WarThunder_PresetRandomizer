﻿using Core.Enumerations;
using System;
using System.Linq;
using System.Text;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="string"/> class. </summary>
    public static class StringExtensions
    {
        #region Methods: Fluency

        /// <summary> Checks whether the specified character is in the string. </summary>
        /// <param name="source"> A source string. </param>
        /// <param name="character"> The character to look for. </param>
        /// <returns></returns>
        public static bool Contains(this string sourceString, char character) =>
            sourceString.Contains(character.ToString());

        /// <summary> Fluently replaces format placeholders in a string with members of an argument array. </summary>
        /// <param name="source"> A source string. </param>
        /// <param name="arguments"> An array of objects whose string representations to insert into the source string. </param>
        /// <returns></returns>
        public static string FormatFluently(this string source, params object[] arguments) =>
            string.Format(source, arguments);

        /// <summary> Fluently checks whether the string is null, empty, or consists only of white-space characters. </summary>
        /// <param name="source"> A source string. </param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpaceFluently(this string source) =>
            string.IsNullOrWhiteSpace(source);

        /// <summary> Returns the source string as a new string with the specified number of trailing items skipped. </summary>
        /// <param name="sourceString"> A source string. </param>
        /// <param name="amountOfCharactersToSkip"> The amount of trailing characters to skip. </param>
        /// <returns></returns>
        public static string SkipLast(this string sourceString, int amountOfCharactersToSkip) =>
            sourceString.Substring(0, sourceString.Length - amountOfCharactersToSkip);

        #region Split()

        /// <summary> Splits the string into substrings by the specified separator with set split option. </summary>
        /// <param name="sourceString"> The string to split. </param>
        /// <param name="separator"> A separator to split by. </param>
        /// <param name="options"> split options. </param>
        /// <returns></returns>
        public static string[] Split(this string sourceString, char separator, StringSplitOptions options) =>
            sourceString.Split(new char[] { separator }, options);

        /// <summary> Splits the string into substrings by the specified separator. </summary>
        /// <param name="sourceString"> The string to split. </param>
        /// <param name="separator"> A separator to split by. </param>
        /// <returns></returns>
        public static string[] Split(this string sourceString, string separator) =>
            sourceString.Split(new string[] { separator }, StringSplitOptions.None);

        #endregion Split()

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

            var stringBuilder = new StringBuilder();

            for (var i = 0; i < substrings.Count; i++)
            {
                stringBuilder.Append(substrings[i]);
                stringBuilder.Append(i == substrings.Count - 1? string.Empty : $"{ECharacter.BraceLeft}{i}{ECharacter.BraceRight}");
            }

            return stringBuilder.ToString();
        }

        #endregion Methods: Formatting
    }
}
