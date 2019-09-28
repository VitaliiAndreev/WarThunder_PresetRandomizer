using Core.DataBase.WarThunder.Enumerations;
using System.Collections.Generic;

namespace Core.Organization.Objects.SearchSpecifications
{
    /// <summary> A specification used for filtering preferred items from collections before randomizing the former. </summary>
    public class NationSpecification
    {
        /// <summary> The nation. </summary>
        public ENation Nation { get; }
        /// <summary> Branches of the nation. </summary>
        public IEnumerable<EBranch> Branches { get; }
        /// <summary> Available crew slots for the nation. </summary>
        public int CrewSlots { get; }

        public NationSpecification(ENation nation, IEnumerable<EBranch> branches, int crewSlots)
        {
            Nation = nation;
            Branches = branches;
            CrewSlots = crewSlots;
        }
    }
}