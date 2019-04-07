using Core.WarThunderUnpackingToolsIntegration.Enumerations;
using System;

namespace Core.WarThunderUnpackingToolsIntegration.Helpers.Interfaces
{
    /// <summary> Provides methods to parse file contents. </summary>
    public interface IParser
    {
        /// <summary> Reads the client version from .yup file contents. </summary>
        /// <param name="rawFileContents"> A string to read from. </param>
        /// <returns></returns>
        Version GetClientVersion(string rawFileContents);
    }
}
