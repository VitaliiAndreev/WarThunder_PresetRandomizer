using Core.DataBase.Helpers.Interfaces;
using Core.DataBase.Objects.Interfaces;
using Core.Helpers;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using Moq;
using NLog;
using System.Collections.Generic;
using System.IO;

namespace Core.Tests
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

        /// <summary> A pre-set instance of a bare-bone mock data repository. Create a mock locally if you need to customize it. </summary>
        public static Mock<IDataRepository> MockDataRepository { get; private set; }

        #endregion Properties
        #region Constructors and Initialization

        /// <summary> The static constructor is used for initialization. </summary>
        static Presets()
        {
            UseLiveLogging = true;
            CleanUpLogs = true;

            InitializeLogger();
            InitializeMockDataRepository();
        }

        /// <summary> Creates a <see cref="Logger"/> instance. </summary>
        private static void InitializeLogger()
        {
            if (UseLiveLogging)
            {
                Logger = new ConfiguredNLogger(LoggerName.FileLogger, new ExceptionFormatter());
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

        /// <summary> Creates a <see cref="MockDataRepository"/> instance. </summary>
        private static void InitializeMockDataRepository()
        {
            var loggers = new List<IConfiguredLogger> { Logger };

            MockDataRepository = new Mock<IDataRepository>();
            MockDataRepository
                .Setup(dataRepository => dataRepository.Loggers)
                .Returns(loggers);
            MockDataRepository
                .Setup(dataRepository => dataRepository.NewObjects)
                .Returns(new List<IPersistentObject>());
        }

        #endregion Constructors and Initialization
        #region Clean Up

        /// <summary> Cleans up any files generated during unit test runs. </summary>
        public static void CleanUp()
        {
            if (UseLiveLogging)
            {
                while (LogManager.IsLoggingEnabled())
                    LogManager.DisableLogging();
            }

            DeleteDataBaseFiles();
            DeleteLogFiles();

            if (UseLiveLogging)
            {
                while (!LogManager.IsLoggingEnabled())
                    LogManager.EnableLogging();
            }
        }

        /// <summary> Deletes "sqlite3" database files. </summary>
        private static void DeleteDataBaseFiles()
        {
            var fileManager = new FileManager(Logger);
            fileManager.DeleteFiles(Directory.GetCurrentDirectory(), FileExtension.SqLite3);
        }

        /// <summary> Deletes "log" files. </summary>
        private static void DeleteLogFiles()
        {
            if (UseLiveLogging && CleanUpLogs)
            {
                var fileManager = new FileManager(Logger);
                fileManager.DeleteFiles($"{Directory.GetCurrentDirectory()}\\Logs", FileExtension.Log);
            }
        }

        #endregion Clean Up
    }
}