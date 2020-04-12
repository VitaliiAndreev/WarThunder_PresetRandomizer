﻿using Core.Enumerations;

namespace Core.UnpackingToolsIntegration.Enumerations
{
    public class EDirectory : EVocabulary
    {
        public class WarThunder
        {
            public class Archive
            {
                public class AtlasesWromfsBin
                {
                    public const string UnitIcons = _units;
                }
            }
            public class Subdirectory
            {
                public static readonly string Ui = _UI.ToLower();
            }
        }
    }
}