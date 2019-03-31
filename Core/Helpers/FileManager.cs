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

        #region DeleteFiles()

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

        /// <summary> Deletes all files in a directory. </summary>
        /// <param name="path"> The path to a directory. </param>
        /// <param name="includeNested"> Whether to delete files in all nested directories. </param>
        /// <param name="deleteEmptyDirectories"> Whether to delete directories if they are empty after file deletion regardless of whether they had files prior. </param>
        public void DeleteFiles(string path, bool includeNested = false, bool deleteEmptyDirectories = false) =>
            DeleteFiles(path, new List<string>(), includeNested, deleteEmptyDirectories);

        /// <summary> Deletes all files with the specified extension in a directory. </summary>
        /// <param name="path"> The path to a directory. </param>
        /// <param name="fileExtension"> A collection of file extensions to delete from the folder. </param>
        public void DeleteFiles(string path, params string[] fileExtensions) =>
            DeleteFiles(path, fileExtensions.ToList());

        /// <summary> Deletes all files with specified extensions in a directory. </summary>
        /// <param name="path"> The path to a directory. </param>
        /// <param name="fileExtension"> A collection of file extensions to delete from the folder. </param>
        /// <param name="includeNested"> Whether to delete files in all nested directories. </param>
        /// <param name="deleteEmptyDirectories"> Whether to delete directories if they are empty after file deletion regardless of whether they had files prior. </param>
        public void DeleteFiles(string path, IEnumerable<string> fileExtensions, bool includeNested = false, bool deleteEmptyDirectories = false)
        {
            var rootDirectory = new DirectoryInfo(path);

            if (!rootDirectory.Exists)
            {
                LogWarn(ECoreLogMessage.WarnDirectoryDoestExist_DeletingAborted.FormatFluently(rootDirectory.FullName));
                return;
            }

            LogDebug(ECoreLogMessage.SelectingAllFilesFromDirectory.FormatFluently(rootDirectory.FullName));

            var files = rootDirectory.GetFiles();
            if (files.IsEmpty())
            {
                LogDebug(ECoreLogMessage.WarnEmptyDirectory.FormatFluently(rootDirectory.FullName));

                if (includeNested)
                    DeleteFilesInSubdirectories(rootDirectory, fileExtensions, deleteEmptyDirectories);

                return;
            }

            if (fileExtensions?.Any() ?? false)
            {
                LogDebug(ECoreLogMessage.FilteringFilesFromSelection.FormatFluently(fileExtensions.StringJoin(", ")));
                files = files
                    .Where
                    (
                        file => file.Extension.Substring(1).ToLower().IsIn(fileExtensions.Select(extension => extension.ToLower()))
                            && !file.Attributes.HasFlag(FileAttributes.Directory | FileAttributes.Hidden | FileAttributes.ReadOnly | FileAttributes.System)
                            && !file.IsReadOnly
                    )
                    .ToArray()
                ;

                if (files.IsEmpty())
                {
                    LogDebug(ECoreLogMessage.WarnNoFilesOfSpecifiedFormatToDelete);
                    return;
                }
            }

            LogDebug(ECoreLogMessage.SelectedFileCount.FormatFluently(files.Count()));
            DeleteFiles(files);

            if (includeNested)
                DeleteFilesInSubdirectories(rootDirectory, fileExtensions, deleteEmptyDirectories);
        }

        #endregion DeleteFiles()
        #region DeleteFilesInSubdirectories()

        /// <summary> Deletes all files with specified extensions in all subfolders. </summary>
        /// <param name="directory"> A directory from whose subfolders to delete files. </param>
        /// <param name="fileExtension"> A collection of file extensions to delete from the folder. </param>
        /// <param name="deleteEmptyDirectories"> Whether to delete directories if they are empty after file deletion regardless of whether they had files prior. </param>
        private void DeleteFilesInSubdirectories(DirectoryInfo directory, IEnumerable<string> fileExtensions, bool deleteEmptyDirectories = false)
        {
            LogDebug(ECoreLogMessage.CheckingSubfolders);

            var subdirectories = directory.GetDirectories();
            if (subdirectories.IsEmpty())
            {
                LogDebug(ECoreLogMessage.WarnNoSubfolders);
                return;
            }

            LogDebug(ECoreLogMessage.SubfoldersFound.FormatFluently(subdirectories.Count()));

            for (var i = 0; i < subdirectories.Count(); i++)
            {
                var subdirectory = subdirectories[i];
                DeleteFiles(subdirectory.FullName, fileExtensions, true, deleteEmptyDirectories);

                if (deleteEmptyDirectories)
                    Directory.Delete(subdirectory.FullName);
            }
        }

        #endregion DeleteFilesInSubdirectories()

        /// <summary> Empties the specified directory. </summary>
        /// <param name="path"> The path to a directory. </param>
        public void EmptyDirectory(string path) =>
            DeleteFiles(path, true, true);

        /// <summary> Deletes the specified directory. </summary>
        /// <param name="path"> The path to a directory. </param>
        public void DeleteDirectory(string path)
        {
            EmptyDirectory(path);

            if (Directory.Exists(path))
                Directory.Delete(path);
        }

        #endregion Methods: Deletion
    }
}