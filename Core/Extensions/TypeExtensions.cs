using Core.Enumerations;
using Core.Enumerations.Logger;
using System;
using System.Collections.Generic;
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

        /// <summary> Creates an instance of the specified type. </summary>
        /// <param name="type"> A type to instantiate. </param>
        /// <param name="arguments"> Constructor arguments. </param>
        /// <returns></returns>
        public static object CreateInstance(this Type type, params object[] arguments) =>
            Activator.CreateInstance(type, arguments);

        /// <summary> Creates an instance of the specified type and casts it into the desired type. </summary>
        /// <typeparam name="T"> A generic type. </typeparam>
        /// <param name="type"> A type to instantiate. </param>
        /// <returns></returns>
        public static T CreateInstance<T>(this Type type) where T : class =>
            CreateInstance(type) as T;

        /// <summary> Creates an instance of the specified type and casts it into the desired type. </summary>
        /// <typeparam name="T"> A generic type. </typeparam>
        /// <param name="type"> A type to instantiate. </param>
        /// <param name="arguments"> Constructor arguments. </param>
        /// <returns></returns>
        public static T CreateInstance<T>(this Type type, params object[] arguments) where T : class =>
            CreateInstance(type, arguments) as T;

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

        #region Methods: For Enums

        public static bool ValidateAsEnum(this Type type, bool @throw = true)
        {
            if (!type.IsEnum)
                return new ArgumentException(ECoreLogMessage.TypeIsNotEnumeration.Format(type.ToStringLikeCode())).ThrowOrReturnFalse(@throw);

            return true;
        }

        public static bool ValidateAsEnum<T>(this Type type, bool @throw = true) where T : struct
        {
            try
            {
                var isEnum = type.ValidateAsEnum(@throw);

                if (isEnum)
                {
                    var enumerationValueType = type.GetEnumUnderlyingType();

                    if (enumerationValueType != typeof(T))
                        return new ArgumentException(ECoreLogMessage.EnumValueMustBe.Format(enumerationValueType.ToStringLikeCode())).ThrowOrReturnFalse(@throw);
                }

                return false;
            }
            catch (Exception exception)
            {
                return exception.ThrowOrReturnFalse(@throw);
            }
        }

        /// <summary> Fluently gets enumeration items of the given type (the generic type and the type parameters have to match), optionally skipping the "None" item. </summary>
        /// <typeparam name="T"> The enumeration type. </typeparam>
        /// <param name="type"> The enumeration type. </param>
        /// <param name="skipInvalidItems"> Whether to exclude invalid items. </param>
        /// <param name="skipInvalidItems"> Whether the "All" item is considered valid. </param>
        /// <returns></returns>
        public static IEnumerable<T> GetEnumerationItems<T>(this Type type, bool skipInvalidItems = false, bool allItemIsValid = false)
        {
            var genericType = typeof(T);

            if (!genericType.IsEnum)
                throw new ArgumentException(ECoreLogMessage.TypeIsNotEnumeration.Format(genericType.ToStringLikeCode()));

            if (!type.IsEnum)
                throw new ArgumentException(ECoreLogMessage.TypeIsNotEnumeration.Format(type.ToStringLikeCode()));

            if (genericType != type)
                throw new ArgumentException(ECoreLogMessage.GenericTypeParameterAndTypeParameterDontMatch.Format(genericType, type));

            var enumerationItems = type.GetEnumValues().Cast<T>();

            if (skipInvalidItems)
            {
                enumerationItems = enumerationItems.Where(enumerationItem => enumerationItem.ToString() != EWord.None);

                if (!allItemIsValid)
                    enumerationItems = enumerationItems.Where(enumerationItem => !enumerationItem.ToString().StartsWith(EWord.All));
            }

            return enumerationItems;
        }

        #endregion Methods: For Enums
    }
}