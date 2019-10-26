using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Organization.Helpers.Interfaces;
using Core.Organization.Objects;
using System.Collections.Generic;
using System.Linq;

namespace Core.Organization.Extensions
{
    /// <summary> Methods extending the <see cref="IDictionary{TKey, TValue}"/> interface. </summary>
    public static class IDictionaryExtensions
    {
        /// <summary> Checks whether a research tree for the given <paramref name="nation"/> exists. </summary>
        /// <param name="dictionary"> The dictionary of research trees indexed by nations. </param>
        /// <param name="nation"> The nation to check. </param>
        /// <returns></returns>
        public static bool Has(this IDictionary<ENation, ResearchTree> dictionary, ENation nation) =>
            dictionary.TryGetValue(nation, out var researchTree) && researchTree is ResearchTree && researchTree.Any();

        /// <summary>
        /// Randomly picks several vehicles with the highest battle rating from the specified dictionary.
        /// If there are fewer vehicles with the highest battle rating than required, vehicles with the next lower battle rating step are rendomly taken, and so on.
        /// <see cref="IVehicleSelector.GetRandom(IDictionary{decimal, IList{IVehicle}})"/> is being fluently called.
        /// </summary>
        /// <param name="vehicles"> The dictionary of battle ratings with vehicles to select from. </param>
        /// <param name="vehicleSelector"> The instance of a vehicle selector to select with. </param>
        /// <param name="amountToSelect"> The amount of vehicles to select. </param>
        /// <returns></returns>
        public static IEnumerable<IVehicle> GetRandomVehicles(this IDictionary<decimal, IList<IVehicle>> vehicles, IVehicleSelector vehicleSelector, int amountToSelect) =>
            vehicleSelector.GetRandom(vehicles, amountToSelect);
    }
}