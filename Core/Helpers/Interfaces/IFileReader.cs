using System.IO;

namespace Core.Helpers.Interfaces
{
    /// <summary> Provides methods to read files. </summary>
    public interface IFileReader
    {
        /// <summary> Reads contents of the file under specified path. </summary>
        /// <param name="path"> The absolute name of the file. </param>
        /// <returns></returns>
        string Read(string path);

        /// <summary> Reads contents of the specified file. </summary>
        /// <param name="file"> The file to read. </param>
        /// <returns></returns>
        string Read(FileInfo file);
    }
}
