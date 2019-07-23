using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Objects;
using System.Collections.Generic;

namespace Core.Organization.Helpers.Interfaces
{
    /// <summary> Provides methods to randomize selection. </summary>
    public interface IVehicleSelector
    {
        #region Methods: Ordering

        /// <summary> Selects vehicles from the given collection by inclusion of their battle ratings (for the corresponding game mode) in the given battle rating bracket. </summary>
        /// <param name="gameMode"> The game mode, battle rating for which to check. </param>
        /// <param name="battleRatingBracket"> The battle rating bracket. </param>
        /// <param name="vehicles"> The vehicles to choose from. </param>
        /// <returns></returns>
        IDictionary<decimal, IList<IVehicle>> OrderByHighestBattleRating(EGameMode gameMode, IntervalDecimal battleRatingBracket, IEnumerable<IVehicle> vehicles);

        #endregion Methods: Ordering
        #region Methods: Randomization

        /// <summary>
        /// Randomly picks <see cref="_vehicleAmountToSelect"/> of vehicles with the highest battle rating from the specified dictionary.
        /// If there are fewer vehicles with the highest battle rating than required, vehicles with the next lower battle rating step are rendomly taken, and so on.
        /// </summary>
        /// <param name="vehiclesByBattleRatings"> The dictionary of battle ratings with vehicles to select from. </param>
        /// <returns></returns>
        IEnumerable<IVehicle> GetRandom(IDictionary<decimal, IList<IVehicle>> vehiclesByBattleRatings);

        #endregion Methods: Randomization
    }
}