using Core.Enumerations;
using Core.Enumerations.Logger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions
{
    /// <summary> Methods extending a generics. </summary>
    public static class GenericExtensions
    {
        #region Methods: Fluency

        public static void AddInto<T>(this T item, IList<T> list) =>
            list.Add(item);

        /// <summary> Fluently executes an operation on the <paramref name="value"/> and returns the result. </summary>
        /// <typeparam name="T"> The input type. </typeparam>
        /// <typeparam name="U"> The output type. </typeparam>
        /// <param name="value"> The value to do the operation on. </param>
        /// <param name="function"> The function to execute over the <paramref name="value"/>. </param>
        /// <returns></returns>
        public static U Do<T, U>(this T value, Func<T, U> function) =>
            function(value);

        /// <summary> Fluently determines whether the given collection contains the object. </summary>
        /// <typeparam name="T"> The object type. </typeparam>
        /// <param name="source"> The source object. </param>
        /// <param name="collection"> The collection to check. </param>
        /// <returns></returns>
        public static bool IsIn<T>(this T source, IEnumerable<T> collection) =>
            collection.Contains(source);

        /// <summary> Fluently determines whether the given dictionary contains the object as a key. </summary>
        /// <typeparam name="T"> The object type. </typeparam>
        /// <typeparam name="U"> The dictionary value type. </typeparam>
        /// <param name="source"> The source object. </param>
        /// <param name="collection"> The dictionary to check. </param>
        /// <returns></returns>
        public static bool IsKeyIn<T, U>(this T source, IDictionary<T, U> collection) =>
            collection.ContainsKey(source);

        /// <summary> Fluently checks whether the value of the given enumeration item is positive. </summary>
        /// <typeparam name="T"> The enumeration type of the <paramref name="source"/>. </typeparam>
        /// <param name="source"> The enumeration item to check. </param>
        /// <returns></returns>
        public static bool EnumerationItemValueIsPositive<T>(this T source) where T : struct
        {
            var type = typeof(T);

            if (type.IsEnum)
            {
                var enumerationValueType = type.GetEnumUnderlyingType();

                if (enumerationValueType == typeof(int))
                    return source.CastTo<int>() > EInteger.Number.Zero;
                else
                    throw new NotImplementedException(ECoreLogMessage.ExplicitImplementationRequiredForType.FormatFluently(enumerationValueType.ToStringLikeCode()));
            }
            else
            {
                throw new ArgumentException(ECoreLogMessage.TypeIsNotEnumeration.FormatFluently(type.ToStringLikeCode()));
            }
        }

        #endregion Methods: Fluency
        #region Methods: Increment() / Decrement()

        /// <summary> Increments the value. </summary>
        /// <typeparam name="T"> The type of the value. </typeparam>
        /// <param name="value"> The value to increment. </param>
        /// <returns></returns>
        public static T Increment<T>(this T value) where T : struct
        {
            return value switch
            {
                int integer => (integer + 1).CastTo<T>(),
                _ => throw new NotImplementedException(ECoreLogMessage.ExplicitImplementationRequiredForType.FormatFluently(typeof(T).FullName)),
            };
        }

        /// <summary> Decrements the value. </summary>
        /// <typeparam name="T"> The type of the value. </typeparam>
        /// <param name="value"> The value to decrement. </param>
        /// <returns></returns>
        public static T Decrement<T>(this T value) where T : struct
        {
            return value switch
            {
                int integer => (integer - 1).CastTo<T>(),
                _ => throw new NotImplementedException(ECoreLogMessage.ExplicitImplementationRequiredForType.FormatFluently(typeof(T).FullName)),
            };
        }

        #endregion Methods: Increment() / Decrement()
    }
}