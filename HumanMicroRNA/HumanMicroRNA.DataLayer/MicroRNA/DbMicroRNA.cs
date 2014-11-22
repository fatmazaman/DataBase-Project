using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HumanMicroRNA.DataLayer.Connection;
using HumanMicroRNA.DataLayer.Base;
using System.Configuration;
using System.Xml;
using iSharpToolkit.Exceptions;
using HumanMicroRNA.DataLayer.Base.Logs;

namespace HumanMicroRNA.DataLayer.MicroRNA
{
    public class DbMicroRNA
    {
        /// <summary>
        /// The following method is designed in order to import the data from the mirna file
        /// downloaded from the ftp server to the mirna table in the HumanMircoRNA database.
        /// </summary>
        /// <param name="filePath">DataTable miRNADt</param>
        /// <param name="tableName">string tableName</param>
        /// <param name="batchSize">bool batchSize</param>
        /// <param name="columnMapping">bool columnMapping</param>
        /// <returns>Returns true for successful bulk copy; otherwise false.</returns>
        public static bool ImportMiRNA(DataTable miRNADt, string tableName, bool batchSize, bool columnMapping)
        {
            using (SqlBulkCopy bulkCopy =
                           new SqlBulkCopy(DbConnection.openConnection()))
            {
                bulkCopy.DestinationTableName = DbConstants.SchemaRNA + "." + tableName;
                try
                {
                    if (batchSize)
                        bulkCopy.BatchSize = 1000;
                    bulkCopy.BulkCopyTimeout = 1000;
                    bulkCopy.WriteToServer(miRNADt);
                    return true;
                }
                catch (Exception ex)
                {
                    ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                                    "ImportMiRNA", DateTime.Now, "UserName");
                    return false;
                }
            }
        }
        /// <summary>
        /// The following method is designed in order to import the data from the mirna file
        /// downloaded from the ftp server to the mirna table in the HumanMircoRNA database.
        /// </summary>
        /// <param name="filePath">DataTable miRNADt</param>
        /// <param name="tableName">string tableName</param>
        /// <returns>Returns true for successful bulk copy; otherwise false.</returns>
        public static bool ImportMiRNAWithColumnMapping(DataTable miRNADt, string tableName, bool batchSize)
        {
            using (SqlBulkCopy bulkCopy =
                           new SqlBulkCopy(DbConnection.openConnection()))
            {
                bulkCopy.DestinationTableName = DbConstants.SchemaRNA + "." + tableName;
                try
                {
                    DataColumnCollection columns = miRNADt.Columns;

                    if (columns.Count > 0)
                    {
                        foreach (DataColumn item in columns)
                        {
                            bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping()
                            {
                                SourceColumn = item.ColumnName,
                                SourceOrdinal = item.Ordinal,
                                DestinationColumn = item.ColumnName,
                                DestinationOrdinal = item.Ordinal
                            });
                        }
                    }

                    if (batchSize)
                        bulkCopy.BatchSize = 1000;

                    bulkCopy.BulkCopyTimeout = 1000;
                    bulkCopy.WriteToServer(miRNADt);
                    return true;
                }
                catch (Exception ex)
                {
                    ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                                    "ImportMiRNA", DateTime.Now, "UserName");
                    return false;
                }
            }
        }
        /// <summary>
        /// The following method is designed in order to return the MiRNA records from
        /// the rna.mirna table from the database based on the parameters passed to it.
        /// </summary>
        /// <param name="autoMiRNA">bool autoMiRNA</param>
        /// <param name="miRNAAcc">bool miRNAAcc</param>
        /// <returns>DataTable containing either the MiRNA auto_mirna, mirna_acc or both fields.</returns>
        public static DataTable GetAllMiRNAByAutoMiRNAorMiRNAAcc(bool autoMiRNA, bool miRNAAcc)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DbConnection.openConnection())
                using (SqlCommand cmd = new SqlCommand("hmrna_get_mirna_all_by_auto_mirna_or_mirna_acc", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@auto_mirna", autoMiRNA);
                    cmd.Parameters.AddWithValue("@mirna_acc", miRNAAcc);

                    cmd.CommandTimeout = 1000;

                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr != null)
                        dt.Load(rdr);

                    conn.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                  "GetAllMiRNAByAutoMiRNAorMiRNAAcc", DateTime.Now, "UserName");
                return null;
            }
        }
        /// <summary>
        /// The following method is designed in order to detele
        /// the records from the designated tables.
        /// </summary>
        /// <param name="tableName">string tableName</param>
        /// <returns>Returns the number of affected records.</returns>
        public static int DeleteMiRNARecords(string tableName)
        {
            int RecordsAffected = 0;
            try
            {
                using (SqlConnection conn = DbConnection.openConnection())
                using (SqlCommand cmd = new SqlCommand("hmrna_delete_records_from_table_sp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tbl_name", tableName);

                    RecordsAffected = cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                  "DeleteMiRNARecords", DateTime.Now, "UserName");
                return -1;
            }

            return RecordsAffected;
        }
        /// <summary>
        /// The following method is designed in order to return the MiRNA
        /// value into a datatable.
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetAllMiRNARecords()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DbConnection.openConnection())
                using (SqlCommand cmd = new SqlCommand("hmrna_get_all_mirna_records_sp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 1000;

                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr != null)
                        dt.Load(rdr);

                    conn.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                  "GetAllMiRNARecords", DateTime.Now, "UserName");
                return null;
            }
        }
        /// <summary>
        /// The following method is designed in order to bulk import
        /// the MiRNA-Var into the hmrna_var_assn table.
        /// </summary>
        /// <param name="miRNAVarDt">DataTable miRNAVarDt</param>
        /// <returns>Boolean value indicating if the import was successful.</returns>
        public static bool ImportMiRNAVar(DataTable miRNAVarDt)
        {
            using (SqlBulkCopy bulkCopy =
                               new SqlBulkCopy(DbConnection.openConnection()))
            {
                bulkCopy.DestinationTableName = DbConstants.Schemdbo + "." + DbConstants.hmrnaVarAssnTable;
                try
                {
                    bulkCopy.BulkCopyTimeout = 1000;
                    bulkCopy.WriteToServer(miRNAVarDt);
                    return true;
                }
                catch (Exception ex)
                {
                    ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                                    "ImportMiRNAVar", DateTime.Now, "UserName");
                    return false;
                }
            }
        }
        /// <summary>
        /// The following method is designed in order to get the
        /// MiRNA records from the database based on the search
        /// criterias.
        /// </summary>
        /// <param name="miRNAID">string miRNAID</param>
        /// <param name="accession">string accession</param>
        /// <param name="chromosome">string chromosome</param>
        /// <param name="variant">string variant</param>
        /// <returns>DataTable of MiRNA based on the search criteria</returns>
        public static DataTable GetMiRNABySearchCriteria(string miRNAID, string accession, string chromosome, string variant)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DbConnection.openConnection())
                using (SqlCommand cmd = new SqlCommand("hmrna_get_mirna_report_by_search_criteria_sp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (!string.IsNullOrEmpty(miRNAID))
                        cmd.Parameters.AddWithValue("@mirna_id", miRNAID);
                    else
                        cmd.Parameters.AddWithValue("@mirna_id", null);
                    if (!string.IsNullOrEmpty(accession))
                        cmd.Parameters.AddWithValue("@mirna_acc", accession);
                    else
                        cmd.Parameters.AddWithValue("@mirna_acc", null);
                    if (!string.IsNullOrEmpty(chromosome))
                        cmd.Parameters.AddWithValue("@xsome", chromosome);
                    else
                        cmd.Parameters.AddWithValue("@xsome", null);
                    if (!string.IsNullOrEmpty(variant))
                        cmd.Parameters.AddWithValue("@var_id", variant);
                    else
                        cmd.Parameters.AddWithValue("@var_id", null);

                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr != null)
                        dt.Load(rdr);

                    conn.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                  "GetMiRNABySearchCriteria", DateTime.Now, "UserName");
                return null;
            }
        }
        /// <summary>
        /// The following method is designed in order to return
        /// the record of a unique miRNA based on the id passed to
        /// it.
        /// </summary>
        /// <param name="miRNAID">string miRNAID</param>
        /// <returns>DataTable of miRNA record.</returns>
        public static DataTable GetUniqueMiRNAByMiRNAID(string miRNAID)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DbConnection.openConnection())
                using (SqlCommand cmd = new SqlCommand("hmrna_get_unique_mirna_by_mirna_id_sp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mirna_id", miRNAID);

                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr != null)
                        dt.Load(rdr);

                    conn.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                  "GetUniqueMiRNAByMiRNAID", DateTime.Now, "UserName");
                return null;
            }
        }
        /// <summary>
        /// The following method is designed in order
        /// to query the database for MiRNA Accession based 
        /// on the string passed to it.
        /// </summary>
        /// <param name="Accession">List of MiRNA Accession</param>
        public static List<string> GetMiRNAAccession(string Accession)
        {
            List<string> list = new List<string>();
            try
            {
                using (SqlConnection conn = DbConnection.openConnection())
                using (SqlCommand cmd = new SqlCommand("hmrna_get_mirna_accession_by_param_sp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mirna_accession", Accession);

                    cmd.CommandTimeout = 1000;

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                        list.Add(rdr["mirna_acc_text"].ToString());

                    conn.Close();
                }
                return list;
            }
            catch (Exception ex)
            {
                ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                  "GetMiRNAAccession", DateTime.Now, "UserName");
                return null;
            }
        }
        /// <summary>
        /// The following method is designed in order to
        /// return a list of MiRNA IDs.
        /// </summary>
        /// <param name="varID">string miRNAID</param>
        /// <returns>List of MiRNA ID</returns>
        public static List<string> GetMiRNAID(string miRNAID)
        {
            List<string> list = new List<string>();
            try
            {
                using (SqlConnection conn = DbConnection.openConnection())
                using (SqlCommand cmd = new SqlCommand("hmrna_get_mirna_id_by_param_sp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mirna_id", miRNAID);

                    cmd.CommandTimeout = 1000;

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                        list.Add(rdr["mirna_id"].ToString());

                    conn.Close();
                }
                return list;
            }
            catch (Exception ex)
            {
                ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                  "GetMiRNAID", DateTime.Now, "UserName");
                return null;
            }
        }
        /// <summary>
        /// The following method is designed in order to
        /// return a list of Var IDs.
        /// </summary>
        /// <param name="varID">string varID</param>
        /// <returns>List of Var ID</returns>
        public static List<string> GeVarID(string varID)
        {
            List<string> list = new List<string>();
            try
            {
                using (SqlConnection conn = DbConnection.openConnection())
                using (SqlCommand cmd = new SqlCommand("hmrna_get_var_id_by_param_sp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@var_id", varID);

                    cmd.CommandTimeout = 1000;

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                        list.Add(rdr["var_id"].ToString());

                    conn.Close();
                }
                return list;
            }
            catch (Exception ex)
            {
                ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                  "GeVarID", DateTime.Now, "UserName");
                return null;
            }
        }
        /// <summary>
        /// The following method is designed in order to import gene
        /// information into the mir_family_info table.
        /// </summary>
        /// <param name="geneDt">DataTable geneDt</param>
        /// <param name="p">bool batchImport</param>
        /// <returns>Return a boolean value indication the status of the import.</returns>
        public static bool ImportGeneFamilyInfo(DataTable geneDt, bool batchImport)
        {
            using (SqlBulkCopy bulkCopy =
                           new SqlBulkCopy(DbConnection.openConnection()))
            {
                bulkCopy.DestinationTableName = DbConstants.SchemaRNA + ".mir_family_info";
                try
                {
                    if (batchImport)
                        bulkCopy.BatchSize = 1000;
                    bulkCopy.BulkCopyTimeout = 1000;
                    bulkCopy.WriteToServer(geneDt);
                    return true;
                }
                catch (Exception ex)
                {
                    ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                                    "ImportGeneFamilyInfo", DateTime.Now, "UserName");
                    return false;
                }
            }
        }
        /// <summary>
        /// The following method is designed in order to return all of the
        /// gene target information.
        /// </summary>
        /// <returns>DataTable holding all of the Gene Target Information.</returns>
        public static DataTable GetAllmiRFamilyInfo()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DbConnection.openConnection())
                using (SqlCommand cmd = new SqlCommand("hmrna_get_all_mir_family_info_sp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 1000;

                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr != null)
                        dt.Load(rdr);

                    conn.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                  "GetAllmiRFamilyInfo", DateTime.Now, "UserName");
                return null;
            }
        }
        /// <summary>
        /// The following method is designed in order to import gene
        /// information into the mir_family_info table.
        /// </summary>
        /// <param name="dtGene">DataTable geneDt</param>
        /// <param name="batchImport">bool batchImport</param>
        /// <returns>Return a boolean value indication the status of the import.</returns>
        public static bool ImportPredictedTargetInfo(DataTable dtGene, bool batchImport)
        {
            using (SqlBulkCopy bulkCopy =
                           new SqlBulkCopy(DbConnection.openConnection()))
            {
                bulkCopy.DestinationTableName = DbConstants.SchemaRNA + ".predicted_target";
                try
                {
                    if (batchImport)
                        bulkCopy.BatchSize = 1000;
                    bulkCopy.BulkCopyTimeout = 1000;
                    bulkCopy.WriteToServer(dtGene);
                    return true;
                }
                catch (Exception ex)
                {
                    ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                                    "ImportPredictedTargetInfo", DateTime.Now, "UserName");
                    return false;
                }
            }
        }
        /// <summary>
        /// Get all of the Gene info associated with a specific mirna id.
        /// </summary>
        /// <param name="miRNAID">string miRNAID</param>
        /// <returns>Returns the gene information about a specific mirna id.</returns>
        public static DataTable GetAllGeneInfoByMiRNAID(string miRNAID)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = DbConnection.openConnection())
                using (SqlCommand cmd = new SqlCommand("hmrna_get_all_gene_by_mir_family_id_sp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mirna_id", miRNAID);

                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr != null)
                        dt.Load(rdr);

                    conn.Close();
                }
                return dt;
            }
            catch (Exception ex)
            {
                ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                  "GetMiRNABySearchCriteria", DateTime.Now, "UserName");
                return null;
            }
        }
    }
}
