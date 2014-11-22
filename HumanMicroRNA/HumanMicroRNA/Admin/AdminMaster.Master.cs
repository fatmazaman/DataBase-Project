using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using HumanMicroRNA.BusinessLayer.Base;
using HumanMicroRNA.BusinessLayer.Users;
using System.Data;

namespace HumanMicroRNA.Admin
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        #region Page Events
        /// <summary>
        /// The event method is designed in order to be exeuted at page load.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SetUsersInformation();
        }
        #endregion
        #region Events
        /// <summary>
        /// The following event is designed in order to be executed 
        /// when the sign out button is clicked.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void lbSignOut_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            lblWelcome.Text = string.Empty;
            tblSignOutWelcome.Visible = false;

            Response.Redirect(BusConstants.PageNavigation.AdminLoginPage);
        }
        #endregion
        #region Private Methods
        /// <summary>
        /// The following method is designed to retrieve
        /// the basec information for a particular user based on 
        /// the username used to log in.
        /// </summary>
        private void SetUsersInformation()
        {
            HttpCookie cookie = Request.Cookies.Get(FormsAuthentication.FormsCookieName);

            if (cookie != null)
            {
                tblSignOutWelcome.Visible = true;
                HttpCookie cookieUserName =  Request.Cookies.Get("UserName");
                if (cookieUserName != null)
                {
                    DataTable dtUserInfo = BusUsers.GetUsersInfoByUserName(cookieUserName.Value.Trim());
                    if (dtUserInfo.Rows.Count > 0)
                        lblWelcome.Text = "Welcome, " + dtUserInfo.Rows[0]["user_first_name"] + " " + dtUserInfo.Rows[0]["user_last_name"];
                }
                else
                    lblWelcome.Text = "Welcome";
            }
            else
            {
                tblSignOutWelcome.Visible = false;

            }
        }
        #endregion
    }
}