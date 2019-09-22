using Client.Console.Enumerations;
using Client.Console.Helpers.Interfaces;
using Core.Csv.WarThunder.Helpers.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Helpers;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Helpers.Logger.Interfaces;
using Core.Json.WarThunder.Helpers.Interfaces;
using Core.Objects;
using Core.Organization.Extensions;
using Core.Organization.Helpers;
using Core.Organization.Helpers.Interfaces;
using Core.Organization.Objects.SearchSpecifications;
using Core.Randomization.Helpers.Interfaces;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Client.Console.Helpers
{
    /// <summary> Controls the flow of the application. </summary>
    public class ConsoleClientManager : Manager, IConsoleClientManager
    {
        #region Constructors

        /// <summary> Creates a new manager and loads settings stored in the <see cref="EConsoleClientFile.Settings"/> file. </summary>
        public ConsoleClientManager
        (
            IWarThunderFileManager fileManager,
            IWarThunderFileReader fileReader,
            IWarThunderSettingsManager settingsManager,
            IParser parser,
            IUnpacker unpacker,
            IWarThunderJsonHelper jsonHelper,
            ICsvDeserializer csvDeserializer,
            IRandomizer randomizer,
            IVehicleSelector vehicleSelector,
            params IConfiguredLogger[] loggers
        ) : base(fileManager, fileReader, settingsManager, parser, unpacker, jsonHelper, csvDeserializer, randomizer, vehicleSelector, loggers)
        {
        }

        #endregion Constructors

        /// <summary> Randomly selects vehicles based on the given specification. </summary>
        /// <param name="specification"> The specification to base the selection on. </param>
        /// <returns></returns>
        public IEnumerable<IVehicle> GetRandomVehicles(Specification specification)
        {
            var nation = _randomizer.GetRandom(specification.Nations);
            var branch = _randomizer.GetRandom(specification.Branches);
            var battleRating = Calculator.GetBattleRating(_randomizer.GetRandom(specification.EconomicRanks));

            var battleRatingBracket = new Interval<decimal>(true, battleRating - _maximumBattleRatingDifference, battleRating, true);
            
            return _cache
                .OfType<IVehicle>()
                .Where(vehicle => vehicle.Nation.GaijinId == EReference.NationsFromEnumeration[nation])
                .Where(vehicle => vehicle.Branch.GaijinId.Contains(EReference.BranchesFromEnumeration[branch]))
                .OrderByHighestBattleRating(_vehicleSelector, specification.GameMode, battleRatingBracket)
                .GetRandomizedVehicles(_vehicleSelector)
                .Take(10)
            ;
        }
    }
}