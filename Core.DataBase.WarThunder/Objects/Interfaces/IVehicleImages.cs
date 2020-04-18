using Core.DataBase.Objects.Interfaces;
using System.Drawing;
using System.IO;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    public interface IVehicleImages : IPersistentObjectWithId
    {
        #region Persistent Properties

        Bitmap Icon { get; }

        #endregion Persistent Properties
        #region Methods: Initialisation

        void SetIcon(Bitmap image);

        #endregion Methods: Initialisation
    }
}