using Core.Enumerations;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Enumerations;
using Core.Helpers.Logger.Interfaces;
using Core.UnpackingToolsIntegration.Enumerations;
using Core.UnpackingToolsIntegration.Exceptions;
using Core.UnpackingToolsIntegration.Helpers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Core.UnpackingToolsIntegration.Helpers
{
    /// <summary> Provides methods to unpack War Thunder files. </summary>
    public class Unpacker : LoggerFluency, IUnpacker
    {
        private readonly Dictionary<string, string> _toolFileNames;

        #region Constructors

        /// <summary> Creates a new unpacker. </summary>
        /// <param name="logger"> An instance of a logger. </param>
        public Unpacker(IConfiguredLogger logger)
            : base(logger, ECoreLogCategory.Unpacker)
        {
            LogDebug(ECoreLogMessage.Created.FormatFluently(ECoreLogCategory.Unpacker));

            _toolFileNames = new Dictionary<string, string>
            {
                { EFileExtension.Blk, ETool.BlkUnpacker },
                { EFileExtension.Clog, ETool.ClogUnpacker },
                { EFileExtension.Ddsx, ETool.DdsxUnpacker },
                { EFileExtension.Dxp, ETool.DxpUnpacker },
                { EFileExtension.VromfsBin, ETool.VromfsUnpacker },
                { EFileExtension.Wrpl, ETool.WrplUnpacker },
            };
        }

        #endregion Constructors

        /// <summary> Selects an appropriate unpacking tool file name (see <see cref="ETool"/>) for a given file extension. </summary>
        /// <param name="fileExtension"> The file extension to look for a match for. The register and period characters are ignored. </param>
        /// <returns></returns>
        private string GetToolFileNameByFileExtension(string fileExtension)
        {
            if (_toolFileNames.TryGetValue(fileExtension.ToLower().Except(new char[] { ECharacter.Period }).StringJoin(), out var toolFileName))
            {
                LogDebug(ECoreLogMessage.UnpackingToolSelected);
                return toolFileName;
            }
            else
            {
                var message = ECoreLogMessage.FileExtensionNotSupportedByUnpackingTools.FormatFluently(fileExtension);
                var exception = new FileExtensionNotSupportedException(message);

                LogError(message, exception);
                throw exception;
            }
        }
    }
}
