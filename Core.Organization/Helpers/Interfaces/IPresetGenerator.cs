using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Organization.Collections;
using Core.Organization.Enumerations;
using Core.Organization.Objects.SearchSpecifications;
using System.Collections.Generic;

namespace Core.Organization.Helpers.Interfaces
{
    public interface IPresetGenerator
    {
        void SetPlayableVehicles(IEnumerable<IVehicle> vehicles);

        /// <summary> Generates two vehicle presets (primary and fallback) based on the given specification. </summary>
        /// <param name="specification"> The specification to base vehicle selection on. </param>
        /// <returns></returns>
        IDictionary<EPreset, Preset> GeneratePrimaryAndFallbackPresets(Specification specification);
    }
}