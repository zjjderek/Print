<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="qingdan.aspx.cs" Inherits="qingdan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="images/sui.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="sui-steps steps-auto">
      <div class="wrap">
        <div class="finished">
          <label><span class="round"><i class="sui-icon icon-pc-right"></i></span>
              <span>第一步：填写出库单</span></label><i class="triangle-right-bg"></i><i class="triangle-right"></i>
        </div>
      </div>
      <div class="wrap">
        <div class="current">
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
</asp:Content>

