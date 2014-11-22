using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iSharpToolkit.Extensions;
using System.Drawing;
using HumanMicroRNA.BusinessLayer.Base;
using System.Web.Security;
using iSharpToolkit.Security.Hashing;
using HumanMicroRNA.BusinessLayer.Users;

namespace HumanMicroRNA.Admin
{
    public partial class AddUser : System.Web.UI.Page
    {
        #region Private Properties
        private List<string> m_errorMessagers;
        #endregion
        #region Page Events
        /// <summary>
        /// This event is designed to be executed at page load.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies.Get(FormsAuthentication.FormsCookieName);

            if (cookie == null)
                Response.Redirect(BusConstants.PageNavigation.AdminLoginPage);
        }
        #endregion

        #region Events
        /// <summary>
        /// The following method is designed in order to add the new
        /// user into the database.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            string firstName = tbFirstName.Text.Trim();
            string lastName = tbLastName.Text.Trim();
            string emailAddress = tbEmailAddress.Text.Trim();
            string userName = tbUsername.Text.Trim();
            string password = tbPassword.Text.Trim();
            string repassword = tbReEnterPassword.Text.Trim();
            int statusVal = rdblStatus.SelectedIndex;

            bool isValid = validationInput(firstName, lastName, emailAddress, userName,
                                                            password, repassword, statusVal);

            if (isValid)
            {
                string hashStrn = string.Empty;
                string saltStrn = string.Empty;

                SaltedHash sh = new SaltedHash();
                sh.GetHashAndSaltString(password.Trim(), out hashStrn, out saltStrn);

                bool usernameExists = BusUsers.CheckIfUserExists(userName);
                bool emailAddrExists = BusUsers.CheckIfEmailAddressExists(emailAddress);

                if (usernameExists || emailAddrExists)
                {
                    if (usernameExists && emailAddrExists)
                        RenderMessage(Color.Red, "Username and email address exist. Please choose differen username and email address.");
                    else if (usernameExists)
                        RenderMessage(Color.Red, "Username is already in use!");
                    else if (emailAddrExists)
                        RenderMessage(Color.Red, "Email address is already in use!");
                }
                else
                {
                    bool userInserted = BusUsers.CreateNewUser(firstName, lastName, emailAddress, userName, hashStrn,
                                                                saltStrn, rdblStatus.SelectedValue.ToInt32(), Request.Cookies["UserName"].Value.ToString());

                    if (userInserted)
                        RenderMessage(Color.Green, "User has been created successfully!");
                    else
                        RenderMessage(Color.Red, "An error occurred while creating the username!");
                }
            }
            else
            {
                blValidationMessage.ForeColor = Color.Red;
                blValidationMessage.Items.Clear();
                blValidationMessage.DataSource = m_errorMessagers;
                blValidationMessage.DataBind();
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// The following method is designed in order to render the validation
        /// methods.
        /// </summary>
        /// <param name="msgColor">Color msgColor</param>
        /// <param name="messageVerbiage">string messageVerbiage</param>
        private void RenderMessage(Color msgColor, string messageVerbiage)
        {
            blValidationMessage.ForeColor = msgColor;
            blValidationMessage.Items.Clear();
            blValidationMessage.Items.Add(messageVerbiage);
            blValidationMessage.DataBind();
        }
        /// <summary>
        /// The following method is designed in order to validate the input from
        /// the user before its being inserted into the database.
        /// </summary>
        /// <param name="firstName">string firstName</param>
        /// <param name="lastName">string lastName</param>
        /// <param name="emailAddress">string emailAddress</param>
        /// <param name="userName">string userName</param>
        /// <param name="password">string password</param>
        /// <param name="repassword">string repassword</param>
        /// <param name="statusVal">string statusVal</param>
        /// <returns>Returns a boolean value indicating the status of the validation.</returns>
        private bool validationInput(string firstName, string lastName,
                    string emailAddress, string userName, string password, string repassword, int statusVal)
        {
            bool isValid = true;
            m_errorMessagers = new List<string>();

            if (string.IsNullOrEmpty(firstName.Trim()))
            {
                isValid = false;
                m_errorMessagers.Add("First Name field is left empty.");
            }

            if (string.IsNullOrEmpty(lastName.Trim()))
            {
                isValid = false;
                m_errorMessagers.Add("Last Name field is left empty.");
            }

            if (string.IsNullOrEmpty(emailAddress.Trim()))
            {
                isValid = false;
                m_errorMessagers.Add("Email Address field is left empty.");
            }
            else
            {
                bool isValidEmail = tbEmailAddress.Text.isValidEmail();

                if (!isValidEmail)
                {
                    isValid = false;
                    m_errorMessagers.Add("Email Address is not in a correct format. (Ex: MyEmail@Mail.com)");
                }
            }

            if (string.IsNullOrEmpty(userName.Trim()))
            {
                isValid = false;
                m_errorMessagers.Add("User Name field is left empty.");
            }

            if (string.IsNullOrEmpty(password.Trim()))
            {
                isValid = false;
                m_errorMessagers.Add("Password field is left empty.");
            }

            if (string.IsNullOrEmpty(repassword.Trim()))
            {
                isValid = false;
                m_errorMessagers.Add("Re-enter Password field is left empty.");
            }

            if (!string.IsNullOrEmpty(password.Trim()) && !string.IsNullOrEmpty(repassword.Trim()))
            {
                bool isPasswordStrong = tbPassword.Text.isPasswordStrong();

                if (!isPasswordStrong)
                {
                    isValid = false;
                    m_errorMessagers.Add("Password must be 8 characters and have both letters, numbers and non-alphanumerical.");
                }
                else
                {
                    if (!password.Equals(repassword))
                    {
                        isValid = false;
                        m_errorMessagers.Add("Passwords do not match.");
                    }
                }
            }
            if (statusVal == -1)
            {
                isValid = false;
                m_errorMessagers.Add("Status left unchecked.");
            }

            return isValid;
        }
        #endregion
    }
}