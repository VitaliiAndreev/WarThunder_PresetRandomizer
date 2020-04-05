using Core.DataBase.Enumerations;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Enumerations.DataBase;
using Core.DataBase.WarThunder.Enumerations.Logger;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Extensions;
using NHibernate.Mapping.Attributes;
using NHibernate.Type;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataBase.WarThunder.Objects
{
    /// <summary> A set of vehicle subclasses a vehicle belongs to. </summary>
    [Class(Table = ETable.VehicleSubclass)]
    public class VehicleSubclass : PersistentObjectWithId, IVehicleSubclass
    {
        #region Persistent Properties

        /// <summary> The vehicle's ID. </summary>
        [Id(Column = EColumn.Id, TypeType = typeof(long), Name = nameof(Id), Generator = EIdGenerator.HiLo)]
        public override long Id { get; protected set; }

        /// <summary> The primary subclass. </summary>
        [Property(TypeType = typeof(EnumStringType<EVehicleSubclass>))]
        public virtual EVehicleSubclass First { get; protected set; } = EVehicleSubclass.None;

        /// <summary> The secondary subclass. </summary>
        [Property(TypeType = typeof(EnumStringType<EVehicleSubclass>))]
        public virtual EVehicleSubclass Second { get; protected set; } = EVehicleSubclass.None;

        /// <summary> The tertiary subclass. </summary>
        [Property(TypeType = typeof(EnumStringType<EVehicleSubclass>))]
        public virtual EVehicleSubclass Third { get; protected set; } = EVehicleSubclass.None;

        #endregion PersistentProperties
        #region Association Properties

        /// <summary> The vehicle the data set belongs to. </summary>
        [ManyToOne(0, Column = ETable.Vehicle + "_" + EColumn.Id, ClassType = typeof(Vehicle), NotNull = true, Lazy = Laziness.Proxy)]
        [Key(1, Unique = true, Column = ETable.Vehicle + "_" + EColumn.Id)]
        public virtual IVehicle Vehicle { get; protected set; }

        #endregion Association Properties
        #region Non-Persistent Properties

        public virtual IEnumerable<EVehicleSubclass> All => new HashSet<EVehicleSubclass> { First, Second, Third }.AsEnumerable();

        #endregion Non-Persistent Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected VehicleSubclass()
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="vehicle"> The vehicle the data set belongs to. </param>
        /// <param name="subclasses"> Vehicle subclasses to process. </param>
        protected internal VehicleSubclass(IDataRepository dataRepository, IVehicle vehicle, IEnumerable<EVehicleSubclass> subclasses)
            : this(dataRepository, -1L, vehicle, subclasses)
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The object's ID. </param>
        /// <param name="vehicle"> The vehicle the data set belongs to. </param>
        /// <param name="subclasses"> Vehicle subclasses to process. </param>
        protected VehicleSubclass(IDataRepository dataRepository, long id, IVehicle vehicle, IEnumerable<EVehicleSubclass> subclasses)
            : base(dataRepository, id)
        {
            Vehicle = vehicle;

            var indexedSubclasses = subclasses.Distinct().Where(subclass => subclass.IsValid()).ToList();

            for (var index = 0; index < indexedSubclasses.Count(); index++)
            {
                var subclass = indexedSubclasses[index];

                switch (index)
                {
                    case 0:
                    {
                        First = subclass;
                        Second = subclass;
                        Third = subclass;
                        break;
                    }
                    case 1:
                    {
                        Second = subclass;
                        break;
                    }
                    case 2:
                    {
                        Third = subclass;
                        break;
                    }
                    default:
                    {
                        throw new NotImplementedException(EDatabaseWarThunderLogMessage.NeedMoreSubclassSlots.FormatFluently(vehicle.GaijinId));
                    }
                }
            }
        }

        #endregion Constructors
    }
}