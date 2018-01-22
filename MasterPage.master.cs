using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if  (Session["nickName"] != null)
            lkbLogout.Text = HttpContext.Current.Session["nickName"].ToString();
    }

    protected void lkbLogout_Click(object sender, EventArgs e)
    {
        System.Web.Security.FormsAuthentication.SignOut();
        Session.Abandon();
        System.Web.Security.FormsAuthentication.RedirectToLoginPage();
    }
}
