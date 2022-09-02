using Client.Wpf.Enumerations;
using Client.Wpf.Enumerations.Logger;
using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Windows.Interfaces;
using Client.Wpf.Windows.Interfaces.Base;
using Core;
using Core.Extensions;
using System;
using System.Windows;
using Decimal = Core.Decimal;

namespace Client.Wpf.Windows
{
    /// <summary> Interaction logic for GuiLoadingWindow.xaml. </summary>
    public partial class GuiLoadingWindow : BaseWindow, IGuiLoadingWindow
    {
        #region Properties

        new public IGuiLoadingWindowPresenter Presenter => base.Presenter as IGuiLoadingWindowPresenter;

        IPresenter IWindowWithPresenter.Presenter => base.Presenter;

        #region CurrentLoadingStage

        public static readonly DependencyProperty CurrentLoadingStageProperty = DependencyProperty.Register
        (
            nameof(CurrentLoadingStage),
            typeof(string),
            typeof(GuiLoadingWindow),
            new UIPropertyMetadata($"{Character.Period}{Character.Period}{Character.Period}")
        );

        public string CurrentLoadingStage
        {
            get { return (string)GetValue(CurrentLoadingStageProperty); }
            set { SetValue(CurrentLoadingStageProperty, value); }
        }

        #endregion CurrentLoadingStage
        #region NationsToPopulate

        public static readonly DependencyProperty NationsToPopulateProperty =
            DependencyProperty.Register(nameof(NationsToPopulate), typeof(int), typeof(GuiLoadingWindow), new UIPropertyMetadata(Integer.Number.One));

        public int NationsToPopulate
        {
            get => (int)GetValue(NationsToPopulateProperty);
            set => SetValue(NationsToPopulateProperty, value);
        }

        #endregion NationsToPopulate
        #region NationsPopulated

        public static readonly DependencyProperty NationsPopulatedProperty =
            DependencyProperty.Register(nameof(NationsPopulated), typeof(int), typeof(GuiLoadingWindow), new UIPropertyMetadata(Integer.Number.Zero));

        public int NationsPopulated
        {
            get => (int)GetValue(NationsPopulatedProperty);
            set => SetValue(NationsPopulatedProperty, value);
        }

        #endregion NationsPopulated
        #region NationsPopulatedDecimal

        public static readonly DependencyProperty NationsPopulatedDecimalProperty =
            DependencyProperty.Register(nameof(NationsPopulatedDecimal), typeof(decimal), typeof(GuiLoadingWindow), new UIPropertyMetadata(Decimal.Number.Zero));

        public decimal NationsPopulatedDecimal
        {
            get => (decimal)GetValue(NationsPopulatedDecimalProperty);
            set => SetValue(NationsPopulatedDecimalProperty, value);
        }

        #endregion NationsPopulated
        #region CurrentlyPopulatedNation

        public static readonly DependencyProperty CurrentlyPopulatedNationProperty =
            DependencyProperty.Register(nameof(CurrentlyPopulatedNation), typeof(string), typeof(GuiLoadingWindow), new UIPropertyMetadata(string.Empty));

        public string CurrentlyPopulatedNation
        {
            get => (string)GetValue(CurrentlyPopulatedNationProperty);
            set => SetValue(CurrentlyPopulatedNationProperty, value);
        }

        #endregion CurrentlyPopulatedNation
        #region BranchesToPopulate

        public static readonly DependencyProperty BranchesToPopulateProperty =
            DependencyProperty.Register(nameof(BranchesToPopulate), typeof(int), typeof(GuiLoadingWindow), new UIPropertyMetadata(Integer.Number.One));

        public int BranchesToPopulate
        {
            get => (int)GetValue(BranchesToPopulateProperty);
            set => SetValue(BranchesToPopulateProperty, value);
        }

        #endregion BranchesToPopulate
        #region BranchesPopulated

        public static readonly DependencyProperty BranchesPopulatedProperty =
            DependencyProperty.Register(nameof(BranchesPopulated), typeof(int), typeof(GuiLoadingWindow), new UIPropertyMetadata(Integer.Number.Zero));

