namespace Client.Wpf.Enumerations
{
    /// <summary> String keys of styles defined in <see cref="WpfClient"/>. </summary>
    public class EStyleKey
    {
        /// <summary> String keys of styles for <see cref="System.Windows.Controls.Border"/>. </summary>
        public class Border
        {
            public const string ResearchTreeCell = "ResearchTreeCell";
            public const string ResearchTreeCellHighlighted = "ResearchTreeCellHighlighted";

            public const string SquadronResearchTreeCell = "SquadronResearchTreeCell";
            public const string SquadronResearchTreeCellHighlighted = "SquadronResearchTreeCellHighlighted";

            public const string PremiumResearchTreeCell = "PremiumResearchTreeCell";
            public const string PremiumResearchTreeCellHighlighted = "PremiumResearchTreeCellHighlighted";

            public const string RankDivider = "RankDivider";
        }
        /// <summary> String keys of styles for <see cref="System.Windows.Controls.Image"/>. </summary>
        public class Image
        {
            public const string FlagIcon = "FlagIcon";
            public const string FlagIcon16px = "FlagIcon16px";
            public const string LocalizationIcon = "LocalizationIcon";
        }
        /// <summary> String keys of styles for <see cref="Controls.ResearchTreeCellControl"/>. </summary>
        public class ResearchTreeCellControl
        {
            public const string Rank1 = "Rank1";
            public const string Rank2 = "Rank2";
            public const string Rank3 = "Rank3";
            public const string Rank4 = "Rank4";
            public const string Rank5 = "Rank5";
            public const string Rank6 = "Rank6";
            public const string Rank7 = "Rank7";
        }

        /// <summary> String keys of styles for <see cref="System.Windows.Controls.TextBox"/>. </summary>
        public class TextBox
        {
            public const string ValidText = "ValidText";
            public const string InvalidText = "InvalidText";
        }

        /// <summary> String keys of styles for <see cref="System.Windows.Controls.Primitives.ToggleButton"/>. </summary>
        public class ToggleButton
        {
            public const string BranchToggle = "BranchToggle";
            public const string CountryToggle = "CountryToggle";
            public const string NationToggle = "NationToggle";
            public const string VehicleClassToggle = "VehicleClassToggle";
        }
    }
}