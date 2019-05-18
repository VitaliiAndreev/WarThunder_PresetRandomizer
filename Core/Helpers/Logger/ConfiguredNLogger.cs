using Core.Extensions;
using Core.Helpers.Logger.Enumerations;
using Core.Helpers.Logger.Interfaces;
using NLog;
using System;

namespace Core.Helpers.Logger
{
    /// <summary> A configured logger that wraps around an <see cref="ILogger"/> (of <see cref="NLog"/>). </summary>
    public class ConfiguredNLogger : IConfiguredLogger
    {
        #region Fields

        /// <summary> A format string used to customize log messages. </summary>
        private readonly string _messageFormat;

        /// <summary> The instance of a logger. </summary>
        private readonly ILogger _logger;

        #endregion Fields
        #region Properties

        /// <summary> An exception formatter. </summary>
        public IExceptionFormatter ExceptionFormatter { get; }

        #endregion Properties
        #region Constructors

        /// <summary> Creates and configures a new logger. </summary>
        /// <param name="exceptionFormatter"> An instance of an exception formatter. </param>
        public ConfiguredNLogger(ELoggerName loggerName, IExceptionFormatter exceptionFormatter)
        {
            ExceptionFormatter = exceptionFormatter;

            LogManager.Configuration.Variables[EVariableName.ConsoleLayout] = "${message}";
            LogManager.Configuration.Variables[EVariableName.FileLayout] = "${longdate:format=yyyy/MM/dd_HH:mm:ss} ${level:upperCase=true} / ${message}";
            LogManager.Configuration.Variables[EVariableName.FileName] = @"Logs\Log_${var:startTime}.log";
            LogManager.Configuration.Variables[EVariableName.StartTime] = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

            _logger = LogManager.GetLogger(loggerName.ToString());
            _messageFormat = "{0} : {1}{2}";

            LogDebug(ECoreLogCategory.Logger, ECoreLogMessage.Created.FormatFluently(_logger.Name));
        }

        #endregion Constructors
        #region Methods: Logging

        /// <summary> A wrapper around <see cref="ILoggerBase.Log(LogLevel, string, Exception)"/> that forms a customized message string before logging it. </summary>
        /// <param name="level"> A log level. </param>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        /// <param name="exception"> An exception whose data to log. </param>
        private void CreateLog(LogLevel level, string category, string message, Exception exception = null) =>
            _logger.Log(level, _messageFormat.FormatFluently(category, message, exception == null ? null : ExceptionFormatter.GetFormattedException(exception)));

        /// <summary> Creates a log entry of the "Trace" level. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        public void LogTrace(string category, string message) =>
            CreateLog(LogLevel.Trace, category, message);

        /// <summary> Creates a log entry of the "Debug" level. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        public void LogDebug(string category, string message) =>
            CreateLog(LogLevel.Debug, category, message);

        /// <summary> Creates a log entry of the "Info" level. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        public void LogInfo(string category, string message) =>
            CreateLog(LogLevel.Info, category, message);

        /// <summary> Creates a log entry of the "Warn" level. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        public void LogWarn(string category, string message) =>
            CreateLog(LogLevel.Warn, category, message);

        /// <summary> Creates a log entry of the "Error" level. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        /// <param name="exception"> An exception whose data to log. </param>
        public void LogError(string category, string message, Exception exception) =>
            CreateLog(LogLevel.Error, category, message, exception);

        /// <summary> Creates a log entry of the "Fatal" level. </summary>
        /// <param name="category"> The category of the event being logged. </param>
        /// <param name="message"> A message to supplement the log with. </param>
        /// <param name="exception"> An exception whose data to log. </param>
        public void LogFatal(string category, string message, Exception exception) =>
            CreateLog(LogLevel.Fatal, category, message, exception);

        #endregion Methods: Logging
    }
}
