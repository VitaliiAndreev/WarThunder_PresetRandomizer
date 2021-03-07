using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Extensions
{
    public static class AnonymousObjectExtensions
    {
        private const BindingFlags _fieldFlags = BindingFlags.NonPublic | BindingFlags.Instance;

        private static readonly string[] _backingFieldFormats = { "<{0}>i__Field", "<{0}>" };

        public static TObject Set<TObject, TProperty>(this TObject instance, MemberInfo property, TProperty newValue) where TObject : class
        {
            var backingFieldNames = _backingFieldFormats
                .Select(format => format.Format(property.Name))
                .ToList()
            ;
            var backingField = typeof(TObject)
                .GetFields(_fieldFlags)
                .FirstOrDefault(field => backingFieldNames.Contains(field.Name))
            ;

            if (backingField is null)
                throw new NotSupportedException($"Cannot find a backing field for {property.Name}");

            backingField.SetValue(instance, newValue);

            return instance;
        }

        public static TObject Set<TObject, TProperty>(this TObject instance, Expression<Func<TObject, TProperty>> propertyExpression, TProperty newValue) where TObject : class
        {
            if (!(propertyExpression.Body is MemberExpression memberExpression))
                throw new ArgumentException($"The body of \"{nameof(propertyExpression)}\" is not a \"{nameof(MemberExpression)}\".");

            var property = memberExpression.Member;

            return instance.Set(property, newValue);
        }
    }
}