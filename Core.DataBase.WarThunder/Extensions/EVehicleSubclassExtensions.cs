using Core.DataBase.WarThunder.Enumerations;
using Core.Enumerations;
using Core.Extensions;

namespace Core.DataBase.WarThunder.Extensions
{
    public static class EVehicleSubclassExtensions
    {
        /// <summary> Checks whether the vehicle class is valid. </summary>
        /// <param name="vehicleClass"> The vehicle class to check. </param>
        /// <returns></returns>
        public static bool IsValid(this EVehicleSubclass vehicleSubclass) =>
            vehicleSubclass.CastTo<int>() > EInteger.Number.NinetyNine;

        /// <summary> Returns the vehicle class to which the subclass belongs to. </summary>
        /// <param name="vehicleSubclass"> The vehicle subclass whose class to get. </param>
        /// <returns></returns>
        public static EVehicleClass GetVehicleClass(this EVehicleSubclass vehicleSubclass)
        {
            var vehicleSubclassEnumerationValue = vehicleSubclass.CastTo<int>();

            if (vehicleSubclass.IsValid())
                return vehicleSubclassEnumerationValue.Do(value => value / EInteger.Number.Ten).CastTo<EVehicleClass>();
            else
                return vehicleSubclassEnumerationValue.CastTo<EVehicleClass>();
        }
    }
}