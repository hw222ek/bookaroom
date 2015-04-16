<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SiteMaster.Master" AutoEventWireup="true" CodeBehind="BookingUpdate.aspx.cs" Inherits="BookARoom.Pages.BookingPages.BookingUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <%--RUBRIK--%>
    <h2>Ändra bokning</h2>
    <p>
        Välj en ny tid i rullisten nedan och tryck "Uppdatera" för att ändra ursprunglig bokningstid. Med "Tillbaka återgår du till bokningsdetaljer. Om rullisten är tom finns det inga alternativa bokningstider lediga aktuellt datum."
    </p>

    <%--VALIDERINGSSUMMERING--%>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="ValidationSummary" />
    
    <%--BOKNINGSDETALJER--%>
    <asp:Label CssClass="HeaderLabel" ID="Label1" runat="server" Text="Välj ny starttid"/><br /><br />

    <%--VÄLJ BONINGSTID (INKL. VALIDERING) OCH BOKA--%>
    <asp:DropDownList ID="TimeDropDownList" runat="server" ItemType="BookARoom.Model.BLL.Booking" ViewStateMode="Enabled" AppendDataBoundItems="true" >
        <asp:ListItem Value="99">-- Lediga tider --</asp:ListItem>
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="ValError" ErrorMessage="En starttid måste väljas." ControlToValidate="TimeDropDownList" Text="*" Display="Dynamic" />
    <br /><br />
    
    <asp:LinkButton ID="UppdateraLinkButton" CssClass="action-button shadow animate green" runat="server" Text="Boka nu" OnClick="UppdateraLinkButton_Click" />
    <asp:LinkButton ID="TillbakaLinkButton" CssClass="action-button shadow animate blue" runat="server" Text="Tillbaka" OnClick="TillbakaLinkButton_Click" CausesValidation="false" />
</asp:Content>