        public int BranchesPopulated
        {
            get => (int)GetValue(BranchesPopulatedProperty);
            set => SetValue(BranchesPopulatedProperty, value);
        }

        #endregion BranchesPopulated
        #region BranchesPopulatedDecimal

        public static readonly DependencyProperty BranchesPopulatedDecimalProperty =
            DependencyProperty.Register(nameof(BranchesPopulatedDecimal), typeof(decimal), typeof(GuiLoadingWindow), new UIPropertyMetadata(Decimal.Number.Zero));

        public decimal BranchesPopulatedDecimal
        {
            get => (decimal)GetValue(BranchesPopulatedDecimalProperty);
            set => SetValue(BranchesPopulatedDecimalProperty, value);
        }

        #endregion BranchesPopulated
        #region CurrentlyPopulatedNation

        public static readonly DependencyProperty CurrentlyPopulatedBranchProperty =
            DependencyProperty.Register(nameof(CurrentlyPopulatedBranch), typeof(string), typeof(GuiLoadingWindow), new UIPropertyMetadata(string.Empty));

        public string CurrentlyPopulatedBranch
        {
            get => (string)GetValue(CurrentlyPopulatedBranchProperty);
            set => SetValue(CurrentlyPopulatedBranchProperty, value);
        }

        #endregion CurrentlyPopulatedNation
        #region RanksToPopulate

        public static readonly DependencyProperty RanksToPopulateProperty =
            DependencyProperty.Register(nameof(RanksToPopulate), typeof(int), typeof(GuiLoadingWindow), new UIPropertyMetadata(Integer.Number.One));

        public int RanksToPopulate
        {
            get => (int)GetValue(RanksToPopulateProperty);
            set => SetValue(RanksToPopulateProperty, value);
        }

        #endregion RanksToPopulate
        #region RanksPopulated

        public static readonly DependencyProperty RanksPopulatedProperty =
            DependencyProperty.Register(nameof(RanksPopulated), typeof(int), typeof(GuiLoadingWindow), new UIPropertyMetadata(Integer.Number.Zero));

        public int RanksPopulated
        {
            get => (int)GetValue(RanksPopulatedProperty);
            set => SetValue(RanksPopulatedProperty, value);
        }

        #endregion RanksPopulated
        #region RanksPopulatedDecimal

        public static readonly DependencyProperty RanksPopulatedDecimalProperty =
            DependencyProperty.Register(nameof(RanksPopulatedDecimal), typeof(decimal), typeof(GuiLoadingWindow), new UIPropertyMetadata(Decimal.Number.Zero));

        public decimal RanksPopulatedDecimal
        {
            get => (decimal)GetValue(RanksPopulatedDecimalProperty);
            set => SetValue(RanksPopulatedDecimalProperty, value);
        }

        #endregion RanksPopulatedDecimal
        #region CurrentlyPopulatedRank

        public static readonly DependencyProperty CurrentlyPopulatedRankProperty =
            DependencyProperty.Register(nameof(CurrentlyPopulatedRank), typeof(string), typeof(GuiLoadingWindow), new UIPropertyMetadata(string.Empty));

        public string CurrentlyPopulatedRank
        {
            get => (string)GetValue(CurrentlyPopulatedRankProperty);
            set => SetValue(CurrentlyPopulatedRankProperty, value);
        }

        #endregion CurrentlyPopulatedRank
        #region RowsToPopulate

        public static readonly DependencyProperty RowsToPopulateProperty =
            DependencyProperty.Register(nameof(RowsToPopulate), typeof(int), typeof(GuiLoadingWindow), new UIPropertyMetadata(Integer.Number.One));

        public int RowsToPopulate
        {
            get => (int)GetValue(RowsToPopulateProperty);
            set => SetValue(RowsToPopulateProperty, value);
        }

        #endregion RowsToPopulate
        #region RowsPopulated

        public static readonly DependencyProperty RowsPopulatedProperty =
            DependencyProperty.Register(nameof(RowsPopulated), typeof(int), typeof(GuiLoadingWindow), new UIPropertyMetadata(Integer.Number.Zero));

