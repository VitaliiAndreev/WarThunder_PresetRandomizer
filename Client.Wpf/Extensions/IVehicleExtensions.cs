﻿using Client.Wpf.Enumerations.ShrinkProfiles;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;

namespace Client.Wpf.Extensions
{
    public static class IVehicleExtensions
    {
        public static object AsLite(this IVehicle vehicle, EVehicleProfile profile, ELanguage language)
        {
            static string localise(object key) => ApplicationHelpers.LocalisationManager.GetLocalisedString(key);

            return profile switch
            {
                EVehicleProfile.Nation => new
                {
                    vehicle.GaijinId,
                    Name = vehicle.ResearchTreeName.GetLocalisation(language),
                    Country = localise(vehicle.Country),
                    Branch = localise(vehicle.Branch.AsEnumerationItem),
                    Rank = vehicle.RankAsEnumerationItem,
                    Class = localise(vehicle.Class),
                    Subclass1 = localise(vehicle.Subclasses.First),
                    Subclass2 = localise(vehicle.Subclasses.Second),
                    vehicle.IsResearchable,
                    vehicle.IsSquadronVehicle,
                    vehicle.IsHiddenUnlessOwned,
                    vehicle.IsPremium,
                    vehicle.IsPurchasableForGoldenEagles,
                    vehicle.GiftedToNewPlayersForSelectingTheirFirstBranch,
                    vehicle.IsSoldInTheStore,
                    vehicle.IsSoldOnTheMarket,
                    vehicle.IsAvailableOnlyOnConsoles,
                },
                EVehicleProfile.NationAndCountry => new
                {
                    vehicle.GaijinId,
                    Name = vehicle.ResearchTreeName.GetLocalisation(language),
                    Branch = localise(vehicle.Branch.AsEnumerationItem),
                    Rank = vehicle.RankAsEnumerationItem,
                    Class = localise(vehicle.Class),
                    Subclass1 = localise(vehicle.Subclasses.First),
                    Subclass2 = localise(vehicle.Subclasses.Second),
                    vehicle.IsResearchable,
                    vehicle.IsSquadronVehicle,
                    vehicle.IsHiddenUnlessOwned,
                    vehicle.IsPremium,
                    vehicle.IsPurchasableForGoldenEagles,
                    vehicle.GiftedToNewPlayersForSelectingTheirFirstBranch,
                    vehicle.IsSoldInTheStore,
                    vehicle.IsSoldOnTheMarket,
                    vehicle.IsAvailableOnlyOnConsoles,
                },
                _ => null
            };
        }
    }
}