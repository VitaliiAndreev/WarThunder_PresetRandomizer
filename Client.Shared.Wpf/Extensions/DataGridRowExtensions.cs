using System.Linq;
using System.Windows.Controls;

namespace Client.Shared.Wpf.Extensions
{
    public static class DataGridRowExtensions
    {
        #region Methods: Reading

        public static object GetFieldValue(this DataGridRow row, string fieldName)
        {
            return row
                .Item
                .GetType()
                .GetProperties()
                .FirstOrDefault(property => property.Name == fieldName)
                ?.GetValue(row.Item)
            ;
        }

        #endregion Methods: Reading
    }
}