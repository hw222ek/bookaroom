<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SiteMaster.Master" AutoEventWireup="true" CodeBehind="BookingDetails.aspx.cs" Inherits="BookARoom.Pages.BookingPages.BookingDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    
    <%--RUBRIK & KORT BESKRIVNING--%>
    <h2>Bokningsinformation</h2>
    <p>
        Välj "Ta bort" för att radera en bokning. Tänk på att inte radera andras bokningar - då kan du uppfattas som osympatisk. Välj tillbaka för att se dagens bokningar där du kan välja en annan tid eller bokning.
    </p>

    <%--VALIDERINGSSUMMERING--%>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="ValidationSummary" />

    <%--LISTVIEW--%>
    <asp:ListView ID="BookingListView" runat="server"
        ItemType="BookARoom.Model.BLL.Booking"
        SelectMethod="BookingListView_GetData" 
        OnItemDataBound="BookingListView_ItemDataBound">
        <ItemTemplate>
            <asp:Label CssClass="HeaderLabel" ID="Label1" runat="server" Text="Bokningsdetaljer"/><br />
            <asp:Label ID="InfoLabel" runat="server">
                Datum: 20<%#: Item.StartTime.ToString("yy-MM-dd") %> Klockslag: <%#: Item.StartTime.ToString("HH:mm") %> - <%#: Item.EndTime.AddMinutes(1).ToString("HH:mm") %>
            </asp:Label>
            
            <br /><br /><asp:Label CssClass="HeaderLabel" ID="Label2" runat="server" Text="Organisation"/><br />
            <asp:Label  ID="CustomerLabel" runat="server" Text="{0}" /><br /><br />

            <asp:Label CssClass="HeaderLabel" ID="Label3" runat="server" Text="Bokningsansvarig"/><br />
            <span><%#: Item.Person.ToString() %></span><br /><br />

            <div>
                <asp:HyperLink runat="server" CssClass="action-button shadow animate red" Text="Ta bort" NavigateUrl='<%# GetRouteUrl("BookingDelete", new { room = Item.RoomId,id = Item.CustomerId, date = Item.StartTime.ToString("yyyy-MM-dd"), hour = Item.StartTime.ToString("HH") }) %>' />
                <%--<asp:HyperLink runat="server" CssClass="action-button shadow animate green" Text="Byt tid" NavigateUrl='<%# GetRouteUrl("BookingUpdate", new { id = Item.CustomerId, date = Item.StartTime.ToString("yyyy-MM-dd"), hour = Item.StartTime.ToString("HH") })%>' />--%>
                <asp:HyperLink runat="server" CssClass="action-button shadow animate blue" Text="Tillbaka" NavigateUrl='<%# GetRouteUrl("Bookings", new { room = Item.RoomId, date = Item.StartTime.ToString("yyyy-MM-dd") })%>' />   
            </div>  
        </ItemTemplate>
        <EmptyDataTemplate>
            <div class="InlineNames">
                    <p>
                        Bokningsdata saknas
                    </p>
            </div>
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>
