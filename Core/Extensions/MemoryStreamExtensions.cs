using System;
using System.IO;

namespace Core
{
    public static class MemoryStreamExtensions
    {
        /// <summary> Reads all characters from the current position to the end of the <paramref name="memoryStream"/>. </summary>
        /// <param name="memoryStream"> The memory stream to read. </param>
        /// <returns></returns>
        public static string Read(this MemoryStream memoryStream)
        {
            using (var streamReader = new StreamReader(memoryStream))
                return streamReader.ReadToEnd();
        }

        /// <summary> Reads all characters from the current position to the end of the <paramref name="memoryStream"/> and prints them to <see cref="Console"/>. </summary>
        /// <param name="memoryStream"> The memory stream to output. </param>
        public static void OutputToConsole(this MemoryStream memoryStream) =>
            Console.WriteLine(memoryStream.Read());
    }
}