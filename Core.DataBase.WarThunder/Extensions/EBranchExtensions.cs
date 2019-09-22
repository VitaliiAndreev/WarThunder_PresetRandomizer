using Core.DataBase.WarThunder.Enumerations;
using Core.Extensions;

namespace Core.DataBase.WarThunder.Extensions
{
    /// <summary> Methods extending the <see cref="EBranch"/> enumeration. </summary>
    public static class EBranchExtensions
    {
        /// <summary> Gets the amount of branches in the branch set. </summary>
        /// <param name="branchBitField"> The branch set. </param>
        /// <returns></returns>
        public static int GetItemCount(this EBranch branchBitField) =>
            branchBitField.CastTo<int>().GetBitCount();
    }
}