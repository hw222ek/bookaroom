<%@ Page Title="" Language="C#" MasterPageFile="~/Shared/SiteMaster.Master" AutoEventWireup="true" CodeBehind="CustomerListing.aspx.cs" Inherits="BookARoom.Pages.CustomerPages.CustomerListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    
    <asp:LinkButton CssClass="action-button shadow animate blue right" ID="LogoutLinkButton" runat="server" OnClick="LogoutButton_Click" Text="Logga ut"/>

    <%--RUBRIK--%>
    <h2>Kundlista</h2>

    <%--MEDDELANDE & VALIDERINGSSUMMERING--%>
    <asp:Panel runat="server" ID="MessagePanel" Visible="false" CssClass="GoodNews">
        <div class="MessageDiv">
            <asp:Literal runat="server" ID="MessageLiteral" />
            <asp:Button ID="MessageButton" CssClass="MessageButton" runat="server" Text="X" CausesValidation="False" OnClick="MessageButton_Click" />
        </div>
    </asp:Panel>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="ValidationSummary"/>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="ValEdit" ShowModelStateErrors="false" CssClass="ValidationSummary" DisplayMode="List" HeaderText="<strong>Fel! Korrigera uppdatera kund</strong>" />
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="ValOne" ShowModelStateErrors="false" CssClass="ValidationSummary" DisplayMode="List" HeaderText="<strong>Fel! Korrigera ny kund</strong>" />

    <%--LISTVIEW--%>
    <asp:ListView ID="CustomerListView" runat="server" 
        ItemType="BookARoom.Model.BLL.Customer" 
        SelectMethod="CustomerListView_GetData" 
        InsertMethod="CustomerListView_InsertItem" 
        UpdateMethod="CustomerListView_UpdateItem" 
        DataKeyNames="CustomerId" 
        OnItemDataBound="CustomerListView_ItemDataBound" 
        InsertItemPosition="FirstItem">
        <LayoutTemplate>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
        </LayoutTemplate>
         <InsertItemTemplate>
             <div class="InsertCustomer">
                 <h3 class="NewHeader">Lägg till ny kund</h3>
                 <table>
                    <tr>
                         <td class="CTypeTd">
                             <asp:TextBox ID="NameTextBox" runat="server" placeholder="Kundnamn" MaxLength="20" Text='<%#: BindItem.Name %>' />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="ValError" SetFocusOnError="true"  Text="*" ErrorMessage="Fyll i ett kundnamn." ControlToValidate="NameTextBox" Display="Dynamic" ValidationGroup="ValOne" />
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="ValError" SetFocusOnError="true"  Text="*" ErrorMessage="Kundnamn får endast innehålla tecken A-Z och 0-9" ControlToValidate="NameTextBox" Display="Dynamic" ValidationGroup="ValOne" ValidationExpression='^[0-9A-ZÅÄÖa-zåäö ]+$' />
                         </td>
                         <td class="CTypeTd">
                             <asp:DropDownList CssClass="BeautyDrop" ID="DropDownList1" runat="server" SelectedValue='<%#: BindItem.CustomerTypeId %>' >
                                 <asp:ListItem Value="1" Selected="True">Intern</asp:ListItem>
                                 <asp:ListItem Value="2">Extern</asp:ListItem>
                             </asp:DropDownList>
                         </td>
                     </tr>
                     <tr>
                         <td>
                             <asp:LinkButton runat="server" CssClass="action-button shadow animate green" Text="Spara kund" CommandName="Insert" ValidationGroup="ValOne" />
                             <asp:LinkButton runat="server" CssClass="action-button shadow animate red" CommandName="Cancel" Text="Rensa" CausesValidation="false" />
                         </td>
                         <td></td>
                     </tr>
                 </table>
             </div>
        </InsertItemTemplate>
        <ItemTemplate>
            <div class="BookingContainer">

                <div class="InlineNames">
                    <asp:Label ID="CustomerNameLabel" runat="server" Text='<%#: Item.Name %>' />
                </div>

                <div class="InlineBlock Type<%#: Item.CustomerTypeId %>">
                    <asp:Label ID="CustomerTypeLabel" runat="server" Text="{0}" />
                </div>

                <div class="InlineBlock">
                    <asp:HyperLink runat="server" CssClass="action-button shadow animate blue" NavigateUrl='<%# GetRouteUrl("CustomerDetails", new { id = Item.CustomerId })  %>' Text='Detaljer' />
                </div>

                <div class="InlineBlock">
                    <asp:LinkButton CssClass="action-button shadow animate red" runat="server" CommandName="Edit" Text="Ändra" CausesValidation="false" />
                </div>
            </div>
             </ItemTemplate>
        <EditItemTemplate>
            <div class="BookingContainer">

                <div class="InlineNames">
                    <asp:TextBox ID="EditNameTextBox" runat="server" MaxLength="20" Text='<%#: BindItem.Name %>' ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="ValError" SetFocusOnError="true" Text="*" ErrorMessage="Fyll i ett kundnamn." ControlToValidate="EditNameTextBox" Display="Dynamic" ValidationGroup="ValEdit" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="ValError" SetFocusOnError="true"  Text="*" ErrorMessage="Kundnamn får endast innehålla tecken A-Z och 0-9" ControlToValidate="EditNameTextBox" Display="Dynamic" ValidationGroup="ValEdit" ValidationExpression="^[0-9A-ZÅÄÖa-zåäö ]+$" />
                </div>

                <div class="InlineBlock TypeEdit">
                    <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%#: BindItem.CustomerTypeId %>' >
                        <asp:ListItem Value="1">Intern</asp:ListItem>
                        <asp:ListItem Value="2">Extern</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="InlineBlock">
                     <asp:LinkButton runat="server" CssClass="action-button shadow animate green" CommandName="Update" Text="Spara" ValidationGroup="ValEdit" />
                </div>

                <div class="InlineBlock">
                    <asp:LinkButton runat="server" CssClass="action-button shadow animate red" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                </div>
            </div>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table id="CustomerTable">
                <tr>
                    <td>
                        Kunduppgifter saknas.
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>
