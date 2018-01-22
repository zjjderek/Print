<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title><%=AppCommon.AppInfo.ApplicationName%></title>    
    <script src="js/jquery-1.8.3.min.js"></script>
    <script src="js/jquery.tipsy.js"></script>
    <script src="js/login.js"></script>
    <script src="layui/layui.js"></script>
    <link href="images/Style.css" rel="stylesheet" />
    <link href="js/tipsy.css" rel="stylesheet" />
    <link href="layui/css/layui.css" rel="stylesheet" />
    
</head>
<body>
    <form id="form1" runat="server">
        <div id="lgFrame">
            <div id="top">
                <div id="logo">
                    <img alt="" src="images/logo.png" /></div>
                <div id="lgTitle"><%=AppCommon.AppInfo.ApplicationName %></div>
                <div id="lgSubTitle">
                    <a href="http://110.52.15.45:8000" class="webLink" target="_blank" title="原分公司运行维护平台">运维OA平台</a> | 
                    <a href="http://10.209.1.59" class="webLink" target="_blank" title="中国联通企业云门户">MSS系统</a> |                    
                    <a href="http://mail.wo.com.cn" class="webLink" target="_blank" title="165 WO邮箱">WO邮箱</a> | 
                </div>
            </div>
            <div id="mid">
                <div id="lgImg">
                    <div id="divUsr">用户名：<asp:TextBox ID="usrname" runat="server" onkeyup="showHint(this.value)" ToolTip="请输入用户名" placeholder="用户名"></asp:TextBox><span id="lbN" class="lbName"></span>
                    </div>
                    <div id="divPswd">密码：<asp:TextBox ID="pswd" runat="server" TextMode="Password" ToolTip="请输入登录密码" placeholder="密码"></asp:TextBox>
                    </div>
                    <div id="divRemb">
                        <asp:CheckBox ID="ckRemb" runat="server" lay-skin="primary" Text="自动登录" /></div>
                    <div id="divBtn">
                        <asp:Button ID="btLgn" runat="server" CssClass="layui-btn layui-btn-normal layui-btn-sm" Text="登录" />
                    </div>

                </div>
            </div>
            <div id="footer"><span class="cr"> 中国联通张家界市分公司 © 1998-2018 By：<a class="ClearLink" href="mailto:15607444955@wo.cn">Derek.Hsu</a></span></div>
        </div>
    </form>
</body>
</html>
