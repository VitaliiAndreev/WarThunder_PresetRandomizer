using Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="string"/> class. </summary>
    public static class StringExtensions
    {
        #region Methods: Filtering

        /// <summary> Returns the source string as a new string with the specified number of trailing items skipped. </summary>
        /// <param name="sourceString"> A source string. </param>
        /// <param name="amountOfCharactersToSkip"> The amount of trailing characters to skip. </param>
        /// <returns></returns>
        public static string SkipLast(this string sourceString, int amountOfCharactersToSkip)
        {
            if (amountOfCharactersToSkip >= sourceString.Count())
                return string.Empty;
            else if (amountOfCharactersToSkip < 0)
                return sourceString;

            return sourceString.Substring(0, sourceString.Count() - amountOfCharactersToSkip);
        }

        /// <summary> Creates a new string from the source by taking the specified amount of characters from the tail end of it. </summary>
        /// <param name="sourceString"> A source string. </param>
        /// <param name="amountOfCharactersToSkip"> The amount of trailing characters to take. </param>
        /// <returns></returns>
        public static string TakeLast(this string sourceString, int amountOfCharactersToSkip)
        {
            if (amountOfCharactersToSkip >= sourceString.Count())
                return sourceString;
            else if (amountOfCharactersToSkip < 0)
                return string.Empty;

            return sourceString.Substring(sourceString.Count() - amountOfCharactersToSkip, amountOfCharactersToSkip);
        }

        #endregion Methods: Filtering
        #region Methods: Fluency

        /// <summary> Fluently replaces format placeholders in a string with members of an argument array. </summary>
        /// <param name="sourceString"> A source string. </param>
        /// <param name="arguments"> An array of objects whose string representations to insert into the source string. </param>
        /// <returns></returns>
        public static string FormatFluently(this string sourceString, params object[] arguments) =>
            string.Format(sourceString, arguments);

        /// <summary> Fluently parses the specified name of an item of the given enumeration type into the corresponding enumeration item. </summary>
        /// <typeparam name="T"> The enumeration to parse a value of. </typeparam>
        /// <param name="enumerationItemName"> The name of the enumeration item to parse. </param>
        /// <returns></returns>
        public static T ParseEnumeration<T>(this string enumerationItemName) where T : struct =>
            (T)Enum.Parse(typeof(T), enumerationItemName);

        /// <summary> Fluently tries to parse the specified name of an item of the given enumeration type into the corresponding enumeration item. </summary>
        /// <typeparam name="T"> The enumeration to parse a value of. </typeparam>
        /// <param name="enumerationItemName"> The name of the enumeration item to parse. </param>
        /// <param name="enumerationItem"> The resulting enumeration item. </param>
        /// <returns></returns>
        public static bool TryParseEnumeration<T>(this string enumerationItemName, out T enumerationItem) where T : struct =>
            Enum.TryParse<T>(enumerationItemName, out enumerationItem);

        #region Regular Expressions

        /// <summary> Checks whether the string patches the specified regular expression pattern. </summary>
        /// <param name="sourceString"> A source string. </param>
        /// <param name="regularExpression"> The pattern to match against. </param>
        /// <returns></returns>
        public static bool Matches(this string sourceString, string regularExpression) =>
            new Regex(regularExpression).IsMatch(sourceString);

        /// <summary> Matches the string against the specified regular expression pattern and returns all occurrences. </summary>
        /// <param name="sourceString"> A source string. </param>
        /// <param name="regularExpression"> The pattern to match against. </param>
        /// <returns></returns>
        public static IEnumerable<string> GetMatchesWith(this string sourceString, string regularExpression) =>
            new Regex(regularExpression).Matches(sourceString).OfType<string>();

        #endregion Regular Expressions
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
        #region Methods: Searching

        /// <summary> Checks whether the specified character is in the string. </summary>
        /// <param name="sourceString"> A source string. </param>
        /// <param name="character"> The character to look for. </param>
        /// <returns></returns>
        public static bool Contains(this string sourceString, char character) =>
            sourceString.Contains(character.ToString());

        /// <summary> Checks whether the string occurrs within the specified string. </summary>
        /// <param name="sourceString"> A source string. </param>
        /// <param name="targetString"> The string to search in. </param>
        /// <returns></returns>
        public static bool IsIn(this string sourceString, string targetString) =>
            targetString.Contains(sourceString);

        /// <summary> Checks whether the string occurrs within any of strings in the collection. </summary>
        /// <param name="sourceString"> A source string. </param>
        /// <param name="stringCollection"> The collection to search in. </param>
        /// <returns></returns>
        public static bool IsPartiallyIn(this string sourceString, IEnumerable<string> stringCollection) =>
            stringCollection
                .Any(stringInCollection => stringInCollection.Contains(sourceString));

        /// <summary> Checks whether the string contains any occurrences of strings in the collection. </summary>
        /// <param name="sourceString"> A source string. </param>
        /// <param name="stringCollection"> The collection to search check. </param>
        /// <returns></returns>
        public static bool ContainsAny(this string sourceString, IEnumerable<string> stringCollection) =>
            stringCollection
                .Any(stringInCollection => sourceString.Contains(stringInCollection));

        #endregion Methods: Searching

        /// <summary> Creates a new text reader with the given string. </summary>
        /// <param name="sourceString"> A source string. </param>
        /// <returns></returns>
        [SuppressMessage("Code Quality", "IDE0067:Dispose objects before losing scope", Justification = "StreamWriter cannot be closed here.")]
        public static TextReader CreateTextReader(this string sourceString)
        {
            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream);
                
            streamWriter.Write(sourceString);
            streamWriter.Flush();
            memoryStream.Position = 0;

            return new StreamReader(memoryStream);
        }
    }
}