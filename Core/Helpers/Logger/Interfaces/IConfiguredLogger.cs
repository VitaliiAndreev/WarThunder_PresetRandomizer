using System;

namespace Core.Helpers.Logger.Interfaces
{
    /// <summary> A configured logger. </summary>
    public interface IConfiguredLogger
    {
        /// <summary> An exception formatter. </summary>
        IExceptionFormatter ExceptionFormatter { get; }

        /// <summary> Creates a log entry of the "Trace" level. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        void LogTrace(string category, string message);
        /// <summary> Creates a log entry of the "Debug" level. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        void LogDebug(string category, string message);
        /// <summary> Creates a log entry of the "Info" level. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        void LogInfo(string category, string message);
        /// <summary> Creates a log entry of the "Warn" level. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        void LogWarn(string category, string message);
        /// <summary> Creates a log entry of the "Error" level. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        /// <param name="exception"> An exception whose data to log. </param>
        void LogError(string category, string message, Exception exception);
        /// <summary> Creates a log entry of the "Fatal" level. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        /// <param name="exception"> An exception whose data to log. </param>
        void LogFatal(string category, string message, Exception exception);
    }
}
