using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects;
using Core.DataBase.WarThunder.Objects.Localization.Interfaces;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Localization
{
    /// <summary> A localisation set. </summary>
    public class Localisation : PersistentObjectWithId, ILocalisation
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
        protected Localisation()
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        /// <param name="localisationRecord"> A collection of localisation values read from CSV files. </param>
        protected Localisation(IDataRepository dataRepository, IList<string> localisationRecord)
            : base(dataRepository, -1L)
        {
            English = localisationRecord[1];
            French = localisationRecord[2];
            Italian = localisationRecord[3];
            German = localisationRecord[4];
            Spanish = localisationRecord[5];
            Russian = localisationRecord[6];
            Polish = localisationRecord[7];
            Czech = localisationRecord[8];
            Turkish = localisationRecord[9];
            Chinese = localisationRecord[10];
            Japanese = localisationRecord[11];
            Portuguese = localisationRecord[12];
            Vietnamese = localisationRecord[13];
            Ukrainian = localisationRecord[14];
            Serbian = localisationRecord[15];
            Hungarian = localisationRecord[16];
            Korean = localisationRecord[17];
            Belarusian = localisationRecord[18];
            Romanian = localisationRecord[19];
            TChinese = localisationRecord[20];
            HChinese = localisationRecord[21];
        }

        #endregion Constructors
    }
}