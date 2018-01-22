<%@ WebHandler Language="C#" Class="getName" %>

using System;
using System.Web;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

public class getName : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string word = context.Request.QueryString["q"];

        string strSQL = "select nickName from users where userName=@usr";
        MySqlConnection con = new MySqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["printCon"].ToString());
        con.Open();
        MySqlCommand cmd = new MySqlCommand(strSQL, con);
        cmd.Parameters.AddWithValue("@usr", word);
        object rst = cmd.ExecuteScalar();

        if (rst == null)
        {
            context.Response.Write("<font color='red'>未知用户</font>");
        }
        else
        {
            context.Response.Write((string)rst);
        }

        con.Close();
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}