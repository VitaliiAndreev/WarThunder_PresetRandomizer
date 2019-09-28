using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Core.Organization.Collections
{
    /// <summary> A vehicle preset implemented as a <see cref="ReadOnlyCollection{T}"/> of <see cref="IVehicle"/>. This class' purpose if fluency and reference of selection made during randomization. </summary>
    public class Preset : ReadOnlyCollection<IVehicle>
    {
        #region Properties

        /// <summary> The preset's nation. </summary>
        public ENation Nation { get; }
        /// <summary> The preset's main branch. </summary>
        public EBranch MainBranch { get; }
        /// <summary> The preset's battle rating. </summary>
        public string BattleRating { get; }

        #endregion Properties
        #region Constructors

        /// <summary> Cretes a new preset from the given list of vehicles. </summary>
        /// <param name="nation"> The preset's nation. </param>
        /// <param name="mainBranch"> The preset's main branch. </param>
        /// <param name="battleRating"> The preset's battle rating. </param>
        /// <param name="vehicleList"> The vehicle list to use for initialization. </param>
        public Preset(ENation nation, EBranch mainBranch, string battleRating, IList<IVehicle> vehicleList)
            : base(vehicleList)
        {
            Nation = nation;
            MainBranch = mainBranch;
            BattleRating = battleRating;
        }

        #endregion Constructors

        /// <summary> Returns the string representation of the collection. </summary>
        /// <returns></returns>
        public override string ToString() => $"Vehicle preset {(this.Any() ? $"for {this.First().Nation.AsEnumerationItem}" : string.Empty) + ECharacter.Space}with {this.Count()} vehicles.";
    }
}