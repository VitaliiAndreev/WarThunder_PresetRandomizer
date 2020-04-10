using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Objects;
using Core.Organization.Helpers.Interfaces;
using System.Collections.Generic;

namespace Core.Organization.Extensions
{
    /// <summary> Methods extending the <see cref="IEnumerable{T}"/> class. </summary>
    public static class IEnumerableExtensions
    {
        #region Methods: Ordering

        /// <summary>
        /// Selects vehicles using the given vehicle selector by inclusion of their battle ratings (for the corresponding game mode) in the given battle rating bracket.
        /// <see cref="IVehicleSelector.OrderByHighestBattleRating(EGameMode, Interval, IEnumerable{IVehicle})"/> is being fluently called.
        /// </summary>
        /// <param name="gameMode"> The game mode, battle rating for which to check. </param>
        /// <param name="battleRatingBracket"> The battle rating bracket. </param>
        /// <param name="vehicles"> The vehicles to choose from. </param>
        /// <returns></returns>
        public static IDictionary<decimal, IList<IVehicle>> OrderByHighestBattleRating(this IEnumerable<IVehicle> vehicles, IVehicleSelector vehicleSelector, EGameMode gameMode, Interval<decimal> battleRatingBracket) =>
            vehicleSelector.OrderByHighestBattleRating(gameMode, battleRatingBracket, vehicles);

        #endregion Methods: Ordering
    }
}