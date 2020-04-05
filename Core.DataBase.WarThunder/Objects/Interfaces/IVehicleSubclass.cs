using Core.DataBase.Objects.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A set of vehicle subclasses a vehicle belongs to. </summary>
    public interface IVehicleSubclass : IPersistentObjectWithId
    {
        #region Persistent Properties

        /// <summary> The primary subclass. </summary>
        EVehicleSubclass First { get; }
        /// <summary> The secondary subclass. </summary>
        EVehicleSubclass Second { get; }
        /// <summary> The tertiary subclass. </summary>
        EVehicleSubclass Third { get; }

        #endregion PersistentProperties
        #region Non-Persistent Properties

        IEnumerable<EVehicleSubclass> All { get; }

        #endregion Non-Persistent Properties
    }
}