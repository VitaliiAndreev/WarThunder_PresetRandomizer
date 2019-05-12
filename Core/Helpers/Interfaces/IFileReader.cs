using System.IO;

namespace Core.Helpers.Interfaces
{
    /// <summary> Provides methods to read files. </summary>
    public interface IFileReader
    {
        #region Methods: CreateStreamReader()

        /// <summary> Creates a stream reader from the file under the specified path. </summary>
        /// <param name="path"> The full path to the file. </param>
        /// <returns></returns>
        StreamReader CreateStreamReader(string path);

        /// <summary> Creates a stream reader from the specified file. </summary>
        /// <param name="file"> The file to create a stream reader with. </param>
        /// <returns></returns>
        StreamReader CreateStreamReader(FileInfo file);

        #endregion Methods: CreateStreamReader()
        #region Methods: Read()

        /// <summary> Reads contents of the file under the specified path. </summary>
        /// <param name="path"> The full path to the file. </param>
        /// <returns></returns>
        string Read(string path);

        /// <summary> Reads contents of the specified file. </summary>
        /// <param name="file"> The file to read. </param>
        /// <returns></returns>
        string Read(FileInfo file);

        #endregion Methods: Read()
    }
}
