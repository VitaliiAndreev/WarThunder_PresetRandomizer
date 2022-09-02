using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Core.Organization.Collections
{
    /// <summary> A vehicle preset implemented as a <see cref="ReadOnlyCollection{T}"/> of <see cref="IVehicle"/>. This class' purpose if fluency and reference of selection made during randomization. </summary>
    public class Preset : ReadOnlyCollection<IVehicle>
    {
        #region Properties

        public EGameMode GameMode { get; }

        /// <summary> The preset's nation. </summary>
        public ENation Nation { get; }

        /// <summary> The preset's main branch. </summary>
        public EBranch MainBranch { get; }

        /// <summary> Vehicle branches of vehicles in the collection. </summary>
        public IEnumerable<EBranch> Branches { get; }

        /// <summary> The preset's <see cref="IVehicle.EconomicRank"/>. </summary>
        public int EconomicRank { get; }

        /// <summary> The preset's battle rating. </summary>
        public string BattleRating { get; }

        #endregion Properties
        #region Constructors

        /// <summary> Cretes a new preset from the given list of vehicles. </summary>
        /// <param name="nation"> The preset's nation. </param>
        /// <param name="gameMode"> The preset's game mode. </param>
        /// <param name="mainBranch"> The preset's main branch. </param>
        /// <param name="economicRank"> The preset's <see cref="IVehicle.EconomicRank"/>. </param>
        /// <param name="battleRating"> The preset's battle rating. </param>
        /// <param name="vehicleList"> The vehicle list to use for initialisation. </param>
        public Preset(EGameMode gameMode, ENation nation, EBranch mainBranch, int economicRank, string battleRating, IList<IVehicle> vehicleList)
            : base(vehicleList)
        {
            GameMode = gameMode;
            Nation = nation;
            MainBranch = mainBranch;
            EconomicRank = economicRank;
            BattleRating = battleRating;
            Branches = this.Select(vehicle => vehicle.Branch).Distinct();
        }

        #endregion Constructors

        /// <summary> Returns the string representation of the collection. </summary>
        /// <returns></returns>
        public override string ToString() => $"Vehicle preset {(this.Any() ? $"for {this.First().Nation.AsEnumerationItem}" + Character.Space : string.Empty)}with {this.Count()} vehicles.";
    }
}