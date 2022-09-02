using Core.Enumerations;

namespace Core.UnpackingToolsIntegration.Enumerations
{
    public class EDirectory : Vocabulary
    {
        public class WarThunder
        {
            public class Archive
            {
                public class AtlasesWromfsBin
                {
                    public const string UnitIcons = _units;
                }
                public class TexWromfsBin
                {
                    public const string Aircraft = _aircraft + _s;
                    public const string Ships = _ships;
                    public const string Tanks = _tanks;
                }
            }
            public class Subdirectory
            {
                public static readonly string Ui = _UI.ToLower();
            }
        }
    }
}