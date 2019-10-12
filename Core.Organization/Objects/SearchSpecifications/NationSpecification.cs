using Core.DataBase.WarThunder.Enumerations;
using System.Collections.Generic;

namespace Core.Organization.Objects.SearchSpecifications
{
    /// <summary> A specification used for filtering preferred items from collections before randomizing the former. </summary>
    public class NationSpecification
    {
        #region Properties

        /// <summary> The nation. </summary>
        public ENation Nation { get; }

        /// <summary> Branches of the nation. </summary>
        public IEnumerable<EBranch> Branches { get; }

        /// <summary> Available crew slots for the nation. </summary>
        public int CrewSlots { get; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new filter specification with the given parameters. </summary>
        /// <param name="nation"> The nation. </param>
        /// <param name="branches"> Branches of the nation. </param>
        /// <param name="crewSlots"> Available crew slots for the nation. </param>
        public NationSpecification(ENation nation, IEnumerable<EBranch> branches, int crewSlots)
        {
            Nation = nation;
            Branches = branches;
            CrewSlots = crewSlots;
        }

        #endregion Constructors
    }
}