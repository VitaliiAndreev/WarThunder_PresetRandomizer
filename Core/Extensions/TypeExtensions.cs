using Core.Enumerations;
using System;
using System.Linq;

namespace Core.Extensions
{
    /// <summary> Methods extending the <see cref="Type"/> class. </summary>
    public static class TypeExtensions
    {
        #region Methods: Instantiation

        /// <summary> Creates an instance of the specified type. </summary>
        /// <param name="type"> A type to instantiate. </param>
        /// <returns></returns>
        public static object CreateInstance(this Type type) =>
            Activator.CreateInstance(type);

        /// <summary> Creates an instance of the specified type and casts it into the desired type. </summary>
        /// <typeparam name="T"> A generic type. </typeparam>
        /// <param name="type"> A type to instantiate. </param>
        /// <returns></returns>
        public static T CreateInstance<T>(this Type type) where T : class =>
            CreateInstance(type) as T;

        #endregion Methods: Instantiation

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
