using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            if ((usrname.Text.Trim() == "") || (pswd.Text.Trim() == ""))
                AppCommon.MyFunction.AlertwithIcon(Page, this.GetType(), "用户名或密码不能为空！", 0);
            else
                AuthUser(usrname.Text.Trim(), pswd.Text.Trim());
        }
    }

    protected void AuthUser(string usr ,string psw)
    {
        string strSQL = "select userName,nickName,department from users where userName=@usr and secKey=@psw";
        MySqlConnection con = new MySqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["printCon"].ToString());
        con.Open();
        MySqlCommand cmd = new MySqlCommand(strSQL, con);
        cmd.Parameters.AddWithValue("@usr", usr);
        cmd.Parameters.AddWithValue("@psw", psw);

        //object rst = cmd.ExecuteScalar();  if (rst == null)
        MySqlDataReader rd = cmd.ExecuteReader();
        if (! rd.Read())
        {
            AppCommon.MyFunction.AlertwithIcon(Page, this.GetType(), "用户名或密码错误！", 6);
        }
        else
        {
            string nick = rd["nickName"].ToString();
            Session["nickName"] = nick;
            Session["department"] = rd["department"].ToString();

            FormsAuthentication.RedirectFromLoginPage(nick, ckRemb.Checked);
        }

        con.Close();
    }
}