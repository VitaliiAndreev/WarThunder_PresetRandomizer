using Core.DataBase.WarThunder.Enumerations;
using Core.Extensions;
using System.Linq;

namespace Core.DataBase.WarThunder.Objects.Connectors
{
    public class NationBranchPair
    {
        #region Properties

        /// <summary> The nation. </summary>
        public ENation Nation { get; }

        /// <summary> The branch. </summary>
        public EBranch Branch { get; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new nation-branch pair. </summary>
        /// <param name="nation"> The nation. </param>
        /// <param name="branch"> The branch. </param>
        public NationBranchPair(ENation nation, EBranch branch)
        {
            Nation = nation;
            Branch = branch;
        }

        #endregion Constructors
        #region Methods: Equality Comparison

        /// <summary> Determines whether the specified object is equal to the current object. </summary>
        /// <param name="obj"> The object to compare with the current object. </param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is NationBranchPair otherPair))
                return false;

            return Nation.Equals(otherPair.Nation)
                && Branch.Equals(otherPair.Branch);
        }

        /// <summary> Serves as the default hash function. </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hash = Integer.Number.PrimesAboveHundred.First();

                hash = hash * Integer.Number.PrimesAboveHundred.Second() + Nation.GetHashCode();
                hash = hash * Integer.Number.PrimesAboveHundred.Third() + Branch.GetHashCode();

                return hash;
            }
        }

        #endregion Methods: Equality Comparison
        
        public override string ToString() => $"{Nation}_{Branch}";
    }
}