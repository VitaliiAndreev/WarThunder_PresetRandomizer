using Core.Randomization.Enumerations;
using System.Collections.Generic;

namespace Core.Randomization.Helpers.Interfaces
{
    /// <summary> Provides methods to randomise selection. </summary>
    public interface IRandomiser
    {
        /// <summary> Picks randomly an item from the collection. </summary>
        /// <typeparam name="T"> The type of items in the collection. </typeparam>
        /// <param name="items"> The collection of items to pick from. </param>
        /// <param name="randomisationStep"> The current randomisation step. </param>
        /// <returns></returns>
        T GetRandom<T>(IEnumerable<T> items, ERandomisationStep randomisationStep = ERandomisationStep.NotRelevant);
    }
}