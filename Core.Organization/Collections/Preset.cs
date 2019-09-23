using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Core.Organization.Collections
{
    /// <summary> A vehicle preset implemented as a <see cref="ReadOnlyCollection{T}"/> of <see cref="IVehicle"/>. This class' purpose if fluency. </summary>
    public class Preset : ReadOnlyCollection<IVehicle>
    {
        #region Constructors

        /// <summary> Cretes a new preset from the given list of vehicles. </summary>
        /// <param name="vehicleList"> The vehicle list to use for initialization. </param>
        public Preset(IList<IVehicle> vehicleList)
            : base(vehicleList)
        {
        }

        #endregion Constructors

        /// <summary> Returns the string representation of the collection. </summary>
        /// <returns></returns>
        public override string ToString() => $"Vehicle preset {(this.Any() ? $"for {this.First().Nation.AsEnumerationItem}" : string.Empty) + ECharacter.Space}with {this.Count()} vehicles.";
    }
}