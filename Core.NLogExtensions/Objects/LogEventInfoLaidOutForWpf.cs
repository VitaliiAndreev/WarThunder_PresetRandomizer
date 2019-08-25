using NLog;

namespace Core.NLogExtensions.Objects
{
    /// <summary> A wrapper for <see cref="NLog.LogEventInfo"/> with a pre-set layout. </summary>
    public class LogEventInfoLaidOutForWpf
    {
        #region Properties

        /// <summary> The internal instance of the log event information. </summary>
        public LogEventInfo LogEventInfo { get; }
        
        /// <summary> A ready-to-to message formed using a pre-set layout. </summary>
        public string LaidOutMessage => $"{LogEventInfo.TimeStamp.TimeOfDay} {LogEventInfo.Level.Name.ToUpper()} / {LogEventInfo.FormattedMessage}";

        #endregion Properties
        #region Constructors

        /// <summary> Creates a new wrapper for <see cref="LogEventInfo"/> with a pre-set layout. </summary>
        /// <param name="logEventInfo"> The internal instance of the log event information. </param>
        public LogEventInfoLaidOutForWpf(LogEventInfo logEventInfo)
        {
            LogEventInfo = logEventInfo;
        }

        #endregion Constructors
    }
}