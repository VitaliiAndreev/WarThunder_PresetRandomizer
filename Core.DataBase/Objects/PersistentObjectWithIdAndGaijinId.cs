﻿using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects;
using Core.DataBase.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Json.Interfaces;
using Core.Enumerations;
using System;
using System.Linq;

namespace Core.DataBase.WarThunder.Objects
{
    /// <summary> A persistent (stored in a database) object that has an ID and a name. </summary>
    public abstract class PersistentObjectWithIdAndGaijinId : PersistentObjectWithId, IPersistentObjectWithIdAndGaijinId
    {
        #region Properties

        /// <summary> The object's Gaijin ID. </summary>
        public virtual string GaijinId { get; protected set; }

        #endregion Properties
        #region Constructors

        /// <summary>
        /// Creates a new transient object that can be persisted later.
        /// This constructor is used to maintain inheritance of class composition required for NHibernate mapping.
        /// </summary>
        protected PersistentObjectWithIdAndGaijinId()
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="gaijinId"> The object's Gaijin ID. </param>
        protected PersistentObjectWithIdAndGaijinId(IDataRepository dataRepository, string gaijinId)
            : this(dataRepository, Guid.NewGuid(), gaijinId)
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The object's ID. </param>
        /// <param name="gaijinId"> The object's Gaijin ID. </param>
        protected PersistentObjectWithIdAndGaijinId(IDataRepository dataRepository, Guid id, string gaijinId)
            : base(dataRepository, id)
        {
            GaijinId = gaijinId;
        }

        #endregion Constructors

        /// <summary> Returns a string that represents the instance. </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var baseRepresentation = base.ToString().Split(ECharacter.Space);
            var type = baseRepresentation.First();
            var id = baseRepresentation.Last();
            return $"{type} [{GaijinId}] {id}";
        }

        /// <summary> Checks whether the specified instance can be considered equivalent to the current one. </summary>
        /// <param name="comparedPersistentObject"> An instance of a compared object. </param>
        /// <returns></returns>
        protected override bool IsEquivalentTo(IPersistentObject comparedPersistentObject)
        {
            if (base.IsEquivalentTo(comparedPersistentObject) && comparedPersistentObject is PersistentObjectWithIdAndGaijinId comparedPersistentObjectWithIdAndName)
            {
                if (GaijinId != comparedPersistentObjectWithIdAndName.GaijinId)
                    return false;

                return true;
            }
            return false;
        }
    }
}