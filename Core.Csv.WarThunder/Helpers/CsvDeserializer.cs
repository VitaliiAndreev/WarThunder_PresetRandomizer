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

        /// <summary> Deserializes the given indexed collection of CSV records and uses that to initialize localization properties of the vehicles in the specified dictionary. </summary>
        /// <param name="vehicles"> The dictionary of vehicles to initialize localization of. </param>
        /// <param name="csvRecords"> The indexed collection of CSV records to deserialize. </param>
        public void DeserializeVehicleLocalization(IDictionary<string, IVehicle> vehicles, IList<IList<string>> csvRecords)
        {
            for (var lineIndex = EInteger.Number.One; lineIndex < csvRecords.Count(); lineIndex++) // Starts at 1 to skip headers.
            {
                var record = csvRecords[lineIndex];
                var recordGaijinId = record.First();
                var shopNameSuffix = "_shop";

                if (recordGaijinId.EndsWith(shopNameSuffix))
                {
                    var vehicleGaijinId = recordGaijinId.SkipLast(shopNameSuffix.Count());

                    var fullNameRecordIndex = lineIndex + EInteger.Number.One;
                    var shortNameRecordIndex = fullNameRecordIndex + EInteger.Number.One;
                    var classNameRecordIndex = shortNameRecordIndex + EInteger.Number.One;

                    var shopNameRecord = record;
                    var fullNameRecord = csvRecords[fullNameRecordIndex];
                    var shortNameRecord = csvRecords[shortNameRecordIndex];
                    var classNameRecord = csvRecords[classNameRecordIndex];

                    if (!csvRecords[classNameRecordIndex].First().Contains(vehicleGaijinId))
                    {
                        classNameRecord = shortNameRecord;
                        classNameRecordIndex -= EInteger.Number.One;
                    }

                    if (vehicles.TryGetValue(vehicleGaijinId, out var vehicle))
                        vehicle.InitializeLocalization(fullNameRecord, shopNameRecord, shortNameRecord, classNameRecord);

                    lineIndex = classNameRecordIndex;
                    continue;
                }
            }
        }
    }
}