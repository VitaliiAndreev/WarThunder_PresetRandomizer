using Core.Enumerations;
using System;
using System.Linq;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="Type"/> class. </summary>
    public static class TypeExtensions
    {
        /// <summary> Creates a string representation of a type that closely resembles how it is written in C# code. However, instead of C#, .Net types are being used. </summary>
        /// <param name="type"> A type to process. </param>
        /// <returns></returns>
        public static string ToStringLikeCode(this Type type)
        {
            var typeName = type.Name;

            if (typeName.Contains(ECharacter.Grave))
                return $"{typeName.Split(ECharacter.Grave).First()}<{type.GetGenericArguments().Select(subType => subType.ToStringLikeCode()).StringJoin(", ")}>";

            return typeName;
        }
    }
}
