using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers.Interfaces;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Core.Helpers
{
    /// <summary> Provides methods to read files. </summary>
    public class FileReader : LoggerFluency, IFileReader
    {
        #region Constructors

        /// <summary> Creates a new file reader. </summary>
        /// <param name="loggers"> Instances of loggers. </param>
        public FileReader(params IConfiguredLogger[] loggers)
            : base(ECoreLogCategory.FileReader, loggers)
        {
            LogDebug(ECoreLogMessage.Created.Format(ECoreLogCategory.FileReader));
        }

        #endregion Constructors
        #region Methods: CreateTextReader()

        /// <summary> Creates a text reader from the file under the specified path. </summary>
        /// <param name="path"> The full path to the file. </param>
        /// <returns></returns>
        public TextReader CreateTextReader(string path) =>
            CreateTextReader(new FileInfo(path));

        /// <summary> Creates a text reader from the specified file. </summary>
        /// <param name="file"> The file to create a text reader with. </param>
        /// <returns></returns>
        public TextReader CreateTextReader(FileInfo file)
        {
            if (!file.Exists)
            {
                LogErrorAndThrow<FileNotFoundException>
                (
                    ECoreLogMessage.NotFound.Format(file.FullName),
                    ECoreLogMessage.ErrorReadingFile
                );
                return null;
            }

            LogDebug(ECoreLogMessage.CreatingStreamReader.Format(file.FullName));

            var streamReader = new StreamReader(file.FullName);

            LogDebug(ECoreLogMessage.StreamReaderCreated);

            return streamReader;
        }

        #endregion Methods: CreateTextReader()
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

            LogDebug(ECoreLogMessage.Reading.Format(file.FullName));

            using (var textReader = CreateTextReader(file.FullName))
                fileContents = textReader.ReadToEnd();

            LogDebug(ECoreLogMessage.ReadCharacters.Format(fileContents.Count()));

            return fileContents;
        }

        /// <summary> Reads contents of the specified CSV file into a collection of records. </summary>
        /// <param name="file"> The CSV file to read. </param>
        /// <param name="delimiter"> The field delimeter. </param>
        /// <returns></returns>
        public IList<IList<string>> ReadCsv(FileInfo file, char delimiter)
        {
            var lines = new List<IList<string>>();

            using (var parser = new TextFieldParser(file.FullName) { TextFieldType = FieldType.Delimited, Delimiters = new string[] { delimiter.ToString() }, })
            {
                while (!parser.EndOfData)
                {
                    var record = new List<string>();
                    var readValues = parser.ReadFields();

                    foreach (var fieldValue in readValues)
                        record.Add(fieldValue);

                    lines.Add(record);
                }
            }
            return lines;
        }

        public Bitmap ReadImage(FileInfo file)
        {
            return new Bitmap(file.FullName);
        }

        public byte[] ReadBytes(FileInfo file)
        {
            LogTrace(ECoreLogMessage.ReadingBytesFromFile.Format(file.FullName));

            var bytes = File.ReadAllBytes(file.FullName);

            if (bytes is null)
                LogWarn(ECoreLogMessage.ErrorReadingBytesFromFile.Format(file.FullName));
            else
                LogTrace(ECoreLogMessage.ReadBytesFromFile.Format(bytes.Count(), file.FullName));

            return bytes;
        }

        #endregion Methods: Read()
    }
}