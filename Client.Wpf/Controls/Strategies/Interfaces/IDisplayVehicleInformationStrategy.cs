using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;

namespace Client.Wpf.Controls.Strategies.Interfaces
{
    /// <summary> A strategy for generating a formatted string with <see cref="IVehicle"/> information for the given <see cref="EGameMode"/>. </summary>
    public interface IDisplayVehicleInformationStrategy
    {
        #region Methods: Checks

        bool ShowReserveTag(IVehicle vehicle);

        bool ShowStarterGiftTag(IVehicle vehicle);

        bool ShowEyeIcon(IVehicle vehicle);

        bool ShowControllerIcon(IVehicle vehicle);

        bool ShowSpaceAfterControllerIcon(IVehicle vehicle);

        bool ShowPackTag(IVehicle vehicle);

        bool ShowGoldenEagleCost(IVehicle vehicle);

        bool ShowMarketIcon(IVehicle vehicle);

        bool ShowSpaceAfterSpecialIconsAndTags(IVehicle vehicle);

        bool ShowBinocularsIcon(IVehicle vehicle);

        bool ReplaceClassWithSubclass(IVehicle vehicle);

        bool ShowSecondSubclass(IVehicle vehicle);

        bool ShowResearchCosts(IVehicle vehicle);

        bool ShowSilverLionCosts(IVehicle vehicle);

        #endregion Methods: Checks
        #region Methods: Output

        char GetClassIcon(IVehicle vehicle);

        string GetClass(IVehicle vehicle);

        string GetBattleRating(EGameMode gameMode, IVehicle vehicle);

        ERank GetRank(IVehicle vehicle);

        /// <summary> Generates a formatted string with <paramref name="vehicle"/> information for the given <paramref name="gameMode"/>. </summary>
        /// <param name="gameMode"> The game mode to account for. </param>
        /// <param name="vehicle"> The vehicle whose information to display. </param>
        /// <returns></returns>
        string GetVehicleInfoBottomRow(EGameMode gameMode, IVehicle vehicle);

        string GetVehicleCardClassRow(IVehicle vehicle);

        string GetVehicleCardCountryRow(IVehicle vehicle);

        string GetVehicleCardRequirementsRow(IVehicle vehicle);

        #endregion Methods: Output
    }
}