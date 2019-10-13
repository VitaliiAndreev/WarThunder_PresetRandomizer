using Core.DataBase.WarThunder.Enumerations;

namespace Core.DataBase.WarThunder.Extensions
{
    /// <summary> Methods extending the <see cref="ENation"/> enumeration. </summary>
    public static class ENationExtensions
    {
        /// <summary> Returns the base country of the given playable <paramref name="nation"/>. </summary>
        /// <param name="nation"> The playable nation whose base country to return. </param>
        /// <returns></returns>
        public static ECountry GetBaseCountry(this ENation nation)
        {
            return nation switch
            {
                ENation.Usa => ECountry.Usa,
                ENation.Germany => ECountry.NaziGermany,
                ENation.Ussr => ECountry.Ussr,
                ENation.Britain => ECountry.Britain,
                ENation.Japan => ECountry.EmpireOfJapan,
                ENation.China => ECountry.China,
                ENation.Italy => ECountry.KingdomOfItaly,
                ENation.France => ECountry.France,
                _ => ECountry.None,
            };
        }
    }
}