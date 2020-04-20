using Core.DataBase.Objects.Interfaces;

namespace Core.DataBase.WarThunder.Objects.Interfaces
{
    public interface IPersistentObjectWithIdAndVehicle : IPersistentObjectWithId
    {
        public IVehicle Vehicle { get; }
    }
}