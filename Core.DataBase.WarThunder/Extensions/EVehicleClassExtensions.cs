using Core.DataBase.WarThunder.Enumerations;
using Core.Enumerations;
using Core.Extensions;

namespace Core.DataBase.WarThunder.Extensions
{
    /// <summary> Methods extending the <see cref="EVehicleClass"/> enumeration. </summary>
    public static class EVehicleClassExtensions
    {
        /// <summary> Checks whether the vehicle class is valid. </summary>
        /// <param name="vehicleClass"> The vehicle class to check. </param>
        /// <returns></returns>
        public static bool IsValid(this EVehicleClass vehicleClass) =>
            vehicleClass.CastTo<int>() > EInteger.Number.Nine;

        /// <summary> Returns the vehicle branch to which the class belongs to. </summary>
        /// <param name="vehicleClass"> The vehicle class whose branch to get. </param>
        /// <returns></returns>
        public static EBranch GetBranch(this EVehicleClass vehicleClass)
        {
            var vehicleClassEnumerationValue = vehicleClass.CastTo<int>();

            if (vehicleClass.IsValid())
                return vehicleClassEnumerationValue.Do(value => value / EInteger.Number.Ten).CastTo<EBranch>();
            else
                return vehicleClassEnumerationValue.CastTo<EBranch>();
        }
    }
}