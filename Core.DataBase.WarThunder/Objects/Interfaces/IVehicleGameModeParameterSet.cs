namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    /// <summary> A set of parameters that vary depending on the game mode. </summary>
    /// <typeparam name="T"> The type of parameter values. </typeparam>
    public interface IVehicleGameModeParameterSet<T> : IVehicleGameModeParameterSetBase
    {
        /// <summary> The value in Arcade Battles. </summary>
        T Arcade { get; }
        /// <summary> The value in Realistic Battles. </summary>
        T Realistic { get; }
        /// <summary> The value in Simulator Battles. </summary>
        T Simulator { get; }
        /// <summary> The value in Event Battles. </summary>
        T Event { get; }
    }
}