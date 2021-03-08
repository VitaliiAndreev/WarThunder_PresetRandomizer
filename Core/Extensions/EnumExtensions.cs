using Core.Enumerations;
using Core.Enumerations.Logger;
using System;

namespace Core.Extensions
{
    public static class EnumExtensions
    {
        /// <summary> Fluently checks whether the value of the given enumeration item is positive. </summary>
        /// <typeparam name="T"> The enumeration type of the <paramref name="source"/>. </typeparam>
        /// <param name="source"> The enumeration item to check. </param>
        /// <returns></returns>
        public static bool EnumerationItemValueIsPositive<T>(this T source) where T : struct
        {
            var type = typeof(T);

            type.ValidateAsEnum();

            var enumerationValueType = type.GetEnumUnderlyingType();

            if (enumerationValueType == typeof(int))
                return source.CastTo<int>() > EInteger.Number.Zero;
            else
                throw new NotImplementedException(ECoreLogMessage.ExplicitImplementationRequiredForType.Format(enumerationValueType.ToStringLikeCode()));
        }

        /// <summary>
        /// Upcasts an enum value of <typeparamref name="TIn"/> type to a value of <typeparamref name="TOut"/> type.
        /// For it to work underlying values must be integers and <typeparamref name="TIn"/> values must be coded with an extra integer at the end comparing to values of <typeparamref name="TOut"/>.
        /// </summary>
        /// <typeparam name="TIn"> The type whose value to upcast. </typeparam>
        /// <typeparam name="TOut"> The type to upcast to. </typeparam>
        /// <param name="enumValue"> The value to upcast. </param>
        /// <returns></returns>
        public static TOut Upcast<TIn, TOut>(this TIn enumValue)
            where TIn : struct
            where TOut : struct
        {
            var inType = typeof(TIn);
            var outType = typeof(TOut);

            inType.ValidateAsEnum<int>();
            outType.ValidateAsEnum<int>();

            try
            {
                var incomingUnderlyingValue = enumValue.CastTo<int>();
                var outcomingUnderlyingValue = incomingUnderlyingValue < 0 ? incomingUnderlyingValue : incomingUnderlyingValue / 10;

                return outcomingUnderlyingValue.CastTo<TOut>();
            }
            catch (Exception exception)
            {
                throw new ArgumentException(ECoreLogMessage.EnumValueCouldntBeUpcastTo.Format(enumValue, outType), exception);
            }
        }

        /// <summary>
        /// Downcasts an enum value of <typeparamref name="TIn"/> type to a value of <typeparamref name="TOut"/> type.
        /// For it to work underlying values must be integers and <typeparamref name="TIn"/> values must be coded with one less integer at the end comparing to values of <typeparamref name="TOut"/>.
        /// </summary>
        /// <typeparam name="TIn"> The type whose value to downcast. </typeparam>
        /// <typeparam name="TOut"> The type to downcast to. </typeparam>
        /// <param name="enumValue"> The value to downcast. </param>
        /// <returns></returns>
        public static TOut Downcast<TIn, TOut>(this TIn enumValue)
            where TIn : struct
            where TOut : struct
        {
            var inType = typeof(TIn);
            var outType = typeof(TOut);

            inType.ValidateAsEnum<int>();
            outType.ValidateAsEnum<int>();

            try
            {
                var outcomingEnumUnderlyingValue = enumValue.CastTo<int>() * 10;

                return outcomingEnumUnderlyingValue.CastTo<TOut>();
            }
            catch (Exception exception)
            {
                throw new ArgumentException(ECoreLogMessage.EnumValueCouldntBeUpcastTo.Format(enumValue, outType), exception);
            }
        }
    }
}