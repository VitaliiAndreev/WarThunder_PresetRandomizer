using Client.Wpf.Controls.Base;
using Core.DataBase.WarThunder.Enumerations;
using Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for GameModeSelectionControl.xaml. </summary>
    public partial class GameModeSelectionControl : ExclusiveToggleButtonGroupControlWithToolTip<EGameMode>
    {
        #region Constuctors

        /// <summary> Creates a new control. </summary>
        public GameModeSelectionControl()
        {
            InitializeComponent();

            Buttons.AddRange
            (
                new Dictionary<EGameMode, ToggleButton>
                {
                    { EGameMode.Arcade, _arcadeButton },
                    { EGameMode.Realistic, _realisticButton },
                    { EGameMode.Simulator, _simulatorButton },
                }
            );

            foreach (var buttonKeyValuePair in Buttons)
            {
                var gameMode = buttonKeyValuePair.Key;
                var button = buttonKeyValuePair.Value;

                button.Tag = gameMode;
                button.Click += OnClick;
            }
        }

        #endregion Constuctors
    }
}