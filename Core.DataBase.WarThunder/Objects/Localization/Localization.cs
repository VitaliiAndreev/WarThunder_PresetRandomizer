using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.WarThunder.Objects.Localization.Interfaces;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Localization
{
    /// <summary> A localization set. </summary>
    public class Localization : PersistentObjectWithIdAndGaijinId, ILocalization
    {
        #region Properties

        public virtual string English { get; protected set; }
        public virtual string French { get; protected set; }
        public virtual string Italian { get; protected set; }
        public virtual string German { get; protected set; }
        public virtual string Spanish { get; protected set; }
        public virtual string Russian { get; protected set; }
        public virtual string Polish { get; protected set; }
        public virtual string Czech { get; protected set; }
        public virtual string Turkish { get; protected set; }
        public virtual string Chinese { get; protected set; }
        public virtual string Japanese { get; protected set; }
        public virtual string Portuguese { get; protected set; }
        public virtual string Vietnamese { get; protected set; }
        public virtual string Ukrainian { get; protected set; }
        public virtual string Serbian { get; protected set; }
        public virtual string Hungarian { get; protected set; }
        public virtual string Korean { get; protected set; }
        public virtual string Belarusian { get; protected set; }
        public virtual string Romanian { get; protected set; }
        public virtual string TChinese { get; protected set; }
        public virtual string HChinese { get; protected set; }

        #endregion Properties
        #region Constructors

        /// <summary> This constructor is used by NHibernate to instantiate an entity read from a database. </summary>
        protected Localization()
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="localizationRecord"> A collection of localization values read from CSV files. </param>
        protected Localization(IDataRepository dataRepository, IList<string> localizationRecord)
            : base(dataRepository, -1L, localizationRecord[0])
        {
            English = localizationRecord[1];
            French = localizationRecord[2];
            Italian = localizationRecord[3];
            German = localizationRecord[4];
            Spanish = localizationRecord[5];
            Russian = localizationRecord[6];
            Polish = localizationRecord[7];
            Czech = localizationRecord[8];
            Turkish = localizationRecord[9];
            Chinese = localizationRecord[10];
            Japanese = localizationRecord[11];
            Portuguese = localizationRecord[12];
            Vietnamese = localizationRecord[13];
            Ukrainian = localizationRecord[14];
            Serbian = localizationRecord[15];
            Hungarian = localizationRecord[16];
            Korean = localizationRecord[17];
            Belarusian = localizationRecord[18];
            Romanian = localizationRecord[19];
            TChinese = localizationRecord[20];
            HChinese = localizationRecord[21];
        }

        #endregion Constructors
    }
}