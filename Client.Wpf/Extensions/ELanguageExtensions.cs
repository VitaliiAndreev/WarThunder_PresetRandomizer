using Client.Wpf.Enumerations;
using Core;

namespace Client.Wpf.Extensions
{
    /// <summary> Methods extending the <see cref="Language"/> enumeration. </summary>
    public static class ELanguageExtensions
    {
        /// <summary> Returns the resource key for the flag of the country with the specified language. </summary>
        /// <param name="language"> The language to get a flag resource key for. </param>
        /// <returns></returns>
        public static string GetFlagResourceKey(this Language language)
        {
            return language switch
            {
                Language.English => EBitmapImageKey.FlagUsa,
                Language.Russian => EBitmapImageKey.FlagRussian,
                _ => null,
            };
        }
    }
}