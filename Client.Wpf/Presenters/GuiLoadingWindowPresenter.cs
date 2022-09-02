using Client.Wpf.Presenters.Interfaces;
using Client.Wpf.Windows.Interfaces;
using Core.Enumerations;
using Core.Extensions;
using System;
using System.Windows.Threading;

namespace Client.Wpf.Presenters
{
    public class GuiLoadingWindowPresenter : Presenter, IGuiLoadingWindowPresenter
    {
        #region Properties

        new public IGuiLoadingWindow Owner => base.Owner as IGuiLoadingWindow;

        public string CurrentLoadingStage
        {
            get => Owner?.Invoke(DispatcherPriority.Send, () => { return Owner.CurrentLoadingStage; }).ToString() ?? string.Empty;
            set => Owner?.Invoke(DispatcherPriority.Send, () => { Owner.CurrentLoadingStage = value; });
        }

        public int NationsToPopulate
        {
            get => Owner?.Invoke(DispatcherPriority.Send, () => { return Owner.NationsToPopulate; }).CastTo<int>() ?? Integer.Number.One;
            set => Owner?.Invoke(DispatcherPriority.Send, () => { Owner.NationsToPopulate = value; });
        }
        public int NationsPopulated
        {
            get => Owner?.Invoke(DispatcherPriority.Send, () => { return Owner.NationsPopulated; }).CastTo<int>() ?? Integer.Number.Zero;
            set => Owner?.Invoke
                (
                    DispatcherPriority.Send, () =>
                    {
                        Owner.NationsPopulated = value;
                        Owner.NationsPopulatedDecimal = value;
                    }
                );
        }
        public string CurrentlyPopulatedNation
        {
            get => Owner?.Invoke(DispatcherPriority.Send, () => { return Owner.CurrentlyPopulatedNation; }).ToString() ?? string.Empty;
            set => Owner?.Invoke(DispatcherPriority.Send, () => { Owner.CurrentlyPopulatedNation = value; });
        }

        public int BranchesToPopulate
        {
            get => Owner?.Invoke(DispatcherPriority.Send, () => { return Owner.BranchesToPopulate; }).CastTo<int>() ?? Integer.Number.One;
            set => Owner?.Invoke(DispatcherPriority.Send, () => { Owner.BranchesToPopulate = value; });
        }
        public int BranchesPopulated
        {
            get => Owner?.Invoke(DispatcherPriority.Send, () => { return Owner.BranchesPopulated; }).CastTo<int>() ?? Integer.Number.Zero;
            set => Owner?.Invoke
                (
                    DispatcherPriority.Send,
                    () =>
                    {
                        Owner.BranchesPopulated = value;
                        Owner.BranchesPopulatedDecimal = value;
                        Owner.RecalculateNationsPopulatedDecimal();
                    }
                );
        }
        public string CurrentlyPopulatedBranch
        {
            get => Owner?.Invoke(DispatcherPriority.Send, () => { return Owner.CurrentlyPopulatedBranch; }).ToString() ?? string.Empty;
            set => Owner?.Invoke(DispatcherPriority.Send, () => { Owner.CurrentlyPopulatedBranch = value; });
        }

        public int RanksToPopulate
        {
            get => Owner?.Invoke(DispatcherPriority.Send, () => { return Owner.RanksToPopulate; }).CastTo<int>() ?? Integer.Number.One;
            set => Owner?.Invoke(DispatcherPriority.Send, () => { Owner.RanksToPopulate = value; });
        }
        public int RanksPopulated
        {
            get => Owner?.Invoke(DispatcherPriority.Send, () => { return Owner.RanksPopulated; }).CastTo<int>() ?? Integer.Number.Zero;
            set => Owner?.Invoke
                (
                    DispatcherPriority.Send,
                    () =>
                    {
                        Owner.RanksPopulated = value;
                        Owner.RanksPopulatedDecimal = value;
                        Owner.RecalculateBranchesPopulatedDecimal();
                        Owner.RecalculateNationsPopulatedDecimal();
                    }
                );
        }
        public string CurrentlyPopulatedRank
        {
            get => Owner?.Invoke(DispatcherPriority.Send, () => { return Owner.CurrentlyPopulatedRank; }).ToString() ?? string.Empty;
            set => Owner?.Invoke(DispatcherPriority.Send, () => { Owner.CurrentlyPopulatedRank = value; });
        }

        public string CurrentlyProcessedVehicle
        {
            get => Owner?.Invoke(DispatcherPriority.Send, () => { return Owner.CurrentlyProcessedVehicle; }).ToString() ?? string.Empty;
            set => Owner?.Invoke(DispatcherPriority.Send, () => { Owner.CurrentlyProcessedVehicle = value; });
        }

        public int RowsToPopulate
        {
            get => Owner?.Invoke(DispatcherPriority.Send, () => { return Owner.RowsToPopulate; }).CastTo<int>() ?? Integer.Number.One;
            set => Owner?.Invoke(DispatcherPriority.Send, () => { Owner.RowsToPopulate = value; });
        }
        public int RowsPopulated
        {
            get => Owner?.Invoke(DispatcherPriority.Send, () => { return Owner.RowsPopulated; }).CastTo<int>() ?? Integer.Number.Zero;
            set => Owner?.Invoke
                (
                    DispatcherPriority.Send,
                    () =>
                    {
                        Owner.RowsPopulated = value;
                        Owner.RowsPopulatedDecimal = value;
                        Owner.RecalculateRanksPopulatedDecimal();
                        Owner.RecalculateBranchesPopulatedDecimal();
                        Owner.RecalculateNationsPopulatedDecimal();
                    }
                );
        }


        public int ColumnsToPopulate
        {
            get => Owner?.Invoke(DispatcherPriority.Send, () => { return Owner.ColumnsToPopulate; }).CastTo<int>() ?? Integer.Number.One;
            set => Owner?.Invoke(DispatcherPriority.Send, () => { Owner.ColumnsToPopulate = value; });
        }
        public int ColumnsPopulated
        {
            get => Owner?.Invoke(DispatcherPriority.Send, () => { return Owner.ColumnsPopulated; }).CastTo<int>() ?? Integer.Number.Zero;
            set => Owner?.Invoke
                (
                    DispatcherPriority.Send,
                    () =>
                    {
                        Owner.ColumnsPopulated = value;
                        Owner.RecalculateRowsPopulatedDecimal();
                        Owner.RecalculateRanksPopulatedDecimal();
                        Owner.RecalculateBranchesPopulatedDecimal();
                        Owner.RecalculateNationsPopulatedDecimal();
                    }
                );
        }

        #endregion Properties
        #region Constructors

        public GuiLoadingWindowPresenter()
            : base(null)
        {
        }

        #endregion Constructors
    }
}