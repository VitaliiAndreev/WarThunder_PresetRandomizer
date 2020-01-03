using Core.DataBase.WarThunder.Enumerations;
using Core.Enumerations;

namespace Core.DataBase.WarThunder.Objects
{
    public class NationBranchPair
    {
        #region Properties

        /// <summary> The nation. </summary>
        public ENation Nation { get; private set; }

        /// <summary> The branch. </summary>
        public EBranch Branch { get; private set; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new nation-branch pair. </summary>
        /// <param name="nation"> The nation. </param>
        /// <param name="branch"> The branch. </param>
        public NationBranchPair(ENation nation, EBranch branch)
        {
            Initialize(nation, branch);
        }

        #endregion Constructors
        #region Methods: Initialization

        /// <summary> Initializes the class with the given values. </summary>
        /// <param name="nation"> The nation. </param>
        /// <param name="branch"> The branch. </param>
        private void Initialize(ENation nation, EBranch branch)
        {
            Nation = nation;
            Branch = branch;
        }

        #endregion Methods: Initialization
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
                var hash = 101;

                hash = hash * 103 + Nation.GetHashCode();
                hash = hash * 107 + Branch.GetHashCode();

                return hash;
            }
        }

        #endregion Methods: Equality Comparison

        /// <summary> Returns a string that represents the instance. </summary>
        /// <returns></returns>
        public override string ToString() => $"{Nation}{ECharacter.Underscore}{Branch}";
    }
}