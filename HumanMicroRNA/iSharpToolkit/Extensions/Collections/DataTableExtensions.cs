using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;

namespace iSharpToolkit.Extensions.Collections
{
    /// <summary>
    ///		Class Containing DataTable Extensions.
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        ///		Converts the DataTable Collection&lt;T&gt;
        /// </summary>
        /// <typeparam name="T">
        ///		The type of elements in the list.
        /// </typeparam>
        /// <typeparam name="Collection">
        ///		The type of collection to return.
        /// </typeparam>
        /// <param name="dt">
        ///		DataTable to convert to Collection&lt;T&gt;
        /// </param>
        /// <returns>
        ///		Returns a Collection&lt;T&gt; containing the data in the DataTable given.
        /// </returns>
        public static Collection ToCollection<T, Collection>(this DataTable dt)
            where Collection : List<T>, new()
            where T : new()
        {
            if (dt == null)
                return null;

            Collection collection = new Collection();

            FillCollection(collection, dt);

            return collection;
        }

        /// <summary>
        ///		Converts the DataTable to CSV string
        /// </summary>
        /// <param name="dt">
        ///		Data Table to convert to CSV
        /// </param>
        /// <returns>
        ///		Returns a CSV string containing the data in the given DataTable.
        /// </returns>
        public static string ToCSV(this DataTable dt)
        {
            if (dt == null)
                return String.Empty;

            return FillCSVData(dt, dt.Columns.Cast<DataColumn>().Select(col => col.ColumnName)).ToString();
        }

        /// <summary>
        ///		Converts the DataTable to CSV string
        /// </summary>
        /// <param name="dt">
        ///		Data Table to convert to CSV
        /// </param>
        /// <param name="columns">
        ///		Parent to add.
        /// </param>
        /// <returns>
        ///		Returns a CSV string containing the data in the given DataTable.
        /// </returns>
        public static string ToCSV(this DataTable dt, params string[] columns)
        {
            if (dt == null)
                return String.Empty;

            if (columns == null || columns.Length == 0)
                return ToCSV(dt);

            return FillCSVData(dt, columns).ToString();
        }

        #region Helper Functions
        internal static void FillCollection<T>(List<T> collection, DataTable dt) where T : new()
        {
            if (dt == null)
                return;

            collection.Capacity = collection.Count + dt.Rows.Count;
            List<PropertyInfo> properties = CreatePropertiesMapping<T>(dt);

            foreach (DataRow dr in dt.Rows)
                collection.Add(FillData<T>(properties, dr));
        }
        internal static List<PropertyInfo> CreatePropertiesMapping<T>(DataTable dt)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            List<PropertyInfo> mapping = new List<PropertyInfo>();

            Dictionary<string, PropertyInfo> tempMapping = new Dictionary<string, PropertyInfo>();
            foreach (var item in properties)
                tempMapping.Add(item.Name, item);

            foreach (DataColumn col in dt.Columns)
                if (tempMapping.ContainsKey(col.ColumnName) &&
                    tempMapping[col.ColumnName].PropertyType == col.DataType)
                {
                    mapping.Add(tempMapping[col.ColumnName]);
                }

            return mapping;
        }
        internal static T FillData<T>(List<PropertyInfo> properties, DataRow dr) where T : new()
        {
            T t = new T();

            foreach (PropertyInfo pi in properties)
                pi.SetValue(t, dr[pi.Name], null);

            return t;
        }


        private static StringBuilder FillCSVData(DataTable dt, IEnumerable<string> columns)
        {
            StringBuilder csv = new StringBuilder();

            if (dt == null || columns == null)
                return csv;

            FillCSVHeatherData(csv, columns);
            foreach (DataRow row in dt.Rows)
                FillCSVData(csv, columns, row);

            return csv;
        }
        private static void FillCSVHeatherData(StringBuilder csv, IEnumerable<string> columns)
        {
            bool PrependComma = false;
            foreach (string col in columns)
            {
                IEnumerableExtensions.AddPropertyToCSV(csv, col, PrependComma);
                PrependComma = true;
            }
            csv.Append(Environment.NewLine);
        }
        private static void FillCSVData(StringBuilder csv, IEnumerable<string> columns, DataRow row)
        {
            bool PrependComma = false;
            foreach (string col in columns)
            {
                IEnumerableExtensions.AddPropertyToCSV(csv, row[col].ToString(), PrependComma);
                PrependComma = true;
            }
            csv.Append(Environment.NewLine);
        }
        #endregion
    }
}
