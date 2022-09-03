using Core.DataBase.WarThunder.Enumerations;
using Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataBase.WarThunder.Extensions
{
    /// <summary> Methods extending the <see cref="EVehicleClass"/> enumeration. </summary>
    public static class EVehicleClassExtensions
    {
        /// <summary> Checks whether the vehicle class is valid. </summary>
        /// <param name="vehicleClass"> The vehicle class to check. </param>
        /// <returns></returns>
        public static bool IsValid(this EVehicleClass vehicleClass) =>
            vehicleClass.CastTo<int>() > 99 && !vehicleClass.ToString().StartsWith(EnumerationItem.All);

        /// <summary> Returns the vehicle branch to which the class belongs to. </summary>
        /// <param name="vehicleClass"> The vehicle class whose branch to get. </param>
        /// <returns></returns>
        public static EBranch GetBranch(this EVehicleClass vehicleClass) =>
            vehicleClass.Upcast<EVehicleClass, EBranch>();

        /// <summary> Returns vehicle classes which belong to the branch. </summary>
        /// <param name="vehicleClass"> The branch whose vehicle classes to get. </param>
        /// <param name="selectOnlyValidItems"> Whether to select only valid items. </param>
        /// <returns></returns>
        public static IEnumerable<EVehicleSubclass> GetVehicleSubclasses(this EVehicleClass vehicleClass, bool selectOnlyValidItems = true)
        {
            if (vehicleClass.IsValid())
            {
                return typeof(EVehicleSubclass)
                    .GetEnumerationItems<EVehicleSubclass>()
                    .Where(vehicleSubclass => vehicleSubclass.GetVehicleClass() == vehicleClass && (!selectOnlyValidItems || vehicleSubclass.IsValid()))
                ;
            }
            return new List<EVehicleSubclass>();
        }
    }
}