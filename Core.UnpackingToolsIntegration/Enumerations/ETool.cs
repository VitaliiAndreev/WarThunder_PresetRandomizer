namespace Core.UnpackingToolsIntegration.Enumerations
{
    public static class ETool
    {
        private const string unpack = "_unpack";
        private const string unpacker = "_unpacker";

        private static readonly string exe = $".{FileExtension.Exe}";

        public static readonly string BlkUnpacker = $"blk{unpack}_ng{exe}";
        public static readonly string ClogUnpacker = $"clog{unpack}{exe}";
        public static readonly string DdsxUnpacker = $"ddsx{unpack}{exe}";
        public static readonly string DxpUnpacker = $"dxp{unpack}{exe}";
        public static readonly string VromfsBinUnpacker = $"{EFile.Vromfs}{unpacker}{exe}";
        public static readonly string WrplUnpacker = $"wrpl{unpacker}{exe}";
    }
}