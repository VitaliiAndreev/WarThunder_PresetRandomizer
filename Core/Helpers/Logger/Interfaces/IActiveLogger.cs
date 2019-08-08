using System;

namespace Core.Helpers.Logger.Interfaces
{
    /// <summary> An active logger for classes that can't inherit <see cref="LoggerFluency"/>. </summary>
    public interface IActiveLogger
    {
        #region Methods: Logging

        /// <summary> Creates a log entry of the "Trace" level for the current log category. </summary>
        /// <param name="message"> A message to supplement the log with. </param>
        void Trace(string message);

        /// <summary> Creates a log entry of the "Debug" level for the current log category. </summary>
        /// <param name="message"> A message to supplement the log with. </param>
        void Debug(string message);

        /// <summary> Creates a log entry of the "Info" level for the current log category. </summary>
        /// <param name="message"> A message to supplement the log with. </param>
        void Info(string message);

        /// <summary> Creates a log entry of the "Warn" level for the current log category. </summary>
        /// <param name="message"> A message to supplement the log with. </param>
        void Warn(string message);

        /// <summary> Creates a log entry of the "Error" level for the current log category. </summary>
        /// <param name="message"> A message to supplement the log with. </param>
        /// <param name="exception"> An exception whose data to log. </param>
        void Error(string message, Exception exception);

        /// <summary> Creates a log entry of the "Fatal" level for the current log category. </summary>
        /// <param name="message"> A message to supplement the log with. </param>
        /// <param name="exception"> An exception whose data to log. </param>
        void Fatal(string message, Exception exception);

        #endregion Methods: Logging
    }
}