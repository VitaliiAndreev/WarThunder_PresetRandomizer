using Core.DataBase.WarThunder.Enumerations;
using Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataBase.WarThunder.Extensions
{
    /// <summary> Methods extending the <see cref="EBranch"/> enumeration. </summary>
    public static class EBranchExtensions
    {
        /// <summary> Checks whether the branch is valid. </summary>
        /// <param name="branch"> The branch to check. </param>
        /// <returns></returns>
        public static bool IsValid(this EBranch branch) =>
            branch.EnumerationItemValueIsPositive();

        /// <summary> Returns vehicle branch tags which belong to the branch. </summary>
        /// <param name="branch"> The branch whose vehicle branch tags to get. </param>
        /// <param name="selectOnlyValidItems"> Whether to select only valid items. </param>
        /// <returns></returns>
        public static IEnumerable<EVehicleBranchTag> GetVehicleBranchTags(this EBranch branch, bool selectOnlyValidItems = true) =>
            typeof(EVehicleBranchTag)
                .GetEnumerationItems<EVehicleBranchTag>()
                .Where(vehicleBranchTag => vehicleBranchTag.GetBranch() == branch && (selectOnlyValidItems ? vehicleBranchTag.IsValid() : true))
            ;

        /// <summary> Returns vehicle classes which belong to the branch. </summary>
        /// <param name="branch"> The branch whose vehicle classes to get. </param>
        /// <param name="selectOnlyValidItems"> Whether to select only valid items. </param>
        /// <returns></returns>
        public static IEnumerable<EVehicleClass> GetVehicleClasses(this EBranch branch, bool selectOnlyValidItems = true) =>
            typeof(EVehicleClass)
                .GetEnumerationItems<EVehicleClass>()
                .Where(vehicleClass => vehicleClass.GetBranch() == branch && (selectOnlyValidItems ? vehicleClass.IsValid() : true))
            ;

        /// <summary> Returns the enumeration item representing selection of all vehicle types under the given branch. </summary>
        /// <param name="branch"> The branch whose vehicle classes to get. </param>
        /// <returns></returns>
        public static EVehicleClass GetAllVehicleClassesItem(this EBranch branch) =>
            branch.CastTo<int>().CastTo<EVehicleClass>();
    }
}