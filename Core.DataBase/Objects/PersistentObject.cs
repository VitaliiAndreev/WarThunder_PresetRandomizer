﻿using Core.DataBase.Enumerations.Logger;
using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects.Interfaces;
using Core.Extensions;
using Core.Helpers.Logger.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.DataBase.Objects
{
    /// <summary> A persistent (stored in a database) object. </summary>
    public class PersistentObject : IPersistentObject
    {
        #region Fields

        /// <summary> The category of logs generated by this object. </summary>
        protected string _logCategory;

        /// <summary> The data repository the object belongs to. </summary>
        protected IDataRepository _dataRepository;

        #endregion Fields
        #region Constructors

        /// <summary>
        /// Creates a new transient object that can be persisted later.
        /// This constructor is used to maintain inheritance of class composition required for NHibernate mapping.
        /// </summary>
        protected PersistentObject()
        {
        }

        /// <summary> Creates a new transient object that can be persisted later. </summary>
        /// <param name="dataRepository"> A data repository to persist the object with. </param>
        protected PersistentObject(IDataRepository dataRepository)
        {
            SetLogCategory();
            _dataRepository = dataRepository;
            _dataRepository.NewObjects.Add(this);
        }

        #endregion Constructors
        #region Methods: Initialization

        protected void LogCreation() => LogTrace(ECoreLogMessage.Created.FormatFluently(ToString()));

        /// <summary> Sets the <see cref="_logCategory"/> for the object. </summary>
        protected void SetLogCategory() => _logCategory = GetType().ToStringLikeCode();

        /// <summary> Initializes non-persistent fields of the instance. Use this method to finalize reading from a database. </summary>
        /// <param name="dataRepository"> A data repository to assign the object to. </param>
        public virtual void InitializeNonPersistentFields(IDataRepository dataRepository)
        {
            SetLogCategory();
            _dataRepository = dataRepository;
        }

        #endregion Methods: Initialization
        #region Methods: Logging

        /// <summary> Creates a log entry of the "Trace" level for the current <see cref="_logCategory"/>. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        protected void LogTrace(string message) =>
            _dataRepository.Logger.LogTrace(_logCategory, message);

        /// <summary> Creates a log entry of the "Debug" level for the current <see cref="_logCategory"/>. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        protected void LogDebug(string message) =>
            _dataRepository.Logger.LogDebug(_logCategory, message);

        /// <summary> Creates a log entry of the "Info" level for the current <see cref="_logCategory"/>. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        protected void LogInfo(string message) =>
            _dataRepository.Logger.LogInfo(_logCategory, message);

        /// <summary> Creates a log entry of the "Warn" level for the current <see cref="_logCategory"/>. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        protected void LogWarn(string message) =>
            _dataRepository.Logger.LogWarn(_logCategory, message);

        /// <summary> Creates a log entry of the "Error" level for the current <see cref="_logCategory"/>. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        /// <param name="exception"> An exception whose data to log. </param>
        protected void LogError(string message, Exception exception) =>
            _dataRepository.Logger.LogError(_logCategory, message, exception);

        /// <summary> Creates a log entry of the "Fatal" level for the current <see cref="_logCategory"/>. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        /// <param name="exception"> An exception whose data to log. </param>
        protected void LogFatal(string message, Exception exception) =>
            _dataRepository.Logger.LogFatal(_logCategory, message, exception);

        #endregion Methods: Logging

        /// <summary> Returns a string that represents the instance. </summary>
        /// <returns></returns>
        public override string ToString() => $"[{this.GetTypeString()}]";

        /// <summary> Returns all persistent objects nested in the instance. This method requires overriding implementation to function. </summary>
        /// <returns></returns>
        public virtual IEnumerable<IPersistentObject> GetAllNestedObjects() =>
            new List<IPersistentObject>();

        #region Methods: Equivalence

        /// <summary> Checks whether the specified values can be considered equivalent. </summary>
        /// <param name="thisValue"> The first of the values. </param>
        /// <param name="comparedValue"> The second of the values. </param>
        /// <param name="recursionLevel">
        /// The level of recursion up to which to compare nested objects. Use with CAUTION in case of cyclic links.
        /// <para>Set to zero to disable recursion. It also prevents the method from cheking <see cref="IPersistentObject"/> members and their <see cref="IEnumerable{T}"/> collections for equivalence.</para>
        /// <para>Set to one to check <see cref="IPersistentObject"/> members and their <see cref="IEnumerable{T}"/> collections for equivalence using the same recursion rules as here.</para>
        /// </param>
        /// <returns></returns>
        private bool IsEquivalentTo(object thisValue, object comparedValue, int recursionLevel)
        {
            if (thisValue is null || comparedValue is null)
                return thisValue == comparedValue;

            if (thisValue.GetType() != comparedValue.GetType())
                return false;

            var includeNestedObjects = recursionLevel.IsPositive();

            if (thisValue is IPersistentObject persistentMemberValue && comparedValue is IPersistentObject comparedPersistentMemberValue && includeNestedObjects)
            {
                if (!persistentMemberValue.IsEquivalentTo(comparedPersistentMemberValue, recursionLevel - 1))
                    return false;
            }
            else if (thisValue is IEnumerable<IPersistentObject> persistentCollectionValue && comparedValue is IEnumerable<IPersistentObject> comparedPersistentCollectionValue && includeNestedObjects)
            {
                if (!persistentCollectionValue.IsEquivalentTo(comparedPersistentCollectionValue, recursionLevel - 1))
                    return false;
            }
            else if (thisValue is IEnumerable<object> collection && comparedValue is IEnumerable<object> comparedCollection)
            {
                if (includeNestedObjects && collection.Zip(comparedCollection, (left, right) => left == right).Any(comparisonResult => comparisonResult == false))
                    return false;
            }
            else if (thisValue is decimal decimalValue && comparedValue is decimal comparedDecimal)
            {
                if (decimalValue != comparedDecimal)
                    return false;
            }
            else
            {
                if (thisValue.ToString() != comparedValue.ToString())
                    return false;
            }
            return true;
        }

        /// <summary> Checks whether the specified instance can be considered equivalent to the current one. </summary>
        /// <param name="comparedPersistentObject"> An instance of a compared object. </param>
        /// <param name="recursionLevel">
        /// The level of recursion up to which to compare nested objects. Use with CAUTION in case of cyclic links.
        /// <para>Set to zero to disable recursion. It also prevents the method from cheking <see cref="IPersistentObject"/> members and their <see cref="IEnumerable{T}"/> collections for equivalence.</para>
        /// <para>Set to one to check <see cref="IPersistentObject"/> members and their <see cref="IEnumerable{T}"/> collections for equivalence using the same recursion rules as here.</para>
        /// </param>
        /// <returns></returns>
        public virtual bool IsEquivalentTo(IPersistentObject comparedPersistentObject, int recursionLevel = 0)
        {
            var includeNestedObjects = recursionLevel.IsPositive();

            if (comparedPersistentObject is PersistentObject comparedObject)
            {
                if (_dataRepository != comparedObject._dataRepository)
                    return false;
                if (_logCategory != comparedObject._logCategory)
                    return false;
            }

            foreach (var property in GetType().GetProperties())
            {
                if (comparedPersistentObject.GetType().GetProperties().FirstOrDefault(comparedProperty => comparedProperty.Name == property.Name) is null)
                    continue;

                if (!IsEquivalentTo(property.GetValue(this), property.GetValue(comparedPersistentObject), recursionLevel - 1))
                    return false;
            }
            return true;
        }

        #endregion Methods: Equivalence

        /// <summary> Commit changes to the current persistent object (persist if the object is transient) using the <see cref="IDataRepository"/> provided with the object's constructor. </summary>
        public virtual void CommitChanges()
        {
            LogDebug(EDataBaseLogMessage.PreparingToCommitChangesTo.FormatFluently(ToString()));

            if (_dataRepository.IsClosed)
            {
                LogDebug(EDataBaseLogMessage.DataRepositoryClosed_CommittingAborted);
                return;
            }

            _dataRepository.CommitChanges(this);

            if (_dataRepository.NewObjects.Contains(this))
                _dataRepository.NewObjects.Remove(this);
        }
    }
}
