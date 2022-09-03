using Core.DataBase.WarThunder.Enumerations;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataBase.WarThunder.Extensions
{
    /// <summary> Methods extending the <see cref="EVehicleCategory"/> enumeration. </summary>
    public static class EVehicleCategoryExtensions
    {
        public static bool IsValid(this EVehicleCategory category) =>
            category.ValueIsPositive();

        public static IEnumerable<EVehicleBranchTag> GetVehicleBranchTags(this EBranch branch, bool selectOnlyValidItems = true) =>
            typeof(EVehicleBranchTag)
                .GetEnumerationItems<EVehicleBranchTag>()
                .Where(vehicleClass => EVehicleBranchTagExtensions.GetBranch(vehicleClass) == branch && (!selectOnlyValidItems || vehicleClass.IsValid()));
    }
}