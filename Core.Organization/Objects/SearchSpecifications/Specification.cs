﻿using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Collections.Generic;

namespace Core.Organization.Objects.SearchSpecifications
{
    /// <summary> A specification used for filtering preferred items from collections before randomizing the former. </summary>
    public class Specification
    {
        /// <summary> The game mode. </summary>
        public EGameMode GameMode { get; }
        /// <summary> Allowed nations. </summary>
        public IEnumerable<ENation> Nations { get; }
        /// <summary> Allowed branches. </summary>
        public IEnumerable<EBranch> Branches { get; }
        /// <summary> Allowed values of <see cref="IVehicle.EconomicRank"/>. </summary>
        public IEnumerable<int> EconomicRanks { get; }

        /// <summary> Creates a new filter specification with the given parameters. </summary>
        /// <param name="gameMode"> The game mode. </param>
        /// <param name="nationCrewSlots"> Allowed nations. </param>
        /// <param name="branches"> Allowed branches. </param>
        /// <param name="economicRanks"> Allowed values of <see cref="IVehicle.EconomicRank"/>. </param>
        public Specification(EGameMode gameMode, IEnumerable<ENation> nations, IEnumerable<EBranch> branches, IEnumerable<int> economicRanks)
        {
            GameMode = gameMode;
            Nations = nations;
            Branches = branches;
            EconomicRanks = economicRanks;
        }
    }
}