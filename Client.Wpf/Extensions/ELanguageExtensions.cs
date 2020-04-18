using Client.Wpf.Enumerations;
using Core.Localization.Enumerations;

namespace Client.Wpf.Extensions
{
    /// <summary> Methods extending the <see cref="ELanguage"/> enumeration. </summary>
    public static class ELanguageExtensions
    {
        /// <summary> Returns the resource key for the flag of the country with the specified language. </summary>
        /// <param name="language"> The language to get a flag resource key for. </param>
        /// <returns></returns>
        public static string GetFlagResourceKey(this ELanguage language)
        {
            return language switch
            {
                ELanguage.English => EBitmapImageKey.FlagUsa,
                ELanguage.Russian => EBitmapImageKey.FlagRussian,
                _ => null,
            };
        }
    }
}