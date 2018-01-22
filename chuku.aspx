<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="chuku.aspx.cs" Inherits="chuku" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="images/sui.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <!-- 左侧导航区域 -->
      <div class="layui-side layui-bg-black">
        <div class="layui-side-scroll">      
          <ul class="layui-nav layui-nav-tree"  lay-filter="test">
              <li class="layui-nav-item layui-this"><a href="chuku.aspx">物资出库</a></li>
            <li class="layui-nav-item">
              <a class="" href="javascript:;">出库查询</a>
              <dl class="layui-nav-child">
                <dd><a href="javascript:;">库单查询</a></dd>
                <dd><a href="javascript:;">物资查询</a></dd>
              </dl>
            </li>
            <li class="layui-nav-item">
              <a href="javascript:;">出库分析</a>
              <dl class="layui-nav-child">
                <dd><a href="javascript:;">Top物资</a></dd>
                <dd><a href="javascript:;">日分析</a></dd>
                <dd><a href="">超链接</a></dd>
              </dl>
            </li>        
            <li class="layui-nav-item"><a href="">全部出库单</a></li>
          </ul>
        </div>
      </div>

    <div class="layui-body">
        <div style="padding: 15px;">
            <!-- 步骤条 -->
            <div class="layui-row layui-field-title layui-nav-title mid">填写出库单</div>
            <div class="sui-steps steps-auto">
              <div class="wrap">
                <div class="current">
                  <label><span class="round">1</span>
                      <span>第一步：填写出库单</span></label><i class="triangle-right-bg"></i><i class="triangle-right"></i>
                </div>
              </div>
              <div class="wrap">
                <div class="todo">
                  <label><span class="round">2</span>
                      <span>第二步：添加物资明细</span></label><i class="triangle-right-bg"></i><i class="triangle-right"></i>
                </div>
              </div>
              <div class="wrap">
                <div class="todo">
                  <label><span class="round">3</span>
                      <span>第三步：打印出库单</span></label>
                </div>
              </div>
            </div>   
    
            <!-- 表单 -->
            <div class="layui-row layui-border-box">
                <table class="layui-table">
                    <colgroup>
                        <col width="100" />
                        <col width="200" />
                    </colgroup>
                    <thead>
                        <tr>
                            <th class="mid">名称</th>
                            <th>出库单内容</th>
                        </tr>
                    </thead>
                    <tr>
                        <td class="right">领用部门：</td>
                        <td><asp:TextBox ID="txDpt" runat="server" Enabled="False" CssClass="layui-input" Width="30%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="right">领用人：</td>
                        <td><asp:TextBox ID="txUsr" runat="server" Enabled="False" CssClass="layui-input" Width="30%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="right">所属工程项目：</td>
                        <td><asp:TextBox ID="txPrj" runat="server" placeholder="请输入物资所属工程名称" CssClass="layui-input layui-form-danger" required></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="right">安装地点：</td>
                        <td><asp:TextBox ID="txAddr" runat="server" placeholder="请输入物资安装或使用的地点，尽量具体" CssClass="layui-input layui-form-danger" required></asp:TextBox></td>
                    </tr>
                </table>
            </div>
            <div class="layui-row right mr40">
                <asp:Button ID="btNext" runat="server" Text="下一步" CssClass="layui-btn"/>
            </div>
        </div>
    </div>
</asp:Content>

