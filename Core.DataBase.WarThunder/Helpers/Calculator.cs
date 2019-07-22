using System;

namespace Core.DataBase.WarThunder.Helpers
{
    /// <summary> Provide calculation methods. </summary>
    public static class Calculator
    {
        #region Constants

        private const decimal _firstBattleRatingStep = 0.0m;
        private const decimal _secondBattleRatingStep = 0.3m;
        private const decimal _thirdBattleRatingStep = 0.7m;

        #endregion Constants
        #region Methods

        /// <summary> Calculates battle rating from economic rank. </summary>
        /// <param name="economicRank"> An economic rank to calculate the battle rating from. </param>
        /// <returns></returns>
        public static decimal GetBattleRating(int economicRank) => Math.Round(economicRank / 3.0m + 1, 1);

        /// <summary> Calculates economic rank from battle rating. </summary>
        /// <param name="battleRating"> A battle rating to calculate the economic rank from. </param>
        /// <returns></returns>
        public static int GetEconomicRank(decimal battleRating) => Convert.ToInt32(Math.Round((battleRating - 1m) * 3m));

        /// <summary> Rounds the given value down to the closest valid battle rating. </summary>
        /// <param name="number"> The number to round down. </param>
        /// <returns></returns>
        public static decimal GetRoundedBattleRating(decimal number)
        {
            var integralPart = number.Floor();
            var franctionalPart = number - integralPart;

            var roundedBattleRating = integralPart;

            if (franctionalPart >= _thirdBattleRatingStep)
                roundedBattleRating += _thirdBattleRatingStep;

            else if (franctionalPart >= _secondBattleRatingStep)
                roundedBattleRating += _secondBattleRatingStep;

            else if (franctionalPart >= _firstBattleRatingStep)
                roundedBattleRating += _firstBattleRatingStep;

            else throw new ArgumentException("Invalid fractional part.");

            return roundedBattleRating;
        }

        #endregion Methods
    }
}