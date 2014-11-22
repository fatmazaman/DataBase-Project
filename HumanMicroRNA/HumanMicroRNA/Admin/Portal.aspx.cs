using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using HumanMicroRNA.BusinessLayer.Base;

namespace HumanMicroRNA.Admin
{
    public partial class Portal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies.Get(FormsAuthentication.FormsCookieName);

            if (cookie == null)
                Response.Redirect(BusConstants.PageNavigation.AdminLoginPage);
        }
    }
}