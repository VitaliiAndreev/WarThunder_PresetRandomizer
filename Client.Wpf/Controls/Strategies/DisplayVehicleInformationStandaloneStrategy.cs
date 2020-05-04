using Core.DataBase.WarThunder.Enumerations;
using Core.DataBase.WarThunder.Objects.Interfaces;
using Core.Enumerations;
using System.Text;

namespace Client.Wpf.Controls.Strategies
{
    /// <summary> A strategy for generating a formatted string with extended <see cref="IVehicle"/> information for the given <see cref="EGameMode"/>. </summary>
    public class DisplayVehicleInformationStandaloneStrategy : DisplayVehicleInformationStrategy
    {
        #region Methods: Output

        /// <summary> Generates a formatted string with <paramref name="vehicle"/> information for the given <paramref name="gameMode"/>. </summary>
        /// <param name="gameMode"> The game mode to account for. </param>
        /// <param name="vehicle"> The vehicle whose information to display. </param>
        /// <returns></returns>
        public override string GetVehicleInfoBottomRow(EGameMode gameMode, IVehicle vehicle)
        {
            var stringBuilder = new StringBuilder();

            void append(object stringOrCharacter) => stringBuilder.Append(stringOrCharacter);

            SetFirstSharedPart(stringBuilder, gameMode, vehicle);

            append($"{ESeparator.SpaceSlashSpace}");
            append(GetRank(vehicle));

            SetSecondSharedPart(stringBuilder, vehicle);

            return stringBuilder.ToString();
        }

        #endregion Methods: Output
    }
}