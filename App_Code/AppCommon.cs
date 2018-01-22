using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.OleDb;

namespace AppCommon
{
    /// <summary>
    /// AppCommon 的摘要说明
    /// </summary>
    #region 系统常量
    public static class AppInfo
    {
        public static string ApplicationName = "物资出库管理平台";
    }
    public static class AppConst
    {
        public static string[,] arrCounty = new string[4, 2] { { "永定区", "Y" }, { "慈利县", "L" }, { "桑植县", "S" }, { "武陵源区", "U" } };
        public static string[,] arrZone = new string[2, 2] { { "城区", "城区" }, { "乡镇", "乡镇" } };
        public static string[,] arrSiteType = new string[6, 2] { { "宏基站", "宏基站" }, { "微基站", "微基站" }, { "室内分布", "室内分布" }, { "拉远站", "拉远站" }, { "直放站", "直放站" }, { "接入网机箱", "接入网机箱" } };
        public static string[,] arrProperty = new string[2, 2] { { "自建", "自建" }, { "租赁", "租赁" } };
        public static string[,] arrNetType = new string[2, 2] { { "GSM", "GSM" }, { "WCDMA", "WCDMA" } };
        public static string[,] arrSuject = new string[2, 2] { { "移网", "移网基站" }, { "接入网", "接入网" } };
        public static string[,] arrFileType = new string[16, 2] { { "通知", "通知" }, { "规章制度", "规章制度" }, { "维护规范", "维护规范" }, { "客户响应", "客户响应" }, { "安全生产", "安全生产" }, { "公共资料", "公共资料" }, { "网络监控", "网络监控" }, { "核心网", "核心网" }, { "无线", "无线" }, { "有线传输", "有线传输" }, { "数据通信", "数据通信" }, { "电源配套", "电源配套" }, { "资源管理", "资源管理" }, { "代维相关", "代维相关" }, { "故障相关", "故障相关" }, { "其他", "其他" } };
        public static string[,] arrTimeType = new string[7, 2] { { "上午", "1" }, { "下午", "2" }, { "外勤", "3" }, { "加夜班", "4" }, { "私人", "5" }, { "未签到", "6" }, { "工作督办", "7" } };
        public static string[,] arrUserState = new string[2, 2] { { "正常", "正常" }, { "停用", "停用" } };
        public static string[,] arrKeyType = new string[6, 2] { { "通用钥匙", "通用钥匙" }, { "专用钥匙", "专用钥匙" }, { "其他运营商", "其他运营商" }, { "机箱钥匙", "机箱钥匙" }, { "围墙钥匙", "围墙钥匙" }, { "油机房钥匙", "油机房钥匙" } };
    }
    #endregion

