<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Start.aspx.cs" Inherits="BookARoom.Pages.StaticPages.Start" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <br />
    <asp:ImageButton CssClass="ImageLink" ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" ImageUrl="../../Content/Images/Room1.png" />
    <asp:ImageButton CssClass="ImageLink" ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" ImageUrl="../../Content/Images/Room2.png" />
    <asp:ImageButton CssClass="ImageLink" ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" ImageUrl="../../Content/Images/Room3.png" />
    <asp:ImageButton CssClass="ImageLink" ID="ImageButton11" runat="server" OnClick="ImageButton11_Click" ImageUrl="../../Content/Images/Room11.png" />
</asp:Content>

