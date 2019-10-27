using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Objects;
using System.Collections.Generic;

namespace Core.Organization.Objects.SearchSpecifications
{
    /// <summary> A specification used for filtering preferred items from collections before randomizing the former. </summary>
    public class Specification
    {
        #region Properties

        /// <summary> The game mode. </summary>
        public EGameMode GameMode { get; }

        /// <summary> Nation specifications. </summary>
        public IDictionary<ENation, NationSpecification> NationSpecifications { get; }

        /// <summary> Branch specifications. </summary>
        public IDictionary<EBranch, BranchSpecification> BranchSpecifications { get; }

        /// <summary> Allowed intervals of <see cref="IVehicle.EconomicRank"/>s. </summary>
        public IDictionary<ENation, Interval<int>> EconomicRankIntervals { get; }

        /// <summary> Allowed vehicle Gaijin IDs. </summary>
        public IEnumerable<string> VehicleGaijinIds { get; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new filter specification with the given parameters. </summary>
        /// <param name="gameMode"> The game mode. </param>
        /// <param name="nationSpecifications"> Nation specifications. </param>
        /// <param name="branchSpecifications"> Branch specifications. </param>
        /// <param name="economicRankIntervals"> Allowed values of <see cref="IVehicle.EconomicRank"/>. </param>
        /// <param name="vehicleGaijinIds"> Allowed vehicle Gaijin IDs. </param>
        public Specification
        (
            EGameMode gameMode,
            IDictionary<ENation, NationSpecification> nationSpecifications,
            IDictionary<EBranch, BranchSpecification> branchSpecifications,
            IDictionary<ENation, Interval<int>> economicRankIntervals,
            IEnumerable<string> vehicleGaijinIds
        )
        {
            GameMode = gameMode;
            NationSpecifications = nationSpecifications;
            BranchSpecifications = branchSpecifications;
            EconomicRankIntervals = economicRankIntervals;
            VehicleGaijinIds = vehicleGaijinIds;
        }

        #endregion Constructors
    }
}