<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="BookARoom.Shared.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <br />
    <p>
        Tyvärr inträffade ett oväntat fel vid hanteringen av din förfrågan.
    </p>
    
    <img src="../Content/Images/missing.jpg" />

    <p>
        <a href='<%$ RouteUrl:routename=Default %>' runat="server">Gå till bokningar</a>
        <a href='<%$ RouteUrl:routename=CustomerListing %>' runat="server">Gå till Admin</a>
    </p>
</asp:Content>
