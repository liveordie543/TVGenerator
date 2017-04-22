using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TVGenerator.Data
{
    public class DataRowHelper<T> where T : new()
    {
        public static void SetItemFromRow(T item, DataRow row)
        {
            Parallel.ForEach(row.Table.Columns.Cast<DataColumn>(), column =>
            {
                PropertyInfo propertyInfo = item.GetType().GetProperty(column.ColumnName);

                if (propertyInfo != null && row[column] != DBNull.Value)
                {
                    try
                    {
                        propertyInfo.SetValue(item, Convert.ChangeType(row[column], propertyInfo.PropertyType), null);
                    }
                    catch (Exception) { } //Ignore any exceptions. We'll handle special conversions on a case by case basis.
                }
            });
        }

        public static T CreateItemFromRow(DataRow row)
        {
            T item = new T();

            SetItemFromRow(item, row);

            return item;
        }

        public static List<T> CreateListFromTable(DataTable table)
        {
            ConcurrentBag<T> bag = new ConcurrentBag<T>();

            Parallel.ForEach(table.AsEnumerable(), row =>
            {
                bag.Add(CreateItemFromRow(row));
            });

            return bag.ToList();
        }

        public static IList<T> GetEntityListFromCmd(string commandName, SqlParameter[] parameters)
        {
            using (CustomDataAdapter adapter = new CustomDataAdapter(commandName, CommandType.StoredProcedure, parameters))
            {
                DataTable table = new DataTable();

                adapter.Fill(table);

                return CreateListFromTable(table);
            }
        }
    }
}
