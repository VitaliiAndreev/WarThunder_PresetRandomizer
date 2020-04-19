using Core.DataBase.Objects.Interfaces;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    public interface IVehicleImages : IPersistentObjectWithId
    {
        #region Persistent Properties

        byte[] IconBytes { get; }

        byte[] PortraitBytes { get; }

        #endregion Persistent Properties
        #region Methods: Initialisation

        void SetIcon(byte[] bytes);

        void SetPortrait(byte[] bytes);

        #endregion Methods: Initialisation
    }
}