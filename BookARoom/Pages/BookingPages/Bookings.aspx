<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Bookings.aspx.cs" Inherits="BookARoom.Pages.Default" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="BDP" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

    <div class="LeftWing">
        <%--RUBRIK--%>
        <div class="RoomPresenter">
            <div class="HeaderStyled">
                <asp:Label ID="RoomLabel" runat="server" Text="{0}" />
            </div>
            <div class="DateHeader">
                <asp:Label ID="DateLabel" runat="server" Text="{0}" />
            </div>
        </div>

        <%--MEDDELANDEPANEL & VALIDERINGSSUMMARY--%>
        <asp:Panel runat="server" ID="MessagePanel" Visible="false" CssClass="GoodNews">
            <div class="MessageDiv">
                <asp:Literal runat="server" ID="MessageLiteral" />
                <asp:Button ID="MessageButton" CssClass="MessageButton" runat="server" Text="X" CausesValidation="False" OnClick="MessageButton_Click" />
            </div>
        </asp:Panel>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="ValidationSummary" />

        <%--LISTVIEW--%>
        <asp:ListView ID="BookingsListView" runat="server"
            ItemType="BookARoom.Model.BLL.Booking"
            SelectMethod="BookingsListView_GetData"
            DataKeyNames="RoomId, CustomerId, StartTime"
            OnItemDataBound="BookingsListView_ItemDataBound">
            <LayoutTemplate>
                <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
            </LayoutTemplate>
            <ItemTemplate>
                <div class="BookingContainer">
                    <div class="InlineBlock x">
                        <strong>
                            <asp:Label ID="Label1" runat="server" Text='<%# Item.StartTime.ToString("HH") %>' />
                            <asp:Label ID="Label3" runat="server" Text=" - " />
                            <asp:Label ID="Label2" runat="server" Text='<%# Item.EndTime.AddMinutes(1).ToString("HH") %>' /></strong>
                    </div>

                    <div class='Yepp Customer<%# Item.CustomerId %>'></div>

                    <div class="InlineNames">
                        <asp:Label ID="CustomerLabel" runat="server" Text="{0}" />
                    </div>

                    <div class="InlineBlock">
                        <asp:HyperLink ID="DetailsLink" runat="server" Text="Not loaded" />
                    </div>
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
    </div>

    <div class="RightWing">
        <div class="DateContainer">
            <h3>Datum</h3>
            <p class="CalendarText">1. Klicka på kalenderbilden</p>
            <p class="CalendarText">2. Välj ett datum</p>
            <p class="CalendarText">3. Klicka "Visa datum"</p>
            <br />

            <BDP:BDPLite ID="DatePicker" runat="server" />
            <br />
            <br />
            <asp:LinkButton CssClass="action-button shadow animate blue" ID="DateLinkButton" runat="server" Text="Visa datum" OnClick="DateButton_Click" />
        </div>

        <div class="RoomLinksContainer">
            <h3>Mötesrum</h3>
            <asp:DropDownList CssClass="BeautyDrop" ID="RoomsDropDownList" runat="server" OnSelectedIndexChanged="RoomsDropDownList_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem>Välj rum</asp:ListItem>
                <asp:ListItem Value="1">Mölle</asp:ListItem>
                <asp:ListItem Value="2">Ven</asp:ListItem>
                <asp:ListItem Value="3">Kärnan</asp:ListItem>
                <asp:ListItem Value="11">Projektor</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
</asp:Content>
