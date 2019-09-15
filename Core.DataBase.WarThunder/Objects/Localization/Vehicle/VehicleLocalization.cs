using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Localization.Vehicle.Interfaces;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Localization.Vehicle
{
    /// <summary> A vehicle localization set. </summary>
    public class VehicleLocalization : Localization, IVehicleLocalization
    {
        #region Properties

        /// <summary> The vehicle this localization belongs to. </summary>
        public virtual IVehicle Vehicle { get; protected set; }

        #endregion Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected VehicleLocalization()
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="vehicle"> The vehicle this localization belongs to. </param>
        /// <param name="localizationRecord"> A collection of localization values read from CSV files. </param>
        protected VehicleLocalization(IDataRepository dataRepository, IVehicle vehicle, IList<string> localizationRecord)
            : base(dataRepository, localizationRecord)
        {
            Vehicle = vehicle;
        }

        #endregion Constructors
    }
}