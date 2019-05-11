﻿using Core.Helpers.Logger.Interfaces;
using System;

namespace Core.Helpers.Logger
{
    /// <summary> A base class to encapsulate logging. </summary>
    public abstract class LoggerFluency
    {
        #region Fields

        /// <summary> An instance of a logger. </summary>
        protected readonly IConfiguredLogger _logger;

        /// <summary> The category of logs generated by this instance. </summary>
        protected readonly string _logCategory;

        #endregion Fields
        #region Constructors

        /// <summary> Creates a new fluent logger component. </summary>
        /// <param name="logger"> An instance of a logger. </param>
        /// <param name="logCategory"> The category of logs generated by this instance. </param>
        public LoggerFluency(IConfiguredLogger logger, string logCategory)
        {
            _logger = logger;
            _logCategory = logCategory;
        }

        #endregion Constructors
        #region Methods

        #region Methods: Logging

        /// <summary> Creates a log entry of the "Trace" level for the current <see cref="_logCategory"/>. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        protected void LogTrace(string message) =>
            _logger.LogTrace(_logCategory, message);

        /// <summary> Creates a log entry of the "Debug" level for the current <see cref="_logCategory"/>. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        protected void LogDebug(string message) =>
            _logger.LogDebug(_logCategory, message);

        /// <summary> Creates a log entry of the "Info" level for the current <see cref="_logCategory"/>. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        protected void LogInfo(string message) =>
            _logger.LogInfo(_logCategory, message);

        /// <summary> Creates a log entry of the "Warn" level for the current <see cref="_logCategory"/>. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        protected void LogWarn(string message) =>
            _logger.LogWarn(_logCategory, message);

        /// <summary> Creates a log entry of the "Error" level for the current <see cref="_logCategory"/>. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        /// <param name="exception"> An exception whose data to log. </param>
        protected void LogError(string message, Exception exception) =>
            _logger.LogError(_logCategory, message, exception);

        /// <summary> Creates a log entry of the "Fatal" level for the current <see cref="_logCategory"/>. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        /// <param name="exception"> An exception whose data to log. </param>
        protected void LogFatal(string message, Exception exception) =>
            _logger.LogFatal(_logCategory, message, exception);

        #endregion Methods: Logging
        #region Methods: Fluency

        /// <summary> Throws the specified exception after logging it as an error. Note that the compiler does not see throwing in this method from where it is being called. </summary>
        /// <param name="logMessage"> The nessage to log. </param>
        /// <param name="exception"> The exception to throw. </param>
        public void LogErrorAndThrow(string logMessage, Exception exception)
        {
            LogError(logMessage, exception);
            throw exception;
        }

        /// <summary> Throws an exception of the specified type after logging it as an error. Note that the compiler does not see throwing in this method from where it is being called. </summary>
        /// <typeparam name="T"> The type of the exception. </typeparam>
        /// <param name="exceptionMessage"> The exception message. </param>
        /// <param name="logMessage"> The nessage to log. </param>
        public void LogErrorAndThrow<T>(string exceptionMessage, string logMessage) where T : Exception =>
            LogErrorAndThrow(logMessage, Activator.CreateInstance(typeof(T), exceptionMessage) as T);

        #endregion Methods: Fluency

        #endregion Methods
    }
}
