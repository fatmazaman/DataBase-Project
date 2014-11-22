using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using HumanMicroRNA.DataLayer.Connection;

namespace HumanMicroRNA.DataLayer.Base.Logs
{
    public class ErrorLogs
    {
        /// <summary>
        /// The following method is designed in order to write the exception captured
        /// by the application into a log file with the appropriate information.
        /// </summary>
        /// <param name="exceptionThrown">string exceptionThrown</param>
        /// <param name="methodExecuted">string methodExecuted</param>
        /// <param name="timeStamp">DateTime timeStamp</param>
        public static bool LogExceptionThrownIntoDatabase(string exceptionThrown, string methodExecuted,
                                                                            DateTime timeStamp, string userID)
        {
            bool recordInserted = true;
            try
            {
                using (SqlConnection conn = DbConnection.openConnection())
                using (SqlCommand cmd = new SqlCommand("hmrna_log_application_errors_sp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("exception_log_text", exceptionThrown);
                    cmd.Parameters.AddWithValue("exception_from_method_text", methodExecuted);
                    cmd.Parameters.AddWithValue("exception_dtm", timeStamp);
                    cmd.Parameters.AddWithValue("exception_by_name", userID);

                    cmd.CommandTimeout = 1000;

                    int executeNonQuery = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (executeNonQuery > 0)
                        recordInserted = true;
                }
            }
            catch (Exception ex) { return false; }
            return recordInserted;
        }
    }
}
