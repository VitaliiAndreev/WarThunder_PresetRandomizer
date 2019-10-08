using Core.DataBase.WarThunder.Enumerations;
using Core.Enumerations;
using Core.Extensions;

namespace Core.DataBase.WarThunder.Extensions
{
    /// <summary> Methods extending the <see cref="EVehicleClass"/> enumeration. </summary>
    public static class EVehicleClassExtensions
    {
        /// <summary> Returns the vehicle branch to which the class belongs to. </summary>
        /// <param name="vehicleClass"> The vehicle class whose branch to get. </param>
        /// <returns></returns>
        public static EBranch GetBranch(this EVehicleClass vehicleClass) =>
            vehicleClass
                .CastTo<int>()
                .Do(value => value / EInteger.Number.Ten)
                .CastTo<EBranch>()
            ;
    }
}