using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Extensions;
using Core.Enumerations;
using Core.Extensions;
using Core.Helpers.Logger.Interfaces;
using Core.Organization.Objects;
using Core.Randomization.Enumerations;
using Core.Randomization.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Organization.Helpers
{
    public class CustomRandomiserWithNormalisation : CustomRandomiser
    {
        #region Fields

        private readonly IDictionary<BranchSet, IDictionary<EBranch, int>> _mainBranchOccurrences;
        private readonly IDictionary<BranchSet, IDictionary<EBranch, int>> _mainBranchWeights;

        #endregion Fields
        #region Constructors

        /// <summary> Creates a new randomiser. </summary>
        /// <param name="loggers"> Instances of loggers. </param>
        public CustomRandomiserWithNormalisation(params IConfiguredLogger[] loggers)
            : base(loggers)
        {
            _mainBranchOccurrences = new Dictionary<BranchSet, IDictionary<EBranch, int>>();
            _mainBranchWeights = new Dictionary<BranchSet, IDictionary<EBranch, int>>();
        }

        #endregion Constructors
        #region Methods: Overrides

        protected override T GetRandomCore<T>(IEnumerable<T> items, ERandomisationStep randomisationStep)
        {
            if (typeof(T) == typeof(EBranch) && randomisationStep == ERandomisationStep.MainBranchWhenSelectingByCategories)
            {
                var branches = items.OfType<EBranch>().ToList();
                var branchSet = new BranchSet(branches);

                AddIfMissing(branchSet, branches);

                var mainBranchOccurrences = _mainBranchOccurrences[branchSet];
                var selectedBranch = EBranch.None;

                if (mainBranchOccurrences.Values.AllEqual())
                {
                    selectedBranch = base.GetRandomCore(branches, randomisationStep).CastTo<EBranch>();
                }
                else
                {
                    var mostOccurences = mainBranchOccurrences.Max(keyValuePair => keyValuePair.Value);
                    var leastOccurrences = mainBranchOccurrences.Min(keyValuePair => keyValuePair.Value);
                    var leastOccurredMainBranches = mainBranchOccurrences.GetKeyWhereValue(occurrenceCount => occurrenceCount == leastOccurrences);

                    IncreaseMainBranchWeights(branchSet, leastOccurredMainBranches, mostOccurences - leastOccurrences);

                    selectedBranch = GetRandomMainBranch(branchSet, branches);

                    ResetMainBranchWeights(branchSet, branches);
                }

                mainBranchOccurrences[selectedBranch] += EInteger.Number.One;

                return selectedBranch.CastTo<T>();
            }
            else
            {
                return base.GetRandomCore(items, randomisationStep);
            }
        }

        #endregion Methods: Overrides

        private void AddIfMissing(BranchSet branchSet, IEnumerable<EBranch> branches)
        {
            if (!_mainBranchOccurrences.ContainsKey(branchSet))
            {
                var internalDictionary = new Dictionary<EBranch, int>();

                foreach (var branch in branches)
                    internalDictionary.Add(branch, EInteger.Number.Zero);

                _mainBranchOccurrences.Add(branchSet, internalDictionary);
            }

            if (!_mainBranchWeights.ContainsKey(branchSet))
            {
                var internalDictionary = new Dictionary<EBranch, int>();

                foreach (var branch in branches)
                    internalDictionary.Add(branch, EInteger.Number.One);

                _mainBranchWeights.Add(branchSet, internalDictionary);
            }
        }

        private void ResetMainBranchWeights(BranchSet branchSet, IEnumerable<EBranch> branches)
        {
            var mainBranchWeights = _mainBranchWeights[branchSet];

            foreach (var branch in branches)
                mainBranchWeights[branch] = EInteger.Number.One;
        }

        private void IncreaseMainBranchWeights(BranchSet branchSet, IEnumerable<EBranch> branches, int increment)
        {
            foreach (var branch in branches)
                _mainBranchWeights[branchSet][branch] += increment;
        }

        private EBranch GetRandomMainBranch(BranchSet branchSet, IEnumerable<EBranch> branches)
        {
            var scaleStringBuilder = new StringBuilder();

            foreach (var branch in branches)
            {
                var weight = _mainBranchWeights[branchSet][branch];
                var branchCode = branch.GetSingleDigitCode();
                var paddedString = string.Empty.PadLeft(weight, branchCode.ToString().First());

                scaleStringBuilder.Append(paddedString);
            }

            var scaleString = scaleStringBuilder.ToString();
            var selectedBranchCode = int.Parse(scaleString[_generator.Next(scaleString.Count())].ToString());

            return EBranchExtensions.FromSingleDigitCode(selectedBranchCode);
        }
    }
}
