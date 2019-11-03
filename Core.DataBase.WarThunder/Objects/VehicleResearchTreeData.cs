using Core.DataBase.Enumerations;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Enumerations.DataBase;
using Core.DataBase.WarThunder.Objects.Interfaces;
using NHibernate.Mapping.Attributes;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects
{
    /// <summary> A set of vehicle information pertaining to the research tree. </summary>
    [Class(Table = ETable.VehicleResearchTreeData)]
    public class VehicleResearchTreeData : PersistentWarThunderObjectWithId, IVehicleResearchTreeData
    {
        #region Persistent Properties

        /// <summary> The vehicle's ID. </summary>
        [Id(Column = EColumn.Id, TypeType = typeof(long), Name = nameof(Id), Generator = EIdGenerator.HiLo)]
        public override long Id { get; protected set; }

        /// <summary> The vehicle's research rank. </summary>
        [Property(NotNull = true)] public virtual int Rank { get; protected set; }

        /// <summary> 1-based coordinates of the research tree grid cell this vehicle occupies with its <see cref="Rank"/>. </summary>
        [Property()] public virtual List<int> PresetCellCoordinatesWithinRank { get; protected set; }

        /// <summary> 1-based coordinates of the research tree grid cell this vehicle occupies with its <see cref="Rank"/>. </summary>
        [Property(NotNull = true)] public virtual List<int> CellCoordinatesWithinRank { get; protected set; }

        /// <summary> The Gaijin ID of the vehicle that is required to unlock this one. This property is unreliable as it is only used explicitly when the classic JSON structure (the one used with planes and tanks) is not followed. </summary>
        [Property()] public virtual string RequiredVehicleGaijinId { get; protected set; }

        /// <summary> A 0-based index of the vehicle in its research tree folder. </summary>
        [Property()] public virtual int? FolderIndex { get; protected set; }

        /// <summary> The category of hidden vehicles this one belongs to. </summary>
        [Property()] public virtual string CategoryOfHiddenVehicles { get; protected set; }

        /// <summary> Whether this vehicle is hidden from those that don't own it. </summary>
        [Property()] public virtual bool ShowOnlyWhenBought { get; protected set; }

        /// <summary>
        /// The Gaijin ID of the game platform this vehicle is available for purchase on. It is implicitly considered not available on others. Already purchased vehicles are not affected.
        /// <para> This is the opposite of <see cref="PlatformGaijinIdVehicleIsHiddenOn"/>. </para>
        /// </summary>
        [Property()] public virtual string PlatformGaijinIdVehicleIsAvailableOn { get; protected set; }

        /// <summary>
        /// The Gaijin ID of the game platform this vehicle is unavailable for purchase on. It is implicitly considered available on others. Already purchased vehicles are not affected.
        /// <para> This is the opposite of <see cref="PlatformGaijinIdVehicleIsAvailableOn"/>. </para>
        /// </summary>
        [Property()] public virtual string PlatformGaijinIdVehicleIsHiddenOn { get; protected set; }

        /// <summary>
        /// The condition for hiding this vehicle.
        /// <para> Not related to <see cref="ShowOnlyWhenBought"/> and <see cref="PlatformGaijinIdVehicleIsHiddenOn"/>. </para>
        /// </summary>
        [Property()] public virtual string HideCondition { get; protected set; }

        /// <summary> The Gaijin Marketplace ID. </summary>
        [Property()] public virtual long? MarketplaceId { get; protected set; }

        #endregion Persistent Properties
        #region Association Properties

        /// <summary> The vehicle the data set belongs to. </summary>
        [ManyToOne(0, Column = ETable.Vehicle + "_" + EColumn.GaijinId, ClassType = typeof(Vehicle), NotNull = true)]
        [Key(1, Unique = true, Column = ETable.Vehicle + "_" + EColumn.GaijinId)]
        public virtual IVehicle Vehicle { get; protected set; }

        #endregion Association Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected VehicleResearchTreeData()
        {
        }

        /// <summary> Creates a new data set. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="vehicle"> The set's vehicle. </param>
        public VehicleResearchTreeData(IDataRepository dataRepository, IVehicle vehicle)
            : this(dataRepository, -1L, vehicle)
        {
        }

        /// <summary> Creates a new data set. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The set's ID. </param>
        /// <param name="vehicle"> The set's vehicle. </param>
        public VehicleResearchTreeData(IDataRepository dataRepository, long id, IVehicle vehicle)
            : base(dataRepository, id)
        {
            Vehicle = vehicle;

            LogCreation();
        }

        #endregion Constructors
    }
}