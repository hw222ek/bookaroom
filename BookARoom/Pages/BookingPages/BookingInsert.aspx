<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SiteMaster.Master" AutoEventWireup="true" CodeBehind="BookingInsert.aspx.cs" Inherits="BookARoom.Pages.BookingPages.BookingInsert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    
    <%--RUBRIK--%>
    <h2>Utför bokning</h2>
    <p>
        Börja med att säkerställa att du valt rätt tid för din bokning (det går att ångra sig). Är du säker väljer du ditt företagsnamn i rullisten och trycker "Boka nu". Vill du vänta trycker du "Tillbaka" för att återgå till bokningssidan.
    </p>
    <p>
        Hittar du inte ditt namn i rullisten kontaktar du en administratör som genast med ett leende lägger till dig.
    </p>

    <%--VALIDERINGSSUMMERING--%>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="ValidationSummary" />
    
    <%--BOKNINGSDETALJER--%>
    <asp:Label CssClass="HeaderLabel" ID="Label1" runat="server" Text="Bokningsdetaljer"/><br />
    <asp:Label ID="TimeLabel" runat="server" />
    <br /><br />

    <%--VÄLJ KUNDNAMN (INKL. VALIDERING) OCH BOKA--%>
    <asp:Label ID="Label2" runat="server" Text="Organisation/Företag:"/>
    <asp:DropDownList CssClass="BeautyDrop" ID="CustomerDropDownList" runat="server" ItemType="BookARoom.Model.BLL.Customer" ViewStateMode="Enabled" AppendDataBoundItems="true" >
        <asp:ListItem Value="-1">Välj organisation</asp:ListItem>
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="ValError" ErrorMessage="Ett kundnamn måste väljas." ControlToValidate="CustomerDropDownList" Text="*" Display="Dynamic" />
    <asp:RangeValidator ID="RangeValidator1" runat="server" CssClass="ValError" Text="*" ErrorMessage="Välj ett kundnamn." ControlToValidate="CustomerDropDownList" Display="Dynamic" MinimumValue="0" MaximumValue="2147483647" Type="Integer" />
    <br /><br />
    
    <asp:Label ID="Label3" runat="server" Text="Bokningsansvarig:"/>
    <asp:TextBox ID="PersonTextBox" runat="server" MaxLength="50"/>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Fyll i bokningsansvarig person" ControlToValidate="PersonTextBox" />

    <br /><br />
    <asp:LinkButton ID="BokaLinkButton" CssClass="action-button shadow animate green" runat="server" Text="Boka nu" OnClick="BokaButton_Click"/>
    <asp:LinkButton ID="TillbakaLinkButton" CssClass="action-button shadow animate blue" runat="server" Text="Tillbaka" OnClick="TillbakaLinkButton_Click" CausesValidation="false" />
</asp:Content>