    #region 通用函数
    public class MyFunction
    {
        static public string CountyName(string county)
        {
            string result = "空";
            for (int i = 0; i <= AppConst.arrCounty.GetUpperBound(0); i++)
            {
                if (AppConst.arrCounty[i, 1] == county)
                {
                    result = AppConst.arrCounty[i, 0];
                    break;
                }
            }
            return result;
        }
        #region 填充数据集
        public static void FillGridView(GridView gv, string sqlcmd, string sortExp, string order)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["IMSCon"].ToString());
            SqlDataAdapter sda = new SqlDataAdapter(sqlcmd, con);
            DataSet ds = new DataSet();
            sda.Fill(ds, "tmp");
            ds.Tables["tmp"].DefaultView.Sort = sortExp + " " + order;
            gv.DataSource = ds.Tables["tmp"].DefaultView;
            gv.DataBind();
            con.Close();
        }
        public static void FillGridView(GridView gv, string sqlcmd)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["IMSCon"].ToString());
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = new SqlCommand(sqlcmd, con);
            DataSet ds = new DataSet();
            sda.Fill(ds, "tmp");
            gv.DataSource = ds.Tables["tmp"].DefaultView;
            gv.DataBind();
            con.Close();
        }
        public static void FillList(DropDownList conList, string[,] conArray)
        {
            conList.Items.Clear();
            int u1 = conArray.GetUpperBound(0);
            for (int i = 0; i <= u1; i++)
            {
                conList.Items.Add(new ListItem(conArray[i, 0], conArray[i, 1]));

            }
        }
        public static void FillList(DropDownList conList, string[,] conArray, bool ifAll)
        {
            conList.Items.Clear();
            if (ifAll)
            {
                ListItem itmAll = new ListItem();
                itmAll.Text = "全部";
                itmAll.Value = "";
                itmAll.Selected = true;
                conList.Items.Add(itmAll);
            }

            int u1 = conArray.GetUpperBound(0);
            for (int i = 0; i <= u1; i++)
            {
                conList.Items.Add(new ListItem(conArray[i, 0], conArray[i, 1]));

            }
        }
        public static void FillList(DropDownList dbList, string sqlcmd, string txtField, string Value)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["IMSCon"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlcmd, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            dbList.DataSource = sdr;
            dbList.DataTextField = txtField;
            dbList.DataValueField = Value;
            dbList.DataBind();
            sdr.Close();
            con.Close();
        }
        public static void FillList(RadioButtonList dbList, string sqlcmd, string txtField, string Value)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["IMSCon"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlcmd, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            dbList.DataSource = sdr;
            dbList.DataTextField = txtField;
            dbList.DataValueField = Value;
            dbList.DataBind();
            sdr.Close();
            con.Close();
        }
        public static void FillList(ListBox dbList, string sqlcmd, string txtField, string Value)
        {
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["IMSCon"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlcmd, con);
            SqlDataReader sdr = cmd.ExecuteReader();
            dbList.DataSource = sdr;
            dbList.DataTextField = txtField;
            dbList.DataValueField = Value;
            dbList.DataBind();
            sdr.Close();
            con.Close();
        }
        #endregion

        #region 自定义Popup消息类
        //icon: 0:红圆圈感叹号，1：绿圆圈勾号，2：红圆圈叉号，3：橙圆圈问号，4：灰圆圈锁定， 5：红不高兴脸，6：红开心脸
        public static void Msg(Control obj, Type typeOrg, string Content)
        {
            string csName = "layUI";
            string csText = "layui.use('layer', function(){ var layer = layui.layer; layer.msg('" + Content;
            csText += "') });";
            ScriptManager.RegisterClientScriptBlock(obj, typeOrg, csName, csText, true);
        }
        //AppCommon.MyFunction.Msg(Page, this.GetType(), "用户名或密码不能为空！");
        public static void MsgwithIcon(Control obj, Type typeOrg, string Content, int icon)
        {
            string csName = "layUI";
            string csText = "layui.use('layer', function(){ var layer = layui.layer; layer.msg('" + Content;
            csText += "',{icon:" + icon.ToString() + "}) });";
            ScriptManager.RegisterClientScriptBlock(obj, typeOrg, csName, csText, true);
        }
        //AppCommon.MyFunction.MsgwithIcon(Page, this.GetType(), "用户名或密码不能为空！",2);
        public static void Alert(Control obj, Type typeOrg, string Content)
        {
            string csName = "layUI";
            string csText = "layui.use('layer', function(){ var layer = layui.layer; layer.alert('" + Content;
            csText += "') });";
            ScriptManager.RegisterClientScriptBlock(obj, typeOrg, csName, csText, true);
        }
        //AppCommon.MyFunction.Alert(Page, this.GetType(), "用户名或密码不能为空！");
        public static void AlertwithIcon(Control obj, Type typeOrg, string Content, int icon)
        {
            string csName = "layUI";
            string csText = "layui.use('layer', function(){ var layer = layui.layer; layer.alert('" + Content;
            csText += "',{icon:" + icon.ToString() + "}) });";
            ScriptManager.RegisterClientScriptBlock(obj, typeOrg, csName, csText, true);
        }
        //AppCommon.MyFunction.AlertwithIcon(Page, this.GetType(), "用户名或密码不能为空！", 4);
        public static void Popup2(Control obj, Type typeOrg, string Content)
        {
            string csName = "layUI";
            string csText = "layui.use('layer', function(){ var layer = layui.layer; layer.open({title: '信息',content: '" + Content;
            csText += "' }) });";
            ScriptManager.RegisterClientScriptBlock(obj, typeOrg, csName, csText, true);
        }
        //AppCommon.MyFunction.Popup2(Page, this.GetType(), "popup");
        public static void Popup(Control obj, Type typeOrg, string Content)
        {
            String csname1 = "TipScript";
            String cstext1 = "art.dialog({";
            cstext1 += "content:'" + Content;
            cstext1 += "'});";
            ScriptManager.RegisterClientScriptBlock(obj, typeOrg, csname1, cstext1, true);
        }
        public static void Popup(Page pgOrg, Type typeOrg, string Title, string Content)
        {
            String csname1 = "PopupScript";
            ClientScriptManager cs = pgOrg.ClientScript;
            if (!cs.IsStartupScriptRegistered(typeOrg, csname1))
            {
                String cstext1 = "art.dialog({";
                cstext1 += "title:'" + Title;
                cstext1 += "',content:'" + Content;
                cstext1 += "'});";
                cs.RegisterStartupScript(typeOrg, csname1, cstext1, true);
            }
        }
        public static void Popup(Page pgOrg, Type typeOrg, string Title, string Content, string Options)
        {
            String csname1 = "PopupScript2";
            ClientScriptManager cs = pgOrg.ClientScript;
            if (!cs.IsStartupScriptRegistered(typeOrg, csname1))
            {
                String cstext1 = "art.dialog({";
                cstext1 += "title:'" + Title;
                cstext1 += "',content:'" + Content;
                if (Options.Trim() != "")
                {
                    cstext1 += "'," + Options;
                }
                cstext1 += "});";
                cs.RegisterStartupScript(typeOrg, csname1, cstext1, true);
            }
        }
        // 固定lock提示，Control和page两类，用于异步与普通模式
        // 异步
        public static void myTip(Control obj, Type typeOrg, string Content, string sIcon, double iTime)
        {
            String csname1 = "myTipScript";

            String cstext1 = "art.dialog({";
            cstext1 += "icon:'" + sIcon + "',";
            cstext1 += "id:'Tips',title:'提示',cancel:false,fixed:true,lock:true})";
            cstext1 += ".content('" + Content + "')";
            cstext1 += ".time(" + Convert.ToString(iTime) + ");";
            ScriptManager.RegisterClientScriptBlock(obj, typeOrg, csname1, cstext1, true);

        }
        public static void myTip(Control obj, Type typeOrg, string Content)
        {
            String csname1 = "myTipScript";
            String cstext1 = "art.dialog({";
            cstext1 += "id:'Tips',title:'提示',cancel:false,fixed:true,lock:true})";
            cstext1 += ".content('" + Content + "').time(2)";
            ScriptManager.RegisterClientScriptBlock(obj, typeOrg, csname1, cstext1, true);
        }
        public static void myTip(Control obj, Type typeOrg, string Content, string sIcon)
        {
            String csname1 = "myTipScript";
            String cstext1 = "art.dialog({";
            cstext1 += "icon:'" + sIcon + "',";
            cstext1 += "id:'Tips',title:'提示',cancel:false,fixed:true,lock:true})";
            cstext1 += ".content('" + Content + "').time(2)";
            ScriptManager.RegisterClientScriptBlock(obj, typeOrg, csname1, cstext1, true);
        }
        public static void myTip(Control obj, Type typeOrg, string Content, double iTime)
        {
            String csname1 = "myTipScript";
            String cstext1 = "art.dialog({";
            cstext1 += "id:'Tips',title:'提示',cancel:false,fixed:true,lock:true})";
            cstext1 += ".content('" + Content + "')";
            cstext1 += ".time(" + Convert.ToString(iTime) + ");";
            ScriptManager.RegisterClientScriptBlock(obj, typeOrg, csname1, cstext1, true);
        }
        //普通
        public static void myTip(Page pgOrg, Type typeOrg, string Content)
        {
            String csname1 = "myTipScript";
            ClientScriptManager cs = pgOrg.ClientScript;
            if (!cs.IsStartupScriptRegistered(typeOrg, csname1))
            {
                String cstext1 = "art.dialog({";
                cstext1 += "id:'Tips',title:'提示',cancel:false,fixed:true,lock:true})";
                cstext1 += ".content('" + Content + "').time(2)";
                cs.RegisterStartupScript(typeOrg, csname1, cstext1, true);
            }
        }
        public static void myTip(Page pgOrg, Type typeOrg, string Content, string sIcon)
        {
            String csname1 = "myTipScript";
            ClientScriptManager cs = pgOrg.ClientScript;
            if (!cs.IsStartupScriptRegistered(typeOrg, csname1))
            {
                String cstext1 = "art.dialog({";
                cstext1 += "icon:'" + sIcon + "',";
                cstext1 += "id:'Tips',title:'提示',cancel:false,fixed:true,lock:true})";
                cstext1 += ".content('" + Content + "').time(2)";
                cs.RegisterStartupScript(typeOrg, csname1, cstext1, true);
            }
        }
        public static void myTip(Page pgOrg, Type typeOrg, string Content, double iTime)
        {
            String csname1 = "myTipScript";
            ClientScriptManager cs = pgOrg.ClientScript;
            if (!cs.IsStartupScriptRegistered(typeOrg, csname1))
            {
                String cstext1 = "art.dialog({";
                cstext1 += "id:'Tips',title:'提示',cancel:false,fixed:true,lock:true})";
                cstext1 += ".content('" + Content + "')";
                cstext1 += ".time(" + Convert.ToString(iTime) + ");";
                cs.RegisterStartupScript(typeOrg, csname1, cstext1, true);
            }
        }
        public static void myTip(Page pgOrg, Type typeOrg, string Content, string sIcon, double iTime)
        {
            String csname1 = "myTipScript";
            ClientScriptManager cs = pgOrg.ClientScript;
            if (!cs.IsStartupScriptRegistered(typeOrg, csname1))
            {
                String cstext1 = "art.dialog({";
                cstext1 += "icon:'" + sIcon + "',";
                cstext1 += "id:'Tips',title:'提示',cancel:false,fixed:true,lock:true})";
                cstext1 += ".content('" + Content + "')";
                cstext1 += ".time(" + Convert.ToString(iTime) + ");";
                cs.RegisterStartupScript(typeOrg, csname1, cstext1, true);
            }
        }
        #endregion

        /// <summary>
        /// 根据 User Agent 获取操作系统名称
        /// </summary>
        public static string GetOSNameByUserAgent(string userAgent)//需要列出基本的操作系统.
        {
            string osVersion = "未知";

            if (userAgent.Contains("NT 6.1"))
            {
                osVersion = "Windows 7";
            }
            else
                if (userAgent.Contains("NT 6.0"))
            {
                osVersion = "Windows Vista/Server 2008";
            }
            else if (userAgent.Contains("NT 5.2"))
            {
                osVersion = "Windows Server 2003";
            }
            else if (userAgent.Contains("NT 5.1"))
            {
                osVersion = "Windows XP";
            }
            else if (userAgent.Contains("NT 5"))
            {
                osVersion = "Windows 2000";
            }
            else if (userAgent.Contains("NT 4"))
            {
                osVersion = "Windows NT4";
            }
            else if (userAgent.Contains("Me"))
            {
                osVersion = "Windows Me";
            }
            else if (userAgent.Contains("98"))
            {
                osVersion = "Windows 98";
            }
            else if (userAgent.Contains("95"))
            {
                osVersion = "Windows 95";
            }
            else if (userAgent.Contains("Mac"))
            {
                osVersion = "Mac";
            }
            else if (userAgent.Contains("Unix"))
            {
                osVersion = "UNIX";
            }
            else if (userAgent.Contains("Linux"))
            {
                osVersion = "Linux";
            }
            else if (userAgent.Contains("SunOS"))
            {
                osVersion = "SunOS";
            }
            return osVersion;
        }

        public static string Escape(string s)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byteArr = System.Text.Encoding.Unicode.GetBytes(s);

            for (int i = 0; i < byteArr.Length; i += 2)
            {
                sb.Append("%u");
                sb.Append(byteArr[i + 1].ToString("X2"));//把字节转换为十六进制的字符串表现形式

                sb.Append(byteArr[i].ToString("X2"));
            }
            return sb.ToString();
        }
        public static string UnEscape(string s)
        {
            string str = s.Remove(0, 2);//删除最前面两个＂%u＂
            string[] strArr = str.Split(new string[] { "%u" }, StringSplitOptions.None);//以子字符串＂%u＂分隔
            byte[] byteArr = new byte[strArr.Length * 2];
            for (int i = 0, j = 0; i < strArr.Length; i++, j += 2)
            {
                byteArr[j + 1] = Convert.ToByte(strArr[i].Substring(0, 2), 16);  //把十六进制形式的字串符串转换为二进制字节
                byteArr[j] = Convert.ToByte(strArr[i].Substring(2, 2), 16);
            }
            str = System.Text.Encoding.Unicode.GetString(byteArr); //把字节转为unicode编码
            return str;
        }

        #region 生成部门树图
        public static void MakeDeptmentTreeNodes(TreeView Tree)
        {
            TreeNode tn = new TreeNode();
            tn.Text = "中国联通张家界分公司";
            tn.Value = "4308";
            Tree.Nodes.Add(tn);
            AddChildNodes(tn);

            TreeNode tn2 = new TreeNode();
            tn2.Text = "第三方维护单位";
            tn2.Value = "D308";
            Tree.Nodes.Add(tn2);
            AddChildNodes(tn2);
        }

        protected static void AddChildNodes(TreeNode tn)
        {
            string id = tn.Value;
            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["IMSCon"].ToString());
            string strSQL = "SELECT * from Departments WHERE Used=1 and len(DptID)=6 and left(DptID,4)='" + id + "'";
            DataSet ds = new DataSet();
            DataView dvr = new DataView();
            SqlDataAdapter sda = new SqlDataAdapter(strSQL, conn);
            sda.Fill(ds, "Tree");

            foreach (DataRow row in ds.Tables["Tree"].Rows)
            {
                TreeNode ctn = new TreeNode();
                ctn.Text = row["DepartmentName"].ToString();
                ctn.Value = row["DptID"].ToString();
                tn.ChildNodes.Add(ctn);
                AddMoreChildNodes(ctn);
            }
            conn.Close();
        }

        protected static void AddMoreChildNodes(TreeNode tn)
        {
            string id = tn.Value;
            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["IMSCon"].ToString());
            string strSQL = "SELECT * from Departments WHERE Used=1 and len(DptID)=8 and left(DptID,6)='" + id + "'";
            DataSet ds = new DataSet();
            DataView dvr = new DataView();
            SqlDataAdapter sda = new SqlDataAdapter(strSQL, conn);
            sda.Fill(ds, "sonTree");

            foreach (DataRow row in ds.Tables["sonTree"].Rows)
            {
                TreeNode ctn = new TreeNode();
                ctn.Text = row["DepartmentName"].ToString();
                ctn.Value = row["DptID"].ToString();
                tn.ChildNodes.Add(ctn);
            }
            conn.Close();
        }
        #endregion

        #region 读取用户参数
        public static int getPager(string usr)
        {
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["IMSCon"].ToString());
            SqlCommand sql = new SqlCommand("SELECT pager FROM [Params_usr] WHERE usrName='" + usr + "'", conn);

            conn.Open();
            int rst = int.Parse(sql.ExecuteScalar().ToString());

            rst = rst == 0 ? 15 : rst;

            conn.Close();
            return rst;
        }
        public static string getUserParam(string usr, string sField)
        {
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["IMSCon"].ToString());
            SqlCommand sql = new SqlCommand("SELECT " + sField + " FROM [Params_usr] WHERE usrName='" + usr + "'", conn);

            conn.Open();
            string rst = sql.ExecuteScalar().ToString();

            conn.Close();
            return rst;
        }
        public static string getThemePath(string usr)
        {
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["IMSCon"].ToString());
            SqlCommand sql = new SqlCommand("SELECT theme FROM [Params_usr] WHERE usrName='" + usr + "'", conn);

            conn.Open();
            string rst = "";
            object ob = sql.ExecuteScalar();
            if (ob != null)
                rst = ob.ToString();

            rst = rst == "" ? "red" : rst;

            conn.Close();
            return rst;
        }
        public static string getPhySiteID(string p)
        {
            string rst = "";
            string SQL = "SELECT ID FROM [PhySiteLocation] WHERE SiteName ='" + p + "'";
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["IMSCon"].ToString());
            SqlCommand comm = new SqlCommand(SQL, conn);

            conn.Open();
            object r = comm.ExecuteScalar();
            if (r != null)
                rst = r.ToString();

            conn.Close();
            return rst;
        }
        public static string getLogicSiteID(string p)
        {
            string rst = "";
            string SQL = "SELECT SiteID FROM [LogicSite] WHERE SiteName ='" + p + "'";
            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["IMSCon"].ToString());
            SqlCommand comm = new SqlCommand(SQL, conn);

            conn.Open();
            object r = comm.ExecuteScalar();
            if (r != null)
                rst = r.ToString();

            conn.Close();
            return rst;
        }
        #endregion

        #region Excel相关函数
        //获得Excel第一个sheet的内容
        public static DataSet GetExcelContent(string filePath)
        {
            //excel2007,兼容2003  
            string strCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1';";
            //excel2003  
            // string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=No;IMEX=1'";  
            OleDbConnection myConn = new OleDbConnection(strCon);
            myConn.Open();
            //获取excel第一标签名  
            DataTable schemaTable = myConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
            string tableName = schemaTable.Rows[0][2].ToString().Trim();//标签名  
            string strCom = "SELECT *  FROM [" + tableName + "]";//查询语句  
            System.Data.OleDb.OleDbDataAdapter myCommand = new System.Data.OleDb.OleDbDataAdapter(strCom, myConn);
            //创建一个DataSet对象     
            DataSet myDataSet = new DataSet();
            //得到自己的DataSet对象     
            myCommand.Fill(myDataSet);
            //关闭此数据链接     
            myConn.Close();
            return myDataSet;
        }

        //获得Excel中的所有sheetname。
        public static ArrayList ExcelSheetName(string FilePath)
        {
            ArrayList al = new ArrayList();
            string strConn, tmp;
            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1';";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataTable sheetNames = conn.GetOleDbSchemaTable
            (System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            conn.Close();
            foreach (DataRow dr in sheetNames.Rows)
            {
                tmp = dr[2].ToString();
                tmp = tmp.Substring(0, tmp.LastIndexOf("$"));
                al.Add(tmp);
            }
            return al;
        }

        //该方法实现从Excel中导出数据到DataSet中，其中filepath为Excel文件的绝对路径，sheetname为表示那个Excel表；
        public static DataSet ExcelDataSource(string FilePath, string SheetName)
        {
            string strConn;
            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1';";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strCom = "SELECT *  FROM [" + SheetName + "$]";//查询语句  
            System.Data.OleDb.OleDbDataAdapter myCommand = new System.Data.OleDb.OleDbDataAdapter(strCom, conn);
            //创建一个DataSet对象     
            DataSet myDataSet = new DataSet();
            //得到自己的DataSet对象     
            myCommand.Fill(myDataSet);
            //关闭此数据链接     
            conn.Close();
            return myDataSet;
        }

        #endregion

        public static void RestoreScroll(Page page)
        {
            //注册一个Hidden Filed
            page.ClientScript.RegisterHiddenField("Hidden_AX", "0");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //当滚动条滚动时将其到页顶的位置保存在Hidden Field中
            sb.AppendLine("function getPageScroll()                                         ");
            sb.AppendLine("{                                                                ");
            sb.AppendLine("    var yScroll;                                                 ");
            sb.AppendLine("    if (document.documentElement.scrollTop)//符合DTD标准的Page   ");
            sb.AppendLine("    {                                                            ");
            sb.AppendLine("        yScroll = document.documentElement.scrollTop;            ");
            sb.AppendLine("    }                                                            ");
            sb.AppendLine("    else                                                         ");
            sb.AppendLine("    {                                                            ");
            sb.AppendLine("        yScroll = document.body.scrollTop;                       ");
            sb.AppendLine("    }                                                            ");
            sb.AppendLine("    return yScroll;                                              ");
            sb.AppendLine("}                                                                ");
            sb.AppendLine("function saveScrollPosition()                                    ");
            sb.AppendLine("{                                                                ");
            sb.AppendLine("    document.getElementById('Hidden_AX').value = getPageScroll();");
            sb.AppendLine("}                                                                ");
            sb.AppendLine("window.onscroll=saveScrollPosition;                              ");
            page.ClientScript.RegisterStartupScript(page.GetType(), "AX", sb.ToString(), true);
            sb.Remove(0, sb.Length);
            sb.AppendLine("function setScrollPosition()                                      ");
            sb.AppendLine("{                                                                 ");
            sb.AppendLine("    window.scrollTo(0," + page.Request["Hidden_AX"] + ");         ");
            sb.AppendLine("}                                                                 ");
            sb.AppendLine("document.body.onload=setScrollPosition;                           ");
            page.ClientScript.RegisterStartupScript(page.GetType(), "AXzhz", sb.ToString(), true);
        }

        #region 汉字处理函数
        public static string GetChineseSpell(string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += getSpell(strText.Substring(i, 1), false);
            }
            return myStr;
        }
        static public string[] GetChineseSpell(string[] strText)
        {
            int len = strText.Length;
            string[] myStr = null;
            for (int i = 0; i < len; i++)
            {
                myStr[i] = getSpell(strText[i], false);
            }
            return myStr;
        }
        static public string getSpell(string cnChar, bool Eng)
        {
            byte[] arrCN = Encoding.Default.GetBytes(cnChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "*";
            }
            else
            {
                if (Eng)
                    return cnChar;
                else
                    return "";
            }
        }

    }

    //汉字到全拼转换
    public class Hz2Py
    {
        private static int[] pyValue = new int[]
                {
                    -20319,-20317,-20304,-20295,-20292,-20283,-20265,-20257,-20242,-20230,-20051,-20036,
                    -20032,-20026,-20002,-19990,-19986,-19982,-19976,-19805,-19784,-19775,-19774,-19763,
                    -19756,-19751,-19746,-19741,-19739,-19728,-19725,-19715,-19540,-19531,-19525,-19515,
                    -19500,-19484,-19479,-19467,-19289,-19288,-19281,-19275,-19270,-19263,-19261,-19249,
                    -19243,-19242,-19238,-19235,-19227,-19224,-19218,-19212,-19038,-19023,-19018,-19006,
                    -19003,-18996,-18977,-18961,-18952,-18783,-18774,-18773,-18763,-18756,-18741,-18735,
                    -18731,-18722,-18710,-18697,-18696,-18526,-18518,-18501,-18490,-18478,-18463,-18448,
                    -18447,-18446,-18239,-18237,-18231,-18220,-18211,-18201,-18184,-18183, -18181,-18012,
                    -17997,-17988,-17970,-17964,-17961,-17950,-17947,-17931,-17928,-17922,-17759,-17752,
                    -17733,-17730,-17721,-17703,-17701,-17697,-17692,-17683,-17676,-17496,-17487,-17482,
                    -17468,-17454,-17433,-17427,-17417,-17202,-17185,-16983,-16970,-16942,-16915,-16733,
                    -16708,-16706,-16689,-16664,-16657,-16647,-16474,-16470,-16465,-16459,-16452,-16448,
                    -16433,-16429,-16427,-16423,-16419,-16412,-16407,-16403,-16401,-16393,-16220,-16216,
                    -16212,-16205,-16202,-16187,-16180,-16171,-16169,-16158,-16155,-15959,-15958,-15944,
                    -15933,-15920,-15915,-15903,-15889,-15878,-15707,-15701,-15681,-15667,-15661,-15659,
                    -15652,-15640,-15631,-15625,-15454,-15448,-15436,-15435,-15419,-15416,-15408,-15394,
                    -15385,-15377,-15375,-15369,-15363,-15362,-15183,-15180,-15165,-15158,-15153,-15150,
                    -15149,-15144,-15143,-15141,-15140,-15139,-15128,-15121,-15119,-15117,-15110,-15109,
                    -14941,-14937,-14933,-14930,-14929,-14928,-14926,-14922,-14921,-14914,-14908,-14902,
                    -14894,-14889,-14882,-14873,-14871,-14857,-14678,-14674,-14670,-14668,-14663,-14654,
                    -14645,-14630,-14594,-14429,-14407,-14399,-14384,-14379,-14368,-14355,-14353,-14345,
                    -14170,-14159,-14151,-14149,-14145,-14140,-14137,-14135,-14125,-14123,-14122,-14112,
                    -14109,-14099,-14097,-14094,-14092,-14090,-14087,-14083,-13917,-13914,-13910,-13907,
                    -13906,-13905,-13896,-13894,-13878,-13870,-13859,-13847,-13831,-13658,-13611,-13601,
                    -13406,-13404,-13400,-13398,-13395,-13391,-13387,-13383,-13367,-13359,-13356,-13343,
                    -13340,-13329,-13326,-13318,-13147,-13138,-13120,-13107,-13096,-13095,-13091,-13076,
                    -13068,-13063,-13060,-12888,-12875,-12871,-12860,-12858,-12852,-12849,-12838,-12831,
                    -12829,-12812,-12802,-12607,-12597,-12594,-12585,-12556,-12359,-12346,-12320,-12300,
                    -12120,-12099,-12089,-12074,-12067,-12058,-12039,-11867,-11861,-11847,-11831,-11798,
                    -11781,-11604,-11589,-11536,-11358,-11340,-11339,-11324,-11303,-11097,-11077,-11067,
                    -11055,-11052,-11045,-11041,-11038,-11024,-11020,-11019,-11018,-11014,-10838,-10832,
                    -10815,-10800,-10790,-10780,-10764,-10587,-10544,-10533,-10519,-10331,-10329,-10328,
                    -10322,-10315,-10309,-10307,-10296,-10281,-10274,-10270,-10262,-10260,-10256,-10254
                };

        private static string[] pyName = new string[]
                {
                    "A","Ai","An","Ang","Ao","Ba","Bai","Ban","Bang","Bao","Bei","Ben",
                    "Beng","Bi","Bian","Biao","Bie","Bin","Bing","Bo","Bu","Ba","Cai","Can",
                    "Cang","Cao","Ce","Ceng","Cha","Chai","Chan","Chang","Chao","Che","Chen","Cheng",
                    "Chi","Chong","Chou","Chu","Chuai","Chuan","Chuang","Chui","Chun","Chuo","Ci","Cong",
                    "Cou","Cu","Cuan","Cui","Cun","Cuo","Da","Dai","Dan","Dang","Dao","De",
                    "Deng","Di","Dian","Diao","Die","Ding","Diu","Dong","Dou","Du","Duan","Dui",
                    "Dun","Duo","E","En","Er","Fa","Fan","Fang","Fei","Fen","Feng","Fo",
                    "Fou","Fu","Ga","Gai","Gan","Gang","Gao","Ge","Gei","Gen","Geng","Gong",
                    "Gou","Gu","Gua","Guai","Guan","Guang","Gui","Gun","Guo","Ha","Hai","Han",
                    "Hang","Hao","He","Hei","Hen","Heng","Hong","Hou","Hu","Hua","Huai","Huan",
                    "Huang","Hui","Hun","Huo","Ji","Jia","Jian","Jiang","Jiao","Jie","Jin","Jing",
                    "Jiong","Jiu","Ju","Juan","Jue","Jun","Ka","Kai","Kan","Kang","Kao","Ke",
                    "Ken","Keng","Kong","Kou","Ku","Kua","Kuai","Kuan","Kuang","Kui","Kun","Kuo",
                    "La","Lai","Lan","Lang","Lao","Le","Lei","Leng","Li","Lia","Lian","Liang",
                    "Liao","Lie","Lin","Ling","Liu","Long","Lou","Lu","Lv","Luan","Lue","Lun",
                    "Luo","Ma","Mai","Man","Mang","Mao","Me","Mei","Men","Meng","Mi","Mian",
                    "Miao","Mie","Min","Ming","Miu","Mo","Mou","Mu","Na","Nai","Nan","Nang",
                    "Nao","Ne","Nei","Nen","Neng","Ni","Nian","Niang","Niao","Nie","Nin","Ning",
                    "Niu","Nong","Nu","Nv","Nuan","Nue","Nuo","O","Ou","Pa","Pai","Pan",
                    "Pang","Pao","Pei","Pen","Peng","Pi","Pian","Piao","Pie","Pin","Ping","Po",
                    "Pu","Qi","Qia","Qian","Qiang","Qiao","Qie","Qin","Qing","Qiong","Qiu","Qu",
                    "Quan","Que","Qun","Ran","Rang","Rao","Re","Ren","Reng","Ri","Rong","Rou",
                    "Ru","Ruan","Rui","Run","Ruo","Sa","Sai","San","Sang","Sao","Se","Sen",
                    "Seng","Sha","Shai","Shan","Shang","Shao","She","Shen","Sheng","Shi","Shou","Shu",
                    "Shua","Shuai","Shuan","Shuang","Shui","Shun","Shuo","Si","Song","Sou","Su","Suan",
                    "Sui","Sun","Suo","Ta","Tai","Tan","Tang","Tao","Te","Teng","Ti","Tian",
                    "Tiao","Tie","Ting","Tong","Tou","Tu","Tuan","Tui","Tun","Tuo","Wa","Wai",
                    "Wan","Wang","Wei","Wen","Weng","Wo","Wu","Xi","Xia","Xian","Xiang","Xiao",
                    "Xie","Xin","Xing","Xiong","Xiu","Xu","Xuan","Xue","Xun","Ya","Yan","Yang",
                    "Yao","Ye","Yi","Yin","Ying","Yo","Yong","You","Yu","Yuan","Yue","Yun",
                    "Za", "Zai","Zan","Zang","Zao","Ze","Zei","Zen","Zeng","Zha","Zhai","Zhan",
                    "Zhang","Zhao","Zhe","Zhen","Zheng","Zhi","Zhong","Zhou","Zhu","Zhua","Zhuai","Zhuan",
                    "Zhuang","Zhui","Zhun","Zhuo","Zi","Zong","Zou","Zu","Zuan","Zui","Zun","Zuo"
                };

        /// <summary>
        /// 把汉字转换成拼音(全拼)
        /// </summary>
        /// <param name="hzString">汉字字符串</param>
        /// <returns>转换后的拼音(全拼)字符串</returns>
        public static string Convert(string hzString, bool Eng)
        {
            // 匹配中文字符
            Regex regex = new Regex("^[\u4e00-\u9fa5]$");
            byte[] array = new byte[2];
            string pyString = "";
            int chrAsc = 0;
            int i1 = 0;
            int i2 = 0;
            char[] noWChar = hzString.ToCharArray();

            for (int j = 0; j < noWChar.Length; j++)
            {
                // 中文字符
                if (regex.IsMatch(noWChar[j].ToString()))
                {
                    array = System.Text.Encoding.Default.GetBytes(noWChar[j].ToString());
                    i1 = (short)(array[0]);
                    i2 = (short)(array[1]);
                    chrAsc = i1 * 256 + i2 - 65536;
                    if (chrAsc > 0 && chrAsc < 160)
                    {
                        pyString += noWChar[j];
                    }
                    else
                    {
                        // 修正部分文字
                        if (chrAsc == -9254)  // 修正“圳”字
                            pyString += "Zhen";
                        else
                        {
                            for (int i = (pyValue.Length - 1); i >= 0; i--)
                            {
                                if (pyValue[i] <= chrAsc)
                                {
                                    pyString += pyName[i];
                                    break;
                                }
                            }
                        }
                    }
                }
                // 非中文字符
                else
                {
                    if (Eng) { pyString += noWChar[j].ToString(); }
                }
            }
            return pyString;
        }
    }
    #endregion
    #endregion
}