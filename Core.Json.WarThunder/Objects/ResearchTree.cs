﻿using Core.DataBase.WarThunder.Objects.Json;
using System.Collections;
using System.Collections.Generic;

namespace Core.Json.WarThunder.Objects
{
    /// <summary> A research tree. </summary>
    public class ResearchTree : IEnumerable<ResearchTreeBranch>
    {
        #region Properties

        /// <summary> The Gaijin ID of the research tree's nation. </summary>
        public string NationGaijinId { get; }

        /// <summary> Research tree branches comprising the tree. </summary>
        public IList<ResearchTreeBranch> Branches { get; }

        /// <summary> All vehicles postioned in the tree. </summary>
        public IEnumerable<ResearchTreeVehicleFromJson> Vehicles
        {
            get
            {
                var vehicles = new List<ResearchTreeVehicleFromJson>();

                foreach (var branch in Branches)
                    vehicles.AddRange(branch.Vehicles);

                return vehicles;
            }
        }

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new research tree. </summary>
        /// <param name="nationGaijinId"> The Gaijin ID of the research tree's nation. </param>
        public ResearchTree(string nationGaijinId)
        {
            NationGaijinId = nationGaijinId;
            Branches = new List<ResearchTreeBranch>();
        }

        #endregion Constructors

        public IEnumerator<ResearchTreeBranch> GetEnumerator() => Branches.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}