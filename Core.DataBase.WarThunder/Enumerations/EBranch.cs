using System;

namespace Core.DataBase.WarThunder.Enumerations
{
    /// <summary> Military branches available in War Thunder. </summary>
    [Flags]
    public enum EBranch
    {
        None = 0,
        Army = 1,
        Helicopters = 2,
        Aviation = 4,
        Fleet = 8,
    }
}