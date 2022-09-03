using Core.DataBase.Enumerations;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Enumerations.DataBase;
using Core.DataBase.WarThunder.Enumerations.Logger;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Json;
using NHibernate.Mapping.Attributes;
using NHibernate.Type;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataBase.WarThunder.Objects
{
    /// <summary> A set of vehicle subclasses a vehicle belongs to. </summary>
    [Class(Table = ETable.VehicleSubclass)]
    public class VehicleSubclasses : PersistentObjectWithIdAndVehicle, IVehicleSubclasses
    {
        #region Persistent Properties

        /// <summary> The vehicle's ID. </summary>
        [Id(Column = EColumn.Id, TypeType = typeof(long), Name = nameof(Id), Generator = EIdGenerator.HiLo)]
        public override long Id { get; protected set; }

        /// <summary> The primary subclass. </summary>
        [Property(TypeType = typeof(EnumStringType<EVehicleSubclass>))]
        public virtual EVehicleSubclass? First { get; protected set; }

        /// <summary> The secondary subclass. </summary>
        [Property(TypeType = typeof(EnumStringType<EVehicleSubclass>))]
        public virtual EVehicleSubclass? Second { get; protected set; }

        #endregion PersistentProperties
        #region Association Properties

        /// <summary> The vehicle the data set belongs to. </summary>
        [ManyToOne(0, Column = ETable.Vehicle + "_" + EColumn.Id, ClassType = typeof(Vehicle), NotNull = true, Lazy = Laziness.Proxy)]
        [Key(1, Unique = true, Column = ETable.Vehicle + "_" + EColumn.Id)]
        public override IVehicle Vehicle { get; protected set; }

        #endregion Association Properties
        #region Non-Persistent Properties

        public virtual IEnumerable<EVehicleSubclass> All
        {
            get
            {
                if (First.HasValue)
                    yield return First.Value;

                if (Second.HasValue)
                    yield return Second.Value;
            }
        }

        #endregion Non-Persistent Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected VehicleSubclasses()
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="vehicle"> The vehicle the data set belongs to. </param>
        /// <param name="deserializedTags"> Vehicle tags deserialized from JSON data to initialize this instance with. </param>
        protected internal VehicleSubclasses(IDataRepository dataRepository, IVehicle vehicle, VehicleTagsDeserializedFromJson deserializedTags)
            : this(dataRepository, -1L, vehicle, deserializedTags)
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The object's ID. </param>
        /// <param name="vehicle"> The vehicle the data set belongs to. </param>
        /// <param name="deserializedTags"> Vehicle tags deserialized from JSON data to initialize this instance with. </param>
        protected VehicleSubclasses(IDataRepository dataRepository, long id, IVehicle vehicle, VehicleTagsDeserializedFromJson deserializedTags)
            : base(dataRepository, id, vehicle)
        {
            InitialiseProperties(SelectSubclassesToProcess(deserializedTags, vehicle.Class));
            LogCreation();
        }

        #endregion Constructors
        #region Methods: Initialisation

        /// <summary> Selects subclasses from <paramref name="deserializedTags"/> based on what pertains to the <paramref name="vehicleClass"/>. </summary>
        /// <param name="deserializedTags"> Vehicle tags deserialized from JSON data to select subclasses from. </param>
        /// <param name="vehicleClass"> The vehicle class for which to select subclasses. </param>
        /// <returns></returns>
        private IEnumerable<EVehicleSubclass> SelectSubclassesToProcess(VehicleTagsDeserializedFromJson deserializedTags, EVehicleClass vehicleClass)
        {
            static IEnumerable<EVehicleSubclass> selectSubclasses(IDictionary<EVehicleSubclass, bool> subclassFlags) =>
                subclassFlags.Where(subclassIsUsed => subclassIsUsed.Value).Select(subclassIsUsed => subclassIsUsed.Key);

            if (vehicleClass == EVehicleClass.TankDestroyer)
            {
                var subclassDictionary = new Dictionary<EVehicleSubclass, bool>
                {
                    { EVehicleSubclass.TankDestroyer, deserializedTags.IsTankDestroyer && !deserializedTags.IsAtgmCarrier },
                    { EVehicleSubclass.AntiTankMissileCarrier, deserializedTags.IsAtgmCarrier },
                };

                return selectSubclasses(subclassDictionary);
            }
            else if (vehicleClass == EVehicleClass.Fighter)
            {
                var subclassDictionary = new Dictionary<EVehicleSubclass, bool>
                {
                    {
                        EVehicleSubclass.Fighter,
                        deserializedTags.IsFighter
                            && !deserializedTags.IsInterceptor
                            && !deserializedTags.IsAirDefenceFighter
                            && !deserializedTags.IsJetFighter
                    },
                    { EVehicleSubclass.Interceptor, deserializedTags.IsInterceptor },
                    { EVehicleSubclass.AirDefenceFighter, deserializedTags.IsAirDefenceFighter },
                    { EVehicleSubclass.JetFighter, deserializedTags.IsJetFighter },
                };

                return selectSubclasses(subclassDictionary);
            }
            else if (vehicleClass == EVehicleClass.Attacker)
            {
                var subclassDictionary = new Dictionary<EVehicleSubclass, bool>
                {
                    { EVehicleSubclass.StrikeAircraft, deserializedTags.IsStrikeAircraft },
                };

                return selectSubclasses(subclassDictionary);
            }
            else if (vehicleClass == EVehicleClass.Bomber)
            {
                var subclassDictionary = new Dictionary<EVehicleSubclass, bool>
                {
                    { EVehicleSubclass.LightBomber, deserializedTags.IsLightBomber },
                    { EVehicleSubclass.DiveBomber, deserializedTags.IsDiveBomber },
                    {
                        EVehicleSubclass.Bomber,
                        deserializedTags.IsBomber
                            && !deserializedTags.IsLightBomber
                            && !deserializedTags.IsDiveBomber
                            && !deserializedTags.IsFrontlineBomber
                            && !deserializedTags.IsLongRangeBomber
                            && !deserializedTags.IsJetBomber
                    },
                    { EVehicleSubclass.FrontlineBomber, deserializedTags.IsFrontlineBomber },
                    { EVehicleSubclass.LongRangeBomber, deserializedTags.IsLongRangeBomber },
                    { EVehicleSubclass.JetBomber, deserializedTags.IsJetBomber },
                };

                return selectSubclasses(subclassDictionary);
            }
            else if (vehicleClass == EVehicleClass.Boat)
            {
                var subclassDictionary = new Dictionary<EVehicleSubclass, bool>
                {
                    { EVehicleSubclass.MotorGunboat, deserializedTags.IsGunBoat },
                    { EVehicleSubclass.MotorTorpedoBoat, deserializedTags.IsTorpedoBoat },
                    { EVehicleSubclass.Minelayer, deserializedTags.IsMinelayer },
                };

                return selectSubclasses(subclassDictionary);
            }
            else if (vehicleClass == EVehicleClass.HeavyBoat)
            {
                var subclassDictionary = new Dictionary<EVehicleSubclass, bool>
                {
                    { EVehicleSubclass.ArmoredGunboat, deserializedTags.IsArmoredBoat },
                    { EVehicleSubclass.MotorTorpedoGunboat, deserializedTags.IsTorpedoGunBoat },
                    { EVehicleSubclass.SubChaser, deserializedTags.IsSubmarineChaser }
                };

                return selectSubclasses(subclassDictionary);
            }
            else if (vehicleClass == EVehicleClass.Barge)
            {
                var subclassDictionary = new Dictionary<EVehicleSubclass, bool>
                {
                    { EVehicleSubclass.AntiAirFerry, deserializedTags.IsAaFerry },
                    { EVehicleSubclass.NavalFerryBarge, deserializedTags.IsFerry },
                };

                return selectSubclasses(subclassDictionary);
            }
            else if (vehicleClass == EVehicleClass.Frigate)
            {
                var subclassDictionary = new Dictionary<EVehicleSubclass, bool>
                {
                    { EVehicleSubclass.HeavyGunboat, deserializedTags.IsHeavyGunBoat },
                    { EVehicleSubclass.Frigate, deserializedTags.IsFrigate && !deserializedTags.IsHeavyGunBoat },
                };

                return selectSubclasses(subclassDictionary);
            }
            else
            {
                return selectSubclasses(new Dictionary<EVehicleSubclass, bool>());
            }
        }

        /// <summary> Initialises class properties. </summary>
        /// <param name="subclasses"> Vehicle subclasses to process. </param>
        private void InitialiseProperties(IEnumerable<EVehicleSubclass> subclasses)
        {
            var indexedSubclasses = subclasses.Distinct().Where(subclass => subclass.IsValid()).ToList();

            for (var index = 0; index < indexedSubclasses.Count(); index++)
            {
                var subclass = indexedSubclasses[index];

                switch (index)
                {
                    case 0:
                    {
                        First = subclass;
                        break;
                    }
                    case 1:
                    {
                        Second = subclass;
                        break;
                    }
                    default:
                    {
                        throw new NotImplementedException(EDatabaseWarThunderLogMessage.NeedMoreSubclassSlots.Format(Vehicle.GaijinId));
                    }
                }
            }
        }

        #endregion Methods: Initialisation
    }
}