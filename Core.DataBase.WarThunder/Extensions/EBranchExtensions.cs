using Core.DataBase.WarThunder.Enumerations;
using Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataBase.WarThunder.Extensions
{
    public static class EBranchExtensions
    {
        public static bool IsValid(this EBranch branch) =>
            branch.CastTo<int>() > 9 && !branch.ToString().StartsWith(Word.All);

        public static int GetSingleDigitCode(this EBranch branch)
        {
            return branch switch
            {
                EBranch.Army => 1,
                EBranch.Helicopters => 2,
                EBranch.Aviation => 3,
                EBranch.BluewaterFleet => 4,
                EBranch.CoastalFleet => 5,
                _ => -1,
            };
        }

        public static EBranch FromSingleDigitCode(this int code)
        {
            return code switch
            {
                1 => EBranch.Army,
                2 => EBranch.Helicopters,
                3 => EBranch.Aviation,
                4 => EBranch.BluewaterFleet,
                5 => EBranch.CoastalFleet,
                _ => EBranch.None,
            };
        }

        public static EVehicleCategory GetVehicleCategory(this EBranch branch) =>
            branch.Upcast<EBranch, EVehicleCategory>();

        public static IEnumerable<EVehicleClass> GetVehicleClasses(this EBranch branch, bool selectOnlyValidItems = true) =>
            typeof(EVehicleClass)
                .GetEnumerationItems<EVehicleClass>()
                .Where(vehicleClass => EVehicleClassExtensions.GetBranch(vehicleClass) == branch && (!selectOnlyValidItems || vehicleClass.IsValid()));

        public static EVehicleClass GetAllVehicleClassesItem(this EBranch branch) =>
            branch != EBranch.None
                ? branch.Downcast<EBranch, EVehicleClass>()
                : EVehicleClass.None;
    }
}