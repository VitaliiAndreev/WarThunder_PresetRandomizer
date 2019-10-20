using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using Core.Randomization.Enumerations.Logger;
using Core.Randomization.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Randomization.Helpers
{
    /// <summary> Provides methods to randomize selection. </summary>
    public class CustomRandomizer : LoggerFluency, IRandomizer
    {
        #region Fields

        /// <summary> The random number generator. </summary>
        private readonly Random _generator;

        #endregion Fields
        #region Constructors

        /// <summary> Creates a new randomizer. </summary>
        /// <param name="loggers"> Instances of loggers. </param>
        public CustomRandomizer(params IConfiguredLogger[] loggers)
            : base(ERandomizerLogCategory.Ranzomizer, loggers)
        {
            _generator = new Random();

            LogDebug(ECoreLogMessage.Created.FormatFluently(ERandomizerLogCategory.Ranzomizer));
        }

        #endregion Constructors

        /// <summary> Picks randomly an item from the collection. </summary>
        /// <typeparam name="T"> The type of items in the collection. </typeparam>
        /// <param name="items"> The collection of items to pick from. </param>
        /// <returns></returns>
        public T GetRandom<T>(IEnumerable<T> items)
        {
            var itemType = typeof(T);

            if (items.IsEmpty())
            {
                if (itemType.IsValueType)
                    throw new Exception(ECoreLogMessage.NothingToSelectFrom);

                if (itemType.IsClass)
                    return default;
            }

            var itemList = items.ToList();

            return itemList[_generator.Next(itemList.Count)];
        }
    }
}