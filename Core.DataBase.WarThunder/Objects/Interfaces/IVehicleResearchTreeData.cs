using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A set of vehicle information pertaining to the research tree. </summary>
    public interface IVehicleResearchTreeData : IPersistentWarThunderObjectWithId
    {
        #region Persistent Properties

        /// <summary> The vehicle's research rank. </summary>
        int Rank { get; }

        /// <summary> 1-based coordinates of the research tree grid cell this vehicle occupies with its <see cref="Rank"/>. </summary>
        List<int> PresetCellCoordinatesWithinRank { get; }

        /// <summary> 1-based coordinates of the research tree grid cell this vehicle occupies with its <see cref="Rank"/>. </summary>
        List<int> CellCoordinatesWithinRank { get; }

        /// <summary> The Gaijin ID of the vehicle that is required to unlock this one. This property is unreliable as it is only used explicitly when the classic JSON structure (the one used with planes and tanks) is not followed. </summary>
        string RequiredVehicleGaijinId { get; }

        /// <summary> A 0-based index of the vehicle in its research tree folder. </summary>
        int? FolderIndex { get; }

        /// <summary> The category of hidden vehicles this one belongs to. </summary>
        string CategoryOfHiddenVehicles { get; }

        /// <summary>
        /// The Gaijin ID of the game platform this vehicle is available for purchase on. It is implicitly considered not available on others. Already purchased vehicles are not affected.
        /// <para> This is the opposite of <see cref="PlatformGaijinIdVehicleIsHiddenOn"/>. </para>
        /// </summary>
        string PlatformGaijinIdVehicleIsAvailableOn { get; }

        /// <summary>
        /// The Gaijin ID of the game platform this vehicle is unavailable for purchase on. It is implicitly considered available on others. Already purchased vehicles are not affected.
        /// <para> This is the opposite of <see cref="PlatformGaijinIdVehicleIsAvailableOn"/>. </para>
        /// </summary>
        string PlatformGaijinIdVehicleIsHiddenOn { get; }

        /// <summary>
        /// The condition for hiding this vehicle.
        /// <para> Not related to <see cref="ShowOnlyWhenBought"/> and <see cref="PlatformGaijinIdVehicleIsHiddenOn"/>. </para>
        /// </summary>
        string HideCondition { get; }

        /// <summary> The Gaijin Marketplace ID. </summary>
        long? MarketplaceId { get; }

        #endregion Persistent Properties
        #region Association Properties

        /// <summary> The vehicle the data set belongs to. </summary>
        IVehicle Vehicle { get; }

        #endregion Association Properties
    }
}