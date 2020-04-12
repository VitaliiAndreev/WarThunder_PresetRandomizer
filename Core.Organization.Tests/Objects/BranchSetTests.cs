using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.Extensions;
using Core.Organization.Objects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Core.Organization.Tests.Objects
{
    /// <summary> See <see cref="BranchSet"/>. </summary>
    [TestClass]
    public class BranchSetTests
    {
        #region Tests: Constuctor()

        [TestMethod]
        public void Constructor_1_Branch()
        {
            // arrange
            var branches = new List<EBranch> { EBranch.Army };

            // act
            var branchSet = new BranchSet(branches);

            // assert
            branchSet.Branches.Should().BeEquivalentTo(branches);
        }

        [TestMethod]
        public void Constructor_IgnoresInvalidBranches()
        {
            // arrange
            var branches = typeof(EBranch).GetEnumerationItems<EBranch>();
            var validBranches = branches.Where(branch => branch.IsValid());

            // act
            var branchSet = new BranchSet(branches);

            // assert
            branchSet.Branches.Should().BeEquivalentTo(validBranches);
        }

        #endregion Tests: Constuctor()
        #region Tests: GetHashCode()

        [TestMethod]
        public void GetHashCode_EmptySets_ShouldBeEqual()
        {
            // arrange
            var branchesA = new List<EBranch>();
            var branchesB = new List<EBranch>();
            var branchSetA = new BranchSet(branchesA);
            var branchSetB = new BranchSet(branchesB);

            // act
            var setsAreEqual = branchSetA.GetHashCode() == branchSetB.GetHashCode();

            // assert
            setsAreEqual.Should().BeTrue();
        }

        [TestMethod]
        public void GetHashCode_DifferentBranchOrder_ShouldBeEqual()
        {
            // arrange
            var branchesA = new List<EBranch> { EBranch.Army, EBranch.Aviation };
            var branchesB = new List<EBranch> { EBranch.Aviation, EBranch.Army };
            var branchSetA = new BranchSet(branchesA);
            var branchSetB = new BranchSet(branchesB);

            // act
            var setsAreEqual = branchSetA.GetHashCode() == branchSetB.GetHashCode();

            // assert
            setsAreEqual.Should().BeTrue();
        }

        #endregion Tests: GetHashCode()
    }
}
