using Core.Enumerations;
using Core.Helpers;
using Core.Helpers.Interfaces;
using Core.Helpers.Logger.Interfaces;
using Core.UnpackingToolsIntegration.Enumerations;
using Core.UnpackingToolsIntegration.Helpers;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.WarThunderExtractionToolsIntegration;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace WarThunderSimpleUpdateChecker
{
    class App
    {
        private const string _commonPath = @"D:\";
        private const string _warThunderPath = _commonPath + @"Games\_Steam\steamapps\common\War Thunder\";
        private const string _trackerProject = _commonPath + @"Code\Source\_Repositories\\WarThunderJsonFileChanges\";

        private static readonly IFileManager _fileManager = new FileManager(new Mock<IConfiguredLogger>().Object);
        private static readonly IFileReader _fileReader = new FileReader(new Mock<IConfiguredLogger>().Object);
        private static readonly IParser _parser = new Parser(new Mock<IConfiguredLogger>().Object);
        private static readonly IUnpacker _unpacker = new Unpacker(new Mock<IConfiguredLogger>().Object, _fileManager);

        static void Main()
        {
            var sourceFiles = Directory
                .GetFiles(_warThunderPath)
                .Select(filePath => new FileInfo(filePath))
            ;
            var yupFile = sourceFiles.First(file => file.Extension.Contains(EFileExtension.Yup) && !file.Name.Contains("old"));
            var binFiles = sourceFiles.Where(file => file.Extension.Contains(EFileExtension.Bin));

            AppendCurrentClientVersion(yupFile);
            CopyAndUnpackBinFiles(binFiles);
        }

        private static void AppendCurrentClientVersion(FileInfo yupFile)
        {
            var currentVersion = _parser.GetClientVersion(_fileReader.Read(yupFile));

            using (var streamWriter = File.AppendText($@"{_trackerProject}versions.txt"))
                streamWriter.WriteLine($"{yupFile.LastWriteTime.ToShortDateString()} - {currentVersion}");
        }

        private static void CopyAndUnpackBinFiles(IEnumerable<FileInfo> sourceBinFiles)
        {
            var gameFileCopyDirectory = new DirectoryInfo($@"{_trackerProject}Files\");

            foreach(var sourceFile in sourceBinFiles)
            {
                _fileManager.CopyFile(sourceFile, gameFileCopyDirectory.FullName, true, true);

                var defaultTempLocation = Settings.TempLocation;
                Settings.TempLocation = gameFileCopyDirectory.FullName;

                _unpacker.Unpack(sourceFile);

                Settings.TempLocation = defaultTempLocation;
            }

            var unpackedDirectories = gameFileCopyDirectory.GetDirectories();

            foreach (var unpackedDirectory in unpackedDirectories)
                _unpacker.Unpack(unpackedDirectory, ETool.BlkUnpacker);

            var unwantedFiles = GetAllFiles(gameFileCopyDirectory, file => !file.Extension.Contains(EFileExtension.Blkx), true).ToList();

            Thread.Sleep(1000);
            for (var i = 0; i < unwantedFiles.Count(); i++)
                unwantedFiles[i].Delete();
        }

        private static IEnumerable<FileInfo> GetAllFiles(DirectoryInfo directory, Predicate<FileInfo> condition, bool includeSelf = false)
        {
            var files = new List<FileInfo>();
            if (includeSelf)
                files.AddRange(directory.GetFiles().Where(file => condition(file)));

            foreach (var subdirectory in directory.GetDirectories())
            {
                files.AddRange(subdirectory.GetFiles().Where(file => condition(file)));
                files.AddRange(GetAllFiles(subdirectory, condition));
            }
            return files;
        }
    }
}
