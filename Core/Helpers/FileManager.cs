﻿using Core.Helpers.Interfaces;
using Core.Helpers.Logger;
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
        /// <param name="loggers"> Instances of loggers. </param>
        public FileManager(params IConfiguredLogger[] loggers)
            : base(nameof(FileManager), loggers)
        {
            LogDebug($"{nameof(FileManager)} created.");
        }

        #endregion Constructors
        #region Methods: Copying

        /// <summary> Copies a file into a specified directory. </summary>
        /// <param name="filePath"> The absolute name of the file. </param>
        /// <param name="destination"> The destination path. </param>
        /// <param name="overwrite"> Whether to overwrite colliding files at the destination. </param>
        /// <param name="createDirectories"> Whether to create the destination directory. </param>
        public void CopyFile(string filePath, string destination, bool overwrite = false, bool createDirectories = false) =>
            CopyFile(new FileInfo(filePath), destination, overwrite, createDirectories);

        /// <summary> Copies a file into a specified directory. </summary>
        /// <param name="file"> The file to copy. </param>
        /// <param name="destination"> The destination path. </param>
        /// <param name="overwrite"> Whether to overwrite colliding files at the destination. </param>
        /// <param name="createDirectories"> Whether to create the destination directory. </param>
        public void CopyFile(FileInfo file, string destination, bool overwrite = false, bool createDirectories = false)
        {
            var destinationDirectory = new DirectoryInfo(destination);

            // Pre-copying checks and directory creation.

            if (!file.Exists)
            {
                LogDebug($"\"{file.FullName}\" doesnt exist. Copying safely aborted.");
                return;
            }
            else if (!destinationDirectory.Exists)
            {
                if (createDirectories)
                {
                    LogDebug($"Creating \"{destinationDirectory.FullName}\".");

                    destinationDirectory.Create();

                    LogDebug($"\"{destinationDirectory.FullName}\" created.");
                }
                else
                {
                    LogDebug($"\"{destinationDirectory.FullName}\" doesnt exist. Copying \"{file.FullName}\" safely aborted.");
                    return;
                }
            }

            // Copying proper.

            LogDebug($"Copying \"{file.FullName}\" into \"{destinationDirectory.FullName}\".");

            var filePath = Path.Combine(destinationDirectory.FullName, file.Name);

            if (File.Exists(filePath))
            {
                if (!overwrite)
                {
                    LogDebug($"\"{file.Name}\" already exists. Copying skipped.");
                    return;
                }
                LogDebug("Overwriting.");
            }
            file.CopyTo(filePath, overwrite);

            LogDebug($"\"{file.FullName}\" copied.");
        }

        /// <summary> Creates a backup copy of the given file, with its name appended with ".bak". </summary>
        /// <param name="file"> The file to back up. </param>
        public void BackUpFile(FileInfo file) =>
            file.CopyTo(Path.Combine(file.DirectoryName, $"{file.Name}.{FileExtension.Bak}"), true);

        #endregion Methods: Copying
        #region Methods: Deletion

        /// <summary> Deletes the file with the specified name if it exists. </summary>
        /// <param name="fileName"> The name of the file to delete. </param>
        public void DeleteFileSafely(string fileName) =>
            DeleteFileSafely(new FileInfo(fileName));

        /// <summary> Deletes the specified file if it exists. </summary>
        /// <param name="file"> The file to delete. </param>
        public void DeleteFileSafely(FileInfo file)
        {
            LogTrace($"Deleting \"{file.Name}\".");

            try
            {
                file.Refresh();

                if (file.Exists)
                    file.Delete();
                else
                    LogTrace($"\"{file.FullName}\" doesnt exist. No need to delete it.");
            }
            catch (Exception exception)
            {
                LogError("Error deleting file.", exception);
                throw;
            }

            LogTrace("File deleted.");
        }

        #region DeleteFiles()

        /// <summary> Deletes all files under names/paths listed in the specified collection. </summary>
        /// <param name="filePaths"> A collection of file names/paths. </param>
        public void DeleteFiles(IEnumerable<string> filePaths) =>
            DeleteFiles(filePaths.Select(filePath => Path.IsPathRooted(filePath) ? filePath : Path.Combine(Directory.GetCurrentDirectory(), filePath)));

        /// <summary> Deletes all files listed in the specified collection. </summary>
        /// <param name="files"> A collection of file information. </param>
        private void DeleteFiles(IEnumerable<FileInfo> files)
        {
            LogDebug($"Deleting {files.Count()} file(s).");

            foreach (var file in files)
                DeleteFileSafely(file);

            LogDebug("All files deleted.");
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
                LogDebug($"The directory \"{rootDirectory.FullName}\" doesnt exist. Deleting files safely aborted.");
                return;
            }

            // Reading all file information.

            LogDebug($"Selecting all files from \"{rootDirectory.FullName}\".");

            var files = rootDirectory.GetFiles();
            if (files.IsEmpty())
            {
                LogDebug($"The directory \"{rootDirectory.FullName}\" is empty.");

                if (includeNested)
                    DeleteFilesInSubdirectories(rootDirectory, fileExtensions, deleteEmptyDirectories);

                return;
            }

            // Filtering files by specified extensions.

            if (fileExtensions?.Any() ?? false)
            {
                LogDebug($"Filtering \"{fileExtensions.StringJoin(", ")}\" files from selection.");
                files = files
                    .Where
                    (
                        file => file.GetExtensionWithoutPeriod().ToLower().IsIn(fileExtensions.Select(extension => extension.ToLower()))
                            && !file.Attributes.HasFlag(FileAttributes.Directory | FileAttributes.Hidden | FileAttributes.ReadOnly | FileAttributes.System)
                            && !file.IsReadOnly
                    )
                    .ToArray()
                ;

                if (files.IsEmpty())
                {
                    LogDebug("No files of specified format to delete.");
                    return;
                }
            }

            // Deleting files.

            LogDebug($"Selected {files.Count()} file(s).");
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
            LogDebug("Checking for subdirectories.");

            var subdirectories = directory.GetDirectories();
            if (subdirectories.IsEmpty())
            {
                LogDebug("No subdirectories.");
                return;
            }

            LogDebug($"{subdirectories.Count()} subdirectories found.");

            for (var i = 0; i < subdirectories.Count(); i++)
            {
                var subdirectory = subdirectories[i];
                DeleteFiles(subdirectory.FullName, fileExtensions, true, deleteEmptyDirectories);

                if (deleteEmptyDirectories)
                    Directory.Delete(subdirectory.FullName, recursive: true);
            }
        }

        #endregion DeleteFilesInSubdirectories()

        /// <summary> Empties the specified directory. </summary>
        /// <param name="path"> The path to a directory. </param>
        public void EmptyDirectory(string path)
        {
            LogDebug($"Emptying \"{path}\".");

            DeleteFiles(path, true, true);

            LogDebug($"{path}\" emptied.");
        }

        /// <summary> Deletes the specified directory. </summary>
        /// <param name="path"> The path to a directory. </param>
        public void DeleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                LogDebug($"Deleting \"{path}\".");

                Directory.Delete(path, recursive: true);

                LogDebug($"\"{path}\" deleted.");
            }
            else
            {
                LogDebug($"\"{path}\" doesnt exist. No need to delete it.");
            }
        }

        /// <summary> Deletes all files (non-recursively) older than the given timestamp. </summary>
        /// <param name="path"> The path to files. </param>
        /// <param name="deletionDeadline"> The timestamp before which all files should be deleted. </param>
        public void DeleteOldFiles(string path, DateTime deletionDeadline)
        {
            var files = Directory
                .GetFiles(path)
                .Select(filePath => new FileInfo(filePath))
            ;

            foreach (var file in files)
            {
                if (file.LastWriteTimeUtc < deletionDeadline)
                    DeleteFileSafely(file.FullName);
            }
        }

        #endregion Methods: Deletion
        #region Methods: Fluency

        /// <summary> Creates a new instance of <see cref="FileInfo"/> with a directory path and a file name. </summary>
        /// <param name="directoryPath"> The directory path. </param>
        /// <param name="fileName"> The file name. </param>
        /// <returns></returns>
        public FileInfo GetFileInfo(string directoryPath, string fileName) =>
            new FileInfo(Path.Combine(directoryPath, fileName));

        #endregion Methods: Fluency
    }
}