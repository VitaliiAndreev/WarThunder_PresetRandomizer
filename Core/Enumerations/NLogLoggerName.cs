namespace Core
{
    /// <summary> Enumerates NLog logger names that correspond to rule names. </summary>
    public enum NLogLoggerName
    {
        ConsoleLogger,
        FileLogger,
        /// <summary> Requires a custom target and rule loaded programmatically. </summary>
        WpfLogger,
    }
}