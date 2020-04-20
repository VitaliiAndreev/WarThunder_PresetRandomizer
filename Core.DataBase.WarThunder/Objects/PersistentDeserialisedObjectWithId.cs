using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects;
using Core.DataBase.WarThunder.Extensions;
using Core.DataBase.WarThunder.Objects.Json;
using Core.DataBase.WarThunder.Objects.Json.Interfaces;
using Core.DataBase.WarThunder.Objects.VehicleGameModeParameterSets;
using NHibernate.Mapping.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.DataBase.WarThunder.Objects
{
    public class PersistentDeserialisedObjectWithId : PersistentObjectWithId
    {
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected PersistentDeserialisedObjectWithId()
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        protected PersistentDeserialisedObjectWithId(IDataRepository dataRepository)
            : this(dataRepository, -1L)
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The object's ID. </param>
        protected PersistentDeserialisedObjectWithId(IDataRepository dataRepository, long id)
            : base(dataRepository, id)
        {
        }

        #endregion Constructors
        #region Methods: Initialization

        /// <summary> Fills valid properties of the object with values deserialized from JSON data. </summary>
        /// <param name="instanceDeserializedFromJson"> The temporary non-persistent object storing deserialized data. </param>
        public virtual void InitializeWithDeserializedJson(IDeserializedFromJsonWithGaijinId instanceDeserializedFromJson)
        {
            var properties = GetType()
                .GetProperties()
                .Where(property => property.GetCustomAttribute<PropertyAttribute>() is PropertyAttribute)
                .ToDictionary(property => property.Name)
            ;
            var jsonProperties = instanceDeserializedFromJson
                .GetType()
                .GetProperties()
                .ToDictionary(property => property.Name)
            ;

            foreach (var property in properties)
            {
                if (jsonProperties.TryGetValue(property.Key, out var jsonProperty))
                    property.Value.SetValue(this, jsonProperty.GetValue(instanceDeserializedFromJson));
            }
        }

        /// <summary> Consolidates values of JSON properties for <see cref="EGameMode"/> parameters into sets defined in the persistent class. </summary>
        /// <param name="instanceDeserializedFromJson"> The temporary non-persistent object storing deserialized data. </param>
        protected void ConsolidateGameModeParameterPropertiesIntoSets(IDictionary<string, VehicleGameModeParameterSetBase> parameterSets, VehicleDeserializedFromJsonWpCost instanceDeserializedFromJson)
        {
            foreach (var jsonProperty in instanceDeserializedFromJson.GetType().GetProperties()) // With a dictionary of game mode parameter set properties now there's only need to look through the JSON mapping class once.
                parameterSets.InsertJsonPropertyValueIntoGameModeParameterSet(instanceDeserializedFromJson, jsonProperty);
        }

        #endregion Methods: Initialization
    }
}