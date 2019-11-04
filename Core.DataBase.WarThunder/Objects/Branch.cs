using Core.DataBase.Enumerations;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Enumerations.DataBase;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using NHibernate.Mapping.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataBase.WarThunder.Objects
{
    /// <summary> A nation's military branch. </summary>
    [Class(Table = ETable.Branch)]
    public class Branch : PersistentObjectWithIdAndGaijinId, IBranch
    {
        #region Persistent Properties

        /// <summary> The branch's ID. </summary>
        [Id(Column = EColumn.Id, TypeType = typeof(long), Name = nameof(Id), Generator = EIdGenerator.HiLo)]
        public override long Id { get; protected set; }

        /// <summary> The branch's Gaijin ID. Gaijin ID for <see cref="Branch"/> is appended by the Gaijin ID of the <see cref="Nation"/> to make it unique within <see cref="ETable.Branch"/>. </summary>
        [Property(NotNull = true, Unique = true)]
        public override string GaijinId { get; protected set; }

        #endregion Persistent Properties
        #region Association Properties

        /// <summary> The branch's nation. </summary>
        [ManyToOne(0, Column = ETable.Nation + "_" + EColumn.Id, ClassType = typeof(Nation), NotNull = true, Lazy = Laziness.Proxy)]
        [Key(1)]
        public virtual INation Nation { get; protected set; }

        /// <summary> The branch's vehicles. </summary>
        [Bag(0, Name = nameof(Vehicles), Inverse = true, Generic = true, Lazy = CollectionLazy.True)]
        [Key(1, Column = ETable.Branch + "_" + EColumn.Id, NotNull = true)]
        [OneToMany(1, ClassType = typeof(Vehicle))]
        public virtual IEnumerable<IVehicle> Vehicles { get; protected set; } = new List<IVehicle>();

        #endregion Association Properties
        #region Non-Persistent Properties

        /// <summary> Parses the Gaijin ID of the nation as an item of <see cref="EBranch"/>. </summary>
        public virtual EBranch AsEnumerationItem => EReference.BranchesFromString[GaijinId.Split(ECharacter.Underscore).Last()];

        #endregion Non-Persistent Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected Branch()
        {
        }

        /// <summary> Creates a new nation. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="gaijinId"> The branch's Gaijin ID. </param>
        /// <param name="nation"> The branch's nation. </param>
        public Branch(IDataRepository dataRepository, string gaijinId, INation nation)
            : this(dataRepository, -1L, gaijinId, nation)
        {
        }

        /// <summary> Creates a new nation. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="id"> The branch's ID. </param>
        /// <param name="gaijinId"> The branch's Gaijin ID. </param>
        /// <param name="nation"> The branch's nation. </param>
        public Branch(IDataRepository dataRepository, long id, string gaijinId, INation nation)
            : base(dataRepository, id, gaijinId)
        {
            Nation = nation;

            LogCreation();
        }

        #endregion Constructors

        /// <summary> Returns a string that represents the instance. </summary>
        /// <returns></returns>
        public override string ToString() => $"{base.ToString()} of {Nation?.ToString() ?? "?"})";

        /// <summary> Returns all persistent objects nested in the instance. This method requires overriding implementation to function. </summary>
        /// <returns></returns>
        public override IEnumerable<IPersistentObject> GetAllNestedObjects() => new List<IPersistentObject> { Nation };
    }
}