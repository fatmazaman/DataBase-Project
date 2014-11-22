using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HumanMicroRNA.DataLayer.Connection;
using HumanMicroRNA.DataLayer.Base;
using HumanMicroRNA.DataLayer.Base.Logs;
using iSharpToolkit.Exceptions;

namespace HumanMicroRNA.DataLayer.Var
{
    /// <summary>
    /// The following class is designed in order to handle all of the
    /// database operations (Insert, update, Delete ...)
    /// </summary>
    public class DbVar
    {
        /// <summary>
        /// The following method is designed in order to import the data from the var file
        /// downloaded from the ftp server to the db_var table in the HumanMircoRNA database.
        /// </summary>
        /// <param name="filePath">DataTable varDt</param>
        /// <param name="tableName">string tableName</param>
        /// <param name="batchSize">bool batchSize</param>
        /// <param name="columnMapping">bool columnMapping</param>
        /// <returns>Returns true for successful bulk copy; otherwise false.</returns>
        public static bool ImportVar(DataTable varDt, string tableName, bool batchSize, bool columnMapping)
        {
            using (SqlBulkCopy bulkCopy =
                           new SqlBulkCopy(DbConnection.openConnection()))
            {
                bulkCopy.DestinationTableName = DbConstants.SchemaVar + "." + tableName;
                try
                {
                    if (batchSize)
                        bulkCopy.BatchSize = 100000;
                    bulkCopy.BulkCopyTimeout = 1000;
                    bulkCopy.WriteToServer(varDt);
                    return true;
                }
                catch (Exception ex)
                {
                    ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                               "ImportVar", DateTime.Now, "UserName");
                    return false;
                }
            }
        }
        /// <summary>
        /// The following method is designed in order to delete all of
        /// the dbVar records from the table db_var in the database.
        /// </summary>
        /// <returns>Return the number of affected records.</returns>
        public static int DeleteAllVarFromTable()
        {
            int recordsAffected = 0;

            try
            {
                using (SqlConnection conn = DbConnection.openConnection())
                using (SqlCommand cmd = new SqlCommand("hmrna_delete_all_db_var_sp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 1000;

                    recordsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

                ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                           "DeleteAllVarFromTable", DateTime.Now, "UserName");
                return 0;
            }

            return recordsAffected;
        }
        /// <summary>
        /// The following method is designed in order to
        /// return all of the var records.
        /// </summary>
        /// <returns>Return all of the Var records in a datatable.</returns>
        public static DataTable GetAllVar()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = DbConnection.openConnection())
                using (SqlCommand cmd = new SqlCommand("hmrna_get_all_db_var_sp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 1000;

                    SqlDataReader rdr = cmd.ExecuteReader();

                    dt.Load(rdr);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

                ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                           "DeleteAllVarFromTable", DateTime.Now, "UserName");
                return null;
            }

            return dt;
        }

        /// <summary>
        /// The following method is designed in order to
        /// return all of the var records.
        /// </summary>
        /// <returns>Return all of the Var records in a datatable.</returns>
        public static DataTable GetVarByChromosome(string chromosome)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = DbConnection.openConnection())
                using (SqlCommand cmd = new SqlCommand("hmrna_get_all_db_var_by_chromosome_sp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@xsome_num", chromosome);
                    cmd.CommandTimeout = 1000;

                    SqlDataReader rdr = cmd.ExecuteReader();

                    dt.Load(rdr);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

                ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                           "DeleteAllVarFromTable", DateTime.Now, "UserName");
                return null;
            }

            return dt;
        }
        /// <summary>
        /// The following method is designed in order to return
        /// all of the Variants associated with a unique MiRNA based
        /// on the Human MicroRNA ID passed to it.
        /// </summary>
        /// <param name="hMiRNAID">int hMiRNAID</param>
        /// <returns>DataTable of all the Variants records.</returns>
        public static DataTable GetAllVarsByMiRNAID(int hMiRNAID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = DbConnection.openConnection())
                using (SqlCommand cmd = new SqlCommand("hmrna_get_all_var_by_human_mirna_id_sp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@human_micro_rna_id", hMiRNAID);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    dt.Load(rdr);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {

                ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                           "GetAllVarsByMiRNAID", DateTime.Now, "UserName");
                return null;
            }

            return dt;
        }
    }
}
