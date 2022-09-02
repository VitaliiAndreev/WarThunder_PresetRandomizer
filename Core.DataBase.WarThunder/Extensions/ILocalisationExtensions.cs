using Core.DataBase.WarThunder.Objects.Localization.Interfaces;

namespace Core.DataBase.WarThunder.Extensions
{
    /// <summary> Methods extending the <see cref="ILocalisation"/> interface. </summary>
    public static class ILocalisationExtensions
    {
        /// <summary> Gets a localised string mathcing the given language. </summary>
        /// <param name="localizationSet"> The localisation set to read from. </param>
        /// <param name="language"> The language to get a string for. </param>
        /// <returns></returns>
        public static string GetLocalisation(this ILocalisation localizationSet, Language language)
        {
            return language switch
            {
                Language.English => localizationSet.English,
                Language.Russian => localizationSet.Russian,
                _ => null,
            };
        }
    }
}