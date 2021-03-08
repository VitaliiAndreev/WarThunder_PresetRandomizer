using Core.DataBase.WarThunder.Enumerations;
using Core.Enumerations;
using Core.Extensions;

namespace Core.DataBase.WarThunder.Extensions
{
    /// <summary> Methods extending the <see cref="EVehicleBranchTag"/> enumeration. </summary>
    public static class EVehicleBranchTagExtensions
    {
        public static bool IsValid(this EVehicleBranchTag vehicleBranchTag) =>
            vehicleBranchTag.CastTo<int>() > EInteger.Number.Nine && !vehicleBranchTag.ToString().StartsWith(EWord.All);

        public static bool IsDefault(this EVehicleBranchTag tag) =>
            tag.ToString().Contains(EWord.Untagged);

        /// <summary> Returns the vehicle branch to which the branch tag belongs to. </summary>
        /// <param name="vehicleBranchTag"> The vehicle branch tag whose branch to get. </param>
        /// <returns></returns>
        public static EBranch GetBranch(this EVehicleBranchTag vehicleBranchTag) =>
            vehicleBranchTag.Upcast<EVehicleBranchTag, EBranch>();
    }
}