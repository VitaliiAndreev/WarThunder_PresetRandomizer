using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Collections.Generic;

namespace Core.Csv.WarThunder.Helpers.Interfaces
{
    public interface ICsvDeserializer
    {
        /// <summary> Deserializes the given indexed collection of CSV records and uses that to initialize localization properties of the vehicles in the specified dictionary. </summary>
        /// <param name="vehicles"> The dictionary of vehicles to initialize localization of. </param>
        /// <param name="csvRecords"> The indexed collection of CSV records to deserialize. </param>
        void DeserializeVehicleLocalization(IDictionary<string, IVehicle> vehicles, IList<IList<string>> csvRecords);
    }
}