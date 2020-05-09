using Client.Shared.LiteObjectProfiles;
using System;

namespace Client.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ShowVehiclePropertyAttribute : Attribute
    {
        public EVehicleProfile ProhibitedProfiles { get; }

        public ShowVehiclePropertyAttribute(EVehicleProfile prohibitedProfile)
        {
            ProhibitedProfiles = prohibitedProfile;
        }
    }
}