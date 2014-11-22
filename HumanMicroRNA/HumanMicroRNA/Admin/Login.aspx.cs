using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HumanMicroRNA.BusinessLayer.Users;
using iSharpToolkit.Security.Hashing;
using System.Data;
using System.Web.Security;
using HumanMicroRNA.BusinessLayer.Base;
using iSharpToolkit.CommonHelper;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using iSharpToolkit.Net.Mail;

namespace HumanMicroRNA.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        #region Page Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">(object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies.Get(FormsAuthentication.FormsCookieName);

            if (cookie != null)
                Response.Redirect(BusConstants.PageNavigation.AdminMyPortalPage);
        }
        #endregion

        #region Events
        /// <summary>
        /// The following method is designed in order to validate
        /// the user information before loging in.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblErrorMessage.Text = string.Empty;
            string redirectToPage = string.Empty;
            try
            {
                redirectToPage = Request.QueryString["url"].Trim();
            }
            catch { redirectToPage = BusConstants.PageNavigation.AdminMyPortalPage.Replace("~/Admin/", string.Empty); }

            string userName = tbUserName.Text.Trim();
            string password = tbPassword.Text.Trim();

            bool usrValidation = ValidateUser(userName, password, redirectToPage);

            if (!usrValidation)
                lblErrorMessage.Text = "The username or password you entered is incorrect.";
        }
        /// <summary>
        /// The following event is designed in order to be executed when the
        /// submit button is clicked in the reset process.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void btnSubmitReset_Click(object sender, EventArgs e)
        {
            string emailAddress = tbEmailAddress.Text.Trim();
            bool resetSuccessful = PasswordResetProcess(emailAddress);

            bool isEmailalid = CommonHelper.IsValidEmail(emailAddress);

            if (isEmailalid)
            {
                DataTable dtUserInfo = BusUsers.GetUserInfoByEmailAddress(emailAddress);

                if (dtUserInfo.Rows.Count > 0)
                {
                    if (dtUserInfo.Rows[0]["user_email_address_text"] != null)
                    {
                        bool emailSent = false;
                        if (CommonHelper.IsValidEmail(dtUserInfo.Rows[0].Field<string>("user_email_address_text").Trim()))
                        {
                            string emailAddressTemp = dtUserInfo.Rows[0].Field<string>("user_email_address_text").Trim();
                            //TODO: Create password reset request number
                            string passwordRequestNumHash = string.Empty;
                            string passwordRequestNumSalt = string.Empty;

                            SaltedHash passwordReset = new SaltedHash();
                            passwordReset.GetHashAndSaltString(emailAddressTemp, out passwordRequestNumHash, out passwordRequestNumSalt);

                            DataTable passwordResetDt = new DataTable();
                            passwordResetDt.Columns.Add("password_reset_process_id", typeof(int));
                            passwordResetDt.Columns.Add("user_name", typeof(string));
                            passwordResetDt.Columns.Add("email_address_text", typeof(string));
                            passwordResetDt.Columns.Add("password_reset_request_dtm", typeof(DateTime));
                            passwordResetDt.Columns.Add("password_reset_request_by_user_id", typeof(string));
                            passwordResetDt.Columns.Add("password_reset_request_code", typeof(string));
                            passwordResetDt.Columns.Add("password_reset_request_complete_flag", typeof(bool));
                            passwordResetDt.Columns.Add("password_reset_request_complete_dtm", typeof(DateTime));

                            passwordResetDt.Rows.Add(0, dtUserInfo.Rows[0]["user_name"] != null ? dtUserInfo.Rows[0].Field<string>("user_name").Trim() : "N/A",
                                dtUserInfo.Rows[0]["user_email_address_text"] != null ? dtUserInfo.Rows[0].Field<string>("user_email_address_text").Trim() : "N/A",
                                DateTime.Now,
                                dtUserInfo.Rows[0]["user_name"] != null ? dtUserInfo.Rows[0].Field<string>("user_name").Trim() : "N/A",
                                !string.IsNullOrEmpty(passwordRequestNumHash) ? passwordRequestNumHash.Trim() : "N/A",
                                false, DateTime.Now);

                            bool isPasswordRequestSet = BusUsers.SetPasswordRequest(passwordResetDt);

                            if (isPasswordRequestSet)
                            {
                                string emailBodyMessage = BusConstants.GenerateBodyMessage(dtUserInfo.Rows[0].Field<string>("user_first_name").Trim(), BusConstants.HMiRNAsqvURL + passwordRequestNumHash);

                                //TODO: Generate email content (subject, body, ...) along with password reset request number appended to a URL
                                emailSent = iSharpMail.Send(dtUserInfo.Rows[0]["user_email_address_text"].ToString(), "noreply@mail.usf.edu", "How to reset your password", emailBodyMessage, true);

                                if (emailSent)
                                {
                                }
                                else
                                {
                                }
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// The following method is designed in order to initiate the
        /// password reset process.
        /// </summary>
        /// <param name="emailAddress">string emailAddress</param>
        /// <returns>Returns a boolean value indicating whether the reset was successful.</returns>
        private bool PasswordResetProcess(string emailAddress)
        {
            bool ProcessStatus = true;



            return ProcessStatus;
        }
        /// <summary>
        /// The following method is designed in order
        /// to validate the user credentials when
        /// logging in to the application.
        /// </summary>
        /// <param name="userName">string userName</param>
        /// <param name="password">string password</param>
        /// <returns>Returns True for successful insert; otherwise false.</returns>
        private bool ValidateUser(string userName, string password, string redirectLink)
        {
            DataTable userValidationInfo = new DataTable();
            bool loginValid = false;

            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                userValidationInfo = BusUsers.UserValidationInfo(userName);
                if (userValidationInfo.Rows.Count > 0)
                {
                    loginValid = new SaltedHash().VerifyHashString(password,
                                               userValidationInfo.Rows[0]["user_password_hash"].ToString(),
                                                   userValidationInfo.Rows[0]["user_password_salt"].ToString());
                    if (loginValid)
                    {
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                                                            userName,
                                                            DateTime.Now,
                                                            DateTime.Now.AddMinutes(30),
                                                            true,
                                                            "Admin",
                                                            FormsAuthentication.FormsCookiePath);

                        // Encrypt the ticket.
                        string encTicket = FormsAuthentication.Encrypt(ticket);

                        // Create the cookie.
                        Response.Cookies.Add(new HttpCookie("UserName", userName));
                        Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                        Response.Redirect("~/Admin/" + redirectLink);
                    }
                }
            }
            return loginValid;
        }
        #endregion
    }
}