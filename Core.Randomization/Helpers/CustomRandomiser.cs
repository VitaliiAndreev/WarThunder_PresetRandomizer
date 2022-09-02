using Core.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using Core.Randomization.Enumerations;
using Core.Randomization.Enumerations.Logger;
using Core.Randomization.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Randomization.Helpers
{
    /// <summary> Provides methods to randomise selection. </summary>
    public class CustomRandomiser : LoggerFluency, IRandomiser
    {
        #region Fields

        /// <summary> The random number generator. </summary>
        protected readonly Random _generator;

        #endregion Fields
        #region Constructors

        /// <summary> Creates a new randomiser. </summary>
        /// <param name="loggers"> Instances of loggers. </param>
        public CustomRandomiser(params IConfiguredLogger[] loggers)
            : base(ERandomizerLogCategory.Randomiser, loggers)
        {
            _generator = new Random();

            LogDebug(CoreLogMessage.Created.Format(ERandomizerLogCategory.Randomiser));
        }

        #endregion Constructors

        protected virtual T GetRandomCore<T>(IEnumerable<T> items, ERandomisationStep randomisationStep)
        {
            return items.ToList()[_generator.Next(items.Count())];
        }

        /// <summary> Picks randomly an item from the collection. </summary>
        /// <typeparam name="T"> The type of items in the collection. </typeparam>
        /// <param name="items"> The collection of items to pick from. </param>
        /// <param name="randomisationStep"> The current randomisation step. </param>
        /// <returns></returns>
        public T GetRandom<T>(IEnumerable<T> items, ERandomisationStep randomisationStep = ERandomisationStep.NotRelevant)
        {
            var itemType = typeof(T);

            if (items.IsEmpty())
            {
                if (itemType.IsEnum)
                {
                    var defaultEnumerationItem = itemType.GetEnumerationItems<T>().FirstOrDefault(item => item.ToString() == Word.None);

                    if (defaultEnumerationItem is null)
                        throw new Exception(CoreLogMessage.EnumerationHasNoDefaultItem.Format(itemType.ToStringLikeCode()));

                    return defaultEnumerationItem;
                }

                if (itemType.IsValueType)
                    throw new Exception(CoreLogMessage.NothingToSelectFrom);

                if (itemType.IsClass)
                    return default;
            }

            return GetRandomCore(items, randomisationStep);
        }
    }
}