﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using HumanMicroRNA.BusinessLayer.Base;

namespace HumanMicroRNA.Admin
{
    public partial class ViewUsers : System.Web.UI.Page
    {
        #region Page Events
        /// <summary>
        /// The following event is designed in order to be executed
        /// at page load.
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

        public static string GetImageSatus(string statusID)
        {
            string imagePath = string.Empty;

            if (!string.IsNullOrEmpty(statusID))
            {
                switch (statusID)
                {
                    case "True":
                        imagePath = "Assets/Images/active.png";
                        break;
                    case "False":
                        imagePath = "Assets/Images/inactive.png";
                        break;
                    default:
                        break;
                }
            }
            return imagePath;
        }
    }
}