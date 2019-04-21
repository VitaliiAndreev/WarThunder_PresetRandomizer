using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Json;
using Core.Enumerations.DataBase;
using NHibernate.Mapping.Attributes;
using System;

namespace Core.DataBase.WarThunder.Objects
{
    public class Vehicle : PersistentObjectWithIdAndGaijinId, IVehicle
    {
        #region Persistent Properties

        /// <summary> The vehicle's ID. </summary>
        [Id(Column = EColumn.Id, TypeType = typeof(Guid), Name = nameof(Id))]
        public override Guid Id { get; protected set; }

        /// <summary> The vehicle's Gaijin ID. </summary>
        [Property(NotNull = true, Unique = true)]
        public override string GaijinId { get; protected set; }

        #endregion Persistent Properties
        #region Constructors

        /// <summary>
        /// Creates a new transient object that can be persisted later.
        /// This constructor is used by NHibernate to instantiate deserialized data read from a database.
        /// </summary>
        protected Vehicle()
        {
        }

        /// <summary> Creates a new nation. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="gaijinId"> The nation's Gaijin ID. </param>
        public Vehicle(IDataRepository dataRepository, string gaijinId)
            : this(dataRepository, Guid.NewGuid(), gaijinId)
        {
        }

        /// <summary> Creates a new nation. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The nation's ID. </param>
        /// <param name="gaijinId"> The nation's Gaijin ID. </param>
        public Vehicle(IDataRepository dataRepository, Guid id, string gaijinId)
            : base(dataRepository, id, gaijinId)
        {
            LogCreation();
        }

        #endregion Constructors
    }
}
