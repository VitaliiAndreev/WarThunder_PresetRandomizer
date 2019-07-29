using Client.Console.Helpers.Interfaces;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Helpers.Logger.Interfaces;
using Core.Json.WarThunder.Helpers.Interfaces;
using Core.Objects;
using Core.Organization.Enumerations;
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

        /// <summary> Creates a new manager. </summary>
        public ConsoleClientManager
        (
            IWarThunderFileManager fileManager,
            IWarThunderFileReader fileReader,
            IWarThunderSettingsManager settingsManager,
            IParser parser,
            IUnpacker unpacker,
            IWarThunderJsonHelper jsonHelper,
            IRandomizer randomizer,
            IVehicleSelector vehicleSelector,
            params IConfiguredLogger[] loggers
        ) : base(fileManager, fileReader, settingsManager, parser, unpacker, jsonHelper, randomizer, vehicleSelector, loggers)
        {
        }

        #endregion Constructors

        /// <summary> Randomly selects vehicles based on the given specification. </summary>
        /// <param name="specification"> The specification to base the selection on. </param>
        /// <returns></returns>
        public IEnumerable<IVehicle> GetRandomVehicles(Specification specification)
        {
            var battleRatingBracket = new IntervalDecimal(true, specification.BattleRating - _maximumBattleRatingDifference, specification.BattleRating, true);

            return _cache
                .OfType<IVehicle>()
                .Where(vehicle => vehicle.Nation.GaijinId == EReference.Nations[specification.Nation])
                .Where(vehicle => vehicle.Branch.GaijinId.Contains(EReference.Branches[specification.Branch]))
                .OrderByHighestBattleRating(_vehicleSelector, specification.GameMode, battleRatingBracket)
                .GetRandomizedVehicles(_vehicleSelector)
                .Take(10)
            ;
        }
    }
}