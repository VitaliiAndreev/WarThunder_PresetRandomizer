using Core.Enumerations;
using Core.Helpers;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using Moq;
using NLog;
using System.IO;

namespace Core.DataBase.Tests
{
    /// <summary> Presets for unit tests. </summary>
    public static class Presets
    {
        #region Properties

        /// <summary> Determines whether to instantiate an actual logger implementation instead of a mock. </summary>
        public static bool UseLiveLogging { get; }

        /// <summary> Determines whether to clean up generated log files. </summary>
        public static bool CleanUpLogs { get; }

        /// <summary> An instance of a logger. </summary>
        public static IConfiguredLogger Logger { get; private set; }

        #endregion Properties
        #region Constructors and Initialization

        /// <summary> A static constructor is used for initialization. </summary>
        static Presets()
        {
            UseLiveLogging = true;
            CleanUpLogs = false;

            InitializeLogger();
        }

        /// <summary> Creates a <see cref="Logger"/> instance. </summary>
        private static void InitializeLogger()
        {
            if (UseLiveLogging)
            {
                Logger = new ConfiguredLogger(new ExceptionFormatter());
            }
            else
            {
                var exceptionFormatter = new Mock<IExceptionFormatter>();
                var mockLogger = new Mock<IConfiguredLogger>();

                mockLogger
                    .Setup(logger => logger.ExceptionFormatter)
                    .Returns(exceptionFormatter.Object);

                Logger = mockLogger.Object;
            }
        }

        #endregion Constructors and Initialization
        #region Clean Up

        /// <summary> Cleans up any files generated during unit test runs. </summary>
        public static void CleanUp()
        {
            if (UseLiveLogging)
                while (LogManager.IsLoggingEnabled())
                    LogManager.DisableLogging();

            DeleteDataBaseFiles();
            DeleteLogFiles();

            if (UseLiveLogging)
                while (!LogManager.IsLoggingEnabled())
                    LogManager.EnableLogging();
        }

        /// <summary> Deletes "sqlite3" database files. </summary>
        private static void DeleteDataBaseFiles()
        {
            var fileManager = new FileManager(Logger);
            fileManager.DeleteFiles(Directory.GetCurrentDirectory(), EFileExtension.SqLite3);
        }

        /// <summary> Deletes "log" files. </summary>
        private static void DeleteLogFiles()
        {
            if (UseLiveLogging && CleanUpLogs)
            {
                var fileManager = new FileManager(Logger);
                fileManager.DeleteFiles($"{Directory.GetCurrentDirectory()}\\Logs", EFileExtension.Log);
            }
        }

        #endregion Clean Up
    }
}
