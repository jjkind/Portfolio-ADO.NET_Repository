using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccessLayer.Extensions
{
    public static class IDataRecordExtensions
    {

        public static bool HasColumn(this IDataRecord record, string columnName)
        {
            for (int i = 0; i < record.FieldCount; i++)
            {
                if (record.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }

            return false;
        }

        public static T MapType<T>(this IDataRecord record) where T: class, new()
        {
            var objT = Activator.CreateInstance<T>();
            foreach (var property in typeof(T).GetProperties())
            {
                if (record.HasColumn(property.Name) && !record.IsDBNull(record.GetOrdinal(property.Name)))
                    property.SetValue(objT, record[property.Name]);
            }

            return objT;
        }
    }
}
