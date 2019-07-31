using Core.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers.Interfaces;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using Core.UnpackingToolsIntegration.Enumerations;
using Core.UnpackingToolsIntegration.Exceptions;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.WarThunderExtractionToolsIntegration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Core.UnpackingToolsIntegration.Helpers
{
    /// <summary> Provides methods to unpack War Thunder files. </summary>
    public class Unpacker : LoggerFluency, IUnpacker
    {
        #region Constants

        private const string _outputDirectorySuffix = "_u";
        private const string _outputFileSuffix = "x";

        #endregion Constants
        #region Fields

        /// <summary> An instance of a file manager. </summary>
        private readonly IFileManager _fileManager;

        /// <summary> A map of unpacking tool file names onto file extensions. </summary>
        private readonly IDictionary<string, string> _toolFileNames;

        #endregion Fields
        #region Constructors

        /// <summary> Creates a new unpacker. </summary>
        /// <param name="fileManager"> An instance of a file manager. </param>
        /// <param name="loggers"> Instances of loggers. </param>
        public Unpacker(IFileManager fileManager, params IConfiguredLogger[] loggers)
            : base(ECoreLogCategory.Unpacker, loggers)
        {
            LogDebug(ECoreLogMessage.Created.FormatFluently(ECoreLogCategory.Unpacker));

            _fileManager = fileManager;
            _toolFileNames = new Dictionary<string, string>
            {
                { EFileExtension.Blk, ETool.BlkUnpacker },
                { EFileExtension.Clog, ETool.ClogUnpacker },
                { EFileExtension.Ddsx, ETool.DdsxUnpacker },
                { EFileExtension.Dxp, ETool.DxpUnpacker },
                { EFileExtension.Bin, ETool.VromfsBinUnpacker },
                { EFileExtension.Wrpl, ETool.WrplUnpacker },
            };
        }

        #endregion Constructors
        #region Methods: Fluency

        /// <summary> Creates a new instance of <see cref="FileInfo"/> with a file name targeting <see cref="Settings.UnpackingToolsLocation"/>. </summary>
        /// <param name="fileName"> The file name. </param>
        /// <returns></returns>
        private FileInfo GetToolFileInfo(string fileName) =>
            _fileManager.GetFileInfo(Settings.UnpackingToolsLocation, fileName);

        /// <summary> Creates a new instance of <see cref="FileInfo"/> with a file name targeting <see cref="Settings.TempLocation"/>. </summary>
        /// <param name="fileName"> The file name. </param>
        /// <returns></returns>
        private FileInfo GetTempFileInfo(string fileName) =>
            _fileManager.GetFileInfo(Settings.TempLocation, fileName);

        /// <summary> Runs a shell command that calls the specified tool with the given directory/file as a parameter. </summary>
        /// <param name="toolPath"> The path to the tool file. </param>
        /// <param name="argumentFilePath"> The path to the argument file. </param>
        /// <returns></returns>
        private Process RunShellCommand(string toolPath, string argumentFilePath)
        {
            var process = Process.Start(new ProcessStartInfo(EProcess.CommandShell, $"CMD /c \"\"{toolPath}\" \"{argumentFilePath}\"\""));
            process.WaitForExit();

            return process;
        }

        /// <summary> Logs a general error and rethrows the exception that caused it. </summary>
        /// <param name="exception"> The exception that has been thrown. </param>
        private void LogErrorAndRethrow(Exception exception)
        {
            LogError(ECoreLogMessage.ErrorRunningUnpackingTool, exception);
            throw exception;
        }

        #endregion Methods: Fluency

        /// <summary> Selects an appropriate unpacking tool file name (see <see cref="ETool"/>) for a given file extension. </summary>
        /// <param name="fileExtension"> The file extension to look for a match for. The register and period characters are ignored. </param>
        /// <returns></returns>
        private string GetToolFileNameByFileExtension(string fileExtension)
        {
            if (_toolFileNames.TryGetValue(fileExtension.ToLower().Except(new char[] { ECharacter.Period }).StringJoin(), out var toolFileName))
            {
                LogDebug(ECoreLogMessage.UnpackingToolSelected.FormatFluently(toolFileName));
                return toolFileName;
            }
            else
            {
                LogErrorAndThrow<FileExtensionNotSupportedException>
                (
                    ECoreLogMessage.FileExtensionNotSupportedByUnpackingTools.FormatFluently(fileExtension),
                    ECoreLogMessage.ErrorMatchingUnpakingToolToFileExtension
                );
                return null;
            }
        }

        /// <summary> Gets an output path for the specified file according to its extension. </summary>
        /// <param name="file"> The file for which to generate the output path. </param>
        /// <returns> A patched version of the output path. </returns>
        private string GetOutputPath(FileInfo file)
        {
            var outputPath = $@"{file.Directory}\{file.Name}";

            switch (file.Extension.Split(ECharacter.Period).Last().ToLower())
            {
                case EFileExtension.Bin:
                    {
                        outputPath = $"{outputPath}{_outputDirectorySuffix}";
                        var outputDirectory = new DirectoryInfo(outputPath);

                        if (!outputDirectory.Exists)
                            throw new OutputDirectoryNotFoundException(ECoreLogMessage.NotFound.FormatFluently(outputDirectory.FullName));

                        break;
                    }
                case EFileExtension.Blk:
                    {
                        outputPath = $"{outputPath}{_outputFileSuffix}";
                        var outputFile = new FileInfo(outputPath);

                        if (!outputFile.Exists)
                            throw new OutputFileNotFoundException(ECoreLogMessage.NotFound.FormatFluently(outputFile.FullName));

                        break;
                    }
                default:
                    {
                        throw new NotImplementedException(ECoreLogMessage.OutputPathGenerationForFileExtensionNotYetImplemented.FormatFluently(file.Extension));
                    }
            }
            return outputPath;
        }

        #region Method: Unpack()

        /// <summary> Copies the file to unpack into the <see cref="Settings.TempLocation"/> and unpacks it. </summary>
        /// <param name="sourceFile"> The file to unpack. </param>
        /// <param name="overwrite"> Whether to overwrite existing files. </param>
        /// <returns> The path to the output file / directory. </returns>
        public string Unpack(FileInfo sourceFile, bool overwrite = false)
        {
            // Preparing temp files.

            LogDebug(ECoreLogMessage.PreparingToUnpack.FormatFluently(sourceFile.FullName));

            _fileManager.CopyFile(sourceFile.FullName, Settings.TempLocation, overwrite, true);

            var toolFile = GetToolFileInfo(GetToolFileNameByFileExtension(sourceFile.Extension));
            var tempFile = GetTempFileInfo(sourceFile.Name);

            // Unpacking proper.

            LogDebug(ECoreLogMessage.Unpacking.FormatFluently(tempFile.Name));

            try
            {
                RunShellCommand(toolFile.FullName, tempFile.FullName);
                var outputPath = GetOutputPath(tempFile);

                LogDebug(ECoreLogMessage.Unpacked.FormatFluently(tempFile.Name));
                return outputPath;
            }
            catch (Exception exception)
            {
                LogErrorAndRethrow(exception);
                return null;
            }
        }

        /// <summary> Unpacks all valid files in the specified folder with the given tool. </summary>
        /// <param name="sourceDirectory"> The directory to unpack unpack files from. </param>
        /// <param name="toolName"> The name of the unpacking tool to use. </param>
        public void Unpack(DirectoryInfo sourceDirectory, string toolName)
        {
            LogDebug(ECoreLogMessage.Unpacking.FormatFluently(sourceDirectory.FullName));

            try
            {
                RunShellCommand(GetToolFileInfo(toolName).FullName, sourceDirectory.FullName);
                LogDebug(ECoreLogMessage.Unpacked.FormatFluently(sourceDirectory.Name));
            }
            catch (Exception exception)
            {
                LogErrorAndRethrow(exception);
            }
        }

        #endregion Method: Unpack()
    }
}