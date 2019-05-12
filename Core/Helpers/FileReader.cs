using Core.Extensions;
using Core.Helpers.Interfaces;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Enumerations;
using Core.Helpers.Logger.Interfaces;
using System.IO;
using System.Linq;

namespace Core.Helpers
{
    /// <summary> Provides methods to read files. </summary>
    public class FileReader : LoggerFluency, IFileReader
    {
        #region Constructors

        /// <summary> Creates a new file reader. </summary>
        /// <param name="logger"> An instance of a logger. </param>
        public FileReader(IConfiguredLogger logger)
            : base(logger, ECoreLogCategory.Unpacker)
        {
            LogDebug(ECoreLogMessage.Created.FormatFluently(ECoreLogCategory.Unpacker));
        }

        #endregion Constructors
        #region Methods: CreateStreamReader()

        /// <summary> Creates a stream reader from the file under the specified path. </summary>
        /// <param name="path"> The full path to the file. </param>
        /// <returns></returns>
        public StreamReader CreateStreamReader(string path) =>
            CreateStreamReader(new FileInfo(path));

        /// <summary> Creates a stream reader from the specified file. </summary>
        /// <param name="file"> The file to create a stream reader with. </param>
        /// <returns></returns>
        public StreamReader CreateStreamReader(FileInfo file)
        {
            if (!file.Exists)
            {
                LogErrorAndThrow<FileNotFoundException>
                (
                    ECoreLogMessage.NotFound.FormatFluently(file.FullName),
                    ECoreLogMessage.ErrorReadingFile
                );
                return null;
            }

            LogDebug(ECoreLogMessage.CreatingStreamReader.FormatFluently(file.FullName));

            var streamReader = new StreamReader(file.FullName);

            LogDebug(ECoreLogMessage.CreatedStreamReader.FormatFluently(file.FullName));

            return streamReader;
        }

        #endregion Methods: CreateStreamReader()
        #region Methods: Read()

        /// <summary> Reads contents of the file under the specified path. </summary>
        /// <param name="path"> The full path to the file. </param>
        /// <returns></returns>
        public string Read(string path) =>
            Read(new FileInfo(path));

        /// <summary> Reads contents of the specified file. </summary>
        /// <param name="file"> The file to read. </param>
        /// <returns></returns>
        public string Read(FileInfo file)
        {
            var fileContents = default(string);

            LogDebug(ECoreLogMessage.Reading.FormatFluently(file.FullName));

            using (var streamReader = CreateStreamReader(file.FullName))
                fileContents = streamReader.ReadToEnd();

            LogDebug(ECoreLogMessage.ReadCharacters.FormatFluently(fileContents.Count()));

            return fileContents;
        }

        #endregion Methods: Read()
    }
}
