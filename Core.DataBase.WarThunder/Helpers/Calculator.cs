using System;

namespace Core.DataBase.WarThunder.Helpers
{
    /// <summary> Provide calculation methods. </summary>
    public static class Calculator
    {
        #region Methods

        /// <summary> Calculates battle rating from economic rank. </summary>
        /// <param name="economicRank"> An economic rank to calculate the battle rating from. </param>
        /// <returns></returns>
        public static decimal GetBattleRating(int economicRank) => Math.Round(economicRank / 3.0m + 1, 1);

        /// <summary> Calculates economic rank from battle rating. </summary>
        /// <param name="battleRating"> A battle rating to calculate the economic rank from. </param>
        /// <returns></returns>
        public static int GetEconomicRank(decimal battleRating) => Convert.ToInt32(Math.Round((battleRating - 1m) * 3m));

        #endregion Methods
    }
}