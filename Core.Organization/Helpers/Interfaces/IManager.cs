﻿using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Organization.Collections;
using Core.Organization.Enumerations;
using Core.Organization.Objects;
using Core.Organization.Objects.SearchSpecifications;
using System;
using System.Collections.Generic;

namespace Core.Organization.Helpers.Interfaces
{
    /// <summary> Controls the flow of the application. </summary>
    public interface IManager : IDisposable
    {
        #region Properties

        bool ShowThunderSkillData { get; }

        /// <summary> Research trees. This collection needs to be filled up after caching vehicles up from the database by calling <see cref="CacheData"/>. </summary>
        IDictionary<ENation, ResearchTree> ResearchTrees { get; }

        /// <summary> Playable vehicles loaded into memory. </summary>
        IDictionary<string, IVehicle> PlayableVehicles { get; }

        IDictionary<EGameMode, IDictionary<EBranch, IDictionary<int, int>>> EconomicRankUsage { get; }

        #endregion Properties
        #region Methods: Initialization

        /// <summary> Reads and stores the version of the game client. </summary>
        void InitialiseGameClientVersion();

        /// <summary> Caches vehicles from the database in runtime memory. </summary>
        void CacheData();

        #endregion Methods: Initialization

        /// <summary> Removes log files older than a week. </summary>
        void RemoveOldLogFiles();

        /// <summary> Generates two vehicle presets (primary and fallback) based on the given specification. </summary>
        /// <param name="specification"> The specification to base vehicle selection on. </param>
        /// <returns></returns>
        IDictionary<EPreset, Preset> GeneratePrimaryAndFallbackPresets(Specification specification);
    }
}