        public int RowsPopulated
        {
            get => (int)GetValue(RowsPopulatedProperty);
            set => SetValue(RowsPopulatedProperty, value);
        }

        #endregion RowsPopulated
        #region RowsPopulatedDecimal

        public static readonly DependencyProperty RowsPopulatedDecimalProperty =
            DependencyProperty.Register(nameof(RowsPopulatedDecimal), typeof(decimal), typeof(GuiLoadingWindow), new UIPropertyMetadata(Decimal.Number.Zero));

        public decimal RowsPopulatedDecimal
        {
            get => (decimal)GetValue(RowsPopulatedDecimalProperty);
            set => SetValue(RowsPopulatedDecimalProperty, value);
        }

        #endregion RowsPopulatedDecimal
        #region ColumnsToPopulate

        public static readonly DependencyProperty ColumnsToPopulateProperty =
            DependencyProperty.Register(nameof(ColumnsToPopulate), typeof(int), typeof(GuiLoadingWindow), new UIPropertyMetadata(Integer.Number.One));

        public int ColumnsToPopulate
        {
            get => (int)GetValue(ColumnsToPopulateProperty);
            set => SetValue(ColumnsToPopulateProperty, value);
        }

        #endregion ColumnsToPopulate
        #region ColumnsPopulated

        public static readonly DependencyProperty ColumnsPopulatedProperty =
            DependencyProperty.Register(nameof(ColumnsPopulated), typeof(int), typeof(GuiLoadingWindow), new UIPropertyMetadata(Integer.Number.Zero));

        public int ColumnsPopulated
        {
            get => (int)GetValue(ColumnsPopulatedProperty);
            set => SetValue(ColumnsPopulatedProperty, value);
        }

        #endregion ColumnsPopulated
        #region CurrentlyProcessedVehicle

        public static readonly DependencyProperty CurrentlyProcessedVehicleProperty =
            DependencyProperty.Register(nameof(CurrentlyProcessedVehicle), typeof(string), typeof(GuiLoadingWindow), new UIPropertyMetadata(string.Empty));

        public string CurrentlyProcessedVehicle
        {
            get => (string)GetValue(CurrentlyProcessedVehicleProperty);
            set => SetValue(CurrentlyProcessedVehicleProperty, value);
        }

        #endregion CurrentlyProcessedVehicle

        #endregion Properties
        #region Constructors

        public GuiLoadingWindow()
            : base(EWpfClientLogCategory.GuiLoadingWindow, null, null)
        {
            InitializeComponent();
        }

        public GuiLoadingWindow(IGuiLoadingWindowPresenter presenter)
            : base(EWpfClientLogCategory.GuiLoadingWindow, null, presenter)
        {
            InitializeComponent();
            Localise();
        }

        #endregion Constructors
        #region Methods: Overrides

        public override void Localise()
        {
            base.Localise();

            Title = ApplicationHelpers.LocalisationManager.GetLocalisedString(ELocalisationKey.ApplicationName);
        }

        #endregion Methods: Overrides
        #region Methods: Calculation

        public void RecalculateNationsPopulatedDecimal()
        {
            NationsPopulatedDecimal = NationsPopulated;
            
            if (BranchesToPopulate.IsPositive())
                NationsPopulatedDecimal += BranchesPopulatedDecimal / BranchesToPopulate;
        }

        public void RecalculateBranchesPopulatedDecimal()
        {
            BranchesPopulatedDecimal = BranchesPopulated;

            if (RanksToPopulate.IsPositive())
                BranchesPopulatedDecimal += RanksPopulatedDecimal / RanksToPopulate;
        }

        public void RecalculateRanksPopulatedDecimal()
        {
            RanksPopulatedDecimal = RanksPopulated;

            if (RowsToPopulate.IsPositive())
                RanksPopulatedDecimal += RowsPopulatedDecimal / RowsToPopulate;
        }

        public void RecalculateRowsPopulatedDecimal()
        {
            RowsPopulatedDecimal = RowsPopulated;

            if (ColumnsToPopulate.IsPositive())
                RowsPopulatedDecimal += Convert.ToDecimal(ColumnsPopulated) / ColumnsToPopulate;
        }

        #endregion Methods: Calculation
    }
}