﻿using Core.Enumerations;
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
        private const bool _dev = false;

        private const string _commonPath = @"D:\";
        private const string _gamesPath = _commonPath + @"Games\";
        private const string _repositoriesPath = _commonPath + @"Code\Source\_Repositories\";
        private const string _trackerProjectName = "WarThunderJsonFileChanges";

        private static readonly string _warThunderPath = _gamesPath + (_dev ? @"War Thunder (Dev)\" : @"_Steam\steamapps\common\War Thunder\");
        private static readonly string _klensysWarThunderToolsPath = _commonPath + @"Software\War Thunder Tools";
        private static readonly string _trackerProjectPath = _repositoriesPath + (_dev ? $@"{_trackerProjectName}DevClient\" : $@"{_trackerProjectName}\");
        private static readonly string _copiedFilesPath = _trackerProjectPath + @"Files\";

        private static readonly IConfiguredLogger _logger = new ConfiguredNLogger(ELoggerName.ConsoleLogger, new ExceptionFormatter());
        private static readonly IFileManager _fileManager = new FileManager(_logger);
        private static readonly IFileReader _fileReader = new FileReader(_logger);
        private static readonly IParser _parser = new Parser(_logger);
        private static readonly IUnpacker _unpacker = new Unpacker(_fileManager, _logger);

        static void Main()
        {
            Settings.WarThunderLocation = _warThunderPath;
            Settings.KlensysWarThunderToolsLocation = _klensysWarThunderToolsPath;

            var sourceFiles = GetFilesFromGameDirectories(_warThunderPath);
            var yupFile = GetVersionInfoFile(sourceFiles);
            var binFiles = GetBinFiles(sourceFiles);
            var gameFileCopyDirectory = new DirectoryInfo(_copiedFilesPath);

            _fileManager.EmptyDirectory(gameFileCopyDirectory.FullName);

            AppendCurrentClientVersion(yupFile);
            CopyAndUnpackBinFiles(binFiles, gameFileCopyDirectory);
            RemoveCopiedSourceFiles(gameFileCopyDirectory);

            _logger.LogInfo(ECoreLogCategory.Empty, $"Procedure complete.");
        }

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

        private static FileInfo GetVersionInfoFile(IEnumerable<FileInfo> files)
        {
            _logger.LogInfo(ECoreLogCategory.Empty, $"Selecting the current YUP file...");

            var yupFile = files.First(file => file.Extension.Contains(EFileExtension.Yup) && !file.Name.Contains("old"));

            _logger.LogInfo(ECoreLogCategory.Empty, $"{yupFile.Name} found.");

            return yupFile;
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

        private static void AppendCurrentClientVersion(FileInfo yupFile)
        {
            _logger.LogInfo(ECoreLogCategory.Empty, $"Reading client version...");

            var currentVersion = _parser.GetClientVersion(_fileReader.Read(yupFile));

            _logger.LogInfo(ECoreLogCategory.Empty, $"Client is {currentVersion}.");
            _logger.LogInfo(ECoreLogCategory.Empty, $"Writing version to versions.txt...");

            using (var streamWriter = File.AppendText($@"{_trackerProjectPath}versions.txt"))
                streamWriter.WriteLine($"{yupFile.LastWriteTime.ToShortDateString()} - {currentVersion}");

            _logger.LogInfo(ECoreLogCategory.Empty, $"Version history appended.");
        }

        private static void CopyAndUnpackBinFiles(IEnumerable<FileInfo> sourceBinFiles, DirectoryInfo gameFileCopyDirectory)
        {
            foreach(var sourceFile in sourceBinFiles)
            {
                _logger.LogInfo(ECoreLogCategory.Empty, $"Unpacking {sourceFile.Name}...");

                var defaultTempLocation = Settings.TempLocation;
                Settings.TempLocation = gameFileCopyDirectory.FullName;

                _unpacker.Unpack(sourceFile);

                Settings.TempLocation = defaultTempLocation;

                _logger.LogInfo(ECoreLogCategory.Empty, $"Unpacked.");
            }

            var unpackedDirectories = gameFileCopyDirectory.GetDirectories();

            foreach (var unpackedDirectory in unpackedDirectories)
            {
                _logger.LogInfo(ECoreLogCategory.Empty, $"Converting BLK files from {unpackedDirectory.Name}...");

                _unpacker.Unpack(unpackedDirectory, ETool.BlkUnpacker);

                _logger.LogInfo(ECoreLogCategory.Empty, $"Converted.");
            }
        }

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
    }
}