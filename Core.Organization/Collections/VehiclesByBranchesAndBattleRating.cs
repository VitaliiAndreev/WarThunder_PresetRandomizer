using Core.DataBase.WarThunder.Enumerations;
using System.Collections.Generic;
using System.Linq;

namespace Core.Organization.Collections
{
    /// <summary> A collection of vehicles grouped by branches and battle ratings, implemented as a <see cref="Dictionary{TKey, TValue}"/> of <see cref="EBranch"/> and <see cref="VehiclesByBattleRating"/>. This class' purpose if fluency. </summary>
    public class VehiclesByBranchesAndBattleRating : Dictionary<EBranch, VehiclesByBattleRating>
    {
        #region Constructors

        /// <summary> Cretes a new collection of vehicles grouped by branches and battle ratings. </summary>
        /// <param name="sourceDictionary"> The donor dictionary. </param>
        public VehiclesByBranchesAndBattleRating(IDictionary<EBranch, VehiclesByBattleRating> sourceDictionary)
            : base(sourceDictionary)
        {
        }

        #endregion Constructors

        /// <summary> Returns the string representation of the collection. </summary>
        /// <returns></returns>
        public override string ToString() => $"{Values.Count()} branches, {Values.Select(vehiclesByBattleRating => vehiclesByBattleRating.Values.Select(vehicles => vehicles.Count()).Sum()).Sum()} vehicles.";
    }
}