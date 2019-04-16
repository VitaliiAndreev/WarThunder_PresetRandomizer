using Core.Enumerations;
using Core.Extensions;
using Core.Helpers.Interfaces;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Enumerations;
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

        #endregion Constants
        #region Fields

        /// <summary> An instance of a file manager. </summary>
        private readonly IFileManager _fileManager;

        /// <summary> A map of unpacking tool file names onto file extensions. </summary>
        private readonly Dictionary<string, string> _toolFileNames;

        #endregion Fields
        #region Constructors

        /// <summary> Creates a new unpacker. </summary>
        /// <param name="logger"> An instance of a logger. </param>
        /// <param name="fileManager"> An instance of a file manager. </param>
        public Unpacker(IConfiguredLogger logger, IFileManager fileManager)
            : base(logger, ECoreLogCategory.Unpacker)
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

        /// <summary> Copies files required for unpacking into the temp directory. </summary>
        /// <param name="unpackingToolFile"> The file used as an unpacking tool. </param>
        /// <param name="targetFile"> The file to unpack. </param>
        private void CopyFilesIntoTempDirectory(FileInfo unpackingToolFile, FileInfo targetFile)
        {
            _fileManager.CopyFile(unpackingToolFile.FullName, Settings.TempLocation);
            _fileManager.CopyFile(targetFile.FullName, Settings.TempLocation);
        }

        /// <summary> Copies both the unpacking tool (because the output folder is relative to it) and the file to unpack into the <see cref="Settings.TempLocation"/> and unpacks the file. </summary>
        /// <param name="sourceFile"> The file to unpack. </param>
        /// <returns></returns>
        public DirectoryInfo Unpack(FileInfo sourceFile)
        {
            // Preparing temp files.

            LogDebug(ECoreLogMessage.PreparingToUnpack.FormatFluently(sourceFile.Name));

            var toolFileName = GetToolFileNameByFileExtension(sourceFile.Extension);

            CopyFilesIntoTempDirectory(GetToolFileInfo(toolFileName), sourceFile);

            var tempToolFile = GetTempFileInfo(toolFileName);
            var tempFile = GetTempFileInfo(sourceFile.Name);

            // Unpacking proper.

            LogDebug(ECoreLogMessage.Unpacking.FormatFluently(tempFile.Name));

            try
            {
                Process.Start(new ProcessStartInfo(tempToolFile.FullName, tempFile.FullName));

                var outputDirectory = new DirectoryInfo($@"{tempFile.Directory}\{tempFile.Name}{_outputDirectorySuffix}");

                if (outputDirectory.Exists)
                {
                    LogDebug(ECoreLogMessage.Unpacked.FormatFluently(tempFile.Name));
                    return outputDirectory;
                }
                else
                {
                    throw new OutputDirectoryNotFoundException(ECoreLogMessage.DoesNotExist.FormatFluently(outputDirectory.FullName));
                }
            }
            catch (Exception exception)
            {
                LogError(ECoreLogMessage.ErrorRunningUnpackingTool, exception);
                throw;
            }
        }
    }
}
