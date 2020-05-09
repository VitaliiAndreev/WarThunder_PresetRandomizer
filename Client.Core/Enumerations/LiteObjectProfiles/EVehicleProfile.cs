using System;

namespace Client.Shared.LiteObjectProfiles
{
    [Flags]
    public enum EVehicleProfile
    {
        None = 0,
        Full = 1,
        Nation = 2,
        NationAndCountry = 4,
        Country = 8,
        NationAndBranch = 16,
        Branch = 32,
        BranchAndCountry = 64,
        BranchAndClass = 128,
        Class = 256,
        NationAndClass = 512,
        Tag = 1024,
        NationAndTag = 2048,
        Subclass = 4096,
        NationAndSubclass = 8192,
    }
}