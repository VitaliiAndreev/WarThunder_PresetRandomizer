using Core.Csv.WarThunder.Helpers.Interfaces;
using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Helpers.Logger;
using Core.Helpers.Logger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Csv.WarThunder.Helpers
{
    /// <summary> Provides methods to work with CSV data specific to War Thunder. </summary>
    public class CsvDeserializer : LoggerFluency, ICsvDeserializer
    {
        #region Constants

        private const string _fullNameSuffix = "_0";
        private const string _shortNameSuffix = "_1";
        private const string _shopNameSuffix = "_shop";

        #endregion Constants
        #region Constructors

        /// <summary> Creates a new CSV deserializer. </summary>
        /// <param name="loggers"> Instances of loggers. </param>
        public CsvDeserializer(params IConfiguredLogger[] loggers)
            : base(nameof(CsvDeserializer), loggers)
        {
            LogDebug($"{nameof(CsvDeserializer)} created.");
        }

        #endregion Constructors

        private Tuple<int, int> GetLocalisationIndeces(IList<IList<string>> localisationRecords, int index, string gaijinId)
        {
            var fullNameRecordIndex = -1;
            var shortNameRecordIndex = -1;

            for (var lineIndex = index - 1; !lineIndex.IsNegative(); lineIndex -= 1)
            {
                var currentRecord = localisationRecords[lineIndex];
                var recordId = currentRecord.First();

                if (fullNameRecordIndex.IsNegative() && recordId.SameAs($"{gaijinId}{_fullNameSuffix}"))
                    fullNameRecordIndex = lineIndex;

                if (shortNameRecordIndex.IsNegative() && recordId.SameAs($"{gaijinId}{_shortNameSuffix}"))
                    shortNameRecordIndex = lineIndex;

                if (!fullNameRecordIndex.IsNegative() && !shortNameRecordIndex.IsNegative())
                    break;
            }

            return new Tuple<int, int>(fullNameRecordIndex, shortNameRecordIndex);
        }

        /// <summary> Deserializes the given indexed collection of CSV records and uses that to initialize localization properties of the vehicles in the specified dictionary. </summary>
        /// <param name="vehicles"> The dictionary of vehicles to initialize localization of. </param>
        /// <param name="csvRecords"> The indexed collection of CSV records to deserialize. </param>
        public void DeserializeVehicleLocalization(IDictionary<string, IVehicle> vehicles, IList<IList<string>> csvRecords)
        {
            var gaijinIdPartsToSkip = new List<string>
            {
                "_football_",
                "_nw_",
                "_race_",
                "_space_suit_",
                "_tutorial_",
                "air_defence/",
                "ships/",
                "shop/group/",
                "structures/",
                "tracked_vehicles/",
                "unic_",
                "wheeled_vehicles/",
            };

            var sortedCsvRecords = csvRecords
                .Skip(1)
                .Where(record => !record.First().ContainsAny(gaijinIdPartsToSkip))
                .AsParallel()
                .ToList()
                .OrderBy(record => record.First())
                .ToList();

            for (var lineIndex = 1; lineIndex < sortedCsvRecords.Count(); lineIndex++) // Starts at 1 to skip headers.
            {
                var record = sortedCsvRecords[lineIndex];
                var recordGaijinId = record.First();

                if (recordGaijinId.EndsWith(_shopNameSuffix))
                {
                    var vehicleGaijinId = recordGaijinId.SkipLast(_shopNameSuffix.Count());

                    var indeces = GetLocalisationIndeces(sortedCsvRecords, lineIndex, vehicleGaijinId);
                    var fullNameRecordIndex = indeces.Item1;
                    var shortNameRecordIndex = indeces.Item2;

                    static IList<string> standardiseSpaces(IList<string> record) => record.Select(line => line.Replace(EGaijinCharacter.SpaceFromCsv, ' ')).ToList();

                    var shopNameRecord = standardiseSpaces(record);
                    var fullNameRecord = standardiseSpaces(sortedCsvRecords[fullNameRecordIndex]);
                    var shortNameRecord = standardiseSpaces(sortedCsvRecords[shortNameRecordIndex]);

                    if (vehicles.TryGetValue(vehicleGaijinId, out var vehicle))
                        vehicle.InitializeLocalization(fullNameRecord, shopNameRecord, shortNameRecord);

                    continue;
                }
            }
        }
    }
}