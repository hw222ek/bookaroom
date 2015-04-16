<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="BookARoom.Pages.CustomerPages.AdminPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <br />
    <asp:Label ID="Label1" runat="server" Text="Namn"></asp:Label><br />
    <asp:TextBox ID="tbUsername" runat="server"></asp:TextBox>
    <br /><br />

    <asp:Label ID="Label2" runat="server" Text="Lösenord"></asp:Label><br />
    <asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox>

    <br /><br />
    <asp:LinkButton CssClass="action-button shadow animate blue" ID="LoginLinkButton" runat="server" OnClick="LoginButton_Click" Text="Logga in"/>
</asp:Content>
