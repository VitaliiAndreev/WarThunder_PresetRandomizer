using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Helpers;
using Core.DataBase.WarThunder.Objects;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.VehicleGameModeParameterSet.String;
using Core.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using Core.Objects;
using Core.Organization.Collections;
using Core.Organization.Enumerations;
using Core.Organization.Enumerations.Logger;
using Core.Organization.Extensions;
using Core.Organization.Helpers.Interfaces;
using Core.Organization.Objects.SearchSpecifications;
using Core.Randomization.Enumerations;
using Core.Randomization.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Organization.Helpers
{
    public class PresetGenerator : LoggerFluency, IPresetGenerator
    {
        #region Constants

        /// <summary>
        /// The maximum difference in battle rating from the battle rating selected by user.
        /// <para> Example: if the difference is 1 and the user chooses 5.7, vehicles of 4.7-5.7 are selected. </para>
        /// </summary>
        private const decimal _maximumBattleRatingDifference = 2.0m;

        #endregion Constants
        #region Fields

        /// <summary> Playable vehicles. </summary>
        private IEnumerable<IVehicle> _playableVehicles;

        /// <summary> An instance of a randomiser. </summary>
        protected readonly IRandomiser _randomiser;
        /// <summary> An instance of a vehicle selector. </summary>
        protected readonly IVehicleSelector _vehicleSelector;

        #endregion Fields
        #region Constructors

        /// <summary> Creates a new randomizer. </summary>
        /// <param name="randomiser"> An instance of a randomiser. </param>
        /// <param name="loggers"> Instances of loggers. </param>
        public PresetGenerator(IRandomiser randomiser, IVehicleSelector vehicleSelector, params IConfiguredLogger[] loggers)
            : base(EOrganizationLogCategory.PresetGenerator, loggers)
        {
            _randomiser = randomiser;
            _vehicleSelector = vehicleSelector;

            LogDebug(ECoreLogMessage.Created.FormatFluently(EOrganizationLogCategory.PresetGenerator));
        }

        #endregion Constructors

        public void SetPlayableVehicles(IEnumerable<IVehicle> vehicles)
        {
            _playableVehicles = vehicles;
        }

        #region Methods: Helper Methods for GeneratePrimaryAndFallbackPresets()

        /// <summary> Filters <see cref="_playableVehicles"/> with <paramref name="vehicleGaijinIds"/>. </summary>
        /// <param name="vehicleGaijinIds"> Gaijin IDs of vehicles enabled via GUI. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> FilterVehiclesByGaijinIds(IEnumerable<string> vehicleGaijinIds)
        {
            var filteredVehicles = _playableVehicles?.Where(vehicle => vehicle.GaijinId.IsIn(vehicleGaijinIds));

            if (filteredVehicles is null || filteredVehicles.IsEmpty())
            {
                LogWarn(EOrganizationLogMessage.NoVehiclesSelected);
                return null;
            }
            return filteredVehicles;
        }

        /// <summary> Assesses <paramref name="filteredVehicles"/> does post-filtering operations. </summary>
        /// <param name="filteredVehicles"> Filtered vehicles to assess. </param>
        /// <param name="validItems"> Items accepted by the filter. </param>
        /// <param name="suppressLogging"> Whether to log empty results. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> AssessFilterResult<T>(IEnumerable<IVehicle> filteredVehicles, IEnumerable<T> validItems, bool suppressLogging)
        {
            if (filteredVehicles is null || filteredVehicles.IsEmpty())
            {
                if (!suppressLogging)
                    LogWarn(EOrganizationLogMessage.NoVehiclesAvailableFor.FormatFluently(validItems.StringJoin(ESeparator.CommaAndSpace)));

                return null;
            }
            return filteredVehicles;
        }

        /// <summary>
        /// Filters given <paramref name="vehicles"/> by <see cref="IVehicle.AircraftTags"/> and <paramref name="validTags"/>.
        /// If emptiness of the resulting collection is logged elsewhere, it's possible to <paramref name="suppressLogging"/>.
        /// </summary>
        /// <param name="vehicles"> Vehicles to filter through. </param>
        /// <param name="validTags"> Vehicle branch tags accepted by the filter. </param>
        /// <param name="suppressLogging"> Whether to log empty results. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> FilterVehiclesByAircraftTags(IEnumerable<IVehicle> vehicles, IEnumerable<EVehicleBranchTag> validTags, bool suppressLogging = false)
        {
            var filteredVehicles = vehicles.Where(vehicle => vehicle.AircraftTags?[validTags] ?? true);

            return AssessFilterResult(filteredVehicles, validTags, suppressLogging);
        }

        /// <summary>
        /// Filters given <paramref name="vehicles"/> based on the <paramref name="itemSelector"/> and <paramref name="validItems"/>.
        /// If emptiness of the resulting collection is logged elsewhere, it's possible to <paramref name="suppressLogging"/>.
        /// </summary>
        /// <typeparam name="T"> The type of filter criteria items. </typeparam>
        /// <param name="vehicles"> Vehicles to filter through. </param>
        /// <param name="validItems"> Items accepted by the filter. </param>
        /// <param name="itemSelector"> The selector function that extracts an evaluated value from given <paramref name="vehicles"/>. </param>
        /// <param name="suppressLogging"> Whether to log empty results. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> FilterVehicles<T>(IEnumerable<IVehicle> vehicles, IEnumerable<T> validItems, Func<IVehicle, T> itemSelector, bool suppressLogging = false)
        {
            var filteredVehicles = vehicles?.Where(vehicle => itemSelector(vehicle).IsIn(validItems)) ?? new List<IVehicle>();

            return AssessFilterResult(filteredVehicles, validItems, suppressLogging);
        }

        /// <summary>
        /// Filters given <paramref name="vehicles"/> based on the <paramref name="itemSelector"/> and <paramref name="validItems"/>.
        /// If emptiness of the resulting collection is logged elsewhere, it's possible to <paramref name="suppressLogging"/>.
        /// </summary>
        /// <typeparam name="T"> The type of filter criteria items. </typeparam>
        /// <param name="vehicles"> Vehicles to filter through. </param>
        /// <param name="validItems"> Items accepted by the filter. </param>
        /// <param name="itemSelector"> The selector function that extracts evaluated values from given <paramref name="vehicles"/>. </param>
        /// <param name="suppressLogging"> Whether to log empty results. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> FilterVehicles<T>(IEnumerable<IVehicle> vehicles, IEnumerable<T> validItems, Func<IVehicle, IEnumerable<T>> itemSelector, bool suppressLogging = false)
        {
            var filteredVehicles = vehicles.Where(vehicle => itemSelector(vehicle).Intersect(validItems).Any());

            return AssessFilterResult(filteredVehicles, validItems, suppressLogging);
        }

        /// <summary> Filters <paramref name="vehicles"/> with <paramref name="enabledNationCountryPairs"/>. </summary>
        /// <param name="enabledNationCountryPairs"> Nation-country pairs enabled via GUI. </param>
        /// <param name="vehicles"> Vehicles to filter. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> FilterVehiclesByCountries(IEnumerable<NationCountryPair> enabledNationCountryPairs, IEnumerable<IVehicle> vehicles) =>
            FilterVehicles(vehicles, enabledNationCountryPairs, vehicle => new NationCountryPair(vehicle.Nation.AsEnumerationItem, vehicle.Country));

        /// <summary> Filters <paramref name="vehicles"/> with <paramref name="validVehicleClasses"/>. </summary>
        /// <param name="validVehicleClasses"> Vehicle classes enabled via GUI and actually available. </param>
        /// <param name="vehicles"> Vehicles to filter. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> FilterVehiclesByClassFilters(IEnumerable<EVehicleClass> validVehicleClasses, IEnumerable<IVehicle> vehicles) =>
            FilterVehicles(vehicles, validVehicleClasses, vehicle => vehicle.Class);

        /// <summary> Filters <paramref name="vehicles"/> with <paramref name="validVehicleSubclasses"/>. </summary>
        /// <param name="validVehicleSubclasses"> Vehicle subclasses enabled via GUI and actually available. </param>
        /// <param name="vehicles"> Vehicles to filter. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> FilterVehiclesBySubclassFilters(IEnumerable<EVehicleSubclass> validVehicleSubclasses, IEnumerable<IVehicle> vehicles) =>
            FilterVehicles(vehicles, validVehicleSubclasses, vehicle => vehicle.Subclasses.All);

        /// <summary> Filters <paramref name="vehicles"/> with <paramref name="validBranches"/>. </summary>
        /// <param name="validBranches"> Vehicle branches enabled via GUI and actually available. </param>
        /// <param name="vehicles"> Vehicles to filter. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> FilterVehiclesByBranches(IEnumerable<EBranch> validBranches, IEnumerable<IVehicle> vehicles) =>
            FilterVehicles(vehicles, validBranches, vehicle => vehicle.Branch.AsEnumerationItem);

        /// <summary> Filters <paramref name="vehicles"/> with <paramref name="validNations"/>. </summary>
        /// <param name="validNations"> Vehicle classes enabled via GUI and actually available. </param>
        /// <param name="vehicles"> Vehicles to filter. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> FilterVehiclesByNations(IEnumerable<ENation> validNations, IEnumerable<IVehicle> vehicles) =>
            FilterVehicles(vehicles, validNations, vehicle => vehicle.Nation.AsEnumerationItem);

        /// <summary> Filters <paramref name="vehicles"/> with <paramref name="ranks"/>. </summary>
        /// <param name="ranks"> Ranks enabled via GUI. </param>
        /// <param name="vehicles"> Vehicles to filter. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> FilterVehiclesByRanks(IEnumerable<ERank> ranks, IEnumerable<IVehicle> vehicles) =>
            FilterVehicles(vehicles, ranks, vehicle => vehicle.RankAsEnumerationItem);

        /// <summary> Filters <paramref name="vehicles"/> with <paramref name="validEconomicRanksNations"/>. </summary>
        /// <param name="validEconomicRanksNations"> Vehicle classes enabled via GUI and actually available. </param>
        /// <param name="vehicles"> Vehicles to filter. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> FilterVehiclesByEconomicRanks(EGameMode gameMode, IDictionary<ENation, IEnumerable<IVehicle>> vehiclesByNations, IDictionary<ENation, IEnumerable<int>> validEconomicRanks)
        {
            var vehiclesWithEconomocRankFilter = new List<IVehicle>();

            foreach (var nationKeyValuePair in validEconomicRanks)
            {
                var nation = nationKeyValuePair.Key;
                var economicRanks = nationKeyValuePair.Value;

                vehiclesWithEconomocRankFilter.AddRange(FilterVehiclesByEconomicRanks(gameMode, economicRanks, vehiclesByNations[nation]));
            }

            if (vehiclesWithEconomocRankFilter.IsEmpty())
            {
                var parameterString = validEconomicRanks
                    .Select(keyValuePair => $"{keyValuePair.Key}: {keyValuePair.Value.StringJoin(ESeparator.CommaAndSpace)}")
                    .StringJoin(ESeparator.VerticalBarAndSpace)
                ;

                LogWarn(EOrganizationLogMessage.NoVehiclesAvailableFor.FormatFluently(parameterString));
                return null;
            }

            return vehiclesWithEconomocRankFilter;
        }

        /// <summary> Filters <paramref name="vehicles"/> with <paramref name="validEconomicRanksNations"/>. </summary>
        /// <param name="validEconomicRanksNations"> Vehicle classes enabled via GUI and actually available. </param>
        /// <param name="vehicles"> Vehicles to filter. </param>
        /// <returns></returns>
        private IEnumerable<IVehicle> FilterVehiclesByEconomicRanks(EGameMode gameMode, IEnumerable<int> validEconomicRanksNations, IEnumerable<IVehicle> vehicles) =>
            FilterVehicles(vehicles, validEconomicRanksNations, vehicle => vehicle.EconomicRank.AsDictionary()[gameMode].Value, true);

        /// <summary> Selects the main branch from selected via GUI (passed with <paramref name="specification"/>) and <paramref name="availableBranches"/>. </summary>
        /// <param name="specification"> The search specification. </param>
        /// <param name="availableBranches"> Branches available after filtering with vehicle classes. </param>
        /// <returns></returns>
        private EBranch SelectMainBranch(Specification specification, IEnumerable<EBranch> availableBranches)
        {
            var mainBranch = _randomiser.GetRandom(specification.BranchSpecifications.Keys.Where(branch => branch.IsIn(availableBranches)), ERandomisationStep.MainBranchWhenSelectingByCategories);

            LogDebug(ECoreLogMessage.Selected.FormatFluently(mainBranch));

            if (mainBranch == EBranch.None)
            {
                LogWarn(EOrganizationLogMessage.NoVehiclesAvailableFor.FormatFluently(availableBranches.StringJoin(ESeparator.CommaAndSpace)));
                return EBranch.None;
            }
            return mainBranch;
        }

        /// <summary> Selects the nation specification from <paramref name="specification"/> based on previously selected <paramref name="mainBranch"/> and <paramref name="availableNations"/>. </summary>
        /// <param name="specification"> The search specification. </param>
        /// <param name="mainBranch"> The main vehicle branch. </param>
        /// <param name="availableNations"> Nations available after filtering with vehicle classes. </param>
        /// <returns></returns>
        private NationSpecification SelectNationSpecification(Specification specification, EBranch mainBranch, IEnumerable<ENation> availableNations)
        {
            var nationSpecificationsWithValidBranches = specification.NationSpecifications.Values.Where(nationSpecification => nationSpecification.Branches.Contains(mainBranch));

            if (nationSpecificationsWithValidBranches.IsEmpty())
            {
                LogWarn(EOrganizationLogMessage.NationsHaveNoBranch.FormatFluently(specification.NationSpecifications.Values.Select(nationSpecification => nationSpecification.Nation).StringJoin(ESeparator.CommaAndSpace), mainBranch));
                return null;
            }

            var nationSpecification = _randomiser.GetRandom(nationSpecificationsWithValidBranches.Where(nationSpecification => nationSpecification.Nation.IsIn(availableNations)));

            if (nationSpecification is null)
                LogWarn(EOrganizationLogMessage.NoVehiclesAvailableFor.FormatFluently(availableNations.StringJoin(ESeparator.CommaAndSpace)));

            return nationSpecification;
        }

        /// <summary> Selects the nation from <paramref name="nationSpecification"/>. </summary>
        /// <param name="nationSpecification"> The previously selected nation specification. </param>
        /// <returns></returns>
        private ENation SelectNation(NationSpecification nationSpecification)
        {
            var nation = nationSpecification.Nation;

            LogDebug(ECoreLogMessage.Selected.FormatFluently(nation));

            return nation;
        }

        /// <summary> Selects valid branches from <paramref name="vehiclesByBranches"/>. </summary>
        /// <param name="vehiclesByBranches">
        /// Vehicles filtered by <see cref="SelectVehiclesWithCountryFilter(IEnumerable{NationCountryPair})"/>,
        /// <see cref="FilterVehiclesByClassFilters(IEnumerable{EVehicleClass}, IEnumerable{IVehicle})"/>, and by the selected nation.
        /// </param>
        /// <returns></returns>
        private IEnumerable<EBranch> GetValidBranches(IDictionary<EBranch, IEnumerable<IVehicle>> vehiclesByBranches)
        {
            var validBranches = vehiclesByBranches.Keys;

            if (validBranches.IsEmpty())
            {
                LogWarn(EOrganizationLogMessage.NoVehiclesAvailableFor.FormatFluently(validBranches.StringJoin(ESeparator.CommaAndSpace)));
                return null;
            }
            return validBranches;
        }

        /// <summary> Selects valid economic ranks from <paramref name="enabledEconomicRanks"/> based on <paramref name="economicRanksWithVehicles"/></summary>
        /// <param name="enabledEconomicRanks"> Economic ranks enabled via GUI (as battle ratings). </param>
        /// <param name="economicRanksWithVehicles"> Economic ranks that have vehicles after application of previous filters. </param>
        /// <param name="getFormattedBattleRating"> A function to get a formatted battle rating from an economic rank. </param>
        /// <param name="nation"> The nation. </param>
        /// <param name="mainBranch"> The main branch. </param>
        /// <returns></returns>
        private IEnumerable<int> GetEconomicRanks(IEnumerable<int> enabledEconomicRanks, IEnumerable<int> economicRanksWithVehicles, Func<int, string> getFormattedBattleRating, ENation nation, EBranch mainBranch)
        {
            var validEconomicRanks = enabledEconomicRanks.Intersect(economicRanksWithVehicles);

            if (validEconomicRanks.IsEmpty())
            {
                var minimumBattleRating = getFormattedBattleRating(enabledEconomicRanks.Min());
                var maximumBattleRating = getFormattedBattleRating(enabledEconomicRanks.Max());

                LogWarn(EOrganizationLogMessage.NoVehiclesAvailableForSelectedBattleRatings.FormatFluently(minimumBattleRating, maximumBattleRating, mainBranch, nation));
                return null;
            }
            return validEconomicRanks;
        }

        /// <summary> Gets a battle rating from <paramref name="economicRank"/>. </summary>
        /// <param name="economicRank"> The economic rank to get a battle rating from. </param>
        /// <param name="getFormattedBattleRating"> A function to get a formatted battle rating from an economic rank. </param>
        /// <returns></returns>
        private decimal GetBattleRating(int economicRank, Func<int, string> getFormattedBattleRating)
        {
            var battleRating = Calculator.GetBattleRating(economicRank);

            LogDebug(ECoreLogMessage.Selected.FormatFluently(getFormattedBattleRating(economicRank)));

            return battleRating;
        }

        /// <summary> Generates a vehicle type composition for a preset. </summary>
        /// <param name="gameMode"> The game mode to generate preset composition for. </param>
        /// <param name="allowedBranches"> Branches allowed in the composition. </param>
        /// <param name="crewSlotAmount"> The amount of available crew slots. </param>
        /// <param name="mainBranch"> The branch whose vehicles serve as the core of a preset. </param>
        /// <returns></returns>
        private IDictionary<EBranch, int> GetPresetComposition(EGameMode gameMode, IEnumerable<EBranch> allowedBranches, int crewSlotAmount, EBranch mainBranch)
        {
            var presetComposition = new Dictionary<EBranch, int>();

            void setAll(EBranch branch) => presetComposition.Add(branch, crewSlotAmount);
            void setQuarter(EBranch branch) => presetComposition.Add(branch, Convert.ToInt32(Math.Ceiling(crewSlotAmount * 0.25)));
            void setTwoThirds(EBranch branch) => presetComposition.Add(branch, Convert.ToInt32(Math.Ceiling(crewSlotAmount * 2.0 / 3.0)));
            void setThreeQuarters(EBranch branch) => presetComposition.Add(branch, Convert.ToInt32(Math.Ceiling(crewSlotAmount * 0.75)));
            void setHalf(EBranch branch) => presetComposition.Add(branch, Convert.ToInt32(Math.Ceiling(crewSlotAmount * 0.5)));
            void setRemaining(EBranch branch, params EBranch[] otherBranches) => presetComposition.Add(branch, crewSlotAmount - otherBranches.Sum(branch => presetComposition[branch]));

            if (mainBranch == EBranch.Army)
            {
                if (gameMode == EGameMode.Arcade || !EBranch.Aviation.IsIn(allowedBranches) && !EBranch.Helicopters.IsIn(allowedBranches))
                {
                    setAll(mainBranch);
                }
                else if (!EBranch.Aviation.IsIn(allowedBranches))
                {
                    setThreeQuarters(EBranch.Army);
                    setRemaining(EBranch.Helicopters, EBranch.Army);
                }
                else if (!EBranch.Helicopters.IsIn(allowedBranches))
                {
                    setThreeQuarters(EBranch.Army);
                    setRemaining(EBranch.Aviation, EBranch.Army);
                }
                else
                {
                    setHalf(EBranch.Army);
                    setQuarter(EBranch.Aviation);
                    setRemaining(EBranch.Helicopters, EBranch.Army, EBranch.Aviation);
                }
            }
            else if (mainBranch == EBranch.Helicopters)
            {
                if (gameMode == EGameMode.Arcade)
                {
                    setAll(mainBranch);
                }
                else
                {
                    setTwoThirds(EBranch.Helicopters);
                    setRemaining(EBranch.Army, EBranch.Helicopters);
                }
            }
            else if (mainBranch == EBranch.Aviation)
            {
                setAll(mainBranch);
            }
            else if (mainBranch == EBranch.Fleet)
            {
                if (gameMode == EGameMode.Simulator)
                    return presetComposition;

                if (crewSlotAmount <= EInteger.Number.Three || !EBranch.Aviation.IsIn(allowedBranches))
                {
                    setAll(mainBranch);
                }
                else
                {
                    setTwoThirds(EBranch.Fleet);
                    setRemaining(EBranch.Aviation, EBranch.Fleet);
                }
            }
            return presetComposition;
        }

        /// <summary> Removes the <paramref name="mainVehicle"/> from <paramref name="randomVehicles"/>. </summary>
        /// <param name="vehiclesByBranchesAndBattleRatings"> The collection of vehicles grouped first by battle ratings and then by branches.</param>
        /// <param name="presetComposition"> The affected preset composition. </param>
        /// <param name="gameMode"> The selected game mode. </param>
        /// <param name="randomVehicles"> Random vehicles to remove <paramref name="mainVehicle"/> from. </param>
        /// <param name="mainVehicle"> The vehicle to remove from <paramref name="randomVehicles"/>. </param>
        private void RemoveMainVehicleBeforeSelectingRandomVehicles
        (
            VehiclesByBranchesAndBattleRating vehiclesByBranchesAndBattleRatings,
            IDictionary<EBranch, int> presetComposition,
            EGameMode gameMode,
            List<IVehicle> randomVehicles,
            IVehicle mainVehicle = null
        )
        {
            if
            (
                mainVehicle is IVehicle
                && presetComposition.ContainsKey(mainVehicle.Branch.AsEnumerationItem)
                && vehiclesByBranchesAndBattleRatings.TryGetValue(mainVehicle.Branch.AsEnumerationItem, out var vehiclesByBattleRating)
                && mainVehicle.BattleRating.AsDictionary()[gameMode] is decimal battleRating
                && vehiclesByBattleRating.ContainsKey(battleRating)
            )
            {
                randomVehicles.Add(mainVehicle);
                presetComposition[mainVehicle.Branch.AsEnumerationItem]--;
                vehiclesByBattleRating[battleRating].Remove(mainVehicle);
            }
        }

        /// <summary> Randomly selects vehicles from the provided collection based on given parameters. </summary>
        /// <param name="vehiclesByBranchesAndBattleRatings"> The vehicle collecion to select from, grouped by branches and battle ratings. </param>
        /// <param name="presetComposition"> The preset composition. </param>
        /// <param name="crewSlotAmount"> The amount of available crew slots. </param>
        /// <returns></returns>
        private IList<IVehicle> GetRandomVehiclesForPreset(VehiclesByBranchesAndBattleRating vehiclesByBranchesAndBattleRatings, IDictionary<EBranch, int> presetComposition, int crewSlotAmount, EGameMode gameMode, IVehicle mainVehicle = null)
        {
            var randomVehicles = new List<IVehicle>();

            RemoveMainVehicleBeforeSelectingRandomVehicles(vehiclesByBranchesAndBattleRatings, presetComposition, gameMode, randomVehicles, mainVehicle);

            while (randomVehicles.Count() < crewSlotAmount && vehiclesByBranchesAndBattleRatings.Any())
            {
                foreach (var branch in vehiclesByBranchesAndBattleRatings.Keys.ToList())
                {
                    var vehiclesToTake = Math.Min(crewSlotAmount - randomVehicles.Count, presetComposition[branch]);
                    randomVehicles.AddRange(vehiclesByBranchesAndBattleRatings[branch].GetRandomVehicles(_vehicleSelector, vehiclesToTake));

                    if (randomVehicles.Count() == crewSlotAmount)
                        break;
                    if (vehiclesByBranchesAndBattleRatings[branch].IsEmpty())
                        vehiclesByBranchesAndBattleRatings.Remove(branch);
                }
            }

            return presetComposition
                .Keys
                .SelectMany(branch => randomVehicles.Where(vehicle => vehicle.Branch.AsEnumerationItem == branch))
                .ToList()
            ;
        }

        #endregion Methods: Helper Methods for GeneratePrimaryAndFallbackPresets()

        /// <summary> Generates two vehicle presets (primary and fallback) based on the given specification. </summary>
        /// <param name="specification"> The specification to base vehicle selection on. </param>
        /// <returns></returns>
        public IDictionary<EPreset, Preset> GeneratePrimaryAndFallbackPresets(Specification specification)
        {
            #region Initial extraction of parameters from specification.

            var emptyPreset = new Dictionary<EPreset, Preset>();
            var enabledVehicleClassesByBranch = specification.BranchSpecifications.ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value.VehicleClasses);
            var enabledNationCountryPairs = specification.NationSpecifications.Values.SelectMany(nationSpecification => nationSpecification.Countries.Select(country => new NationCountryPair(nationSpecification.Nation, country)));

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Filtering by vehicle Gaijin IDs.

            var vehiclesWithGaijinIdFilter = FilterVehiclesByGaijinIds(specification.VehicleGaijinIds);

            if (vehiclesWithGaijinIdFilter is null)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Filtering by branch tags.

            var vehiclesWithBranchTagFilter = vehiclesWithGaijinIdFilter;

            if (specification.BranchSpecifications.ContainsKey(EBranch.Aviation))
            {
                var validVehicleBranchTags = specification.VehicleBranchTags.Intersect(EBranch.Aviation.GetVehicleBranchTags());

                vehiclesWithBranchTagFilter = FilterVehiclesByAircraftTags(vehiclesWithBranchTagFilter, validVehicleBranchTags);
            }

            if (vehiclesWithBranchTagFilter is null)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Filtering by classes.

            var availableVehicleClasses = vehiclesWithBranchTagFilter.Select(vehicle => vehicle.Class).Distinct().ToList();
            var validVehicleClasses = enabledVehicleClassesByBranch.Values.SelectMany(branchVehicleClasses => branchVehicleClasses.Where(vehicleClass => vehicleClass.IsIn(availableVehicleClasses)));
            var vehiclesWithClassFilter = FilterVehiclesByClassFilters(validVehicleClasses, vehiclesWithBranchTagFilter);

            if (vehiclesWithClassFilter is null)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Filtering by subclasses.

            var vehicleSubclassesFromValidClasses = validVehicleClasses.SelectMany(vehicleClass => vehicleClass.GetVehicleSubclasses());
            var validVehicleSubclasses = specification.VehicleSubclasses.Intersect(vehicleSubclassesFromValidClasses).Including(EVehicleSubclass.None);
            var vehiclesWithSubclassFilter = FilterVehiclesBySubclassFilters(validVehicleSubclasses, vehiclesWithClassFilter);

            if (vehiclesWithSubclassFilter is null)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Filtering by countiries.

            var vehiclesWithCountryFilter = FilterVehiclesByCountries(enabledNationCountryPairs, vehiclesWithSubclassFilter);

            if (vehiclesWithCountryFilter is null)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Filtering by ranks.

            var vehiclesWithRankFilter = FilterVehiclesByRanks(specification.Ranks, vehiclesWithCountryFilter);

            if (vehiclesWithRankFilter is null)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------

            return specification.Randomisation switch
            {
                ERandomisation.CategoryBased => GeneratePrimaryAndFallbackPresetsByCategories(specification, vehiclesWithRankFilter),
                ERandomisation.VehicleBased => GeneratePrimaryAndFallbackPresetsByVehicles(specification, vehiclesWithRankFilter),
                _ => new Dictionary<EPreset, Preset>(),
            };
        }

        /// <summary> Finalises generation of presets by grouping vehicles by battle ratings and branches for orderly selection and doing selection proper. </summary>
        /// <param name="gameMode"> The selected game mode. </param>
        /// <param name="vehicles"> Vehicles to choose from. </param>
        /// <param name="nation"> The chosen nation. </param>
        /// <param name="mainBranch"> The chosen branch, main in the <paramref name="presetComposition"/>. </param>
        /// <param name="presetComposition"> The preset composition used for selection. </param>
        /// <param name="battleRatingBracket"> The battle rating bracket to pick from. </param>
        /// <param name="formattedBattleRating"> The chose battle rating, formatted for display. </param>
        /// <param name="crewSlotAmount"> The amount of crew slots. </param>
        /// <param name="mainVehicle"> The main vehicle in the primary preset. </param>
        /// <returns></returns>
        private IDictionary<EPreset, Preset> FinalisePresetGeneration
        (
            EGameMode gameMode,
            IEnumerable<IVehicle> vehicles,
            ENation nation,
            EBranch mainBranch,
            IDictionary<EBranch, int> presetComposition,
            Interval<decimal> battleRatingBracket,
            string formattedBattleRating,
            int crewSlotAmount,
            IVehicle mainVehicle = null)
        {
            var presets = new Dictionary<EPreset, Preset>();

            var vehiclesByBranchesAndBattleRatings = new VehiclesByBranchesAndBattleRating
            (
                presetComposition
                    .Keys
                    .Select(branch => new { Branch = branch, Vehicles = vehicles.Where(vehicle => vehicle.Branch.AsEnumerationItem == branch).OrderByHighestBattleRating(_vehicleSelector, gameMode, battleRatingBracket) })
                    .ToDictionary(item => item.Branch, item => new VehiclesByBattleRating(item.Vehicles))
            );

            void addPreset(EPreset presetType) =>
                presets.Add
                (
                    presetType,
                    new Preset(nation, mainBranch, formattedBattleRating, GetRandomVehiclesForPreset(vehiclesByBranchesAndBattleRatings, presetComposition, crewSlotAmount, gameMode, presetType == EPreset.Primary ? mainVehicle : null))
                );

            addPreset(EPreset.Primary);
            addPreset(EPreset.Fallback);

            return presets;
        }

        /// <summary> Generates two vehicle presets (primary and fallback) based on the given specification, following the <see cref="ERandomisation.CategoryBased"/> algorithm. </summary>
        /// <param name="specification"> The specification to base vehicle selection on. </param>
        /// <returns></returns>
        private IDictionary<EPreset, Preset> GeneratePrimaryAndFallbackPresetsByCategories(Specification specification, IEnumerable<IVehicle> filteredVehicles)
        {
            var emptyPreset = new Dictionary<EPreset, Preset>();
            var gameMode = specification.GameMode;

            #region Main branch selection.

            var availableBranches = filteredVehicles.Select(vehicle => vehicle.Branch.AsEnumerationItem).Distinct().ToList();
            var mainBranch = SelectMainBranch(specification, availableBranches);

            if (mainBranch == EBranch.None)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Nation selection.

            var availableNations = filteredVehicles.Select(vehicle => vehicle.Nation.AsEnumerationItem).Distinct().ToList();
            var nationSpecification = SelectNationSpecification(specification, mainBranch, availableNations);

            if (nationSpecification is null)
                return emptyPreset;

            var nation = SelectNation(nationSpecification);

            if (nation == ENation.None)
                return emptyPreset;

            var crewSlotAmount = nationSpecification.CrewSlots;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Filtering by nation.

            var vehiclesWithNationFilter = FilterVehiclesByNations(new List<ENation> { nation }, filteredVehicles);

            if (vehiclesWithNationFilter is null)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Branch selection.

            var vehiclesByBranches = vehiclesWithNationFilter.GroupBy(vehicle => vehicle.Branch.AsEnumerationItem).ToDictionary(group => group.Key, group => group.AsEnumerable());
            var validBranches = GetValidBranches(vehiclesByBranches);

            if (validBranches is null)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Filtering by preset composition.

            var presetComposition = GetPresetComposition(gameMode, validBranches, crewSlotAmount, mainBranch);
            var presets = new Dictionary<EPreset, Preset>();
            var vehiclesForPreset = vehiclesByBranches.Where(keyValuePair => keyValuePair.Key.IsIn(presetComposition.Keys)).SelectMany(keyValuePair => keyValuePair.Value);

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Battle rating selection.

            var economicRanksWithVehicles = vehiclesForPreset
                .Where(vehicle => vehicle.Branch.AsEnumerationItem == mainBranch)
                .Where(vehicle => vehicle.EconomicRank[gameMode].HasValue)
                .Select(vehicle => vehicle.EconomicRank[gameMode].Value)
                .Distinct()
            ;

            static string getFormattedBattleRating(int economicRank) => Calculator.GetBattleRating(economicRank).ToString(BattleRating.Format);

            var enabledEconomicRanks = specification.EconomicRankIntervals[nation].AsEnumerable();
            var validEconomicRanks = GetEconomicRanks(enabledEconomicRanks, economicRanksWithVehicles, getFormattedBattleRating, nation, mainBranch);

            if (validEconomicRanks is null)
                return new Dictionary<EPreset, Preset> { { EPreset.Primary, new Preset(nation, mainBranch, string.Empty, new List<IVehicle>()) } };

            var economicRank = _randomiser.GetRandom(validEconomicRanks);
            var battleRating = GetBattleRating(economicRank, getFormattedBattleRating);
            var formattedBattleRating = getFormattedBattleRating(economicRank);
            var battleRatingBracket = new Interval<decimal>(true, battleRating - _maximumBattleRatingDifference, battleRating, true);

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------

            return FinalisePresetGeneration(gameMode, vehiclesForPreset, nation, mainBranch, presetComposition, battleRatingBracket, formattedBattleRating, crewSlotAmount);
        }

        /// <summary> Generates two vehicle presets (primary and fallback) based on the given specification, following the <see cref="ERandomisation.VehicleBased"/> algorithm. </summary>
        /// <param name="specification"> The specification to base vehicle selection on. </param>
        /// <returns></returns>
        private IDictionary<EPreset, Preset> GeneratePrimaryAndFallbackPresetsByVehicles(Specification specification, IEnumerable<IVehicle> vehiclesWithClassFilter)
        {
            var emptyPreset = new Dictionary<EPreset, Preset>();
            var gameMode = specification.GameMode;

            #region Filtering by branches.

            var availableBranches = vehiclesWithClassFilter.Select(vehicle => vehicle.Branch.AsEnumerationItem).Distinct().ToList();
            var validBranches = availableBranches.Intersect(specification.BranchSpecifications.Keys).ToList();
            var vehiclesWithBranchFilter = FilterVehiclesByBranches(validBranches, vehiclesWithClassFilter);

            if (vehiclesWithBranchFilter is null)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Filtering by nations.

            var availableNations = vehiclesWithBranchFilter.Select(vehicle => vehicle.Nation.AsEnumerationItem).Distinct().ToList();
            var validNations = availableNations.Intersect(specification.NationSpecifications.Keys).ToList();
            var vehiclesWithNationFilter = FilterVehiclesByNations(validNations, vehiclesWithBranchFilter);

            if (vehiclesWithNationFilter is null)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Filtering by economic ranks.

            var vehiclesByNations = vehiclesWithNationFilter.GroupBy(item => item.Nation.AsEnumerationItem).ToDictionary(group => group.Key, group => group.AsEnumerable());
            var availableEconomicRanks = vehiclesByNations.ToDictionary
            (
                nationKeyValuePair => nationKeyValuePair.Key,
                nationKeyValuePair => nationKeyValuePair.Value.Select(vehicle =>
                {
                    if (vehicle.EconomicRank.AsDictionary()[gameMode] is int economicRank)
                        return new { Nation = nationKeyValuePair.Key, EconomicRank = economicRank };

                    return null;
                })
                .Distinct()
                .Where(item => !(item is null))
            );
            var validEconomicRanks = new Dictionary<ENation, IEnumerable<int>>();

            foreach (var nation in availableEconomicRanks.Keys)
            {
                var availableEconomicRanksForNation = availableEconomicRanks[nation].Select(item => item.EconomicRank);
                var validEconomicRanksForNation = availableEconomicRanksForNation.Where(economicRanks => specification.EconomicRankIntervals[nation].Contains(economicRanks));

                validEconomicRanks.Add(nation, validEconomicRanksForNation);
            }

            var vehiclesWithEconomocRankFilter = FilterVehiclesByEconomicRanks(gameMode, vehiclesByNations, validEconomicRanks);

            if (vehiclesWithEconomocRankFilter is null)
            {
                return emptyPreset;
            }

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Vehicle selection.

            var mainVehicle = _randomiser.GetRandom(vehiclesWithEconomocRankFilter);
            var selectedNation = mainVehicle.Nation.AsEnumerationItem;
            var branch = mainVehicle.Branch.AsEnumerationItem;
            var availableSupplementaryVehicles = vehiclesWithEconomocRankFilter.Where(vehicle => vehicle.Nation.AsEnumerationItem == selectedNation).Except(mainVehicle);
            var branchesAvailableForPresetComposition = availableSupplementaryVehicles.Select(vehicle => vehicle.Branch.AsEnumerationItem).Distinct().ToList();
            var crewSlotAmount = specification.NationSpecifications[selectedNation].CrewSlots;
            var presetComposition = GetPresetComposition(gameMode, branchesAvailableForPresetComposition, crewSlotAmount, branch);

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Filtering by nation.

            var vehiclesOfSelectedNation = FilterVehiclesByNations(new List<ENation> { selectedNation }, vehiclesWithEconomocRankFilter);

            if (vehiclesWithNationFilter is null)
                return emptyPreset;

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Battle rating selection.

            static string getFormattedBattleRating(int economicRank) => Calculator.GetBattleRating(economicRank).ToString(BattleRating.Format);

            if (!(mainVehicle.EconomicRank.AsDictionary()[gameMode] is int economicRank))
            {
                LogWarn(EOrganizationLogMessage.NoEconomiRankSetForVehicleInGameMode.FormatFluently(mainVehicle.GaijinId));
                return null;
            }
            var battleRating = GetBattleRating(economicRank, getFormattedBattleRating);
            var formattedBattleRating = getFormattedBattleRating(economicRank);
            var battleRatingBracket = new Interval<decimal>(true, battleRating - _maximumBattleRatingDifference, battleRating, true);

            #endregion ---------------------------------------------------------------------------------------------------------------------------------------------------------------------

            return FinalisePresetGeneration(gameMode, vehiclesOfSelectedNation, selectedNation, branch, presetComposition, battleRatingBracket, formattedBattleRating, crewSlotAmount, mainVehicle);
        }
    }
}