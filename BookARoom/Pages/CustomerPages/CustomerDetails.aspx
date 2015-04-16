<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SiteMaster.Master" AutoEventWireup="true" CodeBehind="CustomerDetails.aspx.cs" Inherits="BookARoom.Pages.CustomerPages.CustomerDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    
    <%--RUBRIK--%>
    <h2>Kunduppgift</h2>
     <p>
        Välj "Ta bort" för att radera kunden. Tänk på att alla raderingar är permanenta. Välj tillbaka för att se kundlistan igen där du också kan lägga till nya kunder (Vilket är händigt om du råkar radera en kund).
    </p>

    <%--VALIDERINGSSUMMERING--%>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="ValidationSummary" />

    <%--LISTVIEW--%>
    <asp:ListView ID="CustomerListView" runat="server"
        ItemType="BookARoom.Model.BLL.Customer"
        SelectMethod="CustomerListView_GetItem" 
        OnItemDataBound="CustomerListView_ItemDataBound">
        <ItemTemplate>
            <h4><%#: Item.Name %></h4>

            <div class="TypeDetail<%#: Item.CustomerTypeId %>">
                <asp:Label  ID="CustomerTypeLabel" runat="server" Text="{0}" />
            </div>

            <br /><br />
            <div>
                <asp:HyperLink runat="server" CssClass="action-button shadow animate red" Text="Ta bort" NavigateUrl='<%# GetRouteUrl("CustomerDelete", new { id = Item.CustomerId }) %>' />
                <asp:HyperLink runat="server" CssClass="action-button shadow animate blue" Text="Tillbaka" NavigateUrl='<%# GetRouteUrl("CustomerListing", null)%>' />
            </div>  
        </ItemTemplate>
        <EmptyDataTemplate>
            <div class="InlineNames">
                    <p>
                        Kunddata saknas
                    </p>
            </div>
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>
