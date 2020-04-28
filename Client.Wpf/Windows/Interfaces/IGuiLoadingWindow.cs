using Client.Wpf.Presenters.Interfaces;
using Core.DataBase.WarThunder.Enumerations;

namespace Client.Wpf.Windows.Interfaces
{
    public interface IGuiLoadingWindow : IBaseWindow
    {
        #region Properties

        new IGuiLoadingWindowPresenter Presenter { get; }

        string CurrentLoadingStage { get; set; }

        int NationsToPopulate { get; set; }
        int NationsPopulated { get; set; }
        decimal NationsPopulatedDecimal { get; set; }
        string CurrentlyPopulatedNation { get; set; }

        int BranchesToPopulate { get; set; }
        int BranchesPopulated { get; set; }
        decimal BranchesPopulatedDecimal { get; set; }
        string CurrentlyPopulatedBranch { get; set; }

        int RanksToPopulate { get; set; }
        int RanksPopulated { get; set; }
        decimal RanksPopulatedDecimal { get; set; }
        string CurrentlyPopulatedRank { get; set; }

        int RowsToPopulate { get; set; }
        int RowsPopulated { get; set; }
        decimal RowsPopulatedDecimal { get; set; }

        int ColumnsToPopulate { get; set; }
        int ColumnsPopulated { get; set; }

        string CurrentlyProcessedVehicle { get; set; }

        #endregion Properties
        #region Methods: Calculation

        void RecalculateNationsPopulatedDecimal();
        void RecalculateBranchesPopulatedDecimal();
        void RecalculateRanksPopulatedDecimal();
        void RecalculateRowsPopulatedDecimal();

        #endregion Methods: Calculation
    }
}