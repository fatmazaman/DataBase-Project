using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HumanMicroRNA.DataLayer.Users;
using System.Data;

namespace HumanMicroRNA.BusinessLayer.Users
{
    // <summary>
    /// The following class is designed in order to handle all
    /// of the business layer operations.
    /// </summary>
    public class BusUsers
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
            return DbUsers.UserValidationInfo(userName);
        }
        /// <summary>
        /// The followind method is designed in order to return the
        /// user information based oon the username passed to it.
        /// </summary>
        /// <param name="userName">string userName</param>
        /// <returns>Returns a datatable with user info.</returns>
        public static DataTable GetUsersInfoByUserName(string userName)
        {
            return DbUsers.GetUsersInfoByUserName(userName);
        }
        /// <summary>
        /// The followind method is designed in order to return the
        /// user information based oon the email address passed to it.
        /// </summary>
        /// <param name="emailAddress">string emailAddress</param>
        /// <returns>Returns a datatable with user info.</returns>
        public static DataTable GetUserInfoByEmailAddress(string emailAddress)
        {
            return DbUsers.GetUserInfoByEmailAddress(emailAddress);
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
            return DbUsers.SetPasswordRequest(passwordRequestDt);
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
            return DbUsers.CreateNewUser(firstName, lastName, emailAddress, userName, 
                                                        hash, salt, statusVal, createdBy);
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
            return DbUsers.CheckIfEmailAddressExists(emailAddress);
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
            return DbUsers.CheckIfUserExists(usernmae);
        }
         /// <summary>
        /// The following method is designed in order to return all
        /// of the users in the database.
        /// </summary>
        /// <returns>Return a DataTable with all of the users in it.</returns>
        public static DataTable GetAllUsers()
        {
            return DbUsers.GetAllUsers();
        }
    }
}
