using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using System.Collections.Generic;

namespace Core.DataBase.WarThunder.Extensions
{
    /// <summary> Methods extending the <see cref="IVehicleGameModeParameterSet{T}"/> interface. </summary>
    public static class IVehicleGameModeParameterSetExtensions
    {
        public static IDictionary<EGameMode, T> AsDictionary<T>(this IVehicleGameModeParameterSet<T> parameterSet) =>
            new Dictionary<EGameMode, T>
            {
                { EGameMode.Arcade, parameterSet.Arcade},
                { EGameMode.Realistic, parameterSet.Realistic},
                { EGameMode.Simulator, parameterSet.Simulator},
                { EGameMode.Event, parameterSet.Event},
            };
    }
}