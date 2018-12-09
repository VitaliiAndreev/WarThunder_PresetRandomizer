using Core.Extensions;
using Core.Helpers.Interfaces;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Enumerations;
using Core.Helpers.Logger.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core.Helpers
{
    /// <summary> Provides methods to manage files. </summary>
    public class FileManager : LoggerFluency, IFileManager
    {
        #region Constructors

        /// <summary> Creates a new file manager. </summary>
        /// <param name="logger"> An instance of a logger. </param>
        public FileManager(IConfiguredLogger logger)
            : base(logger, ECoreLogCategory.FileManager)
        {
            LogDebug(ECoreLogMessage.Created.FormatFluently("File Manager"));
        }

        #endregion Constructors
        #region Methods: Deletion

        /// <summary> Deletes the specified file. </summary>
        /// <param name="file"> A file to delete. </param>
        private void DeleteFile(FileInfo file)
        {
            LogTrace(ECoreLogMessage.DeletingFile.FormatFluently(file.Name));

            try
            {
                file.Delete();
            }
            catch (Exception exception)
            {
                LogError(ECoreLogMessage.ErrorDeletingFile, exception);
                throw;
            }

            LogTrace(ECoreLogMessage.FileDeleted);
        }

        /// <summary> Deletes all files listed in the specified collection. </summary>
        /// <param name="files"> A collection of file information. </param>
        private void DeleteFiles(IEnumerable<FileInfo> files)
        {
            var fileCount = files.Count();
            LogDebug(ECoreLogMessage.DeletingFiles.FormatFluently(fileCount));

            try
            {
                foreach (var file in files)
                   DeleteFile(file);
            }
            catch (Exception exception)
            {
                LogError(ECoreLogMessage.ErrorDeletingFiles, exception);
                throw;
            }

            LogDebug(ECoreLogMessage.FilesDeleted);
        }

        /// <summary> Deletes all files with the specified extension in a folder. </summary>
        /// <param name="path"> The path to a folder. </param>
        /// <param name="fileExtension"> A collection of file extensions to delete from the folder. </param>
        public void DeleteFiles(string path, params string[] fileExtensions) =>
            DeleteFiles(path, fileExtensions.ToList());

        /// <summary> Deletes all files with specified extensions in a folder. </summary>
        /// <param name="path"> The path to a folder. </param>
        /// <param name="fileExtension"> A collection of file extensions to delete from the folder. </param>
        public void DeleteFiles(string path, IEnumerable<string> fileExtensions)
        {
            var directory = new DirectoryInfo(path);

            if (!directory.Exists)
            {
                LogWarn(ECoreLogMessage.WarnDirectoryDoestExist_DeletingAborted.FormatFluently(directory.FullName));
                return;
            }

            LogDebug(ECoreLogMessage.SelectingFiles.ResetFormattingPlaceholders().FormatFluently(fileExtensions.StringJoin(", "), directory.FullName));
            var files = directory
                .GetFiles()
                .Where
                (
                    file => file.Extension.Substring(1).ToLower().IsIn(fileExtensions.Select(extension => extension.ToLower()))
                        && !file.Attributes.HasFlag(FileAttributes.Directory | FileAttributes.Hidden | FileAttributes.ReadOnly | FileAttributes.System)
                        && !file.IsReadOnly
                )
            ;

            if (files.IsEmpty())
            {
                LogDebug(ECoreLogMessage.WarnEmptyDirectory.FormatFluently(directory.FullName));
                return;
            }

            LogDebug(ECoreLogMessage.SelectedFiles.FormatFluently(files.Count()));
            DeleteFiles(files);
        }

        #endregion Methods: Deletion
    }
}
