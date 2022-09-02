using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Helpers;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using Core.Objects;
using Core.Organization.Enumerations.Logger;
using Core.Organization.Helpers.Interfaces;
using Core.Randomization.Extensions;
using Core.Randomization.Helpers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Core.Organization.Helpers
{
    /// <summary> Provides methods to select vehicles. </summary>
    public class VehicleSelector : LoggerFluency, IVehicleSelector
    {
        #region Fields

        /// <summary> An instance of a randomizer. </summary>
        private readonly IRandomiser _randomiser;

        #endregion Fields
        #region Constructors

        /// <summary> Creates a new vehicle selector. </summary>
        /// <param name="randomiser"> An instance of a randomiser. </param>
        /// <param name="loggers"> Instances of loggers. </param>
        public VehicleSelector(IRandomiser randomiser, params IConfiguredLogger[] loggers)
            : base(EOrganizationLogCategory.VehicleSelector, loggers)
        {
            _randomiser = randomiser;

            LogDebug(CoreLogMessage.Created.Format(EOrganizationLogCategory.VehicleSelector));
        }

        #endregion Constructors
        #region Methods: Ordering

        /// <summary> Selects vehicles from the given collection by inclusion of their battle ratings (for the corresponding game mode) in the given battle rating bracket. </summary>
        /// <param name="gameMode"> The game mode, battle rating for which to check. </param>
        /// <param name="battleRatingBracket"> The battle rating bracket. </param>
        /// <param name="vehicles"> The vehicles to choose from. </param>
        /// <returns></returns>
        public IDictionary<decimal, IList<IVehicle>> OrderByHighestBattleRating(EGameMode gameMode, Interval<decimal> battleRatingBracket, IEnumerable<IVehicle> vehicles)
        {
            var sortedVehicles = new Dictionary<decimal, IList<IVehicle>>();
            var currentBattleRating = Calculator.GetRoundedBattleRating(battleRatingBracket.RightBounded ? battleRatingBracket.RightItem : battleRatingBracket.RightItem - Calculator.MinimumBattleRatingStep);

            while (battleRatingBracket.LeftBounded ? currentBattleRating >= battleRatingBracket.LeftItem : currentBattleRating > battleRatingBracket.LeftItem)
            {
                var vehiclesOnCurrentBattleRating = vehicles.Where(vehicle => vehicle.BattleRating[gameMode].HasValue && vehicle.BattleRating[gameMode].Value == currentBattleRating);

                if (vehiclesOnCurrentBattleRating.Any())
                    sortedVehicles.Add(currentBattleRating, vehiclesOnCurrentBattleRating.ToList());

                currentBattleRating = Calculator.GetRoundedBattleRating(currentBattleRating - Calculator.MinimumBattleRatingStep);
            }

            return sortedVehicles;
        }

        #endregion Methods: Ordering
        #region Methods: Randomization

        /// <summary>
        /// Randomly picks top <paramref name="amountToSelect"/> of vehicles with the highest battle rating from the specified dictionary.
        /// If there are fewer vehicles with the highest battle rating than required, vehicles with the next lower battle rating step are rendomly taken, and so on.
        /// </summary>
        /// <param name="vehiclesByBattleRatings"> The dictionary of battle ratings with vehicles to select from. </param>
        /// <param name="amountToSelect"> The amount of vehicles to select. </param>
        /// <returns></returns>
        public IEnumerable<IVehicle> GetRandom(IDictionary<decimal, IList<IVehicle>> vehiclesByBattleRatings, int amountToSelect)
        {
            var randomizedVehicles = new List<IVehicle>();

            while (randomizedVehicles.Count < amountToSelect && vehiclesByBattleRatings.Any())
            {
                var currentBattleRating = vehiclesByBattleRatings.Keys.First();
                var vehiclesOnCurrentBattleRating = vehiclesByBattleRatings[currentBattleRating];

                var vehiclesToAdd = vehiclesOnCurrentBattleRating
                    .Randomize(_randomiser)
                    .Take(amountToSelect - randomizedVehicles.Count())
                ;

                randomizedVehicles.AddRange(vehiclesToAdd);
                vehiclesOnCurrentBattleRating.RemoveRange(vehiclesToAdd);

                if (vehiclesOnCurrentBattleRating.IsEmpty())
                    vehiclesByBattleRatings.Remove(currentBattleRating);
            }

            return randomizedVehicles;
        }

        #endregion Methods: Randomization
    }
}