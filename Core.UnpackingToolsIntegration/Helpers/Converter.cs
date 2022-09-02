using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using Core.UnpackingToolsIntegration.Enumerations.Logger;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using Core.UnpackingToolsIntegration.Objects;
using System;
using System.Collections.Generic;
using System.IO;

namespace Core.UnpackingToolsIntegration.Helpers
{
    public class Converter : LoggerFluency, IConverter
    {
        #region Constructors

        /// <summary> Creates a new converter. </summary>
        /// <param name="loggers"> Instances of loggers. </param>
        public Converter(params IConfiguredLogger[] loggers)
            : base(EUnpackingToolsIntegrationLogCategory.Converter, loggers)
        {
            LogDebug(CoreLogMessage.Created.Format(EUnpackingToolsIntegrationLogCategory.Converter));
        }

        #endregion Constructors

        public void ConvertDdsToPng(DirectoryInfo directory, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            foreach (var file in directory.GetFiles($"{Character.Asterisk}{Character.Period}{FileExtension.Dds}", searchOption))
            {
                Func<DdsImage> readFile = () => new DdsImage(file.FullName);

                var ignoredReadErrorParts = new List<string>
                {
                    "invalid targa",
                    "21 not supported",
                };

                if (readFile.TryExecuting(out var ddsImage, out var exception))
                {
                    ddsImage.Save(Path.Combine(file.DirectoryName, $"{file.GetNameWithoutExtension()}.{FileExtension.Png}"));
                }
                else if (!(exception is ArgumentException argumentException) || !exception.Message.ContainsAny(ignoredReadErrorParts))
                {
                    throw exception;
                }
            }
        }
    }
}