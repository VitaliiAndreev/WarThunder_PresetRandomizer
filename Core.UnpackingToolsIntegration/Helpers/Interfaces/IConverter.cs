using System.IO;

namespace Core.UnpackingToolsIntegration.Helpers.Interfaces
{
    public interface IConverter
    {
        void ConvertDdsToPng(DirectoryInfo directory, SearchOption searchOption = SearchOption.TopDirectoryOnly);
    }
}