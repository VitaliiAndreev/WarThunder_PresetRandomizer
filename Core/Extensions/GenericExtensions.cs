using Core.Enumerations.Logger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions
{
    /// <summary> Methods extending a generics. </summary>
    public static class GenericExtensions
    {
        #region Fluency

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

        #endregion Fluency

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
    }
}