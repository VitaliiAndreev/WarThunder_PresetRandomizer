using Core.DataBase.Helpers;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects.Interfaces;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Extensions;
using Core.Helpers.Logger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.DataBase.WarThunder.Helpers
{
    public class DataRepositoryWarThunder : DataRepository
    {
        #region Constructors

        /// <summary> Creates a new repository, as well as creates and configures a <see cref="ISessionFactory"/> for it. </summary>
        /// <param name="dataBaseFileName"> The name of an SQLite database file (without an extension). </param>
        /// <param name="overwriteExistingDataBase"> Indicates whether an existing database should be overwritten on creation of the <see cref="SessionFactory"/>. </param>
        /// <param name="assemblyWithMapping"> An assembly containing mapped classes. </param>
        /// <param name="loggers"> Instances of loggers. </param>
        public DataRepositoryWarThunder(string dataBaseFileName, bool overwriteExistingDataBase, Assembly assemblyWithMapping, params IConfiguredLogger[] loggers)
            : base(dataBaseFileName, overwriteExistingDataBase, assemblyWithMapping, loggers)
        {
        }

        #endregion Constructors

        /// <summary>
        /// Persists any transient objects cached in the repository.
        /// This override is used to reorder the <see cref="IDataRepository.NewObjects"/> collection before persisting its contents so that the latter adhere to foreign key constraints when committed.
        /// </summary>
        public override void PersistNewObjects()
        {
            var sortedNewObjects = new List<IPersistentObject>();
            
            sortedNewObjects.AddRange(NewObjects.OfType<INation>());
            sortedNewObjects.AddRange(NewObjects.OfType<IBranch>());
            sortedNewObjects.AddRange(NewObjects.OfType<IVehicle>());
            sortedNewObjects.AddRange(NewObjects.OfType<IVehicleGameModeParameterSetBase>());

            if (sortedNewObjects.Count() != NewObjects.Count())
                throw new ArgumentException("Not all object type have been included in sorting");

            NewObjects.ReplaceBy(sortedNewObjects);

            base.PersistNewObjects();
        }
    }
}