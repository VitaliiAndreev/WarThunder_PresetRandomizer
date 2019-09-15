using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for ResearchTreeControl.xaml. </summary>
    public partial class ResearchTreeControl : UserControl
    {
        #region Fields

        /// <summary> The map of the nation enumeration onto corresponding tabs. </summary>
        private readonly IDictionary<ENation, TabItem> _nationTabs;

        /// <summary> The map of the nation enumeration onto corresponding controls. </summary>
        private readonly IDictionary<ENation, ResearchTreeNationControl> _nationControls;

        #endregion Fields
        #region Constructors

        /// <summary> Creates a new control. </summary>
        public ResearchTreeControl()
        {
            InitializeComponent();

            _nationTabs = new Dictionary<ENation, TabItem>
            {
                { ENation.Usa, _usaTab },
                { ENation.Germany, _germanyTab },
                { ENation.Ussr, _ussrTab },
                { ENation.Commonwealth, _britainTab },
                { ENation.Japan, _japanTab },
                { ENation.China, _chinaTab },
                { ENation.Italy, _italyTab },
                { ENation.France, _franceTab },
            };
            _nationControls = new Dictionary<ENation, ResearchTreeNationControl>
            {
                { ENation.Usa, _usaTree },
                { ENation.Germany, _germanyTree },
                { ENation.Ussr, _ussrTree },
                { ENation.Commonwealth, _britainTree },
                { ENation.Japan, _japanTree },
                { ENation.China, _chinaTree },
                { ENation.Italy, _italyTree },
                { ENation.France, _franceTree },
            };
        }

        #endregion Constructors

        /// <summary> Applies localization to visible text on the control. </summary>
        public void Localize()
        {
            _usaTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Usa);
            _germanyTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Germany);
            _ussrTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Ussr);
            _britainTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Britain);
            _japanTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Japan);
            _chinaTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.China);
            _italyTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Italy);
            _franceTab.Header = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.France);
        }

        /// <summary> Populates tabs with appropriate research trees. </summary>
        public void Populate()
        {
            foreach (var nationTabKeyValuePair in _nationTabs)
            {
                if (ApplicationHelpers.Manager.ResearchTrees.TryGetValue(nationTabKeyValuePair.Key, out var researchTree))
                {
                    _nationControls[nationTabKeyValuePair.Key].Populate(researchTree);
                    continue;
                }
                nationTabKeyValuePair.Value.IsEnabled = false;
            }
        }

        /// <summary> Displays <see cref="IVehicle.BattleRating"/> values for the given <paramref name="gameMode"/>. </summary>
        /// <param name="gameMode"> The game mode for which to display the battle rating. </param>
        public void DisplayBattleRatingFor(EGameMode gameMode)
        {
            foreach (var vehicleCell in _nationControls.Values)
                vehicleCell.DisplayBattleRatingFor(gameMode);
        }
    }
}