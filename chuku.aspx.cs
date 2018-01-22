using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class chuku : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            UpdateSheet();
        } else
            if (Session["nickName"] != null)
            {
                txDpt.Text = Session["department"].ToString();
                txUsr.Text = Session["nickName"].ToString();
            }
    }

    protected int UpdateSheet() {

        return 0;
    }
}