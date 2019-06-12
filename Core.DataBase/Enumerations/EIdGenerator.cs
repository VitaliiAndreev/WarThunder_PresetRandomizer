using NHibernate.Mapping.Attributes;

namespace Core.DataBase.Enumerations
{
    /// <summary> Enumerates constant strings corresponding to ID Generator parameters for the <see cref="IdAttribute.Generator"/>. </summary>
    public class EIdGenerator
    {
        public const string HiLo = "hilo";
    }
}