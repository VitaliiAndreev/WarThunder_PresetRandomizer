using Core.DataBase.WarThunder.Enumerations;
using System.Collections.Generic;

namespace Core.Organization.Objects.SearchSpecifications
{
    /// <summary> A specification used for filtering preferred items from collections before randomizing the former. </summary>
    public class BranchSpecification
    {
        #region Properties

        /// <summary> The branch. </summary>
        public EBranch Branch { get; }

        /// <summary> Vehicle classes in the branch. </summary>
        public IEnumerable<EVehicleClass> VehicleClasses { get; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new filter specification with the given parameters. </summary>
        /// <param name="branch"> The branch. </param>
        /// <param name="vehicleClasses"> Vehicle classes in the branch. </param>
        public BranchSpecification(EBranch branch, IEnumerable<EVehicleClass> vehicleClasses)
        {
            Branch = branch;
            VehicleClasses = vehicleClasses;
        }

        #endregion Constructors
    }
}