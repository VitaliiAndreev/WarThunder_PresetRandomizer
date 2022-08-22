using Core.Enumerations;

namespace Core.UnpackingToolsIntegration.Enumerations
{
    /// <summary> Enumerates names of unpacking tools. </summary>
    public class ETool
    {
        private const string _unpack = "_unpack";
        private const string _unpacker = _unpack + "er";
        private const string _exe = ECharacterString.Period + EFileExtension.Exe;

        public const string BlkUnpacker = "blk" + _unpack + "_ng" + _exe;
        public const string ClogUnpacker = "clog" + _unpack + _exe;
        public const string DdsxUnpacker = "ddsx" + _unpack + _exe;
        public const string DxpUnpacker = "dxp" + _unpack + _exe;
        public const string VromfsBinUnpacker = EFile.Vromfs + _unpacker + _exe;
        public const string WrplUnpacker = "wrpl" + _unpacker + _exe;
    }
}