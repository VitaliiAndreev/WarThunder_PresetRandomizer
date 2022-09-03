using Core;
using System;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;

namespace Client.Wpf
{
    public static partial class DataGridRowExtensions
    {
        private const BindingFlags fieldFlags = BindingFlags.NonPublic | BindingFlags.Instance;

        private static readonly string[] backingFieldFormats = { "<{0}>i__Field", "<{0}>" };

        public static void Localise(this DataGridRow row)
        {
            var rowItem = row.Item;
            var properties = rowItem.GetType().GetProperties();

            foreach (var property in properties)
            {
                var newValue = ApplicationHelpers.LocalisationManager.GetLocalisedString(
                    property.GetValue(rowItem).ToString(),
                    suppressWarnings: true);

                rowItem.Set(property, newValue);
            }
        }

        private static TObject Set<TObject, TProperty>(this TObject instance, MemberInfo property, TProperty newValue)
            where TObject : class
        {
            var backingFieldNames = backingFieldFormats
                .Select(format => format.Format(property.Name))
                .ToList();
            var backingField = typeof(TObject)
                .GetFields(fieldFlags)
                .FirstOrDefault(field => backingFieldNames.Contains(field.Name));

            if (backingField is null)
                throw new MissingMemberException($"Cannot find a backing field for {property.Name} in {instance.GetType().ToStringLikeCode()}");

            backingField.SetValue(instance, newValue);

            return instance;
        }
    }
}