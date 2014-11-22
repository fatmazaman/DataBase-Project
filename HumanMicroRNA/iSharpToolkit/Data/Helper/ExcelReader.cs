using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Data.Common;

namespace iSharpToolkit.Data.Helper
{
    /// <summary>
    /// Makes Excel Operations easier to deal with.
    /// </summary>
    public class ExcelReader
    {
        /// <summary>
        /// Provider Type to read the excel file
        /// </summary>
        public enum ProviderType
        {
            /// <summary>
            /// Microsoft.ACE.OLEDB.12.0
            /// </summary>
            MicrosoftACEOLEDB12,
            /// <summary>
            /// Microsoft.Jet.OLEDB.4.0
            /// </summary>
            MicrosoftJetOLEDB4,
        }
        /// <summary>
        /// The following method is designed to return a database connection.
        /// </summary>
        /// <param name="sourceFile">string sourceFile</param>
        /// <param name="providerType">ProviderType providerType</param>
        /// <returns>Returns a database connection.</returns>
        private static string DbConnection(string sourceFile, ProviderType providerType)
        {
            switch (providerType)
            {
                default:
                case ProviderType.MicrosoftACEOLEDB12:
                    return @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sourceFile +
                            ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=2\'";
                case ProviderType.MicrosoftJetOLEDB4:
                    return @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sourceFile +
                            ";Extended Properties='Excel 8.0;HDR=YES;'";
            }

        }

        /// <summary>
        /// This method is designed to read data from the excel sheet and return them into
        /// a datatable. Default Provider is Microsoft.ACE.OLEDB.12.0.
        /// </summary>
        /// <param name="sourceFile">Source File</param>
        /// <param name="worksheetName">WorkSheet Name</param>
        /// <param name="fields">List of type strings for the Fields Name</param>
        /// <returns>DataTable Loaded with the values from Excel file.</returns>
        public static DataTable ReadDataFromExcel(string sourceFile, string worksheetName, IEnumerable<string> fields)
        {
            ProviderType prvdrType = ProviderType.MicrosoftACEOLEDB12;
            return ReadDataFromExcel(sourceFile, worksheetName, fields, prvdrType);
        }
        /// <summary>
        /// This method is designed to read data from the excel sheet and return them into
        /// a datatable.
        /// </summary>
        /// <param name="sourceFile">Source File</param>
        /// <param name="worksheetName">WorkSheet Name</param>
        /// <param name="fields">List of type strings for the Fields Name</param>
        /// <param name="prvdrType">Provider Type Selection to read the excel file.</param>
        /// <returns>DataTable Loaded with the values from Excel file.</returns>
        public static DataTable ReadDataFromExcel(string sourceFile, string worksheetName, IEnumerable<string> fields, ProviderType prvdrType)
        {
            if (String.IsNullOrEmpty(sourceFile) ||
                  !File.Exists(sourceFile) ||
                  String.IsNullOrEmpty(worksheetName))
            {
                return null;
            }
            try
            {
                DataTable ExcelDataTable = new DataTable();
                string ConnectionString = DbConnection(sourceFile, prvdrType);
                DbProviderFactory Factory = DbProviderFactories.GetFactory("System.Data.OleDb");
                using (DbConnection Connection = Factory.CreateConnection())
                {
                    Connection.ConnectionString = ConnectionString;
                    using (DbCommand command = Connection.CreateCommand())
                    {
                        string FieldsName = string.Empty;
                        if (fields == null || fields.FirstOrDefault() == null)
                            FieldsName = "*";
                        else
                        {
                            string lastItem = fields.ToArray()[fields.Count() - 1];
                            foreach (string item in fields)
                            {
                                if (item != lastItem)
                                    FieldsName = FieldsName + item + ", ";
                                else
                                    FieldsName = FieldsName + item;
                            }
                        }

                        command.CommandText = "SELECT " + FieldsName + " FROM [" + worksheetName + "$]";
                        Connection.Open();
                        using (DbDataReader rdr = command.ExecuteReader())
                            ExcelDataTable.Load(rdr);
                    }
                }
                return ExcelDataTable;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Problem reading the xls(x) file! Source File: {0}, WorkSheet: {1}", sourceFile, worksheetName), ex);
            }

        }

        /// <summary>
        /// This method is designed to read data from the excel sheet and return them into
        /// a datatable. Default Provider is Microsoft.ACE.OLEDB.12.0.
        /// </summary>
        /// <param name="sourceFile">Source File</param>
        /// <param name="worksheetName">WorkSheet Name</param>
        /// <param name="fields">List of type strings for the Fields Name</param>
        /// /// <param name="whereClause">Where clause in a string format.</param>
        /// <returns>DataTable Loaded with the values from Excel file.</returns>
        public static DataTable ReadDataFromExcelWithWhereClause(string sourceFile, string worksheetName, IEnumerable<string> fields, string whereClause)
        {
            ProviderType prvdrType = ProviderType.MicrosoftACEOLEDB12;
            return ReadDataFromExcelWithWhereClause(sourceFile, worksheetName, fields, whereClause, prvdrType);
        }
        /// <summary>
        /// This method is designed to read data from the excel sheet and return them into
        /// a datatable. This method allows you to pass a where clause in order to filter
        /// the data being queried.
        /// </summary>
        /// <param name="sourceFile">Source File</param>
        /// <param name="worksheetName">WorkSheet Name</param>
        /// <param name="fields">List of type strings for the Fields Name</param>
        /// <param name="whereClause">Where clause in a string format.</param>
        /// <param name="prvdrType">Provider Type Selection to read the excel file.</param>
        /// <returns>DataTable Loaded with the values from Excel file.</returns>
        private static DataTable ReadDataFromExcelWithWhereClause(string sourceFile, string worksheetName, IEnumerable<string> fields, string whereClause, ProviderType prvdrType)
        {
            if (String.IsNullOrEmpty(sourceFile) ||
                  !File.Exists(sourceFile) ||
                  String.IsNullOrEmpty(worksheetName))
            {
                return null;
            }
            try
            {
                DataTable ExcelDataTable = new DataTable();
                string ConnectionString = DbConnection(sourceFile, prvdrType);
                DbProviderFactory Factory = DbProviderFactories.GetFactory("System.Data.OleDb");
                using (DbConnection Connection = Factory.CreateConnection())
                {
                    Connection.ConnectionString = ConnectionString;
                    using (DbCommand command = Connection.CreateCommand())
                    {
                        string FieldsName = string.Empty;
                        if (fields == null || fields.FirstOrDefault() == null)
                            FieldsName = "*";
                        else
                        {
                            string lastItem = fields.ToArray()[fields.Count() - 1];
                            foreach (string item in fields)
                            {
                                if (item != lastItem)
                                    FieldsName = FieldsName + item + ", ";
                                else
                                    FieldsName = FieldsName + item;
                            }
                        }

                        if (!string.IsNullOrEmpty(whereClause))
                        {
                            if(whereClause.Trim().ToUpper().StartsWith("WHERE "))
                                command.CommandText = "SELECT " + FieldsName + " FROM [" + worksheetName + "$] " + whereClause.Trim();
                        }
                        else
                            command.CommandText = "SELECT " + FieldsName + " FROM [" + worksheetName + "$]";
                        Connection.Open();
                        using (DbDataReader rdr = command.ExecuteReader())
                            ExcelDataTable.Load(rdr);
                    }
                }
                return ExcelDataTable;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Problem reading the xls(x) file! Source File: {0}, WorkSheet: {1}", sourceFile, worksheetName), ex);
            }
        }    
    }
}
