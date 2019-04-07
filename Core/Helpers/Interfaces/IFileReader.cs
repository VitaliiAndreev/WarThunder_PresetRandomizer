namespace Core.Helpers.Interfaces
{
    public interface IFileReader
    {
        /// <summary> Reads contents of the specified file. </summary>
        /// <param name="path"> The absolute name of the file. </param>
        /// <returns></returns>
        string Read(string path);
    }
}
