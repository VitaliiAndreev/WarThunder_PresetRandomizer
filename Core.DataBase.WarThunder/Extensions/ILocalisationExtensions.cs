using Core.DataBase.WarThunder.Objects.Localization.Interfaces;
using Core.Enumerations;

namespace Core.DataBase.WarThunder.Extensions
{
    /// <summary> Methods extending the <see cref="ILocalisation"/> interface. </summary>
    public static class ILocalisationExtensions
    {
        /// <summary> Gets a localised string mathcing the given language. </summary>
        /// <param name="localizationSet"> The localisation set to read from. </param>
        /// <param name="language"> The language to get a string for. </param>
        /// <returns></returns>
        public static string GetLocalisation(this ILocalisation localizationSet, ELanguage language)
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