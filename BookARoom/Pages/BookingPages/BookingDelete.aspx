<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SiteMaster.Master" AutoEventWireup="true" CodeBehind="BookingDelete.aspx.cs" Inherits="BookARoom.Pages.BookingPages.BookingDelete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    
    <%--RUBRIK--%>
    <h2>Radera bokning</h2>

    <%--VALIDERINGSSUMMERING--%>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="ValidationSummary" />

    <%--MEDDELANDE - ÄR DU SÄKER?--%>
    <asp:PlaceHolder runat="server" ID="ConfirmationPlaceHolder">
        <p>
            Är du säker på att du vill ta bort bokningen <strong><asp:Literal runat="server" ID="BookingDateTime" ViewStateMode="Enabled" /></strong>?
        </p>
    </asp:PlaceHolder>
    
    <%--BEKRÄFTA/AVBRYT--%>
    <div>
        <asp:LinkButton runat="server" ID="DeleteLinkButton" CssClass="action-button shadow animate red" Text="Radera" 
            OnCommand="DeleteLinkButton_Command"  />
        <asp:HyperLink runat="server" CssClass="action-button shadow animate blue" ID="CancelHyperLink" Text="Avbryt" />
    </div>
</asp:Content>
