using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using HumanMicroRNA.DataLayer.Connection;
using System.Data;
using HumanMicroRNA.DataLayer.Base.Logs;
using iSharpToolkit.Exceptions;
using HumanMicroRNA.DataLayer.Base;

namespace HumanMicroRNA.DataLayer.Users
{
    /// <summary>
    /// The following class is designed in order to handle all of the
    /// database operations (Insert, update, Delete ...)
    /// </summary>
    public class DbUsers
    {
        /// <summary>
        /// The followind method is designed in order to validate the
        /// user credentials against the database and return a boolean
        /// value indicating the validation result.
        /// </summary>
        /// <param name="userName">string userName</param>
        /// <returns>Returns a datatable with user info.</returns>
        public static DataTable UserValidationInfo(string userName)
        {
            DataTable userValidationInfo = new DataTable();

            try
            {
                using (SqlConnection conn = DbConnection.openConnection())
                using (SqlCommand cmd = new SqlCommand("hmrna_validate_user_credentials_sp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", userName);

                    SqlDataReader rdr = cmd.ExecuteReader();
                    userValidationInfo.Load(rdr);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                  "UserValidationInfo", DateTime.Now, "UserName");
                return new DataTable();
            }

            return userValidationInfo;
        }
        /// <summary>
        /// The followind method is designed in order to return the
        /// user information based oon the username passed to it.
        /// </summary>
        /// <param name="userName">string userName</param>
        /// <returns>Returns a datatable with user info.</returns>
        public static DataTable GetUsersInfoByUserName(string userName)
        {
            DataTable userValidationInfo = new DataTable();

            try
            {
                using (SqlConnection conn = DbConnection.openConnection())
                using (SqlCommand cmd = new SqlCommand("hmrna_get_user_info_by_user_name_sp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", userName);

                    SqlDataReader rdr = cmd.ExecuteReader();
                    userValidationInfo.Load(rdr);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                  "GetUsersInfoByUserName", DateTime.Now, "UserName");
                return null;
            }

            return userValidationInfo;
        }
        /// <summary>
        /// The followind method is designed in order to return the
        /// user information based oon the email address passed to it.
        /// </summary>
        /// <param name="emailAddress">string emailAddress</param>
        /// <returns>Returns a datatable with user info.</returns>
        public static DataTable GetUserInfoByEmailAddress(string emailAddress)
        {
            DataTable userValidationInfo = new DataTable();

            try
            {
                using (SqlConnection conn = DbConnection.openConnection())
                using (SqlCommand cmd = new SqlCommand("hmrna_get_user_info_by_email_address_sp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email_address", emailAddress);

                    SqlDataReader rdr = cmd.ExecuteReader();
                    userValidationInfo.Load(rdr);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                  "GetUserInfoByEmailAddress", DateTime.Now, "UserName");
                return null;
            }

            return userValidationInfo;
        }
        /// <summary>
        /// The following method is designed in order to set the
        /// password code which is provided to the user for the 
        /// reset process validation.
        /// </summary>
        /// <param name="passwordRequestDt">DataTable passwordRequestDt</param>
        /// <returns>Returns a boolean value indicating true for successful insert; otherwise false.</returns>
        public static bool SetPasswordRequest(DataTable passwordRequestDt)
        {
            using (SqlBulkCopy bulkCopy =
                          new SqlBulkCopy(DbConnection.openConnection()))
            {
                bulkCopy.DestinationTableName = DbConstants.Schemdbo + "." + DbConstants.PasswordResetTable;
                try
                {
                    bulkCopy.BulkCopyTimeout = 1000;
                    bulkCopy.WriteToServer(passwordRequestDt);
                    return true;
                }
                catch (Exception ex)
                {
                    ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                                    "SetPasswordRequest", DateTime.Now, "UserName");
                    return false;
                }
            }
        }
        /// <summary>
        /// The following method is designed in order to insert a new user record
        /// on the database.
        /// </summary>
        /// <param name="firstName">string firstName</param>
        /// <param name="lastName">string lastName</param>
        /// <param name="emailAddress">string emailAddress</param>
        /// <param name="userName">string userName</param>
        /// <param name="hash">string hash</param>
        /// <param name="salt">string salt</param>
        /// <param name="statusVal">int statusVal</param>
        /// <param name="createdBy">string createdBy</param>
        /// <returns>Return a boolean value indication the success of the process.</returns>
        public static bool CreateNewUser(string firstName, string lastName, string emailAddress,
                                            string userName, string hash, string salt,
                                                int statusVal, string createdBy)
        {
            bool inserted = false;
            try
            {
                using (SqlConnection conn = DbConnection.openConnection())
                using (SqlCommand cmd = new SqlCommand("hmrna_create_user_sp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@user_first_name", firstName);
                    cmd.Parameters.AddWithValue("@user_last_name", lastName);
                    cmd.Parameters.AddWithValue("@user_email_address_text", emailAddress);
                    cmd.Parameters.AddWithValue("@user_name", userName);
                    cmd.Parameters.AddWithValue("@user_password_hash", hash);
                    cmd.Parameters.AddWithValue("@user_password_salt", salt);
                    cmd.Parameters.AddWithValue("@user_act_flag", statusVal);
                    cmd.Parameters.AddWithValue("@user_created_dtm", DateTime.Now);
                    cmd.Parameters.AddWithValue("@user_created_by_name", createdBy);

                    inserted = cmd.ExecuteNonQuery() > 0;
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                  "CreateNewUser", DateTime.Now, "UserName");
                return false;
            }
            return inserted;
        }
        /// <summary>
        /// The following method is designed in order to check
        /// whether the username passed to this function exists in the
        /// database.
        /// </summary>
        /// <param name="usernmae">string usernmae</param>
        /// <returns>Returns bool value indicating whether the username exists.</returns>
        public static bool CheckIfUserExists(string usernmae)
        {
            bool inserted = false;
            try
            {
                using (SqlConnection conn = DbConnection.openConnection())
                using (SqlCommand cmd = new SqlCommand("hmrna_check_if_user_exists_sp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", usernmae);

                    inserted = (int)cmd.ExecuteScalar() > 0;
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                  "CheckIfUserExists", DateTime.Now, "UserName");
                return false;
            }
            return inserted;
        }
        /// <summary>
        /// The following method is designed in order to check
        /// whether the email address passed to this function exists in the
        /// database.
        /// </summary>
        /// <param name="emailAddress">string emailAddress</param>
        /// <returns>Returns bool value indicating whether the username exists.</returns>
        public static bool CheckIfEmailAddressExists(string emailAddress)
        {
            bool inserted = false;
            try
            {
                using (SqlConnection conn = DbConnection.openConnection())
                using (SqlCommand cmd = new SqlCommand("hmrna_check_if_email_address_exists_sp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email_address", emailAddress);

                    inserted = (int)cmd.ExecuteScalar() > 0;
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                  "CheckIfEmailAddressExists", DateTime.Now, "UserName");
                return false;
            }
            return inserted;
        }
        /// <summary>
        /// The following method is designed in order to return all
        /// of the users in the database.
        /// </summary>
        /// <returns>Return a DataTable with all of the users in it.</returns>
        public static DataTable GetAllUsers()
        {
            DataTable userDt = new DataTable();

            try
            {
                using (SqlConnection conn = DbConnection.openConnection())
                using (SqlCommand cmd = new SqlCommand("hmrna_get_all_users_sp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader rdr = cmd.ExecuteReader();
                    userDt.Load(rdr);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLogs.LogExceptionThrownIntoDatabase(new XmlExceptions(ex, false).ToString(),
                                                  "GetAllUsers", DateTime.Now, "UserName");
                return new DataTable();
            }

            return userDt;
        }
    }
}
