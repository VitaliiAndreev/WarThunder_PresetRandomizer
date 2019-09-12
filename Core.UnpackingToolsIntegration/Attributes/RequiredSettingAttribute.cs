using System;

namespace Core.UnpackingToolsIntegration.Attributes
{
    /// <summary> Designates a property that is required when reading configuration files. </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredSettingAttribute : Attribute
    {
    }
}