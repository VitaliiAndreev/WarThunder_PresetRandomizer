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

        /// <summary> Reads contents of the specified file. </summary>
        /// <param name="path"> The absolute name of the file. </param>
        /// <returns></returns>
        public string Read(string path)
        {
            var file = new FileInfo(path);
            var fileContents = default(string);

            if (!file.Exists)
            {
                var message = ECoreLogMessage.NotFound.FormatFluently(file.FullName);
                var exception = new FileNotFoundException(message, file.FullName);

                LogError(message, exception);
                throw exception;
            }

            LogDebug(ECoreLogMessage.Reading.FormatFluently(file.FullName));

            using (var streamReader = new StreamReader(file.FullName))
                fileContents = streamReader.ReadToEnd();

            LogDebug(ECoreLogMessage.ReadCharacters.FormatFluently(fileContents.Count()));

            return fileContents;
        }
    }
}
