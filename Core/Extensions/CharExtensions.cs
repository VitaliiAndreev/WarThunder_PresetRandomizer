namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="char"/> class. </summary>
    public static class CharExtensions
    {
        #region Methods: Fluency

        /// <summary> Fluently determines whether the source character is a digit. </summary>
        /// <param name="character"> The source character. </param>
        /// <returns></returns>
        public static bool IsDigitFluently(this char character) =>
            char.IsDigit(character);

        /// <summary> Fluently determines whether the source character is a punctuation mark. </summary>
        /// <param name="character"> The source character. </param>
        /// <returns></returns>
        public static bool IsPunctuationFluently(this char character) =>
            char.IsPunctuation(character);

        /// <summary> Fluently determines whether the source character is white space. </summary>
        /// <param name="character"> The source character. </param>
        /// <returns></returns>
        public static bool IsWhiteSpaceFluently(this char character) =>
            char.IsWhiteSpace(character);

        #endregion Methods: Fluency
    }
}
