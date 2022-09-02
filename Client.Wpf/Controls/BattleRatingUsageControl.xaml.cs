using Client.Shared.Wpf.Extensions;
using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.Enumerations;
using Core.Extensions;
using Core.Organization.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Client.Wpf.Controls
{
    public partial class BattleRatingUsageControl : LocalisedUserControl
    {
        #region Constants

        private const int _economicRankForkSize = Integer.Number.Three;

        private static readonly Color _downtierColor = new Color().From(0, 255, 0);
        private static readonly Color _sameTierColor = new Color().From(255, 255, 0);
        private static readonly Color _uptierColor = new Color().From(255, 0, 0);

        #endregion Constants
        #region Fields

        private static readonly IDictionary<int, Color> _colors;

        #endregion Fields
        #region Constructors

        static BattleRatingUsageControl()
        {
            _colors = new Dictionary<int, Color>
            {
                { Integer.Number.Zero, _sameTierColor }
            };

            var downtierColors = _sameTierColor.InterpolateTo(_downtierColor, _economicRankForkSize);
            var uptierColors = _sameTierColor.InterpolateTo(_uptierColor, _economicRankForkSize);

            static Color getColor(IEnumerable<Color> colors, int index) => colors.At(index - Integer.Number.One);

            for (var downtierEconomicRank = -Integer.Number.One; downtierEconomicRank >= -_economicRankForkSize; downtierEconomicRank--)
                _colors.Add(downtierEconomicRank, getColor(downtierColors, -downtierEconomicRank));

            for (var uptierEconomicRank = Integer.Number.One; uptierEconomicRank <= _economicRankForkSize; uptierEconomicRank++)
                _colors.Add(uptierEconomicRank, getColor(uptierColors, uptierEconomicRank));
        }

        public BattleRatingUsageControl()
        {
            InitializeComponent();
        }

        #endregion Constructors
        #region Methods: Initialisation

        public BattleRatingUsageControl Populate(Preset preset)
        {
            if (!ApplicationHelpers.Manager.ShowThunderSkillData)
            {
                var textBlock = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    TextAlignment = TextAlignment.Center,
                    FontSize = Integer.Number.Sixteen,
                    Text = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.NoData),
                };

                _grid.Add(textBlock);

                return this;
            }

            var usageCounts = new Dictionary<int, int>();

            for (var economicRank = preset.EconomicRank - _economicRankForkSize; economicRank <= preset.EconomicRank + _economicRankForkSize; economicRank++)
            {
                usageCounts.Add(economicRank, Integer.Number.Zero);

                foreach (var branch in preset.Branches)
                {
                    if (usageCounts[economicRank].IsNegative())
                        break;

                    if (ApplicationHelpers.Manager.EconomicRankUsage[preset.GameMode][branch].TryGetValue(economicRank, out var usageCount))
                    {
                        usageCounts.Increment(economicRank, usageCount);
                    }
                    else
                    {
                        usageCounts[economicRank] = -Integer.Number.One;
                    }
                }
            }

            var totalUsageCount = usageCounts.Values.Sum();
            var ratios = usageCounts
                .Where(usageCount => !usageCount.Value.IsNegative())
                .ToDictionary(economicRank => economicRank.Key, economicRank => totalUsageCount.IsPositive() ? Convert.ToDouble(economicRank.Value) / totalUsageCount : Integer.Number.Zero)
            ;
            var columnIndex = Integer.Number.Zero;
            var colorIndex = _colors.Keys.Min();

            foreach (var usageCountRecord in usageCounts)
            {
                var economicRank = usageCountRecord.Key;
                var column = new BattleRatingUsageColumn();

                if (economicRank.IsNegative() || economicRank > EReference.MaximumEconomicRank)
                {
                    column.Hide();
                    colorIndex++;
                }
                else
                {
                    var usageCount = usageCountRecord.Value;
                    var color = _colors.TryGetValue(colorIndex++, out var colorFromGradient) ? colorFromGradient : Colors.Black;

                    column.SetRatio(economicRank, ratios[economicRank]);
                    column.SetColor(color);
                }
                _grid.Add(column, columnIndex++);
            }
            return this;
        }

        new public BattleRatingUsageControl Localise()
        {
            base.Localise();

            static string getLocalisedString(string localisationKey) => ApplicationHelpers.LocalisationManager.GetLocalisedString(localisationKey);

            _header.Text = getLocalisedString(ELocalisationKey.BattleRatingSaturation);
            _control.ToolTip = getLocalisedString(ELocalisationKey.BattleRatingSaturationTooltip);

            return this;
        }

        #endregion Methods: Initialisation

        public BattleRatingUsageControl Reset()
        {
            _grid.Children.Clear();

            return this;
        }
    }
}