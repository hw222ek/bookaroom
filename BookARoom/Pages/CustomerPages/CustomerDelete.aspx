<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SiteMaster.Master" AutoEventWireup="true" CodeBehind="CustomerDelete.aspx.cs" Inherits="BookARoom.Pages.CustomerPages.CustomerDelete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    
    <%--RUBRIK--%>
    <h2>Radera kund</h2>

    <%--VALIDERINGSSUMMERING--%>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="ValidationSummary" />

    <%--MEDDELANDE - SÄKER?--%>
    <asp:PlaceHolder runat="server" ID="ConfirmationPlaceHolder">
        <p>
            Är du säker på att du vill ta bort kunden <strong><asp:Literal runat="server" ID="CustomerName" ViewStateMode="Enabled" /></strong>?
        </p>
    </asp:PlaceHolder>
    
    <%--BEKRÄFTA/AVBRYT--%>
    <div>
        <asp:LinkButton runat="server" CssClass="action-button shadow animate red" ID="DeleteLinkButton" Text="Radera" 
            OnCommand="DeleteLinkButton_Command" CommandArgument='<%$ RouteValue:id %>' />
        <asp:HyperLink runat="server" CssClass="action-button shadow animate blue" ID="CancelHyperLink" Text="Avbryt" />
    </div>
</asp:Content>
