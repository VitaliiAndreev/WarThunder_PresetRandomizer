using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Client.Wpf.Extensions;
using Core.DataBase.WarThunder.Enumerations;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

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
            CreateToggleButtons(_buttonGrid, new List<EGameMode> { EGameMode.Arcade, EGameMode.Realistic, EGameMode.Simulator }, null, EStyleKey.ToggleButton.BaseToggleButton);

            foreach (var buttonKeyValuePair in Buttons)
            {
                var gameMode = buttonKeyValuePair.Key;
                var button = buttonKeyValuePair.Value;

                void setImageAsButtonContent(string imageKey)
                {
                    button.Content = new Image()
                    {
                        Source = FindResource(imageKey) as ImageSource,
                        Style = this.GetStyle(EStyleKey.Image.Icon32px),
                    };
                }

                if (gameMode == EGameMode.Arcade)
                    setImageAsButtonContent(EBitmapImageKey.ArcadeIcon);

                else if (gameMode == EGameMode.Realistic)
                    setImageAsButtonContent(EBitmapImageKey.RealisticIcon);

                else if (gameMode == EGameMode.Simulator)
                    setImageAsButtonContent(EBitmapImageKey.SimulatorIcon);
            }
        }

        #endregion Constuctors
    }
}