using Core.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers;
using Core.Helpers.Interfaces;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using Core.UnpackingToolsIntegration.Enumerations;
using Core.UnpackingToolsIntegration.Helpers;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.WarThunderExtractionToolsIntegration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace WarThunderSimpleUpdateChecker
{
    class Application : LoggerFluency
    {
        #region Fields

        private static readonly IConfiguredLogger _fileLogger = new ConfiguredNLogger(ELoggerName.FileLogger, new ExceptionFormatter());
        private static readonly IConfiguredLogger _consoleLogger = new ConfiguredNLogger(ELoggerName.ConsoleLogger, new ExceptionFormatter());
        private static readonly Application _logger = new Application();
        private static readonly IFileManager _fileManager = new FileManager(_fileLogger, _consoleLogger);
        private static readonly IFileReader _fileReader = new FileReader(_fileLogger, _consoleLogger);
        private static readonly IParser _parser = new Parser(_fileLogger, _consoleLogger);
        private static readonly IUnpacker _unpacker = new Unpacker(_fileManager, _fileLogger, _consoleLogger);
        private static readonly IConverter _converter = new Converter(_fileLogger, _consoleLogger);
        private static readonly TaskFactory _taskFactory = new TaskFactory();

        private static string _warThunderPath;
        private static string _warThunderToolsPath;
        private static string _outputPath;

        #endregion Fields
        #region Properties

        private static string OutputFilesPath => Path.Combine(_outputPath, "Files");

        #endregion Properties
        #region Constructors

        private Application() : base("Update Tracker", _fileLogger, _consoleLogger) { }

        #endregion Constructors

        static void Main(params string[] args)
        {
            if (args.Count() != 3)
            {
                _logger.LogInfo($"The app requires 3 arguments: path to War Thunder, path to Klensy's WT Tools, output path.");
                return;
            }

            _warThunderPath = args[0];
            _warThunderToolsPath = args[1];
            _outputPath = args[2];

            if (!PathsAreValid())
                return;

            InitialiseSettings();

            var outputFilesDirectory = new DirectoryInfo(OutputFilesPath);

            RemoveFilesFromPreviousPatch(outputFilesDirectory);

            var versionInfoFile = GetVersionInfoFile(_warThunderPath);
            var currentVersion = GetVersion(versionInfoFile);
            var sourceFilesInCache = GetFilesFromCacheDirectories(currentVersion.ToString(EInteger.Number.Three));
            var sourceFiles = GetFilesFromGameDirectories(_warThunderPath, sourceFilesInCache);
            var binFiles = GetBinFiles(sourceFiles);
            var unpackingTasks = StartCopyingAndUnpackingBinFiles(binFiles, outputFilesDirectory);
            var outputDirectories = DecompressFiles(binFiles, unpackingTasks);

            currentVersion = SelectFinalVersion(currentVersion, GetVersionFromUnpackedFiles(outputDirectories));

            var mostRecentSourceFileWriteDate = GetLastWriteDate(sourceFiles);

            AppendCurrentClientVersion(currentVersion, mostRecentSourceFileWriteDate);
            RemoveCopiedSourceFiles(outputFilesDirectory);

            _logger.LogInfo($"Procedure complete.");

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey(true);
        }

        #region Methods: Initialisation

        private static bool PathsAreValid()
        {
            if (!Directory.Exists(_warThunderPath))
            {
                _logger.LogInfo($"Specified path to War Thunder doesn't exist.");
                return false;
            }
            if (!Directory.Exists(_warThunderToolsPath))
            {
                _logger.LogInfo($"Specified path to Klensy's WT Tools doesn't exist.");
                return false;
            }
            if (!Directory.Exists(_warThunderToolsPath))
            {
                _logger.LogInfo($"Specified output path doesn't exist.");
                return false;
            }
            return true;
        }

        private static void InitialiseSettings()
        {
            Settings.WarThunderLocation = _warThunderPath;
            Settings.KlensysWarThunderToolsLocation = _warThunderToolsPath;
        }

        private static void RemoveFilesFromPreviousPatch(DirectoryInfo outputFilesDirectory)
        {
            _logger.LogInfo($"Removing files from the previous patch.");
            {
                _fileManager.EmptyDirectory(outputFilesDirectory.FullName);
            }
            _logger.LogInfo($"Files removed.");
        }

        #endregion Methods: Initialisation
        #region Methods: Working with Game Version

        private static FileInfo GetVersionInfoFile(string warThunderPath)
        {
            _logger.LogInfo($"Selecting the current YUP file...");

            var yupFile = new FileInfo(Directory.GetFiles(warThunderPath, "*.yup", SearchOption.TopDirectoryOnly).First(fullName => !fullName.Contains("old")));

            _logger.LogInfo($"\"{yupFile.Name}\" found.");

            return yupFile;
        }

        private static Version GetVersion(FileInfo yupFile)
        {
            _logger.LogInfo($"Reading the client version...");

            var currentVersion = _parser.GetClientVersion(_fileReader.Read(yupFile));

            _logger.LogInfo($"The client is {currentVersion}.");

            return currentVersion;
        }

        public static Version GetVersionFromUnpackedFiles(IEnumerable<DirectoryInfo> unpackedDirectories)
        {
            _logger.LogInfo($"Reading client versions from unpacked files...");

            var versionFiles = unpackedDirectories
                .SelectMany(directory => directory.GetFiles("version", SearchOption.AllDirectories))
            ;
            var versions = versionFiles
                .Select(file => File.ReadAllText(file.FullName).Trim())
                .Select(versionText => new Version(versionText))
                .Distinct()
                .OrderBy(version => version)
            ;
            var latestVersion = versions.Last();

            _logger.LogInfo($"{versions.Count()} found: {versions.StringJoin(ESeparator.CommaAndSpace)}. The latest one is {latestVersion}...");

            return latestVersion;
        }

        public static Version SelectFinalVersion(Version clientVersion, Version dataVersion)
        {
            if (dataVersion > clientVersion)
            {
                _logger.LogInfo($"Unpacked files have the latest version...");
                return dataVersion;
            }
            else if (dataVersion < clientVersion)
            {
                _logger.LogInfo($"The client version is the latest...");
            }

            return clientVersion;
        }

        private static void AppendCurrentClientVersion(Version version, string updateTime)
        {
            _logger.LogInfo($"Writing the version number to versions.txt...");

            var pathToVersionsLog = Path.Combine(_outputPath, "versions.txt");

            if (!File.Exists(pathToVersionsLog))
                File.Create(pathToVersionsLog).Close();

            using (var streamWriter = File.AppendText(pathToVersionsLog))
                streamWriter.WriteLine($"{updateTime} - {version}");

            _logger.LogInfo($"Version history appended.");
        }

        #endregion Methods: Working with Game Version
        #region Methods: Selecting Files

        private static IDictionary<string, FileInfo> GetFilesFromCacheDirectories(string cacheDirectorySuffix)
        {
            var cacheDirectoriesPath = Settings.CacheLocation;
            var cacheDirectoriesExist = Directory.Exists(cacheDirectoriesPath);
            var cacheDirectoryPath = default(string);

            if (cacheDirectoriesExist)
            {
                _logger.LogInfo($"Looking up files in \"{cacheDirectoryPath}\"...");

                var cacheDirectories = Directory
                    .GetDirectories(cacheDirectoriesPath, $"binary.{cacheDirectorySuffix}*", SearchOption.TopDirectoryOnly)
                    .Select(path => new DirectoryInfo(path))
                    .OrderByDescending(directory => directory.LastWriteTimeUtc);

                if (cacheDirectories.HasSeveral()) throw new AmbiguousMatchException("Several cache directories matching the given version have been found. The developer of this automation app would like to know about this case and resolve the collision.");

                cacheDirectoryPath = cacheDirectories.First().FullName;

                var filesInCache = Directory
                    .GetFiles(cacheDirectoryPath)
                    .Select(filePath => new FileInfo(filePath))
                    .ToDictionary(file => file.Name);

                _logger.LogInfo($"{filesInCache.Count()} found.");

                return filesInCache;
            }

            _logger.LogInfo($"No files found in the cache.");

            return new Dictionary<string, FileInfo>();
        }

        private static IEnumerable<FileInfo> GetFilesFromGameDirectories(string rootDirectoryPath, IDictionary<string, FileInfo> sourceFilesInCache)
        {
            var uiDirectoryPath = Path.Combine(rootDirectoryPath, "ui");

            _logger.LogInfo($"Looking up files in \"{rootDirectoryPath}\" and \"{uiDirectoryPath}\"...");

            var filesInRootDirectory = Directory.GetFiles(rootDirectoryPath);
            var filesInUiDirectory = Directory.GetFiles(uiDirectoryPath);
            var sourceFiles = filesInRootDirectory
                .Concat(filesInUiDirectory)
                .Select(filePath => new FileInfo(filePath))
                .ToList()
            ;

            if (sourceFilesInCache.Any())
            {
                for (var fileIndex = EInteger.Number.Zero; fileIndex < sourceFiles.Count(); fileIndex++)
                {
                    var sourceFile = sourceFiles[fileIndex];
                    var sourceFileName = sourceFile.Name;

                    if (sourceFilesInCache.TryGetValue(sourceFileName, out var cachedSourceFile))
                    {
                        if (cachedSourceFile.LastWriteTimeUtc > sourceFile.LastWriteTimeUtc)
                            sourceFiles[fileIndex] = cachedSourceFile;

                        sourceFilesInCache.Remove(sourceFileName);
                    }
                }

                sourceFiles.AddRange(sourceFilesInCache.Values);
            }

            _logger.LogInfo($"{sourceFiles.Count()} found.");

            return sourceFiles;
        }

        private static string GetLastWriteDate(IEnumerable<FileInfo> files)
        {
            _logger.LogInfo($"Selecting the most recent edit...");

            var latestWriteDate = files.Select(file => file.LastWriteTimeUtc).OrderBy(time => time).Last().ToShortDateString();

            _logger.LogInfo($"The latest edit has been done on {latestWriteDate}.");

            return latestWriteDate;
        }

        private static IEnumerable<FileInfo> GetBinFiles(IEnumerable<FileInfo> files)
        {
            _logger.LogInfo($"Selecting BIN files...");

            var excludedBinFileNames = new string[]
            {
                EFile.WarThunder.GuiParameters,
                EFile.WarThunder.WebUiParameters,
            };

            var binFiles = files.Where(file => file.GetExtensionWithoutPeriod() == EFileExtension.Bin && !file.Name.IsIn(excludedBinFileNames));

            _logger.LogInfo($"{binFiles.Count()} found.");

            return binFiles;
        }

        #endregion Methods: Selecting Files
        #region Methods: Unpacking Files

        private static IDictionary<string, Task<DirectoryInfo>> StartCopyingAndUnpackingBinFiles(IEnumerable<FileInfo> sourceBinFiles, DirectoryInfo gameFileCopyDirectory)
        {
            var tasks = new Dictionary<string, Task<DirectoryInfo>>();

            foreach (var sourceFile in sourceBinFiles)
            {
                DirectoryInfo unpack()
                {
                    _logger.LogInfo($"Unpacking \"{sourceFile.Name}\"...");

                    var defaultTempLocation = Settings.TempLocation;
                    Settings.TempLocation = gameFileCopyDirectory.FullName;

                    var outputPath = _unpacker.Unpack(sourceFile);

                    Settings.TempLocation = defaultTempLocation;

                    _logger.LogInfo($"\"{sourceFile.Name}\" unpacked.");

                    return new DirectoryInfo(outputPath);
                }
                var unpackTask = _taskFactory.StartNew(unpack);

                tasks.Add($"{sourceFile.Name}", unpackTask);
            }
            return tasks;
        }

        #endregion Methods: Unpacking Files
        #region Methods: Decompressing Files

        private static IEnumerable<DirectoryInfo> DecompressFiles(IEnumerable<FileInfo> binFiles, IDictionary<string, Task<DirectoryInfo>> unpackingTasks)
        {
            var decompressBlkTasks = new Dictionary<string, Task>();
            var decompressDdsxTasks = new Dictionary<string, Task>();
            var outputDirectories = new List<DirectoryInfo>();

            while (unpackingTasks.Any())
            {
                foreach (var binFile in binFiles)
                {
                    var fileName = binFile.Name;

                    if (unpackingTasks.TryGetValue(fileName, out var startedUnpackingTask))
                    {
                        if (startedUnpackingTask.IsCompleted)
                        {
                            var outputDirectory = startedUnpackingTask.Result;

                            if (decompressBlkTasks.TryGetValue(fileName, out var startedDecompressBlkTask))
                            {
                                if (startedDecompressBlkTask.IsCompleted)
                                {
                                    if (decompressDdsxTasks.TryGetValue(fileName, out var startedDecompressDdsxTask))
                                    {
                                        if (startedDecompressDdsxTask.IsCompleted)
                                        {
                                            outputDirectories.Add(outputDirectory);

                                            unpackingTasks.Remove(fileName);
                                            decompressBlkTasks.Remove(fileName);
                                            decompressDdsxTasks.Remove(fileName);
                                        }
                                    }
                                    else
                                    {
                                        void decomplressDdsx() => DecompressDdsxFiles(outputDirectory);

                                        var decompressDdsxTask = _taskFactory.StartNew(decomplressDdsx);

                                        decompressDdsxTasks.Add(fileName, decompressDdsxTask);
                                    }
                                }
                            }
                            else
                            {
                                void decomplressBlk() => DecompressBlkFiles(outputDirectory);

                                var decompressBlkTask = _taskFactory.StartNew(decomplressBlk);

                                decompressBlkTasks.Add(fileName, decompressBlkTask);
                            }
                        }
                    }
                }
            }

            return outputDirectories;
        }

        private static void DecompressBlkFiles(DirectoryInfo unpackedDirectory)
        {
            _logger.LogInfo($"Decompressing BLK files in \"{unpackedDirectory.Name}\"...");

            _unpacker.Unpack(unpackedDirectory, ETool.BlkUnpacker);

            _logger.LogInfo($"BLK files in \"{unpackedDirectory.Name}\" decompressed.");
        }

        private static void DecompressDdsxFiles(DirectoryInfo unpackedDirectory)
        {
            var ddsxFilter = "*.ddsx";

            bool directoryContainsDdsxFiles(DirectoryInfo directrory) => directrory.GetFiles(ddsxFilter, SearchOption.AllDirectories).Any();

            if (!directoryContainsDdsxFiles(unpackedDirectory))
                return;

            _logger.LogInfo($"Decompressing DDSX files and converting to PNG in \"{unpackedDirectory.Name}\"...");

            foreach (var subdirectory in unpackedDirectory.GetDirectories(SearchOption.AllDirectories).Including(unpackedDirectory))
            {
                if (!directoryContainsDdsxFiles(subdirectory))
                    continue;

                var retryAttempts = EInteger.Number.Ten;
                var retryAttempt = EInteger.Number.One;

                void Try(Action method)
                {
                    try
                    {
                        method();
                    }
                    catch (Exception exception)
                    {
                        _logger.LogErrorSilently($"Error processing files in \"{subdirectory.FullName}\".", exception);

                        if (retryAttempt < retryAttempts)
                        {
                            retryAttempt++;

                            Thread.Sleep(EInteger.Time.MillisecondsInSecond);
                            Try(method);
                        }
                        else
                        {
                            throw new IOException($"After {retryAttempts} attempts {method} couldn't process \"{subdirectory.FullName}\".", exception);
                        }
                    }
                }

                Try(() => _unpacker.Unpack(subdirectory, ETool.DdsxUnpacker));
                Try(() => _converter.ConvertDdsToPng(subdirectory, SearchOption.AllDirectories));
            }

            _logger.LogInfo($"DDSX files in \"{unpackedDirectory.Name}\" decompressed and converted.");
        }

        #endregion Methods: Decompressing Files
        #region Methods: Clean-up

        private static void RemoveCopiedSourceFiles(DirectoryInfo gameFileCopyDirectory)
        {
            _logger.LogInfo($"Looking up leftover source files...");

            var unwantedFileExtensions = new string[]
            {
                EFileExtension.Bin,
                EFileExtension.Blk,
                EFileExtension.Css,
                EFileExtension.Html,
                EFileExtension.Js,
                EFileExtension.Nut,
                EFileExtension.Tpl,
            };
            var unwantedFiles = gameFileCopyDirectory.GetFiles(file => !string.IsNullOrWhiteSpace(file.Extension) && file.GetExtensionWithoutPeriod().IsIn(unwantedFileExtensions), SearchOption.AllDirectories).ToList();

            _logger.LogInfo($"{unwantedFiles.Count()} found.");

            Thread.Sleep(1000);

            _logger.LogInfo($"Deleting leftover source files...");

            for (var i = 0; i < unwantedFiles.Count(); i++)
                unwantedFiles[i].Delete();

            _logger.LogInfo($"Leftover source files deleted.");
        }

        #endregion Methods: Clean-up
    }
}