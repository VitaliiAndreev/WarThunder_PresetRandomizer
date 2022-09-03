using Core.DataBase.Enumerations;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Enumerations.DataBase;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Json;
using NHibernate.Mapping.Attributes;
using System.Linq;

namespace Core.DataBase.WarThunder.Objects
{
    /// <summary> A set of vehicle information pertaining to graphics. </summary>
    [Class(Table = ETable.VehicleGraphicsData)]
    public class VehicleGraphicsData : PersistentDeserialisedObjectWithIdAndVehicle, IVehicleGraphicsData
    {
        #region Association Properties

        /// <summary> The vehicle the data set belongs to. </summary>
        [ManyToOne(0, Column = ETable.Vehicle + "_" + EColumn.Id, ClassType = typeof(Vehicle), NotNull = true, Lazy = Laziness.Proxy)]
        [Key(1, Unique = true, Column = ETable.Vehicle + "_" + EColumn.Id)]
        public override IVehicle Vehicle { get; protected set; }

        #endregion Association Properties
        #region Persistent Properties

        /// <summary> The vehicle's ID. </summary>
        [Id(Column = EColumn.Id, TypeType = typeof(long), Name = nameof(Id), Generator = EIdGenerator.HiLo)]
        public override long Id { get; protected set; }

        /// <summary> The path to the image file used as an icon. Can have either no extension (corresponds to PNG) or SVG. </summary>
        [Property()] public virtual string CustomClassIco { get; protected set; }

        /// <summary> The path to the image file used as a banner icon. </summary>
        [Property()] public virtual string BannerImageName { get; protected set; }

        /// <summary> The path to the image file used as a portrait. </summary>
        [Property()] public virtual string PortraitName { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual string CommonWeaponImage { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual int? WeaponMask { get; protected set; }

        /// <summary> [THERE IS NO FULL UNDERSTANDING OF THIS PROPERTY] </summary>
        [Property()] public virtual int? BulletsIconParam { get; protected set; }

        #endregion Persistent Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected VehicleGraphicsData()
        {
        }

        /// <summary> Creates a data set. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="vehicle"> The vehicle this data set belongs to. </param>
        /// <param name="instanceDerializedFromJson"> A non-persistent instance deserialized from JSON data to initialize this instance with. </param>
        public VehicleGraphicsData(IDataRepository dataRepository, IVehicle vehicle, VehicleDeserializedFromJsonWpCost instanceDerializedFromJson)
            : this(dataRepository, -1L, vehicle)
        {
            InitializeWithDeserializedJson(instanceDerializedFromJson);
        }

        /// <summary> Creates a data set. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The objects's ID. </param>
        /// <param name="vehicle"> The vehicle this data set belongs to. </param>
        public VehicleGraphicsData(IDataRepository dataRepository, long id, IVehicle vehicle)
            : base(dataRepository, id, vehicle)
        {
            LogCreation();
        }

        #endregion Constructors

        private string GetInheritedGaijinId(string imagePathPropertyValue)
        {
            var firstSeparator = "#";
            var secondSeparator = "/";

            if (imagePathPropertyValue is null || !imagePathPropertyValue.ContainsAny(new string[] { firstSeparator, secondSeparator }))
                return string.Empty;

            return imagePathPropertyValue
                .Split(firstSeparator)
                .Last()
                .Split(secondSeparator)
                .Last()
            ;
        }

        public virtual string GetInheritedGaijinId(EVehicleImage imageType)
        {
            return imageType switch
            {
                EVehicleImage.Banner => GetInheritedGaijinId(BannerImageName),
                EVehicleImage.Portrait => GetInheritedGaijinId(PortraitName),
                _ => string.Empty,
            };
        }
    }
}