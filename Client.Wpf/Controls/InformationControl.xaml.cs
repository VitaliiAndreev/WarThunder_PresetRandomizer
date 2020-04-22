﻿using Client.Wpf.Controls.Base;
using Client.Wpf.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;

namespace Client.Wpf.Controls
{
    /// <summary> Interaction logic for InformationControl.xaml. </summary>
    public partial class InformationControl : LocalizedUserControl
    {
        #region Properties

        public ResearchTreeControl ResearchTreeControl => _researchTreeControl;

        #endregion Properties
        #region Constructors

        public InformationControl()
        {
            InitializeComponent();
        }

        #endregion Constructors
        #region Methods: Overrides

        public override void Localise()
        {
            base.Localise();

            _researchTreeHeader.Text = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.ResearchTrees);
            _statisticsHeader.Text = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.Statistics);
            _vehicleCardHeader.Text = ApplicationHelpers.LocalizationManager.GetLocalizedString(ELocalizationKey.VehicleInformation);

            _researchTreeControl.Localise();
        }

        #endregion Methods: Overrides
        #region Methods: Navigation

        public void BringIntoView(IVehicle vehicle, bool changeTabs = false)
        {
            if (changeTabs)
                _tabControl.SelectedItem = _researchTreeTab;

            _researchTreeControl.BringIntoView(vehicle, changeTabs);
        }

        #endregion Methods: Navigation
    }
}