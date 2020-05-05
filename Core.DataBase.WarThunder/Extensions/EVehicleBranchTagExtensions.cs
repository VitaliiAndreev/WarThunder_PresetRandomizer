using Core.DataBase.WarThunder.Enumerations;
using Core.Enumerations;
using Core.Extensions;

namespace Core.DataBase.WarThunder.Extensions
{
    /// <summary> Methods extending the <see cref="EVehicleBranchTag"/> enumeration. </summary>
    public static class EVehicleBranchTagExtensions
    {
        /// <summary> Checks whether the vehicle branch tag is valid. </summary>
        /// <param name="vehicleBranchTag"> The vehicle branch tag to check. </param>
        /// <returns></returns>
        public static bool IsValid(this EVehicleBranchTag vehicleBranchTag) =>
            vehicleBranchTag.CastTo<int>() > EInteger.Number.Nine;

        public static bool IsDefault(this EVehicleBranchTag tag) =>
            tag.ToString().Contains(EWord.Untagged);

        /// <summary> Returns the vehicle branch to which the branch tag belongs to. </summary>
        /// <param name="vehicleBranchTag"> The vehicle branch tag whose branch to get. </param>
        /// <returns></returns>
        public static EBranch GetBranch(this EVehicleBranchTag vehicleBranchTag)
        {
            var vehicleBranchTagEnumerationValue = vehicleBranchTag.CastTo<int>();

            if (vehicleBranchTag.IsValid())
                return vehicleBranchTagEnumerationValue.Do(value => value / EInteger.Number.Ten).CastTo<EBranch>();
            else
                return vehicleBranchTagEnumerationValue.CastTo<EBranch>();
        }
    }
}