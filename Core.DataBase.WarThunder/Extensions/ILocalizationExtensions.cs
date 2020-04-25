using Core.DataBase.WarThunder.Objects.Localization.Interfaces;
using Core.Enumerations;

namespace Core.DataBase.WarThunder.Extensions
{
    /// <summary> Methods extending the <see cref="ILocalization"/> interface. </summary>
    public static class ILocalizationExtensions
    {
        /// <summary> Gets a localized string mathcing the given language. </summary>
        /// <param name="localizationSet"> The localization set to read from. </param>
        /// <param name="language"> The language to get a string for. </param>
        /// <returns></returns>
        public static string GetLocalization(this ILocalization localizationSet, ELanguage language)
        {
            return language switch
            {
                ELanguage.English => localizationSet.English,
                ELanguage.Russian => localizationSet.Russian,
                _ => null,
            };
        }
    }
}