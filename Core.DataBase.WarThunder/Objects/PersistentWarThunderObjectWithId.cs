using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects;
using Core.DataBase.WarThunder.Objects.Json.Interfaces;
using System.Linq;

namespace Core.DataBase.WarThunder.Objects
{
    public class PersistentWarThunderObjectWithId : PersistentObjectWithId
    {
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected PersistentWarThunderObjectWithId()
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        protected PersistentWarThunderObjectWithId(IDataRepository dataRepository)
            : this(dataRepository, -1L)
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The object's ID. </param>
        protected PersistentWarThunderObjectWithId(IDataRepository dataRepository, long id)
            : base(dataRepository, id)
        {
        }

        #endregion Constructors
        #region Methods: Initialization

        /// <summary> Fills valid properties of the object with values deserialized from JSON data. </summary>
        /// <param name="instanceDeserializedFromJson"> The temporary non-persistent object storing deserialized data. </param>
        public virtual void InitializeWithDeserializedJson(IDeserializedFromJsonWithGaijinId instanceDeserializedFromJson)
        {
            var properties = GetType().GetProperties().ToDictionary(property => property.Name);
            var jsonProperties = instanceDeserializedFromJson.GetType().GetProperties().ToDictionary(property => property.Name);

            foreach (var jsonProperty in jsonProperties)
            {
                if (properties.TryGetValue(jsonProperty.Key, out var property))
                    property.SetValue(this, jsonProperty.Value.GetValue(instanceDeserializedFromJson));
            }
        }

        #endregion Methods: Initialization
    }
}