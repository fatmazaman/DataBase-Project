using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using HumanMicroRNA.DataLayer.Base;
using HumanMicroRNA.DataLayer.Connection;
using HumanMicroRNA.DataLayer.Base.Logs;
using iSharpToolkit.Exceptions;

namespace HumanMicroRNA.DataLayer.SNP
{
    /// <summary>
    /// The following class is designed in order to handle all of the
    /// database operations (Insert, update, Delete ...)
    /// </summary>
    public class DbSNP
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
        public static bool ImportSNP(DataTable miRNADt, string tableName, bool batchSize, bool columnMapping)
        {
            using (SqlBulkCopy bulkCopy =
                           new SqlBulkCopy(DbConnection.openConnection()))
            {
                bulkCopy.DestinationTableName = DbConstants.SchemaSNP + "." + tableName;
                try
                {
                    if (batchSize)
                        bulkCopy.BatchSize = 100000;
                    bulkCopy.BulkCopyTimeout = 1000;
                    bulkCopy.WriteToServer(miRNADt);
                    return true;
                }
                catch (Exception ex)
                {
                    ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                                    "ImportSNP", DateTime.Now, "UserName");
                    return false;
                }
            }
        }
    }
}
