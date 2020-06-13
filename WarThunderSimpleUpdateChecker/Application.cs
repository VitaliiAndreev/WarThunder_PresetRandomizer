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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using WarThunderSimpleUpdateChecker.Enumerations;

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
        private static string _fileListPath;
        private static bool _proceedOnNewVersions;
        private static bool _excludeGuiFiles;
        private static bool _disablePrompts;

        #endregion Fields
        #region Properties

        private static string OutputFilesPath => Path.Combine(_outputPath, "Files");
        private static string TempPath => Path.Combine(_warThunderToolsPath, "Temp");
        private static string VersionLogPath => Path.Combine(_outputPath, "versions.txt");

        #endregion Properties
        #region Constructors

        private Application() : base("Update Tracker", _fileLogger, _consoleLogger) { }

        #endregion Constructors

        static void Main(params string[] args)
        {
            if (args.Count() < 3)
            {
                _logger.LogInfo($"The first 3 arguments must be: path to War Thunder, path to Klensy's WT Tools, output path. Use \"-new\" to skip unpacking if the current version is the same as the previous one.");
                return;
            }

            var fileListPathArgumentMarker = "-files=";

            _warThunderPath = args[0];
            _warThunderToolsPath = args[1];
            _outputPath = args[2];
            _fileListPath = args.Where(argument => argument.StartsWith(fileListPathArgumentMarker)).FirstOrDefault() is string fileListPathArgument
                ? fileListPathArgument.Substring(fileListPathArgumentMarker.Count())
                : string.Empty;
            _proceedOnNewVersions = args.Contains("-new");
            _excludeGuiFiles = args.Contains("-nofrontend");
            _disablePrompts = args.Contains("-noprompt");

            if (!PathsAreValid())
            {
                PromptUserToConfirmExit();
                return;
            }

            InitialiseSettings();
            RemoveFilesFromDirectory(TempPath);

            var versionInfoFile = GetVersionInfoFile(_warThunderPath);

            var previousVersion = GetPreviousVersion();
            var currentClientVersion = GetVersion(versionInfoFile);
            var currentDataVersion = GetVersion(_warThunderPath, Data.FromRoot);
            var cachePath = GetCachePath(currentClientVersion);
            var currentCachedDataVersion = GetVersion(cachePath, Data.FromCache);
            var currentVersion = SelectFinalVersion(currentClientVersion, currentDataVersion, currentCachedDataVersion);

            if (previousVersion == currentVersion)
            {
                _logger.LogInfo($"The version number hasn't changed since the last unpacking session.");

                if (_proceedOnNewVersions)
                {
                    PromptUserToConfirmExit();
                    return;
                }
            }

            var outputFilesDirectory = new DirectoryInfo(OutputFilesPath);

            RemoveFilesFromDirectory(outputFilesDirectory);

            var sourceFilesInCache = GetFilesFromCacheDirectory(cachePath);
            var sourceFiles = GetFilesFromGameDirectories(_warThunderPath, sourceFilesInCache).ToList();
            var binFiles = GetBinFiles(sourceFiles).ToList();
            var unpackingTasks = StartCopyingAndUnpackingBinFiles(binFiles, outputFilesDirectory);

            DecompressFiles(binFiles, unpackingTasks);

            var mostRecentSourceFileWriteDate = GetLastWriteDate(sourceFiles);

            AppendCurrentClientVersion(currentVersion, mostRecentSourceFileWriteDate);
            
            var fileFilter = ApplyFileFilter(outputFilesDirectory).ToList();

            RemoveCopiedSourceFiles(outputFilesDirectory, fileFilter);

            _logger.LogInfo($"Procedure complete.");

            PromptUserToConfirmExit();
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
            if (!File.Exists(_fileListPath))
            {
                _logger.LogInfo($"Specified file list path doesn't exist.");
                return false;
            }
            return true;
        }

        private static void InitialiseSettings()
        {
            Settings.WarThunderLocation = _warThunderPath;
            Settings.KlensysWarThunderToolsLocation = _warThunderToolsPath;
        }

        private static void RemoveFilesFromDirectory(DirectoryInfo directory)
        {
            RemoveFilesFromDirectory(directory.FullName);
        }

        private static Version GetPreviousVersion()
        {
            if (!string.IsNullOrWhiteSpace(VersionLogPath))
            {
                if (!File.Exists(VersionLogPath))
                {
                    _logger.LogInfo($"\"{VersionLogPath}\" doesn't exist. No previous version is detected.");
                    return null;
                }

                _logger.LogInfo($"Reading \"{VersionLogPath}\"...");

                var text = File.ReadAllText(VersionLogPath).Trim();

                if (text.IsEmpty())
                {
                    _logger.LogInfo($"\"{VersionLogPath}\" is empty.");
                    return null;
                }

                try
                {
                    _logger.LogInfo($"Reading the previous version number...");

                    var lines = text.Split(ECharacter.NewLine, StringSplitOptions.RemoveEmptyEntries);
                    var versionText = lines.Last().Split(ECharacter.Minus).Last().Trim();
                    var version = new Version(versionText);

                    _logger.LogInfo($"The previous version is {version}.");

                    return version;
                }
                catch (Exception exception)
                {
                    _logger.LogInfo($"Failed to parse \"{VersionLogPath}\": {exception}");

                    return null;
                }
            }
            return null;
        }

        private static void RemoveFilesFromDirectory(string path)
        {
            _logger.LogInfo($"Removing files from the \"{path}\".");
            {
                _fileManager.EmptyDirectory(path);
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

        [SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "Implicit variable declarations.")]
        private static Version GetVersion(string path, Data source)
        {
            var tempDirectory = new DirectoryInfo(TempPath);
            var sourceFiles = GetFilesFrom(path, source);
            var version = default(Version);

            if (sourceFiles.Any())
            {
                var binFiles = GetBinFiles(sourceFiles.Values);
                var unpackingTasks = StartCopyingAndUnpackingBinFiles(binFiles, tempDirectory, true);

                Task.WaitAll(unpackingTasks.Values.ToArray());

                version = GetVersionFromUnpackedFiles(new DirectoryInfo[] { tempDirectory });
            }

            RemoveFilesFromDirectory(TempPath);

            return version;
        }

        public static Version GetVersionFromUnpackedFiles(IEnumerable<DirectoryInfo> unpackedDirectories)
        {
            _logger.LogInfo($"Reading versions from unpacked files...");

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

        public static Version SelectFinalVersion(Version clientVersion, Version dataVersion, Version cachedDataVersion)
        {
            _logger.LogInfo($"Selecting the latest version from: client - {clientVersion} / root - {dataVersion} / cache - {cachedDataVersion}.");

            var versions = new Version[] { clientVersion, dataVersion, cachedDataVersion }.Distinct();
            var latestVersion = versions.Max();

            _logger.LogInfo($"The latest version is {latestVersion}.");

            return latestVersion;
        }

        private static void AppendCurrentClientVersion(Version version, string updateTime)
        {
            _logger.LogInfo($"Writing the version number to versions.txt...");

            if (!File.Exists(VersionLogPath))
                File.Create(VersionLogPath).Close();

            using (var streamWriter = File.AppendText(VersionLogPath))
                streamWriter.WriteLine($"{updateTime} - {version}");

            _logger.LogInfo($"Version history appended.");
        }

        #endregion Methods: Working with Game Version
        #region Methods: Selecting Files

        private static IDictionary<string, FileInfo> GetFilesFrom(string path, Data source)
        {
            return source switch
            {
                Data.FromRoot => GetFilesFromRootDirectory(path),
                Data.FromCache => GetFilesFromCacheDirectory(path),
                _ => new Dictionary<string, FileInfo>(),
            };
        }

        private static string GetCachePath(Version clientVersion)
        {
            _logger.LogInfo($"Looking up the cache directory.");

            var cacheDirectoriesPath = Settings.CacheLocation;
            var cacheDirectoriesExist = Directory.Exists(cacheDirectoriesPath);
            var cacheDirectoryPath = default(string);

            if (cacheDirectoriesExist)
            {
                var cacheDirectories = Directory
                    .GetDirectories(cacheDirectoriesPath, $"binary.{clientVersion.ToString(EInteger.Number.Three)}*", SearchOption.TopDirectoryOnly)
                    .Select(path => new DirectoryInfo(path))
                    .OrderByDescending(directory => directory.LastWriteTimeUtc);

                if (cacheDirectories.HasSeveral()) throw new AmbiguousMatchException("Several cache directories matching the given version have been found. The developer of this automation app would like to know about this case and resolve the collision.");

                cacheDirectoryPath = cacheDirectories.First().FullName;

                _logger.LogInfo($"The cache is at \"{cacheDirectoryPath}\".");

                return cacheDirectoryPath;
            }

            _logger.LogInfo($"No cache directories found.");

            return null;
        }

        private static IDictionary<string, FileInfo> GetFilesFromCacheDirectory(string cacheDirectoryPath)
        {
            if (!string.IsNullOrWhiteSpace(cacheDirectoryPath))
            {
                _logger.LogInfo($"Looking up files in \"{cacheDirectoryPath}\"...");

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

        private static IDictionary<string, FileInfo> GetFilesFromRootDirectory(string rootDirectoryPath)
        {
            var uiDirectoryPath = Path.Combine(rootDirectoryPath, "ui");

            _logger.LogInfo($"Looking up files in \"{rootDirectoryPath}\"...");

            var filesInRootDirectory = Directory.GetFiles(rootDirectoryPath);
            var filesInUiDirectory = Directory.GetFiles(uiDirectoryPath);
            var sourceFiles = filesInRootDirectory
                .Concat(filesInUiDirectory)
                .ToDictionary(filePath => filePath, filePath => new FileInfo(filePath))
            ;

            _logger.LogInfo($"{sourceFiles.Count()} found.");

            return sourceFiles;
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

            var excludedBinFileNames = new List<string>();

            if (_excludeGuiFiles)
            {
                var guiBinFileNames = new string[]
                {
                    EFile.WarThunder.GuiParameters,
                    EFile.WarThunder.WebUiParameters,
                };

                excludedBinFileNames.AddRange(guiBinFileNames);
            }

            var binFiles = files.Where(file => file.GetExtensionWithoutPeriod() == EFileExtension.Bin && !file.Name.IsIn(excludedBinFileNames));

            _logger.LogInfo($"{binFiles.Count()} found.");

            return binFiles;
        }

        #endregion Methods: Selecting Files
        #region Methods: Unpacking Files

        private static IDictionary<string, Task<DirectoryInfo>> StartCopyingAndUnpackingBinFiles(IEnumerable<FileInfo> binFiles, DirectoryInfo gameFileCopyDirectory, bool hideFileLogs = false)
        {
            return StartCopyingAndUnpackingBinFiles(binFiles, gameFileCopyDirectory.FullName, hideFileLogs);
        }

        private static IDictionary<string, Task<DirectoryInfo>> StartCopyingAndUnpackingBinFiles(IEnumerable<FileInfo> binFiles, string path, bool hideFileLogs = false)
        {
            var tasks = new Dictionary<string, Task<DirectoryInfo>>();

            foreach (var binFile in binFiles)
            {
                DirectoryInfo unpack()
                {
                    if (hideFileLogs)
                        _logger.LogDebug($"Unpacking \"{binFile.Name}\"...");
                    else
                        _logger.LogInfo($"Unpacking \"{binFile.Name}\"...");

                    var defaultTempLocation = Settings.TempLocation;
                    Settings.TempLocation = path;

                    var outputPath = _unpacker.Unpack(binFile);

                    Settings.TempLocation = defaultTempLocation;

                    if (hideFileLogs)
                        _logger.LogDebug($"\"{binFile.Name}\" unpacked.");
                    else
                        _logger.LogInfo($"\"{binFile.Name}\" unpacked.");

                    return new DirectoryInfo(outputPath);
                }
                var unpackTask = _taskFactory.StartNew(unpack);

                tasks.Add($"{binFile.Name}", unpackTask);
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

        private static IEnumerable<string> ApplyFileFilter(DirectoryInfo gameFileCopyDirectory)
        {
            var fileNames = new List<string>();

            if (!string.IsNullOrWhiteSpace(_fileListPath) && File.Exists(_fileListPath))
            {
                _logger.LogInfo($"Reading the file name list to be filtered in...");

                fileNames.AddRange(File.ReadAllLines(_fileListPath));

                _logger.LogInfo($"{fileNames.Count()} found.");
                _logger.LogInfo($"Looking up filtered out files...");

                var unwantedFiles = gameFileCopyDirectory.GetFiles(file => !file.Name.IsIn(fileNames), SearchOption.AllDirectories).ToList();

                _logger.LogInfo($"{unwantedFiles.Count()} found.");

                if (unwantedFiles.Any())
                {
                    Thread.Sleep(1000);

                    _logger.LogInfo($"Deleting filtered out files...");

                    for (var i = 0; i < unwantedFiles.Count(); i++)
                        unwantedFiles[i].Delete();

                    _logger.LogInfo($"Filtered out files deleted.");
                }
            }

            return fileNames;
        }

        private static void RemoveCopiedSourceFiles(DirectoryInfo gameFileCopyDirectory, IEnumerable<string> fileNames)
        {
            _logger.LogInfo($"Looking up leftover source files...");

            var unwantedFileExtensions = new List<string>
            {
                EFileExtension.Bin,
                EFileExtension.Blk,
            };

            if (_excludeGuiFiles)
            {
                var frontendFileExtensions = new string[]
                {
                    EFileExtension.Css,
                    EFileExtension.Html,
                    EFileExtension.Js,
                    EFileExtension.Nut,
                    EFileExtension.Tpl,
                };

                unwantedFileExtensions.AddRange(frontendFileExtensions);
            }

            var unwantedFiles = gameFileCopyDirectory
                .GetFiles(file => !string.IsNullOrWhiteSpace(file.Extension) && file.GetExtensionWithoutPeriod().IsIn(unwantedFileExtensions) && !file.Name.IsIn(fileNames), SearchOption.AllDirectories)
                .ToList()
            ;

            _logger.LogInfo($"{unwantedFiles.Count()} found.");

            if (unwantedFiles.Any())
            {
                Thread.Sleep(1000);

                _logger.LogInfo($"Deleting leftover source files...");

                for (var i = 0; i < unwantedFiles.Count(); i++)
                    unwantedFiles[i].Delete();

                _logger.LogInfo($"Leftover source files deleted.");
            }
        }

        #endregion Methods: Clean-up

        private static void PromptUserToConfirmExit()
        {
            if (_disablePrompts)
                return;

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey(true);
        }
    }
}