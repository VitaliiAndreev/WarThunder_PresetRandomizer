namespace Core.DataBase.Tests.Enumerations
{
    /// <summary> Names of assemblies loaded for testing of schema creation and other fundamental components. </summary>
    public class EAssemblies
    {
        public const string AssemblyWithNoMapping = "Core.DataBase.Tests.Mapping.Empty";
        public const string AssemblyWithMappingBase = "Core.DataBase.Tests.Mapping.OneClass.Id";
        public const string AssemblyWithMappingAltered = "Core.DataBase.Tests.Mapping.OneClass.IdAndName";
    }
}
