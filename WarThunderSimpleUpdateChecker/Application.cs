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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace WarThunderSimpleUpdateChecker
{
    class Application
    {
        #region Fields

        private static readonly IConfiguredLogger _logger = new ConfiguredNLogger(ELoggerName.ConsoleLogger, new ExceptionFormatter());
        private static readonly IFileManager _fileManager = new FileManager(_logger);
        private static readonly IFileReader _fileReader = new FileReader(_logger);
        private static readonly IParser _parser = new Parser(_logger);
        private static readonly IUnpacker _unpacker = new Unpacker(_fileManager, _logger);
        private static readonly IConverter _converter = new Converter(_logger);

        private static string _warThunderPath;
        private static string _warThunderToolsPath;
        private static string _outputPath;

        #endregion Fields
        #region Properties

        private static string OutputFilesPath => Path.Combine(_outputPath, "Files");

        #endregion Properties

        static void Main(params string[] args)
        {
            if (args.Count() != 3)
            {
                _logger.LogInfo(ECoreLogCategory.Empty, $"The app requires 3 arguments: path to War Thunder, path to Klensy's WT Tools, output path.");
                return;
            }

            _warThunderPath = args[0];
            _warThunderToolsPath = args[1];
            _outputPath = args[2];

            if (!PathsAreValid())
                return;

            InitialiseSettings();

            var sourceFiles = GetFilesFromGameDirectories(_warThunderPath);
            var yupFile = GetVersionInfoFile(sourceFiles);
            var binFiles = GetBinFiles(sourceFiles);
            var outputFilesDirectory = new DirectoryInfo(OutputFilesPath);

            RemoveFilesFromPreviousPatch(outputFilesDirectory);
            AppendCurrentClientVersion(yupFile);

            var unpackedDirectories = CopyAndUnpackBinFiles(binFiles, outputFilesDirectory);

            DecompressBlkFiles(unpackedDirectories);
            DecompressDdsxFiles(unpackedDirectories);
            RemoveCopiedSourceFiles(outputFilesDirectory);

            _logger.LogInfo(ECoreLogCategory.Empty, $"Procedure complete.");
        }

        #region Methods: Initialisation

        private static bool PathsAreValid()
        {
            if (!Directory.Exists(_warThunderPath))
            {
                _logger.LogInfo(ECoreLogCategory.Empty, $"Specified path to War Thunder doesn't exist.");
                return false;
            }
            if (!Directory.Exists(_warThunderToolsPath))
            {
                _logger.LogInfo(ECoreLogCategory.Empty, $"Specified path to Klensy's WT Tools doesn't exist.");
                return false;
            }
            if (!Directory.Exists(_warThunderToolsPath))
            {
                _logger.LogInfo(ECoreLogCategory.Empty, $"Specified output path doesn't exist.");
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
            _logger.LogInfo(ECoreLogCategory.Empty, $"Removing files from the previous patch.");
            {
                _fileManager.EmptyDirectory(outputFilesDirectory.FullName);
            }
            _logger.LogInfo(ECoreLogCategory.Empty, $"Files removed.");
        }

        #endregion Methods: Initialisation
        #region Methods: Working with Game Version

        private static FileInfo GetVersionInfoFile(IEnumerable<FileInfo> files)
        {
            _logger.LogInfo(ECoreLogCategory.Empty, $"Selecting the current YUP file...");

            var yupFile = files.First(file => file.Extension.Contains(EFileExtension.Yup) && !file.Name.Contains("old"));

            _logger.LogInfo(ECoreLogCategory.Empty, $"{yupFile.Name} found.");

            return yupFile;
        }

        private static void AppendCurrentClientVersion(FileInfo yupFile)
        {
            _logger.LogInfo(ECoreLogCategory.Empty, $"Reading client version...");

            var currentVersion = _parser.GetClientVersion(_fileReader.Read(yupFile));

            _logger.LogInfo(ECoreLogCategory.Empty, $"Client is {currentVersion}.");
            _logger.LogInfo(ECoreLogCategory.Empty, $"Writing version to versions.txt...");

            var pathToVersionsLog = Path.Combine(_outputPath, "versions.txt");

            if (!File.Exists(pathToVersionsLog))
                File.Create(pathToVersionsLog).Close();

            using (var streamWriter = File.AppendText(pathToVersionsLog))
                streamWriter.WriteLine($"{yupFile.LastWriteTime.ToShortDateString()} - {currentVersion}");

            _logger.LogInfo(ECoreLogCategory.Empty, $"Version history appended.");
        }

        #endregion Methods: Working with Game Version
        #region Methods: Selecting Files

        private static IEnumerable<FileInfo> GetFilesFromGameDirectories(string rootDirectoryPath)
        {
            var uiDirectoryPath = Path.Combine(rootDirectoryPath, "ui");

            _logger.LogInfo(ECoreLogCategory.Empty, $"Looking up files in \"{rootDirectoryPath}\" and \"{uiDirectoryPath}\"...");

            var filesInRootDirectory = Directory.GetFiles(rootDirectoryPath);
            var filesInUiDirectory = Directory.GetFiles(uiDirectoryPath);
            var sourceFiles = filesInRootDirectory
                .Concat(filesInUiDirectory)
                .Select(filePath => new FileInfo(filePath))
            ;

            _logger.LogInfo(ECoreLogCategory.Empty, $"{sourceFiles.Count()} found.");

            return sourceFiles;
        }

        private static IEnumerable<FileInfo> GetBinFiles(IEnumerable<FileInfo> files)
        {
            _logger.LogInfo(ECoreLogCategory.Empty, $"Selecting BIN files...");

            var excludedBinFileNames = new string[]
            {
                EFile.WarThunder.GuiParameters,
                EFile.WarThunder.WebUiParameters,
            };

            var binFiles = files.Where(file => file.GetExtensionWithoutPeriod() == EFileExtension.Bin && !file.Name.IsIn(excludedBinFileNames));

            _logger.LogInfo(ECoreLogCategory.Empty, $"{binFiles.Count()} found.");

            return binFiles;
        }

        #endregion Methods: Selecting Files
        #region Methods: Unpacking Files

        private static IEnumerable<DirectoryInfo> CopyAndUnpackBinFiles(IEnumerable<FileInfo> sourceBinFiles, DirectoryInfo gameFileCopyDirectory)
        {
            foreach (var sourceFile in sourceBinFiles)
            {
                _logger.LogInfo(ECoreLogCategory.Empty, $"Unpacking {sourceFile.Name}...");

                var defaultTempLocation = Settings.TempLocation;
                Settings.TempLocation = gameFileCopyDirectory.FullName;

                _unpacker.Unpack(sourceFile);

                Settings.TempLocation = defaultTempLocation;

                _logger.LogInfo(ECoreLogCategory.Empty, $"Unpacked.");
            }

            return gameFileCopyDirectory.GetDirectories();
        }

        #endregion Methods: Unpacking Files
        #region Methods: Decompressing Files

        private static void DecompressBlkFiles(IEnumerable<DirectoryInfo> unpackedDirectories)
        {
            foreach (var unpackedDirectory in unpackedDirectories)
            {
                _logger.LogInfo(ECoreLogCategory.Empty, $"Decompressing BLK files in {unpackedDirectory.Name}...");

                _unpacker.Unpack(unpackedDirectory, ETool.BlkUnpacker);

                _logger.LogInfo(ECoreLogCategory.Empty, $"Decompressed.");
            }
        }

        private static void DecompressDdsxFiles(IEnumerable<DirectoryInfo> unpackedDirectories)
        {
            var ddsxFilter = "*.ddsx";

            bool directoryContainsDdsxFiles(DirectoryInfo directrory) => directrory.GetFiles(ddsxFilter, SearchOption.AllDirectories).Any();

            foreach (var unpackedDirectory in unpackedDirectories)
            {
                if (!directoryContainsDdsxFiles(unpackedDirectory))
                    continue;

                _logger.LogInfo(ECoreLogCategory.Empty, $"Decompressing DDSX files and converting to PNG in {unpackedDirectory.Name}...");

                foreach (var subdirectory in unpackedDirectory.GetDirectories(SearchOption.AllDirectories).Including(unpackedDirectory))
                {
                    if (!directoryContainsDdsxFiles(subdirectory))
                        continue;

                    _unpacker.Unpack(subdirectory, ETool.DdsxUnpacker);
                    _converter.ConvertDdsToPng(subdirectory, SearchOption.AllDirectories);
                }

                _logger.LogInfo(ECoreLogCategory.Empty, $"Decompressed.");
            }
        }

        #endregion Methods: Decompressing Files
        #region Methods: Clean-up

        private static void RemoveCopiedSourceFiles(DirectoryInfo gameFileCopyDirectory)
        {
            _logger.LogInfo(ECoreLogCategory.Empty, $"Looking up leftover source files...");

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

            _logger.LogInfo(ECoreLogCategory.Empty, $"{unwantedFiles.Count()} found.");

            Thread.Sleep(1000);

            _logger.LogInfo(ECoreLogCategory.Empty, $"Deleting leftover source files...");

            for (var i = 0; i < unwantedFiles.Count(); i++)
                unwantedFiles[i].Delete();

            _logger.LogInfo(ECoreLogCategory.Empty, $"Deleted.");
        }

        #endregion Methods: Clean-up
    }
}