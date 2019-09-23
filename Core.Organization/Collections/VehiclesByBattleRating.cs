using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Core.Organization.Collections
{
    /// <summary> A collection of vehicles grouped by battle ratings, implemented as a <see cref="Dictionary{TKey, TValue}"/> of <see cref="decimal"/> and <see cref="IList{T}"/> of <see cref="IVehicle"/>. This class' purpose if fluency. </summary>
    public class VehiclesByBattleRating : Dictionary<decimal, IList<IVehicle>>
    {
        #region Constructors

        /// <summary> Cretes a new collection of vehicles grouped by battle ratings. </summary>
        /// <param name="sourceDictionary"> The donor dictionary. </param>
        public VehiclesByBattleRating(IDictionary<decimal, IList<IVehicle>> sourceDictionary)
            : base(sourceDictionary)
        {
        }

        #endregion Constructors

        /// <summary> Returns the string representation of the collection. </summary>
        /// <returns></returns>
        public override string ToString() => $"{Values.Count} battle ratings, {Values.Select(vehicles => vehicles.Count()).Sum()} vehicles.";
    }
}