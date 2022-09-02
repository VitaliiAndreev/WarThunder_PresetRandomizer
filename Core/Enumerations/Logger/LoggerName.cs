namespace Core.Enumerations.Logger
{
    /// <summary> Enumerates NLog logger names that correspond to rule names set up. </summary>
    public enum LoggerName
    {
        ConsoleLogger,
        FileLogger,
        /// <summary> Requires a custom target and rule loaded programmatically. </summary>
        WpfLogger,
    }
}