using Client.Shared.Wpf.Extensions;
using Client.Wpf.Controls.Base;
using Core;
using Core.DataBase.WarThunder.Helpers;
using Core.DataBase.WarThunder.Objects.VehicleGameModeParameterSet.String;
using System;
using System.Windows;
using System.Windows.Media;

namespace Client.Wpf.Controls
{
    public partial class BattleRatingUsageColumn : LocalisedUserControl
    {
        #region Constructors

        public BattleRatingUsageColumn()
        {
            InitializeComponent();
        }

        #endregion Constructors
        #region Methods: Initialisation

        public void SetRatio(int economicRank, double ratio)
        {
            var percentage = Math.Round(ratio * Integer.Number.Hundred, Integer.Number.Zero);

            _battleRatingLabel.Text = Calculator.GetBattleRating(economicRank).ToString(BattleRating.Format);
            _percentageLabel.Text = $"{percentage}{Character.Percent}";

            _filledBarDefinition.Height = new GridLength(percentage, GridUnitType.Star);
            _notFilledBarDefinition.Height = new GridLength(Integer.Number.Hundred - percentage, GridUnitType.Star);
        }

        public void SetColor(byte red, byte green, byte blue)
        {
            _filledBarGrid.Background = new SolidColorBrush(new Color().From(red, green, blue));
        }

        public void SetColor(Color color)
        {
            _filledBarGrid.Background = new SolidColorBrush(color);
        }

        #endregion Methods: Initialisation
        #region Methods: Display

        public void Show()
        {
            _chartGrid.Visibility = Visibility.Visible;
            _battleRatingLabel.Visibility = Visibility.Visible;
            _percentageLabel.Visibility = Visibility.Visible;
        }

        public void Hide()
        {
            _chartGrid.Visibility = Visibility.Hidden;
            _battleRatingLabel.Visibility = Visibility.Hidden;
            _percentageLabel.Visibility = Visibility.Hidden;
        }

        #endregion Methods: Display
    }
}