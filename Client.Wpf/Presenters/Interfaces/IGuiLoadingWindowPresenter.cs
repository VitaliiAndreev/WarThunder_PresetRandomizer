namespace Client.Wpf.Presenters.Interfaces
{
    public interface IGuiLoadingWindowPresenter : IPresenter
    {
        #region Properties

        string CurrentLoadingStage { get; set; }

        int NationsToPopulate { get; set; }
        int NationsPopulated { get; set; }
        string CurrentlyPopulatedNation { get; set; }

        int BranchesToPopulate { get; set; }
        int BranchesPopulated { get; set; }
        string CurrentlyPopulatedBranch { get; set; }

        int RanksToPopulate { get; set; }
        int RanksPopulated { get; set; }
        string CurrentlyPopulatedRank { get; set; }

        int RowsToPopulate { get; set; }
        int RowsPopulated { get; set; }

        int ColumnsToPopulate { get; set; }
        int ColumnsPopulated { get; set; }

        string CurrentlyProcessedVehicle { get; set; }

        #endregion Properties
    }
}