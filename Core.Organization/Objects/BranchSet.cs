using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.Enumerations;
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
        #region Methods: Overrides

        public override int GetHashCode()
        {
            var branchesIncluded = Branches.OrderBy(branch => branch).ToList();
            var primes = EInteger.Number.PrimesAboveHundred.ToList();
            var hashCode = EInteger.Number.One;

            for (var index = EInteger.Number.Zero; index < Branches.Count(); index++)
            {
                var prime = primes[index];
                var branch = branchesIncluded[index];

                hashCode *= prime + branch.GetHashCode();
            }

            return hashCode;
        }

        #endregion Methods: Overrides
    }
}