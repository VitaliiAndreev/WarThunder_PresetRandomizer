﻿namespace Client.Wpf.Enumerations
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
        public class DataGridRow
        {
            public const string DataGridRowPremium = "DataGridRowPremium";
            public const string DataGridRowRegular = "DataGridRowRegular";
            public const string DataGridRowSquadron = "DataGridRowSquadron";
        }
        /// <summary> String keys of styles for <see cref="System.Windows.Controls.Grid"/>. </summary>
        public class Grid
        {
            public const string SkyQuakeGrid = "SkyQuakeGrid";
        }
        /// <summary> String keys of styles for <see cref="System.Windows.Controls.Image"/>. </summary>
        public class Image
        {
            public const string FlagIcon = "FlagIcon";
            public const string FlagIcon16px = "FlagIcon16px";
            public const string Icon32px = "Icon32px";
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
        public class StackPanel
        {
            public const string StackPanelPremium = "StackPanelPremium";
            public const string StackPanelSquadron = "StackPanelSquadron";
        }
        /// <summary> String keys of styles for <see cref="System.Windows.Controls.TextBlock"/>. </summary>
        public class TextBlock
        {
            public const string TextBlock12px = "TextBlock12px";
            public const string TextBlockFontSize16 = "TextBlockFontSize16";
            public const string TextBlockWithSkyQuake = "TextBlockWithSkyQuake";
            public const string TextBlockWithSkyQuake10px = "TextBlockWithSkyQuake10px";
            public const string TextBlockWithSkyQuake12pxRightCenter = "TextBlockWithSkyQuake12pxRightCenter";
            public const string TextBlockWithSkyQuake12pxCenterDefault = "TextBlockWithSkyQuakeHorizontallyCentered12px";
            public const string TextBlockWithSkyQuake16px = "TextBlockWithSkyQuake16px";
            public const string TextBlockWithSkyQuakeHorizontallyCentered = "TextBlockWithSkyQuakeHorizontallyCentered";
            public const string TextBlockWithSkyQuakeUncondensed = "TextBlockWithSkyQuakeUncondensed";
            public const string TextBlockWithSkyQuakeVerticallyCentered = "TextBlockWithSkyQuakeVerticallyCentered";
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
            public const string CountryToggleAll = "CountryToggleAll";
            public const string NationToggle = "NationToggle";
            public const string BaseToggleButton = "ToggleButton";
            public const string VehicleClassToggle = "VehicleClassToggle";
            public const string RankToggle = "RankToggle";
        }
    }
}