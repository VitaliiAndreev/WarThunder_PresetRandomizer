using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Localization.Vehicle.Interfaces;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Localization.Vehicle
{
    /// <summary> A vehicle localisation set. </summary>
    public class VehicleLocalisation : Localisation, IVehicleLocalisation
    {
        #region Properties

        /// <summary> The vehicle this localisation belongs to. </summary>
        public virtual IVehicle Vehicle { get; protected set; }

        #endregion Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected VehicleLocalisation()
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="vehicle"> The vehicle this localisation belongs to. </param>
        /// <param name="localisationRecord"> A collection of localisation values read from CSV files. </param>
        protected VehicleLocalisation(IDataRepository dataRepository, IVehicle vehicle, IList<string> localisationRecord)
            : base(dataRepository, localisationRecord)
        {
            Vehicle = vehicle;
        }

        #endregion Constructors
    }
}