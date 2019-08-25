using NLog.Common;
using NLog.Targets;
using System;

namespace Core.NLogExtensions.Targets
{
    /// <summary> A custom target made for WPF applications. </summary>
    [Target("WpfTarget")]
    public class WpfTarget : Target
    {
        /// <summary> The event of logging. </summary>
        public event Action<AsyncLogEventInfo> Log;

        /// <summary> Writes an asynchronous log event to the log target. </summary>
        /// <param name="logEvent"> The asynchronous Log event to write out. </param>
        protected override void Write(AsyncLogEventInfo logEvent)
        {
            base.Write(logEvent);

            Log?.Invoke(logEvent);
        }
    }
}