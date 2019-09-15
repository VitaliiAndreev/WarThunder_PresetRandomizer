using Core.Csv.WarThunder.Enumerations.Logger;
using Core.Csv.WarThunder.Helpers.Interfaces;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using Core.Enumerations.Logger;
using Core.Extensions;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Core.Csv.WarThunder.Helpers
{
    /// <summary> Provides methods to work with CSV data specific to War Thunder. </summary>
    public class CsvDeserializer : LoggerFluency, ICsvDeserializer
    {
        #region Constructors

        /// <summary> Creates a new CSV deserializer. </summary>
        /// <param name="loggers"> Instances of loggers. </param>
        public CsvDeserializer(params IConfiguredLogger[] loggers)
            : base(ECsvLogCategory.CsvDeserializer, loggers)
        {
            LogDebug(ECoreLogMessage.Created.FormatFluently(ECsvLogCategory.CsvDeserializer));
        }

        #endregion Constructors
    }
}