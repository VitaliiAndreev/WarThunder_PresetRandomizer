using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Core.Organization.Objects
{
    public struct BranchSet
    {
        #region Proeprties

        public IEnumerable<EBranch> Branches { get; }

        #endregion Proeprties
        #region Constructors

        public BranchSet(IEnumerable<EBranch> branches)
        {
            var loadedBranches = new List<EBranch>();

            var validBranches = branches.Where(branch => branch.IsValid());

            foreach (var branch in validBranches)
                loadedBranches.Add(branch);

            Branches = loadedBranches;
        }

        #endregion Constructors

        public override bool Equals(object obj)
        {
            return 
                obj is BranchSet other &&
                Equals(other);
        }

        public bool Equals(BranchSet other)
        {
            return Equals(Branches, other.Branches);
        }

        public override int GetHashCode()
        {
            return Branches != null
                ? Branches.GetHashCode()
                : 0;
        }
    }
}